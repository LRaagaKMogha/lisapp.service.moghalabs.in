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
using Service.Model.PatientInfo;
using Service.Model.Sample;
using System.Text;

namespace DEV.API.SERVICE.Controllers
{
    public class BulkResultCultureValidation
    {
        // Get Bulk Result (Entry & Validation) - Culture //
        public static ErrorResponse GetCultureBulkResultEtry(GetBulkCultureResultRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.gentype == "Custom" || req.gentype == string.Empty)
            {
                if (string.IsNullOrEmpty(req.fromdate) || req.fromdate.TrimStart() == string.Empty ||
                        string.IsNullOrEmpty(req.todate) || req.todate.TrimStart() == string.Empty)
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

        // Save Bulk Result (Entry & Validation) - Culture //
        public static ErrorResponse SaveCultureBulkResultEtry(SaveBulkCUltureResultRequest req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.lstCulture != null && req.lstCulture.Count > 0)
            {
                int isCheckedAvail = 0;

                foreach (var item in req.lstCulture)
                {
                    if (item.ischecked == true)
                    {
                        isCheckedAvail = 1;
                    }
                }

                if (isCheckedAvail == 0)
                {
                    errors.Add("Please select any test");
                }

                var lstReportStatusSelected = req.lstCulture
                    .Where(d => d.ischecked == true && (d.reportstatus == 0)).ToList();
                if (lstReportStatusSelected.Count > 0)
                {
                    errors.Add("Please choose report status for the selected records");
                }

                var lstResultStatusSelected = req.lstCulture
                    .Where(d => d.ischecked == true && (d.resultstatus == 0)).ToList();
                if (lstResultStatusSelected.Count > 0)
                {
                    errors.Add("Please choose result status for the selected records");
                }

                var selectedData = req.lstCulture
                    .Where(d => d.ischecked == true && d.prevreportstatus == d.reportstatus).ToList();
                if (selectedData.Count > 0 && req.pagecode == "PCRE")
                {
                    errors.Add("Please change the report status");
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