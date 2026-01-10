using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class ReagentOpeningStockResponse
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int ReagentOSNo { get; set; }
        public int BranchNo { get; set; }
        public string BranchName { get; set; }
        public int storeNo { get; set; }
        public string storeName { get; set; }
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public string BatchNo { get; set; }
        public string ExpireDate { get; set; }
        public int Quantity { get; set; }
        public int NoOfTest { get; set; }
        public bool Status { get; set; }

    }
}
