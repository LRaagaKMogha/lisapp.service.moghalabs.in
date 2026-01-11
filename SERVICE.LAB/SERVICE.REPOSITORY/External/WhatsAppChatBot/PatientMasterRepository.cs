using Dev.IRepository.External.WhatsAppChatBot;
using DEV.Common;
using Service.Model.EF.External.WhatsAppChatBot;
using Service.Model.External.WhatsAppChatBot;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;

namespace Dev.Repository.External.WhatsAppChatBot
{
    public class PatientMasterRepository : IPatientMasterRepository
    {
        private IConfiguration _config;
        public PatientMasterRepository(IConfiguration config) { _config = config; }
        public GetPatientResponse GetPatientMaster(GetPatientRequest objReq)
        {
            GetPatientResponse objResponse = new GetPatientResponse();
            try
            {
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientMasterRepository.GetPatientMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
        public int UpdatePatientMaster(UpdatePatientRequest objReq)
        {
            int patientno = 0;
            try
            {
                using (var context = new PatientContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string Password = Guid.NewGuid().ToString("N").Substring(0, 7);
                    var _PatientNo = new SqlParameter("PatientNo", objReq.PatientNo);
                    var _Title = new SqlParameter("Title", objReq.Title);
                    var _FirstName = new SqlParameter("FirstName", objReq.FirstName);
                    var _MiddleName = new SqlParameter("MiddleName", string.IsNullOrEmpty(objReq.MiddleName) ? string.Empty : objReq.MiddleName);
                    var _LastName = new SqlParameter("LastName", string.IsNullOrEmpty(objReq.LastName) ? string.Empty : objReq.LastName);
                    var _DOB = new SqlParameter("DOB", string.IsNullOrEmpty(objReq.DOB) ? string.Empty : objReq.DOB);
                    var _Gender = new SqlParameter("Gender", objReq.Gender);
                    var _Age = new SqlParameter("AgeInYears", objReq.AgeInYear);
                    var _AgeMonths = new SqlParameter("AgeInMonths", objReq.AgeInMonths);
                    var _AgeDays = new SqlParameter("AgeInDays", objReq.AgeInDays);
                    var _MobileNumber = new SqlParameter("MobileNumber", string.IsNullOrEmpty(objReq.MobileNumber) ? string.Empty : objReq.MobileNumber);
                    var _WhatsappNumber = new SqlParameter("WhatsappNumber", string.IsNullOrEmpty(objReq.WhatsappNumber) ? string.Empty : objReq.WhatsappNumber);
                    var _LandlineNumber = new SqlParameter("LandlineNumber", string.IsNullOrEmpty(objReq.LandlineNumber) ? string.Empty : objReq.LandlineNumber);
                    var _EmailID = new SqlParameter("EmailID", string.IsNullOrEmpty(objReq.EmailID) ? string.Empty : objReq.EmailID);
                    var _Address = new SqlParameter("Address", string.IsNullOrEmpty(objReq.Address) ? string.Empty : objReq.Address);
                    var _CountryNo = new SqlParameter("CountryNo", objReq.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", objReq.StateNo);
                    var _CityNo = new SqlParameter("CityNo", objReq.CityNo);
                    var _Place = new SqlParameter("Place", string.IsNullOrEmpty(objReq.Place) ? string.Empty : objReq.Place);
                    var _Pincode = new SqlParameter("Pincode", string.IsNullOrEmpty(objReq.Pincode) ? string.Empty : objReq.Pincode);
                    var _RemarksHistory = new SqlParameter("RemarksHistory", string.IsNullOrEmpty(objReq.RemarksHistory) ? string.Empty : objReq.RemarksHistory);
                    var _MaritalStatus = new SqlParameter("MaritalStatus", objReq.MaritalStatus);
                    var _Nationality = new SqlParameter("Nationality", objReq.NationalityNo);
                    var _BloodGroup = new SqlParameter("BloodGroup", objReq.BloodGroup);
                    var _venueno = new SqlParameter("VenueNo", objReq.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", objReq.VenueBranchNo);
                    var _UserID = new SqlParameter("UserID", objReq.UserID);
                    var _IsActive = new SqlParameter("IsActive", objReq.IsActive);
                    var _Password = new SqlParameter("Pass", CommonSecurity.EncodePassword(Password, CommonSecurity.GeneratePassword(1)));

                    var lst = context.SavePatientMaster.FromSqlRaw(
                       "Execute dbo.pro_CB_InsertPatientMaster " +
                       "@PatientNo, @Title, @FirstName, @MiddleName, @LastName, @DOB, @Gender, @AgeInYears, @AgeInMonths,@AgeInDays, " +
                       "@MobileNumber, @WhatsappNumber, @LandlineNumber, @EmailID, @Address, @CountryNo, @StateNo, @CityNo, @Place, @Pincode, " +
                       "@RemarksHistory, @MaritalStatus, @Nationality, @BloodGroup, @VenueNo, @VenueBranchNo, @UserID, @IsActive, @Pass",
                        _PatientNo, _Title, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeMonths, _AgeDays,
                        _MobileNumber, _WhatsappNumber, _LandlineNumber, _EmailID, _Address, _CountryNo, _StateNo, _CityNo, _Place, _Pincode, 
                        _RemarksHistory, _MaritalStatus, _Nationality, _BloodGroup, _venueno, _venuebranchno, _UserID, _IsActive, _Password).ToList();
                    patientno = lst[0].PatientNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientMasterRepository.UpdatePatientMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return patientno;
        }

        public List<PatientInformationResponse> GetPatientList(GetPatientRequest objReq)
        {
            List<PatientInformationResponse> objResponse = new List<PatientInformationResponse>();

            try
            {
                using (var context = new PatientContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", objReq.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", objReq.VenueBranchNo);
                    var _mobileNo = new SqlParameter("MobileNo", objReq.MobileNo);

                    objResponse = context.GetPatientList.FromSqlRaw(
                           "Execute dbo.pro_CB_GetPatientsList" +
                           " @VenueNo, @VenueBranchNo, @MobileNo",
                             _venueNo, _venueBranchNo, _mobileNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientMasterRepository.GetPatientList", ExceptionPriority.High, ApplicationType.REPOSITORY, objReq.VenueNo, objReq.VenueBranchNo, 0);
            }
            return objResponse;
        }

        public PatientVisitResponse GetPatientVisit(GetPatientVisitRequest objReq)
        {
            PatientVisitResponse objResponse = new PatientVisitResponse();

            try
            {
                using (var context = new PatientContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", objReq.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", objReq.VenueBranchNo);
                    var _patientNo = new SqlParameter("PatientNo", objReq.PatientNo);
                    var _visitId = new SqlParameter("VisitId", string.IsNullOrEmpty(objReq.VisitId) ? string.Empty : objReq.VisitId);

                    var lst = context.GetPatientVisitList.FromSqlRaw(
                           "Execute dbo.pro_CB_GetPatientVisitsList" +
                           " @VenueNo, @VenueBranchNo, @PatientNo, @VisitId",
                             _venueNo, _venueBranchNo, _patientNo, _visitId).ToList();

                    objResponse.RowNo = lst[0].RowNo;
                    objResponse.PatientId = lst[0].PatientId;
                    objResponse.PatientNo = lst[0].PatientNo;
                    objResponse.VisitNo = lst[0].VisitNo;
                    objResponse.VisitId = lst[0].VisitId;
                    objResponse.VisitDtTm = lst[0].VisitDtTm;
                    objResponse.FullName = lst[0].FullName;
                    objResponse.AgeInYears = lst[0].AgeInYears;
                    objResponse.AgeInMonths = lst[0].AgeInMonths;
                    objResponse.AgeInDays = lst[0].AgeInDays;
                    objResponse.Gender = lst[0].Gender;
                    objResponse.IsDue = lst[0].IsDue;
                    objResponse.PayList = JsonConvert.DeserializeObject<List<PayList>>(lst[0].PayList);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientMasterRepository.GetPatientVisit", ExceptionPriority.High, ApplicationType.REPOSITORY, objReq.VenueNo, objReq.VenueBranchNo, 0);
            }
            return objResponse;
        }
    }
}