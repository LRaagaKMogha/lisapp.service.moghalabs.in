using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblReportMaster
    {
        public int ReportNo { get; set; }
        public string? ReportKey { get; set; }
        public string? ReportName { get; set; }
        public string? Description { get; set; }
        public string? ProcedureName { get; set; }
        public string? ReportPath { get; set; }
        public string? ExportPath { get; set; }
        public string? ExportURL { get; set; }
        public string? Parameterstring { get; set; }
        public int? VenueNo { get; set; }
        public int? VenueBranchNo { get; set; }
        public bool? Status { get; set; }
    }
}
