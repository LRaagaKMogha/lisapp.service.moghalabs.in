using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class InsertReagentOpeningStockRequest
    {
        public int ReagentOSNo { get; set; }
        public int ProductNo { get; set; }        
        public int BranchNo { get; set; }
        public int MainDepartmentNo { get; set; }
        public int SubDepartmentNo { get; set; }
        public string BatchNo { get; set; }
        public string ExpireDate { get; set; }
        public int Quantity { get; set; }
        public int NoOfTest { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public int ModifiedBy { get; set; }
        public bool Status { get; set; }
       

    }
}
