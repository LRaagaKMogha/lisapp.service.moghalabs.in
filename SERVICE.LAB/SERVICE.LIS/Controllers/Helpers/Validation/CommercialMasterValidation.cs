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

namespace DEV.API.SERVICE.Controllers
{
    public class CommercialMasterValidation
    {
        // Company Master //
        public static ErrorResponse Insertcompanymaster(CommericalInsReq insReq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(insReq.CompanyName) || insReq.CompanyName.TrimStart() == string.Empty)
                errors.Add("Company Name is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // GST Master //
        public static ErrorResponse InsertGSTMaster(GSTInsReq insReq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(insReq.Description) || insReq.Description.TrimStart() == string.Empty)
                errors.Add("Description is required");
            if (insReq.Percentage == 0 || insReq.Percentage.ToString().Trim() == string.Empty)
                errors.Add("Percentage is required");

            if (insReq.Percentage <= 0 || insReq.Percentage > 100)
                errors.Add("Invalid Percentage : Percentage must be between 0 and 100");
            else 
                insReq.Percentage = Math.Round(insReq.Percentage, 2);

            if (string.IsNullOrEmpty(insReq.FromDate) || insReq.FromDate.TrimStart() == string.Empty)
                errors.Add("From Date is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Manufacturer Master //
        public static ErrorResponse InsertManufacturerDetails(postManufacturerMasterDTO objManufacturer)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(objManufacturer.manufacturerName) || objManufacturer.manufacturerName.TrimStart() == string.Empty)
                errors.Add("Manufacturer Name is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Contract Master //
        public static ErrorResponse InserContractMaster(InsertContractReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            // Contract Master & Commercial Master Approval for Contract without Reject //
            if (req.IsApproval == 0 || req.IsApproval == 1 && req.IsReject == false)
            {
                if (string.IsNullOrEmpty(req.Code) || req.Code.TrimStart() == string.Empty)
                    errors.Add("Contract Code is required");
                if (string.IsNullOrEmpty(req.Description) || req.Description.TrimStart() == string.Empty)
                    errors.Add("Contract Name is required");
                if (string.IsNullOrEmpty(req.ValidFrom) || req.ValidFrom.TrimStart() == string.Empty)
                    errors.Add("Valid From is required");
                if (string.IsNullOrEmpty(req.ValidTo) || req.ValidTo.TrimStart() == string.Empty)
                    errors.Add("Valid To is required");

                bool validAmountFound = false;

                if (req.serviceDetails != null && req.serviceDetails.Count > 0)
                {
                    foreach (var rateService in req.serviceDetails)
                    {
                        if (rateService.Amount > 0)
                        {
                            validAmountFound = true;
                            break;
                        }
                    }

                    if (!validAmountFound)
                    {
                        errors.Add("Amount is required");
                    }
                }
            }

            // Commercial Master Approval for Contract with Reject //
            else if (req.IsApproval == 1 && req.IsReject == true)
            {
                if (string.IsNullOrEmpty(req.Code) || req.Code.TrimStart() == string.Empty)
                    errors.Add("Contract Code is required");
                if (string.IsNullOrEmpty(req.Description) || req.Description.TrimStart() == string.Empty)
                    errors.Add("Contract Name is required");
                if (string.IsNullOrEmpty(req.ValidFrom) || req.ValidFrom.TrimStart() == string.Empty)
                    errors.Add("Valid From is required");
                if (string.IsNullOrEmpty(req.ValidTo) || req.ValidTo.TrimStart() == string.Empty)
                    errors.Add("Valid To is required");
                if (string.IsNullOrEmpty(req.RejectReason) || req.RejectReason.TrimStart() == string.Empty)
                    errors.Add("Reject Reason is required");

                bool validAmountFound = false;

                if (req.serviceDetails != null && req.serviceDetails.Count > 0)
                {
                    foreach (var rateService in req.serviceDetails)
                    {
                        if (rateService.Amount > 0)
                        {
                            validAmountFound = true;
                            break;
                        }
                    }

                    if (!validAmountFound)
                    {
                        errors.Add("Amount is required");
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

        // Referrer Special Rate Master //
        public static ErrorResponse InsertReferrerlst(InsertReflstReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.ReferrerNo == 0)
                errors.Add("Referrer No is required");

            bool validAmountFound = false;

            if (req.serviceDetails != null && req.serviceDetails.Count > 0)
            {
                foreach (var rateService in req.serviceDetails)
                {
                    if (rateService.Amount > 0)
                    {
                        validAmountFound = true;
                        break;
                    }
                }

                if (!validAmountFound)
                {
                    errors.Add("Amount is required");
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