using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertBBReportOutputDetails
    (
         string PatientExportFile,
         string PatientExportFolderPath,
         string ExportURL
    );
}