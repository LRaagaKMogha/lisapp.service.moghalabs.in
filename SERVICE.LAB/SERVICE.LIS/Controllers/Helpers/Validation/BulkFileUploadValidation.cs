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
    public class BulkFileUploadValidation
    {
        // Bulk File Upload //
        public static ErrorResponse BulkUploadFile(List<BulkFileUpload> lstjDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (lstjDTO.Count > 0)
            {
                foreach (var fileUpload in lstjDTO)
                {
                    //if (fileUpload.FileType != "pdf")
                    //{
                    //    errors.Add("File format should be pdf");
                    //}

                    if (string.IsNullOrEmpty(fileUpload.ManualFileName) || fileUpload.ManualFileName.TrimStart() == string.Empty)
                    {
                        errors.Add("Manual File Name is required");
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

        // File Upload //
        public static ErrorResponse UploadFile(FrontOffficeDTO objDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(objDTO.FileName) || objDTO.FileName.TrimStart() == string.Empty)
            {
                errors.Add("Manual File Name is required");
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