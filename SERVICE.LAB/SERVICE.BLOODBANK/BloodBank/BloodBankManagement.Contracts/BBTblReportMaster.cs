using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record BBTblReportMaster
     (
        int ReportNo,
    string ReportKey,
    string ReportName,
    string Description,
    string ProcedureName,
    string ReportPath,
    string ExportPath,
    string ExportURL,
    string Parameterstring,
    int? VenueNo,
    int? VenueBranchNo,
    bool? Status
     );
}