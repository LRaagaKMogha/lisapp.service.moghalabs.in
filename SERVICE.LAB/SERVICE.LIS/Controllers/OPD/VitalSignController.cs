using System;
using System.Collections.Generic;
using System.Data;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class VitalSignController : ControllerBase
    {
        private readonly IVitalSignRepository _VitalSignRepository;
        public VitalSignController(IVitalSignRepository noteRepository)
        {
            _VitalSignRepository = noteRepository;
        }

        [HttpPost]
        [Route("api/VitalSign/InsertVitalSign")]
        public SaveVitalSignDTOResponse InsertVitalSign(SaveVitalSignDTORequest req)
        {
            SaveVitalSignDTOResponse result = new SaveVitalSignDTOResponse();
            try
            {
                result = _VitalSignRepository.InsertVitalSign(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.InsertVitalSign", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebno, req.userno);
            }
            return result;
        }
        [HttpPost]
        [Route("api/VitalSign/GetVitalSignList")]
        public List<VitalSignDTO> GetVitalSignList(VitalSignDTORequest req)
        {
            List<VitalSignDTO> result = new List<VitalSignDTO>();
            try
            {
                result = _VitalSignRepository.GetVitalSignList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetVitalSignList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebno, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/VitalSign/GetVitalSignMasters")]
        public List<VitalSignMastersResponse> GetVitalSignMasters(VitalSignMastersRequest req)
        {
            List<VitalSignMastersResponse> result = new List<VitalSignMastersResponse>();
            try
            {
                result = _VitalSignRepository.GetVitalSignMasters(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetVitalSignMasters", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebno, req.userno);
            }
            return result;
        }

        [HttpPost]
        [Route("api/VitalSign/SaveAllergyDetails")]
        public SaveAllergyResponse SaveAllergyDetails(SaveAllergyRequest objDTO)
        {
            SaveAllergyResponse result = new SaveAllergyResponse();
            try
            {
                result = _VitalSignRepository.SaveAllergyDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.SaveAllergyDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, objDTO.venueno, objDTO.venuebno, objDTO.userno);
            }
            return result;
        }
        [HttpPost]
        [Route("api/VitalSign/GetAllergyDetails")]
        public List<GetAllergyResponse> GetAllergyDetails(GetAllergyRequest RequestItem)
        {
            List<GetAllergyResponse> result = new List<GetAllergyResponse>();
            try
            {
                result = _VitalSignRepository.GetAllergyDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetAllergyDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebno, RequestItem.userno);
            }
            return result;
        }

        [HttpPost]
        [Route("api/VitalSign/SaveDiseasesDetails")]
        public SaveDiseasesResponse SaveDiseasesDetails(SaveDiseasesRequest objDTO)
        {
            SaveDiseasesResponse result = new SaveDiseasesResponse();
            try
            {
                result = _VitalSignRepository.SaveDiseasesDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.SaveDiseasesDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, objDTO.venueno, objDTO.venuebno, objDTO.userno);
            }
            return result;
        }
        [HttpPost]
        [Route("api/VitalSign/GetDiseasesDetails")]
        public List<GetDiseasesResponse> GetDiseasesDetails(GetDiseasesRequest RequestItem)
        {
            List<GetDiseasesResponse> result = new List<GetDiseasesResponse>();
            try
            {
                result = _VitalSignRepository.GetDiseasesDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetDiseasesDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebno, RequestItem.userno);
            }
            return result;
        }

        [HttpPost]
        [Route("api/VitalSign/GetVitalResultHistory")]
        public string GetVitalResultHistory(GetAllergyRequest RequestItem)
        {
            string result = string.Empty;
            try
            {
                result = _VitalSignRepository.GetVitalResultHistory(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetVitalResultHistory", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebno, RequestItem.userno);
            }
            return result;
        }
        [HttpPost]
        [Route("api/VitalSign/GetVaccineSchedule")]
        public List<lstVaccineSchedule> GetVaccineSchedule(GetVaccineScheduleRequest req)
        {
            List<lstVaccineSchedule> result = new List<lstVaccineSchedule>();
            try
            {
                result = _VitalSignRepository.GetVaccineSchedule(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetVaccineSchedule", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/VitalSign/SaveVaccineRecord")]
        public void SaveVaccineRecord(SaveVaccineRecordDTORequest req)
        {
            try
            {
                _VitalSignRepository.SaveVaccineRecord(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.SaveVaccineRecord", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
        }

        [HttpPost]
        [Route("api/VitalSign/GetPatientLatestVisit")]
        public lstPatientLatestVisit GetPatientLatestVisit(GetLatestPatientVisitRequest req)
        {
            lstPatientLatestVisit result = new lstPatientLatestVisit();
            try
            {
                result = _VitalSignRepository.GetPatientLatestVisit(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetPatientLatestVisit", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/VitalSign/GetVitalResultDateTime")]
        public List<GetVitalResultResponse> GetVitalResultDateTime(GetAllergyRequest RequestItem)
        {
            List<GetVitalResultResponse> result = new List<GetVitalResultResponse>();
            try
            {
                result = _VitalSignRepository.GetVitalResultDateTime(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignController.GetVitalResultDateTime", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebno, RequestItem.userno);
            }
            return result;
        }
    }
}