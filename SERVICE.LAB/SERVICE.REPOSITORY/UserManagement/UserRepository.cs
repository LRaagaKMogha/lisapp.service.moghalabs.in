using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Service.Model.EF;
using Microsoft.Data.SqlClient;
using DEV.Common;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;
using OtpNet;

namespace Dev.Repository
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _config;
        public UserRepository(IConfiguration config) { _config = config; }
        public UserResponseEntity UserLogIn(UserRequestEntity req, IJWTManagerRepository _jWTManagerRepository)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                if ((int)LoginType.CUSTOMER == req.LoginType)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblCustomer.Where(a => a.UserName == req.LoginName && a.Status == true && a.VenueBranchNo == req.VenueBranchNo && (a.IsFranchisee == 0 || a.IsFranchisee == null)).Select(x => new { x.CustomerName, x.CustomerNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();

                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.UserName = userresult.CustomerName;
                                result.UserNo = userresult.CustomerNo;
                                result.VenueNo = userresult.VenueNo;
                                result.VenueBranchNo = userresult.VenueBranchNo;
                                result.IsAdmin = false;
                                result.IsSuperAdmin = false;
                                result.IsEditLabResults = false;
                                result.lstUserRoleName = GetUserMenuCode(userresult.CustomerNo, userresult.VenueNo, userresult.VenueBranchNo, req.LoginType);
                                var token = _jWTManagerRepository.Authenticate(result);
                                result.Token = token.Token;
                                result.RefreshToken = token.RefreshToken;

                                using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    TblUserSession objsessionitem = new TblUserSession();
                                    objsessionitem.UserNo = result.UserNo;
                                    objsessionitem.LoginType = req.LoginType;
                                    objsessionitem.LogInDateTime = DateTime.Now;
                                    objsessionitem.ClientSysteminfo = req.ClientSysteminfo;
                                    objsessionitem.Ipaddress = req.Ipaddress;
                                    objsessionitem.VenueNo = result.VenueNo;
                                    objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                    objsessionitem.Status = true;
                                    objsessionitem.IsClosed = false;
                                    objsessionitem.IsAdmin = false;
                                    objsessionitem.IsSuperAdmin = false;
                                    objsessionitem.IsEditLabResults = false;
                                    objsessionitem.Token = token.Token;
                                    Sessioncontext.TblUserSession.Add(objsessionitem);
                                    Sessioncontext.SaveChanges();
                                }
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }

                if ((int)LoginType.CUSTOMERSUBUSER == req.LoginType)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var usersubresult = context.tblCustomerSubUser.Where(a => a.LoginName == req.LoginName && a.Status == true && a.VenueBranchNo == req.VenueBranchNo).Select(x => new { x.Email, x.PhoneNo, x.UserName, x.LoginName, x.CustomerSubUserNo, x.CustomerNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();
                        if (usersubresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (usersubresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.UserName = usersubresult.UserName;
                                result.UserNo = usersubresult.CustomerSubUserNo;
                                result.VenueNo = usersubresult.VenueNo;
                                result.VenueBranchNo = usersubresult.VenueBranchNo;
                                result.IsAdmin = false;
                                result.IsSuperAdmin = false;
                                result.IsEditLabResults = false;
                                result.lstUserRoleName = GetUserMenuCode(usersubresult.CustomerSubUserNo, usersubresult.VenueNo, usersubresult.VenueBranchNo, req.LoginType);
                                var token = _jWTManagerRepository.Authenticate(result);
                                result.Token = token.Token;
                                result.RefreshToken = token.RefreshToken;

                                using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    TblUserSession objsessionitem = new TblUserSession();
                                    objsessionitem.UserNo = result.UserNo;
                                    objsessionitem.LoginType = req.LoginType;
                                    objsessionitem.LogInDateTime = DateTime.Now;
                                    objsessionitem.ClientSysteminfo = req.ClientSysteminfo;
                                    objsessionitem.Ipaddress = req.Ipaddress;
                                    objsessionitem.VenueNo = result.VenueNo;
                                    objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                    objsessionitem.Status = true;
                                    objsessionitem.IsClosed = false;
                                    objsessionitem.IsAdmin = false;
                                    objsessionitem.IsSuperAdmin = false;
                                    objsessionitem.IsEditLabResults = false;
                                    objsessionitem.Token = result.Token;
                                    Sessioncontext.TblUserSession.Add(objsessionitem);
                                    Sessioncontext.SaveChanges();
                                }
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
                else if ((int)LoginType.LABORATORY == req.LoginType)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblUser.Where(a => a.LoginName == req.LoginName && a.Status == true && a.IsLogin == true).Select(x => new { x.UserName, x.UserNo, x.Password, x.VenueBranchNo, x.VenueNo, x.IsSuperAdmin, x.IsAdmin, x.IsProvisional, x.IsEditLabResults, x.IsResultEntryHIV, x.IsResultEntryVIP, x.IsPriceShow, x.isLock, x.IsadmultifactorAccess, x.ladpsecretkey, x.PhoneNo, x.Isadaccess, x.IsAbnormalAvail, x.IsPOApproval, x.IsGrnApproval, x.IsGrnReturnApproval, x.IsStockAdjustmentApproval, x.IsConsumptionApproval, x.IsClientApproval, x.Gender }).FirstOrDefault();
                       
                        if (userresult != null)
                        {
                            if ((bool)userresult.Isadaccess)
                            {
                                MasterRepository _IMasterRepository = new MasterRepository(_config);
                                var LDAPURL = _IMasterRepository.GetSingleAppSetting("LDAPURL").ConfigValue;
                                try
                                {
                                    DirectoryEntry dr = new DirectoryEntry(LDAPURL, req.LoginName, req.Password);
                                    DirectorySearcher ds = new DirectorySearcher(dr);
                                    var adresult = ds.FindOne();
                                    if (adresult != null)
                                    {
                                        if (string.IsNullOrEmpty(userresult.PhoneNo))
                                        {
                                            result.ResponseStatus = 3;
                                            result.Token = userresult.ladpsecretkey;
                                            result.UserName = userresult.UserName;
                                            result.UserNo = userresult.UserNo;
                                            result.VenueNo = userresult.VenueNo;
                                            result.VenueBranchNo = userresult.VenueBranchNo;
                                            result.DomainCode = "0";
                                        }
                                        else if (string.IsNullOrEmpty(userresult.ladpsecretkey))
                                        {
                                            var userdata = context.TblUser.Where(user => user.UserNo == userresult.UserNo).FirstOrDefault();
                                            userdata.LoginAttempt = 0;
                                            userdata.ladpsecretkey = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
                                            context.Update(userdata);
                                            context.SaveChanges();
                                            if ((bool)userresult.IsadmultifactorAccess)
                                            {
                                                string SMSURL = _IMasterRepository.GetSingleAppSetting("SMSURL").ConfigValue;
                                                if (!string.IsNullOrEmpty(SMSURL) && !string.IsNullOrEmpty(userresult.PhoneNo))
                                                {
                                                    CommonHelper.SendSMS(SMSURL, userresult.ladpsecretkey, userresult.PhoneNo);
                                                }
                                            }
                                            result.ResponseStatus = 2;
                                            result.Token = userdata.ladpsecretkey;
                                            result.UserName = userresult.UserName;
                                            result.UserNo = userresult.UserNo;
                                            result.VenueNo = userresult.VenueNo;
                                            result.VenueBranchNo = userresult.VenueBranchNo;
                                            result.DomainCode = "0";
                                        }
                                        else
                                        {
                                            result.ResponseStatus = 2;
                                            result.Token = userresult.ladpsecretkey;
                                            result.UserName = userresult.UserName;
                                            result.UserNo = userresult.UserNo;
                                            result.VenueNo = userresult.VenueNo;
                                            result.VenueBranchNo = userresult.VenueBranchNo;
                                            result.DomainCode = "1";
                                        }
                                        }
                                    }
                                catch (Exception ex)
                                {
                                    result.ResponseStatus = -1;
                                }
                                return result;
                            }

                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                using (var CommonContext = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    int VenueBranchNo = 0;
                                    var Mappingitem = CommonContext.TblUserBranchMap.Where(a => a.UserNo == userresult.UserNo && a.Isdefault == true).Select(x => new { x.MappingBranchNo }).FirstOrDefault();
                                    if (Mappingitem != null)
                                    {
                                        VenueBranchNo = Mappingitem.MappingBranchNo;
                                    }
                                    else
                                    {
                                        VenueBranchNo = userresult.VenueBranchNo;
                                    }
                             
                                    //branch name below the user name in top right corener of the screen should show the default branch for the user
                                    var venueBranchItem = CommonContext.TblVenueBranches.Where(a => a.VenueBranchNo == VenueBranchNo).Select(x => new { x.Domaincode, x.VenueBranchName }).FirstOrDefault();
                                    result.VenueBranchName = venueBranchItem.VenueBranchName;
                                    result.ResponseStatus = 1;
                                    result.UserName = userresult.UserName;
                                    result.UserNo = userresult.UserNo;
                                    result.IsPriceShow = userresult.IsPriceShow;
                                    result.VenueNo = userresult.VenueNo;
                                    result.VenueBranchNo = VenueBranchNo;
                                    result.DomainCode = venueBranchItem?.Domaincode;
                                    result.IsSuperAdmin = userresult.IsSuperAdmin == null ? false : userresult.IsSuperAdmin;
                                    result.IsAdmin = userresult.IsAdmin == 0 ? false : true;
                                    result.IsProvisional = userresult.IsProvisional == null ? false : userresult.IsProvisional;
                                    result.IsEditLabResults = userresult.IsEditLabResults == null ? false : userresult.IsEditLabResults;
                                    result.IsResultEntryHIV = userresult.IsResultEntryHIV == null ? false : userresult.IsResultEntryHIV;
                                    result.IsResultEntryVIP = userresult.IsResultEntryVIP == null ? false : userresult.IsResultEntryVIP;
                                    result.lstUserRoleName = GetUserMenuCode(userresult.UserNo, userresult.VenueNo, userresult.VenueBranchNo, req.LoginType);
                                    result.isLock = userresult.isLock == null ? false : userresult.isLock;
                                    result.IsAbnormalAvail = userresult.IsAbnormalAvail == null ? false : userresult.IsAbnormalAvail;
                                    result.IsPOApproval = userresult.IsPOApproval == null ? false : userresult.IsPOApproval;
                                    result.IsGrnApproval = userresult.IsGrnApproval == null ? false : userresult.IsGrnApproval;
                                    result.IsGrnReturnApproval = userresult.IsGrnReturnApproval == null ? false : userresult.IsGrnReturnApproval;
                                    result.IsStockAdjustmentApproval = userresult.IsStockAdjustmentApproval == null ? false : userresult.IsStockAdjustmentApproval;
                                    result.IsConsumptionApproval = userresult.IsConsumptionApproval == null ? false : userresult.IsConsumptionApproval;
                                    result.IsClientApproval = userresult.IsClientApproval == null ? false : userresult.IsClientApproval;
                                    var token = _jWTManagerRepository.Authenticate(result);
                                    result.Token = token.Token;
                                    result.RefreshToken = token.RefreshToken;
                                    result.Gender = userresult.Gender == null ? "" : userresult.Gender;
                                    var userdata = context.TblUser.Where(user => user.UserNo == userresult.UserNo).FirstOrDefault();
                                    userdata.LoginAttempt = 0;
                                    userdata.RefreshToken = result.RefreshToken;
                                    userdata.RefreshTokenExpiryTime = token.RefreshTokenExpiryTime;
                                    context.Update(userdata);
                                    context.SaveChanges();
                                }
                                using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    TblUserSession objsessionitem = new TblUserSession();
                                    objsessionitem.UserNo = result.UserNo;
                                    objsessionitem.LoginType = req.LoginType;
                                    objsessionitem.LogInDateTime = DateTime.Now;
                                    objsessionitem.ClientSysteminfo = req.ClientSysteminfo;
                                    objsessionitem.Ipaddress = req.Ipaddress;
                                    objsessionitem.VenueNo = result.VenueNo;
                                    objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                    objsessionitem.Status = true;
                                    objsessionitem.IsClosed = false;
                                    objsessionitem.IsAdmin = result.IsAdmin;
                                    objsessionitem.IsSuperAdmin = result.IsSuperAdmin;
                                    objsessionitem.IsProvisional = result.IsProvisional;
                                    objsessionitem.IsEditLabResults = result.IsEditLabResults;
                                    objsessionitem.IsResultEntryHIV = result.IsResultEntryHIV;
                                    objsessionitem.IsResultEntryVIP = result.IsResultEntryVIP;
                                    objsessionitem.IsAbnormalAvail = result.IsAbnormalAvail;
                                    objsessionitem.isLock = result.isLock;
                                    objsessionitem.Token = result.Token;
                                    objsessionitem.IsPOApproval = result.IsPOApproval;
                                    objsessionitem.IsGrnApproval = result.IsGrnApproval;
                                    objsessionitem.IsGrnReturnApproval = result.IsGrnReturnApproval;
                                    objsessionitem.IsStockAdjustmentApproval = result.IsStockAdjustmentApproval;
                                    objsessionitem.IsConsumptionApproval = result.IsConsumptionApproval;
                                    objsessionitem.IsClientApproval = result.IsClientApproval;

                                    Sessioncontext.TblUserSession.Add(objsessionitem);
                                    Sessioncontext.SaveChanges();
                                }
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
                else if ((int)LoginType.PATIENT == req.LoginType)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblPatient.Where(a => a.PatientId + a.VenueNo == req.LoginName && a.Status == true).Select(x => new { x.PatientId, x.PatientNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.UserName = userresult.PatientId;
                                result.UserNo = userresult.PatientNo;
                                result.VenueNo = userresult.VenueNo;
                                result.VenueBranchNo = userresult.VenueBranchNo;
                                result.lstUserRoleName = GetUserMenuCode(userresult.PatientNo, userresult.VenueNo, userresult.VenueBranchNo, req.LoginType);

                                var token = _jWTManagerRepository.Authenticate(result);
                                result.Token = token.Token;
                                result.RefreshToken = token.RefreshToken;
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
                if ((int)LoginType.FRANCHISEE == req.LoginType)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblCustomer.Where(a => a.UserName == req.LoginName && a.Status == true && a.VenueBranchNo == req.VenueBranchNo && a.IsFranchisee == 1).Select(x => new { x.CustomerName, x.CustomerNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.UserName = userresult.CustomerName;
                                result.UserNo = userresult.CustomerNo;
                                result.VenueNo = userresult.VenueNo;
                                result.VenueBranchNo = userresult.VenueBranchNo;
                                result.lstUserRoleName = GetUserMenuCode(userresult.CustomerNo, userresult.VenueNo, userresult.VenueBranchNo, req.LoginType);
                                var token = _jWTManagerRepository.Authenticate(result);
                                result.Token = result.Token;
                                result.RefreshToken = token.RefreshToken;

                                using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    TblUserSession objsessionitem = new TblUserSession();
                                    objsessionitem.UserNo = result.UserNo;
                                    objsessionitem.LoginType = req.LoginType;
                                    objsessionitem.LogInDateTime = DateTime.Now;
                                    objsessionitem.ClientSysteminfo = req.ClientSysteminfo;
                                    objsessionitem.Ipaddress = req.Ipaddress;
                                    objsessionitem.VenueNo = result.VenueNo;
                                    objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                    objsessionitem.Status = true;
                                    objsessionitem.IsClosed = false;
                                    objsessionitem.IsAdmin = false;
                                    objsessionitem.IsSuperAdmin = false;
                                    objsessionitem.IsEditLabResults = false;
                                    objsessionitem.Token = result.Token;
                                    Sessioncontext.TblUserSession.Add(objsessionitem);
                                    Sessioncontext.SaveChanges();
                                }
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
                if ((int)LoginType.PHYSICIAN == req.LoginType)
                {

                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblPhysician.Where(a => a.physicianusername == req.LoginName && a.Status == true && a.VenueBranchNo == req.VenueBranchNo).Select(x => new { x.physicianusername, x.PhysicianNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();

                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.UserName = userresult.physicianusername;
                                result.UserNo = userresult.PhysicianNo;
                                result.VenueNo = (int)userresult.VenueNo;
                                result.VenueBranchNo = (int)userresult.VenueBranchNo;
                                result.lstUserRoleName = GetUserMenuCode(userresult.PhysicianNo, (int)userresult.VenueNo, (int)userresult.VenueBranchNo, req.LoginType);
                                var token = _jWTManagerRepository.Authenticate(result);
                                result.Token = token.Token;
                                result.RefreshToken = token.RefreshToken;

                                using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    TblUserSession objsessionitem = new TblUserSession();
                                    objsessionitem.UserNo = result.UserNo;
                                    objsessionitem.LoginType = req.LoginType;
                                    objsessionitem.LogInDateTime = DateTime.Now;
                                    objsessionitem.VenueNo = result.VenueNo;
                                    objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                    objsessionitem.Status = true;
                                    objsessionitem.IsClosed = false;
                                    objsessionitem.IsAdmin = false;
                                    objsessionitem.IsSuperAdmin = false;
                                    objsessionitem.IsEditLabResults = false;
                                    objsessionitem.Token = token.Token;
                                    objsessionitem.isLock = false;
                                    Sessioncontext.TblUserSession.Add(objsessionitem);
                                    Sessioncontext.SaveChanges();
                                }
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {

                            result.ResponseStatus = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.UserLogIn/LoginName - " + req.LoginName, ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public UserResponseEntity ValidateOTP(UserRequestEntity req, IJWTManagerRepository _jWTManagerRepository)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUser.Where(a => a.LoginName == req.LoginName && a.Status == true && a.IsLogin == true).Select(x => new { x.UserName, x.UserNo, x.Password, x.VenueBranchNo, x.VenueNo, x.IsSuperAdmin, x.IsAdmin, x.IsProvisional, x.IsEditLabResults, x.IsResultEntryHIV, x.IsResultEntryVIP, x.IsPriceShow, x.isLock, x.IsadmultifactorAccess, x.ladpsecretkey }).FirstOrDefault();
                    
                    if (userresult != null)
                    {
                        var totp = new Totp(Base32Encoding.ToBytes(userresult.ladpsecretkey));
                        var isVerified = totp.VerifyTotp(req.Password.ValidateEmpty(), out long timestepmatched);
                        
                        if (isVerified)
                        {
                            if ((bool)userresult.IsadmultifactorAccess)
                            {
                                using (var CommonContext = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                {
                                    int VenueBranchNo = 0;
                                    var Mappingitem = CommonContext.TblUserBranchMap.Where(a => a.UserNo == userresult.UserNo && a.Isdefault == true).Select(x => new { x.MappingBranchNo }).FirstOrDefault();
                                    if (Mappingitem != null)
                                    {
                                        VenueBranchNo = Mappingitem.MappingBranchNo;
                                    }
                                    else
                                    {
                                        VenueBranchNo = userresult.VenueBranchNo;
                                    }
                                    var venueBranchItem = CommonContext.TblVenueBranches.Where(a => a.VenueBranchNo == userresult.VenueBranchNo).Select(x => new { x.Domaincode }).FirstOrDefault();

                                    result.ResponseStatus = 1;
                                    result.UserName = userresult.UserName;
                                    result.UserNo = userresult.UserNo;
                                    result.IsPriceShow = userresult.IsPriceShow;
                                    result.VenueNo = userresult.VenueNo;
                                    result.VenueBranchNo = VenueBranchNo;
                                    result.DomainCode = venueBranchItem?.Domaincode;
                                    result.IsSuperAdmin = userresult.IsSuperAdmin == null ? false : userresult.IsSuperAdmin;
                                    result.IsAdmin = userresult.IsAdmin == 0 ? false : true;
                                    result.IsProvisional = userresult.IsProvisional == null ? false : userresult.IsProvisional;
                                    result.IsEditLabResults = userresult.IsEditLabResults == null ? false : userresult.IsEditLabResults;
                                    result.IsResultEntryHIV = userresult.IsResultEntryHIV == null ? false : userresult.IsResultEntryHIV;
                                    result.IsResultEntryVIP = userresult.IsResultEntryVIP == null ? false : userresult.IsResultEntryVIP;
                                    result.lstUserRoleName = GetUserMenuCode(userresult.UserNo, userresult.VenueNo, userresult.VenueBranchNo, req.LoginType);
                                    result.isLock = userresult.isLock == null ? false : userresult.isLock;
                                    var token = _jWTManagerRepository.Authenticate(result);
                                    result.Token = token.Token;
                                    result.RefreshToken = token.RefreshToken;
                                    var userdata = context.TblUser.Where(user => user.UserNo == userresult.UserNo).FirstOrDefault();
                                    userdata.LoginAttempt = 0;
                                    userdata.RefreshToken = result.RefreshToken;
                                    userdata.RefreshTokenExpiryTime = token.RefreshTokenExpiryTime;
                                    context.Update(userdata);
                                    context.SaveChanges();

                                    using (var Sessioncontext = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                                    {
                                        TblUserSession objsessionitem = new TblUserSession();
                                        objsessionitem.UserNo = result.UserNo;
                                        objsessionitem.LoginType = req.LoginType;
                                        objsessionitem.LogInDateTime = DateTime.Now;
                                        objsessionitem.ClientSysteminfo = req.ClientSysteminfo;
                                        objsessionitem.Ipaddress = req.Ipaddress;
                                        objsessionitem.VenueNo = result.VenueNo;
                                        objsessionitem.VenueBranchNo = result.VenueBranchNo;
                                        objsessionitem.Status = true;
                                        objsessionitem.IsClosed = false;
                                        objsessionitem.IsAdmin = result.IsAdmin;
                                        objsessionitem.IsSuperAdmin = result.IsSuperAdmin;
                                        objsessionitem.IsProvisional = result.IsProvisional;
                                        objsessionitem.IsEditLabResults = result.IsEditLabResults;
                                        objsessionitem.IsResultEntryHIV = result.IsResultEntryHIV;
                                        objsessionitem.IsResultEntryVIP = result.IsResultEntryVIP;
                                        objsessionitem.isLock = result.isLock;
                                        objsessionitem.Token = result.Token;
                                        Sessioncontext.TblUserSession.Add(objsessionitem);
                                        Sessioncontext.SaveChanges();
                                    }
                                }
                            }

                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ValidateOTP/LoginName-" + req.LoginName, ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public int ResetSecret(UserRequestEntity req)
        {
            int result = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUser.Where(a => a.LoginName == req.LoginName && a.Status == true && a.IsLogin == true).FirstOrDefault();
                    
                    if (userresult != null)
                    {
                        userresult.ladpsecretkey = "";
                        context.Update(userresult);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ResetSecret", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }     
        public int sendOTP(int userno, string phoneno) {

            int result = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUser.Where(a => a.UserNo == userno).FirstOrDefault();
                    
                    if (userresult != null)
                    {
                        string otptext = GenerateRandomNo().ToString();
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        string SMSURL = _IMasterRepository.GetSingleAppSetting("SMSURL").ConfigValue;
                        CommonHelper.SendSMS(SMSURL, otptext, phoneno);
                        userresult.otp = otptext;
                        userresult.PhoneNo = phoneno;
                        userresult.otpexpiry = DateTime.Now.AddMinutes(1);
                        context.Update(userresult);
                        context.SaveChanges();
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.sendOTP", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public int verifyOTP(int userno, string otptext)
        {
            int result = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUser.Where(a => a.UserNo == userno).FirstOrDefault();
                    if (userresult != null)
                    {
                        if (userresult.otp == otptext && userresult.otpexpiry > DateTime.Now)
                        {
                            userresult.LoginAttempt = 0;
                            userresult.ladpsecretkey = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
                            context.Update(userresult);
                            context.SaveChanges();
                            
                            if ((bool)userresult.IsadmultifactorAccess)
                            {
                                MasterRepository _IMasterRepository = new MasterRepository(_config);
                                string SMSURL = _IMasterRepository.GetSingleAppSetting("SMSURL").ConfigValue;
                                if (!string.IsNullOrEmpty(SMSURL) && !string.IsNullOrEmpty(userresult.PhoneNo))
                                {
                                    CommonHelper.SendSMS(SMSURL, userresult.ladpsecretkey, userresult.PhoneNo);
                                }
                            }
                            result = 1;
                        }
                        else {
                            result = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.verifyOTP", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public List<UserDetailsDTO> GetUserDetails(int VenueNo, int VenueBranchNo, int PageIndex,int DefaultBranchNo)
        {
            List<UserDetailsDTO> objresult = new List<UserDetailsDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", PageIndex);
                    var _DefaultBranchNo = new SqlParameter("DefaultBranchNo", DefaultBranchNo);
                    
                    objresult = context.UserDetailsEF.FromSqlRaw(
                    "Execute dbo.Pro_UserMasterDetails " +
                    "@VenueNo,@VenueBranchNo,@PageIndex,@DefaultBranchNo", 
                    _venueno, _venuebranchno, _PageIndex, _DefaultBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetUserDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public int InsertUserMaster(UserDetailsDTO Useritem)
        {
            int result = 0;
            try
            {
                string _CacheKey = CacheKeys.CommonMaster + "USER" + Useritem.VenueNo + Useritem.VenueBranchNo;
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _UserNo = new SqlParameter("UserNo", Useritem.UserNo);
                    var _UserName = new SqlParameter("UserName", Useritem.UserName);
                    var _LoginName = new SqlParameter("LoginName", Useritem.LoginName);
                    var _Address = new SqlParameter("Address", Useritem.Address.ValidateEmpty());
                    var _PinCode = new SqlParameter("PinCode", Useritem.PinCode.ValidateEmpty());
                    var _Email = new SqlParameter("Email", Useritem.Email.ValidateEmpty());
                    var _PhoneNo = new SqlParameter("PhoneNo", Useritem.PhoneNo.ValidateEmpty());
                    var _IsLogin = new SqlParameter("IsLogin", Useritem.IsLogin);
                    var _Discount = new SqlParameter("Discount", Useritem.Discount);
                    var _IsRider = new SqlParameter("IsRider", Useritem.IsRider);
                    var _IsMarketing = new SqlParameter("IsMarketing", Useritem.IsMarketing);
                    var _IsMobile = new SqlParameter("IsMobile", Useritem.IsMobile);
                    var _Status = new SqlParameter("Status", Useritem.Status);
                    var _Createdby = new SqlParameter("Createdby", Useritem.Createdby);
                    var _VenueNo = new SqlParameter("VenueNo", Useritem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Useritem.VenueBranchNo);
                    var _deptJson = new SqlParameter("deptJson", Useritem.deptJson);
                    var _patientEdit = new SqlParameter("PatientEdit", Useritem.EditPatient);
                    var _duePrint = new SqlParameter("DuePrint", Useritem.DueReport);
                    var _dashBoardJson = new SqlParameter("dashBoardJson", Useritem?.dashBoardDetailsJson);
                    var _branchJson = new SqlParameter("branchJson", Useritem?.branchJson);
                    var _roleId = new SqlParameter("roleId", Useritem?.roleId);
                    var _dob = new SqlParameter("dob", Useritem?.dob);
                    var _doj = new SqlParameter("doj", Useritem?.doj);
                    var _dor = new SqlParameter("dor", Useritem?.dor);
                    var _Gender = new SqlParameter("Gender", Useritem?.Gender.ValidateEmpty());
                    var _IsEditResults = new SqlParameter("IsEditResults", Useritem?.IsEditResults);
                    var _IsSuperAdmin = new SqlParameter("IsSuperAdmin", Useritem?.IsSuperAdmin);
                    var _IsResultsEntryVIP = new SqlParameter("IsResultsEntryVIP", Useritem?.IsResultsEntryVIP);
                    var _IsResultsEntryHIV = new SqlParameter("IsResultsEntryHIV", Useritem?.IsResultsEntryHIV);
                    var _IsPathologist = new SqlParameter("IsPathologist", Useritem?.IsPathologist);
                    var _isLock = new SqlParameter("isLock", Useritem?.isLock);
                    var _isPriceShow = new SqlParameter("isPriceShow", Useritem?.isPriceShow);
                    var _IsadmultifactorAccess = new SqlParameter("IsadmultifactorAccess", Useritem?.IsadmultifactorAccess);
                    var _Isadaccess = new SqlParameter("Isadaccess", Useritem?.Isadaccess);                 
                    var _IsEditGrn = new SqlParameter("IsEditGrn", Useritem?.IsEditGrn != null ? Useritem.IsEditGrn : false);
                    var _IsPOApproval = new SqlParameter("IsPOApproval", Useritem?.IsPOApproval != null ? Useritem.IsPOApproval : false);
                    var _IsGrnApproval = new SqlParameter("IsGrnApproval", Useritem?.IsGrnApproval != null ? Useritem.IsGrnApproval : false);
                    var _IsGrnReturnApproval = new SqlParameter("IsGrnReturnApproval", Useritem?.IsGrnReturnApproval != null ? Useritem.IsGrnReturnApproval : false);
                    var _IsStockTransferApproval = new SqlParameter("IsStockTransferApproval", Useritem?.IsStockTransferApproval != null ? Useritem.IsStockTransferApproval : false);
                    var _IsStockAdjustmentApproval = new SqlParameter("IsStockAdjustmentApproval", Useritem?.IsStockAdjustmentApproval != null ? Useritem.IsStockAdjustmentApproval : false);
                    var _IsConsumptionApproval = new SqlParameter("IsConsumptionApproval", Useritem?.IsConsumptionApproval != null ? Useritem.IsConsumptionApproval : false);
                    var _IsEditPO = new SqlParameter("IsEditPO", Useritem?.IsEditPO != null ? Useritem.IsEditPO : false);
                    var _IsEditGrnReturn = new SqlParameter("IsEditGrnReturn", Useritem?.IsEditGrnReturn != null ? Useritem.IsEditGrnReturn : false);
                    var _IsEditStockTransfer = new SqlParameter("IsEditStockTransfer", Useritem?.IsEditStockTransfer != null ? Useritem.IsEditStockTransfer : false);
                    var _IsEditStockAdjustment = new SqlParameter("IsEditStockAdjustment", Useritem?.IsEditStockAdjustment != null ? Useritem.IsEditStockAdjustment : false);
                    var _IsEditConsumption = new SqlParameter("IsEditConsumption", Useritem?.IsEditConsumption != null ? Useritem.IsEditConsumption : false);
                    var _IsApproveClient = new SqlParameter("IsApproveClient", Useritem?.IsApproveClient != null ? Useritem.IsApproveClient : false);
                    var _IsProvisional = new SqlParameter("IsProvisional", Useritem.IsProvisional == null ? false : Useritem.IsProvisional);
                    var _SuppressSCDtTm = new SqlParameter("SuppressSCDtTm", Useritem.SuppressSCDtTm == null ? false : Useritem.SuppressSCDtTm);


                    var resultdata = context.UserResponseEF.FromSqlRaw(
                    "Execute dbo.Pro_InsertUserMaster @UserNo,@UserName,@LoginName,@Address,@PinCode,@Discount,@Email,@PhoneNo,@IsLogin,@IsRider,@IsMarketing,@IsMobile" +
                    ",@Status,@VenueBranchNo,@VenueNo,@Createdby,@deptJson,@PatientEdit,@DuePrint,@dashBoardJson,@branchJson,@roleId,@dob,@doj,@dor,@Gender,@IsEditResults," +
                    "@IsSuperAdmin,@IsResultsEntryVIP,@IsResultsEntryHIV,@IsPathologist,@isLock,@isPriceShow,@IsadmultifactorAccess,@Isadaccess,@IsEditGrn,@IsPOApproval," +
                    "@IsGrnApproval,@IsGrnReturnApproval,@IsStockTransferApproval,@IsStockAdjustmentApproval,@IsConsumptionApproval,@IsEditPO,@IsEditGrnReturn,@IsEditStockTransfer," +
                    "@IsEditStockAdjustment,@IsEditConsumption, @IsApproveClient,@IsProvisional,@SuppressSCDtTm",
                    _UserNo, _UserName, _LoginName, _Address, _PinCode, _Discount, _Email, _PhoneNo, _IsLogin, _IsRider, _IsMarketing, _IsMobile, _Status,
                    _VenueBranchNo, _VenueNo, _Createdby, _deptJson, _patientEdit, _duePrint, _dashBoardJson, _branchJson, _roleId, _dob, _doj, _dor, _Gender, _IsEditResults,
                    _IsSuperAdmin, _IsResultsEntryVIP, _IsResultsEntryHIV, _IsPathologist, _isLock, _isPriceShow, _IsadmultifactorAccess, _Isadaccess, _IsEditGrn,_IsPOApproval, 
                    _IsGrnApproval,_IsGrnReturnApproval,_IsStockTransferApproval,_IsStockAdjustmentApproval,_IsConsumptionApproval, _IsEditPO, _IsEditGrnReturn, _IsEditStockTransfer, 
                    _IsEditStockAdjustment, _IsEditConsumption, _IsApproveClient,_IsProvisional,_SuppressSCDtTm).AsEnumerable().FirstOrDefault();

                    result = resultdata?.UserNo ?? 0;
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.InsertUserMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, Useritem.VenueNo, Useritem.VenueBranchNo, Useritem.UserNo);
            }
            return result;
        }
        public List<UserModuleDTO> GetUserMenuMapping(int VenueNo, int VenueBranchNo, int userno, int MenuLoadUserNo)
        {
            List<UserModuleDTO> objresult = new List<UserModuleDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _userno = new SqlParameter("userno", userno);
                    var _menuLoadUserNo = new SqlParameter("MenuLoadUserNo", MenuLoadUserNo);
                    
                    var menulst = context.UserMenuMappingEF.FromSqlRaw(
                    "Execute dbo.Pro_GetUserMenuMapping @VenueNo,@VenueBranchNo,@userno, @MenuLoadUserNo", 
                    _venueno, _venuebranchno, _userno, _menuLoadUserNo).ToList();

                    int oldModuleNo = 0;
                    int newModuleNo = 0;
                    int oldMenuNo = 0;
                    int newMenuNo = 0;
                    int oldTaskNo = 0;
                    int newTaskNo = 0;
                    objresult = new List<UserModuleDTO>();
                    
                    foreach (var obj in menulst)
                    {
                        UserModuleDTO UserModuleItem = new UserModuleDTO();
                        List<UserMenuDTO> lstUserMenuDTO = new List<UserMenuDTO>();
                        List<UserTaskDTO> lstUserTaskDTO = new List<UserTaskDTO>();
                        newModuleNo = obj.ModuleId;
                        
                        var MenuItem = menulst.Where(x => x.ModuleId == newModuleNo).Select(x => new { x.MenuId, x.MenuName, x.MenuStatus }).ToList();
                        
                        if (newModuleNo != oldModuleNo)
                        {
                            UserModuleItem.ModuleId = obj.ModuleId;
                            UserModuleItem.ModuleName = obj.ModuleName;
                            var Menustatus = menulst.Where(x => x.ModuleId == obj.ModuleId && x.MenuStatus == true).ToList();
                            
                            if (Menustatus.Count > 0)
                                UserModuleItem.Status = true;
                            
                            oldModuleNo = obj.ModuleId;
                            oldMenuNo = 0;
                            
                            foreach (var Mitem in MenuItem)
                            {
                                lstUserTaskDTO = new List<UserTaskDTO>();
                                newMenuNo = Mitem.MenuId;
                                
                                if (oldMenuNo != newMenuNo)
                                {
                                    UserMenuDTO ObjUserMenuDTO = new UserMenuDTO()
                                    {
                                        MenuId = Mitem.MenuId,
                                        MenuName = Mitem.MenuName,
                                        Status = Mitem.MenuStatus,
                                        TaskItem = lstUserTaskDTO
                                    };
                                    oldMenuNo = newMenuNo;
                                    lstUserMenuDTO.Add(ObjUserMenuDTO);

                                    var TaskItem = menulst.Where(x => x.ModuleId == newModuleNo && x.MenuId == newMenuNo)
                                        .Select(x => new { x.TaskMasterid, x.TaskName, x.TaskStatus }).ToList();
                                    oldTaskNo = 0;
                                    foreach (var item in TaskItem)
                                    {
                                        newTaskNo = item.TaskMasterid;
                                        if (oldTaskNo != newTaskNo)
                                        {
                                            UserTaskDTO UserDTO = new UserTaskDTO()
                                            {
                                                TaskNo = item.TaskMasterid,
                                                TaskName = item.TaskName,
                                                Status = item.TaskStatus
                                            };
                                            oldTaskNo = newTaskNo;
                                            lstUserTaskDTO.Add(UserDTO);
                                        }
                                    }
                                    UserModuleItem.UserMenuItem = lstUserMenuDTO;
                                }
                            }

                            objresult.Add(UserModuleItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetUserMenuMapping", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }
        public List<UserMenuDTO> GetUserTask(int VenueNo, int VenueBranchNo, int userno)
        {
            List<UserMenuDTO> objresult = new List<UserMenuDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _userno = new SqlParameter("userno", userno);
                    var menulst = context.UserMenuTaskEF.FromSqlRaw("Execute dbo.Pro_GetUserPageTask @VenueNo,@VenueBranchNo,@userno", _venueno, _venuebranchno, _userno).ToList();

                    string oldMenuCode = string.Empty;
                    string newMenuCode = string.Empty;
                    string oldTaskcode = string.Empty;
                    string newTaskcode = string.Empty;
                    objresult = new List<UserMenuDTO>();
                    
                    foreach (var Mitem in menulst)
                    {
                        List<UserMenuDTO> lstUserMenuDTO = new List<UserMenuDTO>();
                        List<UserTaskDTO> lstUserTaskDTO = new List<UserTaskDTO>();

                        lstUserTaskDTO = new List<UserTaskDTO>();
                        newMenuCode = Mitem.MenuCode;
                        
                        if (oldMenuCode != newMenuCode)
                        {
                            UserMenuDTO ObjUserMenuDTO = new UserMenuDTO()
                            {
                                MenuCode = Mitem.MenuCode,
                                TaskItem = lstUserTaskDTO
                            };
                            oldMenuCode = newMenuCode;
                            lstUserMenuDTO.Add(ObjUserMenuDTO);

                            var TaskItem = menulst.Where(x => x.MenuCode == newMenuCode)
                                .Select(x => new { x.Taskcode, x.TaskName, x.TaskStatus }).ToList();
                            oldTaskcode = string.Empty;
                            foreach (var item in TaskItem)
                            {
                                newTaskcode = item.Taskcode;
                                if (oldTaskcode != newTaskcode)
                                {
                                    UserTaskDTO UserDTO = new UserTaskDTO()
                                    {
                                        TaskCode = item.Taskcode,
                                        TaskName = item.TaskName,
                                        Status = item.TaskStatus
                                    };
                                    oldTaskcode = newTaskcode;
                                    lstUserTaskDTO.Add(UserDTO);
                                }
                            }
                            objresult.Add(ObjUserMenuDTO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetUserTask", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, userno);
            }
            return objresult;
        }
        public int InsertMenuMapping(ReqUserMenu Useritem)
        {
            int result = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string strXML = commonUtility.ToXML(Useritem.usermenuitem);
                string _CacheKey = CacheKeys.UserMenu + Useritem.userNo + Useritem.venueNo + Useritem.venueBranchNo + "2";
                
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MenuXML = new SqlParameter("MenuXML", strXML);
                    var _UserNo = new SqlParameter("UserNo", Useritem.userNo);
                    var _CreatedBy = new SqlParameter("CreatedBy", Useritem.createdBy);
                    var _VenueNo = new SqlParameter("VenueNo", Useritem.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Useritem.venueBranchNo);
                    var _MenuUserNo = new SqlParameter("MenuUserNo", Useritem.MenuUserNo);

                    var resultdata = context.UserInsertMenuEF.FromSqlRaw(
                    "Execute dbo.pro_InsertMenuMapping @MenuXML,@UserNo,@CreatedBy,@VenueBranchNo,@VenueNo,@MenuUserNo",
                    _MenuXML, _UserNo, _CreatedBy, _VenueBranchNo, _VenueNo, _MenuUserNo).AsEnumerable().FirstOrDefault();
                    
                    result = resultdata?.UserNo ?? 0;
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.InsertMenuMapping", ExceptionPriority.High, ApplicationType.REPOSITORY, Useritem.venueNo, Useritem.venueBranchNo, Useritem.userNo);
            }
            return result;
        }
        public List<NavDTO> GetPageMenuList(int VenueNo, int VenueBranchNo, int userno, int logintype)
        {
            List<NavDTO> objresult = new List<NavDTO>();
            try
            {
                string _CacheKey = CacheKeys.UserMenu + userno + VenueNo + VenueBranchNo + logintype;
                objresult = MemoryCacheRepository.GetCacheItem<List<NavDTO>>(_CacheKey);
                
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("VenueNo", VenueNo);
                        var _venuebranchno = new SqlParameter("VenueBranchNo", VenueBranchNo);
                        var _userno = new SqlParameter("userno", userno);
                        var _logintype = new SqlParameter("logintype", logintype);
                        var menulst = context.UserNavEF.FromSqlRaw("Execute dbo.Pro_GetUserMenu @VenueNo,@VenueBranchNo,@userno,@logintype", _venueno, _venuebranchno, _userno, _logintype).ToList();

                        int oldModuleNo = 0;
                        int newModuleNo = 0;
                        int oldMenuNo = 0;
                        int newMenuNo = 0;
                        objresult = new List<NavDTO>();
                        
                        foreach (var obj in menulst)
                        {
                            NavDTO UserModuleItem = new NavDTO();
                            List<MenuChildDTO> lstUserMenuDTO = new List<MenuChildDTO>();
                            newModuleNo = obj.ModuleId;
                            var MenuItem = menulst.Where(x => x.ModuleId == newModuleNo).Select(x => new { x.MenuNo, x.MenuName, x.MenuURL, x.MenuIcon, x.MenuCode }).ToList();
                            
                            if (newModuleNo != oldModuleNo)
                            {
                                UserModuleItem.Name = obj.ModuleName;
                                UserModuleItem.Icon = obj.ModuleIcon;
                                UserModuleItem.Url = obj.ModuleURL;
                                UserModuleItem.Code = obj.MenuCode;
                                oldModuleNo = obj.ModuleId;
                                oldMenuNo = 0;
                                
                                foreach (var Mitem in MenuItem)
                                {
                                    newMenuNo = Mitem.MenuNo;
                                    if (oldMenuNo != newMenuNo)
                                    {
                                        MenuChildDTO ObjUserMenuDTO = new MenuChildDTO()
                                        {
                                            Name = Mitem.MenuName,
                                            Icon = Mitem.MenuIcon,
                                            Url = Mitem.MenuURL,
                                            Code = Mitem.MenuCode
                                        };
                                        oldMenuNo = newMenuNo;
                                        lstUserMenuDTO.Add(ObjUserMenuDTO);
                                        UserModuleItem.Children = lstUserMenuDTO;
                                    }
                                }
                                objresult.Add(UserModuleItem);
                                //
                                MasterRepository _IMasterRepository = new MasterRepository(_config);
                                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                                objAppSettingResponse = new AppSettingResponse();
                                string AppCacheMemoryTime = "CacheMemoryTime";
                                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                                int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                    ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;

                                MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                            }
                        }   











                           


                           
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetPageMenuList", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public int ChangePassword(ChangePasswordEntity req)
        {
            int result = 0;
            try
            {
                if ((int)LoginType.CUSTOMER == req.usertype || (int)LoginType.FRANCHISEE == req.usertype)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblCustomer.Where(a => a.CustomerNo == req.UserNo && a.Status == true).Select(x => new { x.CustomerName, x.CustomerNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();
                        
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.oldPassword, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                var newPassword = CommonSecurity.EncodePassword(req.newPassword, CommonSecurity.GeneratePassword(1));
                                var _Passowrd = new SqlParameter("Password", newPassword);
                                var _UserNo = new SqlParameter("Userno", req.UserNo);
                                var _VenueNo = new SqlParameter("VenueNo", req.venueNo);
                                var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                                var _usertype = new SqlParameter("usertype", req.usertype);
                                var _changeUserNo = new SqlParameter("ChangeUserNo", req.changeUserNo);

                                var resultdata = context.tblUserPassEF.FromSqlRaw(
                                "Execute dbo.Pro_UpdatePassword @VenueBranchNo,@VenueNo,@Userno,@Password,@usertype,@ChangeUserNo", 
                                _VenueBranchNo, _VenueNo, _UserNo, _Passowrd, _usertype, _changeUserNo).AsEnumerable().FirstOrDefault();
                                
                                result = 1;
                            }
                            else
                            {
                                result = -1;
                            }
                        }
                        else
                        {
                            result = -2;
                        }
                    }
                }
                else if ((int)LoginType.LABORATORY == req.usertype)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblUser.Where(a => a.UserNo == req.UserNo && a.Status == true && a.IsLogin == true).Select(x => new { x.UserName, x.UserNo, x.Password, x.VenueBranchNo, x.VenueNo }).FirstOrDefault();
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.oldPassword, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                var newPassword = CommonSecurity.EncodePassword(req.newPassword, CommonSecurity.GeneratePassword(1));
                                var _Passowrd = new SqlParameter("Password", newPassword);
                                var _UserNo = new SqlParameter("Userno", req.UserNo);
                                var _VenueNo = new SqlParameter("VenueNo", req.venueNo);
                                var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                                var _usertype = new SqlParameter("usertype", req.usertype);

                                var resultdata = context.tblUserPassEF.FromSqlRaw(
                                "Execute dbo.Pro_UpdatePassword @VenueBranchNo,@VenueNo,@Userno,@Password,@usertype", 
                                _VenueBranchNo, _VenueNo, _UserNo, _Passowrd, _usertype).AsEnumerable().FirstOrDefault();
                                
                                result = 1;
                            }
                            else
                            {
                                result = -1;
                            }
                        }
                        else
                        {
                            result = -2;
                        }
                    }
                }
                else if ((int)LoginType.PATIENT == req.usertype)
                {
                    using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblPatient.Where(a => a.PatientNo == req.UserNo).FirstOrDefault();
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.oldPassword, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                var newPassword = CommonSecurity.EncodePassword(req.newPassword, CommonSecurity.GeneratePassword(1));
                                var _Passowrd = new SqlParameter("Password", newPassword);
                                var _UserNo = new SqlParameter("Userno", req.UserNo);
                                var _VenueNo = new SqlParameter("VenueNo", req.venueNo);
                                var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                                var _usertype = new SqlParameter("usertype", req.usertype);
                                var _changeUserNo = new SqlParameter("ChangeUserNo", req.changeUserNo);

                                var resultdata = context.tblUserPassEF.FromSqlRaw(
                                "Execute dbo.Pro_UpdatePassword @VenueBranchNo,@VenueNo,@Userno,@Password,@usertype,@ChangeUserNo", 
                                _VenueBranchNo, _VenueNo, _UserNo, _Passowrd, _usertype, _changeUserNo).AsEnumerable().FirstOrDefault();
                                
                                result = 1;
                            }
                            else
                            {
                                result = -1;
                            }
                        }
                        else
                        {
                            result = -2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ChangePassword", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.UserNo);
            }
            return result;
        }
        public int ResetPassword(int UserNo, int venueNo, int VenueBranchNo, int usertype, int ResetUserNo)
        {
            int result = 0;
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _UserNo = new SqlParameter("Userno", UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _usertype = new SqlParameter("usertype", usertype);
                    var _ResetUserNo = new SqlParameter("ResetUserNo", ResetUserNo);

                    var resultdata = context.tblUserPassEF.FromSqlRaw(
                    "Execute dbo.Pro_ResetPassword @VenueBranchNo,@VenueNo,@Userno,@usertype, @ResetUserNo",
                    _VenueBranchNo, _VenueNo, _UserNo, _usertype, _ResetUserNo).AsEnumerable().FirstOrDefault();
                    
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ResetPassword", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, VenueBranchNo, UserNo);
            }
            return result;
        }
        public List<UserDashBoardMasterResponse> GetDashBoardMaster(int CustomerNo, int VenueNo, int VenueBranchNo)
        {
            List<UserDashBoardMasterResponse> result = new List<UserDashBoardMasterResponse>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", CustomerNo);
                 
                    result = context.UserDashBoardMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetDashBoardMaster @VenueNo,@VenueBranchNo,@CustomerNo",
                    _VenueNo, _VenueBranchNo, _CustomerNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetDashBoardMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public List<userbranchlist> GetUserBranchList(int userno, int VenueNo, int VenueBranchNo)
        {
            List<userbranchlist> result = new List<userbranchlist>();
            try
            {
                using (var context = new FrontOfficeContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _userno = new SqlParameter("userno", userno);

                    result = context.userbranchlistDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetUserBranchMapping @VenueNo,@VenueBranchNo,@userno",
                    _VenueNo, _VenueBranchNo, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetUserBranchList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public bool ValidateUserSession(int userno, int VenueNo, int VenueBranchNo, string token)
        {
            bool result = false;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var userresult = context.TblUserSession.Where(a => a.UserNo == userno && a.VenueNo == VenueNo && a.Token == token && a.IsClosed == false).AsEnumerable().ToList();
                    if (userresult.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ValidateUserSession", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public void UpdateSession(int userno, int VenueNo, int VenueBranchNo, string token)
        {
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _UserNo = new SqlParameter("UserNo", userno);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _loginType = new SqlParameter("LoginType", 2);
                    var _token = new SqlParameter("token", token);
                    
                    var result = context.usersesssionUpdate.FromSqlRaw(
                    "Execute dbo.Pro_UpdateSessionStatus @UserNo, @VenueNo, @VenueBranchNo, @LoginType, @token",
                    _UserNo, _VenueNo, _VenueBranchNo, _loginType, _token).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ValidateUserSession", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
        }
        public List<UserModuleDTO> GetRoleMenuMapping(RolegetReqDTO rolereq)
        {
            List<UserModuleDTO> objresult = new List<UserModuleDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", rolereq.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", rolereq.VenueBranchNo);
                    var _RoleId = new SqlParameter("RoleId", rolereq.RoleId);
                    
                    var menulst = context.RoleMenuMappingEF.FromSqlRaw("Execute dbo.Pro_GetRoleMenuMapping @VenueNo, @VenueBranchNo, @RoleId", _venueno, _venuebranchno, _RoleId).ToList();

                    int oldModuleNo = 0;
                    int newModuleNo = 0;
                    int oldMenuNo = 0;
                    int newMenuNo = 0;
                    int oldTaskNo = 0;
                    int newTaskNo = 0;
                    objresult = new List<UserModuleDTO>();

                    foreach (var obj in menulst)
                    {
                        UserModuleDTO UserModuleItem = new UserModuleDTO();
                        List<UserMenuDTO> lstUserMenuDTO = new List<UserMenuDTO>();
                        List<UserTaskDTO> lstUserTaskDTO = new List<UserTaskDTO>();
                        newModuleNo = obj.ModuleId;
                        var MenuItem = menulst.Where(x => x.ModuleId == newModuleNo).Select(x => new { x.MenuId, x.MenuName, x.MenuStatus }).ToList();
                        
                        if (newModuleNo != oldModuleNo)
                        {
                            UserModuleItem.ModuleId = obj.ModuleId;
                            UserModuleItem.ModuleName = obj.ModuleName;
                            var Menustatus = menulst.Where(x => x.ModuleId == obj.ModuleId && x.MenuStatus == true).ToList();
                            
                            if (Menustatus.Count > 0)
                                UserModuleItem.Status = true;
                            
                            oldModuleNo = obj.ModuleId;
                            oldMenuNo = 0;
                            
                            foreach (var Mitem in MenuItem)
                            {
                                lstUserTaskDTO = new List<UserTaskDTO>();
                                newMenuNo = Mitem.MenuId;
                                if (oldMenuNo != newMenuNo)
                                {
                                    UserMenuDTO ObjUserMenuDTO = new UserMenuDTO()
                                    {
                                        MenuId = Mitem.MenuId,
                                        MenuName = Mitem.MenuName,
                                        Status = Mitem.MenuStatus,
                                        TaskItem = lstUserTaskDTO
                                    };
                                    oldMenuNo = newMenuNo;
                                    lstUserMenuDTO.Add(ObjUserMenuDTO);

                                    var TaskItem = menulst.Where(x => x.ModuleId == newModuleNo && x.MenuId == newMenuNo)
                                        .Select(x => new { x.TaskMasterid, x.TaskName, x.TaskStatus }).ToList();
                                    oldTaskNo = 0;
                                    
                                    foreach (var item in TaskItem)
                                    {
                                        newTaskNo = item.TaskMasterid;
                                        if (oldTaskNo != newTaskNo)
                                        {
                                            UserTaskDTO UserDTO = new UserTaskDTO()
                                            {
                                                TaskNo = item.TaskMasterid,
                                                TaskName = item.TaskName,
                                                Status = item.TaskStatus
                                            };
                                            oldTaskNo = newTaskNo;
                                            lstUserTaskDTO.Add(UserDTO);
                                        }
                                    }
                                    UserModuleItem.UserMenuItem = lstUserMenuDTO;
                                }
                            }

                            objresult.Add(UserModuleItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetRoleMenuMapping", ExceptionPriority.High, ApplicationType.REPOSITORY, rolereq.VenueNo, rolereq.VenueBranchNo, 0);
            }
            return objresult;
        }
        public int InsertRoleMenuMapping(ReqRoleMenu Useritem)
        {
            int result = 0;
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string strXML = commonUtility.ToXML(Useritem.usermenuitem);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MenuXML = new SqlParameter("MenuXML", strXML);
                    var _CreatedBy = new SqlParameter("CreatedBy", Useritem.createdBy);
                    var _VenueNo = new SqlParameter("VenueNo", Useritem.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Useritem.venueBranchNo);
                    var _RoleId = new SqlParameter("RoleId", Useritem.RoleId);
                    
                    var resultdata = context.RoleInsertMenuEF.FromSqlRaw(
                    "Execute dbo.pro_InsertRoleMenuMapping @MenuXML,@CreatedBy,@VenueBranchNo,@VenueNo,@RoleId", 
                    _MenuXML, _CreatedBy, _VenueBranchNo, _VenueNo, _RoleId).ToList();
                    
                    result = resultdata[0].RoleId;
                    
                    foreach (var obj in resultdata)
                    {
                        if (obj.UserNo > 0)
                        {
                            string _CacheKey = CacheKeys.UserMenu + obj.UserNo + Useritem.venueNo + Useritem.venueBranchNo + "2";
                            MemoryCacheRepository.RemoveItem(_CacheKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.InsertRoleMenuMapping", ExceptionPriority.High, ApplicationType.REPOSITORY, Useritem.venueNo, Useritem.venueBranchNo, 0);
            }
            return result;
        }
        public int ValidateActionMenu(int UserNo, int VenueNo, string MenuType)
        {
            int result = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _UserNo = new SqlParameter("UserNo", UserNo);
                    var _MenuType = new SqlParameter("MenuType", MenuType);

                    var resultdata = context.GetActionMenuEF.FromSqlRaw(
                    "Execute dbo.pro_CheckUserMenuAvailable @VenueNo,@UserNo,@MenuType",
                    _VenueNo, _UserNo, _MenuType).AsEnumerable().FirstOrDefault();

                    result = resultdata?.Result ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ValidateActionMenu", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, 0, UserNo);
            }
            return result;
        }

        public bool ValidateUserMenuCode(int UserNo, int VenueNo, int VenueBranchNo, string menuCode)
        {
            return true;
        }

        public List<UserRoleNameDTO> GetUserMenuCode(int UserNo, int VenueNo, int VenueBranchNo, int LoginType)
        {
            List<UserRoleNameDTO> objResult = new List<UserRoleNameDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _UserNo = new SqlParameter("UserNo", UserNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _LoginType = new SqlParameter("LoginType", LoginType);

                    objResult = context.UserRoleNameDTOs.FromSqlRaw(
                    "Execute dbo.pro_GetUserRoleNames @VenueNo,@VenueBranchNo, @UserNo, @LoginType",
                    _VenueNo, _VenueBranchNo, _UserNo, _LoginType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.ValidateActionMenu", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, 0, UserNo);
            }
            return objResult;
        }
        public InsertRoleRes InsertRoleMaster(InsertRoleReq req)
        {
            InsertRoleRes result = new InsertRoleRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RoleId = new SqlParameter("RoleId", req.RoleId);
                    var _RoleName = new SqlParameter("RoleName", req.RoleName);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _Status = new SqlParameter("Status", req.Status);
                    var _UserNo = new SqlParameter("UserNo", req.UserNo);

                    result = context.InsertRoleMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertRoleMaster @RoleId,@RoleName,@VenueNo,@VenueBranchNo,@Status,@UserNo",
                    _RoleId, _RoleName, _VenueNo, _VenueBranchNo, _Status, _UserNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.InsertRoleMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }
        public List<GetRoleRes> GetRoleMaster(GetRoleReq req)
        {
            List<GetRoleRes> objResult = new List<GetRoleRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RoleId = new SqlParameter("RoleId", req.RoleId);
                    var _VenueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", req.pageIndex);

                    objResult = context.GetRoleMaster.FromSqlRaw(
                    "Execute dbo.pro_GetRoleMaster @RoleId,@VenueNo, @pageIndex",
                    _RoleId, _VenueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetRoleMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.RoleId, 0);
            }
            return objResult;
        }
        public UserResponseEntity UserESign(UserRequestEntity req)
        {
            UserResponseEntity result = new UserResponseEntity();
            try
            {
                if ((int)LoginType.LABORATORY == req.LoginType)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var userresult = context.TblUser.Where(a => a.LoginName == req.LoginName && a.Status == true && a.IsLogin == true).Select(x => new { x.UserName, x.UserNo, x.Password, x.VenueBranchNo, x.VenueNo, x.IsSuperAdmin, x.IsAdmin, x.IsProvisional, x.IsEditLabResults, x.IsResultEntryHIV, x.IsResultEntryVIP, x.IsPriceShow, x.isLock, x.IsadmultifactorAccess, x.ladpsecretkey, x.PhoneNo, x.Isadaccess }).FirstOrDefault();
                        if (userresult != null)
                        {
                            var encodingPassword = CommonSecurity.EncodePassword(req.Password, CommonSecurity.GeneratePassword(1));
                            if (userresult.Password == encodingPassword)
                            {
                                result.ResponseStatus = 1;
                                result.VenueNo = userresult.VenueNo;
                                result.VenueBranchNo = userresult.VenueBranchNo;
                                result.UserNo = userresult.UserNo;
                                result.UserName = userresult.UserName;
                            }
                            else
                            {
                                result.ResponseStatus = -1;
                            }
                        }
                        else
                        {
                            result.ResponseStatus = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.UserESign", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
        public List<GetUserDepartmentDTO> GetUserDepartment(UserDepartmentDTO deptreq)
        {
            List<GetUserDepartmentDTO> result = new List<GetUserDepartmentDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", deptreq?.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", deptreq?.venueBranchNo);
                    var _userNo = new SqlParameter("userNo", deptreq?.userNo);

                    result = context.GetUserDepartment.FromSqlRaw(
                    "Execute dbo.pro_GetUserDepartment @venueNo,@venueBranchNo,@userNo",
                    _venueNo, _venueBranchNo, _userNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserRepository.GetUserDepartment", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return result;
        }
    }
}
