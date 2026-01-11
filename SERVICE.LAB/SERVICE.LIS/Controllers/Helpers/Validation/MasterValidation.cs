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
    public class MasterValidation
    {
        // Physician Master //
        public static ErrorResponse InsertPhysicianDetails(PostPhysicianMaster req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            Regex _csvcheck = new Regex(@"^(=|\+|\-|@)");

            if (string.IsNullOrEmpty(req.tblPhysician.PhysicianName) || req.tblPhysician.PhysicianName.TrimStart() == string.Empty)
                errors.Add("Physician Name is required");
            if (_csvcheck.IsMatch(req.tblPhysician.PhysicianName.ToSubstring(req.tblPhysician.PhysicianName == null ? 0 : req.tblPhysician.PhysicianName.Length)) || _csvcheck.IsMatch(req.tblPhysician.Qualification.ToSubstring(req.tblPhysician.Qualification == null ? 0 : req.tblPhysician.Qualification.Length)) ||
               _csvcheck.IsMatch(req.tblPhysician.PhysicianEmail.ToSubstring(req.tblPhysician.PhysicianEmail == null ? 0 : req.tblPhysician.PhysicianEmail.Length)) || _csvcheck.IsMatch(req.tblPhysician.PhysicianMobileNo.ToSubstring(req.tblPhysician.PhysicianMobileNo == null ? 0 : req.tblPhysician.PhysicianMobileNo.Length)) ||
               _csvcheck.IsMatch(req.tblPhysician.WhatsAppNo.ToSubstring(req.tblPhysician.WhatsAppNo == null ? 0 : req.tblPhysician.WhatsAppNo.Length)) || _csvcheck.IsMatch(req.tblPhysician.specification.ToSubstring(req.tblPhysician.specification == null ? 0 : req.tblPhysician.specification.Length)) ||
                _csvcheck.IsMatch(req.tblPhysician.pincode.ToSubstring(req.tblPhysician.pincode == null ? 0 : req.tblPhysician.pincode.Length)) || _csvcheck.IsMatch(req.tblPhysician.area.ToSubstring(req.tblPhysician.area == null ? 0 : req.tblPhysician.area.Length)))
                errors.Add("Special character not allowed");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Specialization Master //
        public static ErrorResponse Insertspecializatiomaster(Tblspecialization tblspecialization)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (string.IsNullOrEmpty(tblspecialization.specialization) || tblspecialization.specialization.TrimStart() == string.Empty)
                errors.Add("Specialization Name is required");
            if (ContainsSpecialCharacters(tblspecialization.specialization, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Specialization Name");
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

        // Common Master //
        public static ErrorResponse InsertTitlemaster(TblName tblTitle)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (tblTitle.CommonNo == 0)
                errors.Add("Common No is required");
            if (string.IsNullOrEmpty(tblTitle.commonValue) || tblTitle.commonValue.TrimStart() == string.Empty)
                errors.Add("Common Value is required");
            if (tblTitle.sequenceNo == 0)
                errors.Add("Sequence No is required");
            if (ContainsSpecialCharacters(tblTitle.sequenceNo.ToString(), pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Sequence No");
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