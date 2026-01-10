using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Model.EF;
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
using DEV.Model.Sample;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using ErrorOr;

namespace Dev.Repository
{
    public class OPDPatientRepository : IOPDPatientRepository
    {
        private IConfiguration _config;
        public OPDPatientRepository(IConfiguration config) { _config = config; }

        public OPDPatientDTOResponse InsertOPDPatient(OPDPatientOfficeDTO objDTO)
        {
            OPDPatientDTOResponse result = new OPDPatientDTOResponse();
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string prevApptProcedureDtlXML = "";
                if (objDTO.prevApptProcedureDtl != null && objDTO.prevApptProcedureDtl.Count > 0)
                {
                    prevApptProcedureDtlXML = commonUtility.ToXML(objDTO.prevApptProcedureDtl);
                }

                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDPatientNo = new SqlParameter("OPDPatientNo", objDTO?.OPDPatientNo);
                    var _oPDPatientAppointmentNo = new SqlParameter("oPDPatientAppointmentNo", objDTO?.oPDPatientAppointmentNo);
                    var _TitleCode = new SqlParameter("TitleCode", objDTO?.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO?.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO?.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO?.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO?.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO?.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO?.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO?.AgeType.Substring(0, 1));
                    var _MobileNumber = new SqlParameter("MobileNumber", objDTO?.MobileNumber.ValidateEmpty());
                    var _AltMobileNumber = new SqlParameter("WhatsappNo", objDTO?.WhatsappNo.ValidateEmpty());
                    var _EmailID = new SqlParameter("EmailID", objDTO?.EmailID.ValidateEmpty());
                    var _SecondaryEmailID = new SqlParameter("SecondaryEmailID", objDTO?.SecondaryEmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO?.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO?.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO?.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO?.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO?.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO?.Pincode.ValidateEmpty());
                    var _AppointmentMode = new SqlParameter("AppointmentMode", objDTO?.AppointmentMode);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", objDTO?.SpecializationNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", objDTO?.PhysicianNo);
                    var _Reason = new SqlParameter("Reason", objDTO?.Reason.ValidateEmpty());
                    var _AppointmentDateTime = new SqlParameter("AppointmentDateTime", objDTO?.AppointmentDateTime.ValidateEmpty());
                    var _ArrivedDateTime = new SqlParameter();
                    if (string.IsNullOrEmpty(objDTO?.ArrivedDateTime))
                        _ArrivedDateTime = new SqlParameter("ArrivedDateTime", DBNull.Value);
                    else
                        _ArrivedDateTime = new SqlParameter("ArrivedDateTime", objDTO?.ArrivedDateTime.ValidateEmpty());
                    var _appointmentStatus = new SqlParameter("appointmentStatus", objDTO?.appointmentStatus);
                    var _IsNew = new SqlParameter("IsNew", objDTO?.IsNew);
                    var _IsEmergency = new SqlParameter("IsEmergency", objDTO?.IsEmergency);
                    var _IsVIP = new SqlParameter("IsVIP", objDTO?.IsVIP);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO?.UserNo.ToString());
                    var _EmiratesId = new SqlParameter("EmiratesId", objDTO?.EmiratesId.ValidateEmpty());
                    var _IsAutoEmail = new SqlParameter("IsAutoEmail", objDTO?.IsAutoEmail);
                    var _IsAutoSMS = new SqlParameter("IsAutoSMS", objDTO?.IsAutoSMS);
                    var _IsAutoWhatsApp = new SqlParameter("IsAutoWhatsApp", objDTO?.IsAutoWhatsApp);
                    var _IsSysCalDOB = new SqlParameter("IsSysCalDOB", objDTO?.IsSysCalDOB);
                    var _PatientVisitID = new SqlParameter("PatientVisitID", objDTO?.PatientVisitID);
                    var _ReasonType = new SqlParameter("ReasonType", objDTO?.ReasonType);
                    var _RefferalType = new SqlParameter("RefferalType", objDTO?.RefferalType);
                    var _PhysNo = new SqlParameter("PhysNo", objDTO?.PhysNo);
                    var _RefTypeothers = new SqlParameter("RefTypeothers", objDTO?.RefTypeothers);
                    var _LastVisitDate = new SqlParameter("LastVisitDate", objDTO?.LastVisitDate == null ? "" : objDTO?.LastVisitDate);
                    var _LastConfees = new SqlParameter("LastConfees", objDTO?.LastConfees);
                    var _prevApptProcedureDtlXML = new SqlParameter("prevApptProcedureDtlXML", prevApptProcedureDtlXML);
                    var _ApptmtDetails = new SqlParameter("ApptmtDetails", string.Empty);
                    var _ApptCancelReason = new SqlParameter("CancelReason", objDTO?.CancelReason);
                    var _ApptRescheduleReason = new SqlParameter("RescheduleReason", objDTO?.RescheduleReason);

                    result = context.OPDPatient.FromSqlRaw(
                   "Execute dbo.pro_InsertOPDPatient " +
                   "@OPDPatientNo,@oPDPatientAppointmentNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@MobileNumber,@WhatsappNo," +
                   "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@AppointmentMode,@SpecializationNo,@PhysicianNo,@Reason,@AppointmentDateTime,@ArrivedDateTime," +
                   "@appointmentStatus,@IsNew,@IsEmergency,@IsVIP,@VenueNo,@VenueBranchNo,@userNo,@EmiratesId,@IsAutoEmail,@IsAutoSMS,@IsAutoWhatsApp,@IsSysCalDOB,@PatientVisitID,@ReasonType," +
                   "@RefferalType, @PhysNo, @RefTypeothers, @LastVisitDate, @LastConfees, @prevApptProcedureDtlXML, @ApptmtDetails, @CancelReason, @RescheduleReason",
                   _OPDPatientNo, _oPDPatientAppointmentNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _MobileNumber, _AltMobileNumber,
                   _EmailID, _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _AppointmentMode, _SpecializationNo, _PhysicianNo, _Reason,
                   _AppointmentDateTime, _ArrivedDateTime, _appointmentStatus, _IsNew, _IsEmergency, _IsVIP, _VenueNo, _VenueBranchNo, _UserID, _EmiratesId, 
                   _IsAutoEmail, _IsAutoSMS, _IsAutoWhatsApp, _IsSysCalDOB, _PatientVisitID, _ReasonType, _RefferalType, _PhysNo, _RefTypeothers, _LastVisitDate, _LastConfees, 
                   _prevApptProcedureDtlXML, _ApptmtDetails, _ApptCancelReason, _ApptRescheduleReason).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.InsertOPDPatient : PatientVisitNo - " + objDTO?.PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO?.VenueNo, objDTO?.VenueBranchNo, objDTO?.UserNo);
            }
            return result;
        }
        public List<OPDPatientDTOList> GetOPDPatientList(CommonFilterRequestDTO RequestItem)
        {
            List<OPDPatientDTOList> result = new List<OPDPatientDTOList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", RequestItem?.departmentNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.physicianNo);
                    var _AppointmentStatus = new SqlParameter("AppointmentStatus", RequestItem?.orderStatus);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem?.userNo);
                    var _AppointmentCategory = new SqlParameter("AppointmentCategory", RequestItem?.AppointmentCategory);
                    var _AppointmentMode = new SqlParameter("AppointmentMode", RequestItem?.AppointmentMode);
                    var _BranchNo = new SqlParameter("BranchNo", RequestItem?.BilledBranchNo);
                    var _MobileNo = new SqlParameter("MobileNo", RequestItem?.MobileNo);
                    var _PatientName = new SqlParameter("PatientName", RequestItem?.PatientName);
                    var _AppointmentNo = new SqlParameter("AppointmentNo", RequestItem?.AppointmentNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);
                    var _loginType = new SqlParameter("loginType", RequestItem?.loginType);
                    var _PhysicianName = new SqlParameter("PhysicianName", RequestItem.PhysicianName == null ? "" : RequestItem.PhysicianName);
                    var _SpecializationName = new SqlParameter("SpecializationName", RequestItem?.SpecializationName == null ? "" : RequestItem?.SpecializationName);

                    result = context.OPDPatientDTOList.FromSqlRaw(
                    "Execute dbo.Pro_GetOPDTransaction @FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @SpecializationNo, @PhysicianNo, @AppointmentStatus, @PageIndex, @UserNo," +
                    "@AppointmentCategory, @AppointmentMode, @BranchNo, @MobileNo, @PatientName, @AppointmentNo, @PatientNo, @loginType, @PhysicianName, @SpecializationName",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _SpecializationNo, _PhysicianNo, _AppointmentStatus, _pageIndex, _userNo,
                    _AppointmentCategory, _AppointmentMode, _BranchNo, _MobileNo, _PatientName, _AppointmentNo, _PatientNo, _loginType, _PhysicianName, _SpecializationName).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDPatientList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return result;
        }

        public List<OPDPatientDoctorDTOList> GetOPDDoctorPatientList(CommonFilterRequestDTO RequestItem)
        {
            List<OPDPatientDoctorDTOList> result = new List<OPDPatientDoctorDTOList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", RequestItem?.departmentNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.physicianNo);
                    var _AppointmentStatus = new SqlParameter("AppointmentStatus", RequestItem?.orderStatus);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem?.userNo);
                    var _BranchNo = new SqlParameter("BranchNo", RequestItem.BilledBranchNo);

                    result = context.OPDPatientDoctorDTOList.FromSqlRaw(
                    "Execute dbo.Pro_GetOPDDoctorTransaction @FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @SpecializationNo, @PhysicianNo, @AppointmentStatus, @PageIndex, @UserNo, @BranchNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _SpecializationNo, _PhysicianNo, _AppointmentStatus, _pageIndex, _userNo, _BranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDDoctorPatientList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.userNo);
            }
            return result;
        }
        public List<OPDPatientBookingList> GetPatientBookingList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDPatientBookingList> result = new List<OPDPatientBookingList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AppointmentDate = new SqlParameter("AppointmentDate", RequestItem?.AppointmentDate);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.PhysicianNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _PhysicianVenueBranchNo = new SqlParameter("PhysicianVenueBranchNo", RequestItem.PhysicianVenueBranchNo);
                    var _BookingType = new SqlParameter("BookingType", RequestItem.BookingType);

                    result = context.OPDPatientBookingList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDBookingdata @AppointmentDate, @PhysicianNo, @VenueNo, @VenueBranchNo, @PhysicianVenueBranchNo, @BookingType",
                    _AppointmentDate, _PhysicianNo, _VenueNo, _VenueBranchNo, _PhysicianVenueBranchNo, _BookingType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientBookingList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, 0);
            }
            return result;
        }
        public List<SearchOPDPatient> GetPatientData(SearchOPDPatientRequest RequestItem)
        {
            List<SearchOPDPatient> result = new List<SearchOPDPatient>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchkey = new SqlParameter("Searchkey", RequestItem.Searchkey);
                    var _Searchvalue = new SqlParameter("Searchvalue", RequestItem.Searchvalue);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _Userno = new SqlParameter("Userno", RequestItem.Userno != null ? RequestItem.Userno:0);

                    result = context.SearchOPDPatient.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientdata @Searchkey, @Searchvalue, @VenueNo, @VenueBranchNo,@userno",
                    _Searchkey, _Searchvalue, _VenueNo, _VenueBranchNo,_Userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }

        public List<OPDPatientVitalList> GetPatientVitalData(SearchOPDPatientVitalRequest RequestItem)
        {
            List<OPDPatientVitalList> result = new List<OPDPatientVitalList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.OPDPatientVitalList.FromSqlRaw(
                    "Execute dbo.pro_GetVitalPatientData @PatientNo, @VenueNo, @VenueBranchNo",
                    _PatientNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientVitalData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public OPDPatientOPDData GetPatientOPDData(SearchOPDPatientDataRequest RequestItem)
        {
            OPDPatientOPDData result = new OPDPatientOPDData();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchvalue = new SqlParameter("OPDAppointmentNo", RequestItem.Searchvalue);
                    var _Searchkey = new SqlParameter("PhysicianNo", RequestItem.Searchkey);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.OPDPatientOPDDataList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientSOAP @OPDAppointmentNo, @VenueNo, @VenueBranchNo, @PhysicianNo",
                    _Searchvalue, _VenueNo, _VenueBranchNo, _Searchkey).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientOPDData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDOPDPatientHistory> OpDPatientHistory(SearchOPDPatientRequest RequestItem)
        {
            List<OPDOPDPatientHistory> result = new List<OPDOPDPatientHistory>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchvalue = new SqlParameter("PatientNo", RequestItem.Searchvalue);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.OPDOPDPatientHistory.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientHistory @PatientNo, @VenueNo, @VenueBranchNo",
                    _Searchvalue, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.OpDPatientHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }

        public List<OPDPatientOPDDrugData> GetPatientOPDDrugData(SearchOPDPatientDataRequest RequestItem)
        {
            List<OPDPatientOPDDrugData> result = new List<OPDPatientOPDDrugData>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchvalue = new SqlParameter("OPDAppointmentNo", RequestItem.Searchvalue);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.OPDPatientOPDDrugDataList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientDrugsData @OPDAppointmentNo, @VenueNo, @VenueBranchNo",
                    _Searchvalue, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientOPDDrugData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDPatientOPDTestData> GetPatientOPDTestData(SearchOPDPatientDataRequest RequestItem)
        {
            List<OPDPatientOPDTestData> result = new List<OPDPatientOPDTestData>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchvalue = new SqlParameter("OPDAppointmentNo", RequestItem.Searchvalue);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.OPDPatientOPDTestDataList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientTestData @OPDAppointmentNo, @VenueNo, @VenueBranchNo",
                    _Searchvalue, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientOPDTestData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDPatientMedicineList> GetOPDMedicineData(int VenueNo, int VenueBranchNo, int doctorNo)
        {
            List<OPDPatientMedicineList> result = new List<OPDPatientMedicineList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchvalue = new SqlParameter("DoctorNo", doctorNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    result = context.OPDPatientMedicineList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDMedicineData @DoctorNo, @VenueNo, @VenueBranchNo",
                    _Searchvalue, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDMedicineData", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        public List<ServiceSearchDTO> GetOPDService(int VenueNo, int VenueBranchNo, int doctorNo, int type)
        {
            List<ServiceSearchDTO> objresult = new List<ServiceSearchDTO>();
            try
            {
                string _CacheKey = CacheKeys.ServiceList + VenueNo;// + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<ServiceSearchDTO>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _doctorNo = new SqlParameter("doctorNo", doctorNo);
                        var _TestType = new SqlParameter("TestType", type);
                        var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                        objresult = context.ServiceSearchDTO.FromSqlRaw(
                        "Execute dbo.pro_OPDSearchService @doctorNo,@TestType,@VenueNo,@VenueBranchNo",
                        _doctorNo, _TestType, _VenueNo, _VenueBranchNo).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDService", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public OPDDiagnosisDTOResponse InsertPhysicianDiagnosis(OPDDiagnosisDTORequest objDTO)
        {
            OPDDiagnosisDTOResponse result = new OPDDiagnosisDTOResponse();
            try
            {
                XDocument ServiceXML = new XDocument(new XElement("Testxml", from Item in objDTO.TestList
                                                                             select
                  new XElement("TestList",
                  new XElement("ServiceNo", Item.TestNo),
                  new XElement("ServiceType", Item.TestType),
                  new XElement("IsRadiology", Item.IsRadiology),
                  new XElement("TestDate", Item.TestDate),
                  new XElement("Remarks", Item.Remarks)
                  )));

                XDocument DrugsXML = new XDocument(new XElement("Drugsxml", from Item in objDTO.DrugsList
                                                                            select
                 new XElement("DrugsList",
                 new XElement("ProductMasterNo", Item.ProductMasterNo),
                 new XElement("FromDate", Item.FromDate),
                 new XElement("ToDate", Item.ToDate),
                 new XElement("FrequencyNo", Item.FrequencyNo),
                 new XElement("MedicineintakeNo", Item.MedicineintakeNo),
                 new XElement("RootNo", Item.RootNo),
                 new XElement("DosageNo", Item.DosageNo),
                 new XElement("Remarks", Item.Remarks),
                 new XElement("Instruction", Item.Instruction)
                 )));

                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);
                    var _oPDPatientAppointmentNo = new SqlParameter("oPDPatientAppointmentNo", objDTO.OPDPatientAppointmentNo);
                    var _Subjective = new SqlParameter("Subjective", objDTO.Subjective.ValidateEmpty());
                    var _Objective = new SqlParameter("Objective", objDTO.Objective.ValidateEmpty());
                    var _Assessment = new SqlParameter("Assessment", objDTO.Assessment.ValidateEmpty());
                    var _Plan = new SqlParameter("Plan", objDTO.Plan.ValidateEmpty());
                    var _Drugsxml = new SqlParameter("Drugsxml", DrugsXML.ToString());
                    var _Testxml = new SqlParameter("Testxml", ServiceXML.ToString());
                    var _Imagingxml = new SqlParameter("Imagingxml", "");
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo.ToString());
                    var _TempDiseaseNo = new SqlParameter("TempDiseaseNo", objDTO.TempDiseaseNo);
                    var _TemplateNo = new SqlParameter("TemplateNo", objDTO.TemplateNo);
                    var _TemplateName = new SqlParameter("TemplateName", objDTO.TemplateName != null ? objDTO.TemplateName.ToString() : "");
                    var _TemplateText = new SqlParameter("TemplateText", objDTO.TemplateText.ToString());
                    var _PhysicianNo = new SqlParameter("PhysicianNo", objDTO.PhysicianNo);
                    var _ReferralPhysicianNo = new SqlParameter("ReferralPhysicianNo", objDTO.ReferralPhysicianNo);
                    var _CheifComplaints = new SqlParameter("CheifComplaints", objDTO.CheifComplaints.ValidateEmpty());
                    var _PresentingComplaints = new SqlParameter("PresentingComplaints", objDTO.PresentingComplaints.ValidateEmpty());
                    var _ProvisionalDiagnosis = new SqlParameter("ProvisionalDiagnosis", objDTO.ProvisionalDiagnosis.ValidateEmpty());
                    var _PastHistory = new SqlParameter("PastHistory", objDTO.PastHistory.ValidateEmpty());
                    var _PhysicalExamination = new SqlParameter("PhysicalExamination", objDTO.PhysicalExamination.ValidateEmpty());
                    var _SystemicExamination = new SqlParameter("SystemicExamination", objDTO.SystemicExamination.ValidateEmpty());
                    var _NutritionalSpec = new SqlParameter("NutritionalSpec", objDTO.NutritionalSpec.ValidateEmpty());
                    var _DiagnosisFollowup = new SqlParameter("DiagnosisFollowup", objDTO.DiagnosisFollowup.ValidateEmpty());                    
                    var _followUpCommands = new SqlParameter("followUpCommands", objDTO.followUpCommands != null ? objDTO.followUpCommands.ToString() : "");
                    var _followUpDate = new SqlParameter("followupDate", objDTO.followUpDate != null ? objDTO.followUpDate.ToString() : "");
                    var _OutputTypeNo = new SqlParameter("OutputTypeNo", objDTO.OutputTypeNo);
                    var _GeneralCommands = new SqlParameter("GeneralCommands", objDTO.GeneralCommands != null ? objDTO.GeneralCommands.ToString() : "");
                    var _cheifComplaintslst = new SqlParameter("cheifComplaintslst", objDTO.CheifComplaintslst.ValidateEmpty());

                    result = context.InsertOPDDiagnosis.FromSqlRaw(
                   "Execute dbo.pro_InsertPhysicianDiagnosis " +
                   "@PatientNo, @oPDPatientAppointmentNo, @Subjective, @Objective, @Assessment, @Plan, @Drugsxml, @Testxml, @Imagingxml, @VenueNo, @VenueBranchNo," +
                   "@userNo, @TempDiseaseNo, @TemplateNo, @TemplateName, @TemplateText, @PhysicianNo, @ReferralPhysicianNo, @followUpCommands, @followupDate, @OutputTypeNo," +
                   "@CheifComplaints, @PresentingComplaints, @PastHistory, @PhysicalExamination, @SystemicExamination, @NutritionalSpec, @DiagnosisFollowup, @ProvisionalDiagnosis,@GeneralCommands,@CheifComplaintslst",
                   _PatientNo, _oPDPatientAppointmentNo, _Subjective, _Objective, _Assessment, _Plan, _Drugsxml, _Testxml, _Imagingxml, _VenueNo, _VenueBranchNo, 
                   _UserID, _TempDiseaseNo, _TemplateNo, _TemplateName, _TemplateText, _PhysicianNo, _ReferralPhysicianNo, _followUpCommands, _followUpDate, _OutputTypeNo, 
                   _CheifComplaints, _PresentingComplaints, _PastHistory, _PhysicalExamination, _SystemicExamination, _NutritionalSpec, _DiagnosisFollowup, _ProvisionalDiagnosis, _GeneralCommands, _cheifComplaintslst).AsEnumerable().FirstOrDefault();

                    if (objDTO.TemplateNo > 0 && objDTO.TemplateText != "")
                    {
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("OPDTransTemplateFilePath");

                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                        ? objAppSettingResponse.ConfigValue : "";
                        path = path + objDTO.VenueNo.ToString() + "/" + objDTO.PatientNo + "/" + objDTO.OPDPatientAppointmentNo + "/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string createText = objDTO.TemplateText + Environment.NewLine;
                        File.WriteAllText(path + objDTO.TemplateNo + "_" + objDTO.PatientNo + ".ym", createText);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.InsertPhysicianDiagnosis - PatientNo : " + objDTO.PatientNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        public List<OPDApptDetails> GetOPDApptDetails(OPDApptDetailsreq RequestItem)
        {
            List<OPDApptDetails> result = new List<OPDApptDetails>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _MobileNo = new SqlParameter("MobileNo", RequestItem?.MobileNo);
                    var _PatientName = new SqlParameter("PatientName", RequestItem?.PatientName);
                    var _AppointmentNo = new SqlParameter("AppointmentNo", RequestItem?.AppointmentNo);
                    var _PatientID = new SqlParameter("PatientID", RequestItem?.PatientID);

                    result = context.GetOPDApptDetails.FromSqlRaw(
                    "Execute dbo.Pro_GetApptDetails @FROMDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @PageIndex," +
                    "@UserNo, @MobileNo, @PatientName, @AppointmentNo, @PatientID",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _pageIndex, _userNo, _MobileNo, _PatientName, _AppointmentNo, _PatientID).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDApptDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return result;
        }
        public List<OPDDoctorMainList> GetOPDDoctorList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDDoctorMainList> result = new List<OPDDoctorMainList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AppointmentDate = new SqlParameter("AppointmentDate", RequestItem.AppointmentDate.ValidateEmpty());
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.PhysicianNo);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", RequestItem.SpecializationNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PhysicianVenueBranchNo = new SqlParameter("PhysicianVenueBranchNo", RequestItem.PhysicianVenueBranchNo);

                    var output = context.OPDDoctorList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPhysicianAppointment @AppointmentDate,@PhysicianNo,@SpecializationNo,@VenueNo,@VenueBranchNo,@PhysicianVenueBranchNo",
                    _AppointmentDate, _PhysicianNo, _SpecializationNo, _VenueNo, _VenueBranchNo, _PhysicianVenueBranchNo).ToList();

                    result = GetOPDDoctorResponse(output);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDDoctorList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDDoctorMainList> GetOPDDoctorResponse(List<OPDDoctorList> listDTO)
        {
            List<OPDDoctorMainList> objresult = new List<OPDDoctorMainList>();
            try
            {
                int oldPhysicianNo = 0;
                int newPhysicianNo = 0;
                int OldBranchNo = 0;
                int newBranchNo = 0;
                int OldDayNo = 0;
                int newDayNo = 0;
                foreach (var itemList in listDTO)
                {
                    OPDDoctorMainList Responseitem = new OPDDoctorMainList();
                    List<OPDDoctorMainBranchList> lstBranchDetail = new List<OPDDoctorMainBranchList>();

                    newPhysicianNo = itemList.PhysicianNo;
                    var BranchItem = listDTO.Where(x => x.PhysicianNo == newPhysicianNo).Select(x => new
                    {
                        x.VenueBranchNo,
                        x.VenueBranchName,
                        x.Amount
                    }).Distinct().ToList();
                    if (newPhysicianNo != oldPhysicianNo)
                    {
                        Responseitem.PhysicianNo = itemList.PhysicianNo;
                        Responseitem.PhysicianName = itemList.PhysicianName;
                        Responseitem.Qualification = itemList.Qualification;
                        Responseitem.SpecializationName = itemList.SpecializationName;
                        Responseitem.NoofVisits = itemList.NoofVisits;
                        Responseitem.NoofBooked = itemList.NoofBooked;
                        Responseitem.OpdNotes = itemList.OpdNotes;
                        oldPhysicianNo = itemList.PhysicianNo;
                        OldBranchNo = 0;
                        foreach (var Item in BranchItem)
                        {
                            List<OPDDoctorMainDayList> lstDayDetail = new List<OPDDoctorMainDayList>();
                            newBranchNo = (int)Item.VenueBranchNo;
                            var dayItem = listDTO.Where(x => x.PhysicianNo == newPhysicianNo && x.VenueBranchNo == Item.VenueBranchNo).Select(x => new
                            {
                                x.DayNo,
                                x.DayDate
                            }).ToList();

                            if (OldBranchNo != newBranchNo)
                            {
                                OPDDoctorMainBranchList objbranch = new OPDDoctorMainBranchList();
                                objbranch.VenueBranchNo = Item.VenueBranchNo;
                                objbranch.VenueBranchName = Item.VenueBranchName;
                                objbranch.Amount = Item.Amount;
                                OldBranchNo = Item.VenueBranchNo;
                                OldDayNo = 0;
                                foreach (var subItem in dayItem)
                                {
                                    newDayNo = (int)subItem.DayNo;
                                    if (OldDayNo != newDayNo)
                                    {
                                        OPDDoctorMainDayList objDay = new OPDDoctorMainDayList()
                                        {
                                            DayNo = subItem.DayNo,
                                            DayName = CommonHelper.GetEnumValue<Weekdays>(subItem.DayNo).ToString(),
                                            DayDate = subItem.DayDate
                                        };
                                        OldDayNo = newDayNo;
                                        lstDayDetail.Add(objDay);
                                    }
                                    objbranch.DayList = lstDayDetail;
                                }
                                lstBranchDetail.Add(objbranch);
                            }
                            Responseitem.BranchList = lstBranchDetail;

                        }
                        objresult.Add(Responseitem);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDDoctorResponse", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }

        public int GetOPDPhysicianAmount(OPDPatientOfficeDTO RequestItem)
        {
            int result = 0;
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.PhysicianNo);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", RequestItem?.SpecializationNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _IsNew = new SqlParameter("IsNew", RequestItem?.IsNew);
                    var _IsVIP = new SqlParameter("IsVIP", RequestItem?.IsVIP);

                    var obj = context.GetOPDPhysicianAmount.FromSqlRaw(
                    "Execute dbo.Pro_GetOPDPhysicianAmount @PhysicianNo,@SpecializationNo,@VenueNo,@VenueBranchNo,@IsNew,@IsVIP",
                    _PhysicianNo, _SpecializationNo, _VenueNo, _VenueBranchNo, _IsNew, _IsVIP).AsEnumerable().FirstOrDefault();

                    result = (int)obj?.Amount;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDPhysicianAmount", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }

        public List<Humanbodyparts> Gethumanbodyparts(int VenueNo, int VenueBranchNo, int type)
        {
            List<Humanbodyparts> objresult = new List<Humanbodyparts>();
            try
            {
                string _CacheKey = CacheKeys.ServiceList + VenueNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<Humanbodyparts>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _Type = new SqlParameter("Type", type);
                        var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                        objresult = context.Humanbodyparts.FromSqlRaw(
                        "Execute dbo.pro_humanbodyparts @Type, @VenueNo, @VenueBranchNo",
                        _Type, _VenueNo, _VenueBranchNo).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.Gethumanbodyparts", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<OPDPatientDisVsInvDetails> GetOPDPatientMasterDefinedInvDetails(string type, int PatientNo, int VenueNo, int VenueBranchNo)
        {
            List<OPDPatientDisVsInvDetails> result = new List<OPDPatientDisVsInvDetails>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", type);
                    var _PatientNo = new SqlParameter("PatientNo", PatientNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    //OPDPatientOPDImageList
                    result = context.GetOPDMasterDefinedInvDetails.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientMasterDefinedInvDetails @Type,@PatientNo,@VenueNo,@VenueBranchNo",
                    _type, _PatientNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDPatientMasterDefinedInvDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        public List<OPDBeforeAfterImageList> GetOPDBeforeANDAfterImageList(int VenueNo, int VenueBranchNo, int OPDPatientNo, int PatientNo, int VisitNo)
        {
            List<OPDBeforeAfterImageList> result = new List<OPDBeforeAfterImageList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDPatientNo = new SqlParameter("OPDPatientNo", OPDPatientNo);
                    var _PatientNo = new SqlParameter("PatientNo", PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", VisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    //OPDPatientOPDImageList
                    result = context.OPDBeforeAfterImageList.FromSqlRaw(
                    "Execute dbo.pro_GetOPDBeforeAfterImage @OPDPatientNo, @PatientNo, @VisitNo, @VenueNo, @VenueBranchNo",
                    _OPDPatientNo, _PatientNo, _VisitNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDBeforeANDAfterImageList", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        public OPDTreatmentPlan GetOPDTreatmentPlanDetails(OPDTreatmentPlan req)
        {
            OPDTreatmentPlan objresult = new OPDTreatmentPlan();
            List<OPDTreatmentPlanProcedures> resultPRO = new List<OPDTreatmentPlanProcedures>();
            List<OPDTreatmentPlanPharmacy> resultPRM = new List<OPDTreatmentPlanPharmacy>();
            List<OPDTreatmentPlanRes> resultplan = new List<OPDTreatmentPlanRes>();

            try
            {
                var _PatientNo = new SqlParameter("PatientNo", req?.patientNo);
                var _AppointmentNo = new SqlParameter("AppointmentNo", req?.appointmentNo);
                var _VenueNo = new SqlParameter("VenueNo", req?.venueNo);
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.venueBranchNo);

                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    resultplan = context.GetOPDTreatmentPlan.FromSqlRaw(
                    "Execute dbo.pro_GetOPDTreatmentPlan @PatientNo,@AppointmentNo, @VenueNo, @VenueBranchNo",
                    _PatientNo, _AppointmentNo, _VenueNo, _VenueBranchNo).ToList();
                }

                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    resultPRO = context.GetOPDTreatmentPlanPRO.FromSqlRaw(
                    "Execute dbo.pro_GetOPDTreatmentPlanPRO @PatientNo,@AppointmentNo, @VenueNo, @VenueBranchNo",
                    _PatientNo, _AppointmentNo, _VenueNo, _VenueBranchNo).ToList();
                }
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    resultPRM = context.GetOPDTreatmentPlanPRM.FromSqlRaw(
                    "Execute dbo.pro_GetOPDTreatmentPlanPRM @PatientNo,@AppointmentNo, @VenueNo, @VenueBranchNo",
                    _PatientNo, _AppointmentNo, _VenueNo, _VenueBranchNo).ToList();
                }

                if (resultplan.Count > 0)
                {
                    objresult.appointmentNo = resultplan[0].appointmentNo;
                    objresult.oPDTreatmentNo = resultplan[0].oPDTreatmentNo;
                    objresult.patientNo = resultplan[0].patientNo;
                    objresult.nextAppointmentDate = resultplan[0].nextAppointmentDate;
                }
                objresult.totalAmount = req.totalAmount;
                objresult.venueNo = req.venueNo;
                objresult.venueBranchNo = req.venueBranchNo;
                objresult.lstpharmacy = resultPRM;
                objresult.lstProcedures = resultPRO;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetTreatmentMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return objresult;
        }
        public List<OPDPatientDisVsDrugDetails> GetOPDPatientMasterDefinedDrugDetails(string type, int PatientNo, int VenueNo, int VenueBranchNo)
        {
            List<OPDPatientDisVsDrugDetails> result = new List<OPDPatientDisVsDrugDetails>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", type);
                    var _PatientNo = new SqlParameter("PatientNo", PatientNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    //OPDPatientOPDImageList
                    result = context.GetOPDMasterDefinedDrugDetails.FromSqlRaw(
                    "Execute dbo.pro_GetOPDPatientMasterDefinedDrugDetails @Type,@PatientNo,@VenueNo,@VenueBranchNo",
                    _type, _PatientNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDPatientMasterDefinedDrugDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        public List<OPDBulkFileUpload> GetPatientDocumentDetails(PatientDocUploadReq obj)
        {
            List<OPDBulkFileUpload> lstresult = new List<OPDBulkFileUpload>();
            OPDBulkFileUpload result = new OPDBulkFileUpload();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);

            string AppUploadPathInit = "UploadPathInit";
            string FilePath = string.Empty;
            try
            {
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                var visitId = obj.patientNumber;
                var visitno = obj.ApptNo;
                var venueno = obj.venueNo;
                var venuebNo = obj.venueBranchNo;
                string folderName = venueno + "\\" + venuebNo + "\\" + visitId + "\\" + visitno + "\\" + obj.docType;
                string newPath = Path.Combine(uplodpathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new OPDBulkFileUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            result.actualFileName = filePaths[f].ToString().Split("$$")[3];
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[5];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientDocumentDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lstresult;
        }

        public List<DocumentInfo> GetPatientDocumentAll(PatientDocUploadReq obj)
        {
            List<DocumentInfo> lstresult = new List<DocumentInfo>();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);

            string AppUploadPathInit = "UploadPathInit";
            string FilePath = string.Empty;
            try
            {
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                var visitId = obj.patientNumber;
                var venueno = obj.venueNo;
                var venuebNo = obj.venueBranchNo;
                string folderName = venueno + "\\" + venuebNo + "\\" + visitId;
                string newPath = Path.Combine(uplodpathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] files = Directory.GetFiles(newPath, "*.*", SearchOption.AllDirectories);
                    if (files != null && files.Length > 0)
                    {
                        foreach (string filePath in files)
                        {
                            string[] parts = filePath.Split(Path.DirectorySeparatorChar);

                            if (parts.Length >= 8)
                            {
                                string appointmentId = parts[parts.Length - 3]; 
                                if (string.IsNullOrEmpty(appointmentId))
                                {
                                     appointmentId = "NONE";
                                }
                                string fileName = Path.GetFileName(filePath);
                                DateTime createdDate = File.GetCreationTime(filePath);
                                string fileExtension = Path.GetExtension(filePath).TrimStart('.').ToUpper();
                                Byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                                String base64String = Convert.ToBase64String(bytes);
                                String ActualBinaryData = base64String;

                                lstresult.Add(new DocumentInfo
                                {
                                    AppointmentId = appointmentId,
                                    DocumentType = filePath,
                                    FileName = fileName,
                                    CreatedDate = createdDate.ToString("dd-MM-yyy HH:mm"),
                                    FileExtension = fileExtension,
                                    BinaryData = ActualBinaryData
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetPatientDocumentAll", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lstresult;
        }

        public List<drugresponse> GetDrugDetails(drugreq RequestItem)
        {
            List<drugresponse> result = new List<drugresponse>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", RequestItem.venueNo);
                    var _productNo = new SqlParameter("productNo", RequestItem.productNo);

                    result = context.GetDrugDetails.FromSqlRaw(
                    "Execute dbo.pro_GetDrugDetails @venueNo, @productNo", _venueNo, _productNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDrugDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.venueNo, 0, 0);
            }
            return result;
        }

        public List<ClinicalHistory> GetSkinHistory(SkinHistoryReq req)
        {
            List<ClinicalHistory> lstskinHistories = new List<ClinicalHistory>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", req?.PatientVisitNo);
                    var _ApptNo = new SqlParameter("ApptNo", req?.ApptNo);

                    var skinHistories = context.GetClinicalHistories.FromSqlRaw(
                    "Execute dbo.Pro_GetSkinHistory @VenueNo, @VenueBranchNo, @PatientVisitNo, @ApptNo",
                    _VenueNo, _VenueBranchNo, _PatientVisitNo, _ApptNo).ToList();

                    if (skinHistories != null || skinHistories.Any())
                    {
                        lstskinHistories = skinHistories?.GroupBy(ch => new { ch.GroupNo, ch.GroupName })
                         .Select(clinicalHistory => new ClinicalHistory
                         {
                             GroupNo = clinicalHistory.Key.GroupNo,
                             GroupName = clinicalHistory.Key.GroupName,
                             ClinicalHistoryMasters = clinicalHistory.Select(ch => new ClinicalHistoryMaster
                             {
                                 MasterNo = ch.MasterNo,
                                 MasterName = ch.MasterName,
                                 ControlType = ch.ControlType,
                                 MasterValue = ch.MasterValue
                             }).ToList()

                         }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetSkinHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lstskinHistories;
        }
        public List<ClinicalHistory> GetopdclinicalHistory(SkinHistoryReq req)
        {
            List<ClinicalHistory> lstskinHistories = new List<ClinicalHistory>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", req?.PatientVisitNo);
                    var _ApptNo = new SqlParameter("ApptNo", req?.ApptNo);

                    var skinHistories = context.GetClinicalHistories.FromSqlRaw(
                    "Execute dbo.Pro_GetopdclinicalHistory @VenueNo,@VenueBranchNo,@PatientVisitNo,@ApptNo",
                    _VenueNo, _VenueBranchNo, _PatientVisitNo, _ApptNo).ToList();

                    if (skinHistories != null || skinHistories.Any())
                    {
                        lstskinHistories = skinHistories?.GroupBy(ch => new { ch.GroupNo, ch.GroupName })
                         .Select(clinicalHistory => new ClinicalHistory
                         {
                             GroupNo = clinicalHistory.Key.GroupNo,
                             GroupName = clinicalHistory.Key.GroupName,
                             ClinicalHistoryMasters = clinicalHistory.Select(ch => new ClinicalHistoryMaster
                             {
                                 MasterNo = ch.MasterNo,
                                 MasterName = ch.MasterName,
                                 ControlType = ch.ControlType,
                                 MasterValue = ch.MasterValue
                             }).ToList()

                         }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetopdclinicalHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lstskinHistories;
        }
        public OPDDiagnosisDTOFollowupResponse InsertFollowUpAppointment(OPDDiagnosisDTORequest objDTO)
        {
            OPDDiagnosisDTOFollowupResponse result = new OPDDiagnosisDTOFollowupResponse();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNo = new SqlParameter("PatientNo", objDTO.PatientNo);
                    var _oPDPatientAppointmentNo = new SqlParameter("oPDPatientAppointmentNo", objDTO.OPDPatientAppointmentNo);
                    var _followUpCommands = new SqlParameter("followUpCommands", objDTO.followUpCommands);
                    var _followUpDate = new SqlParameter("followupDate", objDTO.followUpDate);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO.UserNo);
                    var _FollowOPDNo = new SqlParameter("FollowOPDNo", objDTO.FollowOPDNo);

                    var obj = context.InsertFollowUpAppointment.FromSqlRaw(
                   "Execute dbo.pro_InsertFollowUpAppointment @PatientNo, @oPDPatientAppointmentNo, @followUpCommands, @followupDate, @VenueNo, @VenueBranchNo, @userNo, @FollowOPDNo",
                   _PatientNo, _oPDPatientAppointmentNo, _followUpCommands, _followUpDate, _VenueNo, _VenueBranchNo, _UserID, _FollowOPDNo).AsEnumerable().FirstOrDefault();
                    
                    result.AppointmentNo = obj.AppointmentNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.InsertFollowUpAppointment - " + objDTO.PatientNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
        public CommonAdminResponse InsertopdclinicHistory(InsertSkinHistory insertSkinHistory)
        {
            CommonAdminResponse commonAdminResponse = new CommonAdminResponse();

            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string ClinicalHistoryXML = commonUtility.ToXML(insertSkinHistory.ClinicalHistories);

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClinicalHistoryXML = new SqlParameter("ClinicalHistoryXML", ClinicalHistoryXML);
                    var _ApptNo = new SqlParameter("ApptNo", insertSkinHistory?.ApptNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", insertSkinHistory?.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", insertSkinHistory?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertSkinHistory?.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", insertSkinHistory?.UserNo);

                    commonAdminResponse = context.InsertClinicalHistories.FromSqlRaw(
                    "Execute dbo.Pro_InsertopdclinicHistory @ClinicalHistoryXML, @ApptNo, @PatientVisitNo, @VenueNo, @VenueBranchNo, @UserNo",
                   _ClinicalHistoryXML, _ApptNo, _PatientVisitNo, _VenueNo, _VenueBranchNo, _UserNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.InsertopdclinicHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertSkinHistory.VenueNo, insertSkinHistory.VenueBranchNo, insertSkinHistory.PatientVisitNo);
            }
            return commonAdminResponse;
        }
        public CommonAdminResponse InsertSkinHistory(InsertSkinHistory insertSkinHistory)
        {
            CommonAdminResponse commonAdminResponse = new CommonAdminResponse();

            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string skinHistoryXML = commonUtility.ToXML(insertSkinHistory.ClinicalHistories);

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SkinHistoryXML = new SqlParameter("SkinHistoryXML", skinHistoryXML);
                    var _ApptNo = new SqlParameter("ApptNo", insertSkinHistory?.ApptNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", insertSkinHistory?.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", insertSkinHistory?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertSkinHistory?.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", insertSkinHistory?.UserNo);

                    commonAdminResponse = context.InsertClinicalHistories.FromSqlRaw(
                    "Execute dbo.Pro_InsertSkinHistory @SkinHistoryXML, @ApptNo, @PatientVisitNo, @VenueNo, @VenueBranchNo, @UserNo",
                   _SkinHistoryXML, _ApptNo, _PatientVisitNo, _VenueNo, _VenueBranchNo, _UserNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.InsertSkinHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertSkinHistory.VenueNo, insertSkinHistory.VenueBranchNo, insertSkinHistory.UserNo);
            }
            return commonAdminResponse;
        }

        public OPDBeforeAfterImageList OPDImagingFile(OPDBeforeAfterImageList objDTO)
        {
            OPDBeforeAfterImageList result = new OPDBeforeAfterImageList();
            try
            {
                if (objDTO.b_Type == "Before" && objDTO.imagingNo == 0)
                {
                    var base64data = objDTO.b_Src;
                    base64data = base64data.Replace("data:image/png;base64,", string.Empty);
                    var patientNo = objDTO.patientNo;
                    var appointmentNo = objDTO.appointmentNo;
                    var venueNo = objDTO.venueNo;
                    var venuebNo = objDTO.venueBranchNo;
                    var format = objDTO.b_FileType;
                    
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadOPDImaging");
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    path = path + "\\" + objDTO.b_Type.ToString() + "\\" + objDTO.venueNo.ToString() + "\\" + objDTO.patientNo.ToString();

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path); //OPDBeforeAfterImageListResponse
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = objDTO.b_FileName;
                        string fullPath = Path.Combine(path, fileName);
                        objDTO.b_PathName = fullPath;
                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                    result.b_Src = objDTO.b_Src;
                    result.b_FileType = objDTO.b_FileType;
                    result.b_PathName = objDTO.b_PathName;
                    result.b_Type = objDTO.b_Type;
                    result.b_FileName = objDTO.b_FileName;

                    result.a_Src = objDTO.a_Src;
                    result.a_FileType = objDTO.a_FileType;
                    result.a_PathName = objDTO.a_PathName;
                    result.a_Type = objDTO.a_Type;
                    result.a_FileName = objDTO.a_FileName;

                    result.imagingNo = objDTO.imagingNo;
                    result.patientNo = objDTO.patientNo;
                    result.appointmentNo = objDTO.appointmentNo;
                    result.Status = objDTO.Status;
                    result.venueNo = objDTO.venueNo;
                    result.venueBranchNo = objDTO.venueBranchNo;
                    result.userNo = objDTO.userNo;
                }
                else if (objDTO.a_Type == "After" && objDTO.imagingNo > 0)
                {
                    var base64data = objDTO.a_Src;
                    base64data = base64data.Replace("data:image/png;base64,", string.Empty);
                    var patientNo = objDTO.patientNo;
                    var appointmentNo = objDTO.appointmentNo;
                    var venueNo = objDTO.venueNo;
                    var venuebNo = objDTO.venueBranchNo;
                    var format = objDTO.b_FileType;
                    
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadOPDImaging");
                    string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    path = path + "\\" + objDTO.a_Type.ToString() + "\\" + objDTO.venueNo.ToString() + "\\" + objDTO.patientNo.ToString();

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path); //OPDBeforeAfterImageListResponse
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = objDTO.a_FileName;
                        string fullPath = Path.Combine(path, fileName);
                        objDTO.a_PathName = fullPath;
                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                    result.b_Src = objDTO.b_Src;
                    result.b_FileType = objDTO.b_FileType;
                    result.b_PathName = objDTO.b_PathName;
                    result.b_Type = objDTO.b_Type;
                    result.b_FileName = objDTO.b_FileName;

                    result.a_Src = objDTO.a_Src;
                    result.a_FileType = objDTO.a_FileType;
                    result.a_PathName = objDTO.a_PathName;
                    result.a_Type = objDTO.a_Type;
                    result.a_FileName = objDTO.a_FileName;

                    result.imagingNo = objDTO.imagingNo;
                    result.patientNo = objDTO.patientNo;
                    result.appointmentNo = objDTO.appointmentNo;
                    result.Status = objDTO.Status;
                    result.venueNo = objDTO.venueNo;
                    result.venueBranchNo = objDTO.venueBranchNo;
                    result.userNo = objDTO.userNo;
                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.OPDImagingFile", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objDTO.venueNo, (int)objDTO.venueBranchNo, (int)objDTO.userNo);
            }
            return result;
        }

        public OPDBeforeAfterImageListResponse InserOPDImaging(OPDBeforeAfterImageList objImageList)
        {
            OPDBeforeAfterImageListResponse result = new OPDBeforeAfterImageListResponse();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _imagingNo = new SqlParameter("imagingNo", objImageList?.imagingNo);
                    var _physicianDiagnosisNo = new SqlParameter("physicianDiagnosisNo", objImageList?.physicianDiagnosisNo);
                    var _AppointmentNo = new SqlParameter("appointmentNo", objImageList?.appointmentNo);
                    var _PatientNo = new SqlParameter("patientNo", objImageList?.patientNo);
                    var _VenueNo = new SqlParameter("venueNo", objImageList?.venueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", objImageList?.venueBranchNo);
                    var _userNo = new SqlParameter("userNo", objImageList?.userNo);
                    var _Status = new SqlParameter("status", objImageList?.Status);

                    var B_Type = new SqlParameter("b_type", objImageList?.b_Type);
                    var B_FileType = new SqlParameter("b_fileType", objImageList?.b_FileType);
                    var B_FileName = new SqlParameter("b_fileName", objImageList?.b_FileName);
                    var B_PathName = new SqlParameter("b_pathName", objImageList?.b_PathName);

                    var A_Type = new SqlParameter("a_type", objImageList?.a_Type);
                    var A_FileType = new SqlParameter("a_fileType", objImageList?.a_FileType);
                    var A_FileName = new SqlParameter("a_fileName", objImageList?.a_FileName);
                    var A_PathName = new SqlParameter("a_pathName", objImageList?.a_PathName);
                    var Includingreport = new SqlParameter("includingreport", objImageList?.Includingreport);

                    var obj = context.InserOPDPatientImaging.FromSqlRaw(
                   "Execute dbo.pro_InserOPDPatientImaging @imagingNo, @physicianDiagnosisNo,@appointmentNo,@patientNo,@b_type,@b_fileType,@b_fileName,@b_pathName,@a_type,@a_fileType,@a_fileName,@a_pathName,@venueNo,@venueBranchNo,@userNo,@status,@includingreport",
                   _imagingNo, _physicianDiagnosisNo, _AppointmentNo, _PatientNo, B_Type, B_FileType, B_FileName, B_PathName, A_Type, A_FileType, A_FileName, A_PathName, _VenueNo, _VenueBranchNo, _userNo, _Status, Includingreport).ToList();
                    result.ResultNo = obj[0].ResultNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.OPDImagingFile", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objImageList.venueNo, (int)objImageList.venueBranchNo, (int)objImageList.userNo);
            }
            return result;
        }

        public TreatmentPlanResponse InsertTreatmentPlan(OPDTreatmentPlan req)
        {
            TreatmentPlanResponse objresult = new TreatmentPlanResponse();
            try
            {
                XDocument TreatmentProXML = new XDocument(new XElement("TreatmentProxml", from Item in req.lstProcedures
                                                                                          select
                                                                                          new XElement("TreatmentProList",
                                                                                          new XElement("oPDTreatmentPlanProceduresNo", Item.oPDTreatmentPlanProceduresNo),
                                                                                          new XElement("oPDTreatmentNo", Item.oPDTreatmentNo),
                                                                                          new XElement("testNo", Item.testNo),
                                                                                          new XElement("testName", Item.testName),
                                                                                          new XElement("scheduleEveryNo", Item.scheduleEveryNo),
                                                                                          new XElement("frequencyNo", Item.frequencyNo),
                                                                                          new XElement("daySunday", Item.daySunday),
                                                                                          new XElement("dayMonday", Item.dayMonday),
                                                                                          new XElement("dayTuesday", Item.dayTuesday),
                                                                                          new XElement("dayWednesday", Item.dayWednesday),
                                                                                          new XElement("dayThursday", Item.dayThursday),
                                                                                          new XElement("dayFriday", Item.dayFriday),
                                                                                          new XElement("daySaturday", Item.daySaturday),
                                                                                          new XElement("totalTreatments", Item.totalTreatments),
                                                                                          new XElement("performPhysicianNo", Item.performPhysicianNo),
                                                                                          new XElement("performPhysicianName", Item.performPhysicianName),
                                                                                          new XElement("nextAppointmentDate", Item.nextAppointmentDate),
                                                                                          new XElement("specializationNo", Item.specializationNo),
                                                                                          new XElement("rate", Item.rate)
                                                                                          )));

                XDocument TreatmentPrmXML = new XDocument(new XElement("TreatmentPrmXML", from Item in req.lstpharmacy
                                                                                          select
                                                                                          new XElement("TreatmentPrmList",
                                                                                          new XElement("oPDTreatmentPlanPharmacyNo", Item.oPDTreatmentPlanPharmacyNo),
                                                                                          new XElement("oPDTreatmentNo", Item.oPDTreatmentNo),
                                                                                           new XElement("productMasterNo", Item.productMasterNo),
                                                                                           new XElement("productMasterName", Item.productMasterName),
                                                                                           new XElement("daily", Item.daily),
                                                                                           new XElement("am", Item.am),
                                                                                           new XElement("pm", Item.pm),
                                                                                           new XElement("weekly", Item.weekly),
                                                                                           new XElement("asNeeded", Item.asNeeded)
                                                                                          )));
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", (req?.oPDTreatmentNo == 0 ? "INSERT" : "UPDATE"));
                    var _OPDTreatmentNo = new SqlParameter("OPDTreatmentNo", req?.oPDTreatmentNo);
                    var _AppointmentNo = new SqlParameter("AppointmentNo", req?.appointmentNo);
                    var _PatientNo = new SqlParameter("PatientNo", req?.patientNo);
                    var _NextAppointMentDate = new SqlParameter("NextAppointMentDate", req?.nextAppointmentDate == null ? "" : req?.nextAppointmentDate);
                    var _TotalAmount = new SqlParameter("TotalAmount", req?.totalAmount);
                    var _TreatmentProxml = new SqlParameter("TreatmentProxml", TreatmentProXML.ToString());
                    var _TreatmentPrmxml = new SqlParameter("TreatmentPrmxml", TreatmentPrmXML.ToString());
                    var _UserNo = new SqlParameter("UserNo", req?.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.venueBranchNo);

                    var dbResponse = context.OPDInsertTreatmentplan.FromSqlRaw(
                    "Execute dbo.Pro_OPDInsertTreatmentPlan @Type, @OPDTreatmentNo,@AppointmentNo,@PatientNo,@NextAppointMentDate,@TotalAmount,@TreatmentProxml,@TreatmentPrmxml,@VenueNo,@VenueBranchNo,@UserNo",
                    _Type, _OPDTreatmentNo, _AppointmentNo, _PatientNo, _NextAppointMentDate, _TotalAmount, _TreatmentProxml, _TreatmentPrmxml, _VenueNo, _VenueBranchNo, _UserNo).AsEnumerable().FirstOrDefault();

                    objresult.oPDTreatmentNo = dbResponse.oPDTreatmentNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertTreatmentPlan", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return objresult;
        }

        public ImageListResponse OPDImagingIncludingreport(OPDBeforeAfterImageList objImageList)
        {
            ImageListResponse result = new ImageListResponse();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _imagingNo = new SqlParameter("imagingNo", objImageList?.imagingNo);
                    var _physicianDiagnosisNo = new SqlParameter("physicianDiagnosisNo", objImageList?.physicianDiagnosisNo);
                    var _AppointmentNo = new SqlParameter("appointmentNo", objImageList?.appointmentNo);
                    var _PatientNo = new SqlParameter("patientNo", objImageList?.patientNo);
                    var _VenueNo = new SqlParameter("venueNo", objImageList?.venueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", objImageList?.venueBranchNo);
                    var _userNo = new SqlParameter("userNo", objImageList?.userNo);
                    var _Status = new SqlParameter("status", objImageList?.Status);
                    var Includingreport = new SqlParameter("includingreport", objImageList?.Includingreport);

                    var obj = context.OPDImagingIncludingreport.FromSqlRaw(
                   "Execute dbo.pro_UpdateOPDPatientImagingIncludingreport @imagingNo, @physicianDiagnosisNo,@appointmentNo,@patientNo,@venueNo,@venueBranchNo,@userNo,@status,@includingreport",
                   _imagingNo, _physicianDiagnosisNo, _AppointmentNo, _PatientNo, _VenueNo, _VenueBranchNo, _userNo, _Status, Includingreport).ToList();
                    result.ResultNo = obj[0].ResultNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InserOPDImagingIncludingreport", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objImageList.venueNo, (int)objImageList.venueBranchNo, (int)objImageList.userNo);
            }
            return result;
        }

        public List<OPDStatusLogListResponse> GetOPDStatusLogList(OPDStatusLogListRequest RequestItem)
        {
            List<OPDStatusLogListResponse> result = new List<OPDStatusLogListResponse>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _AppointmentNo = new SqlParameter("AppointmentNo", RequestItem?.AppointmentNo);
                    var _OPDPatientAppointmentNo = new SqlParameter("OPDPatientAppointmentNo", RequestItem?.OPDPatientAppointmentNo);
                    var _Userno = new SqlParameter("Userno", RequestItem?.Userno);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem?.PageIndex);                                        
                    var _specialisationNo = new SqlParameter("SpecialisationNo", RequestItem?.SpecialisationNo);
                    var _physicianNo = new SqlParameter("PhysicianNo", RequestItem?.PhysicianNo);
                    var _status = new SqlParameter("Status", RequestItem?.Status);


                    result = context.OPDStatusLogList.FromSqlRaw(
                    "Execute dbo.Pro_GetOPDStatusLog @VenueNo, @VenueBranchNo,@AppointmentNo,@OPDPatientAppointmentNo,@Userno,@PageIndex,@FromDate,@ToDate,@Type,@SpecialisationNo,@PhysicianNo,@Status",
                    _VenueNo,_VenueBranchNo,_AppointmentNo,_OPDPatientAppointmentNo,_Userno,_pageIndex,_FromDate,_ToDate,_Type,_specialisationNo,_physicianNo,_status).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetOPDStatusLogList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem?.VenueNo, RequestItem?.VenueBranchNo, RequestItem?.Userno);
            }
            return result;
        }

        public List<displaylist> GetDisplayView(int VenueNo, int VenueBranchNo, int type)
        {
            List<displaylist> objresult = new List<displaylist>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _type = new SqlParameter("type", type);

                    objresult = context.displaylistEF.FromSqlRaw(
                        "Execute dbo.Pro_GetOPD_Display @VenueNo,@VenueBranchNo,@type", _VenueNo, _VenueBranchNo, _type).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientRepository.GetDisplayView", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<SearchOPDMachinePatient> GetPatientMachineData(SearchOPDPatientRequest RequestItem)
        {
            List<SearchOPDMachinePatient> result = new List<SearchOPDMachinePatient>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Searchkey = new SqlParameter("Searchkey", RequestItem.Searchkey);
                    var _Searchvalue = new SqlParameter("Searchvalue", RequestItem.Searchvalue);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);

                    result = context.SearchOPDMachinePatient.FromSqlRaw(
                        "Execute dbo.pro_GetOPDMachinePatientdata @Searchkey,@Searchvalue,@VenueNo,@VenueBranchNo",
                    _Searchkey, _Searchvalue, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientMachineData", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDPatientMachineBookingList> GetPatientMachineBookingList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDPatientMachineBookingList> result = new List<OPDPatientMachineBookingList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AppointmentDate = new SqlParameter("AppointmentDate", RequestItem.AppointmentDate);
                    var _MachineNo = new SqlParameter("MachineNo", RequestItem.MachineNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _BookingType = new SqlParameter("BookingType", RequestItem.BookingType);

                    result = context.OPDPatientMachineBookingList.FromSqlRaw(
                        "Execute dbo.pro_GetOPDMachineBookingdata @AppointmentDate,@MachineNo,@VenueNo,@VenueBranchNo,@BookingType",
                    _AppointmentDate, _MachineNo, _VenueNo, _VenueBranchNo, _BookingType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientMachineBookingList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        public List<OPDPatientMachineDTOList> GetOPDPatientMachineList(CommonFilterRequestDTO RequestItem)
        {
            List<OPDPatientMachineDTOList> result = new List<OPDPatientMachineDTOList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem?.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _machineNo = new SqlParameter("machineNo", RequestItem?.machineNo);
                    var _AppointmentStatus = new SqlParameter("AppointmentStatus", RequestItem?.orderStatus);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _AppointmentCategory = new SqlParameter("AppointmentCategory", RequestItem?.AppointmentCategory);
                    var _AppointmentMode = new SqlParameter("AppointmentMode", RequestItem?.AppointmentMode);
                    var _BranchNo = new SqlParameter("BranchNo", RequestItem?.BilledBranchNo);
                    var _MobileNo = new SqlParameter("MobileNo", RequestItem?.MobileNo);
                    var _PatientName = new SqlParameter("PatientName", RequestItem?.PatientName);
                    var _AppointmentNo = new SqlParameter("AppointmentNo", RequestItem?.AppointmentNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem?.PatientNo);
                    var _loginType = new SqlParameter("loginType", RequestItem?.loginType);


                    result = context.OPDPatientMachineDTOList.FromSqlRaw(
                        "Execute dbo.Pro_GetOPDTransactionMachine  @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@MachineNo,@AppointmentStatus,@PageIndex," +
                        "@UserNo,@AppointmentCategory,@AppointmentMode,@BranchNo,@MobileNo,@PatientName,@AppointmentNo,@PatientNo,@loginType",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _machineNo, _AppointmentStatus, _pageIndex, _userNo, _AppointmentCategory,
                    _AppointmentMode, _BranchNo, _MobileNo, _PatientName, _AppointmentNo, _PatientNo, _loginType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetOPDPatientList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return result;
        }

        public OPDPatientMachineResponse InsertOPDMachinePatient(OPDPatientOfficeDTO objDTO)
        {
            OPDPatientMachineResponse result = new OPDPatientMachineResponse();
            try
            {

                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDPatientNo = new SqlParameter("OPDPatientNo", objDTO?.OPDPatientNo);
                    var _oPDPatientAppointmentNo = new SqlParameter("oPDPatientAppointmentNo", objDTO?.oPDPatientAppointmentNo);
                    var _TitleCode = new SqlParameter("TitleCode", objDTO?.TitleCode.ValidateEmpty());
                    var _FirstName = new SqlParameter("FirstName", objDTO?.FirstName.ValidateEmpty());
                    var _MiddleName = new SqlParameter("MiddleName", objDTO?.MiddleName.ValidateEmpty());
                    var _LastName = new SqlParameter("LastName", objDTO?.LastName.ValidateEmpty());
                    var _DOB = new SqlParameter("DOB", objDTO?.DOB.ValidateEmpty());
                    var _Gender = new SqlParameter("Gender", objDTO?.Gender.ValidateEmpty());
                    var _Age = new SqlParameter("Age", objDTO?.Age);
                    var _AgeType = new SqlParameter("AgeType", objDTO?.AgeType.Substring(0, 1));
                    var _MobileNumber = new SqlParameter("MobileNumber", objDTO?.MobileNumber.ValidateEmpty());
                    var _AltMobileNumber = new SqlParameter("WhatsappNo", objDTO?.WhatsappNo.ValidateEmpty());
                    var _EmailID = new SqlParameter("EmailID", objDTO?.EmailID.ValidateEmpty());
                    var _SecondaryEmailID = new SqlParameter("SecondaryEmailID", objDTO?.SecondaryEmailID.ValidateEmpty());
                    var _Address = new SqlParameter("Address", objDTO?.Address.ValidateEmpty());
                    var _CountryNo = new SqlParameter("CountryNo", objDTO?.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objDTO?.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objDTO?.CityNo);
                    var _AreaName = new SqlParameter("AreaName", objDTO?.AreaName.ValidateEmpty());
                    var _Pincode = new SqlParameter("Pincode", objDTO?.Pincode.ValidateEmpty());
                    var _AppointmentMode = new SqlParameter("AppointmentMode", objDTO?.AppointmentMode);
                    var _MachineNo = new SqlParameter("MachineNo", objDTO?.MachineNo);
                    var _Reason = new SqlParameter("Reason", objDTO?.Reason.ValidateEmpty());
                    var _AppointmentDateTime = new SqlParameter("AppointmentDateTime", objDTO?.AppointmentDateTime.ValidateEmpty());
                    var _ArrivedDateTime = new SqlParameter();
                    if (string.IsNullOrEmpty(objDTO?.ArrivedDateTime))
                        _ArrivedDateTime = new SqlParameter("ArrivedDateTime", DBNull.Value);
                    else
                        _ArrivedDateTime = new SqlParameter("ArrivedDateTime", objDTO?.ArrivedDateTime.ValidateEmpty());
                    var _appointmentStatus = new SqlParameter("appointmentStatus", objDTO?.appointmentStatus);
                    var _IsNew = new SqlParameter("IsNew", objDTO?.IsNew);
                    var _IsEmergency = new SqlParameter("IsEmergency", objDTO?.IsEmergency);
                    var _IsVIP = new SqlParameter("IsVIP", objDTO?.IsVIP);
                    var _VenueNo = new SqlParameter("VenueNo", objDTO?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO?.VenueBranchNo);
                    var _UserID = new SqlParameter("userNo", objDTO?.UserNo.ToString());
                    var _PatientVisitID = new SqlParameter("PatientVisitID", objDTO?.PatientVisitID);
                    var _ReasonType = new SqlParameter("ReasonType", objDTO?.ReasonType);
                    var _RefferalType = new SqlParameter("RefferalType", objDTO?.RefferalType);
                    var _PhysNo = new SqlParameter("PhysNo", objDTO?.PhysNo);
                    var _RefTypeothers = new SqlParameter("RefTypeothers", objDTO?.RefTypeothers);
                    result = context.OPDPatientMachineList.FromSqlRaw(
                   "Execute dbo.pro_InsertOPDMachinePatient @OPDPatientNo,@oPDPatientAppointmentNo,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeType,@MobileNumber,@WhatsappNo," +
                   "@EmailID,@SecondaryEmailID,@Address,@CountryNo,@StateNo,@CityNo,@AreaName,@Pincode,@AppointmentMode,@MachineNo,@Reason,@AppointmentDateTime,@ArrivedDateTime," +
                   "@appointmentStatus,@IsNew,@IsEmergency,@IsVIP,@VenueNo,@VenueBranchNo,@userNo,@PatientVisitID,@ReasonType,@RefferalType,@PhysNo,@RefTypeothers ",
                   _OPDPatientNo, _oPDPatientAppointmentNo, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeType, _MobileNumber, _AltMobileNumber, _EmailID,
                   _SecondaryEmailID, _Address, _CountryNo, _StateNo, _CityNo, _AreaName, _Pincode, _AppointmentMode, _MachineNo, _Reason, _AppointmentDateTime,
                   _ArrivedDateTime, _appointmentStatus, _IsNew, _IsEmergency, _IsVIP, _VenueNo, _VenueBranchNo, _UserID,
                   _PatientVisitID, _ReasonType, _RefferalType, _PhysNo, _RefTypeothers).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportRepository.InsertOPDMachinePatient/PatientVisitNo-" + objDTO.PatientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
    }
}
