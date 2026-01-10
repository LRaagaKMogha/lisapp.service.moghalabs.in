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

namespace DEV.API.SERVICE.Controllers
{
    public class MassRegistrationValidation
    {
        public static ErrorResponse InsertMassRegistration(ExternalBulkFile objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            // Required fields validation
            if (string.IsNullOrEmpty(objDTO.filename))
                errors.Add("File Name is required");
            if (objDTO.customerno == 0)
                errors.Add("Customer No is required");
            if (objDTO.physicianno == 0)
                errors.Add("Physician No is required");
            if (objDTO.contractno == 0)
                errors.Add("Contract No is required");
            if (objDTO.testno == 0)
                errors.Add("Test No is required");
            if (string.IsNullOrEmpty(objDTO.validfrom))
                errors.Add("Valid From is required");
            if (string.IsNullOrEmpty(objDTO.validto))
                errors.Add("Valid To is required");
            if (objDTO.iadditionalrecords == 0)
                errors.Add("Additional Records is required");
            if (objDTO.patientlst == null || !objDTO.patientlst.Any())
                errors.Add("Uploaded file cannot be empty");

            // Validate and populate patient data
            if (objDTO.patientlst != null)
            {
                var validColumns = new HashSet<string>
                {
                    "Idnumber", "PatientName", "Gender", "DOB",
                    "Email", "Contact", "Street", "Block",
                    "BuildinName", "PostalCode", "Alternate_email", "Nationality"
                };

                for (int x = 0; x < objDTO.patientlst.Count; x++)
                {
                    ExternalmassPatient patientItem = new ExternalmassPatient();
                    for (int y = 0; y <= 11; y++)
                    {
                        string coldata = GetColumnData(objDTO.patientlst[x], y);

                        //if (x == 0 && !validColumns.Contains(coldata))
                        //{
                        //    errors.Add($"Invalid column name {coldata}");
                        //    break;
                        //}

                        //// Populate patient item
                        //switch (y)
                        //{
                        //    case 0: patientItem.idNo = coldata; break;
                        //    case 1: patientItem.patientname = coldata; break;
                        //    case 2: patientItem.gender = coldata; break;
                        //    case 3: patientItem.dob = coldata; break;
                        //    case 4: patientItem.email = coldata; break;
                        //    case 5: patientItem.contact = coldata; break;
                        //    case 6: patientItem.street = coldata; break;
                        //    case 7: patientItem.block = coldata; break;
                        //    case 8: patientItem.buildingname = coldata; break;
                        //    case 9: patientItem.postalcode = coldata; break;
                        //    case 10: patientItem.alternate_email = coldata; break;s
                        //    case 11: patientItem.nationality = coldata; break;
                        //}
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


        // Helper method to get column data from ExternalmassPatient
        private static string GetColumnData(ExternalmassPatient patient, int columnIndex)
        {
            switch (columnIndex)
            {
                case 0: return patient.idNo;
                case 1: return patient.patientname;
                case 2: return patient.gender;
                case 3: return patient.dob;
                case 4: return patient.email;
                case 5: return patient.contact;
                case 6: return patient.street;
                case 7: return patient.block;
                case 8: return patient.buildingname;
                case 9: return patient.postalcode;
                case 10: return patient.alternate_email;
                case 11: return patient.nationality;
                default: return string.Empty;
            }
        }

        // Cancel Test Validation //
        public static ErrorResponse InsertCancelTest(CancelVisit Req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (Req.lstCancelVisitTest == null || Req.lstCancelVisitTest.Count == 0)
            {
                errors.Add("No more records");
            }

            var lst = Req.lstCancelVisitTest.Where(s => s.isCancelled == true).ToList();
            if (lst.Count == 0)
            {
                errors.Add("Select at least one service");
            }

            foreach (var test in Req.lstCancelVisitTest)
            {
                if (test.isCancelled == true && (string.IsNullOrEmpty(test.cancelReason) || test.cancelReason.TrimStart() == string.Empty))
                {
                    errors.Add($"Enter the cancel reason for {test.serviceName}");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Patient Copying Validation //
        public static ErrorResponse SavePatientMerge(PatientmergeDTO RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if ((RequestItem.FpatientvisitNo <= 0) && RequestItem.TpatientvisitNo > 0)
            {
                errors.Add("Please select the from lab accession no");
            }

            if ((RequestItem.FpatientvisitNo <= 0) && (RequestItem.TpatientvisitNo <= 0))
            {
                errors.Add("No merged data found");
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