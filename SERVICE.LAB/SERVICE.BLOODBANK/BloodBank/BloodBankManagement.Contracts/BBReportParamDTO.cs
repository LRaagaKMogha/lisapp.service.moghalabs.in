using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record BbReportParamDto
    (
        List<Dictionary<string, object>> datatable,
        Dictionary<string, string> paramerter,
        string ReportPath,
        string ExportPath,
        string ExportFormat
    );
}