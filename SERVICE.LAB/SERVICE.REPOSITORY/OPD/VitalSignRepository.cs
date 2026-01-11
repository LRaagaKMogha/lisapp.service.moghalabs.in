using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Service.Model.EF;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using System.Data;
using Serilog;
using System.Text.RegularExpressions;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Service.Model.Sample;

namespace Dev.Repository
{
    public class VitalSignRepository : IVitalSignRepository
    {
        private IConfiguration _config;
        public VitalSignRepository(IConfiguration config) { _config = config; }

        public SaveVitalSignDTOResponse InsertVitalSign(SaveVitalSignDTORequest objDTO)
        {
            var vitalSignXml = objDTO?.lstSaveVitalSignDTO;
            CommonHelper commonUtility = new CommonHelper();
            var vitalSignlist = commonUtility.ToXML(vitalSignXml);
            SaveVitalSignDTOResponse result = new SaveVitalSignDTOResponse();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Vitalxml = new SqlParameter("vitalxml", vitalSignlist);
                    var _VenueNo = new SqlParameter("venueNo", objDTO?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", objDTO?.venuebno);
                    var _UserNo = new SqlParameter("userNo", objDTO?.userno);
                    var _Patientno = new SqlParameter("patientno", objDTO?.patientno);
                    var _OPDPatientno = new SqlParameter("opdpatientno", objDTO?.opdpatientno);
                    var _OPDPatientAppointmentNo = new SqlParameter("OPDPatientAppointmentNo", objDTO?.OPDPatientAppointmentNo);

                    result = context.SaveVitalSign.FromSqlRaw(
                   "Execute dbo.pro_InsertVitalEntryDetails @vitalxml, @venueno, @venuebno, @userno, @patientno, @opdpatientNo, @OPDPatientAppointmentNo",
                   _Vitalxml, _VenueNo, _VenueBranchNo, _UserNo, _Patientno, _OPDPatientno, _OPDPatientAppointmentNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.InsertVitalSign", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO?.venueno, objDTO?.venuebno, objDTO?.userno);
            }
            return result;
        }
        public List<VitalSignDTO> GetVitalSignList(VitalSignDTORequest RequestItem)
        {
            List<VitalSignDTO> result = new List<VitalSignDTO>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {                  
                    var _VenueNo = new SqlParameter("venueno", RequestItem?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", RequestItem?.venuebno);
                    var _Vitalno = new SqlParameter("vitalno", RequestItem?.vitalno);
                    var _Patientno = new SqlParameter("patientno", RequestItem?.patientno);                   

                    result = context.GetVitalSignList.FromSqlRaw(
                    "Execute dbo.pro_GetVitalEntryDetails @venueno,@venuebno,@vitalno,@patientno",
                    _VenueNo, _VenueBranchNo, _Vitalno, _Patientno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetVitalSignList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.venueno, RequestItem?.venuebno, RequestItem?.userNo);
            }
            return result;
        }

        public List<VitalSignMastersResponse> GetVitalSignMasters(VitalSignMastersRequest RequestItem)
        {
            List<VitalSignMastersResponse> result = new List<VitalSignMastersResponse>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", RequestItem?.venuebno);
                    var _Mastertype = new SqlParameter("mastertype", RequestItem?.mastertype);
                    var _Patientno = new SqlParameter("patientno", RequestItem?.patientno);

                    result = context.GetVitalSignMaster.FromSqlRaw(
                    "Execute dbo.pro_GetVitalSignMasters @venueno, @venuebno, @mastertype, @patientno",
                    _VenueNo,_VenueBranchNo,_Mastertype,_Patientno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetVitalSignMasters", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.venueno, RequestItem?.venuebno, RequestItem?.userno);
            }
            return result;
        }

        #region Allergy Details
        public List<GetAllergyResponse> GetAllergyDetails(GetAllergyRequest RequestItem)
        {
            List<GetAllergyResponse> result = new List<GetAllergyResponse>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", RequestItem?.venuebno);
                    var _Patientno = new SqlParameter("patientno", RequestItem?.patientno);
                    var _OPDAppointmentno = new SqlParameter("OPDAppointmentNo", RequestItem?.OPDAppointmentNo);

                    result = context.GetAllergyDetails.FromSqlRaw(
                    "Execute dbo.pro_GetAllergyDetails @venueno,@venuebno,@patientno,@opdAppointmentNo",
                    _VenueNo, _VenueBranchNo,_Patientno, _OPDAppointmentno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetAllergyDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.venueno, RequestItem?.venuebno, RequestItem?.userno);
            }
            return result;
        }
        public SaveAllergyResponse SaveAllergyDetails(SaveAllergyRequest objDTO)
        {
            var vitalSignXml = objDTO?.lstAllergyDetails;
            CommonHelper commonUtility = new CommonHelper();
            var vitalSignlist = commonUtility.ToXML(vitalSignXml);
            SaveAllergyResponse result = new SaveAllergyResponse();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Allergyxml = new SqlParameter("allergyxml", vitalSignlist);
                    var _VenueNo = new SqlParameter("venueNo", objDTO?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", objDTO?.venuebno);
                    var _UserNo = new SqlParameter("userNo", objDTO?.userno);
                    var _Patientno = new SqlParameter("patientno", objDTO?.patientno);
                    var _OPDPatientno = new SqlParameter("opdpatientno", objDTO?.opdpatientno);
                    var _OPDPatientAppointmentNo = new SqlParameter("OPDPatientAppointmentNo", objDTO?.OPDPatientAppointmentNo);

                    result = context.SaveAllergyDetails.FromSqlRaw(
                   "Execute dbo.pro_InsertAllergyDetails @allergyxml,@venueno,@venuebno,@userno,@patientno,@opdpatientNo, @OPDPatientAppointmentNo",
                   _Allergyxml, _VenueNo, _VenueBranchNo, _UserNo, _Patientno, _OPDPatientno, _OPDPatientAppointmentNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.SaveAllergyDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO?.venueno, objDTO?.venuebno, objDTO?.userno);
            }
            return result;
        }
        #endregion

        #region Diseases Details
        public List<GetDiseasesResponse> GetDiseasesDetails(GetDiseasesRequest RequestItem)
        {
            List<GetDiseasesResponse> result = new List<GetDiseasesResponse>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", RequestItem?.venuebno);
                    var _Patientno = new SqlParameter("patientno", RequestItem?.patientno);
                    var _OPDAppointmentno = new SqlParameter("OPDAppointmentNo", RequestItem?.OPDAppointmentNo);

                    result = context.GetDiseasesDetails.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseDetails @venueno,@venuebno,@patientno,@opdAppointmentNo",
                    _VenueNo, _VenueBranchNo, _Patientno, _OPDAppointmentno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetDiseasesDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.venueno, RequestItem?.venuebno, RequestItem?.userno);
            }
            return result;
        }
        public SaveDiseasesResponse SaveDiseasesDetails(SaveDiseasesRequest objDTO)
        {
            var vitalSignXml = objDTO?.lstDiseasesDetails;
            CommonHelper commonUtility = new CommonHelper();
            var vitalSignlist = commonUtility.ToXML(vitalSignXml);
            SaveDiseasesResponse result = new SaveDiseasesResponse();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _diseasexml = new SqlParameter("diseasexml", vitalSignlist);
                    var _VenueNo = new SqlParameter("venueNo", objDTO?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", objDTO?.venuebno);
                    var _UserNo = new SqlParameter("userNo", objDTO?.userno);
                    var _Patientno = new SqlParameter("patientno", objDTO?.patientno);
                    var _OPDPatientno = new SqlParameter("opdpatientno", objDTO?.opdpatientno);
                    var _OPDPatientAppointmentNo = new SqlParameter("OPDPatientAppointmentNo", objDTO?.OPDPatientAppointmentNo);

                    result = context.SaveDiseasesDetails.FromSqlRaw(
                   "Execute dbo.pro_InsertDiseaseDetails @diseasexml,@venueno,@venuebno,@userno,@patientno,@opdpatientNo, @OPDPatientAppointmentNo",
                   _diseasexml, _VenueNo, _VenueBranchNo, _UserNo, _Patientno, _OPDPatientno, _OPDPatientAppointmentNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.SaveDiseasesDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO?.venueno, objDTO?.venuebno, objDTO?.userno);
            }
            return result;
        }
        #endregion

        #region vital result history
        public string GetVitalResultHistory(GetAllergyRequest RequestItem)
        {
            string result = string.Empty;
            try
            {
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("venueno", RequestItem?.venueno.ToString());
                objdictionary.Add("venuebno", RequestItem?.venuebno.ToString());
                objdictionary.Add("patientno", RequestItem?.patientno.ToString());

                DataTable dta = objReportContext.getdatatable(objdictionary, "pro_GetVitalResultDetails");
                result = JsonConvert.SerializeObject(dta); 
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetVitalResultHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.venueno, RequestItem?.venuebno, (RequestItem?.userno));
            }
            return result;
        }
        #endregion

        #region VaccineRecords Details
        public List<lstVaccineSchedule> GetVaccineSchedule(GetVaccineScheduleRequest req)
        {
            List<lstVaccineSchedule> result = new List<lstVaccineSchedule>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNo = new SqlParameter("PatientNo", req.PatientNo ?? (object)DBNull.Value);
                    var _IsAdult = new SqlParameter("IsAdult", req.IsAdult);

                    result = context.lstVaccineSchedule.FromSqlRaw(
                        "EXEC dbo.pro_GetVaccineSchedule @PatientNo, @IsAdult",
                        _PatientNo, _IsAdult).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetVaccineSchedule", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public void SaveVaccineRecord(SaveVaccineRecordDTORequest req)
        {
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    foreach (var item in req.lstVaccineRecord)
                    {
                        var _PatientNo = new SqlParameter("PatientNo", item.PatientNo);
                        var _IsAdult = new SqlParameter("IsAdult", item.IsAdult);
                        var _VaccineId = new SqlParameter("VaccineId", item.VaccineId);
                        var _DateOfVaccination = new SqlParameter("DateOfVaccination", item.DateOfVaccination);
                        var _DueDate = new SqlParameter("DueDate", (object?)item.DueDate ?? DBNull.Value);
                        var _VaccinatedBy = new SqlParameter("VaccinatedBy", item.VaccinatedBy);

                        context.Database.ExecuteSqlRaw(
                            "EXEC dbo.pro_SaveVaccineRecord @PatientNo,@IsAdult, @VaccineId, @DateOfVaccination, @DueDate, @VaccinatedBy",
                            _PatientNo, _IsAdult, _VaccineId, _DateOfVaccination, _DueDate, _VaccinatedBy);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveVaccineRecord", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
        }

        public lstPatientLatestVisit GetPatientLatestVisit(GetLatestPatientVisitRequest req)
        {
            lstPatientLatestVisit result = null;

            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var patientNoParam = new SqlParameter("@PatientNo", req.PatientNo);

                    result = context.lstPatientLatestVisit
                        .FromSqlRaw("EXEC dbo.pro_GetLatestPatientVisitsVaccine @PatientNo", patientNoParam)
                        .AsEnumerable()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientLatestVisit", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }

            return result;
        }
        #endregion

        public List<GetVitalResultResponse> GetVitalResultDateTime(GetAllergyRequest RequestItem)
        {
            List<GetVitalResultResponse> result = new List<GetVitalResultResponse>();
            try
            {
                using (var context = new VitalSignContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem?.venueno);
                    var _VenueBranchNo = new SqlParameter("venuebno", RequestItem?.venuebno);
                    var _Patientno = new SqlParameter("patientno", RequestItem?.patientno);

                    result = context.GetVitalResultDateTime.FromSqlRaw(
                    "Execute dbo.pro_GetVitalResultDateTime @venueno, @venuebno, @patientno",
                    _VenueNo, _VenueBranchNo, _Patientno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VitalSignRepository.GetVitalResultDateTime - Patient No : " + RequestItem?.patientno, ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.venueno, RequestItem.venuebno, RequestItem.userno);
            }
            return result;
        }
    }
}
