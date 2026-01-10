using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory
{
    public partial class CommonInventory
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userID { get; set; }        
    }
    public partial class InventoryReportOutput
    {
        public string PatientExportFile { get; set; }
        public string PatientExportFolderPath { get; set; }
        public string ExportURL { get; set; }
    }
}
