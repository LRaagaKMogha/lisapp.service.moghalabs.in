using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory
{
    public class tbl_IV_ProductSupplierMapping
    {
        public int ProductSupplierMappingNo { get; set; }
        public int Supplierno { get; set; }
        public int ProductNo { get; set; }
        public decimal Amount { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
}
