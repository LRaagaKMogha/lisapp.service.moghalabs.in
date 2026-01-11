using System;

namespace Service.Model.Inventory
{
    public class GetPOBySupplierResponse
    {
        public Int64 RowNo { get; set; }
        public int PurchaseOrderNo { get; set; }
        public string PoName { get; set; }
        public int FromDeptNo { get; set; }
        public int SupplierNo { get; set; }
        public int StoreNo { get; set; }
    }
    public class GetAllGRNResponse
    {
        public Int32 RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int GRNMasterNo { get; set; }
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public int GrnTypeNo { get; set; }
        public string GrnType { get;set; }
        public string GRNNo { get; set; }
        public string DocumentNo { get; set; }
        public string GRNDate { get; set; }
        public int PurchaseOrderNo { get; set; }
        public string PONo { get; set; }
        public bool Status { get; set; }
        public bool WithPo { get; set; }
        public decimal? RoundOff { get; set; }
        public decimal? RoundOffInCurrency { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PODate { get; set; }
        public string GRNStatus { get; set; }
        public string statusColorCode { get; set; } 
        public bool IsRateChanged { get; set; }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public bool IsGRNEditable { get; set; }
        public bool IsInvoiceEditable { get; set; }
        public string invDate { get; set; }
        public string invNo { get; set; }
    }
}
