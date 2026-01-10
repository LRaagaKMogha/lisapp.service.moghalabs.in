using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory.Report
{
    public class InventoryReportDTO
    {
        public string ReportKey { get; set; }
        public string fileType { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userID { get; set; }
        public List<ReportKeyParamDTO> ReportParamitem { get; set; }
    }
    public class InventoryReportKeyParamDTO
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
