using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.BloodSampleResults;
using BloodBankManagement.Services.Integration;
using BloodBankManagement.Services.SampleReceiving;
using BloodBankManagement.Services.StartupServices;
using BloodBankManagement.Validators;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Audit;
using static BloodBankManagement.ServiceErrors.Errors;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]
    public class BloodBankRegistrationController : ApiController
    {
        private readonly IBloodBankRegistrationService _registrationService;
        private readonly ISampleReceivingService _sampleReceivingService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<BloodBankRegistrationController> _logger;
        private readonly IAuditService _auditService;
        private readonly IIntegrationService _integrationService;
        private readonly IBloodSampleResultService _bloodSampleResultService;

        public BloodBankRegistrationController(ISampleReceivingService sampleReceivingService, IBloodBankRegistrationService RegistrationService, IHttpContextAccessor _httpContextAccessor, ILogger<BloodBankRegistrationController> logger, IAuditService auditService, IIntegrationService integrationService, IBloodSampleResultService bloodSampleResultService)
        {
            _registrationService = RegistrationService;
            httpContextAccessor = _httpContextAccessor;
            _sampleReceivingService = sampleReceivingService;
            _logger = logger;
            _auditService = auditService;
            _integrationService = integrationService;
            _bloodSampleResultService = bloodSampleResultService;
        }

        [HttpPost("sendOutOrder")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.CreateBloodBankOrderLISRequest>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBloodBankLISOrder(CreateBloodBankOrderLISRequest request)
        {
            ErrorOr<Models.BloodBankRegistration> getPatientRegistrationResult = await _registrationService.GetPatientRegistration(request.OrderId);
            var response = new CreateBloodBankOrderLISRequest(request.OrderId, request.Action, 0, "");
            if (!getPatientRegistrationResult.IsError)
            {
                var registrationResponse = MapPatientRegistrationResponse(getPatientRegistrationResult.Value);
                var user = httpContextAccessor.HttpContext!.Items["User"] as User;
                var sendoutOrder = await _integrationService.InsertLISRegistration(request.OrderId, registrationResponse, user!);
                response = new CreateBloodBankOrderLISRequest(request.OrderId, request.Action, sendoutOrder.Item1, sendoutOrder.Item2);
            }
            
            return getPatientRegistrationResult.Match(
                        Tariffs => base.Ok(new ServiceResponse<Contracts.CreateBloodBankOrderLISRequest>("", "200", response)),
                        errors => Problem(errors));
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.UpdateRegistration>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRegistrations(UpdateBloodBankRegistrationStatusRequest request)
        {
            var errors = Shared.Helpers.ValidateInput<UpdateBloodBankRegistrationStatusRequest, UpdateBloodBankRegistrationStatusRequestValidator>(request, httpContextAccessor.HttpContext!);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Contracts.UpdateRegistration>> upsertRegistrations = await _registrationService.UpdateRegistrations(request);
            return upsertRegistrations.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.UpdateRegistration>>("", "200", upsertRegistrations.Value)),
             errors => Problem(errors));
        }
        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankRegistration>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRegistrationForBloodSampleResult(FetchBloodSampleResultRequest request)
        {
            ErrorOr<List<Models.BloodBankRegistration>> response = await _registrationService.GetBloodBankRegistrationsForResult(request);
            return response.Match(
                registrations => base.Ok(new ServiceResponse<List<Contracts.BloodBankRegistration>>("", "200", registrations.Select(x => MapPatientRegistrationResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpGet("bloodbanktests")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.TestResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBloodBankTests()
        {
            ErrorOr<List<Contracts.TestResponse>> testDetails = await _registrationService.GetBloodBankTests();
            return testDetails.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.TestResponse>>("", "200", testDetails.Value)),
             errors => Problem(errors));
        }

        [HttpGet("bloodbankgroups")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.GroupResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBloodBankGroups()
        {
            ErrorOr<List<Contracts.GroupResponse>> testDetails = await _registrationService.GetBloodBankGroups();
            return testDetails.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.GroupResponse>>("", "200", testDetails.Value)),
             errors => Problem(errors));
        }

        [HttpGet("bloodbanksubtests")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.SubTestResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBloodBankSubTests()
        {
            ErrorOr<List<Contracts.SubTestResponse>> testDetails = await _registrationService.GetBloodBankSubTests();
            return testDetails.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.SubTestResponse>>("", "200", testDetails.Value)),
             errors => Problem(errors));
        }
        [HttpGet("bloodbanktestspicklist")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.TestPickListResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBloodBankSubTestsPickList()
        {
            ErrorOr<List<Contracts.TestPickListResponse>> testDetails = await _registrationService.GetBloodBankSubTestsPickList();
            return testDetails.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.TestPickListResponse>>("", "200", testDetails.Value)),
             errors => Problem(errors));
        }


        [HttpPost("createbloodbankorder")]
        [ProducesResponseType(typeof(ServiceResponse<Dictionary<string, string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBloodBankPatientRegistrationFromExternal(Dictionary<string, Dictionary<string, string>> input)
        {
            UpsertBloodBankRegistrationRequest request = createRequest(input);
            var requestToRegistrationResult = Models.BloodBankRegistration.From(request, httpContextAccessor.HttpContext!);

            if (requestToRegistrationResult.IsError)
            {
                requestToRegistrationResult.Errors.ForEach(x =>
                {
                    _logger.LogInformation("ErrorDetails", x.Description + x.Code);
                });
                return Problem(requestToRegistrationResult.Errors);
            }
            var registration = requestToRegistrationResult.Value;
            ErrorOr<Models.BloodBankRegistration> createPatientRegistrationResult;
            using (var auditScope = new AuditScope<UpsertBloodBankRegistrationRequest>(request, _auditService, "", new string[] { request.Identifier == 0 ? "Create Registration" : "Edit Registration" }))
            {
                createPatientRegistrationResult = await _registrationService.CreatePatientRegistration(registration);
                auditScope.IsRollBack = createPatientRegistrationResult.IsError;
                auditScope.VisitNo = createPatientRegistrationResult.Value != null ? createPatientRegistrationResult.Value.RegistrationId.ToString() : "";
                auditScope.LabAccessionNo = createPatientRegistrationResult.Value != null ? createPatientRegistrationResult.Value.LabAccessionNumber : "";
            }
            //if (!createPatientRegistrationResult.IsError)
            //{
            //    Dictionary<string, string> orgPatientDetails = input["PatientInformation"];
            //    var sampleTypeId = orgPatientDetails["SampleTypeId"] ?? "0";
            //    var patientDetails = createPatientRegistrationResult.Value;
            //    var BloodSamples = new List<Models.BloodSample>()
            //    {
            //        new Models.BloodSample { BarCode = patientDetails.LabAccessionNumber, IsActive = true, LastModifiedDateTime = DateTime.Now, ModifiedBy = request.ModifiedBy, ModifiedByUserName = request.ModifiedByUserName,
            //        UnitCount = 1, TubeNo = "Tube 1", Tests = orgPatientDetails["TestShortNames"], SampleTypeId = Int32.Parse(sampleTypeId), RegistrationId = patientDetails.RegistrationId, ParentRegistrationId = 0, PatientId = patientDetails.BloodBankPatientId }
            //    };
            //    ErrorOr<List<Models.BloodSample>> upsertBloodSampleResult = await _sampleReceivingService.SaveBloodSamplesList(BloodSamples);
            //}
            var response = new Dictionary<string, string>();
            if (!createPatientRegistrationResult.IsError)
            {
                response.Add("LabAccessionNo", createPatientRegistrationResult.Value.LabAccessionNumber);
                response.Add("VisitNo", createPatientRegistrationResult.Value.RegistrationId.ToString());
            }
            return createPatientRegistrationResult.Match(
                created => base.Ok(new ServiceResponse<Dictionary<string, string>>("", "200", response)),
                errors => Problem(errors));
        }

        private UpsertBloodBankRegistrationRequest createRequest(Dictionary<string, Dictionary<string, string>> input)
        {
            Dictionary<string, string> patientDetails = input["PatientInformation"];
            Dictionary<string, string> orderDetails = input["Results"];
            var results = new List<BloodSampleResultResponse>();

            foreach (KeyValuePair<string, string> kvp in orderDetails)
            {
                var bbTestsList = new List<Tuple<TestResponse, int>>();
                var bbGroups = GlobalConstants.Groups.Where(g => g.GroupNo.ToString() == kvp.Key).ToList();
                if(bbGroups != null && bbGroups.Count > 0)
                {
                    bbGroups.ForEach(groupTest =>
                    {
                        var bbTestFromGroup = GlobalConstants.Tests.First(t => t.TestNo.ToString() == groupTest.TestNo.ToString());
                        bbTestsList.Add(Tuple.Create(bbTestFromGroup, groupTest.GroupNo));
                    });
                }
                else
                {
                    var bbTest = GlobalConstants.Tests.First(t => t.TestNo.ToString() == kvp.Key);
                    bbTestsList.Add(Tuple.Create(bbTest, 0));
                }

                bbTestsList.ForEach(bbTestsTuple =>
                {
                    var bbTests = bbTestsTuple.Item1;
                    var groupId = bbTestsTuple.Item2;
                    var bbSubTests = GlobalConstants.SubTests.Where(s => s.TestNo == bbTests.TestNo).ToList();
                    var testResult = new BloodSampleResultResponse(
                        0, 0, 0, bbTests.TestNo, 0, 0, bbTests.TestName, "", "", "", "", "Registered", false, null,
                        HelperMethods.ParseStringToLong(patientDetails["ModifiedBy"]).GetValueOrDefault(),
                        patientDetails["ModifiedUserName"], DateTime.Now, false, false, false, null, groupId);
                    results.Add(testResult);
                    bbSubTests.ForEach(st =>
                    {
                        var subTestResult = new BloodSampleResultResponse(
                                          0, 0, 0, st.SubTestNo, st.TestNo, 0, st.SubTestName, "", "", "", "", "Registered", false, null,
                                          HelperMethods.ParseStringToLong(patientDetails["ModifiedBy"]).GetValueOrDefault(),
                                          patientDetails["ModifiedUserName"], DateTime.Now, false, false, false, null, groupId);
                        results.Add(subTestResult);
                    });
                });
                
            }
            var residenceId = string.IsNullOrEmpty(patientDetails["Residence"]) ? GlobalConstants.Lookups.FirstOrDefault(x => x.Type == "residence")?.Identifier.ToString() ?? "0" : patientDetails["Residence"];
            var nationalityId = string.IsNullOrEmpty(patientDetails["Nationality"]) || patientDetails["Nationality"] == "0" ? GlobalConstants.Lookups.FirstOrDefault(x => x.Type == "nationality")?.Identifier.ToString() ?? "0" : patientDetails["Nationality"];
            return new UpsertBloodBankRegistrationRequest(
                    patientDetails["NRICNumber"],
                    patientDetails["PatientName"],
                    HelperMethods.ParseStringToDateTime(patientDetails["PatientDOB"]).GetValueOrDefault(),
                    patientDetails["CaseOrVisitNumber"],
                    HelperMethods.ParseStringToLong(nationalityId).GetValueOrDefault(),
                    HelperMethods.ParseStringToLong(patientDetails["Gender"]).GetValueOrDefault(),
                    HelperMethods.ParseStringToLong(patientDetails["Race"]).GetValueOrDefault(),
                    HelperMethods.ParseStringToLong(residenceId).GetValueOrDefault(),
                    HelperMethods.ParseStringToLong(patientDetails["ClinicalDiagnosis"]),
                    HelperMethods.ParseStringToLong(patientDetails["TransfusionIndicator"]).GetValueOrDefault(),
                    patientDetails["ClinicalDiagnosisOthers"],
                    patientDetails["IndicationOfTransfusionOthers"],
                    patientDetails["DoctorOthers"],
                    patientDetails["DoctorMCROthers"],
                    patientDetails["IsEmergency"] == "T",
                    HelperMethods.ParseStringToLong(patientDetails["WardId"]),
                    HelperMethods.ParseStringToLong(patientDetails["ClinicId"]),
                    HelperMethods.ParseStringToLong(patientDetails["DoctorId"]),
                    new List<PatientRegisteredProducts>(),
                    results,
                    new List<RegisteredSpecialRequirementResponse>(),
                    0,
                    true,
                    "Registered",
                    0,
                    "",
                    "",
                    HelperMethods.ParseStringToLong(patientDetails["ModifiedBy"]).GetValueOrDefault(),
                    patientDetails["ModifiedUserName"],
                    0,
                    DateTime.Now

                );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.BloodBankRegistration>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBloodBankPatientRegistration(UpsertBloodBankRegistrationRequest request)
        {
            var requestToRegistrationResult = Models.BloodBankRegistration.From(request, httpContextAccessor.HttpContext!);

            if (requestToRegistrationResult.IsError)
            {
                return Problem(requestToRegistrationResult.Errors);
            }
            var registration = requestToRegistrationResult.Value;
            ErrorOr<Models.BloodBankRegistration> createPatientRegistrationResult;
            using (var auditScope = new AuditScope<UpsertBloodBankRegistrationRequest>(request, _auditService, "", new string[] { request.Identifier == 0 ? "Create Registration" : "Edit Registration" }))
            {
                createPatientRegistrationResult = await _registrationService.CreatePatientRegistration(registration);
                var bloodsampleResults = createPatientRegistrationResult.Value.Results.Where(x => x.Status == "ResultValidated").ToList();
                if(bloodsampleResults.Count > 0)
                {
                    var upsertBloodSampleResult = await _bloodSampleResultService.UpsertBloodSampleResult(bloodsampleResults);
                }
                auditScope.IsRollBack = createPatientRegistrationResult.IsError;
                auditScope.VisitNo = createPatientRegistrationResult.Value != null ? createPatientRegistrationResult.Value.RegistrationId.ToString() : "";
                auditScope.LabAccessionNo = createPatientRegistrationResult.Value != null ? createPatientRegistrationResult.Value.LabAccessionNumber : "";
            }
            return createPatientRegistrationResult.Match(
                created => CreatedAtGetPatientRegistration(registration),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.BloodBankRegistration>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatientRegistration(Int64 id)
        {
            ErrorOr<Models.BloodBankRegistration> getPatientRegistrationResult = await _registrationService.GetPatientRegistration(id);

            return getPatientRegistrationResult.Match(
                PatientRegistration => base.Ok(new ServiceResponse<Contracts.BloodBankRegistration>("", "200", MapPatientRegistrationResponse(PatientRegistration))),
                errors => Problem(errors));
        }

        [HttpGet("recall/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<List<string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RecallRegistration(Int64 id)
        {
            ErrorOr<List<string>> getPatientRegistrationResult;
            var request = new RecallRequest(id);
            using (var auditScope = new AuditScope<RecallRequest>(request, _auditService, "", new string[] { "Recall Registration" }))
            {
                getPatientRegistrationResult = await _registrationService.RecallRegistration(id);
                auditScope.IsRollBack = getPatientRegistrationResult.IsError;
                auditScope.VisitNo = id.ToString();
            }

            return getPatientRegistrationResult.Match(
                PatientRegistration => base.Ok(new ServiceResponse<List<string>>("", "200", PatientRegistration)),
                errors => Problem(errors));
        }

        [HttpGet("transactions/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.RegistrationTransactionResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatientRegistrationTransactions(Int64 id)
        {
            ErrorOr<List<RegistrationTransaction>> getPatientRegistrationResult = await _registrationService.GetPatientRegistrationTransaction(id);

            return getPatientRegistrationResult.Match(
                transactions => base.Ok(new ServiceResponse<List<Contracts.RegistrationTransactionResponse>>("", "200", transactions.Select(x => MapTransaction(x)).ToList())),
                errors => Problem(errors));
        }
        private CreatedAtActionResult CreatedAtGetPatientRegistration(Models.BloodBankRegistration PatientRegistration)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetPatientRegistration),
                routeValues: new { id = PatientRegistration.RegistrationId },
                value: new ServiceResponse<Contracts.BloodBankRegistration>("", "201", MapPatientRegistrationResponse(PatientRegistration)));
        }

        private static Contracts.RegistrationTransactionResponse MapTransaction(RegistrationTransaction transaction)
        {
            return new RegistrationTransactionResponse(transaction.Identifier, transaction.RegistrationStatus, transaction.Comments, transaction.ModifiedBy, transaction.EntityType, transaction.EntityId, transaction.EntityAction, transaction.RegistrationId, transaction.ModifiedByUserName, transaction.LastModifiedDateTime);
        }
        public static Contracts.BloodBankRegistration MapPatientRegistrationResponse(Models.BloodBankRegistration response)
        {
            var products = response.Products != null && response.Products.Count > 0 ? response.Products.Select(product =>
            {
                return new PatientRegisteredProducts(product.Identifier, product.ProductId, product.MRP, product.Unit, product.Price);
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
            var patient = response.BloodBankPatient ?? new Models.BloodBankPatient();
            var bloodSampleInventories = response.BloodSampleInventories != null && response.BloodSampleInventories.Count > 0 ? response.BloodSampleInventories.Select(bloodSample =>
            {
            return new BloodSampleInventoryResponse(bloodSample.Identifier, bloodSample.RegistrationId, bloodSample.BloodSampleResultId, bloodSample.ProductId, bloodSample.InventoryId, bloodSample.IsMatched, bloodSample.IsComplete, bloodSample.Comments, bloodSample.Status, bloodSample.IssuedToNurseId, bloodSample.ClinicId, bloodSample.PostIssuedClinicId, bloodSample.ReturnedByNurseId, bloodSample.TransfusionDateTime, bloodSample.TransfusionVolume, bloodSample.IsTransfusionReaction, bloodSample.TransfusionComments, bloodSample.ModifiedBy, bloodSample.ModifiedByUserName, bloodSample.LastModifiedDateTime, bloodSample.CrossMatchingTestId, bloodSample.CompatibilityValidTill, bloodSample.IssuedDateTime);
            }).ToList() : new List<BloodSampleInventoryResponse>();
            var billings = response.Billings != null && response.Billings.Count > 0 ? response.Billings.Select(billing =>
            {
                return new BloodBankBillingResponse(billing.Identifier, billing.ProductId, billing.TestId, billing.ClinicId, billing.EntityId, billing.MRP, billing.Unit, billing.Price, billing.Status, billing.ModifiedBy, billing.ModifiedByUserName, billing.LastModifiedDateTime, billing.IsBilled, billing.ServiceType);
            }).ToList() : new List<BloodBankBillingResponse>();
            var patientDetails = new BloodBankPatientResponse(patient.Identifier, patient.NRICNumber, patient.PatientName, patient.PatientDOB, patient.NationalityId, patient.GenderId, patient.RaceId, patient.ResidenceStatusId, patient.BloodGroup, patient.NoOfIterations, patient.AntibodyScreening, patient.AntibodyIdentified, patient.ColdAntibodyIdentified, patient.ModifiedBy, patient.ModifiedByUserName, patient.IsTransfusionReaction, patient.Comments, patient.LastModifiedDateTime, patient.BloodGroupingDateTime, patient.AntibodyScreeningDateTime, patient.LatestAntibodyScreeningDateTime);
            return new Contracts.BloodBankRegistration(response.RegistrationId, response.NRICNumber, response.PatientName, response.PatientDOB, response.CaseOrVisitNumber, response.NationalityId, response.GenderId, response.RaceId, response.ResidenceStatusId, response.ClinicalDiagnosisId, response.IndicationOfTransfusionId, response.ClinicalDiagnosisOthers, response.IndicationOfTransfusionOthers, response.DoctorOthers, response.DoctorMCROthers, response.IsEmergency, response.WardId, response.ClinicId, response.DoctorId, products, specialRequirements, results, transactions, patientDetails, response.ProductTotal, response.IsActive,
            response.Status, response.NurseId, response.IssuingComments, response.LabAccessionNumber, response.ModifiedBy, response.ModifiedByUserName, response.LastModifiedDateTime, response.SampleReceivedDateTime, response.RegistrationDateTime, bloodSampleInventories, billings, response.PatientVisitNo, response.VisitId);

        }


        [HttpPost("bulkuploadfile")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.UploadDocResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BulkUploadFile(List<BulkFileUpload> lstjDTO)
        {
            var errors = Shared.Helpers.ValidateInput<List<BulkFileUpload>, BloodBankBulkFileUploadValidator>(lstjDTO, httpContextAccessor.HttpContext!);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }

            ErrorOr<Contracts.UploadDocResponse> uploadDetail = await _registrationService.BulkUploadFile(lstjDTO);
            return uploadDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<Contracts.UploadDocResponse>("", "200", uploadDetail.Value)),
             errors => Problem(errors));
        }

        [HttpPost("ConvertToBase64")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BulkFileUpload>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConvertToBase64(BulkFileUpload objjDTO)
        {
            ErrorOr<List<Contracts.BulkFileUpload>> uploadDetail = await _registrationService.ConvertToBase64(objjDTO);
            return uploadDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Contracts.BulkFileUpload>>("", "200", uploadDetail.Value)),
             errors => Problem(errors));
        }
    }
}
