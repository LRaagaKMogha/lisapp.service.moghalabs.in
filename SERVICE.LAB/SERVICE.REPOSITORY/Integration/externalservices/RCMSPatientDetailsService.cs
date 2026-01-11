using DEV.Common;
using Service.Model.Integration;
using Microsoft.Extensions.Configuration;
using RCMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Integration.externalservices
{
    public class RCMSPatientDetailsService : IPatientDetailsService
    {
        public IConfiguration config { get; set;  }
        public string url { get; set; }
        public RCMSPatientDetailsService(IConfiguration _config)
        {
            config = _config;
            this.url = config["Urls:RCMS"];
        }
        public async Task<ExternalPatientDetails> GetPatientDetails(string patientId)
        {
            var responsePatient = new ExternalPatientDetails();
            try
            {
                BasicHttpsBinding binding = new BasicHttpsBinding();
                EndpointAddress address = new EndpointAddress(this.url);
                PatientServiceClient rcmsPatientService = new PatientServiceClient(binding, address);
                var request = new GetPatientInfoRequest
                {
                    param = new PatientInfoParameterDTO
                    {
                        SearchParameter = new PatientInfoSearchParameter
                        {
                            IncludeInactiveRecord = false,
                            PatientSystemIds = new string[] { patientId }
                        },
                        SystemId = "LIS"
                    }
                };
                var response = await rcmsPatientService.GetPatientInfoAsync(request);
                var data = response.GetPatientInfoResult.SearchResult.FirstOrDefault();
                if(data != null)
                {
                    responsePatient = new ExternalPatientDetails
                    {
                        EmailID = data.Email,
                        CountryName = data.CountryOfResidence,
                        MobileNumer = data.Mobile,
                        Address = data.LocalAddress != null ? data.LocalAddress.Street : null,
                        PatientHomeNo = data.HomeNo,
                        PatientUnitNo = data.LocalAddress != null ? data.LocalAddress.UnitNo : null,
                        PatientFloor = data.LocalAddress != null ? data.LocalAddress.FloorNo : null,
                        PatientBlock = data.LocalAddress != null ? data.LocalAddress.BlockHouseLotNumber : null,
                        PatientBuilding = data.LocalAddress != null ? data.LocalAddress.Building : null,
                        AltMobileNumber = data.OfficeNo,
                        AreaName = data.LocalAddress != null ? data.LocalAddress.Street : null,
                        CityNo = 0,
                        maritalStatus = data.MaritalStatus,
                        MiddleName = "",
                        SexId = data.Gender == "F" ? "2": "1", //F, M, U
                        FirstName = data.Name,
                        LastName = "",
                        DateOfBirth = data.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        AlternativeIdType = data.AlterNativeIDType, //M, N, O, P
                        AlternativeIdNumber = data.AlternativeIDNumber,
                        RaceCode = data.Race,
                        RaceDescription = data.Race,
                        NationalityCode = data.NationalityCode,
                        NationalityDescription = data.Nationality,
                        IsVip = false,//NOT PROVIDED.
                        NursingOU = "",
                        Bed = "",
                        Room = "",
                        RoomNumber = "",
                        IsAllergy = false,
                        AllergyDetails = "",
                        PostalCode = data.LocalAddress != null ? data.LocalAddress.PostalCode : "000000",
                        DocumentNumber = "",
                        PatientNumber = patientId,
                        PatientOccupation = ""
                    };
                }
               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetWaitingList", ExceptionPriority.High, ApplicationType.REPOSITORY, 1, 1, 0);
                return responsePatient;
            }
            return responsePatient;
        }
    }
}
