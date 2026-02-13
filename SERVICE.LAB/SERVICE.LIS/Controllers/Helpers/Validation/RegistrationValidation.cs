using Service.Model;
using Service.Model.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Service.IRepository;

namespace Service.API.SERVICE.Controllers
{
    public class RegistrationValidation
    {
        private static List<BulkFileUpload> _lstBulkImages;
        private static IConfiguration _config;
        private static IMasterRepository _IMasterRepository;
        public static void SetValidation(List<BulkFileUpload> lstBulkImages, IConfiguration config, IMasterRepository IMasterRepository)
        {
            _lstBulkImages = lstBulkImages;
            _config = config;
            _IMasterRepository = IMasterRepository;
        }

        // Registration & Billing //
        public static ErrorResponse InsertFrontOfficeMaster(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");

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
            if (objDTO.Age == 0 && (string.IsNullOrEmpty(objDTO.DOB) || (objDTO.DOB.TrimStart() == string.Empty)))
                errors.Add("Age is required");
            if (string.IsNullOrEmpty(objDTO.AgeType) || (objDTO.AgeType.TrimStart() == string.Empty))
                errors.Add("Age Type is required");
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

            if (objDTO.RefferralTypeNo == 3 && (pdforimgupload == null || pdforimgupload.Count == 0))
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

        public static ErrorResponse InsertFrontOfficeRegistration(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");

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
    
            if (objDTO.RefferralTypeNo == 0)
                errors.Add("Refferral Type No is required");
            if ((objDTO.RefferralTypeNo == 2) && (objDTO.CustomerNo == 0))
                errors.Add("Customer No is required");
    
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