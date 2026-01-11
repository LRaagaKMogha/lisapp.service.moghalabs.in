using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dev.IRepository;
using Service.Model;
using Service.Model.Integration;
using System.Text.Json;
using DEV.Common;
using Service.Model.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Dev.Repository.Integration.externalservices
{
    public class BloodBankService
    {
        private readonly ILogger<BloodBankService> _logger;
        public readonly IJWTManagerRepository _jwtUtils;
        public IConfiguration config { get; set; }
        public string url { get; set; }
        public BloodBankService(IConfiguration _config, ILogger<BloodBankService> logger, IJWTManagerRepository jwtUtils)
        {
            config = _config;
            _logger = logger;
            _jwtUtils = jwtUtils;
            this.url = config["Urls:BloodBank"]; ///BBMgmtService/BloodBankRegistration
        }
        private List<TestAdditionalInformation> GetTestAdditionalInformation(IntegrationOrderDetailsResponse order, FrontOffficeDTO input)
        {
            List<TestDetailsRetrieval> testDetails = new List<TestDetailsRetrieval>()
            {
                new TestDetailsRetrieval { TestType = "Gender", Code = input.Gender },
                 new TestDetailsRetrieval { TestType = "clinicaldiagnosiscode", Code = order.IntegrationOrderPatientDetails.diagnosiscode},
                 new TestDetailsRetrieval { TestType = "clinicaldiagnosis", Code = order.IntegrationOrderPatientDetails.diagnosisdescription},
                 new TestDetailsRetrieval { TestType = "transfusionindicator", Code = order.IntegrationOrderPatientDetails.transfusionindication},
            };
            var testData = JsonConvert.SerializeObject(testDetails);
            List<TestAdditionalInformation> response = new List<TestAdditionalInformation>();

            try
            {
                using (var context = new IntegrationContext(config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var testInfo = new SqlParameter("testInfo", testData);

                    response = context.TestAdditionalInformation.FromSqlRaw("Execute dbo.Pro_GetTestAdditionalInformation @testInfo", testInfo).ToList();
                }
            }
            catch (Exception exp)
            {

            }

            return response;
        }
        private Dictionary<string, Dictionary<string, string>> MapRequest(FrontOffficeDTO input, UserClaimsIdentity user, IntegrationOrderDetailsResponse response)
        {
            var request = new Dictionary<string, Dictionary<string, string>>();
            var testAdditionalInformation = GetTestAdditionalInformation(response, input);
            var clinicalDiagnosisId = HelperMethods.getAdditionalId(testAdditionalInformation, "clinicaldiagnosis");
            if(clinicalDiagnosisId == 0 ) HelperMethods.getAdditionalId(testAdditionalInformation, "clinicaldiagnosiscode");
            var transfusionIndicatorId = HelperMethods.getAdditionalId(testAdditionalInformation, "transfusionindicator");
            var patientInformation = new Dictionary<string, string>()
            {
                {"NRICNumber", response.IntegrationOrderPatientDetails.patientid },
                {"PatientName", input.FullName },
                {"PatientDOB", input.DOB },
                {"CaseOrVisitNumber", HelperMethods.getSourceSystem(response.SourceSystem) == "RCMS" ? response.IntegrationOrderVisitDetails.visitno : response.IntegrationOrderVisitDetails.casenumber },
                {"Nationality", input.NationalityNo.ToString() },
                {"Gender", HelperMethods.getAdditionalId(testAdditionalInformation, "Gender").ToString()}, // 
                {"Race",  input.RaceNo.ToString()},
                {"Residence", "" }, //TODO: 
                {"ClinicalDiagnosis", clinicalDiagnosisId.ToString() },
                {"TransfusionIndicator", transfusionIndicatorId.ToString() },
                {"ClinicalDiagnosisOthers", string.IsNullOrEmpty(response.IntegrationOrderPatientDetails.diagnosisdescription) ? "None" :  response.IntegrationOrderPatientDetails.diagnosisdescription},
                {"IndicationOfTransfusionOthers", string.IsNullOrEmpty(response.IntegrationOrderPatientDetails.transfusionindication) ? "None" :  response.IntegrationOrderPatientDetails.transfusionindication},
                {"DoctorOthers", "" },
                {"DoctorMCROthers", "" },
                {"IsEmergency", input.IsStat ? "T": "F" },
                {"WardId", input.WardNo.ToString() },
                {"ClinicId", "" },
                {"DoctorId", input.PhysicianNo.ToString() },
                {"ModifiedBy", user.UserNo.ToString() },
                {"ModifiedUserName", user.UserName },
                {"SampleTypeId", response.IntegrationOrderTestDetails.FirstOrDefault(x => x.SampleTypeId > 0)?.SampleTypeId.ToString() },
                {"TestShortNames", string.Join(",", input.Orders.Select(x => x.TestCode)) }

            };
            var results = new Dictionary<string, string>();
            input.Orders.ForEach(row =>
            {
                results.Add(row.TestNo.ToString(), row.TestCode);
            });
            request.Add("PatientInformation", patientInformation);
            request.Add("Results", results);
            return request;
        }

        public async Task<Tuple<int, string>> InsertBloodBankRegistration(FrontOffficeDTO input, UserClaimsIdentity user, IntegrationOrderDetailsResponse response)
        {
            var patientVisitNo = 0;
            var visitNo = "";
            var request = MapRequest(input, user, response);
            bool isCertificateAvailable = this.config.GetValue<bool>("IsCertificateAvailable");
            _logger.LogInformation("bloodBankUrl {url}", this.url);
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage requestMessage, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors sslErrors) =>
            {
                _logger.LogInformation("Requested URI: {uri}", requestMessage.RequestUri);
                _logger.LogInformation("Effective date: {date}", certificate?.GetEffectiveDateString());
                _logger.LogInformation("Exp date: {date}", certificate?.GetExpirationDateString());
                _logger.LogInformation("Issuer: {issuer}", certificate?.Issuer);
                _logger.LogInformation("Subject: {subject}", certificate?.Subject);

                // Based on the custom logic it is possible to decide whether the client considers certificate valid or not
                _logger.LogInformation("Errors: {errors}", sslErrors);

                return sslErrors == SslPolicyErrors.None || !isCertificateAvailable;
            };
            var httpClient = new HttpClient(httpClientHandler);
            try
            {
                var userData = new UserResponseEntity { IsAdmin = true, IsSuperAdmin = true, VenueNo = user.VenueNo, VenueBranchNo = user.VenueBranchNo, UserNo = user.UserNo, UserName = user.UserName, IsProvisional = true, lstUserRoleName = new List<UserRoleNameDTO> { new UserRoleNameDTO() { RoleName = "BloodBankMgmt" }, new UserRoleNameDTO() { RoleName = "BloodBankMasters" } } };
                var token = _jwtUtils.Authenticate(userData);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var json = System.Text.Json.JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var serviceResponseData = await httpClient.PostAsync(this.url + "/BloodBankRegistration/createbloodbankorder", content);
                var lookupsstring = await serviceResponseData.Content.ReadAsStringAsync();
                var exp = new Exception(string.Format("response details {0} -> {1} - {2} - {3} - {4}", this.url, json,  serviceResponseData.StatusCode, serviceResponseData, lookupsstring));
                MyDevException.Error(exp, "BloodBankService", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<Dictionary<string, string>>>(lookupsstring).Data;
                patientVisitNo = Int32.Parse(serviceResponse["VisitNo"]);
                visitNo = serviceResponse["LabAccessionNo"];
            }
            catch (Exception exp)
            {
                _logger.LogInformation("exception {ExceptionMessage}", exp?.InnerException?.Message);
            }
            return Tuple.Create(patientVisitNo, visitNo);
        }
    }
}
