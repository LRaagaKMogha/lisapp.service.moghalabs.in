using Service.Model;
using Service.Model.Common;
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
    public class MBMasterValidation
    {
        // Organism Type //
        public static bool NumberOnly(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static ErrorResponse InsertOrgtypemaster(orgtyperesponse orgtyinsertreq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(orgtyinsertreq.organismTypeName) || orgtyinsertreq.organismTypeName.TrimStart() == string.Empty)
                errors.Add("Organism Type Name is required");
            if (orgtyinsertreq.sequenceno == 0)
                errors.Add("Organism Type Sequence No is required");
            if (orgtyinsertreq.sequenceno <= 0 || orgtyinsertreq.sequenceno > 255)
                errors.Add("Organism Type Sequence No should be less than or equal to 1 to 255");
            else if (!NumberOnly(orgtyinsertreq.sequenceno.ToString().Trim()))
                errors.Add("Organism Type Sequence No must be numeric");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Organism Master //
        public static ErrorResponse InsertOrgmaster(orgresponse orginsertreq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(orginsertreq.organismname) || orginsertreq.organismname.TrimStart() == string.Empty)
                errors.Add("Organism Name is required");
            if ( orginsertreq.organismgroupno == 0)
                errors.Add("Organism Type No is required");
            if (orginsertreq.sequenceno == 0)
                errors.Add("Organism Sequence No is required");
            if (orginsertreq.sequenceno <= 0 || orginsertreq.sequenceno > 2000)
                errors.Add("Organism Sequence No should be less than or equal to 1 to 2000");
            else if (!NumberOnly(orginsertreq.sequenceno.ToString().Trim()))
                errors.Add("Organism Sequence No must be numeric");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Antibiotic //
        public static ErrorResponse Insertantimaster(antiresponse antinsertreq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(antinsertreq.antibioticName) || antinsertreq.antibioticName.TrimStart() == string.Empty)
                errors.Add("Antibiotic Name is required");
            if (antinsertreq.sequenceno == 0)
                errors.Add("Antibiotic Sequence No is required");
            if (antinsertreq.sequenceno <= 0 || antinsertreq.sequenceno > 255)
                errors.Add("Antibiotic Sequence No should be less than or equal to 1 to 255");
            else if (!NumberOnly(antinsertreq.sequenceno.ToString().Trim()))
                errors.Add("Antibiotic Sequence No must be numeric");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Organism Type Antibiotic Mapping //
        public static ErrorResponse InsertorgAntimaster(orgAntinsertresponse orgAntinsertreq)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (orgAntinsertreq.organismTypeNo == 0)
                errors.Add("Organism Type No is required");
            if (orgAntinsertreq.antibioticno == 0)
                errors.Add("Antibiotic No is required");
            if (orgAntinsertreq.sequenceno == 0)
                errors.Add("Organism Type Antibiotic Map Sequence No is required");
            if (orgAntinsertreq.sequenceno <= 0 || orgAntinsertreq.sequenceno > 255)
                errors.Add("Organism Type Antibiotic Map Sequence No should be less than or equal to 1 to 255");
            else if (!NumberOnly(orgAntinsertreq.sequenceno.ToString().Trim()))
                errors.Add("Organism Type Antibiotic Map Sequence No must be numeric");
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(",", errors);
            }
            return errorResponse;
        }

        // Organism Antibiotic Interp Range //
        public static ErrorResponse SaveOrganismAntibioticRange(orgAntiRange req)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            List<string> errors = new List<string>();

            if (req.lstorgAntiRange == null || req.lstorgAntiRange.Count == 0)
            {
                errors.Add("Organism Antibiotic Range list is required");
            }
            else
            {
                foreach (var item in req.lstorgAntiRange)
                {
                    if (item.organismno == 0)
                    {
                        errors.Add("Organism No is required for each item in the list");
                        break;
                    }
                }
            }
            if (errors.Count > 0)
            {
                errorResponse.status = true;
                errorResponse.message = string.Join(", ", errors);
            }
            return errorResponse;
        }
    }
}