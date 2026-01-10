using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpsertBBTblReportMasterDetails
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
      int VenueNo,
      int VenueBranchNo,
      bool Status
    );
}