using Service.Model.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Service.Model.PatientInfo;
using Service.Model.Sample;

namespace Service.API.SERVICE.Controllers
{
    public class PatientInformationValidation
    {
        public static ErrorResponse UpdatePatientDetails(EditPatientRequest editPatientRequest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _emailCheck = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$");

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
            if (!string.IsNullOrEmpty(editPatientRequest.email))
            {
                if (!_emailCheck.IsMatch(editPatientRequest.email))
                    errors.Add("Please provide a valid email address");
            }            
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