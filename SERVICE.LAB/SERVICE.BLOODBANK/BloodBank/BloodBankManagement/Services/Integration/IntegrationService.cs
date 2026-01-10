using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.Integration;
using BloodBankManagement.Services.StartupServices;
using DEV.Common;
using DEV.Model.EF;
using DEV.Model.Integration;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared;

namespace BloodBankManagement.Services.BloodBankPatients
{
    public class IntegrationService : IIntegrationService
    {
        private readonly BloodBankDataContext dataContext;
        protected readonly IConfiguration Configuration;
        private readonly ILogger<IntegrationService> _logger;
        public readonly IJwtUtils _jwtUtils;
        public string url { get; set; }
        public IntegrationService(BloodBankDataContext dataContext, IConfiguration configuration, ILogger<IntegrationService> logger, IJwtUtils jwtUtils)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
            this._logger = logger;
            this._jwtUtils = jwtUtils;
            this.url = Configuration["Urls:LIS"];
        }

        public async Task<ErrorOr<List<BloodBankPatient>>> GetBloodBankPatientSearch(string searchText)
        {
            return await dataContext.BloodBankPatients.Where(x => x.NRICNumber.Contains(searchText)).OrderBy(x => x.LastModifiedDateTime).ToListAsync();
        }

        public async Task<ErrorOr<List<BloodBankRegistration>>> GetPDFReportDetails(reportrequestdetails reportrequestdetails, int venueNo, int venueBranchNo)
        {
            List<BloodBankRegistration> bloodBankRegistrations = new List<BloodBankRegistration>();

            if (!string.IsNullOrEmpty(reportrequestdetails.visitnumber) || !string.IsNullOrEmpty(reportrequestdetails.casenumber))
            {
                var searchText = reportrequestdetails.visitnumber ?? reportrequestdetails.casenumber;
                return await dataContext.BloodBankRegistrations.Where(x => x.CaseOrVisitNumber.Contains(searchText)).OrderBy(x => x.RegistrationId).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(reportrequestdetails.patientnumber))
            {
                return await dataContext.BloodBankRegistrations.Where(x => x.NRICNumber.Contains(reportrequestdetails.patientnumber) && x.RegistrationDateTime >= reportrequestdetails.startdate &&
                x.RegistrationDateTime <= reportrequestdetails.enddate).OrderBy(x => x.RegistrationId).ToListAsync();
            }
            return bloodBankRegistrations;
        }
        public async Task<ErrorOr<List<BloodSampleResult>>> GetTestDetails(long RegistrationId)
        {
            return await dataContext.BloodSampleResults.Where(x => x.BloodBankRegistrationId == RegistrationId).ToListAsync();
        }

        public async Task<Tuple<int, string>> InsertLISRegistration(Int64 orderId, Contracts.BloodBankRegistration input, User user)
        {
            var patientVisitNo = 0;
            var visitNo = "";
            bool isCertificateAvailable = this.Configuration.GetValue<bool>("IsCertificateAvailable");
            _logger.LogInformation("Lis {url}", this.url);
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
                var userData = new User { IsAdmin = true, IsSuperAdmin = true, VenueNo = user.VenueNo, VenueBranchNo = user.VenueBranchNo, UserNo = user.UserNo, UserName = user.UserName, IsProvisional = true, Roles = new List<string> { "BloodBankMgmt", "BloodBankMasters" } };
                var token = _jwtUtils.GenerateToken(user);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var json = System.Text.Json.JsonSerializer.Serialize(input);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var serviceResponseData = await httpClient.PostAsync(this.url + "/api/Integration/CreateBloodBankOrder", content);
                var lookupsstring = await serviceResponseData.Content.ReadAsStringAsync();
                var exp = new Exception(string.Format("response details {0} -> {1} - {2} - {3} - {4}", this.url, json, serviceResponseData.StatusCode, serviceResponseData, lookupsstring));
                var billingDetails = lookupsstring.Split("_");
                if(!string.IsNullOrEmpty(lookupsstring) && billingDetails.Length == 2) //billing details present
                {
                    patientVisitNo = Int32.Parse(billingDetails[0]);
                    visitNo = billingDetails[1];
                    var billings = await dataContext.BloodBankBillings.Where(x => x.BloodBankRegistrationId == orderId).ToListAsync();
                    if (billings != null)
                    {
                        billings.ForEach(bill =>
                        {
                            bill.IsBilled = true;
                        });
                        await dataContext.SaveChangesAsync();
                    }
                    var registration = dataContext.BloodBankRegistrations.First(x => x.RegistrationId == orderId);
                    if (billingDetails[1] != "-")
                    {
                        registration.PatientVisitNo = patientVisitNo;
                        registration.VisitId = visitNo;
                    }
                    await dataContext.SaveChangesAsync();
                }
                
                //MyDevException.Error(exp, "IntegrationService External API", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                //var serviceResponse = JsonConvert.DeserializeObject<MasterManagement.Contracts.ServiceResponse<Dictionary<string, string>>>(lookupsstring).Data;

            }
            catch (Exception exp)
            {
                _logger.LogInformation("exception {ExceptionMessage}", exp?.InnerException?.Message);
            }
            return Tuple.Create(patientVisitNo, visitNo);
        }

    }
}