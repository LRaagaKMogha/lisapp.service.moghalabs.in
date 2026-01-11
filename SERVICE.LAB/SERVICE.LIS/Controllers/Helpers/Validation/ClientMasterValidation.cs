using Service.Model;
using Service.Model.Common;
using DEV.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Linq;

namespace DEV.API.SERVICE.Controllers
{
    public class ClientMasterValidation
    {
        // Client Master //
        public static ErrorResponse InsertClientMasterDetails(PostCustomerMaster req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _mobileNoCheck = new Regex(@"^[0-9]+$");
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if(req.tblcustomer.IsApproval == 0)
            {
                if (string.IsNullOrEmpty(req.tblcustomer.CustomerName) || req.tblcustomer.CustomerName.TrimStart() == string.Empty)
                    errors.Add("CustomerName is required");
                //if (string.IsNullOrEmpty(req.tblcustomer.hcicode) || req.tblcustomer.hcicode.TrimStart() == string.Empty)
                //    errors.Add("HCI Code is required");
                if (req.tblcustomer.CustomerType == 0)
                    errors.Add("CustomerType is required");
                if (string.IsNullOrEmpty(req.tblcustomer.UserName) || req.tblcustomer.UserName.TrimStart() == string.Empty)
                    errors.Add("UserName is required");
                if ((!string.IsNullOrEmpty(req.tblcustomer.CustomerEmail)))
                {
                    if (!_emailCheck.IsMatch(req.tblcustomer.CustomerEmail))
                        errors.Add("Please provide a valid email address");
                }
                if (_csvcheck.IsMatch(req.tblcustomer.CustomerName.ToSubstring(req.tblcustomer.CustomerName == null ? 0 : req.tblcustomer.CustomerName.Length)) || _csvcheck.IsMatch(req.tblcustomer.CShortName.ToSubstring(req.tblcustomer.CShortName == null ? 0 : req.tblcustomer.CShortName.Length)) ||
                    _csvcheck.IsMatch(req.tblcustomer.CustomerCode.ToSubstring(req.tblcustomer.CustomerCode == null ? 0 : req.tblcustomer.CustomerCode.Length)) || _csvcheck.IsMatch(req.tblcustomer.ContactPersonName.ToSubstring(req.tblcustomer.ContactPersonName == null ? 0 : req.tblcustomer.ContactPersonName.Length)) ||
                    _csvcheck.IsMatch(req.tblcustomer.CustomerMobileNo.ToSubstring(req.tblcustomer.CustomerMobileNo == null ? 0 : req.tblcustomer.CustomerMobileNo.Length)) || _csvcheck.IsMatch(req.tblcustomer.CustomerEmail.ToSubstring(req.tblcustomer.CustomerEmail == null ? 0 : req.tblcustomer.CustomerEmail.Length)) ||
                    _csvcheck.IsMatch(req.tblcustomer.UserName.ToSubstring(req.tblcustomer.UserName == null ? 0 : req.tblcustomer.UserName.Length)) || _csvcheck.IsMatch(req.tblcustomer.Address.ToSubstring(req.tblcustomer.Address == null ? 0 : req.tblcustomer.Address.Length)) || _csvcheck.IsMatch(req.tblcustomer.Area.ToSubstring(req.tblcustomer.Area == null ? 0 : req.tblcustomer.Area.Length)) ||
                     _csvcheck.IsMatch(req.tblcustomer.Pincode.ToSubstring(req.tblcustomer.Pincode == null ? 0 : req.tblcustomer.Pincode.Length)) || _csvcheck.IsMatch(req.tblcustomer.secondaryemail.ToSubstring(req.tblcustomer.secondaryemail == null ? 0 : req.tblcustomer.secondaryemail.Length)))
                    errors.Add("Special character not allowed");
            }

            // Client Master Approval without Reject //
            else if(req.tblcustomer.IsApproval == 1 && req.tblcustomer.IsReject == false)
            {
                if (string.IsNullOrEmpty(req.tblcustomer.CustomerName) || req.tblcustomer.CustomerName.TrimStart() == string.Empty)
                    errors.Add("CustomerName is required");
                if (req.tblcustomer.CustomerType == 0)
                    errors.Add("CustomerType is required");
                if (string.IsNullOrEmpty(req.tblcustomer.UserName) || req.tblcustomer.UserName.TrimStart() == string.Empty)
                    errors.Add("UserName is required");
                if ((!string.IsNullOrEmpty(req.tblcustomer.CustomerEmail)))
                {
                    if (!_emailCheck.IsMatch(req.tblcustomer.CustomerEmail))
                        errors.Add("Please provide a valid email address");
                }
            }

            // Client Master Approval with Reject //
            else if (req.tblcustomer.IsApproval == 1 && req.tblcustomer.IsReject == true)
            {
                if (string.IsNullOrEmpty(req.tblcustomer.CustomerName) || req.tblcustomer.CustomerName.TrimStart() == string.Empty)
                    errors.Add("CustomerName is required");
                if (req.tblcustomer.CustomerType == 0)
                    errors.Add("CustomerType is required");
                if (string.IsNullOrEmpty(req.tblcustomer.UserName) || req.tblcustomer.UserName.TrimStart() == string.Empty)
                    errors.Add("UserName is required");
                if ((!string.IsNullOrEmpty(req.tblcustomer.CustomerEmail)))
                {
                    if (!_emailCheck.IsMatch(req.tblcustomer.CustomerEmail))
                        errors.Add("Please provide a valid email address");
                }
                if (string.IsNullOrEmpty(req.tblcustomer.RejectReason) || req.tblcustomer.RejectReason.TrimStart() == string.Empty)
                    errors.Add("Reject Reason is required");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
        
        // Client Sub Master //
        public static ErrorResponse InsertClientSubMaster(PostCustomersubuserMaster postcustomerDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _mobileNoCheck = new Regex(@"^[0-9]+$");
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");

            if (string.IsNullOrEmpty(postcustomerDTO.userName) || postcustomerDTO.userName.TrimStart() == string.Empty)
                errors.Add("UserName is required");
            if (string.IsNullOrEmpty(postcustomerDTO.LoginName) || postcustomerDTO.LoginName.TrimStart() == string.Empty)
                errors.Add("LoginName is required");
            if (string.IsNullOrEmpty(postcustomerDTO.PhoneNo) || postcustomerDTO.PhoneNo.TrimStart() == string.Empty || !_mobileNoCheck.IsMatch(postcustomerDTO.PhoneNo))
                errors.Add("PhoneNo is required");
            if (string.IsNullOrEmpty(postcustomerDTO.Email))
                errors.Add("Email is required");
            else if (!_emailCheck.IsMatch(postcustomerDTO.Email))
                errors.Add("Please provide a valid email address");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}