using System;

namespace Service.Common.Core.Model
{
    public class PatientReport
    {
    }
    public partial class ReportDownloadListDTO
    {
        public string PatientName { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string VisitID { get; set; }
        public Nullable<int> ResultTypeNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool IsHeader { get; set; }
    }
}
