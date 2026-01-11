using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text;
using System.Linq;
using Shared.Audit;
using RtfPipe.Tokens;
using Newtonsoft.Json;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IUserRepository _IUserRepository;
        private readonly IAuditService _auditService;
        public UserController(IUserRepository noteRepository, IJWTManagerRepository jWTManager, IAuditService auditService)
        {
            this._jWTManager = jWTManager;
            _IUserRepository = noteRepository;
            _auditService = auditService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/authenticate")]
        public UserResponseEntity UserLogIn(UserRequestEntity req)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                result = _IUserRepository.UserLogIn(req, _jWTManager);
                if (result.ResponseStatus == 0 || result.ResponseStatus == -1)
                {
                    Unauthorized();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.UserLogIn/LoginName-" + req.LoginName, ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/ValidateOTP")]
        public UserResponseEntity ValidateOTP(UserRequestEntity req)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                result = _IUserRepository.ValidateOTP(req, _jWTManager);
                if (result.ResponseStatus == 0 || result.ResponseStatus == -1)
                {
                    Unauthorized();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ValidateOTP/LoginName-" + req.LoginName, ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/ResetSecret")]
        public int ResetSecret(UserRequestEntity req)
        {
            int result = 0;
            try
            {
                result = _IUserRepository.ResetSecret(req);
              
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ResetSecret/LoginName-" + req.LoginName, ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/Master/InsertUserMaster")]
        public ActionResult InsertUserMaster(UserDetailsDTO Useritem)
        {
            int result = 0;
            try
            {
                var _errormsg = UserValidation.InsertUserMaster(Useritem);
                if (!_errormsg.status)
                {
                    result = _IUserRepository.InsertUserMaster(Useritem);
                    string _CommonCommonCatch = CacheKeys.CommonMaster + "COMMON" + Useritem.VenueNo + Useritem.VenueBranchNo;
                    string _CommonRidderCatch = CacheKeys.CommonMaster + "RIDER" + Useritem.VenueNo + Useritem.VenueBranchNo;
                    string _CommonMarketingCatch = CacheKeys.CommonMaster + "MARKETING" + Useritem.VenueNo + Useritem.VenueBranchNo;
                    string _CommonSuperUserCatch = CacheKeys.CommonMaster + "ISUSPER_USER" + Useritem.VenueNo + Useritem.VenueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CommonCommonCatch);
                    MemoryCacheRepository.RemoveItem(_CommonRidderCatch);
                    MemoryCacheRepository.RemoveItem(_CommonMarketingCatch);
                    MemoryCacheRepository.RemoveItem(_CommonSuperUserCatch);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.InsertUserMaster/LoginName-" + Useritem.LoginName, ExceptionPriority.High, ApplicationType.APPSERVICE, Useritem.VenueNo, Useritem.VenueBranchNo, Useritem.UserNo);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/User/GetUserDetails")]
        public List<UserDetailsDTO> GetUserDetails(int VenueNo, int VenueBranchNo, int PageIndex, int DefaultBranchNo)
        {
            List<UserDetailsDTO> result = new List<UserDetailsDTO>();
            try
            {
                result = _IUserRepository.GetUserDetails(VenueNo, VenueBranchNo, PageIndex, DefaultBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetUserDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpGet]
        [Route("api/User/GetUserMenuMapping")]
        public List<UserModuleDTO> GetUserMenuMapping(int VenueNo, int VenueBranchNo, int userno, int MenuLoadUserNo)
        {
            List<UserModuleDTO> objresult = new List<UserModuleDTO>();
            try
            {
                objresult = _IUserRepository.GetUserMenuMapping(VenueNo, VenueBranchNo, userno, MenuLoadUserNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetUserMenuMapping", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpGet]
        [Route("api/User/GetUserTask")]
        public List<UserModuleDTO> GetUserTask(int VenueNo, int VenueBranchNo, int userno, int MenuLoadUserNo)
        {
            List<UserModuleDTO> objresult = new List<UserModuleDTO>();
            try
            {
                objresult = _IUserRepository.GetUserMenuMapping(VenueNo, VenueBranchNo, userno, MenuLoadUserNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetUserTask", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/User/InsertMenuMapping")]
        public int InsertMenuMapping(ReqUserMenu Useritem)
        {
            int result = 0;
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            try
            {
                if (user.UserNo == Useritem.userNo)
                {
                    if (_IUserRepository.ValidateActionMenu(user.UserNo, Useritem.venueNo, "UMM") == 0) //User Menu Mapping
                    {
                       
                        string _CacheKey = CacheKeys.UserMenu + Useritem.MenuUserNo + Useritem.venueNo + Useritem.venueBranchNo + "2";
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                        Unauthorized();
                        return result;
                    }
                    else
                    {                       
                        result = _IUserRepository.InsertMenuMapping(Useritem);
                        string _CacheKey = CacheKeys.UserMenu + Useritem.MenuUserNo + Useritem.venueNo + Useritem.venueBranchNo + "2";
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }                    
                }
                else
                {
                    Unauthorized();
                    return result;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.InsertMenuMapping", ExceptionPriority.High, ApplicationType.APPSERVICE, Useritem.venueNo, Useritem.venueBranchNo, Useritem.userNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/User/GetPageMenuList")]
        public List<NavDTO> GetPageMenuList(int VenueNo, int VenueBranchNo, int userno, int logintype)
        {
            List<NavDTO> objresult = new List<NavDTO>();
            try
            {
                objresult = _IUserRepository.GetPageMenuList(VenueNo, VenueBranchNo, userno, logintype);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetPageMenuList", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public int ChangePassword(ChangePasswordEntity req)
        {
            int result = 0;
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                if (user.UserNo == req.UserNo)
                {
                    result = _IUserRepository.ChangePassword(req);
                }
                else
                {
                    Unauthorized();
                    return result;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ChangePassword", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.UserNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpGet]
        [Route("api/User/ResetPassword")]
        public int ResetPassword(int ResetUserNo, int VenueNo, int VenueBranchNo, int usertype)
        {
            int result = 0;
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            try
            {
                if (_IUserRepository.ValidateActionMenu(user.UserNo, VenueNo, "PRS") == 0) //PRS - Password reset
                {
                    Unauthorized();
                    return result;
                }   
                else
                {
                    result = _IUserRepository.ResetPassword(user.UserNo, VenueNo, VenueBranchNo, usertype, ResetUserNo);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ResetPassword", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, user.UserNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/User/GetDashBoardMaster")]
        public List<UserDashBoardMasterResponse> GetDashBoardMaster(int CustomerNo, int VenueNo, int VenueBranchNo)
        {
            List<UserDashBoardMasterResponse> result = new List<UserDashBoardMasterResponse>();
            try
            {
                result = _IUserRepository.GetDashBoardMaster(CustomerNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ResetPassword", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/User/GetUserBranchList")]
        public List<userbranchlist> GetUserBranchList(int VenueNo, int VenueBranchNo, int userno)
        {
            List<userbranchlist> result = new List<userbranchlist>();
            try
            {
                result = _IUserRepository.GetUserBranchList(userno, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.ResetPassword", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/User/InsertRoleMenuMapping")]
        public ActionResult InsertRoleMenuMapping(ReqRoleMenu Useritem)
        {
            int result = 0;
            try
            {
                var reqClone = JsonConvert.DeserializeObject<ReqRoleMenu>(JsonConvert.SerializeObject(Useritem));
                using (var auditScope = new AuditScope<ReqRoleMenu>(reqClone, _auditService, "ROLEMENU", new string[] { "Role Menu Save" }))
                {
                    var _errormsg = UserValidation.InsertRoleMenuMapping(Useritem);
                    if (!_errormsg.status)
                    {
                        result = _IUserRepository.InsertRoleMenuMapping(Useritem);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.InsertRoleMenuMapping", ExceptionPriority.High, ApplicationType.APPSERVICE, Useritem.venueNo, Useritem.venueBranchNo, 0);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/User/GetRoleMenuMapping")]
        public List<UserModuleDTO> GetRoleMenuMapping(RolegetReqDTO rolereq)
        {
            List<UserModuleDTO> result = new List<UserModuleDTO>();
            try
            {
                result = _IUserRepository.GetRoleMenuMapping(rolereq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetRoleMenuMapping", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/User/Logout")]
        public void Logout()
        {
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                _IUserRepository.UpdateSession(user.UserNo, user.VenueNo, user.VenueBranchNo, token ?? "");
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.Logout", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpGet]
        [Route("api/User/getusermenucode")]
        public List<UserRoleNameDTO> GetUserMenuCode(int userno, int VenueNo, int VenueBranchNo, int LoginType)
        {
            List<UserRoleNameDTO> objresult = new List<UserRoleNameDTO>();
            try
            {
                objresult = _IUserRepository.GetUserMenuCode(VenueNo, VenueBranchNo, userno, LoginType);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.getusermenucode", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/sendOTP")]
        public int sendOTP(int userno,string phoneno)
        {
            int result = 0;
            try
            {
                result=_IUserRepository.sendOTP(userno, phoneno);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.sendOTP", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, userno);
            }
            return result;
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/verifyOTP")]
        public int verifyOTP(int userno, string otptext)
        {
            int result = 0;
            try
            {
                result = _IUserRepository.verifyOTP(userno, otptext);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.verifyOTP", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, userno);
            }
            return result;
        }
        
        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/User/InsertRoleMaster")]
        public ActionResult<InsertRoleRes> InsertRoleMaster (InsertRoleReq req)
        {
            InsertRoleRes result = new InsertRoleRes();
            try
            {
                using (var auditScoped = new AuditScope<InsertRoleReq>(req, _auditService))
                {
                    var _errormsg = UserValidation.InsertRoleMaster(req);
                    if (!_errormsg.status)
                    {
                        result = _IUserRepository.InsertRoleMaster(req);
                        string _CacheKey = CacheKeys.CommonMaster + "ROLEMASTER" + req.VenueNo + req.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.InsertRoleMaster", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSUserMgmt")]
        [HttpPost]
        [Route("api/User/GetRoleMaster")]
        public List<GetRoleRes> GetRoleMaster (GetRoleReq req)
        {
            List<GetRoleRes> result = new List<GetRoleRes>();
            try
            {
                result = _IUserRepository.GetRoleMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetRoleMaster", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.RoleId, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpPost]
        [Route("api/User/useresign")]
        public UserResponseEntity UserESign(UserRequestEntity req)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                result = _IUserRepository.UserESign(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.UserESign-" + req.LoginName, ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpPost]
        [Route("api/User/GetUserDepartment")]
        public List<GetUserDepartmentDTO> GetUserDepartment(UserDepartmentDTO deptreq)
        {
            List<GetUserDepartmentDTO> result = new List<GetUserDepartmentDTO>();
            try
            {
                result = _IUserRepository.GetUserDepartment(deptreq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserController.GetUserDepartment", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
    }
}