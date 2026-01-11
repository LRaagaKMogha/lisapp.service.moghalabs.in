using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.External.WhatsAppChatBot
{
    public partial class PatientReportRequest
    {
        public string? Fullname { get; set; }
        public bool IsHeaderFooter { get; set; }
        public bool IsNABLlogo { get; set; }
        public string? OrderListNos { get; set; }
        public string? PageCode { get; set; }
        public string? PatientVisitNo { get; set; }
        public int Process { get; set; }
        public string? ResultTypeNos { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool IsDefault { get; set; }
    }
    public partial class PatientReportResponse
    {
        public string? PatientExportFile { get; set; }
        public string? PatientExportFolderPath { get; set; }
        public string? ExportURL { get; set; }
    }
}
