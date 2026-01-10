using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertBbReportParamDtoDetails
    (
        int IdentifyId,
       List<Dictionary<string, object>> Datatable,
    Dictionary<string, string> Paramerter,
    string ReportPath,
    string ExportPath,
    string ExportFormat
    );
}