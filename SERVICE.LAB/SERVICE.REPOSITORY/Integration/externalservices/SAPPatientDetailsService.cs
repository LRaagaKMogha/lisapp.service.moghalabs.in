using DEV.Common;
using Service.Model.Integration;
using Microsoft.Extensions.Configuration;
using SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Integration.externalservices
{
    public class SAPPatientDetailsService : IPatientDetailsService
    {
        public IConfiguration config { get; set; }
        public string url { get; set; }
        public SAPPatientDetailsService(IConfiguration _config)
        {
            config = _config;
            this.url = config["Urls:SAP"];
        }
        public async Task<ExternalPatientDetails> GetPatientDetails(string patientId)
        {
            var responsePatient = new ExternalPatientDetails();
            try
            {
                BasicHttpsBinding binding = new BasicHttpsBinding();
                EndpointAddress address = new EndpointAddress(this.url);
                PatientInfoClient patientInfoClient = new PatientInfoClient(binding, address);
                var request = new GetPatientInfoRequest
                {
                    request = new PatientRequest
                    {
                        //Patnr = patientId,
                        Einri = "RHO",
                        //DocNum = "S123567D",
                        Falnr = patientId
                    }
                };
                var response = await patientInfoClient.GetPatientInfoAsync(request);
                var data = response.GetPatientInfoResult;
                responsePatient = new ExternalPatientDetails
                {
                    EmailID  = data.Email,
                    CountryName = data.Land1,
                    MobileNumer = data.AdMonmbr,
                    Address = data.AdStreet,
                    PatientUnitNo = string.Empty,
                    PatientHomeNo = data.AdTlnmbr,
                    PatientBuilding = data.Building,
                    PatientBlock = data.AdBldng,
                    PatientFloor = data.AdFloor,
                    AltMobileNumber = data.Offnmbr,
                    AreaName = data.AdPstcd1,
                    CityNo = 0,
                    maritalStatus = "",
                    MiddleName = "",
                    SexId = data.Sexid == "2" ? "1" : "2",
                    FirstName = string.IsNullOrEmpty(data.NameFirst) ? data.NameLast : data.NameFirst,
                    LastName = string.IsNullOrEmpty(data.NameFirst) ? string.Empty:data.NameLast,
                    DateOfBirth = data.Birthdt,
                    AlternativeIdType = data.Altidtype,
                    AlternativeIdNumber = data.PntExtnr,
                    RaceCode = data.Race,
                    RaceDescription = data.Racedesc,
                    NationalityCode = data.BuNatio,
                    NationalityDescription = data.Natiodesc,
                    IsVip = data.VipInd == "X",
                    NursingOU = data.Orgpf,
                    Bed = data.Zimmr,
                    Room = data.Bett,
                    IsAllergy = data.AllergyInd == "X",
                    AllergyDetails = data.Allergen,
                    PostalCode = data.AdPstcd1,
                    DocumentNumber = data.DocNum,
                    PatientNumber = data.Patnr,
                    PatientOccupation = data.PatOccu,
                    RoomNumber = data.AdRoomnum
                };
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetWaitingList", ExceptionPriority.High, ApplicationType.REPOSITORY, 1, 1, 0);
                return null;
            }
            return responsePatient;
        }
    }
}
