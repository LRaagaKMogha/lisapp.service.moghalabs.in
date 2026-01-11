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
using Service.Model.Sample;
using RtfPipe.Tokens;

namespace DEV.API.SERVICE.Controllers
{
    public class ReportValidation
    {
        // Antibiotic Sensitivity Report //
        public static ErrorResponse GetSensitivityReport(CommonFilterRequestDTO req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");
            if (string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
                errors.Add("To Date is required");

            if (req.reporttype == 1)
            {
                if (req.serviceNo == 0 || req.serviceNo.ToString().Trim() == string.Empty)
                {
                    errors.Add("Service No is required");
                }              
            }

            if (req.reporttype == 3)
            {
                if (req.CustomerNo == 0 || req.CustomerNo.ToString().Trim() == string.Empty)
                {
                    errors.Add("Customer No is required");
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Non Gynae - WorkLoad Report //
        public static ErrorResponse GetNonGynaeWorkLoadReport(CommonFilterRequestDTO req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");
            if (string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
                errors.Add("To Date is required");
       
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // WorkLoad Report //
        public static ErrorResponse GetWorkloadReport(CommonFilterRequestDTO req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");
            if (string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
                errors.Add("To Date is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // CYTO QC Report //
        public static ErrorResponse GetCytoQCReport(CommonFilterRequestDTO req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");
            if (string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
                errors.Add("To Date is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Common Report Validation //
        public static ErrorResponse GetReport(ReportDTO req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            string action = req.fileType;
            string fromDate = null;
            string toDate = null;
            string typeValue = null;

            foreach (var param in req.ReportParamitem)
            {
                if (param.key.Equals("FromDate", StringComparison.OrdinalIgnoreCase)) fromDate = param.value;
                if (param.key.Equals("ToDate", StringComparison.OrdinalIgnoreCase)) toDate = param.value;
                if (param.key.Equals("Type", StringComparison.OrdinalIgnoreCase)) typeValue = param.value;
            }

            if (typeValue?.Equals("Custom", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (action == "filter" || action == "pdf" || action == "excel")
                {
                    if (string.IsNullOrEmpty(fromDate))
                    {
                        errors.Add("Please select From Date");
                    }

                    if (string.IsNullOrEmpty(toDate))
                    {
                        errors.Add("Please select To Date");
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


        // Result Impression Report //
        public static ErrorResponse GetPatientImpression(CommonFilterRequestDTO RequestItem)
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

        public static ErrorResponse GetPatientImpressionReport(GetImpressionReportRequest RequestItem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            string action = RequestItem.fileType;
            string fromDate = null;
            string toDate = null;
            string typeValue = null;

            foreach (var param in RequestItem.ReportParamitem)
            {
                if (param.key.Equals("FromDate", StringComparison.OrdinalIgnoreCase)) fromDate = param.value;
                if (param.key.Equals("ToDate", StringComparison.OrdinalIgnoreCase)) toDate = param.value;
                if (param.key.Equals("Type", StringComparison.OrdinalIgnoreCase)) typeValue = param.value;
            }

            if (typeValue?.Equals("Custom", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (action == "filter" || action == "pdf" || action == "excel")
                {
                    if (string.IsNullOrEmpty(fromDate))
                    {
                        errors.Add("Please select From Date");
                    }

                    if (string.IsNullOrEmpty(toDate))
                    {
                        errors.Add("Please select To Date");
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


        //Cyto WorkLoad Report //
        public static ErrorResponse GetCytoWorkloadReport(SlidePrintingRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.FromDate) || req.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");
            if (string.IsNullOrEmpty(req.ToDate) || req.ToDate.TrimStart() == string.Empty)
                errors.Add("To Date is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}