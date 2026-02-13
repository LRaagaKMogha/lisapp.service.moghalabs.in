using Service.Model;
using Service.Model.Common;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers
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