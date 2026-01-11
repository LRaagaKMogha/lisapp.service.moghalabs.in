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
using Microsoft.AspNetCore.Mvc;
using Dev.Repository;
using Microsoft.Extensions.Configuration;

namespace DEV.API.SERVICE.Controllers
{
    public class RegistrationValidation
    {
        private static List<BulkFileUpload> _lstBulkImages;
        private static IConfiguration _config;
        public static void SetValidation(List<BulkFileUpload> lstBulkImages, IConfiguration config)
        {
            _lstBulkImages = lstBulkImages;
            _config = config;
        }

        // Registration & Billing //
        public static ErrorResponse InsertFrontOfficeMaster(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            //if (string.IsNullOrEmpty(objDTO.URNType) || (objDTO.URNType.TrimStart() == string.Empty))
            //    errors.Add("URN Type is required");

            //if (string.IsNullOrEmpty(objDTO.URNID) || (objDTO.URNID.TrimStart() == string.Empty))
            //    errors.Add("URN ID is required");
            //if (ContainsSpecialCharacters(objDTO.URNID, pattern))
            //{
            //    errors.Add("Special Characters Are Not Allowed in URN ID");
            //}

            if (!string.IsNullOrEmpty(objDTO.FirstName) && objDTO.FirstName.Length >= 1)
            {
                objDTO.FirstName = CapitalizeFirstLetter(objDTO.FirstName);
            }

            if (!string.IsNullOrEmpty(objDTO.MiddleName) && objDTO.MiddleName.Length >= 1)
            {
                objDTO.MiddleName = CapitalizeFirstLetter(objDTO.MiddleName);
            }

            if (!string.IsNullOrEmpty(objDTO.LastName) && objDTO.LastName.Length >= 1)
            {
                objDTO.LastName = CapitalizeFirstLetter(objDTO.LastName);
            }

            if (string.IsNullOrEmpty(objDTO.FirstName) || (objDTO.FirstName.TrimStart() == string.Empty))
                errors.Add("First Name is required");
            if (string.IsNullOrEmpty(objDTO.Gender) || (objDTO.Gender.TrimStart() == string.Empty))
                errors.Add("Gender is required");
            //if (string.IsNullOrEmpty(objDTO.DOB) || (objDTO.DOB.TrimStart() == string.Empty))
            //    errors.Add("DOB is required");
            if (objDTO.Age == 0 && (string.IsNullOrEmpty(objDTO.DOB) || (objDTO.DOB.TrimStart() == string.Empty)))
                errors.Add("Age is required");
            if (string.IsNullOrEmpty(objDTO.AgeType) || (objDTO.AgeType.TrimStart() == string.Empty))
                errors.Add("Age Type is required");
            if ((objDTO.RefferralTypeNo != 2) && (string.IsNullOrEmpty(objDTO.MobileNumber) || (objDTO.MobileNumber.TrimStart() == string.Empty)))
                errors.Add("Mobile Number is required");
            //if (!string.IsNullOrEmpty(objDTO.EmailID))
            //{
            //    if (!_emailCheck.IsMatch(objDTO.EmailID))
            //        errors.Add("Please provide a valid email address");
            //}
            if (objDTO.RefferralTypeNo == 0)
                errors.Add("Refferral Type No is required");
            if ((objDTO.RefferralTypeNo == 2) && (objDTO.CustomerNo == 0))
                errors.Add("Customer No is required");
            if ((objDTO.RefferralTypeNo == 3) && (objDTO.PhysicianNo == 0))
                errors.Add("Physician No is required");

            //if (objDTO.Payments != null && objDTO.Payments.Count > 0)
            //{
            //    foreach (var payment in objDTO.Payments)
            //    {
            //        if (!string.IsNullOrEmpty(payment.ModeOfType) && (payment.Amount <= 0))
            //        {
            //            errors.Add("Amount is required");
            //        }

            //        if (payment.Amount > 0 && string.IsNullOrEmpty(payment.ModeOfType))
            //        {
            //            errors.Add("Mode Of Type is required");
            //        }
            //    }
            //}

            //if (string.IsNullOrEmpty(objDTO.Pincode) || (objDTO.Pincode.Length < 6))
            //    errors.Add("Pincode should be atleast 6 digits");
            //if (objDTO.DueAmount > 0 || objDTO.DueAmount.ToString().Trim() == string.Empty && (string.IsNullOrEmpty(objDTO.DueRemarks) || (objDTO.DueRemarks.TrimStart() == string.Empty)))
            //    errors.Add("Due Remarks is required");
            //if (string.IsNullOrEmpty(objDTO.Address) || (objDTO.Address.TrimStart() == string.Empty))
            //    errors.Add("Address is required");
            //if (objDTO.IsDiscountApprovalReq == 1 && (string.IsNullOrEmpty(objDTO.discountDescription) || (objDTO.discountDescription.TrimStart() == string.Empty)))
            //    errors.Add("Discount Description is required");

            // Age Validation //
            if (objDTO.Age > 120)
            {
                errors.Add("Invalid Age");
            }

            // Validate for Duplicate Service Names //
            for (int i = 0; i < objDTO.Orders.Count; i++)
            {
                var v = objDTO.Orders[i];
                if (v.TestNo > 0)
                {
                    var isduplicate = objDTO.Orders.Where(x => x.TestNo == v.TestNo && x.TestType == v.TestType).ToList();
                    if (isduplicate.Count > 1)
                    {
                        errors.Add("This Service Already Exists");
                        break;
                    }
                }
            }

            // Prescription upload validation
            var pdforimgupload = _lstBulkImages != null && _lstBulkImages.Count > 0
                                 ? _lstBulkImages.Where(d => d.FileType.Equals("pdf", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("jpg", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("jpeg", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("png", StringComparison.OrdinalIgnoreCase)).ToList()
                                 : new List<BulkFileUpload>();

            if (objDTO.RefferralTypeNo == 3 && (pdforimgupload == null || pdforimgupload.Count == 0))
            {
                errors.Add("Prescription upload is mandatory");
            }

            //// Amount paid exceeded Validation //
            //if (objDTO.DueAmount >= objDTO.CollectedAmount)
            //{
            //    objDTO.DueAmount = Math.Round(objDTO.DueAmount - objDTO.CollectedAmount);
            //}
            //else
            //{
            //    errors.Add("Amount paid exceeded");
            //}

            //// Discount Amount Validation //
            //if (objDTO.DiscountAmount > objDTO.DueAmount)
            //{
            //    errors.Add("Sorry Can't apply discount,Amount Already Paid");
            //}

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse InsertFrontOfficeRegistration(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (!string.IsNullOrEmpty(objDTO.FirstName) && objDTO.FirstName.Length >= 1)
            {
                objDTO.FirstName = CapitalizeFirstLetter(objDTO.FirstName);
            }

            if (!string.IsNullOrEmpty(objDTO.MiddleName) && objDTO.MiddleName.Length >= 1)
            {
                objDTO.MiddleName = CapitalizeFirstLetter(objDTO.MiddleName);
            }

            if (!string.IsNullOrEmpty(objDTO.LastName) && objDTO.LastName.Length >= 1)
            {
                objDTO.LastName = CapitalizeFirstLetter(objDTO.LastName);
            }

            if (string.IsNullOrEmpty(objDTO.FirstName) || (objDTO.FirstName.TrimStart() == string.Empty))
                errors.Add("First Name is required");

            if (string.IsNullOrEmpty(objDTO.Gender) || (objDTO.Gender.TrimStart() == string.Empty))
                errors.Add("Gender is required");

            //if (objDTO.Age == 0 && (string.IsNullOrEmpty(objDTO.DOB) || (objDTO.DOB.TrimStart() == string.Empty)))
            //    errors.Add("Age is required");

            //if (string.IsNullOrEmpty(objDTO.AgeType) || (objDTO.AgeType.TrimStart() == string.Empty))
            //    errors.Add("Age Type is required");

            if ((objDTO.RefferralTypeNo != 2) && (string.IsNullOrEmpty(objDTO.MobileNumber) || (objDTO.MobileNumber.TrimStart() == string.Empty)))
                errors.Add("Mobile Number is required");

            if (objDTO.RefferralTypeNo == 0)
                errors.Add("Refferral Type No is required");

            if ((objDTO.RefferralTypeNo == 2) && (objDTO.CustomerNo == 0))
                errors.Add("Customer No is required");

            if ((objDTO.RefferralTypeNo == 3) && (objDTO.PhysicianNo == 0))
                errors.Add("Physician No is required");  

            // Age Validation //
            if (objDTO.Age > 120)
            {
                errors.Add("Invalid Age");
            }

            // Validate for Duplicate Service Names //
            for (int i = 0; i < objDTO.Orders.Count; i++)
            {
                var v = objDTO.Orders[i];
                if (v.TestNo > 0)
                {
                    var isduplicate = objDTO.Orders.Where(x => x.TestNo == v.TestNo && x.TestType == v.TestType).ToList();
                    if (isduplicate.Count > 1)
                    {
                        errors.Add("This Service Already Exists");
                        break;
                    }
                }
            }

            // Prescription upload validation
            var pdforimgupload = _lstBulkImages != null && _lstBulkImages.Count > 0
                                 ? _lstBulkImages.Where(d => d.FileType.Equals("pdf", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("jpg", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("jpeg", StringComparison.OrdinalIgnoreCase)
                                                    || d.FileType.Equals("png", StringComparison.OrdinalIgnoreCase)).ToList()
                                 : new List<BulkFileUpload>();

            MasterRepository _IMasterRepository = new MasterRepository(_config);
            var objConfigValue = _IMasterRepository.GetSingleConfiguration(objDTO.VenueNo, objDTO.VenueBranchNo, "IsPrscrUpldMandatory");

            if (objConfigValue.ConfigValue == 1 && objDTO.RefferralTypeNo == 3 && (pdforimgupload == null || pdforimgupload.Count == 0))
            {
                errors.Add("Prescription upload is mandatory");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        private static bool ContainsSpecialCharacters(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        // Edit Registration & Billing //
        public static ErrorResponse InsertEditBilling(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            //if (string.IsNullOrEmpty(objDTO.URNType) || (objDTO.URNType.TrimStart() == string.Empty))
            //    errors.Add("URN Type is required");

            //if (string.IsNullOrEmpty(objDTO.URNID) || (objDTO.URNID.TrimStart() == string.Empty))
            //    errors.Add("URN ID is required");
            //if (ContainsSpecialCharacters(objDTO.URNID, pattern))
            //{
            //    errors.Add("Special Characters Are Not Allowed in URN ID");
            //}

            if (!string.IsNullOrEmpty(objDTO.FirstName) && objDTO.FirstName.Length >= 1)
            {
                objDTO.FirstName = CapitalizeFirstLetter(objDTO.FirstName);
            }

            if (!string.IsNullOrEmpty(objDTO.MiddleName) && objDTO.MiddleName.Length >= 1)
            {
                objDTO.MiddleName = CapitalizeFirstLetter(objDTO.MiddleName);
            }

            if (!string.IsNullOrEmpty(objDTO.LastName) && objDTO.LastName.Length >= 1)
            {
                objDTO.LastName = CapitalizeFirstLetter(objDTO.LastName);
            }

            if (string.IsNullOrEmpty(objDTO.FirstName) || (objDTO.FirstName.TrimStart() == string.Empty))
                errors.Add("First Name is required");
            if (string.IsNullOrEmpty(objDTO.Gender) || (objDTO.Gender.TrimStart() == string.Empty))
                errors.Add("Gender is required");
            //if (string.IsNullOrEmpty(objDTO.DOB) || (objDTO.DOB.TrimStart() == string.Empty))
            //    errors.Add("DOB is required");
            //if (objDTO.Age == 0)
            //    errors.Add("Age is required");
            //if (string.IsNullOrEmpty(objDTO.AgeType) || (objDTO.AgeType.TrimStart() == string.Empty))
            //    errors.Add("Age Type is required");
            //if ((objDTO.RefferralTypeNo != 2) && (string.IsNullOrEmpty(objDTO.MobileNumber) || (objDTO.MobileNumber.TrimStart() == string.Empty)))
            //    errors.Add("Mobile Number is required");
            //if ((!string.IsNullOrEmpty(objDTO.EmailID)))
            //{
            //    if (!_emailCheck.IsMatch(objDTO.EmailID))
            //        errors.Add("Please provide a valid email address");
            //}
            if (objDTO.RefferralTypeNo == 0)
                errors.Add("Refferral Type No is required");
            if ((objDTO.RefferralTypeNo == 2) && (objDTO.CustomerNo == 0))
                errors.Add("Customer No is required");
            //if ((objDTO.RefferralTypeNo == 2) && (objDTO.PhysicianNo == 0))
            //    errors.Add("Physician No is required");

            // Age Validation //
            if (objDTO.Age > 120)
            {
                errors.Add("Invalid Age");
            }

            // Validate for Duplicate Service Names //
            for (int i = 0; i < objDTO.Orders.Count; i++)
            {
                var v = objDTO.Orders[i];
                if (v.TestNo > 0)
                {
                    var isduplicate = objDTO.Orders.Where(x => x.TestNo == v.TestNo && x.TestType == v.TestType).ToList();
                    if (isduplicate.Count > 1)
                    {
                        errors.Add("This Service Already Exists");
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
    }
}