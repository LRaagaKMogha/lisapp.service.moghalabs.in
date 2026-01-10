using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Models;
using BloodBankManagement.Helpers;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using BloodBankManagement.Services.BloodBankPatients;
using BloodBankManagement.Services.StartupServices;
using BloodBankManagement.Services.BloodSampleInventories;
using BloodBankManagement.Services.BloodBankInventories;
using System.Linq.Expressions;

namespace BloodBankManagement.Services.BloodBankRegistrations
{
    public class BloodBankRegistrationService : IBloodBankRegistrationService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IConfiguration Configuration;
        private readonly IBloodBankPatientService bloodBankPatientService;
        private readonly IBloodBankInventoryService bloodBankInventoryService;

        public BloodBankRegistrationService(BloodBankDataContext dataContext, IConfiguration configuration, IBloodBankPatientService _bloodBankPatientService, IBloodBankInventoryService _bloodBankInventoryService)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
            this.bloodBankPatientService = _bloodBankPatientService;
            this.bloodBankInventoryService = _bloodBankInventoryService;
        }


        public async Task<bool> UpdateRegistrationStatus(List<Int64> registrationIds, string status, Int64 modifiedBy, string modifiedByUserName = "")
        {

            foreach (var registrationId in registrationIds.Distinct())
            {
                var canUpdate = true;
                if (status == "SampleProcessed")
                {
                    var registrationResults = await dataContext.BloodBankRegistrations.Where(x => registrationId == x.RegistrationId).Include(x => x.Results).ToListAsync();
                    if (registrationResults != null && registrationResults.Count > 0)
                    {
                        canUpdate = registrationResults.First().Results.Where(x => x.ParentRegistrationId.GetValueOrDefault() == 0).All(x => x.Status == "SampleProcessed" || x.Status == "ResultValidated");
                    }
                }
                if (canUpdate)
                {
                    var registration = dataContext.BloodBankRegistrations.Find(registrationId);
                    if (registration != null)
                    {
                        registration.Status = status;
                        registration.ModifiedBy = modifiedBy;
                        registration.ModifiedByUserName = modifiedByUserName;
                        registration.LastModifiedDateTime = DateTime.Now;
                        if (registration.SampleReceivedDateTime == null)
                        {
                            registration.SampleReceivedDateTime = DateTime.Now;
                        }
                        dataContext.BloodBankRegistrations.Update(registration);
                    }

                    var registrationTransaction = new RegistrationTransaction(registrationId, status, "", modifiedBy, "BloodBankRegistration", registrationId, "update", modifiedByUserName, DateTime.Now);
                    dataContext.RegistrationTransactions.Add(registrationTransaction);
                }
            }
            await dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<ErrorOr.ErrorOr<BloodBankRegistration>> CreatePatientRegistration(BloodBankRegistration incomingRegistration)
        {
            //bring transaction scope soon
            if (incomingRegistration.RegistrationId == 0)
            {
                List<Int64> nonRedCellsProductIds = GlobalConstants.Products.Where(x => x.CategoryId != GlobalConstants.RedCellCategoryId).Select(x => x.Identifier).ToList();
                var patient = await bloodBankPatientService.AddBloodBankPatient(incomingRegistration);
                if (patient.IsError) return patient.Errors;
                incomingRegistration.BloodBankPatientId = patient.Value.Identifier;
                incomingRegistration.SpecialRequirements.ToList().ForEach(x => x.PatientId = patient.Value.Identifier);
                incomingRegistration.LabAccessionNumber = GetLabAccessionNumber();
                dataContext.BloodBankRegistrations.Add(incomingRegistration);
                await this.dataContext.SaveChangesAsync();
                if (!incomingRegistration.IsEmergency)
                {
                    var results = incomingRegistration.Results.Where(x => x.ParentTestId == 0 && HelperMethods.IsCrosMatchingTest(x.TestId)).ToList();
                    for (var i = 0; i < results.Count; i++)
                    {
                        var result = results[i];
                        var itemToInsert = new BloodSampleInventory(0, incomingRegistration.RegistrationId, result.Identifier, 0, 0, false, false, "Registered", "Registered", incomingRegistration.ModifiedBy, incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime, result.TestId);
                        var ifExists = dataContext.BloodSampleInventories.Any(x => x.RegistrationId == incomingRegistration.RegistrationId && x.BloodSampleResultId == result.Identifier);
                        if (!ifExists)
                            await dataContext.BloodSampleInventories.AddAsync(itemToInsert);
                    };
                }
                else
                {
                    var registrationTransactionCreate = new RegistrationTransaction(incomingRegistration.RegistrationId, "Registered", "", incomingRegistration.ModifiedBy, "BloodBankRegistration", incomingRegistration.RegistrationId, "create", incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime);
                    dataContext.RegistrationTransactions.Add(registrationTransactionCreate);
                }
                var registrationTransaction = new RegistrationTransaction(incomingRegistration.RegistrationId, incomingRegistration.Status, "", incomingRegistration.ModifiedBy, "BloodBankRegistration", incomingRegistration.RegistrationId, "create", incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime);
                dataContext.RegistrationTransactions.Add(registrationTransaction);
                await this.dataContext.SaveChangesAsync();
                return incomingRegistration;
            }
            else
            {
                var isResidenceUpdated = false;
                var patient = await bloodBankPatientService.AddBloodBankPatient(incomingRegistration);
                if (patient.IsError) return patient.Errors;
                var patientToUpdate = (await GetPatientRegistration(incomingRegistration.RegistrationId)).Value;
                var partialEditStatuses = new List<string>() { "ResultValidated", "ProductIssued", "InventoryAssigned", "Completed", "Expired", "Rejected" };
                var partialUpdate = partialEditStatuses.Any(x => x == patientToUpdate.Status);
                var isProductUpdated = patientToUpdate.Products.Count != incomingRegistration.Products.Count || patientToUpdate.Products.Any(x => !incomingRegistration.Products.Any(y => y.ProductId == x.ProductId && y.Unit == x.Unit));
                var isResultUpdated = patientToUpdate.Results.Count != incomingRegistration.Results.Count || patientToUpdate.Results.Where(x => x.TestId != 0).Any(x => !incomingRegistration.Results.Where(y => y.TestId != 0).Any(y => y.TestId == x.TestId));

                if (!partialUpdate)
                {
                    if (!patientToUpdate.IsEmergency || patientToUpdate.Status == "ResultValidated")
                    {
                        var products = patientToUpdate.Products.ToList();
                        if (isProductUpdated)
                        {
                            var productRemovedCount = products.RemoveAll(x => !incomingRegistration.Products.Any(y => y.ProductId == x.ProductId));
                            products.AddRange(incomingRegistration.Products.Where(y => !patientToUpdate.Products.Any(x => y.ProductId == x.ProductId)));
                            products.ForEach(item =>
                            {
                                var inputProduct = incomingRegistration.Products.First(x => x.ProductId == item.ProductId);
                                if (item.Unit != inputProduct.Unit) isProductUpdated = true;
                                item.Unit = inputProduct.Unit;
                                item.Price = inputProduct.Price;
                                item.MRP = inputProduct.MRP;
                            });
                            patientToUpdate.Products = products;
                        }

                        if (isResultUpdated)
                        {
                            var results = patientToUpdate.Results.ToList();
                            var inventories = results.Select(x => x.InventoryId).Distinct().Where(x => x != 0).ToList();
                            await bloodBankInventoryService.UpdateInventoryStatus(inventories, "available", incomingRegistration.ModifiedBy, incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime);
                            var bloodSampleInventoriesToDelete = dataContext.BloodSampleInventories.Where(x => x.RegistrationId == incomingRegistration.RegistrationId).ToList();
                            dataContext.BloodSampleInventories.RemoveRange(bloodSampleInventoriesToDelete);
                            patientToUpdate.Results.Clear();
                            patientToUpdate.Results = incomingRegistration.Results.ToList();
                        }
                    }
                    if (patientToUpdate.IsEmergency)
                    {
                        if (patientToUpdate.Status == "SampleReceived" || patientToUpdate.Status == "SampleProcessed")
                        {
                            if (isProductUpdated || isResultUpdated) patientToUpdate.Status = "Registered";
                        }

                    }
                    else
                    {
                        if (isProductUpdated || isResultUpdated) patientToUpdate.Status = "Registered"; //for now, always moving to Registration status
                    }


                }
                patientToUpdate.PatientName = incomingRegistration.PatientName;
                patientToUpdate.PatientDOB = incomingRegistration.PatientDOB;
                patientToUpdate.GenderId = incomingRegistration.GenderId;
                patientToUpdate.NRICNumber = incomingRegistration.NRICNumber;
                patientToUpdate.NationalityId = incomingRegistration.NationalityId;
                patientToUpdate.RaceId = incomingRegistration.RaceId;
                isResidenceUpdated = patientToUpdate.ResidenceStatusId != incomingRegistration.ResidenceStatusId;
                patientToUpdate.ResidenceStatusId = incomingRegistration.ResidenceStatusId;
                patientToUpdate.ClinicalDiagnosisId = incomingRegistration.ClinicalDiagnosisId;
                patientToUpdate.ClinicalDiagnosisOthers = incomingRegistration.ClinicalDiagnosisOthers;
                patientToUpdate.IndicationOfTransfusionId = incomingRegistration.IndicationOfTransfusionId;
                patientToUpdate.IndicationOfTransfusionOthers = incomingRegistration.IndicationOfTransfusionOthers;
                patientToUpdate.DoctorOthers = incomingRegistration.DoctorOthers;
                patientToUpdate.DoctorMCROthers = incomingRegistration.DoctorMCROthers;
                patientToUpdate.IsEmergency = incomingRegistration.IsEmergency;
                patientToUpdate.CaseOrVisitNumber = incomingRegistration.CaseOrVisitNumber;
                patientToUpdate.WardId = incomingRegistration.WardId;
                patientToUpdate.ClinicId = incomingRegistration.ClinicId;
                patientToUpdate.DoctorId = incomingRegistration.DoctorId;

                incomingRegistration.SpecialRequirements.ToList().ForEach(row => row.PatientId = patientToUpdate.BloodBankPatientId);
                patientToUpdate.SpecialRequirements.Clear();
                patientToUpdate.SpecialRequirements = incomingRegistration.SpecialRequirements.ToList();

                if (isResidenceUpdated)
                {
                    patientToUpdate.Billings.ToList().ForEach(billing =>
                    {
                        var rate = billing.MRP;
                        var serviceType = billing.ServiceType;
                        if (billing.ProductId > 0)
                        {
                            var productDetails = GlobalConstants.Products.First(t => t.Identifier == billing.ProductId);
                            if (GlobalConstants.Tariffs.Any(y => y.ServiceType == serviceType && y.ProductId == billing.ProductId && y.ResidenceId == patientToUpdate.ResidenceStatusId))
                            {
                                rate = GlobalConstants.Tariffs.First(y => y.ServiceType == serviceType && y.ProductId == billing.ProductId && y.ResidenceId == patientToUpdate.ResidenceStatusId).MRP;
                            }
                        }
                        else if (billing.TestId > 0)
                        {
                            if (serviceType == "T")
                            {
                                rate = GlobalConstants.Tests.First(t => t.TestNo == billing.TestId).Rate;
                            }
                            else if (serviceType == "G")
                            {
                                rate = GlobalConstants.Groups.First(t => t.GroupNo == billing.TestId).GroupRate;
                            }
                            if (GlobalConstants.Tariffs.Any(y => y.ServiceType == serviceType && y.ProductId == billing.TestId && y.ResidenceId == patientToUpdate.ResidenceStatusId))
                            {
                                rate = GlobalConstants.Tariffs.First(y => y.ServiceType == serviceType && y.ProductId == billing.TestId && y.ResidenceId == patientToUpdate.ResidenceStatusId).MRP;
                            }
                        }
                        billing.MRP = rate;
                        billing.Price = billing.MRP * billing.Unit;
                    });
                }

                dataContext.BloodBankRegistrations.Update(patientToUpdate);
                await this.dataContext.SaveChangesAsync();
                if (isResultUpdated)
                {
                    if (!partialUpdate && !incomingRegistration.IsEmergency)
                    {
                        var results = patientToUpdate.Results.Where(x => x.ParentTestId == 0 && HelperMethods.IsCrosMatchingTest(x.TestId)).ToList();
                        for (var i = 0; i < results.Count; i++)
                        {
                            var result = results[i];
                            var itemToInsert = new BloodSampleInventory(0, incomingRegistration.RegistrationId, result.Identifier, 0, 0, false, false, "Registered", "Registered", incomingRegistration.ModifiedBy, incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime, result.TestId);
                            var ifExists = dataContext.BloodSampleInventories.Any(x => x.RegistrationId == incomingRegistration.RegistrationId && x.BloodSampleResultId == result.Identifier);
                            if (!ifExists)
                                await dataContext.BloodSampleInventories.AddAsync(itemToInsert);
                        };
                    }
                }
                var registrationTransaction = new RegistrationTransaction(incomingRegistration.RegistrationId, incomingRegistration.Status, "", incomingRegistration.ModifiedBy, "BloodBankRegistration", incomingRegistration.RegistrationId, "update", incomingRegistration.ModifiedByUserName, incomingRegistration.LastModifiedDateTime);
                dataContext.RegistrationTransactions.Add(registrationTransaction);
                await this.dataContext.SaveChangesAsync();
                return patientToUpdate;
            }

        }

        private string GetLabAccessionNumber()
        {
            var today = DateTime.Now;
            var prependString = "99" + today.Year.ToString().Substring(2) + today.Month.ToString().PadLeft(2, '0') + today.Day.ToString().PadLeft(2, '0');
            var isReset = dataContext.BloodBankRegistrations.Any(x => x.LabAccessionNumber == prependString + "5001");
            if (!isReset)
                dataContext.Database.ExecuteSqlRaw("ALTER SEQUENCE BarCodeId RESTART WITH 5001");

            var p = new SqlParameter("@result", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR BarCodeId", p);
            var nextVal = (int)p.Value;
            var id = nextVal.ToString().PadLeft(4, '0');
            var barcode = prependString + id;
            return barcode;
        }

        public async Task<ErrorOr<BloodBankRegistration>> GetPatientRegistration(Int64 id)
        {
            var data = await dataContext.BloodBankRegistrations.Where(x => x.RegistrationId == id).Include(x => x.BloodSampleInventories).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).Include(x => x.Billings).AsSplitQuery().ToListAsync();
            if (data != null) return data.First();
            return ServiceErrors.Errors.BloodBankRegistration.NotFound;
        }

        public async Task<ErrorOr<List<string>>> RecallRegistration(Int64 id)
        {
            var patientToUpdate = (await GetPatientRegistration(id)).Value;
            if (patientToUpdate.Status == "Registered") return new List<string>();
            var partialEditStatuses = new List<string>() { "ResultValidated", "ProductIssued", "InventoryAssigned" };

            patientToUpdate.Results.ToList().ForEach(x =>
            {
                var testData = GlobalConstants.Tests.FirstOrDefault(z => z.TestNo == x.TestId && x.ParentTestId == 0);
                if (testData != null && testData.IsNonReportable) x.Status = "ResultValidated";
                else x.Status = "Registered";
            });
            patientToUpdate.BloodSampleInventories.ToList().ForEach(x => { x.Status = "OnHold"; x.IsMatched = false; x.IsComplete = false; });
            patientToUpdate.Status = "SampleReceived";

            var aboTestIds = new List<Int64> { GlobalConstants.BloodGroupingRHId, GlobalConstants.BloodGroupingRHAutoId, GlobalConstants.ABOConfirmationId, GlobalConstants.BabyBloodGroupingRHAutoId, GlobalConstants.BabyABOConfirmationId };
            if (patientToUpdate.Results.Any(r => r.ParentTestId == 0 && aboTestIds.Any(t => t == r.TestId)) && patientToUpdate.BloodBankPatient.NoOfIterations == 1)
            {
                patientToUpdate.BloodBankPatient.BloodGroup = "-";
            }
            await dataContext.SaveChangesAsync();
            var relatedlabAccessionNos = await dataContext.BloodBankRegistrations.Where(x => x.BloodBankPatientId == patientToUpdate.BloodBankPatientId).ToListAsync();
            return relatedlabAccessionNos.Where(x => partialEditStatuses.Any(y => y == x.Status)).Select(x => x.LabAccessionNumber).ToList();
        }

        public async Task<ErrorOr<List<Contracts.TestResponse>>> GetBloodBankTests()
        {
            using (var context = new BloodBankDataContext(Configuration))
            {
                return await Task.Run(() => context.TestDetails.FromSqlRaw("Execute dbo.pro_GetBloodBankTests").ToList());
            }
        }

        public async Task<ErrorOr<List<Contracts.GroupResponse>>> GetBloodBankGroups()
        {
            using (var context = new BloodBankDataContext(Configuration))
            {
                return await Task.Run(() => context.GroupDetails.FromSqlRaw("Execute dbo.pro_GetBloodBankGroups").ToList());
            }
        }


        public async Task<ErrorOr<List<Contracts.SubTestResponse>>> GetBloodBankSubTests()
        {
            using (var context = new BloodBankDataContext(Configuration))
            {
                return await Task.Run(() => context.SubTestDetails.FromSqlRaw("Execute dbo.pro_GetBloodBankSubTests").ToList());
            }

        }

        public async Task<ErrorOr<List<Contracts.TestPickListResponse>>> GetBloodBankSubTestsPickList()
        {
            using (var context = new BloodBankDataContext(Configuration))
            {
                return await Task.Run(() => context.TestPickListResponses.FromSqlRaw("Execute dbo.pro_GetBloodBankSubTestsPickList").ToList());
            }

        }
        public async Task<ErrorOr<List<Contracts.UpdateRegistration>>> UpdateRegistrations(Contracts.UpdateBloodBankRegistrationStatusRequest request)
        {
            using (var dbContext = new BloodBankDataContext(Configuration))
            {
                for (var i = 0; i < request.Registrations.Count; i++)
                {
                    var registration = request.Registrations[i];
                    var registrationId = new SqlParameter("@RegistrationId", SqlDbType.Int) { Value = registration.RegistrationId };
                    var registrationStatus = new SqlParameter("@RegistrationStatus", SqlDbType.VarChar) { Value = registration.RegistrationStatus };
                    var rejectedReason = new SqlParameter("@RejectedReason", SqlDbType.NVarChar) { Value = registration.RejectedReason };
                    var isActive = new SqlParameter("@IsActive", SqlDbType.Int) { Value = 1 };
                    var modifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.Int) { Value = registration.ModifiedBy };
                    var modifiedByUserName = new SqlParameter("@ModifiedByUserName", SqlDbType.NVarChar) { Value = registration.ModifiedByUserName };
                    var lastModifiedDateTime = new SqlParameter("@LastModifiedDateTime", SqlDbType.DateTime) { Value = registration.LastModifiedDateTime ?? DateTime.Now };

                    await dbContext.Database.ExecuteSqlRawAsync("EXEC Update_BloodBankRegistrationStatus @RegistrationId, @RegistrationStatus, @RejectedReason, @IsActive, @ModifiedBy, @ModifiedByUserName,  @LastModifiedDateTime", registrationId, registrationStatus, rejectedReason, isActive, modifiedBy, modifiedByUserName, lastModifiedDateTime);
                    if (request.Action == "movetoassignment")
                    {
                        var bloodSampleResults = await dbContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == registration.RegistrationId).ToListAsync();
                        bloodSampleResults.ForEach(x => x.Status = "ResultValidated");
                    }
                    if (registration.SampleReceivedDateTime != null)
                    {
                        var registrationRow = dbContext.BloodBankRegistrations.Find(registration.RegistrationId);
                        if (registrationRow != null)
                        {
                            registrationRow.SampleReceivedDateTime = registration.SampleReceivedDateTime;
                            dbContext.BloodBankRegistrations.Update(registrationRow);
                        }
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            return request.Registrations;
        }
        public async Task<ErrorOr<BloodBankRegistration>> UpdateIssueProduct(Contracts.UpdateBloodSampleInventoryStatusRequest request)
        {
            var bloodBankRegistration = dataContext.BloodBankRegistrations.Include(x => x.Results).FirstOrDefault(x => x.RegistrationId == request.BloodSampleInventories.First().RegistrationId);
            if (bloodBankRegistration != null)
            {
                if (bloodBankRegistration.IsEmergency)
                {
                    var isAnyPendingProductToBeIssued = dataContext.BloodSampleInventories.Any(x => x.RegistrationId == bloodBankRegistration.RegistrationId && x.Status != "ProductIssued" && x.Status != "CaseCancel");
                    if (!isAnyPendingProductToBeIssued)
                    {
                        bloodBankRegistration.Status = "Registered";
                    }
                }
                bloodBankRegistration.NurseId = request.NurseId.GetValueOrDefault();
                bloodBankRegistration.ClinicId = request.WardId != null && request.WardId != 0 ? request.WardId : bloodBankRegistration.ClinicId;
                bloodBankRegistration.IssuingComments = !string.IsNullOrEmpty(request.IssuingComments) ? request.IssuingComments : bloodBankRegistration.IssuingComments;
                dataContext.BloodBankRegistrations.Update(bloodBankRegistration);
                var registrationTransaction = new RegistrationTransaction(bloodBankRegistration.RegistrationId, "ProductIssued", request.IssuingComments ?? "", request.BloodSampleInventories.First().ModifiedBy, "BloodBankRegistration", bloodBankRegistration.RegistrationId, "productIssued", request.BloodSampleInventories.First().ModifiedByUserName, request.BloodSampleInventories.First().LastModifiedDateTime.GetValueOrDefault());
                dataContext.RegistrationTransactions.Add(registrationTransaction);
                await dataContext.SaveChangesAsync();
                return bloodBankRegistration;
            }
            return ServiceErrors.Errors.BloodBankRegistration.NotFound;
        }

        public async Task<ErrorOr<List<BloodBankRegistration>>> GetBloodBankRegistrationsForResult(Contracts.FetchBloodSampleResultRequest request)
        {
            var response = new List<BloodBankRegistration>();
            var statuses = request.Statuses != null ? request.Statuses : new List<string>();
            var nricNumber = request.NRICNumber ?? "";
            if (request.IsAttachInSampleReceiving)
            {
                return await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber == nricNumber && x.Status != "Registered" && x.SampleReceivedDateTime != null && (x.SampleReceivedDateTime ?? DateTime.MinValue).Date.AddDays(3) >= DateTime.Now.Date).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
            }
            else if (request.IsRegistrationsList)
            {
                if (!string.IsNullOrEmpty(request.NRICNumber))
                {
                    response = await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber == request.NRICNumber).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                }
                else if (!string.IsNullOrEmpty(request.LabAccessionNumber))
                {
                    response = await dataContext.BloodBankRegistrations.Where(x => x.LabAccessionNumber == request.LabAccessionNumber).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                }
                else
                {
                    response = await dataContext.BloodBankRegistrations.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                }
                return response;
            }
            else if (request.RegistrationIds != null && request.RegistrationIds.Count > 0)
            {
                return await dataContext.BloodBankRegistrations.Where(x => request.RegistrationIds.Any(id => id == x.RegistrationId)).OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
            }
            else
            {
                //GENERAL SEARCH USED EVERYWHERE... 
                var query = dataContext.BloodBankRegistrations.Where(x => true);
                Expression<Func<BloodBankRegistration, bool>> dateFilterQuery = x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate;
                Expression<Func<BloodBankRegistration, bool>> statusQuery = e => statuses.Any(y => y == e.Status);
                Expression<Func<BloodBankRegistration, bool>> nricnumberQuery = e => e.NRICNumber.Contains(nricNumber);
                var isGeneralQuery = true;
                if (!string.IsNullOrEmpty(request.DonationId))
                {
                    var donationStatuses = new List<string>() { "Issued" };
                    donationStatuses.AddRange(statuses);
                    List<Int64> inventories = await dataContext.BloodBankInventories.Where(x => donationStatuses.Any(status => status == x.Status) && x.DonationId.Contains(request.DonationId)).Select(x => x.Identifier).ToListAsync();
                    List<Int64> filteredRegistrations = await dataContext.BloodSampleInventories.Where(row => inventories.Any(inventoryId => inventoryId == row.InventoryId)).Select(x => x.RegistrationId).Distinct().ToListAsync();

                    if (statuses.Count > 0) query = query.Where(statusQuery);
                    query = query.Where(x => filteredRegistrations.Any(row => row == x.RegistrationId));
                }
                else if (statuses.Count > 0)
                {
                    if (request.IsBloodProductReturnOrBloodTransfusion && !string.IsNullOrEmpty(request.NRICNumber))
                    {
                        if (statuses.Count > 0) query = query.Where(statusQuery);
                        if (!string.IsNullOrEmpty(nricNumber)) query = query.Where(nricnumberQuery);
                    }
                    else
                    {
                        List<Int64> nonRedCellsProductIds = GlobalConstants.Products.Where(x => x.CategoryId != GlobalConstants.RedCellCategoryId).Select(x => x.Identifier).ToList();
                        if (!string.IsNullOrEmpty(nricNumber)) query = query.Where(nricnumberQuery);
                        query = query.Where(dateFilterQuery);
                        var currentStatus = statuses.Count > 0 ? statuses[0] : "";
                        var notAllowedStatuses = new List<string>() { "Expired", "Rejected" };

                        if (currentStatus == "Registered")
                        {
                            query = query.Where(statusQuery);
                        }
                        else if (currentStatus == "SampleReceived")
                        {
                            query = query.Where(x =>
                                                (x.Status == currentStatus ||
                                                x.Results.Where(row => row.ParentTestId == 0).Any(y => y.Status == currentStatus)) && notAllowedStatuses.All(z => z != x.Status)

                            );
                        }
                        else if (currentStatus == "SampleProcessed")
                        {
                            query = query.Where(x =>
                                               (x.Status == currentStatus ||
                                               x.Results.Where(row => row.ParentTestId == 0).Any(y => y.Status == currentStatus))
                                               && !x.Results.Any(y => y.ReCheck) && notAllowedStatuses.All(z => z != x.Status)
                           );
                        }
                        else if (currentStatus == "ResultValidated")
                        {
                            var nonRedCellsRegistrationStatus = new List<string>() { "SampleReceived", "SampleProcessed" };
                            query = query.Where(x =>
                                                x.Status == currentStatus ||
                                                x.BloodSampleInventories.Any(y => y.Status == currentStatus) ||
                                                (nonRedCellsRegistrationStatus.Any(y => y == x.Status) && x.Products.Any(productID => nonRedCellsProductIds.Contains(productID.ProductId)))
                                );
                        }
                        else if (currentStatus == "InventoryAssigned" || currentStatus == "ProductIssued")
                        {
                            query = query.Where(x =>
                                            x.Status == currentStatus ||
                                            (!x.IsEmergency && x.BloodSampleInventories.Any(y => y.Status == currentStatus))
                                );
                        }
                        else
                        {
                            query = query.Where(statusQuery);
                        }
                    }

                }
                else //FOR RETURNING THE BLOODGROUP & ANTIBODY RESULTS ALONE
                {
                    isGeneralQuery = false;
                    var bloodGroupResultsRegistration = await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber == nricNumber && x.Results.Any(result => result.ParentTestId == 0 && result.TestId == GlobalConstants.BloodGroupingRHId && result.TestValue != "" && result.ParentRegistrationId == null && result.Status == "ResultValidated")).Take(1).Include(x => x.Results).AsSplitQuery().ToListAsync();
                    var antibodyResultsRegistration = await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber == nricNumber && x.Results.Any(result => result.ParentTestId == 0 && result.TestId == GlobalConstants.AntibodyScreeningId && result.TestValue != "" && result.ParentRegistrationId == null && result.Status == "ResultValidated")).Take(1).Include(x => x.Results).AsSplitQuery().ToListAsync();
                    if (bloodGroupResultsRegistration != null) response.AddRange(bloodGroupResultsRegistration);
                    if (antibodyResultsRegistration != null) response.AddRange(antibodyResultsRegistration);
                }
                if (isGeneralQuery)
                {
                    try
                    {
                        response = await query.OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Results).Include(x => x.Products).Include(x => x.SpecialRequirements).Include(x => x.Billings).Include(x => x.BloodBankPatient).AsSplitQuery().ToListAsync();
                    }
                    catch (Exception exp)
                    {

                    }
                }
                if (response != null && response.Count > 0)
                {
                    string Pathinit = this.Configuration.GetValue<string>(Constants.UploadPathInit);
                    if (Pathinit != null && Pathinit != "")
                    {
                        for (int g = 0; g < response.Count; g++)
                        {
                            if (response[g].Results != null && response[g].Results.Count > 0)
                            {
                                foreach (var item in response[g].Results)
                                {
                                    item.IsUploadAvail = 0;
                                    if (item != null && (item.ParentTestId == null || item.ParentTestId == 0))
                                    {
                                        string filename = string.Empty;
                                        string regid = response[g].RegistrationId != null && response[g].RegistrationId > 0 ? response[g].RegistrationId.ToString() : "";
                                        string testid = item.TestId != null && item.TestId > 0 ? item.TestId.ToString() : "";
                                        string venueno = "1";
                                        string venuebno = "1";
                                        filename = regid + "_" + testid;
                                        string folderName = venueno + "\\" + venuebno + "\\" + filename;
                                        string newPath = Path.Combine(Pathinit, folderName);
                                        if (Directory.Exists(newPath))
                                        {
                                            string[] filePaths = Directory.GetFiles(newPath);
                                            item.IsUploadAvail = filePaths != null && filePaths.Length > 0 ? 1 : 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return response.OrderByDescending(x => x.LastModifiedDateTime).ToList();
            }

        }

        public async Task<ErrorOr<Contracts.UploadDocResponse>> BulkUploadFile(List<Contracts.BulkFileUpload> lstjDTO)
        {
            List<Contracts.UploadDocResponse> result = new List<Contracts.UploadDocResponse>();

            foreach (var objDTO in lstjDTO)
            {

                var base64data = objDTO.ActualBinaryData;
                var visitId = objDTO.ExternalVisitID;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                var format = objDTO.FileType;
                var actualfilename = objDTO.ActualFileName;
                var manualfilename = objDTO.ManualFileName;
                string webRootPath = this.Configuration.GetValue<string>(Constants.UploadPathInit);
                string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (base64data != null && base64data.Length > 0)
                {
                    string fileName = venueNo + "$$" + venuebNo + "$$" + visitId + "$$" + actualfilename + "$$" + manualfilename + "." + format;
                    string fullPath = Path.Combine(newPath, fileName);

                    byte[] imageBytes = Convert.FromBase64String(base64data);
                    System.IO.File.WriteAllBytes(fullPath, imageBytes);
                }
            }
            return result.FirstOrDefault();
        }
        public async Task<ErrorOr<List<Contracts.BulkFileUpload>>> ConvertToBase64(Contracts.BulkFileUpload objDTO)
        {
            List<Contracts.BulkFileUpload> lstresult = new List<Contracts.BulkFileUpload>();
            Contracts.BulkFileUpload result = new Contracts.BulkFileUpload("", "", "", "", "", "", 0, 0, 0, "");
            //string FilePath = string.Empty;            
            string Pathinit = this.Configuration.GetValue<string>(Constants.UploadPathInit);
            string UploadedPath = this.Configuration.GetValue<string>(Constants.UploadedPath);
            var visitId = objDTO.ExternalVisitID;
            var venueNo = objDTO.VenueNo;
            var venuebNo = objDTO.VenueBranchNo;
            var format = "";
            string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
            string newPath = Path.Combine(Pathinit, folderName);
            if (Directory.Exists(newPath))
            {
                string[] filePaths = Directory.GetFiles(newPath);
                if (filePaths != null && filePaths.Length > 0)
                {
                    for (int f = 0; f < filePaths.Length; f++)
                    {
                        string FullPath = newPath + "\\" + venueNo + "_" + venuebNo + "_" + visitId + "." + format;
                        result = new Contracts.BulkFileUpload("", "", "", "", "", "", 0, 0, 0, "");
                        string path = filePaths[f].ToString();
                        Byte[] bytes = System.IO.File.ReadAllBytes(path);
                        string base64String = Convert.ToBase64String(bytes);
                        string FilePath = UploadedPath != null && UploadedPath != "" ? path.Replace(Pathinit, UploadedPath) : "";
                        string ActualBinaryData = base64String;
                        var split = filePaths[f].ToString().Split('.');
                        int splitcount = split != null ? split.Count() - 1 : 0;
                        string FileType = filePaths[f].ToString().Split('.')[splitcount];
                        string ActualFileName = filePaths[f].ToString().Split("$$")[3];
                        string ManualFileName = filePaths[f].ToString().Split("$$")[4];
                        result = new Contracts.BulkFileUpload(ActualFileName, ManualFileName, "", FileType, filePaths[f].ToString(), "", 0, venueNo, venuebNo, ActualBinaryData);
                        lstresult.Add(result);
                    }
                }
            }
            return lstresult;
        }

        public async Task<ErrorOr<List<RegistrationTransaction>>> GetPatientRegistrationTransaction(long id)
        {
            return await dataContext.RegistrationTransactions.Where(x => x.RegistrationId == id).ToListAsync();
        }

    }
}
