using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.StartupServices;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.BloodSampleResults
{
    public class BloodSampleResultService : IBloodSampleResultService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IBloodBankRegistrationService BloodBankRegistrationService;
        private readonly ILogger<BloodSampleResultService> _logger;

        public BloodSampleResultService(BloodBankDataContext dataContext, IBloodBankRegistrationService bloodBankRegistrationService, ILogger<BloodSampleResultService> logger)
        {
            this.dataContext = dataContext;
            this.BloodBankRegistrationService = bloodBankRegistrationService;
            this._logger = logger;
        }



        public async Task<ErrorOr<List<BloodSampleResult>>> UpsertBloodSampleResult(List<BloodSampleResult> results)
        {
            try
            {


                var firstItem = results.First();
                var toStatus = firstItem.Status;
                var doBilling = false;
                var isAntiBodyTest = false;
                if (toStatus == "SentToHSA")
                {
                    isAntiBodyTest = results.Any(x => x.ParentTestId == 0 && x.TestId == GlobalConstants.AntibodyScreeningId);
                }
                var bloodBankRegistration = dataContext.BloodBankRegistrations.First(x => x.RegistrationId == firstItem.BloodBankRegistrationId);
                for (var i = 0; i < results.Count; i++)
                {
                    var result = results[i];
                    var currentResult = dataContext.BloodSampleResults.FirstOrDefault(x => x.Identifier == result.Identifier);
                    if (currentResult != null)
                    {
                        currentResult.BloodBankRegistrationId = result.BloodBankRegistrationId;
                        currentResult.ContainerId = result.ContainerId;
                        currentResult.TestId = result.TestId;
                        currentResult.ParentTestId = result.ParentTestId;
                        currentResult.InventoryId = result.InventoryId;
                        currentResult.TestName = result.TestName;
                        currentResult.Unit = result.Unit;
                        currentResult.TestValue = result.TestValue;
                        currentResult.Comments = result.Comments;
                        currentResult.BarCode = result.BarCode;
                        if (result.Status != "")
                            currentResult.Status = result.Status;
                        currentResult.IsRejected = result.IsRejected;
                        currentResult.ModifiedBy = result.ModifiedBy;
                        currentResult.ModifiedByUserName = result.ModifiedByUserName;
                        currentResult.LastModifiedDateTime = result.LastModifiedDateTime;
                        currentResult.ReCheck = result.ReCheck;
                        if (result.interfaceispicked != null)
                            currentResult.interfaceispicked = result.interfaceispicked;
                        if (result.IsUploadAvail != null)
                            currentResult.IsUploadAvail = result.IsUploadAvail;
                        if (toStatus == "SentToHSA")
                            currentResult.SentToHSA = true;
                        if (toStatus == "ResultValidated" && currentResult.IsBilled == false)
                        {
                            doBilling = true;
                            currentResult.IsBilled = true;
                        }

                        dataContext.BloodSampleResults.Update(currentResult);

                        //billing. 
                        if (doBilling && toStatus == "ResultValidated" && currentResult.ParentTestId == 0 && currentResult.ParentRegistrationId == null)
                        {
                            var serviceType = currentResult.GroupId > 0 ? "G" : "T";
                            BloodBankBilling? existingBilling;
                            existingBilling = serviceType == "T" ?
                                  dataContext.BloodBankBillings.FirstOrDefault(x => x.BloodBankRegistrationId == currentResult.BloodBankRegistrationId && x.TestId == currentResult.TestId && x.ServiceType == serviceType)
                                : dataContext.BloodBankBillings.FirstOrDefault(x => x.BloodBankRegistrationId == currentResult.BloodBankRegistrationId && x.TestId == currentResult.GroupId && x.ServiceType == serviceType);
                            if (existingBilling == null)
                            {
                                var rate = 0.0m;
                                long serviceId = 0;
                                if (serviceType == "T")
                                {
                                    rate = GlobalConstants.Tests.First(t => t.TestNo == currentResult.TestId).Rate;
                                    serviceId = currentResult.TestId;
                                }
                                else if (serviceType == "G")
                                {
                                    rate = GlobalConstants.Groups.First(t => t.GroupNo == currentResult.GroupId).GroupRate;
                                    serviceId = currentResult.GroupId;
                                }
                                if (GlobalConstants.Tariffs.Any(y => y.ServiceType == serviceType && y.ProductId == serviceId && y.ResidenceId == bloodBankRegistration.ResidenceStatusId))
                                {
                                    rate = GlobalConstants.Tariffs.First(y => y.ServiceType == serviceType && y.ProductId == serviceId && y.ResidenceId == bloodBankRegistration.ResidenceStatusId).MRP;
                                }
                                var billing = new BloodBankBilling(result.BloodBankRegistrationId, 0, (serviceType == "T" ? currentResult.TestId : currentResult.GroupId), bloodBankRegistration.ClinicId.GetValueOrDefault(), currentResult.Identifier.ToString(), rate, 1, rate, toStatus, currentResult.ModifiedBy, currentResult.ModifiedByUserName, currentResult.LastModifiedDateTime, serviceType);
                                dataContext.BloodBankBillings.Add(billing);

                            }
                            else
                            {
                                if (serviceType == "T" && HelperMethods.IsCrosMatchingTest(currentResult.TestId))
                                {
                                    existingBilling.Unit = existingBilling.Unit + 1;
                                    existingBilling.Price = existingBilling.MRP * existingBilling.Unit;
                                }
                            }
                            dataContext.SaveChanges();
                        }

                    }
                    else
                    {
                        await dataContext.BloodSampleResults.AddAsync(result);
                    }
                    if (result.ParentTestId == 0)
                    {
                        var registrationTransaction = new RegistrationTransaction(result.BloodBankRegistrationId, result.Status, result.TestId.ToString(), result.ModifiedBy, "BloodSampleResult", result.Identifier, result.Status, result.ModifiedByUserName, result.LastModifiedDateTime);
                        dataContext.RegistrationTransactions.Add(registrationTransaction);
                    }
                };

                await this.dataContext.SaveChangesAsync();
                if (toStatus == "SampleProcessed")
                {
                    await BloodBankRegistrationService.UpdateRegistrationStatus(results.Select(x => x.BloodBankRegistrationId).ToList(), "SampleProcessed", results.First().ModifiedBy, results.First().ModifiedByUserName);
                }
                else if (toStatus == "SampleReceived")
                {
                    await BloodBankRegistrationService.UpdateRegistrationStatus(results.Select(x => x.BloodBankRegistrationId).ToList(), "SampleReceived", results.First().ModifiedBy, results.First().ModifiedByUserName);
                }
                else if (toStatus == "ResultValidated")
                {
                    var resultIds = results.Select(x => x.Identifier).ToList();
                    var allowedStatuses = new List<string>() { "OnHold", "SampleReceived", "SampleProcessed" };
                    var bloodSampleInventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.BloodBankRegistrationId && resultIds.Any(row => row == x.BloodSampleResultId) && allowedStatuses.Any(row => row == x.Status)).ToList();
                    bloodSampleInventories.ForEach(x => x.Status = toStatus);
                    var resultData = dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == firstItem.BloodBankRegistrationId).ToList();
                    var products = dataContext.RegisteredProducts.Where(x => x.BloodBankRegistrationId == firstItem.BloodBankRegistrationId).ToList();
                    var isAllResultValidated = resultData.Count > 0 && resultData.All(x => x.Status == "ResultValidated");
                    var sampleInventories = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == firstItem.BloodBankRegistrationId).ToList();
                    var productCount = products.Sum(x => x.Unit);
                    var isAllProductAssigned = productCount == sampleInventories.Count();
                    var isAllProductIssuedResult = sampleInventories.Count > 0 && sampleInventories.All(x => x.Status == "ProductIssued" || x.Status == "Returned" || x.Status == "Transfused");
                    var isAllProductIssued = isAllProductAssigned && isAllProductIssuedResult && isAllResultValidated;
                    if (isAllResultValidated)
                    {
                        await this.UpdateBloodSampleInventoriesStatus(firstItem.BloodBankRegistrationId);
                        if (!isAllProductIssued)
                        {
                            var status = products.Count == 0 ? "Completed" : "ResultValidated";
                            if (bloodBankRegistration.Status == "Completed" || bloodBankRegistration.Status == "Expired" || bloodBankRegistration.Status == "Rejected" || bloodBankRegistration.Status == "InventoryAssigned" || bloodBankRegistration.Status == "ProductIssued") status = bloodBankRegistration.Status;
                            await this.BloodBankRegistrationService.UpdateRegistrationStatus(new List<long>() { firstItem.BloodBankRegistrationId }, status, firstItem.ModifiedBy, firstItem.ModifiedByUserName);
                            await this.UpdateBloodBankPatient(resultData);
                        }
                        else
                        {
                            var statusToUpdate = "ProductIssued";
                            if (bloodBankRegistration.Status == "Completed" || bloodBankRegistration.Status == "Expired" || bloodBankRegistration.Status == "Rejected" || bloodBankRegistration.Status == "InventoryAssigned" || bloodBankRegistration.Status == "ProductIssued") statusToUpdate = bloodBankRegistration.Status;
                            await this.BloodBankRegistrationService.UpdateRegistrationStatus(new List<long>() { firstItem.BloodBankRegistrationId }, statusToUpdate, firstItem.ModifiedBy, firstItem.ModifiedByUserName);
                            var registration = dataContext.BloodBankRegistrations.Find(firstItem.BloodBankRegistrationId);
                            if (registration != null && registration.IsEmergency) await this.UpdateBloodBankPatient(resultData);
                        }

                    }

                }
                else if (toStatus == "SentToHSA")
                {
                    if (isAntiBodyTest)
                    {
                        var isPresent = dataContext.BloodSampleResults.Any(x => x.BloodBankRegistrationId == firstItem.BloodBankRegistrationId && x.ParentTestId == 0 && x.TestId == GlobalConstants.AntibodyIdentifiedId);
                        if (!isPresent)
                        {
                            var itemsToUpsert = new List<BloodSampleResult>();
                            var parentTest = GlobalConstants.Tests.FirstOrDefault(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId);
                            var subTests = GlobalConstants.SubTests.Where(x => x.TestNo == GlobalConstants.AntibodyIdentifiedId);
                            if (parentTest != null)
                            {
                                itemsToUpsert.Add(new BloodSampleResult(0, firstItem.BloodBankRegistrationId, 0, parentTest.TestNo, 0, 0, parentTest.TestName, parentTest.UnitName, "", "", "", "Registered", false, firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, false, null, 0, 0, null));
                                itemsToUpsert.AddRange(subTests.Select(x => new BloodSampleResult(0, firstItem.BloodBankRegistrationId, 0, x.SubTestNo, x.TestNo, 0, x.SubTestName, x.UnitName, "", "", "", "Registered", false, firstItem.ModifiedBy, firstItem.ModifiedByUserName, firstItem.LastModifiedDateTime, false, null, 0, 0, null)));
                                dataContext.BloodSampleResults.UpdateRange(itemsToUpsert);
                            }
                        }

                    }
                }
                await dataContext.SaveChangesAsync();

            }
            catch (Exception exp)
            {

            }
            return results;
        }

        public async Task<bool> UpdateBloodSampleInventoriesStatus(Int64 registrationId)
        {
            var bloodSampleinventories = await dataContext.BloodSampleInventories.Where(x => x.RegistrationId == registrationId).ToListAsync();
            var bloodSampleResults = await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == registrationId).ToListAsync();
            bloodSampleinventories.ForEach(sampleInventory =>
            {
                sampleInventory.IsComplete = true;
                var sampleInventoryResult = bloodSampleResults.FirstOrDefault(x => x.InventoryId == sampleInventory.InventoryId && HelperMethods.GetCrossMatchingInterpretationTestId(sampleInventory.CrossMatchingTestId) == x.TestId);
                if (sampleInventoryResult != null && sampleInventoryResult.TestValue == "Compatible")
                {
                    sampleInventory.IsMatched = true;
                }
                dataContext.BloodSampleInventories.Update(sampleInventory);
            });
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBloodBankPatient(List<BloodSampleResult> results)
        {
            try
            {
                var registration = dataContext.BloodBankRegistrations.Find(results.First().BloodBankRegistrationId);
                if (registration == null) return false;
                var bloodbankPatient = dataContext.BloodBankPatients.Find(registration.BloodBankPatientId);
                if (bloodbankPatient == null) return false;
                var antibodyScreeningResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.AntibodyScreeningInterpretationId && result.ParentTestId == GlobalConstants.AntibodyScreeningId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var antibodyIdentifiedResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.AntibodyIdentifiedInterpretationId && result.ParentTestId == GlobalConstants.AntibodyIdentifiedId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var coldAntibodyIdentifiedResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.ColdAntibodyIdentifiedInterpretationId && result.ParentTestId == GlobalConstants.AntibodyIdentifiedId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var antibodyIdentified2Result = results.FirstOrDefault(result => result.TestId == GlobalConstants.AntibodyIdentified2InterpretationId && result.ParentTestId == GlobalConstants.AntibodyIdentifiedId2 && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var coldAntibodyIdentified2Result = results.FirstOrDefault(result => result.TestId == GlobalConstants.ColdAntibodyIdentified2InterpretationId && result.ParentTestId == GlobalConstants.AntibodyIdentifiedId2 && result.ParentRegistrationId.GetValueOrDefault() == 0);

                var bloodGroupingResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.BloodGroupingRHInterpretationId && result.ParentTestId == GlobalConstants.BloodGroupingRHId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var bloodGroupingAutoResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.BloodGroupingRHAutoInterpretationId && result.ParentTestId == GlobalConstants.BloodGroupingRHAutoId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var aboConfirmationResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.ABOConfirmationInterpretationId && result.ParentTestId == GlobalConstants.ABOConfirmationId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var babyBloodGroupingAutoResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.BabyBloodGroupingRHAutoInterpretationId && result.ParentTestId == GlobalConstants.BabyBloodGroupingRHAutoId && result.ParentRegistrationId.GetValueOrDefault() == 0);
                var babyAboConfirmationResult = results.FirstOrDefault(result => result.TestId == GlobalConstants.BabyABOConfirmationInterpretationId && result.ParentTestId == GlobalConstants.BabyABOConfirmationId && result.ParentRegistrationId.GetValueOrDefault() == 0);

                var modifiedDateTime = results.FirstOrDefault()?.LastModifiedDateTime ?? DateTime.Now;
                if (modifiedDateTime == DateTime.MinValue) modifiedDateTime = DateTime.Now;
                _logger.LogInformation("TRACKER: Updating Patient with NRIC {NRICNumber} and Registration Lab Accession Number as  {LabAccessionNumber}", bloodbankPatient.NRICNumber, registration.LabAccessionNumber);

                if (antibodyScreeningResult != null)
                {
                    bloodbankPatient.AntibodyScreening = antibodyScreeningResult.TestValue;
                    bloodbankPatient.LatestAntibodyScreeningDateTime = modifiedDateTime;
                    if (bloodbankPatient.AntibodyScreening == "Positive")
                        bloodbankPatient.AntibodyScreeningDateTime = modifiedDateTime;
                }
                if (antibodyIdentifiedResult != null)
                {
                    var currentAntibodies = (bloodbankPatient.AntibodyIdentified ?? "").Split(",").ToList();
                    var newAntibodies = antibodyIdentifiedResult.TestValue.Split(",").ToList();
                    var finalAntibodies = currentAntibodies.Union(newAntibodies);
                    bloodbankPatient.AntibodyIdentified = string.Join(",", finalAntibodies.Where(x => x != ""));
                }
                if (coldAntibodyIdentifiedResult != null)
                {
                    var currentAntibodies = (bloodbankPatient.ColdAntibodyIdentified ?? "").Split(",").ToList();
                    var newAntibodies = coldAntibodyIdentifiedResult.TestValue.Split(",").ToList();
                    var finalAntibodies = currentAntibodies.Union(newAntibodies);
                    bloodbankPatient.ColdAntibodyIdentified = string.Join(",", finalAntibodies.Where(x => x != ""));
                }
                if (antibodyScreeningResult != null)
                {
                    bloodbankPatient.AntibodyScreening = antibodyScreeningResult.TestValue;
                    bloodbankPatient.LatestAntibodyScreeningDateTime = modifiedDateTime;
                    if (bloodbankPatient.AntibodyScreening == "Positive")
                        bloodbankPatient.AntibodyScreeningDateTime = modifiedDateTime;
                }
                if (antibodyIdentified2Result != null)
                {
                    var currentAntibodies = (bloodbankPatient.AntibodyIdentified ?? "").Split(",").ToList();
                    var newAntibodies = antibodyIdentified2Result.TestValue.Split(",").ToList();
                    var finalAntibodies = currentAntibodies.Union(newAntibodies);
                    bloodbankPatient.AntibodyIdentified = string.Join(",", finalAntibodies.Where(x => x != ""));
                }
                if (coldAntibodyIdentified2Result != null)
                {
                    var currentAntibodies = (bloodbankPatient.ColdAntibodyIdentified ?? "").Split(",").ToList();
                    var newAntibodies = coldAntibodyIdentified2Result.TestValue.Split(",").ToList();
                    var finalAntibodies = currentAntibodies.Union(newAntibodies);
                    bloodbankPatient.ColdAntibodyIdentified = string.Join(",", finalAntibodies.Where(x => x != ""));
                }
                var newBloodGroup = "";
                var noOfIterations = 0;
                if (bloodGroupingResult != null)
                {
                    noOfIterations = noOfIterations + 1;
                    if (bloodbankPatient.BloodGroup.Trim().ToLower() != bloodGroupingResult.TestValue.Trim().ToLower())
                    {
                        newBloodGroup = bloodGroupingResult.TestValue;
                    }
                }
                if (bloodGroupingAutoResult != null)
                {
                    noOfIterations = noOfIterations + 1;
                    if (bloodbankPatient.BloodGroup.Trim().ToLower() != bloodGroupingAutoResult.TestValue.Trim().ToLower())
                    {
                        newBloodGroup = bloodGroupingAutoResult.TestValue;
                    }
                }
                if (aboConfirmationResult != null)
                {
                    noOfIterations = noOfIterations + 1;
                    if (bloodbankPatient.BloodGroup.Trim().ToLower() != aboConfirmationResult.TestValue.Trim().ToLower())
                    {
                        newBloodGroup = aboConfirmationResult.TestValue;
                    }
                }
                if (babyBloodGroupingAutoResult != null)
                {
                    noOfIterations = noOfIterations + 1;
                    if (bloodbankPatient.BloodGroup.Trim().ToLower() != babyBloodGroupingAutoResult.TestValue.Trim().ToLower())
                    {
                        newBloodGroup = babyBloodGroupingAutoResult.TestValue;
                    }
                }
                if (babyAboConfirmationResult != null)
                {
                    noOfIterations = noOfIterations + 1;
                    if (bloodbankPatient.BloodGroup.Trim().ToLower() != babyAboConfirmationResult.TestValue.Trim().ToLower())
                    {
                        newBloodGroup = babyAboConfirmationResult.TestValue;
                    }
                }
                if (bloodGroupingResult != null || bloodGroupingAutoResult != null || aboConfirmationResult != null || babyBloodGroupingAutoResult != null || babyAboConfirmationResult != null)
                {
                    _logger.LogInformation("TRACKER: BloodGrouping Test Present --> {BloodGroup} and No Of Iterations as ---> {NoOfIterations}", bloodbankPatient.BloodGroup, bloodbankPatient.NoOfIterations);
                    if (newBloodGroup != "")
                    {
                        bloodbankPatient.BloodGroup = newBloodGroup;
                        bloodbankPatient.NoOfIterations = noOfIterations;
                    }
                    else
                        bloodbankPatient.NoOfIterations = bloodbankPatient.NoOfIterations + noOfIterations;
                    bloodbankPatient.BloodGroupingDateTime = modifiedDateTime;
                }
                _logger.LogInformation("TRACKER: patient BloodGroup --> {BloodGroup}  and No Of Iterations as ---> {NoOfIterations}", bloodbankPatient.BloodGroup, bloodbankPatient.NoOfIterations);


                await dataContext.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _logger.LogInformation("TRACKER: patient BloodGroup --> {BloodGroup}  and No Of Iterations as ---> {NoOfIterations} exception", exp.Message, exp.Message);
            }

            return true;
        }

        public async Task<ErrorOr<List<BloodSampleResult>>> AddBloodSampleResults(List<BloodSampleResult> request)
        {
            await dataContext.BloodSampleResults.AddRangeAsync(request);
            await dataContext.SaveChangesAsync();
            return request.ToList();
        }

        public async Task<ErrorOr<List<BloodSampleResult>>> UpsertBloodSampleResultStatus(List<long> bloodSampleResultIds, string status)
        {
            var bloodSampleResults = await dataContext.BloodSampleResults.Where(x => bloodSampleResultIds.Any(y => x.Identifier == y)).ToListAsync();
            bloodSampleResults.ForEach(result => result.Status = status);
            await dataContext.SaveChangesAsync();
            return bloodSampleResults;
        }
    }
}
