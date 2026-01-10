using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetStockCorrectionResponse
    {
        public int Sno { get; set; }
        public int pageindex { get; set; }
        public int totalRecords { get; set; }
        public Int64 MasterNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public int Opening { get; set; }
        public int Closing { get; set; }
        public int Adjust { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public Int16 storeNo { get; set; }
        public string storeName { get; set; }
        public string CreatedByName { get; set; }
    }
    public class GetStockAdjustmentResponse
    {
        public int rowNo { get; set; }
        public int pageindex { get; set; }
        public int totalRecords { get; set; }
        public int MasterNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public Int16 storeNo { get; set; }
        public string storeName { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public int AdjustQty { get; set; }
        public bool Status { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedOn { get; set; }
        public string SAStatus { get; set; }
        public string SAStatusColor { get; set; }
        public bool IsSAEditable { get; set; }
        public string TranDate { get; set; }
        public string TranRefNo { get; set; }
    }
}
