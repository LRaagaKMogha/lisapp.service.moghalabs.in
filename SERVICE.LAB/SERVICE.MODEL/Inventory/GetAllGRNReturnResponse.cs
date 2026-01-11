using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Inventory
{
    public class GetAllGRNReturnResponse
    {
        public Int32 RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int MasterNo { get; set; }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public string GRNNo { get; set; }
        public int GRNMasterNo { get; set; }
        public string GRNName { get; set; }
        public string GRNDate { get; set; }
        public decimal Gross { get; set; }
        public decimal Discount { get; set; }
        public decimal Net { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string Remarks { get; set; }
        public string RefNo { get; set; }
        public decimal CCcharge { get; set; }
        public bool Status { get; set; }
        public int GRNReturnMasterNo { get; set; }
        public string GRNReturnNo { get; set; }
        public string GRNReturnDate { get; set; }
        public string GRNRtnStatus { get; set; }
        public string statusColorCode { get; set; }
        public bool isGRNRtnEditable { get; set; }
    }
}