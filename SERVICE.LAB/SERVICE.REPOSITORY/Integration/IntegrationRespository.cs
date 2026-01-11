using AutoMapper;
using Azure;
using Azure.Core;
using Dev.IRepository;
using Dev.IRepository.PatientInfo;
using Dev.Repository.Integration;
using Dev.Repository.Integration.externalservices;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Integration;
using Service.Model.PatientInfo;
using Service.Model.Sample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RCMS;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dev.Repository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private IConfiguration _config;
        private IMapper _mapper;
        private readonly IFrontOfficeRepository _IFrontOfficeRepository;
        private readonly IManageSampleRepository _manageSampleRepository;
        private readonly IEditBillingRepository _IEditBillingRepository;
        private readonly IPatientInfoRepository patientInfoRepository;
        private readonly IMasterRepository _IMasterRepository;
        private readonly ITestRepository _ITestRepository;

        private readonly BloodBankService bloodBankService;
        public IntegrationRepository(IConfiguration config)
        {
            _config = config;
        }
        public IntegrationRepository(IConfiguration config, IMapper mapper, IFrontOfficeRepository frontRepository, IManageSampleRepository manageSampleRepository, ITestRepository testRepository,
            IEditBillingRepository iEditBillingRepository, BloodBankService _bloodBankService, IPatientInfoRepository _patientInfoRepository, IMasterRepository iMasterRepository)
        {
            _config = config;
            _mapper = mapper;
            _IFrontOfficeRepository = frontRepository;
            _manageSampleRepository = manageSampleRepository;
            _IEditBillingRepository = iEditBillingRepository;
            bloodBankService = _bloodBankService;
            patientInfoRepository = _patientInfoRepository;
            _IMasterRepository = iMasterRepository;
            _ITestRepository = testRepository;
        }
        public async Task<WaitingListMessage> updateMessages(WaitingListMessage request, UserClaimsIdentity user)
        {
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var orderEntity = await _dbContext.IntegrationOrderDetails.Where(x => x.Status == true && x.OrderId == request.OrderId).FirstAsync();
                var orderMessages = string.IsNullOrEmpty(orderEntity.Messages) ? new List<WaitingListMessage>() : JsonConvert.DeserializeObject<List<WaitingListMessage>>(orderEntity.Messages);
                orderMessages.Add(request);
                var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

                var messages = JsonConvert.SerializeObject(orderMessages, settings);
                orderEntity.Messages = messages;
                await _dbContext.SaveChangesAsync();
            }
            return request;
        }
        public async Task<bool> UpdateMassRegistration(UpdateMassRegistrationRequest request)
        {
            try
            {
                using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var itemToDelete = _dbContext.MassRegistrations.Find(request.OrderId);
                    var samplesToDelete = await _dbContext.MassRegistrationSamples.Where(x => x.MassRegistrationNo == request.OrderId).ToListAsync();
                    itemToDelete.PatientVisitNo = request.VisitNo;
                    itemToDelete.LabAccessionNo = request.LabAccessionNumber;
                    itemToDelete.Status = false;
                    samplesToDelete.ForEach(x => x.Status = false);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception exp)
            {
                MyDevException.Error(exp, "UpdateMassRegistration", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                return false;
            }
            return true;
        }
        public async Task<GetPatientDetailsResponse> GetPatientDetailsForEditing(EditPatientDetailsRequest request, UserClaimsIdentity user)
        {
            var response = new GetPatientDetailsWithServices();
            try
            {
                if (request.VisitNo > 0)
                    response = _IFrontOfficeRepository.GetPatientDetails(request.VisitNo, user.VenueNo, user.VenueBranchNo);
                else if (request.OrderId > 0)
                {
                    var waitingListRequest = new waitinglistrequest
                    {
                        NRICNumber = "",
                        MassRegistrationNo = request.OrderId,
                        StartDate = new DateTime(1900, 1, 1),
                        EndDate = DateTime.Now,
                    };
                    var massRecords = await this.GetMassRegistrationResponse(waitingListRequest, user);
                    var responseData = massRecords.Response.FirstOrDefault();
                    var massRegistrationData = JsonConvert.DeserializeObject<MassRegistrationList>(responseData.Orders);
                    var externalPatientDetails = await this.GetPatientDetails(HelperMethods.getSourceSystem(responseData.SourceSystem), HelperMethods.getSourceSystem(responseData.SourceSystem) == "RCMS" ? responseData.IntegrationOrderPatientDetails.patientid : responseData.IntegrationOrderVisitDetails.casenumber);
                    if (string.IsNullOrEmpty(externalPatientDetails.NationalityDescription)) externalPatientDetails.NationalityDescription = massRegistrationData.Nationality;
                    externalPatientDetails.EmailID = massRegistrationData.email ?? externalPatientDetails.EmailID;
                    externalPatientDetails.PatientBuilding = massRegistrationData.BuildinName ?? externalPatientDetails.PatientBuilding;
                    externalPatientDetails.PatientBlock = massRegistrationData.Block ?? externalPatientDetails.PatientBlock;
                    externalPatientDetails.PostalCode = massRegistrationData.PostalCode ?? externalPatientDetails.PostalCode;
                    externalPatientDetails.AreaName = massRegistrationData.Street ?? externalPatientDetails.AreaName;
                    var testAdditionalInformation = this.GetTestAdditionalInformation(responseData, externalPatientDetails, user);
                    var ageDescription = HelperMethods.GetAgeDescription(responseData.IntegrationOrderPatientDetails.dateofbirth).Split(' ');
                    List<ServiceSearchDTO> serviceSearchDTOs = new List<ServiceSearchDTO>();
                    var testDetails = new List<TestDetailsRetrieval>
                {
                    new TestDetailsRetrieval { Code = responseData.IntegrationOrderTestDetails[0].PackageId.ToString(), TestType = "2" }
                };
                    var test = this.GetTestInformation(testDetails).FirstOrDefault();
                    ServiceSearchDTO item = new ServiceSearchDTO
                    {
                        TestNo = test.TestNo,
                        TestName = test.TestDisplayName,
                        TestCode = test.TestCode,
                        TestType = "P",
                        Rowno = 1,
                        ShortCode = test.TestCode
                    };
                    serviceSearchDTOs.Add(item);
                    response = new GetPatientDetailsWithServices
                    {
                        Address = string.Join(",", new List<String> { externalPatientDetails.PatientBlock + " " + externalPatientDetails.Address, "#" + externalPatientDetails.PatientUnitNo + " " + externalPatientDetails.PatientFloor + " " + externalPatientDetails.PatientBuilding, externalPatientDetails.CountryName + " " + externalPatientDetails.PostalCode }.Where(x => !string.IsNullOrEmpty(x.Trim()))),
                        Age = Int32.Parse(ageDescription[0]),
                        AgeType = ageDescription[1],
                        AlternateId = externalPatientDetails.AlternativeIdNumber,
                        AlternateIdType = externalPatientDetails.AlternativeIdType,
                        AltMobileNumber = externalPatientDetails.AltMobileNumber,
                        AreaName = externalPatientDetails.AreaName,
                        Amount = 0,
                        CountryNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Country"),
                        dOB = responseData.IntegrationOrderPatientDetails.dateofbirth.ToString("yyyy-MM-dd"),
                        EmailID = externalPatientDetails.EmailID,
                        ExternalVisitID = responseData.LabAccessionNo,
                        FirstName = massRegistrationData.PatientName,
                        FullName = massRegistrationData.PatientName,
                        Gender = Int32.Parse(responseData.IntegrationOrderPatientDetails.gender),
                        HCPatientNo = 0,
                        LastName = externalPatientDetails.LastName,
                        maritalStatus = (short)HelperMethods.getMaritalStatus(externalPatientDetails.maritalStatus),
                        MobileNumber = massRegistrationData.Contact,
                        NationalityNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Nationality"),
                        PatientNo = GetExistingPatientDetails(responseData.IntegrationOrderVisitDetails.idnumber,user.VenueNo,user.VenueBranchNo),
                        PatientFloor = externalPatientDetails.PatientFloor,
                        PatientBuilding = externalPatientDetails.PatientBuilding,
                        PatientBlock = externalPatientDetails.PatientBlock,
                        PatientHomeNo = externalPatientDetails.PatientHomeNo,
                        CityNo = externalPatientDetails.CityNo,
                        MiddleName = externalPatientDetails.MiddleName,
                        PatientUnitNo = externalPatientDetails.RoomNumber,
                        Pincode = externalPatientDetails.PostalCode ?? "000000",
                        NRICNumber = responseData.IntegrationOrderVisitDetails.idnumber,
                        PatientID = massRegistrationData.IdNumber,
                        PhysicianNo = massRegistrationData.PhysicianNo.GetValueOrDefault(),
                        RaceNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Race"),
                        WardNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Ward"),
                        RefferralName = massRegistrationData.CustomerNo.ToString(),
                        RateListNo = 0,
                        RefferralTypeNo = 1,
                        SecondaryAddress = "",
                        SecondaryEmailID = massRegistrationData.Alternate_email,
                        ServiceCode = null,
                        ServiceName = null,
                        ServiceNo = 0,
                        ServiceType = null,
                        Sno = 1,
                        StateNo = 0,
                        TitleCode = "",
                        uRNID = responseData.IntegrationOrderVisitDetails.idnumber,
                        uRNType = HelperMethods.getURNType(responseData.IntegrationOrderVisitDetails.idtype),
                        VaccinationType = "",
                        VaccinationDate = null,
                        serviceRateLists = serviceSearchDTOs,
                    };
                }
            }
            catch (Exception exp)
            {
                MyDevException.Error(exp, "GetPatientDetailsForEditing", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }
        public async Task<bool> UpsertOrder(long visitId, UserClaimsIdentity user, List<FrontOfficeOrderList> ordersToAdd = null, OrderSaveRequest orderData = null, bool isRemove = false)
        {
            try
            {
                var editBilling = _IEditBillingRepository.GetEditPatientDetails(visitId, user.VenueNo, user.VenueBranchNo);
                var inputToSave = _mapper.Map<FrontOffficeDTO>(editBilling);
                if (ordersToAdd != null)
                {
                    if (isRemove)
                    {
                        ordersToAdd.ForEach(order =>
                        {
                            var orderToRemove = inputToSave.Orders.Find(y => y.TestNo == order.TestNo);
                            inputToSave.Orders.Remove(orderToRemove);
                        });
                    }
                    else
                        inputToSave.Orders.AddRange(ordersToAdd);
                }

                if (orderData != null)
                {
                    inputToSave.Gender = HelperMethods.getGender(orderData.GenderId.ToString());
                    inputToSave.IsVipIndication = orderData.IsVip;
                    inputToSave.isFasting = orderData.IsFasting;
                    inputToSave.IsStat = orderData.IsStat;
                }
                inputToSave.VenueNo = user.VenueNo;
                inputToSave.VenueBranchNo = user.VenueBranchNo;
                inputToSave.UserNo = user.UserNo;
                inputToSave.RegisteredType = "EB";
                inputToSave.IsAutoEmail = inputToSave.IsAutoSMS = true;
                var saveResponse = _IEditBillingRepository.InsertEditBilling(inputToSave);
                return saveResponse != null && !string.IsNullOrEmpty(saveResponse.visitID);
            }
            catch (Exception exp)
            {
                MyDevException.Error(exp, "UpsertOrder", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return false;
        }
        public async Task<List<IntegrationOrderTestDetailsResponse>> AddTest(AddTestRequest request, UserClaimsIdentity user, long patientVisitNo = 0)
        {
            List<IntegrationOrderTestDetailsResponse> responseData = new List<IntegrationOrderTestDetailsResponse>();
            var frontOfficeOrder = new List<FrontOfficeOrderList>();
            var testDetails = request.Test.Select(t => new TestDetailsRetrieval { Code = t.TestId.ToString(), TestType = "1" }).ToList();
            var testDetailsData = this.GetTestInformation(testDetails);
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                for (var i = 0; i < request.Test.Count; i++)
                {
                    var test = request.Test[i];
                    var inputToSave = _mapper.Map<IntegrationOrderTestDetails>(test);
                    var testInformation = testDetailsData.FirstOrDefault(y => y.TestNo == test.TestId);
                    if (testInformation != null)
                    {
                        inputToSave.code = testInformation.TestCode;
                        inputToSave.description = testInformation.TestDisplayName;
                        inputToSave.SampleTypeId = testInformation.SampleNo;
                        inputToSave.DepartmentId = testInformation.DepartmentNo;
                        inputToSave.MainDepartmentId = testInformation.MainDeptNo;
                        inputToSave.TestCode = testInformation.TestCode;
                        inputToSave.TestName = testInformation.TestDisplayName;
                    }
                    if (patientVisitNo == 0)
                    {
                        var response = await _dbContext.IntegrationOrderTestDetails.AddAsync(inputToSave);
                        await _dbContext.SaveChangesAsync();
                        var responseEntity = await _dbContext.IntegrationOrderDetails.Where(x => x.Status == true && x.OrderId == request.OrderId).FirstAsync();
                        patientVisitNo = responseEntity.PatientVisitNo.GetValueOrDefault();
                        if (!string.IsNullOrEmpty(responseEntity.LabAccessionNo))
                        {
                            frontOfficeOrder.Add(new FrontOfficeOrderList() { TestNo = inputToSave.TestId, TestCode = inputToSave.TestCode, TestName = inputToSave.TestName, TestType = "T", Rate = testInformation.Rate, Quantity = 1, Amount = testInformation.Rate, status = "N", RateListNo = 0, DiscountType = 0, DiscountAmount = 0, ClientServiceCode = "" });
                        }
                    }
                    else
                    {
                        frontOfficeOrder.Add(new FrontOfficeOrderList() { TestNo = inputToSave.TestId, TestCode = inputToSave.TestCode, TestName = inputToSave.TestName, TestType = "T", Rate = testInformation.Rate, Quantity = 1, Amount = testInformation.Rate, status = "N", RateListNo = 0, DiscountType = 0, DiscountAmount = 0, ClientServiceCode = "" });
                    }
                    responseData.Add(_mapper.Map<IntegrationOrderTestDetailsResponse>(inputToSave));
                }
                if (patientVisitNo > 0 && frontOfficeOrder.Count > 0)
                {
                    var orderReponse = await this.UpsertOrder(patientVisitNo, user, frontOfficeOrder, null);
                }
                await _dbContext.SaveChangesAsync();
            }
            return responseData;
        }

        private List<TestOrderDetails> getTestOrderDetails(IntegrationOrderDetailsResponse responseData)
        {
            var testItems = new List<TestOrderDetails>();
            try
            {
                var jsonObject = JObject.Parse(responseData.Orders);
                if (jsonObject["labdetails"] == null) return testItems;
                var dataArray = jsonObject["labdetails"]["orderitems"].ToString();
                testItems = JsonConvert.DeserializeObject<List<TestOrderDetails>>(dataArray);

            }
            catch (Exception exp)
            {

            }
            return testItems;
        }
        public async Task<Tuple<IntegrationOrderDetailsResponse, long, bool>> CreateOrder(WaitingListCreateManageSampleRequest order, WaitingListSaveRequest request, UserClaimsIdentity user)
        {
            var isFasting = request.manageSamples.Any(sample => sample.OrderId == order.OrderId && sample.IsFasting);
            var orderData = request.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            var isMassRegistration = request.IsMassRegistration;
            var orderId = order.OrderId;
            var integrationOrderTestId = order.IntegrationOrderId;
            var batchTests = request.manageSamples.Where(x => x.OrderId == order.OrderId).ToList();
            var integrationTests = new List<IntegrationOrderTestDetailsResponse>();

            IntegrationOrderDetails responseEntity = null;
            IntegrationOrderDetailsResponse responseData = null;
            long response = 0;
            var newOrder = false;
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                if (isMassRegistration)
                {
                    var waitingListRequest = new waitinglistrequest
                    {
                        NRICNumber = "",
                        MassRegistrationNo = orderId,
                        StartDate = new DateTime(1900, 1, 1),
                        EndDate = DateTime.Now,
                    };
                    var massRecords = await this.GetMassRegistrationResponse(waitingListRequest, user);
                    responseData = massRecords.Response.FirstOrDefault();
                    response = responseData.PatientVisitNo.GetValueOrDefault();
                    responseData.IntegrationOrderPatientDetails.dateofbirth = responseData.IntegrationOrderPatientDetails.dateofbirth.ToString("yyyy-MM-dd") == "0001-01-01" ? new DateTime(1900, 1, 1) : responseData.IntegrationOrderPatientDetails.dateofbirth;
                    integrationTests.AddRange(responseData.IntegrationOrderTestDetails);
                }
                else
                {
                    responseEntity = _dbContext.IntegrationOrderDetails.Where(x => x.Status == true && x.OrderId == orderId)
                    .Include(x => x.IntegrationOrderVisitDetails)
                    .Include(x => x.IntegrationOrderPatientDetails)
                    .Include(x => x.IntegrationOrderClientDetails)
                    .Include(x => x.IntegrationOrderDoctorDetails)
                    .Include(x => x.IntegrationOrderTestDetails)
                    .Include(x => x.IntegrationOrderWardDetails)
                    .Include(x => x.IntegrationOrderAllergyDetails)
                    .AsSplitQuery().First();

                    responseData = _mapper.Map<IntegrationOrderDetailsResponse>(responseEntity);
                    request.manageSamples.Where(x => x.OrderId == orderId && !x.IsOnHold && !x.isnotgiven).ToList().ForEach(sample =>
                    {
                        var itemsToAddToIntegrationTests = responseData.IntegrationOrderTestDetails.Where(x => x.OrderID == sample.OrderId && ((x.TestId == sample.testNo && sample.ServiceType == "T") || (x.GroupId == sample.serviceNo && sample.ServiceType == "G") || (sample.PackageId > 0 && sample.PackageId == x.PackageId))).ToList();
                        itemsToAddToIntegrationTests.ForEach(i =>
                        {
                            if (!integrationTests.Any(z => z.Id == i.Id)) integrationTests.Add(i);
                        });
                    });
                    var testDetails = integrationTests.FirstOrDefault(y => y.PackageId > 0);
                    if (testDetails != null && testDetails.PackageId > 0 && testDetails.LabAccessionNo != null)
                    {
                        AddTestRequest addTestRequest = new AddTestRequest
                        {
                            OrderId = orderId,
                            Test = integrationTests.Where(t => t.PatientVisitNo == 0 && t.PackageId == 0).ToList()
                        };
                        if (addTestRequest.Test.Count > 0)
                        {
                            var addedTestResult = await AddTest(addTestRequest, user, testDetails.PatientVisitNo.GetValueOrDefault());
                            var addedTestIds = addedTestResult.Select(x => x.Id).ToList();
                            responseEntity.IntegrationOrderTestDetails.Where(i => addedTestIds.Any(id => i.Id == id)).ToList().ForEach(test =>
                            {
                                test.LabAccessionNo = testDetails.LabAccessionNo;
                                test.PatientVisitNo = testDetails.PatientVisitNo.GetValueOrDefault();
                            });
                        }
                        await _dbContext.SaveChangesAsync();
                        response = testDetails.PatientVisitNo.GetValueOrDefault();
                    }

                }
            }
            if (response == 0)
            {
                newOrder = true;
                var isStat = orderData != null ? orderData.IsStat : responseData.IntegrationOrderTestDetails.Any(x => x.natureofrequest == "2" || x.natureofrequest == "3" || string.IsNullOrEmpty(x.natureofrequest));
                var externalPatientID = HelperMethods.getSourceSystem(responseData.SourceSystem) == "RCMS" ? responseData.IntegrationOrderPatientDetails.patientid : responseData.IntegrationOrderVisitDetails.casenumber;
                var externalPatientDetails = await this.GetPatientDetails(HelperMethods.getSourceSystem(responseData.SourceSystem), externalPatientID);
                MassRegistrationList massRegistrationData = new MassRegistrationList();
                if (isMassRegistration)
                {
                    massRegistrationData = JsonConvert.DeserializeObject<MassRegistrationList>(responseData.Orders); ;
                    if (string.IsNullOrEmpty(externalPatientDetails.NationalityDescription)) externalPatientDetails.NationalityDescription = massRegistrationData.Nationality;
                    externalPatientDetails.EmailID = massRegistrationData.email ?? externalPatientDetails.EmailID;
                    externalPatientDetails.PatientBuilding = massRegistrationData.BuildinName ?? externalPatientDetails.PatientBuilding;
                    externalPatientDetails.PatientBlock = massRegistrationData.Block ?? externalPatientDetails.PatientBlock;
                    externalPatientDetails.PostalCode = massRegistrationData.PostalCode ?? externalPatientDetails.PostalCode;
                    externalPatientDetails.AreaName = massRegistrationData.Street ?? externalPatientDetails.AreaName;
                    externalPatientDetails.FirstName = massRegistrationData.PatientName ?? externalPatientDetails.FirstName;
                    externalPatientDetails.PatientNumber = massRegistrationData.IdNumber ?? responseData.IntegrationOrderVisitDetails.idnumber;
                    externalPatientDetails.DateOfBirth = responseData.IntegrationOrderPatientDetails.dateofbirth.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    externalPatientDetails.MobileNumer = massRegistrationData.Contact ?? externalPatientDetails.MobileNumer;
                    externalPatientDetails.SexId = responseData.IntegrationOrderPatientDetails.gender ?? externalPatientDetails.SexId;
                }
                var testDetails = new List<TestDetailsRetrieval>();
                integrationTests.Where(x => x.Status == true).ToList().ForEach(row =>
                {
                    testDetails.Add(new TestDetailsRetrieval { Code = row.TestCode.ToString(), TestType = row.itemtype });
                    if (row.itemtype == "2" && !testDetails.Any(test => test.Code == row.PackageId.ToString() && test.TestType == row.itemtype))
                    {
                        testDetails.Add(new TestDetailsRetrieval { Code = row.PackageId.ToString(), TestType = row.itemtype });
                    }
                    else if (row.GroupId > 0 && !testDetails.Any(test => test.Code == row.GroupId.ToString() && test.TestType == "-1"))
                    {
                        testDetails.Add(new TestDetailsRetrieval { Code = row.GroupId.ToString(), TestType = "-1" });
                    }
                });
                var testInformation = this.GetTestInformation(testDetails);
                var testAdditionalInformation = this.GetTestAdditionalInformation(responseData, externalPatientDetails, user);
                var orders = new List<FrontOfficeOrderList>();
                var bbOrders = new List<FrontOfficeOrderList>();
                var testItems = getTestOrderDetails(responseData);
                var bbTests = await this.GetBloodBankTests(user);
                var bbGroups = await this.GetBloodBankGroups(user);
                integrationTests.Where(x => x.PatientVisitNo == 0 && x.Status == true).ToList().ForEach(row =>
                {
                    if (row.itemtype != "4" && row.itemtype != "3")
                    {
                        if (!row.IsNotGiven.GetValueOrDefault())
                        {
                            var testInfo = testInformation.FirstOrDefault(x => x.TestCode == row.TestCode && (x.TestType == "1"));
                            var testType = "T";
                            if (row.PackageId > 0)
                            {
                                testInfo = testInformation.FirstOrDefault(x => x.TestNo == row.PackageId && x.TestType == "2");
                                testType = "P";
                            }
                            else if (row.PackageId == 0 && row.GroupId > 0)
                            {
                                testInfo = testInformation.FirstOrDefault(x => x.TestNo == row.GroupId && x.TestType == "-1");
                                testType = "G";
                            }
                            if (testInfo != null)
                            {
                                var rate = testInfo.Rate;
                                var quantity = testItems.Where(x => x.Code == row.code).Sum(x => x.Quantity);
                                if (quantity == 0) quantity = 1;
                                var orderToAdd = new FrontOfficeOrderList
                                {
                                    TestType = testType,
                                    TestCode = testInfo.TestShortName,
                                    TestNo = testInfo.TestNo,
                                    TestName = testInfo.TestDisplayName,
                                    Rate = rate,
                                    Quantity = quantity,
                                    Amount = rate * quantity,
                                    DiscountType = 0,
                                    DiscountAmount = 0,
                                    RateListNo = 0,
                                    ClientServiceCode = "",
                                    status = ""//TODO: 
                                };
                                if (bbTests.Any(a => a.TestCode == row.code) || bbGroups.Any(a => a.GroupCode == row.code))
                                {
                                    if (!bbOrders.Any(o => o.TestType == orderToAdd.TestType && o.TestNo == orderToAdd.TestNo))
                                        bbOrders.Add(orderToAdd);
                                }
                                else
                                {
                                    if (!orders.Any(o => o.TestType == orderToAdd.TestType && o.TestNo == orderToAdd.TestNo))
                                        orders.Add(orderToAdd);
                                }
                            }
                        }
                    }
                });

                if (orders.Count > 0 || bbOrders.Count > 0)
                {
                    OrderDetailsResponse result = new OrderDetailsResponse();
                    var ageDescription = HelperMethods.GetAgeDescription(responseData.IntegrationOrderPatientDetails.dateofbirth).Split(' ');
                    var customerno = HelperMethods.getAdditionalId(testAdditionalInformation, "Customer");
                    customerno = customerno > 0 ? customerno : HelperMethods.getAdditionalId(testAdditionalInformation, "Clinic");
                    var objDTO = new FrontOffficeDTO
                    {
                        ClinicalDiagnosis = HelperMethods.getAdditionalId(testAdditionalInformation, "clinicaldiagnosiscode"),
                        ClinicalDiagnosisOthers = responseData.IntegrationOrderPatientDetails.diagnosisdescription,
                        isFasting = isFasting,
                        AgeType = ageDescription[1],
                        VenueNo = user.VenueNo,
                        VenueBranchNo = user.VenueBranchNo,
                        UserNo = user.UserNo,
                        CaseNumber = !string.IsNullOrEmpty(responseData.IntegrationOrderVisitDetails.visitno) ? responseData.IntegrationOrderVisitDetails.visitno : responseData.IntegrationOrderVisitDetails.casenumber,
                        ExternalVisitID = responseData.IntegrationOrderVisitDetails.visitno,
                        Remarks = string.Join(",", responseData.IntegrationOrderTestDetails.Select(x => x.remarks).Distinct()),
                        MobileNumber = externalPatientDetails.MobileNumer,
                        Address = string.Join(",", new List<String> { externalPatientDetails.PatientBlock + " " + externalPatientDetails.Address, "#" + externalPatientDetails.PatientUnitNo + " " + externalPatientDetails.PatientFloor + " " + externalPatientDetails.PatientBuilding, externalPatientDetails.CountryName + " " + externalPatientDetails.PostalCode }.Where(x => !string.IsNullOrEmpty(x.Trim()))),
                        EmailID = externalPatientDetails.EmailID,
                        PatientNo = GetExistingPatientDetails(responseData.IntegrationOrderVisitDetails.idnumber,user.VenueNo,user.VenueBranchNo),
                        PatientFloor = externalPatientDetails.PatientFloor,
                        PatientBuilding = externalPatientDetails.PatientBuilding,
                        PatientBlock = externalPatientDetails.PatientBlock,
                        PatientHomeNo = externalPatientDetails.PatientHomeNo,
                        AltMobileNumber = externalPatientDetails.AltMobileNumber,
                        AreaName = externalPatientDetails.AreaName,
                        CityNo = externalPatientDetails.CityNo,
                        MiddleName = externalPatientDetails.MiddleName,
                        PatientOfficeNumber = externalPatientDetails.AltMobileNumber,
                        PatientUnitNo = externalPatientDetails.RoomNumber,
                        Pincode = externalPatientDetails.PostalCode ?? "000000",
                        NRICNumber = responseData.IntegrationOrderVisitDetails.idnumber,
                        WardName = responseData.IntegrationOrderWardDetails.ward,
                        DOB = string.IsNullOrEmpty(externalPatientDetails.DateOfBirth) ? responseData.IntegrationOrderPatientDetails.dateofbirth.ToString("yyyy-MM-dd HH:mm:ss.fff") : externalPatientDetails.DateOfBirth,
                        FirstName = string.IsNullOrEmpty(externalPatientDetails.FirstName) ? string.Empty : externalPatientDetails.FirstName,
                        LastName = string.IsNullOrEmpty(externalPatientDetails.LastName) ? string.Empty : externalPatientDetails.LastName,
                        AlternateId = string.IsNullOrEmpty(externalPatientDetails.AlternativeIdNumber) ? responseData.IntegrationOrderPatientDetails.alternateIdnumber : externalPatientDetails.AlternativeIdNumber,
                        AlternateIdType = HelperMethods.getAlternateURNType(string.IsNullOrEmpty(externalPatientDetails.AlternativeIdType) ? responseData.IntegrationOrderPatientDetails.alternateIdtype : externalPatientDetails.AlternativeIdType),
                        AllergyInfo = string.IsNullOrEmpty(externalPatientDetails.AllergyDetails) ? string.Join(",", responseData.IntegrationOrderAllergyDetails.Where(al => al.IsAllergy == true).Select(row => row.Allergy)) : externalPatientDetails.AllergyDetails,
                        IsVipIndication = orderData != null ? orderData.IsVip : (externalPatientDetails.IsVip || responseData.IntegrationOrderPatientDetails.isVIP),
                        registrationDT = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                        Orders = orders.Count > 0 ? orders : bbOrders,
                        Payments = new List<FrontOfficePayment>(),
                        RegisteredType = HelperMethods.getSourceSystem(responseData.SourceSystem),
                        TitleCode = string.Empty,//HelperMethods.getGenderTitle(responseData.IntegrationOrderPatientDetails.gender),
                        Age = Int32.Parse(ageDescription[0]),
                        Gender = HelperMethods.getGender(orderData != null ? orderData.GenderId.ToString() : externalPatientDetails.SexId),
                        CountryNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Country"),
                        BedNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Bed"),
                        maritalStatus = HelperMethods.getMaritalStatus(externalPatientDetails.maritalStatus),
                        FullName = (string.IsNullOrEmpty(externalPatientDetails.FirstName) ? string.Empty : externalPatientDetails.FirstName) + " " + " " + (string.IsNullOrEmpty(externalPatientDetails.LastName) ? string.Empty : externalPatientDetails.LastName),
                        RefferralType = HelperMethods.getReferralType(responseData), //client, doctor, self and fill the referralType id based on this type from client/doctor integration table
                        RefferralTypeNo = 2,
                        isSelf = HelperMethods.getReferralType(responseData) == "1",
                        PhysicianNo = isMassRegistration ? massRegistrationData.PhysicianNo.GetValueOrDefault() : HelperMethods.getAdditionalId(testAdditionalInformation, "Physician"),
                        CustomerNo = isMassRegistration ? massRegistrationData.CustomerNo.GetValueOrDefault() : customerno,
                        RaceNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Race"),
                        WardNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Ward"),
                        NationalityNo = HelperMethods.getAdditionalId(testAdditionalInformation, "Nationality"),
                        IsStat = isStat,
                        DepartmentNo = responseData.IntegrationOrderTestDetails[0].DepartmentId, //check with praveen
                        ExternalVisitIdentity = isMassRegistration ? responseData.LabAccessionNo : "", // check with praveen
                        URNID = responseData.IntegrationOrderVisitDetails.idnumber,
                        URNType = HelperMethods.getURNType(responseData.IntegrationOrderVisitDetails.idtype),
                        NURNID = "", //responseData.IntegrationOrderVisitDetails.idnumber,
                        NURNType = "", //responseData.IntegrationOrderVisitDetails.idnumber,
                        IPOPNumber = "", //external system check with Praveen
                        IsAutoEmail = true,
                        IsAutoSMS = true,
                        IsAutoWhatsApp = true,
                        Base64Data = "",
                        ClinicalHistory = "",
                        CollectedAmount = 0,
                        CompanyNo = 0,
                        Deliverymode = false,
                        DiscountAmount = 0,
                        DiscountApprovedBy = 0,
                        discountDescription = "",
                        discountno = 0,
                        DueAmount = orders.Sum(x => x.Amount),
                        DueRemarks = "",
                        PatientVisitNo = 0,
                        PhysicianNo2 = 0,
                        RiderNo = 0,
                        RouteNo = 0,
                        SecondaryAddress = "",
                        SecondaryEmailID = "",
                        StateNo = 0,
                        TdiscountAmt = 0,
                        ToBeReturn = 0,
                        VaccinationDate = "",
                        VaccinationType = "",
                        NetAmount = orders.Sum(x => x.Amount),
                        GivenAmount = 0,
                        GrossAmount = orders.Sum(x => x.Amount),
                        HCPatientNo = 0,
                        IsDiscountApprovalReq = 0,
                        IsFranchise = false,
                        IsPregnant = false,
                        MarketingNo = 0,
                        FileFormat = "",
                        FileName = "",
                        FilePath = "",
                        ExternalPatientID = externalPatientID
                    };
                    if (orders.Count > 0)
                    {
                        var tempResult = _IFrontOfficeRepository.InsertFrontOfficeMaster(objDTO);
                        result.patientvisitno = tempResult.patientvisitno;
                        result.visitID = tempResult.visitID;
                    }
                    if (bbOrders.Count > 0)
                    {
                        var output = await bloodBankService.InsertBloodBankRegistration(objDTO, user, responseData);
                        if (output.Item1 > 0)
                        {
                            result.BBOrderId = output.Item1;
                            result.BBLabAccesionNumber = output.Item2;
                        }
                    }

                    response = result.patientvisitno > 0 ? result.patientvisitno : result.BBOrderId;
                    using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        if (!isMassRegistration)
                        {
                            var orderDetails = _dbContext.IntegrationOrderDetails.Include(x => x.IntegrationOrderTestDetails).FirstOrDefault(x => x.Status == true && x.OrderId == orderId);
                            orderDetails.PatientVisitNo = response;
                            if (orders.Count > 0) orderDetails.LabAccessionNo = result.visitID;
                            if (bbOrders.Count > 0) orderDetails.BBLabAccessionNo = result.BBLabAccesionNumber;
                            orderDetails.IntegrationOrderTestDetails.Where(y => integrationTests.Any(z => z.Id == y.Id)).ToList().ForEach(row =>
                            {
                                row.LabAccessionNo = orderDetails.LabAccessionNo;
                                row.PatientVisitNo = orderDetails.PatientVisitNo;
                            });

                        }
                        else
                        {
                            var orderDetails = _dbContext.MassRegistrations.FirstOrDefault(x => x.Status == true && x.MassRegistrationNo == orderId);
                            orderDetails.PatientVisitNo = result.patientvisitno;
                            orderDetails.LabAccessionNo = result.visitID;
                        }
                        _dbContext.SaveChanges();
                        if (responseEntity != null && !string.IsNullOrEmpty(responseEntity.ReferenceNo) && !isMassRegistration)
                        {
                            //added by Prabhu for updating LIS Mass Registration table based on LabAccessionNo from Integration Table
                            UpdateLabAccessionNoMassRegistration(responseEntity.ReferenceNo, true, user.VenueNo, user.VenueBranchNo);
                        }
                        else if (isMassRegistration)
                        {
                            var massdetails = _dbContext.MassRegistrationSamples.FirstOrDefault(x => x.Status == true && x.MassRegistrationNo == orderId);
                            UpdateLabAccessionNoMassRegistration(massdetails.BarCodeNo, false, user.VenueNo, user.VenueBranchNo);
                        }
                        responseData.PatientVisitNo = result.patientvisitno;
                        responseData.LabAccessionNo = result.visitID;
                        responseData.BBLabAccessionNo = result.BBLabAccesionNumber;
                    }
                }
            }
            else
            {
                if (orderData != null)
                {
                    var res = await this.UpsertOrder(response, user, null, orderData);
                }
            }
            this.updateOrderDetails(orderId, user, isFasting, isMassRegistration, orderData);
            return Tuple.Create(responseData, response, newOrder);
        }

        public int GetExistingPatientDetails(string URNNumber, int VenueNo, int VenueBranchno)
        {
            reqcheckExists input = new reqcheckExists();
            input.check = "IDNOSRCH";
            input.checkType = string.Empty;
            input.checkValue = URNNumber;
            input.venueNo = VenueNo;
            input.venueBranchNo = VenueBranchno;
            var response = _IFrontOfficeRepository.checkExists(input);

            if (response != null && response.Count > 0)
            {
                return response.LastOrDefault().patientNo;
            }
            else
                return 0;
        }

        public void UpdateTubeDetails(IntegrationOrderDetailsResponse response, OrderSaveRequest orderData, UserClaimsIdentity user)
        {
            if (orderData != null)
            {
                var patientVisitNo = Int32.Parse(response.PatientVisitNo.ToString());
                var input = new GetSampleRequest() { PatientVisitNo = patientVisitNo, VenueNo = user.VenueNo };
                var patientSamples = patientInfoRepository.GetPatientSampleInfo(input);
                var saveInput = patientSamples
                    .Where(row => orderData.TubeDetails.Any(x => x.SampleName == row.SampleName && x.ContainerName == row.ContainerName))
                    .Select(row =>
                {
                    var tubeCount = orderData.TubeDetails.First(x => x.SampleName == row.SampleName && x.ContainerName == row.ContainerName).TubeCount;
                    var req = new EditSampleRequest()
                    {
                        PatientSamplesNo = row.PatientSamplesNo,
                        UserNo = user.UserNo,
                        VenueNo = user.VenueNo,
                        PatientVisitNo = patientVisitNo,
                        specimenQty = row.specimenQty + tubeCount
                    };
                    return req;
                }).ToList();
                var saveResponse = patientInfoRepository.UpdateSampleDetails(saveInput);
            }
        }
        public void updateOrderDetails(int orderId, UserClaimsIdentity user, bool isFasting, bool isMassRegistration, OrderSaveRequest orderData)
        {
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                if (!isMassRegistration)
                {
                    var testData = _dbContext.IntegrationOrderTestDetails.Where(x => x.OrderID == orderId).ToList();
                    if (orderData != null)
                    {
                        var patientDetails = _dbContext.IntegrationOrderPatientDetails.FirstOrDefault(x => x.Status == true && x.OrderID == orderId);
                        if (patientDetails != null)
                        {
                            patientDetails.gender = orderData.GenderId.ToString();
                            patientDetails.isVIP = orderData.IsVip;
                        }
                        testData.ForEach(r =>
                        {
                            r.natureofrequest = orderData.IsStat ? "2" : "1";
                            r.natureofspecimen = orderData.IsFasting || isFasting ? "1" : "2";
                        });
                    }
                    else
                    {
                        testData.ForEach(r => r.natureofspecimen = isFasting ? "1" : r.natureofspecimen);
                    }
                    _dbContext.SaveChanges();
                }
            }
        }
        public async Task<ExternalPatientDetails> GetPatientDetails(string serviceType, string patientId)
        {
            IPatientDetailsService patientService = PatientDetailsServiceFactory.Create(serviceType, _config);
            return await patientService.GetPatientDetails(patientId);
        }

        public async Task<ExternalPatientDetailsResponse> GetPatientInformation(string patientVisitId, string system, UserClaimsIdentity user)
        {

            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var patientId = "";
                var sourceSystem = "";
                if (system == "BB")
                {
                    var patientDetails = await _dbContext.IntegrationOrderDetails.Include(x => x.IntegrationOrderVisitDetails).Include(x => x.IntegrationOrderPatientDetails).FirstOrDefaultAsync(x => x.BBLabAccessionNo == patientVisitId);
                    if (patientDetails != null)
                    {
                        sourceSystem = HelperMethods.getSourceSystem(patientDetails.SourceSystem);
                        if (sourceSystem == "RCMS") patientId = patientDetails.IntegrationOrderPatientDetails.patientid;
                        else if (sourceSystem == "SAP" || sourceSystem == "EMR") patientId = patientDetails.IntegrationOrderVisitDetails.casenumber;
                    }
                    else
                    {
                        sourceSystem = "BB";
                    }
                }
                else
                {

                    var patientTransactions = await _dbContext.PatientTransactions.Where(x => x.PatientVisitNo == Int32.Parse(patientVisitId)).ToListAsync();
                    if (patientTransactions != null)
                    {
                        var responseData = patientTransactions.LastOrDefault();
                        if (responseData != null)
                        {
                            patientId = responseData.VisitID;
                            sourceSystem = responseData.CreatedFrom;
                            if (sourceSystem == "RCMS") patientId = responseData.ExternalPatientId;
                            else if (sourceSystem == "SAP" || sourceSystem == "EMR") patientId = responseData.ExtenalVisitID;

                        }
                    }
                }
                var externalPatientDetails = await this.GetPatientDetails(sourceSystem, patientId);
                var listOfTestDetails = new List<TestDetailsRetrieval>()
                {
                    new TestDetailsRetrieval { TestType = "Nationality", Code = string.IsNullOrEmpty(externalPatientDetails.NationalityDescription) ? "-" : externalPatientDetails.NationalityDescription },
                    new TestDetailsRetrieval { TestType = "Race", Code = externalPatientDetails.RaceDescription},
                    new TestDetailsRetrieval { TestType = "Gender", Code = HelperMethods.getGender(externalPatientDetails.SexId) },
                };
                var testAdditionalInformation = this.GetTestAdditionalInformation(null, externalPatientDetails, user, listOfTestDetails);
                var response = new ExternalPatientDetailsResponse()
                {
                    IdNumber = externalPatientDetails.PatientNumber,
                    PatientName = (string.IsNullOrEmpty(externalPatientDetails.FirstName) ? string.Empty : externalPatientDetails.FirstName) + " " + " " + (string.IsNullOrEmpty(externalPatientDetails.LastName) ? string.Empty : externalPatientDetails.LastName),
                    DateOfBirth = externalPatientDetails.DateOfBirth,
                    GenderId = HelperMethods.getAdditionalId(testAdditionalInformation, "Gender"),
                    NationalityId = HelperMethods.getAdditionalId(testAdditionalInformation, "Nationality"),
                    RaceId = HelperMethods.getAdditionalId(testAdditionalInformation, "Race"),
                    ResidenceId = 0,
                    FirstName = externalPatientDetails.FirstName,
                    LastName = externalPatientDetails.LastName,
                    MiddleName  = externalPatientDetails.MiddleName,
                    SexId = string.IsNullOrEmpty(externalPatientDetails.SexId)  ? "" : externalPatientDetails.SexId.Substring(0, 1),
                    Address = string.Join(",", new List<String> { externalPatientDetails.PatientBlock + " " + externalPatientDetails.Address, "#" + externalPatientDetails.PatientUnitNo + " " + externalPatientDetails.PatientFloor + " " + externalPatientDetails.PatientBuilding, externalPatientDetails.CountryName + " " + externalPatientDetails.PostalCode }.Where(x => !string.IsNullOrEmpty(x.Trim()))),
                    PostalCode = externalPatientDetails.PostalCode ?? "000000",
                    Email = externalPatientDetails.EmailID,
                    PhoneNumber = externalPatientDetails.MobileNumer,
                    NricNumber = patientId
                };
                return response;
            }
            return null;
        }
        public async Task<waitinglistresponse> GetMassRegistrationResponse(waitinglistrequest request, UserClaimsIdentity user)
        {
            waitinglistresponse response = new waitinglistresponse() { Response = new List<IntegrationOrderDetailsResponse>(), Messages = new List<WaitingListMessage>() };
            List<MassRegistrationList> serviceResponse = new List<MassRegistrationList>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var idNumber = new SqlParameter("IDNumber", request.NRICNumber ?? "");
                    var referenceNumber = new SqlParameter("RefNum", request.CaseOrVisitId ?? "");
                    var fROMDate = new SqlParameter("FROMDate", request.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    var toDate = new SqlParameter("ToDate", request.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    var type = new SqlParameter("Type", "custom");
                    var venueNo = new SqlParameter("VenueNo", user.VenueNo);
                    var venueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo);
                    var massRegistrationNo = new SqlParameter("MassRegistrationNo", request.MassRegistrationNo);

                    serviceResponse = await context.MassRegistrationList.FromSqlRaw("Execute dbo.pro_GetMassRegistration @IDNumber, @FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @MassRegistrationNo, @RefNum", idNumber, fROMDate, toDate, type, venueNo, venueBranchNo, massRegistrationNo, referenceNumber).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetWaitingList", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            serviceResponse.GroupBy(x => x.MassRegistrationNo).ToList().ForEach(group =>
            {
                var order = group.ToList().First();
                var tests = group.ToList().Select(test =>
                {
                    return new IntegrationOrderTestDetailsResponse()
                    {
                        OrderID = Int16.Parse(test.MassRegistrationNo.ToString()),
                        Id = Int16.Parse(test.MassRegistrationSampleNo.ToString()),
                        itemtype = "2",
                        code = test.ServiceCode,
                        TestCode = test.ServiceCode,
                        TestName = test.ServiceName,
                        SampleNo = test.SampleId,
                        TestId = test.TestId,
                        GroupId = test.ServiceType == "3" ? test.GroupId : 0,
                        PackageId = test.PackageNo,
                        DepartmentId = test.DepartmentId,
                        MainDepartmentId = test.MainDeptId,
                        quantity = "1",
                        Status = true,
                        SampleName = "",
                        ContainerName = "",
                        collectiondttm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        natureofrequest = "1",
                        CreatedOn = test.CreatedOn,
                        CreatedBy = test.CreatedBy,
                        barcodenumber = test.BarCodeNo,

                    };
                });
                IntegrationOrderDetailsResponse integrationRow = new IntegrationOrderDetailsResponse()
                {
                    OrderId = Int16.Parse(order.MassRegistrationNo.ToString()),
                    Orders = JsonConvert.SerializeObject(order),
                    VenueNo = Int16.Parse(user.VenueNo.ToString()),
                    VenueBranchNo = Int16.Parse(user.VenueBranchNo.ToString()),
                    SourceSystem = "5",
                    LabOrderId = "",
                    isDowntimeOrder = false,
                    Status = true,
                    PatientVisitNo = order.PatientVisitNo,
                    LabAccessionNo = tests.First().barcodenumber,
                    IntegrationOrderVisitDetails = new IntegrationOrderVisitDetailsResponse { idnumber = order.IdNumber, registrationdttm = order.CreatedOn, casenumber = tests.First().barcodenumber, idtype = "O", visitno = tests.First().barcodenumber },
                    IntegrationOrderClientDetails = new IntegrationOrderClientDetailsResponse { clientname = order.ClientNo.ToString() },
                    IntegrationOrderWardDetails = new IntegrationOrderWardDetailsResponse(),
                    IntegrationOrderAllergyDetails = new List<IntegrationOrderAllergyDetailsResponse>(),
                    IntegrationOrderDoctorDetails = new IntegrationOrderDoctorDetailsResponse(),
                    IntegrationOrderPatientDetails = new IntegrationOrderPatientDetailsResponse { firstname = order.PatientName, patientid = order.IdNumber, dateofbirth = order.DOB.GetValueOrDefault(), gender = (order.Gender == "Male" || order.Gender == "M" ? "1" : "2"), Status = order.Status.GetValueOrDefault() },
                    IntegrationOrderTestDetails = new List<IntegrationOrderTestDetailsResponse>(tests)
                };
                response.Response.Add(integrationRow);
            });
            response.Response = EnrichResponse(response.Response);
            return response;
        }
        private async Task<List<CreateSampleActionRequest>> updateisNotGivenItems(List<CreateSampleActionRequest> input)
        {
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                input.ForEach(row =>
                {

                    var orderDetails = _dbContext.IntegrationOrderTestDetails.Find(Int16.Parse(row.IntegrationOrderTestNo.ToString()));
                    orderDetails.IsNotGiven = true;

                });
                await _dbContext.SaveChangesAsync();
            }
            return input;
        }
        private async Task<List<CreateSampleActionRequest>> updateRejectedOrOnHoldItems(List<CreateSampleActionRequest> input)
        {
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                input.ForEach(row =>
                {
                    var orderDetails = _dbContext.IntegrationOrderTestDetails.Where(r => r.OrderID == row.IntegrationOrderNo && r.Id == Int16.Parse(row.IntegrationOrderTestNo.ToString())).ToList();
                    if (row.ServiceType == "G")
                    {
                        orderDetails = _dbContext.IntegrationOrderTestDetails.Where(r => r.GroupId == row.TestNo && r.OrderID == row.IntegrationOrderNo && r.SampleTypeId == row.SampleNo).ToList();
                    }
                    orderDetails.ForEach(i =>
                                        {
                                            i.IsRejected = row.isReject;
                                            i.RejectedReason = row.isReject || row.IsOnHold ? row.remarks : "";
                                            i.RejectedReasonDesc = row.rejectioncomments;
                                            i.IsOnHold = row.IsOnHold;
                                            i.ModifiedBy = row.userNo;
                                            i.ModifiedOn = DateTime.Now;
                                            if (i.PackageId == 0 && !row.IsOnHold)
                                            {
                                                i.Status = false;
                                            }
                                        });

                });
                await _dbContext.SaveChangesAsync();
            }
            return input;
        }

        public async Task<bool> updateRegistrations(OnHoldRegistrationRequest request, UserClaimsIdentity user)
        {
            using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var responseEntity = await _dbContext.IntegrationOrderTestDetails.Where(x => x.Status == true && request.OrderId.Any(y => y == x.Id)).ToListAsync();
                responseEntity.ForEach(row =>
                {
                    row.Status = false;
                    row.RejectedReason = request.ReasonCode;
                });
                if (responseEntity.Any(p => p.PatientVisitNo > 0))
                {
                    var frontOfficeOrders = responseEntity.Select(y => new FrontOfficeOrderList() { TestNo = y.TestId }).ToList();
                    var orderReponse = await this.UpsertOrder(responseEntity.First(p => p.PatientVisitNo > 0).PatientVisitNo.GetValueOrDefault(), user, frontOfficeOrders, null);
                }
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<WaitingListSaveRequest> CreateManageSample(WaitingListSaveRequest request, UserClaimsIdentity user)
        {
            var patientVisitNos = new List<long>();
            var integrationDetails = new Dictionary<int, Tuple<IntegrationOrderDetailsResponse, long, bool>>();
            var visitAndOrderDetails = new Dictionary<string, long>();
            var createManageSample = request.manageSamples;
            if (request.IsDowntimeOrderRegistration)
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var orderIds = createManageSample.Select(x => x.OrderId).Distinct().ToList();
                    var orders = context.IntegrationOrderDetails.Where(x => orderIds.Any(y => y == x.OrderId)).ToList();
                    createManageSample.Where(x => (x.PackageId == 0 || !x.IsRejected) && !x.IsOnHold).ToList().ForEach(sample =>
                    {
                        var order = orders.First(x => x.OrderId == sample.OrderId);
                        order.Status = false;
                        order.LabAccessionNo = !string.IsNullOrEmpty(sample.DowntimeLabAccessionNo) ? sample.DowntimeLabAccessionNo : sample.LabAccessionNo;
                        order.DownTimeLabAccessionNo = sample.DowntimeLabAccessionNo;
                    });
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                //This is to update isNotGiven Flag in IntegrationTestDetails Table
                var isnotgivenitems = new List<CreateSampleActionRequest>();
                createManageSample.Where(x => x.isnotgiven).ToList().ForEach(x =>
                {
                    var tobeRejectItem = new CreateSampleActionRequest
                    {
                        PatientVisitNo = x.visitNo,
                        TestNo = x.testNo,
                        orderListNo = x.orderListNo,
                        SampleNo = x.sampleNo,
                        userNo = user.UserNo,
                        VenueNo = user.VenueNo,
                        VenueBranchNo = user.VenueBranchNo,
                        barCodeNo = x.barcodeno,
                        isAccept = !x.IsRejected,
                        isReject = x.IsRejected,
                        remarks = x.rejectedReason,
                        IsOnHold = x.IsOnHold,
                        IntegrationOrderTestNo = x.IntegrationOrderId
                    };

                    if (x.isnotgiven)
                    {
                        isnotgivenitems.Add(tobeRejectItem);
                    }

                });
                if (isnotgivenitems.Count > 0)
                {
                    await this.updateisNotGivenItems(isnotgivenitems);
                }
                var res = await UpdateManageSamples(createManageSample, user, false);
                foreach (var order in createManageSample)
                {
                    var canCreateOrder = createManageSample.Any(x => x.OrderId == order.OrderId && !x.IsRejected && !x.IsOnHold && !x.isnotgiven);
                    if (canCreateOrder)
                    {
                        var patientVisitDetails = integrationDetails.FirstOrDefault(x => x.Key == order.OrderId).Value;
                        if (patientVisitDetails == null)
                        {
                            if (!integrationDetails.ContainsKey(order.OrderId))
                            {
                                patientVisitDetails = await this.CreateOrder(order, request, user);
                                integrationDetails.Add(order.OrderId, patientVisitDetails);
                            }
                            else
                            {
                                patientVisitDetails = integrationDetails[order.OrderId];
                            }
                            var itemKey = patientVisitDetails.Item1.LabAccessionNo ?? patientVisitDetails.Item1.BBLabAccessionNo;
                            if (string.IsNullOrEmpty(itemKey)) itemKey = "-";
                            visitAndOrderDetails.TryAdd(itemKey, order.OrderId);
                        }

                        var patientVisitId = patientVisitDetails.Item2;
                        order.visitNo = Convert.ToInt32(patientVisitId);
                        order.LabAccessionNo = patientVisitDetails.Item1?.LabAccessionNo;
                        order.BBLabAccessionNo = patientVisitDetails.Item1?.BBLabAccessionNo;
                        if (!patientVisitNos.Any(x => x == patientVisitId))
                            patientVisitNos.Add(patientVisitId);                        
                    }
                }
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var visitIds = createManageSample.Select(id => id.visitNo).ToList();
                    var orderDetails = from a in context.OrderList
                                           //join b in context.OrderDetails on a.OrderListNo equals b.OrderListNo
                                       join c in context.OrderTransactions on a.OrderListNo equals c.OrderListNo
                                       where visitIds.Contains(a.PatientVisitNo)
                                       select new { OrderNo = c.OrderNo, OrderListNo = c.OrderListNo, ServiceCode = c.ServiceCode, ServiceType = c.ServiceType, PatientVisitNo = c.PatientVisitNo, ServiceNo = c.ServiceNo };

                    foreach (var item in createManageSample)
                    {
                        var orderDetail = orderDetails.FirstOrDefault(row => item.visitNo == row.PatientVisitNo && item.serviceNo == row.ServiceNo && item.ServiceType == row.ServiceType);
                        if (orderDetail != null)
                        {
                            item.ordersNo = orderDetail.OrderNo;
                            item.orderListNo = orderDetail.OrderListNo;
                            item.TestCode = orderDetail.ServiceCode;
                        }
                    }
                }
                List<CreateManageSampleRequest> objsample = createManageSample.Where(test => !string.IsNullOrEmpty(test.TestCode) && string.IsNullOrEmpty(test.rejectedReason)).Select(x =>
                {
                    var requestItem = _mapper.Map<CreateManageSampleRequest>(x);
                    requestItem.fastingOrNonfasting = x.IsFasting ? 1 : 2;
                    return requestItem;
                }).ToList();
                if (objsample.Count > 0)
                {
                    var response = _manageSampleRepository.CreateManageSample(objsample);
                    request.Response = response.Select(x =>
                    {
                        var req = createManageSample.FirstOrDefault(row => x.VisitID == row.LabAccessionNo && x.ContainerNo == row.containerNo && x.SampleNo == row.sampleNo);
                        if (req != null)
                        {
                            req.BarcodeShortNames = x.BarcodeShortNames;
                            req.barcodeno = x.BarcodeNo;
                        }
                        var genObject = _mapper.Map<WaitingListCreateManageSampleResponse>(x);
                        var correspondingOrderId = visitAndOrderDetails.FirstOrDefault(y => y.Key == x.VisitID).Value;
                        genObject.OrderId = correspondingOrderId;
                        return genObject;
                    }).ToList();
                }
                var res2 = await UpdateManageSamples(createManageSample, user, true);
                //request.Orders.ToList().ForEach(orderInfo =>
                //{
                //    var ord = integrationDetails.First(y => y.Key == orderInfo.OrderId);
                //    this.UpdateTubeDetails(ord.Value.Item1, orderInfo, user);
                //});
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var orderIds = createManageSample.Select(x => x.OrderId).Distinct().ToList();
                    if (!request.IsMassRegistration)
                    {
                        var tests = context.IntegrationOrderTestDetails.Where(x => orderIds.Any(y => y == x.OrderID)).ToList();
                        var orders = context.IntegrationOrderDetails.Where(x => orderIds.Any(y => y == x.OrderId)).ToList();
                        createManageSample.Where(x => (x.PackageId == 0 || !x.IsRejected) && !x.IsOnHold && !x.isnotgiven).ToList().ForEach(sample =>
                        {
                            var itemsToUpdate = tests.Where(x => sample.SelectedTestsIds.Any(y => y == x.Id));
                            if (itemsToUpdate != null)
                            {
                                itemsToUpdate.ToList().ForEach(intTest =>
                                {
                                    intTest.Status = false;
                                    intTest.collectiondttm = !string.IsNullOrEmpty(sample.collectedDateTime) ? DateTime.Parse(sample.collectedDateTime) : intTest.collectiondttm;
                                });
                            }
                            var orderTests = tests.Where(x => x.OrderID == sample.OrderId);
                            var optOutTests = orderTests.Where(x => x.itemtype == "4");
                            var orderCompleted = orderTests.Where(x => x.itemtype != "4").All(x =>
                            {
                                return optOutTests.Any(o => o.TestId == x.TestId && o.TestCode == x.TestCode) || x.Status == false;
                            });
                            if (orderCompleted)
                            {
                                var order = orders.First(x => x.OrderId == sample.OrderId);
                                order.Status = false;
                            }
                        });
                        //This is added to handle the optional test ordered from SourceSystem. Only Ordered Optional Test should show seleted in EditBilling PackagePopUp
                        if (integrationDetails.Count() > 0)
                        {
                            foreach (var integ in integrationDetails)
                            {
                                var visitno = integ.Value.Item2;
                                var packagelist = integ.Value.Item1.IntegrationOrderTestDetails.Where(x => x.itemtype == "2");
                                var optionalist = integ.Value.Item1.IntegrationOrderTestDetails.Where(x => x.itemtype == "3");
                                if (packagelist != null && packagelist.ToList().Count() > 0 && optionalist != null && optionalist.ToList().Count() > 0)
                                {
                                    //Prabhu
                                    var packreq = new IntegrationPackageReq
                                    {
                                        VenueBranchNo = user.VenueBranchNo,
                                        VenueNo = user.VenueNo
                                    };
                                    var PackgeList = _ITestRepository.GetIntegrationPackage(packreq);

                                    CreateManageOptionalTestRequest optionalTestRequest = new CreateManageOptionalTestRequest();
                                    optionalTestRequest.IsEditBill = false;
                                    optionalTestRequest.VenueNo = user.VenueNo;
                                    optionalTestRequest.VenueBranchNo = user.VenueBranchNo;
                                    optionalTestRequest.PatientVisitNo = Convert.ToInt16(visitno);
                                    optionalTestRequest.TestCodes = string.Join(",", optionalist.Select(x => x.TestCode).ToList());
                                    optionalTestRequest.PackageCode = PackgeList.Where(x => x.SourcePkgCode == packagelist.FirstOrDefault().code).FirstOrDefault().LISPkgCode;
                                    var response = _manageSampleRepository.ManageOptionalTestPackage(optionalTestRequest);
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            //var test = await context.MassRegistrations.ToListAsync();
                            var orders1 = await context.MassRegistrations.Where(x => orderIds.Contains(x.MassRegistrationNo)).ToListAsync();
                            var tests1 = context.MassRegistrationSamples.Where(x => orderIds.Contains(x.MassRegistrationNo)).ToList();
                            createManageSample.Where(x => (x.PackageId == 0 || !x.IsRejected) && !x.IsOnHold && !x.isnotgiven).ToList().ForEach(sample =>
                            {
                                var itemsToUpdate = tests1.FirstOrDefault(x => x.MassRegistrationNo == sample.OrderId && x.ServiceNo == sample.testNo);
                                if (itemsToUpdate != null)
                                {
                                    itemsToUpdate.Status = false;
                                }
                                if (tests1.Where(x => x.MassRegistrationNo == sample.OrderId).All(x => x.Status == false))
                                {
                                    var order = orders1.First(x => x.MassRegistrationNo == sample.OrderId);
                                    order.Status = false;
                                }
                            });
                        }
                        catch (Exception exp)
                        {

                        }

                    }
                    context.SaveChanges();
                }
            }
            return request;
        }

        public async Task<List<TestResponse>> GetBloodBankTests(UserClaimsIdentity user)
        {
            string _CacheKey = CacheKeys.CommonMaster + "BloodBankTests" + user.VenueNo + user.VenueBranchNo;
            var objResult = MemoryCacheRepository.GetCacheItem<List<TestResponse>>(_CacheKey);
            if (objResult == null)
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objResult = await Task.Run(() => context.TestDetails.FromSqlRaw("Execute dbo.pro_GetBloodBankTests").ToList());
                    var objAppSettingResponse = new AppSettingResponse();
                    string AppCacheMemoryTime = "CacheMemoryTime";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);

                    int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                    MemoryCacheRepository.AddItem(_CacheKey, objResult, Convert.ToInt32(cachetime));
                }
            }
            return objResult;
        }

        public async Task<List<GroupResponse>> GetBloodBankGroups(UserClaimsIdentity user)
        {
            string _CacheKey = CacheKeys.CommonMaster + "BloodBankGroups" + user.VenueNo + user.VenueBranchNo;
            var objResult = MemoryCacheRepository.GetCacheItem<List<GroupResponse>>(_CacheKey);
            if (objResult == null)
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objResult = await Task.Run(() => context.GroupDetails.FromSqlRaw("Execute dbo.pro_GetBloodBankGroups").ToList());
                    var objAppSettingResponse = new AppSettingResponse();
                    string AppCacheMemoryTime = "CacheMemoryTime";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);

                    int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                    MemoryCacheRepository.AddItem(_CacheKey, objResult, Convert.ToInt32(cachetime));
                }
            }
            return objResult;
        }

        public async Task<bool> UpdateManageSamples(List<WaitingListCreateManageSampleRequest> createManageSample, UserClaimsIdentity user, bool createOrderAndReject)
        {
            var rejectedAliquoteItems = new List<CreateSampleActionRequest>();
            var rejectedPackageitems = new List<CreateSampleActionRequest>();
            createManageSample.Where(x => x.IsRejected || x.IsOnHold).ToList().ForEach(x =>
            {
                var tobeRejectItem = new CreateSampleActionRequest
                {
                    PatientVisitNo = x.visitNo,
                    TestNo = x.testNo,
                    ServiceType = x.ServiceType,
                    orderListNo = x.orderListNo,
                    SampleNo = x.sampleNo,
                    userNo = user.UserNo,
                    VenueNo = user.VenueNo,
                    VenueBranchNo = user.VenueBranchNo,
                    barCodeNo = x.barcodeno,
                    isAccept = !x.IsRejected,
                    isReject = x.IsRejected,
                    remarks = x.rejectedReason,
                    IsOnHold = x.IsOnHold,
                    IntegrationOrderTestNo = x.IntegrationOrderId,
                    IntegrationOrderNo = x.OrderId,
                    rejectioncomments = x.rejectedReasonDesc
                };
                if (x.PackageId > 0 || x.IsOnHold)
                {
                    rejectedPackageitems.Add(tobeRejectItem);
                    if (x.rejectedReasonDesc == "Patient opt out" && x.rejectedReason == "Sample Declined")
                        rejectedAliquoteItems.Add(tobeRejectItem);
                }
                else
                    rejectedAliquoteItems.Add(tobeRejectItem);

            });
            if (rejectedAliquoteItems.Count > 0 && createOrderAndReject && rejectedAliquoteItems.Any(y => y.orderListNo > 0))
                _manageSampleRepository.CreateSampleACK(rejectedAliquoteItems);
            if ((rejectedPackageitems.Count > 0 || rejectedAliquoteItems.Count > 0) && !createOrderAndReject)
            {
                rejectedPackageitems.AddRange(rejectedAliquoteItems);
                await this.updateRejectedOrOnHoldItems(rejectedPackageitems);
            }
            return true;
        }

        public async Task<waitinglistresponse> GetWaitingList(waitinglistrequest request, UserClaimsIdentity user)
        {
            waitinglistresponse response = new waitinglistresponse() { Messages = new List<WaitingListMessage>(), Response = new List<IntegrationOrderDetailsResponse>() };
            try
            {
                using (var _dbContext = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    Expression<Func<IntegrationOrderDetails, bool>> NRICNumber = e => e.IntegrationOrderVisitDetails.idnumber == request.NRICNumber;
                    Expression<Func<IntegrationOrderDetails, bool>> caseOrVisitNumber = e => e.IntegrationOrderVisitDetails.casenumber == request.CaseOrVisitId || e.IntegrationOrderVisitDetails.visitno == request.CaseOrVisitId;
                    Expression<Func<IntegrationOrderDetails, bool>> clinicOrClientName = e => e.IntegrationOrderClientDetails.clinicname == request.ClinicOrClientName || e.IntegrationOrderClientDetails.clientname == request.ClinicOrClientName;
                    Expression<Func<IntegrationOrderDetails, bool>> CompanyName = e => e.IntegrationOrderClientDetails.institutioncode == request.CompanyName;
                    Expression<Func<IntegrationOrderDetails, bool>> PatientName = e => (e.IntegrationOrderPatientDetails.firstname + e.IntegrationOrderPatientDetails.lastname).IndexOf(request.PatientName) >= 0;
                    Expression<Func<IntegrationOrderDetails, bool>> SourceSystem = e => e.SourceSystem == request.SourceSystemId.ToString();
                    Expression<Func<IntegrationOrderDetails, bool>> SampleTypeId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.SampleTypeId == request.SampleTypeId);
                    Expression<Func<IntegrationOrderDetails, bool>> TestId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.TestId == request.TestId);
                    Expression<Func<IntegrationOrderDetails, bool>> GroupId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.GroupId == request.GroupId);
                    Expression<Func<IntegrationOrderDetails, bool>> PackageId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.PackageId == request.PackageId);
                    Expression<Func<IntegrationOrderDetails, bool>> DepartmentId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.DepartmentId == request.DepartmentId);
                    Expression<Func<IntegrationOrderDetails, bool>> MainDepartmentId = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.MainDepartmentId == request.MainDepartmentId);
                    Expression<Func<IntegrationOrderDetails, bool>> reasonCode = e => e.IntegrationOrderTestDetails.Any(y => y.IsOnHold == true && y.RejectedReason == request.ReasonCode);
                    Expression<Func<IntegrationOrderDetails, bool>> withoutReasonCode = e => string.IsNullOrEmpty(e.HoldReason);
                    Expression<Func<IntegrationOrderDetails, bool>> downtimeOrders = e => e.isDowntimeOrder == request.DowntimeOrders;
                    Expression<Func<IntegrationOrderDetails, bool>> BarCode = e => e.IntegrationOrderTestDetails.Any(x => x.Status == true && x.barcodenumber == request.BarCode);
                    Expression<Func<IntegrationOrderDetails, bool>> OrderId = e => e.OrderId == request.OrderId;


                    var query = _dbContext.IntegrationOrderDetails.Where(x => x.Status == true && x.CreatedOn >= request.StartDate && x.CreatedOn <= request.EndDate);
                    if (!string.IsNullOrEmpty(request.NRICNumber)) query = query.Where(NRICNumber);
                    if (!string.IsNullOrEmpty(request.BarCode)) query = query.Where(BarCode);
                    if (!string.IsNullOrEmpty(request.CaseOrVisitId)) query = query.Where(caseOrVisitNumber);
                    if (!string.IsNullOrEmpty(request.ClinicOrClientName)) query = query.Where(clinicOrClientName);
                    if (!string.IsNullOrEmpty(request.CompanyName)) query = query.Where(CompanyName);
                    if (!string.IsNullOrEmpty(request.PatientName)) query = query.Where(PatientName);
                    if (request.SampleTypeId != 0) query = query.Where(SampleTypeId);
                    if (request.TestId != 0) query = query.Where(TestId);
                    if (request.GroupId != 0) query = query.Where(GroupId);
                    if (request.PackageId != 0) query = query.Where(PackageId);
                    if (request.DepartmentId != 0) query = query.Where(DepartmentId);
                    if (request.MainDepartmentId != 0) query = query.Where(MainDepartmentId);
                    if (request.SourceSystemId != 0) query = query.Where(SourceSystem);
                    if (request.OrderId > 0) query = query.Where(OrderId);

                    if (string.IsNullOrEmpty(request.ReasonCode)) query = query.Where(withoutReasonCode);
                    else query = query.Where(reasonCode);

                    query = query.Where(downtimeOrders);

                    var responseData = query
                        .Include(x => x.IntegrationOrderVisitDetails)
                        .Include(x => x.IntegrationOrderPatientDetails)
                        .Include(x => x.IntegrationOrderClientDetails)
                        .Include(x => x.IntegrationOrderDoctorDetails)
                        .Include(x => x.IntegrationOrderTestDetails)
                        .Include(x => x.IntegrationOrderWardDetails)
                        .Include(x => x.IntegrationOrderAllergyDetails)
                        .AsSplitQuery().ToList();

                    var tempLocal = responseData.Select(row =>
                    {
                        var orderResponse = _mapper.Map<IntegrationOrderDetailsResponse>(row);
                        return orderResponse;
                    }).ToList();

                    response.Response = EnrichResponse(tempLocal);
                    var messages = tempLocal.Where(x => x != null && x.PatientVisitNo != null && x.PatientVisitNo > 0).ToList().SelectMany(order =>
                    {
                        var waitingListRequest = new PatientNotifyLog { PatientVisitNo = int.Parse(order.PatientVisitNo.GetValueOrDefault().ToString()), VenueBranchNo = user.VenueBranchNo, VenueNo = user.VenueNo, LogType = "" };
                        var response = _IFrontOfficeRepository.GetPatientNotifyLog(waitingListRequest);
                        return response.Select(row =>
                        {
                            var returnValue = _mapper.Map<WaitingListMessage>(row);
                            returnValue.OrderId = order.OrderId;
                            return returnValue;
                        });
                    }).ToList();

                    response.Messages = messages;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetWaitingList", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return response;
        }

        private List<IntegrationOrderDetailsResponse> EnrichResponse(List<IntegrationOrderDetailsResponse> tempLocal)
        {
            var testDetails = new List<TestDetailsRetrieval>();
            tempLocal.SelectMany(x => x.IntegrationOrderTestDetails).ToList().ForEach(row =>
            {
                testDetails.Add(new TestDetailsRetrieval { Code = row.TestCode.ToString(), TestType = row.itemtype });
                if (row.itemtype == "2" && !testDetails.Any(test => test.Code == row.PackageId.ToString() && test.TestType == row.itemtype))
                {
                    testDetails.Add(new TestDetailsRetrieval { Code = row.PackageId.ToString(), TestType = row.itemtype });
                }
                else if (row.GroupId > 0 && !testDetails.Any(test => test.Code == row.GroupId.ToString() && test.TestType == "-1"))
                {
                    testDetails.Add(new TestDetailsRetrieval { Code = row.GroupId.ToString(), TestType = "-1" });
                }
            });
            var testInformation = this.GetTestInformation(testDetails);
            var now = DateTime.Now;
            tempLocal.SelectMany(row => row.IntegrationOrderTestDetails).ToList().ForEach(row =>
            {
                var testInfo = testInformation.FirstOrDefault(x => x.TestCode == row.TestCode);
                var packageInfo = testInformation.FirstOrDefault(x => x.TestType == "2" && x.TestNo == row.PackageId && string.IsNullOrEmpty(x.SampleName));
                var groupInfo = testInformation.FirstOrDefault(x => x.TestType == "-1" && x.TestNo == row.GroupId && string.IsNullOrEmpty(x.SampleName));
                if (testInfo != null)
                {
                    row.ContainerName = testInfo.ContainerName;
                    row.SampleName = testInfo.SampleName;
                    row.ContainerNo = testInfo.ContainerNo;
                    row.SampleNo = testInfo.SampleNo;
                    row.PackageName = packageInfo != null ? packageInfo.TestDisplayName : "";
                    row.GroupName = groupInfo != null ? groupInfo.TestDisplayName : "";
                    row.GroupCode = groupInfo != null ? groupInfo.TestCode : "";
                    //row.collectiondttm = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                }
                else
                {

                }
            });
            return tempLocal;
        }

        public async Task<List<TestValidation>> GetTestValidation(WaitingListSaveRequest request)
        {
            List<TestValidation> testDetails = request.manageSamples.Select(x =>
            {
                return new TestValidation { Gender = x.Gender, OrderId = x.OrderId, TestId = x.testNo, ServiceType = x.ServiceType };
            }).ToList();
            var testData = JsonConvert.SerializeObject(testDetails);
            List<TestValidation> response = new List<TestValidation>();

            using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var testInfo = new SqlParameter("testInfo", testData);

                response = context.TestValidation.FromSqlRaw("Execute dbo.Pro_GetTestValidation @testInfo", testInfo).ToList();
            }
            return response;
        }
        private List<TestInformation> GetTestInformation(List<TestDetailsRetrieval> testDetails)
        {
            var testData = JsonConvert.SerializeObject(testDetails);
            List<TestInformation> response = new List<TestInformation>();

            using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var testInfo = new SqlParameter("testInfo", testData);

                response = context.TestInformation.FromSqlRaw("Execute dbo.Pro_GetTestInformation @testInfo", testInfo).ToList();
            }
            return response;
        }
        private List<TestAdditionalInformation> GetTestAdditionalInformation(IntegrationOrderDetailsResponse order, ExternalPatientDetails externalPatient, UserClaimsIdentity user, List<TestDetailsRetrieval> testDetails = null)
        {
            if (testDetails == null)
            {
                testDetails = new List<TestDetailsRetrieval>()
                {
                    new TestDetailsRetrieval { TestType = "Physician", Code =   order.IntegrationOrderDoctorDetails.doctormcr },
                    new TestDetailsRetrieval { TestType = "PhysicianName", Code =   order.IntegrationOrderDoctorDetails.doctorname },
                    new TestDetailsRetrieval { TestType = "VenueNo", Code =   user.VenueNo.ToString()},
                    new TestDetailsRetrieval { TestType = "VenueBranchNo", Code =   user.VenueBranchNo.ToString() },
                    new TestDetailsRetrieval { TestType = "Customer", Code = order.IntegrationOrderClientDetails.cliniccode??order.IntegrationOrderClientDetails.clientcode},
                    new TestDetailsRetrieval { TestType = "Clinic", Code = order.IntegrationOrderWardDetails.nursingOU??order.IntegrationOrderWardDetails.nursingOU},
                    new TestDetailsRetrieval { TestType = "Nationality", Code = string.IsNullOrEmpty(externalPatient.NationalityDescription) ? "-" : externalPatient.NationalityDescription },
                    new TestDetailsRetrieval { TestType = "Race", Code = externalPatient.RaceDescription},
                    new TestDetailsRetrieval { TestType = "Company", Code = ""},
                    new TestDetailsRetrieval { TestType = "Bed", Code = !string.IsNullOrEmpty(externalPatient.Bed) ? externalPatient.Bed : order.IntegrationOrderWardDetails.bed},
                    new TestDetailsRetrieval { TestType = "Ward", Code =  order.IntegrationOrderWardDetails.ward },
                    new TestDetailsRetrieval { TestType = "clinicaldiagnosiscode", Code = order.IntegrationOrderPatientDetails.diagnosiscode},
                    new TestDetailsRetrieval { TestType = "clinicaldiagnosisdescription", Code = order.IntegrationOrderPatientDetails.diagnosisdescription},
                };
            }

            var testData = JsonConvert.SerializeObject(testDetails);
            List<TestAdditionalInformation> response = new List<TestAdditionalInformation>();

            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var testInfo = new SqlParameter("testInfo", testData);

                    response = context.TestAdditionalInformation.FromSqlRaw("Execute dbo.Pro_GetTestAdditionalInformation @testInfo", testInfo).ToList();
                }
                if (response != null && response.Count > 0 && response.Any(y => y.DataType == "SUMMARY" && !string.IsNullOrEmpty(y.Details)))
                {
                    response.First(x => x.DataType == "SUMMARY").Details.Split(',').ToList().ForEach(response =>
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "Physician" + user.VenueNo + user.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    });
                }

            }
            catch (Exception exp)
            {

            }

            return response;
        }

        public List<testdetailsTrendReport> GetTestTrendReportDetails(requestTrendReport requestTrendReport, string TestCode, UserClaimsIdentity user)
        {
            List<testdetailsTrendReport> testDetails = new List<testdetailsTrendReport>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNumber = new SqlParameter("PatientNumber", requestTrendReport.PatientNumber);
                    var _StartDate = new SqlParameter("StartDate", requestTrendReport.StartDate);
                    var _EndDate = new SqlParameter("EndDate", requestTrendReport.EndDate);
                    var _Testcode = new SqlParameter("Testcode", TestCode);
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                    testDetails = context.GetTestTrendReportDetails.FromSqlRaw
                        ("Execute dbo.Pro_GetTestTrendReportDetails @PatientNumber,@StartDate,@EndDate,@Testcode,@venueNo,@venueBranchNo",
                        _PatientNumber, _StartDate, _EndDate, _Testcode, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SendOrderDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return testDetails;
        }


        public List<TestMasterDetails> GetTestDetails(TestDetailsRequest testDetailsRequest, UserClaimsIdentity user)
        {
            List<TestMasterDetails> testDetails = new List<TestMasterDetails>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _IsDelta = new SqlParameter("IsDelta", testDetailsRequest.IsDelta);
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                    testDetails = context.GetTestDetails.FromSqlRaw
                        ("Execute dbo.Pro_GetTestDetails @isdelta,@venueNo,@venueBranchNo", _IsDelta, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTestDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return testDetails;
        }

        public orderresponse SendOrderDetails(orderrequestdetails orderrequestdetails, UserClaimsIdentity user)
        {
            List<orderresponse> orderresponse = new List<orderresponse>();
            string response = string.Empty;

            var orderdetails = JsonConvert.SerializeObject(orderrequestdetails);
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OrderDetails = new SqlParameter("OrderDetails", orderdetails);
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                    var _LabOrderId = new SqlParameter("LabOrderId", orderrequestdetails.labdetails.orderid);
                    var _IsDownTimeOrder = new SqlParameter("IsDownTimeOrder", orderrequestdetails.labdetails.isDowntimeOrder);
                    var _SourceSystem = new SqlParameter("SourceSystem", orderrequestdetails.sourcesystem);
                    var _LisReferenceNo = new SqlParameter("LISReferenceNo", orderrequestdetails.visitdetails.lisreferenceno ?? string.Empty);
                    orderresponse = context.SendOrderDetails.FromSqlRaw
                        ("Execute dbo.Pro_InsertIntegrationOrderDetails @orderdetails,@venueNo,@venueBranchNo,@labOrderId,@isDownTimeOrder,@sourceSystem,@LISReferenceNo",
                        _OrderDetails, _VenueNo, _VenueBranchNo, _LabOrderId, _IsDownTimeOrder, _SourceSystem, _LisReferenceNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SendOrderDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }

            if (!string.IsNullOrEmpty(orderrequestdetails.visitdetails.lisreferenceno))
            {
                UpdateLabAccessionNoMassRegistration(orderrequestdetails.visitdetails.lisreferenceno, false, user.VenueNo, user.VenueBranchNo);
            }
            if (orderrequestdetails.labdetails.isDowntimeOrder)
            {
                UpdateLabAccessionNoDownTimeOrder(orderrequestdetails.labdetails.orderid, user.VenueNo, user.VenueBranchNo);
            }
            return orderresponse.FirstOrDefault();
        }

        private List<MassRegistrationResponse> UpdateLabAccessionNoDownTimeOrder(string OrderNo, int venueNo, int venueBranchNo)
        {
            List<MassRegistrationResponse> massresponse = new List<MassRegistrationResponse>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OrderNo = new SqlParameter("OrderNo", OrderNo);
                    var _VenueNo = new SqlParameter("VenueNo", venueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo.ToString());
                    var _Hours = new SqlParameter("@DownTimeHours", 4);
                    massresponse = context.UpdateLabAccessionNoMassRegistration.FromSqlRaw
                        ("Execute dbo.Pro_UpdateLabAccessionNoDownTimeOrder @OrderNo,@venueNo,@venueBranchNo,@DownTimeHours",
                        _OrderNo, _VenueNo, _VenueBranchNo, _Hours).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MassRegistrationUpdate", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return massresponse;
        }

        private List<MassRegistrationResponse> UpdateLabAccessionNoMassRegistration(string lisreferenceno, bool IsMassRegister, int venueNo, int venueBranchNo)
        {
            List<MassRegistrationResponse> massresponse = new List<MassRegistrationResponse>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _LisReferenceNo = new SqlParameter("LisReferenceNo", lisreferenceno);
                    var _IsMassRegister = new SqlParameter("IsMassRegister", IsMassRegister);
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    massresponse = context.UpdateLabAccessionNoMassRegistration.FromSqlRaw
                        ("Execute dbo.Pro_UpdateLabAccessionNoMassRegistration @LisReferenceNo,@IsMassRegister,@venueNo,@venueBranchNo",
                        _LisReferenceNo, _IsMassRegister, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MassRegistrationUpdate", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return massresponse;
        }

        public List<massregistration> MassRegistration(List<orderrequestdetails> orderlist, UserClaimsIdentity user)
        {
            List<massregistration> orderresponse = new List<massregistration>();
            List<orderresponse> response = new List<orderresponse>();
            var orderdetails = JsonConvert.SerializeObject(orderlist);
            try
            {
                foreach (var orderrequestdetails in orderlist)
                {
                    using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _OrderDetails = new SqlParameter("OrderDetails", orderdetails);
                        var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                        var _LabOrderId = new SqlParameter("LabOrderId", orderrequestdetails.labdetails.orderid);
                        var _IsDownTimeOrder = new SqlParameter("IsDownTimeOrder", orderrequestdetails.labdetails.isDowntimeOrder);
                        var _SourceSystem = new SqlParameter("SourceSystem", orderrequestdetails.sourcesystem);
                        response = context.SendOrderDetails.FromSqlRaw
                        ("Execute dbo.Pro_InsertIntegrationOrderDetails @orderdetails,@venueNo,@venueBranchNo,@labOrderId,@isDownTimeOrder,@sourceSystem",
                        _OrderDetails, _VenueNo, _VenueBranchNo, _LabOrderId, _IsDownTimeOrder, _SourceSystem).ToList();
                    }
                    orderresponse.Add(new massregistration { patientdetails = orderrequestdetails.patientdetails, referenceno = response.FirstOrDefault().referenceno });
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SendOrderDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return orderresponse;
        }
        public List<labresponsedetails> GetPDFReportDetails(reportrequestdetails reportrequestdetails, UserClaimsIdentity user)
        {
            List<labresponsedetails> labdetails = new List<labresponsedetails>();
            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VisitNumber = new SqlParameter("VisitNumber", reportrequestdetails.visitnumber);
                    var _CaseNumber = new SqlParameter("CaseNumber", reportrequestdetails.casenumber);
                    var _SourceSystem = new SqlParameter("SourceSystem", reportrequestdetails.sourcesystem);
                    var _SourceRequestId = new SqlParameter("SourceRequestId", reportrequestdetails.sourcerequestid);
                    var _PatientNumber = new SqlParameter("PatientNumber", reportrequestdetails.patientnumber);
                    var _StartDate = new SqlParameter("StartDate", reportrequestdetails.startdate);
                    var _EndDate = new SqlParameter("EndDate", reportrequestdetails.enddate);
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                    var labdetailslist = context.GetPDFReportDetails.FromSqlRaw
                        ("Execute dbo.Pro_GetPDFReportDetails @VisitNumber,@CaseNumber,@SourceSystem,@SourceRequestId,@PatientNumber,@StartDate,@EndDate,@venueNo,@venueBranchNo",
                                _VisitNumber, _CaseNumber, _SourceSystem, _SourceRequestId, _PatientNumber, _StartDate, _EndDate, _VenueNo, _VenueBranchNo).ToList();
                    labdetails = labdetailslist;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPDFReportDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return labdetails;
        }
        public List<labtestdetails> GetPDFReportTestDetails(int visitno, UserClaimsIdentity user)
        {
            List<labtestdetails> labtestdetails = new List<labtestdetails>();

            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VisitNumber = new SqlParameter("VisitNumber", visitno);
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo.ToString());
                    labtestdetails = context.GetPDFReportTestDetails.FromSqlRaw
                        ("Execute dbo.Pro_GetPDFReportTestDetails @VisitNumber,@venueNo,@venueBranchNo", _VisitNumber, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPDFReportTestDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return labtestdetails;
        }

        public List<LabReportTestDetails> GetDiscreetLabData(int visitno, UserClaimsIdentity user)
        {
            List<LabReportTestDetails> LabReportTestDetails = new List<LabReportTestDetails>();

            try
            {
                using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PageCode = new SqlParameter("PageCode", "PCPR");
                    var _VenueNo = new SqlParameter("VenueNo", user.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", user.VenueBranchNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", visitno);
                    var _OrderListNos = new SqlParameter("OrderListNos", "");
                    var _IsLogo = new SqlParameter("IsLogo", true);
                    var _IsNABLlogo = new SqlParameter("IsNABLlogo", false);
                    var _IsDraft = new SqlParameter("IsDraft", false);
                    var _UserNo = new SqlParameter("UserNo", user.UserNo);
                    var _QRCodeURL = new SqlParameter("QRCodeURL", "");
                    var _IsProvisional = new SqlParameter("IsProvisional", false);
                    LabReportTestDetails = context.GetDiscreetLabData.FromSqlRaw
                        ("Execute dbo.pro_GetResultReport_API @PageCode,@venueNo,@venueBranchNo,@PatientVisitNo,@OrderListNos,@IsLogo,@IsNABLlogo,@IsDraft," +
                        "@UserNo,@QRCodeURL,@IsProvisional", _PageCode, _VenueNo, _VenueBranchNo, _PatientVisitNo, _OrderListNos, _IsLogo, _IsNABLlogo, _IsDraft,
                        _UserNo, _QRCodeURL, _IsProvisional).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDiscreetDataResults", ExceptionPriority.High, ApplicationType.REPOSITORY, user.VenueNo, user.VenueBranchNo, 0);
            }
            return LabReportTestDetails;
        }
        public void NotifyIntegrationVisitStatus(int visitno, int VenueNo, int VenueBranchNo)
        {
            IntegrationVisitDetails visitdetail = new IntegrationVisitDetails();
            List<IntegrationVisitTestDetails> visittestdetail = new List<IntegrationVisitTestDetails>();
            using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _VisitNumber = new SqlParameter("VisitNo", visitno);
                var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                visitdetail = context.GetIntegrationVisitDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetIntegrationVisitDetails @VisitNo,@venueNo,@venueBranchNo", _VisitNumber, _VenueNo, _VenueBranchNo).ToList().FirstOrDefault();
            }

            using (var context = new IntegrationContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
            {
                var _VisitNumber = new SqlParameter("VisitNo", visitno);
                var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                visittestdetail = context.GetIntegrationVisitTestDetails.FromSqlRaw
                    ("Execute dbo.Pro_GetIntegrationVisitTestDetails @VisitNo,@venueNo,@venueBranchNo", _VisitNumber, _VenueNo, _VenueBranchNo).ToList();
            }

            if (visitdetail != null)
            {
                NotifyResponse response = new NotifyResponse();
                response.visitNumber = visitdetail.visitno;
                response.customerId = visitdetail.clientname;
                response.sourceRequestID = visitdetail.sourcerequestId;
                response.sourceSystem = Enum.GetName(typeof(nsourcesystem), Convert.ToInt32(visitdetail.SourceSystem));
                response.orderId = visitdetail.LabOrderId;
                response.totalInhouseOrdered = visittestdetail.Where(x => x.IsOutSource == false).Count();
                response.totalInhouseCompleted = visittestdetail.Where(x => x.IsOutSource == false && x.OrderListStatusText == "Result Validated").Count();
                response.totalSendOutOrdered = visittestdetail.Where(x => x.IsOutSource == true).Count();
                response.totalInhouseCompleted = visittestdetail.Where(x => x.IsOutSource == true && x.OrderListStatusText == "Result Validated").Count();
                visittestdetail.ForEach(x =>
                {
                    if (response.labResultItems == null)
                    {
                        response.labResultItems = new List<labResultItems>();
                    }
                    response.labResultItems.Add(new labResultItems
                    {
                        testCode = x.ServiceCode,
                        testName = x.ServiceName,
                        testStatus = x.OrderListStatusText
                    });
                });
                NotificationDetailsService notificationDetailsService = new NotificationDetailsService();
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                string IntegrationNotificationUrlConfig = "IntegrationNotificationUrl";
                var objConfigurationDTO = _IMasterRepository.GetSingleConfiguration(VenueNo, VenueBranchNo, IntegrationNotificationUrlConfig);
                notificationDetailsService.PushNotification(response, objConfigurationDTO.Description);
            }
        }

    }
}
