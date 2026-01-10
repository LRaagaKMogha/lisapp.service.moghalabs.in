using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory
{
    public class ProductMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int productNo { get; set; }
        public int productTypeNo { get; set; }
        public int productCategoryNo { get; set; }
        public int genericNo { get; set; }
        public int medicineTypeNo { get; set; }
        public int medicineStrengthNo { get; set; }
        public int manufacturerNo { get; set; }
        public int hsnNo { get; set; }
    }
}
