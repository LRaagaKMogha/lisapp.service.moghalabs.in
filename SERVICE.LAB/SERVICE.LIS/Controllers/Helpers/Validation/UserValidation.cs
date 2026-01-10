using DEV.Model;
using DEV.Model.Common;
using DEV.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Linq;

namespace DEV.API.SERVICE.Controllers
{
    public static class UserValidation
    {
        // Change Password //
        public static ErrorResponse ChangePassword(ChangePasswordEntity _request)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if (string.IsNullOrEmpty(_request.oldPassword))
                errors.Add("Oldpassword is required");
            if (string.IsNullOrEmpty(_request.newPassword))
                errors.Add("NewPasssword is required");
            Regex re = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{12,})");
            if (!re.IsMatch(_request.newPassword))
                errors.Add("password must be a minimum of 12 characters including number, Upper, Lower And one special character");
            if (_csvcheck.IsMatch(_request.oldPassword.ToSubstring(_request.oldPassword == null ? 0 : _request.oldPassword.Length)) || _csvcheck.IsMatch(_request.newPassword.ToSubstring(_request.newPassword == null ? 0 : _request.newPassword.Length)))
                errors.Add("Special character not allowed");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Critical Result Notify //
        public static ErrorResponse SaveCriticalResultNotify(SaveCriticalResultsReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.lstSaveData != null && req.lstSaveData.Count > 0)
            {
                foreach (var item in req.lstSaveData)
                {
                    if (string.IsNullOrEmpty(item.comments?.Trim()))
                    {
                        errors.Add("Please enter the comments");
                        break;
                    }
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // User Master //
        public static ErrorResponse InsertUserMaster(UserDetailsDTO Useritem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if (string.IsNullOrEmpty(Useritem.UserName) || Useritem.UserName.TrimStart() == string.Empty)
                errors.Add("UserName is required");
            if (string.IsNullOrEmpty(Useritem.LoginName) || Useritem.LoginName.TrimStart() == string.Empty)
                errors.Add("LoginName is Required");

            // Validate Email only if provided
            if (!string.IsNullOrEmpty(Useritem.Email) && !_emailCheck.IsMatch(Useritem.Email))
                errors.Add("Please provide a valid email address");

            if (!(Useritem.Discount >= 0 && Useritem.Discount <= 100))
                errors.Add("The discount value is not within the range of 0 to 100");

            if (_csvcheck.IsMatch(Useritem.UserName.ToSubstring(Useritem.UserName == null ? 0 : Useritem.UserName.Length)) || _csvcheck.IsMatch(Useritem.LoginName.ToSubstring(Useritem.LoginName == null ? 0 : Useritem.LoginName.Length)) ||
                _csvcheck.IsMatch(Useritem.Email.ToSubstring(Useritem.Email == null ? 0 : Useritem.Email.Length)) || _csvcheck.IsMatch(Useritem.PhoneNo.ToSubstring(Useritem.PhoneNo == null ? 0 : Useritem.PhoneNo.Length)) ||
                _csvcheck.IsMatch(Useritem.Address.ToSubstring(Useritem.Address == null ? 0 : Useritem.Address.Length)) || _csvcheck.IsMatch(Useritem.roleName.ToSubstring(Useritem.roleName == null ? 0 : Useritem.roleName.Length)))
                errors.Add("Special character not allowed");

            List<Branch> branches = JsonConvert.DeserializeObject<List<Branch>>(Useritem.branchJson);
            var sd = branches.Where(x => x.IsChecked == true && x.Isdefault == true).ToList();
            if (sd.Count == 0)
            {
                errors.Add("Please check Branchname & ensure either IsChecked and Isdefault are set to true");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Menu Mapping //
        public static ErrorResponse InsertMenuMapping(ReqUserMenu req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.MenuUserNo == 0)
                errors.Add("Please select User Name");
            if (req.usermenuitem == null || req.usermenuitem.Count <= 0)
                errors.Add("Atlest one menu is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Role Menu Mapping //
        public static ErrorResponse InsertRoleMenuMapping(ReqRoleMenu Useritem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (Useritem.RoleId == 0)
                errors.Add("Please select Role");
            if (Useritem.usermenuitem == null || Useritem.usermenuitem.Count <= 0)
                errors.Add("Atlest one menu is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Role Master //
        public static ErrorResponse InsertRoleMaster(InsertRoleReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.RoleName) || req.RoleName.TrimStart() == string.Empty)
                errors.Add("RoleName is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}