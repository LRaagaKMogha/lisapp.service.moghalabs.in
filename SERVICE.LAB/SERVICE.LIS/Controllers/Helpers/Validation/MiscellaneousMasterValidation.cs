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

namespace DEV.API.SERVICE.Controllers
{
    public class MiscellaneousMasterValidation
    {
        // Comment Master //
        public static ErrorResponse Insertcommentmaster(CommentInsReq insReq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (insReq.CategoryNo == 0)
                errors.Add("Category No is required");
            if (string.IsNullOrEmpty(insReq.Description) || insReq.Description.TrimStart() == string.Empty)
                errors.Add("Comment Description is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Source Of Specimen Master //
        public static ErrorResponse InsertNationRace(InsNationRaceReq insReq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (insReq.Type == "SPECIMENSOURCE")
            {
                if (string.IsNullOrEmpty(insReq.Description) || insReq.Description.TrimStart() == string.Empty)
                    errors.Add("Specimen Description is required");
            }

            // Nationality Master
            else if (insReq.Type == "nationality")
            {
                if (string.IsNullOrEmpty(insReq.Description) || insReq.Description.TrimStart() == string.Empty)
                    errors.Add("Nationality Description is required");
            }

            // Race Master
            else if (insReq.Type == "race")
            {
                if (string.IsNullOrEmpty(insReq.Description) || insReq.Description.TrimStart() == string.Empty)
                    errors.Add("Race Description is required");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse InsertCommentSubCategory(InsertCommentSubCategoryReqest objRequest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (objRequest.CategoryNo == 0)
                errors.Add("Category No is required");

            if (string.IsNullOrEmpty(objRequest.SubCatyDesc) || objRequest.SubCatyDesc.TrimStart() == string.Empty)
                errors.Add("Sub Category Description is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        public static ErrorResponse InsertBankMaster(InsertBankMastereq objRequest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            
            if (string.IsNullOrEmpty(objRequest.BankName) || objRequest.BankName.Trim() == string.Empty)
                errors.Add("Bank Name is required");
            
            if (objRequest.CountryID <= 0)
                errors.Add("Country is required");
            if (objRequest.StateID <= 0)
                errors.Add("State is required");
            if (objRequest.CityID <= 0)
                errors.Add("City is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true; 
                errorResponse.message = string.Join(",", errors);
            }

            return errorResponse;
        }

        public static ErrorResponse InsertBankBranch(InsertBankbranchreq objRequest)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();


            if (string.IsNullOrEmpty(objRequest.BranchName) || objRequest.BranchName.Trim() == string.Empty)
                errors.Add("branch Name is required");

            if (objRequest.BankID <= 0)
                errors.Add("BankID is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }

            return errorResponse;
        }

    }
}