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
using DEV.Model.Sample;

namespace DEV.API.SERVICE.Controllers
{
    public class PatientReportValidation
    {
        // Audit Trail Report Validation //
        public static ErrorResponse GetAuditTrailReport(GetAuditReportReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.Type == "Custom" || req.Type == string.Empty)
            {
                if (string.IsNullOrEmpty(req.FROMDate) || req.FROMDate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
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

        // Patient Report Validation //
        public static ErrorResponse GetPatientReport(requestpatientreport req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.type == "Custom" || req.type == string.Empty)
            {
                if (string.IsNullOrEmpty(req.fromdate) || req.fromdate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date");
                }

                if (string.IsNullOrEmpty(req.todate) || req.todate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The To Date");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse SinglePrintPatientReport(List<PatientReportDTO> PatientItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (PatientItem.Count > 0)
            {

            }
            else
            {
                errors.Add("Select any Patient");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse PrintPatientReport(PatientReportDTO PatientItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (PatientItem == null)
            {
                errors.Add("Select any Patient");
            }
            else
            {
                if (PatientItem.isProvisional == true)
                {
                    errors.Add("Provisional report couldn't be sent");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse GetAmendedPatientReport(requestamendedpatientreport req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.type == "Custom" || req.type == string.Empty)
            {
                if (string.IsNullOrEmpty(req.fromdate) || req.fromdate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The From Date");
                }

                if (string.IsNullOrEmpty(req.todate) || req.todate.TrimStart() == string.Empty)
                {
                    errors.Add("Select The To Date");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse PrintAmendedPatientReport(AmendedPatientReportDTO RptItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RptItem == null)
            {
                errors.Add("Select any Patient");
            }
            else
            {
                if (RptItem.isProvisional == true)
                {
                    errors.Add("Provisional report couldn't be sent");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse GetATSubCatyMasters(GetATSubCatyMasterSearchReq RptItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (RptItem == null)
            {
                errors.Add("Select any Patient");
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