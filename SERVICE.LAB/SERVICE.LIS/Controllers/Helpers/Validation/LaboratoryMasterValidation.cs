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

namespace DEV.API.SERVICE.Controllers
{
    public class LaboratoryMasterValidation
    {
        // Unit Master //
        public static ErrorResponse InsertUnitDetails(TblUnits req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
      
            if (string.IsNullOrEmpty(req.UnitsName) || req.UnitsName.TrimStart() == string.Empty)
                errors.Add("Units Name is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Container Master
        public static ErrorResponse Insertcontainermaster(TblContainer tblContainer)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (string.IsNullOrEmpty(tblContainer.containerName) || tblContainer.containerName.TrimStart() == string.Empty)
                errors.Add("Container Name is required");
            if (string.IsNullOrEmpty(tblContainer.containerCode) || tblContainer.containerCode.TrimStart() == string.Empty)
                errors.Add("Container Code is required");
            if (string.IsNullOrEmpty(tblContainer.description) || tblContainer.description.TrimStart() == string.Empty)
                errors.Add("Description is required");

            if (ContainsSpecialCharacters(tblContainer.containerName, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Container Name");
            }
            if (ContainsSpecialCharacters(tblContainer.containerCode, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Container Code");
            }
            if (ContainsSpecialCharacters(tblContainer.description, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Description");
            }
            if (ContainsSpecialCharacters(tblContainer.containerImagename, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Container Image Name");
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

        // Specimen Master //
        public static ErrorResponse InsertSampleDetails(TblSample Sampleitem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Sampleitem.SampleName) || Sampleitem.SampleName.TrimStart() == string.Empty)
                errors.Add("Sample Name is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Test Method Master //
        public static ErrorResponse InsertMethodDetails(TblMethod Methoditem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Methoditem.MethodName) || Methoditem.MethodName.TrimStart() == string.Empty)
                errors.Add("Method Name is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Sub-Test Header Master //
        public static ErrorResponse InsertSubtestheadermaster(TblSubtestheader testheader)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (string.IsNullOrEmpty(testheader.headerName) || testheader.headerName.TrimStart() == string.Empty)
                errors.Add("Sub-Test Header Name is required");
            if (string.IsNullOrEmpty(testheader.headerdisplaytext) || testheader.headerdisplaytext.TrimStart() == string.Empty)
                errors.Add("Sub-Test Header Display Text is required");
            if (testheader.sequenceNo == 0)
                errors.Add("Sub-Test Sequence No is required");
            if (testheader.sequenceNo <= 0 || testheader.sequenceNo > 255)
                errors.Add("Sub-Test Sequence No should be less than or equal to 1 to 255");
            if (ContainsSpecialCharacters(testheader.headerName, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Sub-Test Header Name");
            }
            if (ContainsSpecialCharacters(testheader.headerdisplaytext, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Sub-Test Header Display Text");
            }
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Main Department Master //
        public static ErrorResponse InsertMainDepartmentmaster(TblMainDepartment tblmaindepartment)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();
            string pattern = @"[!_@#$%^*()+=\[\]{}\\|;:'"",.<>?]";

            if (string.IsNullOrEmpty(tblmaindepartment.departmentname) || tblmaindepartment.departmentname.TrimStart() == string.Empty)
                errors.Add("Main Department Name is required");
            if (string.IsNullOrEmpty(tblmaindepartment.displayname) || tblmaindepartment.displayname.TrimStart() == string.Empty)
                errors.Add("Main Department Display Name is required");
            if (string.IsNullOrEmpty(tblmaindepartment.shortcode) || tblmaindepartment.shortcode.TrimStart() == string.Empty)
                errors.Add("Main Department Short Code is required");
            if (tblmaindepartment.sequenceno == 0)
                errors.Add("Main Department Sequence No is required");
            if (tblmaindepartment.sequenceno <= 0 || tblmaindepartment.sequenceno > 255)
                errors.Add("Main Department Sequence No should be less than or equal to 1 to 255");

            if (ContainsSpecialCharacters(tblmaindepartment.departmentname, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Main Department Name");
            }
            if (ContainsSpecialCharacters(tblmaindepartment.displayname, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Main Department Display Name");
            }
            if (ContainsSpecialCharacters(tblmaindepartment.shortcode, pattern))
            {
                errors.Add("Special Characters Are Not Allowed in Main Department Short Code");
            }
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Department Master //
        public static ErrorResponse InsertDepartmentDetails(TblDepartment Departmentitem)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Departmentitem.DepartmentName) || Departmentitem.DepartmentName.TrimStart() == string.Empty)
                errors.Add("Department Name is required");
            if (string.IsNullOrEmpty(Departmentitem.DepartmentCode) || Departmentitem.DepartmentCode.TrimStart() == string.Empty)
                errors.Add("Department Code is required");
            if (string.IsNullOrEmpty(Departmentitem.DepartmentDisplayText) || Departmentitem.DepartmentDisplayText.TrimStart() == string.Empty)
                errors.Add("Department Display Text is required");
            if (Departmentitem.DeptSequenceNo == 0)
                errors.Add("Department Sequence No is required");
            if (Departmentitem.MainDeptNo == 0)
                errors.Add("Main Department No is required");   

            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // LOINC Master //
        public static ErrorResponse InsertLoincMaster(InsloincReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.LoincCode) || req.LoincCode.TrimStart() == string.Empty)
                errors.Add("Loinc Code is required");
            if (string.IsNullOrEmpty(req.ComponentName) || req.ComponentName.TrimStart() == string.Empty)
                errors.Add("Component Name is required");
            if (req.ServiceNo == 0)
                errors.Add("Service No is required");
            if (string.IsNullOrEmpty(req.ServiceType) || req.ServiceType.TrimStart() == string.Empty)
                errors.Add("Service Type is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Snomed Master //
        public static ErrorResponse InsertSnomedMaster(InsSnomedReq req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(req.SnomedCode) || req.SnomedCode.TrimStart() == string.Empty)
                errors.Add("Snomed Code is required");
            if (string.IsNullOrEmpty(req.Description) || req.Description.TrimStart() == string.Empty)
                errors.Add("Description is required");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }
    }
}