using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Serilog;
using DEV.Model.Sample;

namespace Dev.Repository
{
    public class ExternalPatientAPIRepositoy : IExternalPatientAPIRepositoy
    {
        private IConfiguration _config;
        public ExternalPatientAPIRepositoy(IConfiguration config) { _config = config; }
        public ExternalPatientLoginResponse Login(ExternalPatientLoginRequest results)
        {
            ExternalPatientLoginResponse result = new ExternalPatientLoginResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    int OTP = CommonHelper.GenerateRandomNo();
                    var _MobileNo = new SqlParameter("MobileNo", results.MobileNo);
                    var _EmailId = new SqlParameter("Email", results.EmailId);
                    var _OTP = new SqlParameter("OTP", OTP);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueNo", results.VenueBranchNo);

                    var output = context.PM_PatientLogin.FromSqlRaw(
                    "Execute dbo.Pro_PA_PatientUser @MobileNo,@Email,@OTP,@VenueNo,@VenueBranchNo",
                    _MobileNo, _EmailId, _OTP, _VenueNo, _VenueBranchNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.result == "0")
                        {
                            result.otp = string.Empty;
                            result.userNo = 0;
                            result.status = 0;

                        }
                        else
                        {
                            result.otp = OTP.ToString();
                            result.userNo = Convert.ToInt16(output.result);
                            result.status = 1;
                        }
                    }
                    else {
                        result.otp = string.Empty;
                        result.userNo = 0;
                        result.status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.Login", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }
        public ExternalPatientOTPResponse OTPVerify(ExternalPatientOTPRequest results)
        {
            ExternalPatientOTPResponse result = new ExternalPatientOTPResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _OTPNo = new SqlParameter("OTPNo", results.otp);
                    var _UserNo = new SqlParameter("userno", results.userNo);

                    var output = context.PM_OTPVerify.FromSqlRaw(
                    "Execute dbo.Pro_PA_OTPVerify @OTPNo,@userno", _OTPNo, _UserNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.username == "0")
                        {
                            result.username = "";
                            result.status = 0;

                        }
                        else
                        {
                            result.username = output.username;
                            result.status = 1;
                        }
                    }
                    else {
                        result.username = "";
                        result.status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalAPIRepository.OTPVerify", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public ExternalPatientSignupResponse Signup(ExternalPatientSignupRequest results)
        {
            ExternalPatientSignupResponse result = new ExternalPatientSignupResponse();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _Title = new SqlParameter("Title", results.Title);
                    var _Name = new SqlParameter("Name", results.Name);
                    var _Gender = new SqlParameter("Gender", results.Gender);
                    var _DOB = new SqlParameter("DOB", results.DOB);
                    var _MobileNo = new SqlParameter("MobileNo", results.MobileNumber);
                    var _EmailId = new SqlParameter("Email", results.EmailId);
                    var _Address = new SqlParameter("Address", results.Address);
                    int OTP = CommonHelper.GenerateRandomNo();
                    var _OTPNo = new SqlParameter("OTPNo", OTP);
                    var _type = new SqlParameter("type", 1);
                    var _UserNo = new SqlParameter("userno", 0);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueNo", results.VenueBranchNo);

                    var output = context.PatientSignUp.FromSqlRaw(
                    "Execute dbo.Pro_PA_PatientSignUp @Title,@Name,@Gender,@DOB,@MobileNo,@Email,@Address,@OTPNo,@type,@userno,@VenueNo,@VenueBranchNo",
                    _Title, _Name, _Gender, _DOB, _MobileNo, _EmailId, _Address, _OTPNo, _type, _UserNo, _VenueNo, _VenueBranchNo).FirstOrDefault();
                    if (output != null)
                    {
                        if (output.result == "0")
                        {
                            result.otp = "";
                            result.userNo = 0;
                            result.status = 0;

                        }
                        else
                        {
                            result.otp = OTP.ToString();
                            result.userNo = Convert.ToInt16(output.result);
                            result.status = 1;
                        }
                    }
                    else {
                        result.otp = "";
                        result.userNo = 0;
                        result.status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.Signup", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, 0);
            }
            return result;
        }
        public ExternalPatientAppResponse addMember(ExternalPatientAddmember results)
        {
            ExternalPatientAppResponse result = new ExternalPatientAppResponse();
            try
            {

                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _SignupNo = new SqlParameter("SignupNo", results.SignupNo);
                    var _TitleCode = new SqlParameter("Title", results.Title);
                    var _FirstName = new SqlParameter("Name", results.Name);
                    var _Age = new SqlParameter("Age", results.Age);
                    var _Gender = new SqlParameter("Gender", results.Gender);
                    var _UserID = new SqlParameter("Address", results.Address);
                    var _Relationship = new SqlParameter("Relationship", results.Relationship);
                    var lst = context.PatientAddmember.FromSqlRaw(
                    "Execute dbo.Pro_PA_Addmember @SignupNo,@Title,@Name,@Age,@Gender,@Address,@Relationship",
                     _SignupNo, _TitleCode, _FirstName, _Age, _Gender, _UserID, _Relationship).FirstOrDefault();
                    if (lst != null)
                    {
                        if (lst.result > 0)
                        {
                            result.status = 1;
                            result.message = "Member created successfully";
                        }
                        else
                        {

                            result.status = 0;
                            result.message = "Invalid Member";
                        }
                    }
                    else {
                        result.status = 0;
                        result.message = "Invalid Member";
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.addMember", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public ExternalPatientMasterData getMasterRecord(ExternalPatientCommonRequest results)
        {
            ExternalPatientMasterData result = new ExternalPatientMasterData();
            try
            {
                IMasterRepository _masterRepository = new MasterRepository(_config);
                var Titlelst = _masterRepository.GetCommonMasterList(results.VenueNo, results.VenueBranchNo, "Title");
                result.lstTitle = new List<ExternalPatientMasterDataResponse>();
                foreach (var title in Titlelst)
                {
                    ExternalPatientMasterDataResponse objitem = new ExternalPatientMasterDataResponse();
                    objitem.Key = title.CommonCode;
                    objitem.Value = title.CommonValue;
                    result.lstTitle.Add(objitem);
                }
                result.lstGender = new List<ExternalPatientMasterDataResponse>();
                var genderlst = _masterRepository.GetCommonMasterList(results.VenueNo, results.VenueBranchNo, "Gender");
                foreach (var title in genderlst)
                {
                    ExternalPatientMasterDataResponse objitem = new ExternalPatientMasterDataResponse();
                    objitem.Key = title.CommonCode;
                    objitem.Value = title.CommonValue;
                    result.lstGender.Add(objitem);
                }
                result.lstRelationship = new List<ExternalPatientMasterDataResponse>();
                var Relationshiplst = _masterRepository.GetCommonMasterList(results.VenueNo, results.VenueBranchNo, "Relationship");
                foreach (var title in genderlst)
                {
                    ExternalPatientMasterDataResponse objitem = new ExternalPatientMasterDataResponse();
                    objitem.Key = title.CommonCode;
                    objitem.Value = title.CommonValue;
                    result.lstRelationship.Add(objitem);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.getMasterRecord", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.userNo);
            }
            return result;
        }
        public ExternalPatientAppServiceResponse GetService(int VenueNo, int VenueBranchNo, int IsApproval)
        {
            ExternalPatientAppServiceResponse result = new ExternalPatientAppServiceResponse();
            try
            {
                IFrontOfficeRepository frontOfficeRepository = new FrontOfficeRepository(_config);
                var data = frontOfficeRepository.GetService(VenueNo, VenueBranchNo, IsApproval);
                if (data.Count > 0)
                {
                    result.lstService = new List<ServiceSearchDTO>();
                    result.status = 1;
                    result.message = "Data Fetched Successfully";
                }
                else
                {
                    result.status = 0;
                    result.message = "Failed";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.GetService", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        public ServiceRateList GetServiceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo)
        {
            ServiceRateList objresult = new ServiceRateList();
            try
            {
                IFrontOfficeRepository frontOfficeRepository = new FrontOfficeRepository(_config);
                objresult = frontOfficeRepository.GetServiceDetails(ServiceNo, ServiceType, ClientNo, VenueNo, VenueBranchNo, 0, 0);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.GetServiceDetails/ServiceNo-" + ServiceNo, ExceptionPriority.Medium, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public ExternalPatientEditResponse editprofile(ExternalPatientEditRequest results)
        {
            ExternalPatientEditResponse result = new ExternalPatientEditResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientNo = new SqlParameter("PatientNo", results.UserNo);
                    var _TitleCode = new SqlParameter("TitleCode", results.Title);
                    var _FirstName = new SqlParameter("FirstName", results.Name);
                    var _DOB = new SqlParameter("DOB", results.DOB);
                    var _Gender = new SqlParameter("Gender", results.Gender);
                    var _EmailID = new SqlParameter("EmailID", results.EmailId);
                    var _Address = new SqlParameter("Address", results.Address);
                    var output = context.ExternalPatientEditResult.FromSqlRaw(
                     "Execute dbo.pro_EditPatientmaster @PatientNo,@TitleCode,@FirstName,@DOB,@Gender,@EmailID,@Address",
                     _PatientNo, _TitleCode, _FirstName, _DOB, _Gender, _EmailID, _Address).FirstOrDefault();
                    result.status = 1;
                    result.message = "Account Updated successfully";
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.editprofile", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public ExternalPatientUserDetail getUserDetail(ExternalPatientCommonRequest results)
        {
            ExternalPatientUserDetail result = new ExternalPatientUserDetail();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _UserNo = new SqlParameter("PatientNo", results.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", results.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueNo", results.VenueBranchNo);

                    var output = context.PatientUserEF.FromSqlRaw(
                    "Execute dbo.Pro_PatientUserDetail @PatientNo,@VenueNo,@VenueBranchNo",
                     _UserNo, _VenueNo, _VenueBranchNo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ExternalPatientController.getUserDetail", ExceptionPriority.High, ApplicationType.REPOSITORY, results.VenueNo, results.VenueBranchNo, results.userNo);
            }
            return result;
        }

    }
}
