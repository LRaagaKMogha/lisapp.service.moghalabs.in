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
using RtfPipe.Tokens;

namespace DEV.API.SERVICE.Controllers
{
    public class DeviceInterfaceValidation
    {
        // Analyzer Master //
        public static ErrorResponse InsertAnalyzerDetails(TblAnalyzerresponse TblAnalyzerresponse)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(TblAnalyzerresponse.description) || TblAnalyzerresponse.description.TrimStart() == string.Empty)
                errors.Add("Description is required");
            if (string.IsNullOrEmpty(TblAnalyzerresponse.assetCode) || TblAnalyzerresponse.assetCode.TrimStart() == string.Empty)
                errors.Add("Asset Code is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Analyzer Vs Parameter Master //
        public static ErrorResponse InsertAnaParam(AnaParamDto AnaParamobj)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (AnaParamobj.AnalyzerMasterNo == 0)
                errors.Add("Analyzer Master No is required");
            if (string.IsNullOrEmpty(AnaParamobj.Description) || AnaParamobj.Description.TrimStart() == string.Empty)
                errors.Add("Description is required");
            if (AnaParamobj.SampleNo == 0)
                errors.Add("Sample No is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Analyzer Vs Parameter Master Vs TestMapping Master //
        public static ErrorResponse InsertAnalVsParamVsTest(responseTest responseTest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (responseTest.analyzerMasterNo == 0)
                errors.Add("Analyzer Master No is required");
            if (responseTest.analyzerParamNo == 0)
                errors.Add("Analyzer Param No is required");
            if (responseTest.testNo == 0)
                errors.Add("Test No is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}