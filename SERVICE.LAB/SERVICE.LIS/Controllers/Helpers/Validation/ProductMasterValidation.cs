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
    public class ProductMasterValidation
    {
        // Reagent Master //
        public static ErrorResponse InsertProductMasterDetails(postProductMasterDTO postProductMasterDTO)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(postProductMasterDTO.tblproductMaster.ProductMasterName) || postProductMasterDTO.tblproductMaster.ProductMasterName.TrimStart() == string.Empty)
                errors.Add("Product Master Name is required");

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}