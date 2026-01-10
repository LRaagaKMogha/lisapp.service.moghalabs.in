using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record BBReportOutput
     (
         int RowNo,
         string PatientExportFile,
         string PatientExportFolderPath,
         string ExportURL
     );
}