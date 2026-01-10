using DEV.Model;
using DEV.Model.Common;
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
using DEV.Model.PatientInfo;
using DEV.Model.Sample;

namespace DEV.API.SERVICE.Controllers
{
    public class PatientInformationValidation
    {
        public static ErrorResponse UpdatePatientDetails(EditPatientRequest editPatientRequest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");

            //if (string.IsNullOrEmpty(editPatientRequest.urnType) || (editPatientRequest.urnType.TrimStart() == string.Empty))
            //    errors.Add("URN Type is required");

            //if (string.IsNullOrEmpty(editPatientRequest.urnId) || (editPatientRequest.urnId.TrimStart() == string.Empty))
            //    errors.Add("URN ID is required");

            if (!string.IsNullOrEmpty(editPatientRequest.firstName) && editPatientRequest.firstName.Length >= 1)
            {
                editPatientRequest.firstName = CapitalizeFirstLetter(editPatientRequest.firstName);
            }

            if (!string.IsNullOrEmpty(editPatientRequest.middleName) && editPatientRequest.middleName.Length >= 1)
            {
                editPatientRequest.middleName = CapitalizeFirstLetter(editPatientRequest.middleName);
            }

            if (!string.IsNullOrEmpty(editPatientRequest.lastName) && editPatientRequest.lastName.Length >= 1)
            {
                editPatientRequest.lastName = CapitalizeFirstLetter(editPatientRequest.lastName);
            }

            if (string.IsNullOrEmpty(editPatientRequest.firstName) || (editPatientRequest.firstName.TrimStart() == string.Empty))
                errors.Add("First Name is required");
            if (editPatientRequest.gender == 0)
                errors.Add("Gender is required");
            if (editPatientRequest.dob == DateTime.MinValue)
                errors.Add("DOB is required");
            //if (editPatientRequest.patientAge == 0)
            //    errors.Add("Patient Age is required");
            //if (string.IsNullOrEmpty(editPatientRequest.ageType) || (editPatientRequest.ageType.TrimStart() == string.Empty))
            //    errors.Add("Age Type is required");
            if (!string.IsNullOrEmpty(editPatientRequest.email))
            {
                if (!_emailCheck.IsMatch(editPatientRequest.email))
                    errors.Add("Please provide a valid email address");
            }
            //if (string.IsNullOrEmpty(editPatientRequest.pincode) || (editPatientRequest.pincode.TrimStart() == string.Empty))
            //    errors.Add("Pincode is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse GetPatientInfoDetails(CommonFilterRequestDTO RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date and To Date");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static ErrorResponse GeteLabPatientInfoDetails(PatientInfoRequestDTO RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RequestItem.Type == "Custom" || RequestItem.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(RequestItem.FromDate) || RequestItem.FromDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(RequestItem.ToDate) || RequestItem.ToDate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date and To Date");
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