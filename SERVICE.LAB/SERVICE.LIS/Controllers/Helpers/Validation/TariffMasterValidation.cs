using DEV.Model;
using DEV.Model.Common;
using DEV.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RtfPipe.Tokens;

namespace DEV.API.SERVICE.Controllers
{
    public class TariffMasterValidation
    {
        // Tariff Master //
        public static ErrorResponse InsertTariffMaster(InsertTariffMasterRequest tariffMasteritem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            // Tariff Master & Commercial Master Approval for Tariff without Reject //
            if(tariffMasteritem.IsApproval == 0 || tariffMasteritem.IsApproval == 1 && tariffMasteritem.IsReject == false)
            {
                if (string.IsNullOrEmpty(tariffMasteritem.RateListName) || tariffMasteritem.RateListName.TrimStart() == string.Empty)
                    errors.Add("Rate List Name is required");
                if (string.IsNullOrEmpty(tariffMasteritem.EffectiveFrom) || tariffMasteritem.EffectiveFrom.TrimStart() == string.Empty)
                    errors.Add("Effective From is required");
                if (string.IsNullOrEmpty(tariffMasteritem.EffectiveTo) || tariffMasteritem.EffectiveTo.TrimStart() == string.Empty)
                    errors.Add("Effective To is required");
            }

            // Commercial Master Approval for Tariff with Reject //
            else if(tariffMasteritem.IsApproval == 1 && tariffMasteritem.IsReject == true)
            {
                if (string.IsNullOrEmpty(tariffMasteritem.RateListName) || tariffMasteritem.RateListName.TrimStart() == string.Empty)
                    errors.Add("Rate List Name is required");
                if (string.IsNullOrEmpty(tariffMasteritem.EffectiveFrom) || tariffMasteritem.EffectiveFrom.TrimStart() == string.Empty)
                    errors.Add("Effective From is required");
                if (string.IsNullOrEmpty(tariffMasteritem.EffectiveTo) || tariffMasteritem.EffectiveTo.TrimStart() == string.Empty)
                    errors.Add("Effective To is required");
                if (string.IsNullOrEmpty(tariffMasteritem.RejectReason) || tariffMasteritem.RejectReason.TrimStart() == string.Empty)
                    errors.Add("Reject Reason is required");
            }
           
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Client Tariff Mapping //
        public static ErrorResponse InsertClienttTariffMap(InsTariffReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.RateListNo == 0)
                errors.Add("Rate List No is required");
            if (req.ReferrerNo == 0)
                errors.Add("Referrer No is required");
            if (string.IsNullOrEmpty(req.EffectiveFrom) || req.EffectiveFrom.TrimStart() == string.Empty)
                errors.Add("Effective From is required");
            //if (string.IsNullOrEmpty(req.EffectiveTo) || req.EffectiveTo.TrimStart() == string.Empty)
            //    errors.Add("Effective To is required");

            DateTime EffectiveFrom, EffectiveTo;
            if (!string.IsNullOrEmpty(req.EffectiveFrom) && DateTime.TryParse(req.EffectiveFrom, out EffectiveFrom) &&
                !string.IsNullOrEmpty(req.EffectiveTo) && DateTime.TryParse(req.EffectiveTo, out EffectiveTo))
            {
                int outt = DateDiffInDays(EffectiveFrom, EffectiveTo);
                if (outt < 0)
                {
                    errors.Add("The EffectiveFrom should be less than the EffectiveTo");
                    req.EffectiveTo = null;
                }
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
        private static int DateDiffInDays(DateTime a, DateTime b)
        {
            return (b.Date - a.Date).Days;
        }

        // Tariff Client Mapping //
        public static ErrorResponse InsertTariffMasterDetails(InsertTariffMasterRequest tariffMasteritem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (tariffMasteritem.RateListNo == 0)
                errors.Add("Rate List No is required");
            if (tariffMasteritem.ClientNo == 0)
                errors.Add("Client No is required");
            if (string.IsNullOrEmpty(tariffMasteritem.EffectiveFrom) || tariffMasteritem.EffectiveFrom.TrimStart() == string.Empty)
                errors.Add("Effective From is required");
            if (string.IsNullOrEmpty(tariffMasteritem.EffectiveTo) || tariffMasteritem.EffectiveTo.TrimStart() == string.Empty)
                errors.Add("Effective To is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}