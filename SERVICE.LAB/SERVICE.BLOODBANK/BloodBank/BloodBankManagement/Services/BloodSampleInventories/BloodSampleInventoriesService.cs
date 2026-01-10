using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.BloodSampleResults;
using BloodBankManagement.Services.StartupServices;
using BloodBankManagement.Services.BloodBankInventories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.BloodSampleInventories
{
    public class BloodSampleInventoriesService : IBloodSampleInventoriesService
    {
        private readonly BloodBankDataContext dataContext;
        protected readonly IBloodBankRegistrationService BloodBankRegistrationService;
        protected readonly IBloodSampleResultService bloodSampleResultService;
        private readonly ILogger<BloodSampleInventoriesService> _logger;

        protected readonly IBloodBankInventoryService bloodBankInventoryService;

        public BloodSampleInventoriesService(BloodBankDataContext dataContext, IBloodBankRegistrationService bloodBankRegistrationService, IBloodSampleResultService _bloodSampleResultService, IBloodBankInventoryService _bloodBankInventoryService, ILogger<BloodSampleInventoriesService> logger)
        {
            this.dataContext = dataContext;
            this.BloodBankRegistrationService = bloodBankRegistrationService;
            this.bloodSampleResultService = _bloodSampleResultService;
            this.bloodBankInventoryService = _bloodBankInventoryService;
            this._logger = logger;
        }
        //1. CALLED DURING INVENTORY ASSIGNMENT SAVE...
        public async Task<ErrorOr<bool>> UpdateInventoriesWithAssignments(List<UpsertInventoryRequest> input)
        {
            var inventoriesToMarkAsAvailable = new List<Int64>();
            var firstItem = input.First();
            var bbRegistration = dataContext.BloodBankRegistrations.Find(firstItem.RegistrationId);
            if (bbRegistration == null) return ServiceErrors.Errors.BloodBankRegistration.NotFound;
            //1. Update BloodSampleInventories if REDCELLS or Add if NON-REDCELLS
            for (var i = 0; i < input.Count; i++)
            {
                var productDetail = input[i];
                if (productDetail.IsRedCells && !bbRegistration.IsEmergency)
                {
                    var inventoryIdentifiers = productDetail.InventoriesData.Select(x => x.InventoryId).ToList();
                    var returnValue = await dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.RegistrationId && inventoryIdentifiers.Any(y => y == x.InventoryId)).ToListAsync();
                    returnValue.ForEach(x =>
                    {
                        var productItem = input.FirstOrDefault(item => item.InventoriesData.Select(y => y.InventoryId).Any(id => id == x.InventoryId));
                        var inputInventoryDetail = productItem?.InventoriesData.FirstOrDefault(item => item.InventoryId == x.InventoryId);
                        Int64 productId = 0;
                        if (productItem != null)
                            productId = productItem.ProductId;
                        else
                            inventoriesToMarkAsAvailable.Add(x.InventoryId);

                        x.Status = "InventoryAssigned";
                        x.ProductId = productId;
                        x.ModifiedBy = firstItem.ModifiedBy;
                        x.ModifiedByUserName = firstItem.ModifiedByUserName;
                        x.LastModifiedDateTime = DateTime.Now;
                        x.Comments = inputInventoryDetail != null && inputInventoryDetail.Comments != null ? inputInventoryDetail.Comments : x.Comments;
                        x.CompatibilityValidTill = inputInventoryDetail != null && inputInventoryDetail.CompatibilityValidTill != null ? inputInventoryDetail.CompatibilityValidTill : x.CompatibilityValidTill;
                        x.IssuedDateTime = inputInventoryDetail != null && inputInventoryDetail.IssuedDateTime != null ? inputInventoryDetail.IssuedDateTime : x.IssuedDateTime;
                        if (x.ProductId != 0)
                        {
                            dataContext.BloodSampleInventories.Update(x);
                            var registrationTransaction = new RegistrationTransaction(firstItem.RegistrationId, x.Status, x.Comments, x.ModifiedBy, "BloodSampleInventories", x.Identifier, "UpdateBloodSampleInventories", x.ModifiedByUserName, x.LastModifiedDateTime);
                            dataContext.RegistrationTransactions.Add(registrationTransaction);
                        }
                        else
                        {
                            dataContext.BloodSampleInventories.Remove(x);
                        }

                    });
                    var sampleInventoriesToUpdate = await dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.RegistrationId).ToListAsync();
                    var countOfProducts = await dataContext.RegisteredProducts.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).ToListAsync();
                    var bloodSampleResultsToUpdate = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).ToListAsync();
                    var itemsToRemoved = new List<BloodSampleInventory>();
                    var resultsToBeRemoved = new List<BloodSampleResult>();
                    sampleInventoriesToUpdate.Where(x => x.Status == "Registered" || x.Status == "SampleReceived" || x.Status == "SampleProcessed" || x.Status == "ResultValidated").ToList().ForEach(si =>
                    {
                        var currentProductCount = countOfProducts.First(y => y.ProductId == si.ProductId).Unit;
                        var assignedProductCount = sampleInventoriesToUpdate.Where(z => z.ProductId == si.ProductId).Count(y => y.Status == "CaseCancel" || y.Status == "InventoryAssigned" || y.Status == "ProductIssued" || y.Status == "Returned" || y.Status == "Transfused");
                        if (currentProductCount == assignedProductCount)
                        {
                            inventoriesToMarkAsAvailable.Add(si.InventoryId);
                            itemsToRemoved.Add(si);
                            resultsToBeRemoved.AddRange(bloodSampleResultsToUpdate.Where(y => y.InventoryId == si.InventoryId).ToList());
                        }
                    });
                    if (itemsToRemoved.Count > 0) dataContext.BloodSampleInventories.RemoveRange(itemsToRemoved);
                    if (resultsToBeRemoved.Count > 0) dataContext.BloodSampleResults.RemoveRange(resultsToBeRemoved);
                }
                else
                {
                    //get the registration related crossmatchingtestid 

                    var crossMatchingTestId = GlobalConstants.CrossMatchingXMId;
                    var crossMatchingTests = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId && (x.TestId == GlobalConstants.CrossMatchingXMId || x.TestId == GlobalConstants.CrossMatchingManualXMId || x.TestId == GlobalConstants.CrossMatchingImmediateSpinXMId) && x.ParentRegistrationId == null && x.ParentTestId == 0).ToListAsync();
                    if (crossMatchingTests != null && crossMatchingTests.Count > 0)
                    {
                        crossMatchingTestId = crossMatchingTests.First().TestId;
                    }
                    var isRedCellsProduct = GlobalConstants.RedCellsProducts.Any(x => x.Identifier == productDetail.ProductId);
                    var bloodSampleInventories = await dataContext.BloodSampleInventories.Where(y => y.RegistrationId == productDetail.RegistrationId).ToListAsync();
                    var itemsToUpsert = productDetail.InventoriesData
                        .Where(y =>
                        {
                            return !bloodSampleInventories.Any(z => z.InventoryId == y.InventoryId);
                        })
                        .Select(x =>
                        {
                            return new BloodSampleInventory(0, productDetail.RegistrationId, 0, productDetail.ProductId, x.InventoryId, true, true, x.Comments, "InventoryAssigned", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, (isRedCellsProduct ? crossMatchingTestId : 0), x.CompatibilityValidTill, x.IssuedDateTime);
                        }).ToList();

                    await this.UpsertBloodSampleInventoriesWithChecks(itemsToUpsert, bbRegistration.IsEmergency, isRedCellsProduct);
                }
            }
            await dataContext.SaveChangesAsync();

            //2. Update BloodBankInventories Status
            var inventoryIds = input.SelectMany(x => x.InventoriesData.Select(x => x.InventoryId)).ToList();
            await this.bloodBankInventoryService.UpdateInventoryStatus(inventoryIds, "Assigned", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, "Inventory Assigned to Registration with Lab Accession Number " + bbRegistration?.LabAccessionNumber);
            if (inventoriesToMarkAsAvailable.Any())
            {
                await this.bloodBankInventoryService.UpdateInventoryStatus(inventoriesToMarkAsAvailable, "available", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, "Inventory Disassociated from Lab Accession Number " + bbRegistration?.LabAccessionNumber + " and made as Avaiable.");
            }
            var nonRedCellsInventoryIds = input.SelectMany(x => x.InventoriesData.Where(row => row.ModifiedProductId != 0)).ToList();
            var inventoriesDBData = await this.dataContext.BloodBankInventories.Where(x => nonRedCellsInventoryIds.Select(x => x.InventoryId).Any(y => y == x.Identifier)).ToListAsync();
            inventoriesDBData.ForEach(inventory =>
            {
                var inputDetail = nonRedCellsInventoryIds.Find(x => x.InventoryId == inventory.Identifier);
                if (inputDetail != null)
                {
                    inventory.ModifiedProductId = inputDetail.ModifiedProductId;
                    var isThawedProduct = GlobalConstants.Products.Any(product => product.Identifier == inputDetail.ModifiedProductId && GlobalConstants.ThawedPlazmaCodes.Any(x => x == product.Code));
                    var isThawedCryoprecipitate = GlobalConstants.Products.Any(product => product.Identifier == inputDetail.ModifiedProductId && GlobalConstants.ThawedCryoprecipitateCodes.Any(x => x == product.Code));
                    var newExpirationDate = input.First().LastModifiedDateTime;
                    if (isThawedCryoprecipitate) newExpirationDate = input.First().LastModifiedDateTime.AddHours(6);
                    if (isThawedProduct) newExpirationDate = input.First().LastModifiedDateTime.AddDays(1);
                    if (inventory.ExpirationDateAndTime > newExpirationDate)
                        inventory.ExpirationDateAndTime = newExpirationDate;
                }
            });
            await dataContext.SaveChangesAsync();

            //3. Update BloodBankRegistrations Status
            var isAllSamplesValidated = dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).All(x => x.Status == "ResultValidated" || (bbRegistration != null && bbRegistration.IsEmergency));
            var sampleInventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.RegistrationId);
            var productCount = dataContext.RegisteredProducts.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).Sum(x => x.Unit);
            var isAllProductAssigned = productCount == sampleInventories.Count();
            var isAllSampleInventoriesAssigned = sampleInventories.All(x => x.Status == "CaseCancel" || x.Status == "InventoryAssigned" || x.Status == "ProductIssued" || x.Status == "Returned" || x.Status == "Transfused");
            if (isAllSamplesValidated && isAllSampleInventoriesAssigned && isAllProductAssigned)
            {
                if (bbRegistration != null)
                {
                    bbRegistration.Status = "InventoryAssigned";
                    dataContext.BloodBankRegistrations.Update(bbRegistration);
                    var registrationTransaction = new RegistrationTransaction(firstItem.RegistrationId, bbRegistration.Status, bbRegistration.Status, firstItem.ModifiedBy, "BloodBankRegistration", bbRegistration.RegistrationId, "Inventory Assigned", firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime);
                    dataContext.RegistrationTransactions.Add(registrationTransaction);
                }
                await this.dataContext.SaveChangesAsync();
            }
            return true;
        }

        //1. CALLED DURING PRODUCT ISSUE SAVE.
        //2. CALLED DURING BLOOD PRODUCT RETURN 
        //3. CALLED DURING BLOOD TRANSFUSION. 
        public async Task<ErrorOr<List<UpdateBloodSampleInventory>>> UpsertBloodSampleInventoriesStatus(Contracts.UpdateBloodSampleInventoryStatusRequest request)
        {
            var statuses = request.BloodSampleInventories.Select(x => x.Status);
            var toStatus = statuses.Distinct().Count() == 1 ? statuses.First() : statuses.Where(x => x != "CaseCancel").Distinct().First();
            var bloodSampleinventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == request.BloodSampleInventories.First().RegistrationId).ToList();
            var bloodBankRegistration = dataContext.BloodBankRegistrations.Where(x => x.RegistrationId == request.BloodSampleInventories.First().RegistrationId).Include(x => x.Products).First();
            request.BloodSampleInventories.ForEach(sample =>
            {
                var currentResult = bloodSampleinventories.FirstOrDefault(x => x.Identifier == sample.Identifier);
                if (currentResult != null)
                {

                    if (!request.IsTransfusionUpdatedPostCompletion)
                    {
                        currentResult.Status = sample.Status;
                        if (sample.Status == "Transfused" || sample.Status == "Returned")
                        {
                            currentResult.ReturnedByNurseId = request.NurseId;
                            if (sample.Status == "Transfused") currentResult.TransfusionDateTime = sample.TransfusionDateTime;
                            currentResult.TransfusionVolume = sample.Status == "Transfused" ? sample.TransfusionVolume ?? "" : "";
                        }
                        else if (sample.Status == "CaseCancel")
                        {
                            currentResult.ReturnedByNurseId = request.NurseId;
                        }
                        else
                            currentResult.IssuedToNurseId = request.NurseId;
                        if (request.WardId != null && request.WardId != 0)
                        {
                            if (sample.Status == "Transfused" || sample.Status == "Returned" || sample.Status == "CaseCancel")
                                currentResult.PostIssuedClinicId = request.WardId;
                            else
                                currentResult.ClinicId = request.WardId;
                        }
                        if (sample.Status == "ProductIssued")
                        {
                            var existingBilling = dataContext.BloodBankBillings.FirstOrDefault(x => x.BloodBankRegistrationId == currentResult.RegistrationId && x.ProductId == currentResult.ProductId);
                            if (existingBilling != null)
                            {
                                var existingEntities = existingBilling.EntityId.Split(',');
                                if (!existingEntities.Any(y => !string.IsNullOrEmpty(y) && y.ToString() == currentResult.InventoryId.ToString()))
                                {
                                    existingBilling.Unit = existingBilling.Unit + 1;
                                    existingBilling.Price = existingBilling.MRP * existingBilling.Unit;
                                    existingBilling.EntityId = existingBilling.EntityId + "," + currentResult.InventoryId.ToString();
                                    existingBilling.Status = sample.Status;
                                    existingBilling.ModifiedBy = sample.ModifiedBy;
                                    existingBilling.ModifiedByUserName = sample.ModifiedByUserName;
                                    existingBilling.LastModifiedDateTime = sample.LastModifiedDateTime.GetValueOrDefault();
                                }
                            }
                            else
                            {
                                var productDetails = GlobalConstants.Products.First(t => t.Identifier == currentResult.ProductId);
                                var tariff = bloodBankRegistration.Products.First(x => x.ProductId == currentResult.ProductId);
                                var billing = new BloodBankBilling(currentResult.RegistrationId, productDetails.Identifier, 0, request.WardId.GetValueOrDefault(), currentResult.InventoryId.ToString(), tariff.MRP, 1, tariff.MRP, toStatus, currentResult.ModifiedBy, currentResult.ModifiedByUserName, currentResult.LastModifiedDateTime, "P");
                                dataContext.BloodBankBillings.Add(billing);
                            }
                            dataContext.SaveChanges();
                        }

                    }
                    currentResult.IsTransfusionReaction = sample.IsTransfusionReaction;
                    currentResult.TransfusionComments = sample.Status == "Transfused" ? sample.TransfusionComments ?? "" : sample.Temperature ?? "";
                    currentResult.ModifiedBy = sample.ModifiedBy;
                    currentResult.ModifiedByUserName = sample.ModifiedByUserName;
                    currentResult.LastModifiedDateTime = sample.LastModifiedDateTime != null ? sample.LastModifiedDateTime.GetValueOrDefault() : DateTime.Now;
                    if (sample.CompatibilityValidTill != null) currentResult.CompatibilityValidTill = sample.CompatibilityValidTill;
                    if (sample.IssuedDateTime != null) currentResult.IssuedDateTime = sample.IssuedDateTime;
                    dataContext.BloodSampleInventories.Update(currentResult);

                    if (currentResult.IsTransfusionReaction)
                    {
                        var patientId = dataContext.BloodBankRegistrations.Find(currentResult.RegistrationId)?.BloodBankPatientId;
                        if (patientId != null && patientId > 0)
                        {
                            var patient = dataContext.BloodBankPatients.Find(patientId);
                            if (patient != null)
                                patient.IsTransfusionReaction = true;
                        }
                    }
                    var inventoryIds = request.BloodSampleInventories.Select(x => x.InventoryId).ToList();
                    var bloodBankInventories = dataContext.BloodBankInventories.Where(x => inventoryIds.Any(y => y == x.Identifier)).ToList();

                    request.BloodSampleInventories.Where(x => x.Status == "CaseCancel").ToList().ForEach(row =>
                    {
                        var inventory = bloodBankInventories.Find(x => x.Identifier == row.InventoryId);
                        if (inventory != null)
                        {
                            var product = GlobalConstants.Products.FirstOrDefault(x => x.Identifier == inventory.ProductCode);
                            var productName = product != null ? product.Description : inventory.ProductCode.ToString();
                            var comments = "Inventory Case Cancelled for Inventory with DonationId " + inventory.DonationId + " and Product " + productName;
                            var registrationTransaction = new RegistrationTransaction(currentResult.RegistrationId, currentResult.Status, comments, currentResult.ModifiedBy, "BloodSampleInventories", currentResult.Identifier, "BloodSampleInventories", currentResult.ModifiedByUserName, currentResult.LastModifiedDateTime);
                            dataContext.RegistrationTransactions.Add(registrationTransaction);
                        }

                    });

                    var registrationTransaction = new RegistrationTransaction(currentResult.RegistrationId, currentResult.Status, currentResult.TransfusionComments, currentResult.ModifiedBy, "BloodSampleInventories", currentResult.Identifier, "BloodSampleInventories", currentResult.ModifiedByUserName, currentResult.LastModifiedDateTime);
                    dataContext.RegistrationTransactions.Add(registrationTransaction);
                }
            });
            await this.dataContext.SaveChangesAsync();
            if (!request.IsTransfusionUpdatedPostCompletion)
            {
                var isAllProductReturnOrTransfused = bloodSampleinventories.All(x => x.Status == "Returned" || x.Status == "Transfused" || x.Status == "Expired" || x.Status == "CaseCancel");
                if (isAllProductReturnOrTransfused)
                {
                    var inventoryIds = request.BloodSampleInventories.Select(x => x.InventoryId).ToList();
                    var bloodBankInventories = await dataContext.BloodBankInventories.Where(x => inventoryIds.Any(y => y == x.Identifier)).ToListAsync();
                    bloodBankInventories.ForEach(inventory =>
                    {
                        var requestItem = request.BloodSampleInventories.First(x => x.InventoryId == inventory.Identifier);
                        inventory.Status = requestItem.Status == "CaseCancel" ? "available" : requestItem.Status;
                        inventory.ModifiedBy = requestItem.ModifiedBy;
                        inventory.ModifiedByUserName = requestItem.ModifiedByUserName;
                        inventory.LastModifiedDateTime = requestItem.LastModifiedDateTime != null ? requestItem.LastModifiedDateTime.GetValueOrDefault() : DateTime.Now;
                        if (requestItem.Status == "Returned")
                            inventory.Temprature = requestItem.Temperature ?? "";
                        dataContext.BloodBankInventories.Update(inventory);
                        var bbRegistration = dataContext.BloodBankRegistrations.Find(requestItem.RegistrationId);
                        var comments = (requestItem.Status == "Returned" ? "Inventory Returned with temprature as " + inventory.Temprature : "Inventory Transfused") + " from Registration with Lab Accession Number " + bbRegistration?.LabAccessionNumber;
                        var logStatus = requestItem.Status;
                        if (requestItem.Status == "CaseCancel")
                        {
                            logStatus = "available";
                            comments = "Case Cancelled and made as Available from Registration  " + bbRegistration?.LabAccessionNumber;
                        }
                        var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, logStatus, comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                        this.dataContext.bloodBankInventoryTransactions.Add(inventoryTransaction);
                    });
                    var isAllProductReturnOrTransfusedInBloodSampleInventories = bloodSampleinventories.Count > 0 && bloodSampleinventories.All(x => x.Status == "Returned" || x.Status == "Transfused" || x.Status == "Expired" || x.Status == "CaseCancel");
                    if (isAllProductReturnOrTransfusedInBloodSampleInventories)
                    {
                        var canUpdate = true;
                        if (request.IsEmergency) canUpdate = request.IsAllResultsCompleted;
                        if (canUpdate)
                            await this.BloodBankRegistrationService.UpdateRegistrationStatus(request.BloodSampleInventories.Select(x => x.RegistrationId).Distinct().ToList(), "Completed", request.BloodSampleInventories.First().ModifiedBy, request.BloodSampleInventories.First().ModifiedByUserName);
                    }
                    await this.dataContext.SaveChangesAsync();

                }
                else if (toStatus != "")
                {
                    var updateStatus = false;
                    var firstItem = request.BloodSampleInventories.First();
                    var bbRegistration = await dataContext.BloodBankRegistrations.FindAsync(firstItem.RegistrationId);
                    if (toStatus == Constants.ProductIssued || toStatus == "CaseCancel")
                    {
                        var updateResponse = await this.BloodBankRegistrationService.UpdateIssueProduct(request);
                        var isEmergency = updateResponse.Value.IsEmergency;
                        if (isEmergency && toStatus == Constants.ProductIssued && updateResponse.Value.Results.Any(x => HelperMethods.IsCrosMatchingTest(x.ParentTestId)))
                        {
                            var redCellsRelatedBloodSampleInventories = await GetRedCellsRelatedBloodSampleInventories(bloodSampleinventories.Where(x => x.Status != "CaseCancel").ToList());
                            await UpsertBloodSampleInventoriesWithChecks(redCellsRelatedBloodSampleInventories, true);
                        }

                        if (!updateResponse.IsError)
                        {
                            var isAllProductsIssuedResult = bloodSampleinventories.All(x => x.Status == "ProductIssued" || x.Status == "Expired" || x.Status == "CaseCancel" || x.Status == "Returned" || x.Status == "Transfused");
                            var isAllSamplesValidated = dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).All(x => x.Status == "ResultValidated" || (bbRegistration != null && bbRegistration.IsEmergency));
                            var sampleInventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.RegistrationId);
                            var productCount = dataContext.RegisteredProducts.Where(x => x.BloodBankRegistrationId == firstItem.RegistrationId).Sum(x => x.Unit);
                            var isAllProductAssigned = productCount == sampleInventories.Count();
                            var isAllSampleInventoriesAssigned = sampleInventories.All(x => x.Status == "CaseCancel" || x.Status == "InventoryAssigned" || x.Status == "ProductIssued" || x.Status == "Returned" || x.Status == "Transfused");

                            updateStatus = !isEmergency && (isAllSamplesValidated && isAllSampleInventoriesAssigned && isAllProductAssigned && isAllProductsIssuedResult);
                            if (updateStatus)
                                toStatus = Constants.ProductIssued;
                        }
                    }
                    await dataContext.SaveChangesAsync();

                    //2. Update BloodBankInventories Status
                    if (toStatus == Constants.ProductIssued || toStatus == "CaseCancel")
                    {
                        var inventoryIds = request.BloodSampleInventories.Where(x => x.Status != "CaseCancel").Select(x => x.InventoryId).ToList();
                        var caseCancelInventoryIds = request.BloodSampleInventories.Where(x => x.Status == "CaseCancel").Select(x => x.InventoryId).ToList();
                        await this.bloodBankInventoryService.UpdateInventoryStatus(inventoryIds, "Issued", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime ?? DateTime.Now, "Issued to Registration with Lab Accession Number " + bbRegistration?.LabAccessionNumber);
                        await this.bloodBankInventoryService.UpdateInventoryStatus(caseCancelInventoryIds, "available", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime ?? DateTime.Now, "Case Cancelled and made as Available from Registration " + bbRegistration?.LabAccessionNumber);
                    }
                    else if (toStatus == "Transfused" || toStatus == "Returned")
                    {
                        var inventoryIds = request.BloodSampleInventories.Where(x => x.Status != "CaseCancel").Select(x => x.InventoryId).ToList();
                        var bsInventories = request.BloodSampleInventories.Where(x => x.Status != "CaseCancel").ToList();
                        for (var i = 0; i < bsInventories.Count; i++)
                        {
                            var inventoryId = bsInventories[i];
                            await this.bloodBankInventoryService.UpdateInventoryStatus(new List<long>() { inventoryId.InventoryId }, inventoryId.Status, firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime ?? DateTime.Now, "Issued to Registration with Lab Accession Number " + bbRegistration?.LabAccessionNumber);
                        };
                    }
                    if (updateStatus)
                    {
                        await this.BloodBankRegistrationService.UpdateRegistrationStatus(request.BloodSampleInventories.Select(x => x.RegistrationId).Distinct().ToList(), toStatus, request.BloodSampleInventories.First().ModifiedBy, request.BloodSampleInventories.First().ModifiedByUserName);
                    }
                }
            }

            return request.BloodSampleInventories;
        }

        //1. CALLED DURING SAMPLE PROCESSING -> EDIT CROSS MATCHING -> DURING SAVE 
        //2. CALLED AS PART OF UpsertBloodSampleInventoriesStatus METHOD -> CALLED FROM PRODUCT ISSUE/BLOOD PRODUCT RETURN/BLOOD TRANSFUSION
        public async Task<ErrorOr<List<BloodSampleInventory>>> UpsertBloodSampleInventoriesWithChecks(List<BloodSampleInventory> bloodSampleInventories, bool isEmergency = false, bool isRedCellsProduct = true)
        {
            if (isRedCellsProduct)
            {
                //1. Update into BloodSamplesResults - assign inventoryId to the bloodSampleResults for crossmatching Tests.
                var updatedInventories = await this.SyncBloodSampleResultsData(bloodSampleInventories);
                //1.1 Add into BloodSampleResults - for the Inventories which is not assigned as part of above step. 
                //1.2 Update the BloodSampleResultId to the BloodSampleInventory.BloodSampleResultId column for association
                var newBloodSampleInventories = bloodSampleInventories.Where(bloodSampleInventory => !updatedInventories.Any(x => x == bloodSampleInventory.InventoryId)).ToList();
                if (newBloodSampleInventories.Count > 0)
                {
                    for (var i = 0; i < newBloodSampleInventories.Count; i++)
                    {
                        var bloodSampleInventory = newBloodSampleInventories[i];
                        var crossMatchingTest = GlobalConstants.Tests.FirstOrDefault(x => x.TestNo == bloodSampleInventory.CrossMatchingTestId);
                        var crossMatchingSubTests = GlobalConstants.SubTests.Where(x => x.TestNo == bloodSampleInventory.CrossMatchingTestId).ToList();
                        var isExists = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == bloodSampleInventory.RegistrationId && ((x.TestId == bloodSampleInventory.CrossMatchingTestId && x.ParentTestId == 0) || x.ParentTestId == bloodSampleInventory.CrossMatchingTestId) && x.InventoryId == bloodSampleInventory.InventoryId).ToListAsync();
                        if (isExists.Count == 0 && crossMatchingTest != null && crossMatchingSubTests != null && crossMatchingSubTests.Count > 0)
                        {
                            List<BloodSampleResult> newItemsToAdd = new List<BloodSampleResult>
                        {
                            new BloodSampleResult(0, bloodSampleInventory.RegistrationId, 0, crossMatchingTest.TestNo, 0, bloodSampleInventory.InventoryId, crossMatchingTest.TestName, "", "", "", "", "Registered", false, bloodSampleInventory.ModifiedBy, bloodSampleInventory.ModifiedByUserName, bloodSampleInventory.LastModifiedDateTime, false, null,0, 0, null)
                        };
                            newItemsToAdd.AddRange(crossMatchingSubTests.Select(x =>
                            {
                                return new BloodSampleResult(0, bloodSampleInventory.RegistrationId, 0, x.SubTestNo, x.TestNo, bloodSampleInventory.InventoryId, x.SubTestName, "", "", "", "", "Registered", false, bloodSampleInventory.ModifiedBy, bloodSampleInventory.ModifiedByUserName, bloodSampleInventory.LastModifiedDateTime, false, null, 0, 0, null);
                            }).ToList());

                            var bloodSampleResults = await bloodSampleResultService.UpsertBloodSampleResult(newItemsToAdd);
                            var bloodSampleResultId = bloodSampleResults.Value.FirstOrDefault(x => x.BloodBankRegistrationId == bloodSampleInventory.RegistrationId && x.InventoryId == bloodSampleInventory.InventoryId && x.ParentTestId == 0);
                            if (bloodSampleResultId != null)
                                bloodSampleInventory.BloodSampleResultId = bloodSampleResultId.Identifier;
                        }
                        else
                        {
                            var bloodSampleResultId = isExists.FirstOrDefault(x => x.BloodBankRegistrationId == bloodSampleInventory.RegistrationId && x.InventoryId == bloodSampleInventory.InventoryId && x.ParentTestId == 0);
                            if (bloodSampleResultId != null)
                                bloodSampleInventory.BloodSampleResultId = bloodSampleResultId.Identifier;
                        }
                    }
                }
                await this.dataContext.SaveChangesAsync();

            }

            //2. Add Or Update BloodSampleInventories 
            await UpsertBloodSampleInventories(bloodSampleInventories);

            //3. Update BloodBankInventories
            if (!isEmergency)
            {
                //2. Update BloodBankInventories Status as ONHOLD
                var inventoryIds = bloodSampleInventories.Select(x => x.InventoryId).ToList();
                var firstItem = bloodSampleInventories.First();
                var bbRegistration = await dataContext.BloodBankRegistrations.FindAsync(firstItem.RegistrationId);
                await this.bloodBankInventoryService.UpdateInventoryStatus(inventoryIds, "OnHold", firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, "On-Hold against Registration with Lab Accession Number " + bbRegistration?.LabAccessionNumber);
            }
            await dataContext.SaveChangesAsync();
            return bloodSampleInventories;
        }

        //1. CALLED DURING INVENTORY ASSIGNMENT SAVE... -> AS PART OF UpdateInventoriesWithAssignments METHOD
        //2. CALLED AS PART OF UpsertBloodSampleInventoriesWithChecks METHOD
        public async Task<ErrorOr<List<BloodSampleInventory>>> UpsertBloodSampleInventories(List<BloodSampleInventory> inventories)
        {
            for (var i = 0; i < inventories.Count; i++)
            {
                var result = inventories[i];
                var currentResult = dataContext.BloodSampleInventories.FirstOrDefault(x => x.Identifier == result.Identifier || (x.RegistrationId == result.RegistrationId && x.BloodSampleResultId == result.BloodSampleResultId && x.InventoryId == result.InventoryId));
                if (currentResult != null)
                {
                    currentResult.RegistrationId = result.RegistrationId;
                    currentResult.BloodSampleResultId = result.BloodSampleResultId;
                    currentResult.ProductId = result.ProductId;
                    currentResult.InventoryId = result.InventoryId;
                    currentResult.IsMatched = result.IsMatched;
                    currentResult.IsComplete = result.IsComplete;
                    currentResult.Comments = result.Comments;
                    currentResult.Status = result.Status;
                    currentResult.ModifiedBy = result.ModifiedBy;
                    currentResult.ModifiedByUserName = result.ModifiedByUserName;
                    currentResult.LastModifiedDateTime = result.LastModifiedDateTime;
                    currentResult.CompatibilityValidTill = result.CompatibilityValidTill != null ? result.CompatibilityValidTill : currentResult.CompatibilityValidTill;
                    currentResult.IssuedDateTime = result.IssuedDateTime != null ? result.IssuedDateTime : currentResult.IssuedDateTime;
                    dataContext.BloodSampleInventories.Update(currentResult);
                    var registrationTransaction = new RegistrationTransaction(result.RegistrationId, result.Status, result.Comments, result.ModifiedBy, "BloodSampleInventories", result.Identifier, "UpdateBloodSampleInventories", result.ModifiedByUserName, DateTime.Now);
                    dataContext.RegistrationTransactions.Add(registrationTransaction);
                }
                else
                {
                    await dataContext.BloodSampleInventories.AddAsync(result);
                    var registrationTransaction = new RegistrationTransaction(result.RegistrationId, result.Status, result.Comments, result.ModifiedBy, "BloodSampleInventories", result.Identifier, "AddBloodSampleInventories", result.ModifiedByUserName, DateTime.Now);
                    dataContext.RegistrationTransactions.Add(registrationTransaction);
                }
            };

            await this.dataContext.SaveChangesAsync();
            return inventories;
        }

        //1. CALLED AS PART OF UpsertBloodSampleInventoriesWithChecks METHOD
        private async Task<List<Int64>> SyncBloodSampleResultsData(List<BloodSampleInventory> bloodSampleInventories)
        {

            //1. get bloodbankresults for which inventory is not assigned. 
            //2. assign inventoryId to the bloodSampleResults for crossmatching Tests.
            var assignedInventories = new List<Int64>();
            var crossMatchingTests = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == bloodSampleInventories.First().RegistrationId && (((x.TestId == GlobalConstants.CrossMatchingXMId || x.TestId == GlobalConstants.CrossMatchingManualXMId || x.TestId == GlobalConstants.CrossMatchingImmediateSpinXMId) && x.ParentTestId == 0) || (x.ParentTestId == GlobalConstants.CrossMatchingXMId || x.ParentTestId == GlobalConstants.CrossMatchingManualXMId || x.ParentTestId == GlobalConstants.CrossMatchingImmediateSpinXMId)) && x.InventoryId == 0).ToListAsync();
            bloodSampleInventories.ForEach(bloodSampleInventory =>
            {
                var crossMatchingTest = crossMatchingTests.FirstOrDefault(x => x.TestId == bloodSampleInventory.CrossMatchingTestId && x.InventoryId == 0);
                if (crossMatchingTest != null)
                {
                    crossMatchingTest.InventoryId = bloodSampleInventory.InventoryId;
                    GlobalConstants.SubTests.Where(x => x.TestNo == bloodSampleInventory.CrossMatchingTestId).ToList().ForEach(subtest =>
                    {
                        var subtestData = crossMatchingTests.FirstOrDefault(x => x.TestId == subtest.SubTestNo && x.InventoryId == 0);
                        if (subtestData != null)
                            subtestData.InventoryId = bloodSampleInventory.InventoryId;
                    });
                    assignedInventories.Add(bloodSampleInventory.InventoryId);
                }
            });
            await dataContext.SaveChangesAsync();
            return assignedInventories;
        }

        private static List<Contracts.BloodBankRegistration> frameRegistrationResponse(List<Models.BloodBankRegistration> registrations, List<Models.BloodSampleInventory> bloodSampleInventories, List<Models.BloodBankInventory> inventories)
        {
            var response = registrations.Select(registration =>
            {
                var registrationBloodSampleInventories = bloodSampleInventories.Where(y => y.RegistrationId == registration.RegistrationId).ToList();
                var inventoryIds = registrationBloodSampleInventories.Select(x => x.InventoryId).ToList();
                var registrationInventories = inventories.Where(x => inventoryIds.Any(y => y == x.Identifier)).ToList();
                return MapPatientRegistrationResponse(registration, registrationBloodSampleInventories, registrationInventories);
            }).ToList();
            return response;
        }

        public async Task<List<BloodSampleInventory>> GetRedCellsRelatedBloodSampleInventories(List<BloodSampleInventory> input)
        {
            var inputInv = input.Select(y => y.InventoryId);
            var inventories = await dataContext.BloodBankInventories.Where(x => inputInv.Any(y => y == x.Identifier)).ToListAsync();
            var redCellsInventories = inventories.Where(inventory => GlobalConstants.RedCellsProducts.Any(x => x.Identifier == inventory.ProductCode));
            return input.Where(x => redCellsInventories.Any(y => y.Identifier == x.InventoryId)).ToList();
        }

        private static Contracts.BloodBankRegistration MapPatientRegistrationResponse(Models.BloodBankRegistration response, List<Models.BloodSampleInventory> bloodSampleInventories, List<Models.BloodBankInventory> inventories)
        {
            var products = response.Products != null && response.Products.Count > 0 ? response.Products.Select(product =>
            {
                var regBloodSampleInventories = bloodSampleInventories.Select(sampleResult =>
                {
                    var inventory = inventories.Find(inventory => inventory.Identifier == sampleResult.InventoryId);
                    Contracts.BloodBankInventoryResponse? inventoryResponse = null;
                    if (inventory != null)
                    {
                        inventoryResponse = new Contracts.BloodBankInventoryResponse(
                            inventory.Identifier,
                            inventory.BatchId,
                            inventory.DonationId,
                            inventory.CalculatedDonationId,
                            inventory.ProductCode,
                            inventory.ExpirationDateAndTime,
                            inventory.AboOnLabel,
                            inventory.Volume,
                            inventory.AntiAGrading,
                            inventory.AntiBGrading,
                            inventory.AntiABGrading,
                            inventory.AboResult,
                            inventory.AboPerformedByUserName,
                            inventory.AboPerformedByDateTime,
                            inventory.Status,
                            inventory.IsRejected,
                            inventory.IsVisualInspectionPassed,
                            inventory.Comments,
                            inventory.Temprature,
                            inventory.ModifiedBy,
                            inventory.ModifiedByUserName,
                            inventory.Antibodies,
                            inventory.ModifiedProductId,
                            inventory.LastModifiedDateTime,
                            null
                        );
                    }

                    return new Contracts.BloodSampleInventoryResponse(
                        sampleResult.Identifier,
                        sampleResult.RegistrationId,
                        sampleResult.BloodSampleResultId,
                        sampleResult.ProductId,
                        sampleResult.InventoryId,
                        sampleResult.IsMatched,
                        sampleResult.IsComplete,
                        sampleResult.Comments,
                        sampleResult.Status,
                        sampleResult.IssuedToNurseId,
                        sampleResult.ClinicId,
                        sampleResult.PostIssuedClinicId,
                        sampleResult.ReturnedByNurseId,
                        sampleResult.TransfusionDateTime,
                        sampleResult.TransfusionVolume,
                        sampleResult.IsTransfusionReaction,
                        sampleResult.TransfusionComments,
                        sampleResult.ModifiedBy,
                        sampleResult.ModifiedByUserName,
                        sampleResult.LastModifiedDateTime,
                        sampleResult.CrossMatchingTestId,
                        sampleResult.CompatibilityValidTill,
                        sampleResult.IssuedDateTime,
                        inventoryResponse
                    );
                }).ToList();
                return new PatientRegisteredProducts(product.Identifier, product.ProductId, product.MRP, product.Unit, product.Price, regBloodSampleInventories);
            }).ToList() : new List<PatientRegisteredProducts>();
            var results = response.Results != null && response.Results.Count > 0 ? response.Results.Select(result =>
            {
                return new BloodSampleResultResponse(result.Identifier, result.BloodBankRegistrationId, result.ContainerId, result.TestId, result.ParentTestId, result.InventoryId, result.TestName, result.Unit, result.TestValue, result.Comments, result.BarCode, result.Status, result.IsRejected, result.ParentRegistrationId, result.ModifiedBy, result.ModifiedByUserName, result.LastModifiedDateTime, result.ReCheck, result.SentToHSA, result.interfaceispicked, result.IsUploadAvail, result.GroupId);
            }).ToList() : new List<BloodSampleResultResponse>();
            var specialRequirements = response.SpecialRequirements != null && response.SpecialRequirements.Count > 0 ? response.SpecialRequirements.Select(specialRequirement =>
            {
                return new RegisteredSpecialRequirementResponse(specialRequirement.Identifier, specialRequirement.RegistrationId, specialRequirement.SpecialRequirementId, specialRequirement.Validity, specialRequirement.ModifiedBy, specialRequirement.ModifiedByUserName, specialRequirement.LastModifiedDateTime);
            }).ToList() : new List<RegisteredSpecialRequirementResponse>();
            var transactions = response.Transactions != null && response.Transactions.Count > 0 ? response.Transactions.Select(transaction =>
            {
                return new RegistrationTransactionResponse(transaction.Identifier, transaction.RegistrationStatus, transaction.Comments, transaction.ModifiedBy, transaction.EntityType, transaction.EntityId, transaction.EntityAction, transaction.RegistrationId, transaction.ModifiedByUserName, transaction.LastModifiedDateTime);
            }).ToList() : new List<RegistrationTransactionResponse>();
            var billings = response.Billings != null && response.Billings.Count > 0 ? response.Billings.Select(billing =>
            {
                return new BloodBankBillingResponse(billing.Identifier, billing.ProductId, billing.TestId, billing.ClinicId, billing.EntityId, billing.MRP, billing.Unit, billing.Price, billing.Status, billing.ModifiedBy, billing.ModifiedByUserName, billing.LastModifiedDateTime, billing.IsBilled, billing.ServiceType);
            }).ToList() : new List<BloodBankBillingResponse>();
            var patient = response.BloodBankPatient ?? new BloodBankPatient();
            var bloodSampleInventoriesRes = response.BloodSampleInventories != null && response.BloodSampleInventories.Count > 0 ? response.BloodSampleInventories.Select(bloodSample =>
            {
                return new BloodSampleInventoryResponse(bloodSample.Identifier, bloodSample.RegistrationId, bloodSample.BloodSampleResultId, bloodSample.ProductId, bloodSample.InventoryId, bloodSample.IsMatched, bloodSample.IsComplete, bloodSample.Comments, bloodSample.Status, bloodSample.IssuedToNurseId, bloodSample.ClinicId, bloodSample.PostIssuedClinicId, bloodSample.ReturnedByNurseId, bloodSample.TransfusionDateTime, bloodSample.TransfusionVolume, bloodSample.IsTransfusionReaction, bloodSample.TransfusionComments, bloodSample.ModifiedBy, bloodSample.ModifiedByUserName, bloodSample.LastModifiedDateTime, bloodSample.CrossMatchingTestId, bloodSample.CompatibilityValidTill, bloodSample.IssuedDateTime);
            }).ToList() : new List<BloodSampleInventoryResponse>();
            var patientDetails = new BloodBankPatientResponse(patient.Identifier, patient.NRICNumber, patient.PatientName, patient.PatientDOB, patient.NationalityId, patient.GenderId, patient.RaceId, patient.ResidenceStatusId, patient.BloodGroup, patient.NoOfIterations, patient.AntibodyScreening, patient.AntibodyIdentified, patient.ColdAntibodyIdentified, patient.ModifiedBy, patient.ModifiedByUserName, patient.IsTransfusionReaction, patient.Comments, patient.LastModifiedDateTime, patient.BloodGroupingDateTime, patient.AntibodyScreeningDateTime, patient.LatestAntibodyScreeningDateTime);
            return new Contracts.BloodBankRegistration(response.RegistrationId, response.NRICNumber, response.PatientName, response.PatientDOB, response.CaseOrVisitNumber, response.NationalityId, response.GenderId, response.RaceId, response.ResidenceStatusId, response.ClinicalDiagnosisId, response.IndicationOfTransfusionId, response.ClinicalDiagnosisOthers, response.IndicationOfTransfusionOthers, response.DoctorOthers, response.DoctorMCROthers, response.IsEmergency, response.WardId, response.ClinicId, response.DoctorId, products, specialRequirements, results, transactions, patientDetails, response.ProductTotal, response.IsActive,
            response.Status, response.NurseId, response.IssuingComments, response.LabAccessionNumber, response.ModifiedBy, response.ModifiedByUserName, response.LastModifiedDateTime, response.SampleReceivedDateTime, response.RegistrationDateTime, bloodSampleInventoriesRes, billings, response.PatientVisitNo, response.VisitId);

        }

        public async Task<ErrorOr<List<BloodSampleInventory>>> GetBloodSampleInventories(FetchBloodSampleInventoriesRequest request)
        {
            return await dataContext.BloodSampleInventories.Where(x => request.RegistrationIds.Any(id => id == x.RegistrationId) && (request.BloodSampleId == 0 || x.BloodSampleResultId == request.BloodSampleId)).ToListAsync();
        }

        public async Task<ErrorOr<List<Contracts.BloodBankRegistration>>> GetPatientHistory(FetchPatientHistoryRequest request)
        {
            try
            {
                var nricNumber = request.NRICNumber ?? "-";
                var registrations = new List<Models.BloodBankRegistration>();
                if (request.StartDate == null && request.EndDate == null)
                    registrations = await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber == nricNumber).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                else
                {
                    var registrationsQuery = from registration in dataContext.BloodBankRegistrations
                                             join transaction in dataContext.RegistrationTransactions
                                             on registration.RegistrationId equals transaction.RegistrationId
                                             where transaction.LastModifiedDateTime >= request.StartDate && transaction.LastModifiedDateTime <= request.EndDate && transaction.RegistrationStatus == request.Status
                                             select registration.RegistrationId;
                    if (request.Status == null && request.NRICNumber != null)
                    {
                        if (request.NRICNumber == "")
                        {
                            registrationsQuery = from registration in dataContext.BloodBankRegistrations
                                                 join transaction in dataContext.RegistrationTransactions
                                                 on registration.RegistrationId equals transaction.RegistrationId
                                                 where transaction.LastModifiedDateTime >= request.StartDate && transaction.LastModifiedDateTime <= request.EndDate
                                                 select registration.RegistrationId;

                        }
                        else
                        {
                            registrationsQuery = from registration in dataContext.BloodBankRegistrations
                                                 join transaction in dataContext.RegistrationTransactions
                                                 on registration.RegistrationId equals transaction.RegistrationId
                                                 where transaction.LastModifiedDateTime >= request.StartDate && transaction.LastModifiedDateTime <= request.EndDate && registration.NRICNumber == request.NRICNumber
                                                 select registration.RegistrationId;

                        }
                    }
                    var registrationids = registrationsQuery.ToList();
                    registrations = await dataContext.BloodBankRegistrations.Where(x => registrationids.Any(y => y == x.RegistrationId)).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.Billings).Include(x => x.SpecialRequirements).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                }

                var registrationIds = registrations.Select(x => x.RegistrationId).Distinct().ToList();
                var bloodSampleInventories = await dataContext.BloodSampleInventories.Where(x => registrationIds.Any(y => y == x.RegistrationId)).ToListAsync();
                var inventoryIds = bloodSampleInventories.Select(x => x.InventoryId).Distinct().ToList();
                var inventories = await dataContext.BloodBankInventories.Where(x => inventoryIds.Any(y => y == x.Identifier)).ToListAsync();
                var response = frameRegistrationResponse(registrations, bloodSampleInventories, inventories);
                return response;
            }
            catch (Exception exp)
            {
                _logger.LogInformation("Exception Message {ErrorMessage}", exp.Message);
            }
            return new List<Contracts.BloodBankRegistration>();
        }

        public async Task<ErrorOr<bool>> RemoveInventoryAssociation(RemoveInventoryAssociationRequest request)
        {
            var bloodSampleResultEntries = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == request.RegistrationId && x.InventoryId == request.InventoryId).ToListAsync();
            if (bloodSampleResultEntries.Count == 0) return false;
            var bloodSampleInventory = dataContext.BloodSampleInventories.First(x => x.RegistrationId == request.RegistrationId && x.InventoryId == request.InventoryId);
            var inventory = dataContext.BloodBankInventories.Find(request.InventoryId);
            if (bloodSampleInventory == null || inventory == null) return false;
            if (request.DeleteEntry)
            {
                dataContext.BloodSampleInventories.Remove(bloodSampleInventory);
                dataContext.BloodSampleResults.RemoveRange(bloodSampleResultEntries);
            }
            else
            {
                bloodSampleInventory.InventoryId = 0;
                bloodSampleResultEntries.ForEach(row => row.InventoryId = 0);
            }

            if (inventory != null)
            {
                inventory.Status = inventory.ExpirationDateAndTime <= DateTime.Now ? "Expired" : "available";
                inventory.ModifiedBy = request.ModifiedBy;
                inventory.ModifiedByUserName = request.ModifiedByUserName;
                inventory.LastModifiedDateTime = DateTime.Now;
                dataContext.BloodBankInventories.Update(inventory);
                var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, inventory.Comments, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime);
                await this.dataContext.bloodBankInventoryTransactions.AddAsync(inventoryTransaction);
            }
            var registrationTransaction = new RegistrationTransaction(request.RegistrationId, "SampleReceived", "Inventory with DonationId " + (inventory?.DonationId ?? "") + " has been removed from Cross Matching", request.ModifiedBy, "BloodBankRegistration", request.RegistrationId, "update", request.ModifiedByUserName, request.LastModifiedDateTime);
            dataContext.RegistrationTransactions.Add(registrationTransaction);
            await dataContext.SaveChangesAsync();
            return true;

        }
    }
}
