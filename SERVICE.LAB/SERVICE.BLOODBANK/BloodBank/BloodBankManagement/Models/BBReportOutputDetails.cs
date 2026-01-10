using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class BBReportOutputDetails
    {        
        public string PatientExportFile { get; set; }
        public string PatientExportFolderPath { get; set; }
        public string ExportURL { get; set; }
        public BBReportOutputDetails()
        {

        }

        public BBReportOutputDetails(
          string patientExportFile,
          string patientExportFolderPath,
          string exportURL

        )
        {
            PatientExportFile = patientExportFile;
            PatientExportFolderPath = patientExportFolderPath;
            ExportURL = exportURL;
        }

        public static ErrorOr<BBReportOutputDetails> Create(
            string patientExportFile,
            string patientExportFolderPath,
            string exportURL
            )
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new BBReportOutputDetails(patientExportFile,patientExportFolderPath,exportURL);
        }

        public static ErrorOr<BBReportOutputDetails> From(UpsertBBReportOutputDetails request)
        {
            return Create(request.PatientExportFile,request.PatientExportFolderPath,request.ExportURL);
        }
    }

}
