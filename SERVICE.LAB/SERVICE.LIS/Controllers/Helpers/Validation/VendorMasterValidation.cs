using Service.Model;
using Service.Model.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Service.API.SERVICE.Controllers
{
    public class VendorMasterValidation
    {
        // Vendor Master //
        public static ErrorResponse InsertVendorMaster(Responsevendor obj1)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (string.IsNullOrEmpty(obj1.vendorName) || obj1.vendorName.TrimStart() == string.Empty)
                errors.Add("Vendor Name is required");
            if (ContainsSpecialCharacters(obj1.vendorName, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Vendor Name");
            }

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
        private static bool ContainsSpecialCharacters(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        // Vendor Vs Contact //
        public static ErrorResponse InsertVendorContactmaster(Savecontact creq1)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (creq1.Getcontactlst != null && creq1.Getcontactlst.Count > 0)
            {
                foreach (var contact in creq1.Getcontactlst)
                {
                    if (string.IsNullOrEmpty(contact.cname) || contact.cname.TrimStart() == string.Empty)
                    {
                        errors.Add("Contact Name is required");
                    }

                    if (string.IsNullOrEmpty(contact.cmobileNo) || contact.cmobileNo.TrimStart() == string.Empty)
                    {
                        errors.Add("Contact Mobile No is required");
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

        // Vendor Vs Services //
        public static ErrorResponse InsertVendorService(Saveservice serviceobj)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (serviceobj.Getservicelst != null && serviceobj.Getservicelst.Count > 0)
            {
                foreach (var service in serviceobj.Getservicelst)
                {
                    if (string.IsNullOrEmpty(service.serviceName) || service.serviceName.TrimStart() == string.Empty)
                    {
                        errors.Add("Service Name is required");
                    }
                }
            }
            else
            {
                errors.Add("Contact Service List is empty");
            }

            // Validate for Duplicate Service Names //
            if (serviceobj.Getservicelst != null)
            {
                for (int i = 0; i < serviceobj.Getservicelst.Count; i++)
                {
                    var v = serviceobj.Getservicelst[i];
                    if (v.serviceNo > 0)
                    {
                        var isduplicate = serviceobj.Getservicelst.Where(x => x.serviceName == v.serviceName).ToList();
                        if (isduplicate.Count > 1)
                        {
                            errors.Add("This Service Name Is Already Exists In This Vendor");
                        }
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
    }
}