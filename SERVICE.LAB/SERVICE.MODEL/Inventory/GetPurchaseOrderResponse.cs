using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory
{
    public class GetPurchaseOrderResponse
    {
        public Int32 Sno { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int PurchaseOrderNo { get; set; }
        public int BranchNo { get; set; }
        public string BranchName { get; set; }
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public int CurrencyNo { get; set; }
        public string CurrencyName { get; set; }
        public string RefNo { get; set; }
        public string RefDate { get; set; }
        public string PoNo { get; set; }
        public string PoDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal OtherCharge { get; set; }
        public decimal Net { get; set; }
        public decimal TotalccCharge { get; set; }
        public decimal Totalgross { get; set; }
        public int TotaldiscPercentage { get; set; }  
        public int POStatusNo { get; set; }
        public string POStatusText { get; set; }
        public bool Status { get; set; }
        public int PoQty { get; set; }
        public string indentIds { get; set; }
        public bool IsRateChanged { get; set; }
        public string statusColorCode { get; set; }
        public bool IsPOEditable { get; set; }
    }

    public class GetSupplierServiceDTO
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int TaxNo { get; set; }
        public string TaxName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int UnitNo { get; set; }
        public string UnitName { get; set; }
        public int HSNNO { get; set; }
        public string HsnName { get; set; }
        public decimal Amount { get; set; }
        public decimal Gross { get; set; }
        public bool Free { get; set; }
        public int Disc { get; set; }
        public decimal DiscAmt { get; set; }
        public int CCCharge { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public int avlquantity { get; set; }
        public DateTime? reqdt { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public bool IsExpDtRequired { get; set; }
        public bool IsBatchNoRequired { get; set; }
    }
    public class GetPurchaseDetailsDTO
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int TaxNo { get; set; }
        public string TaxName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int UnitNo { get; set; }
        public string UnitName { get; set; }
        public int HSNNO { get; set; }
        public string HsnName { get; set; }
        public decimal Amount { get; set; }
        public bool Free { get; set; }
        public int Disc { get; set; }
        public decimal DiscAmt { get; set; }
        public int CCCharge { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int TaxPercentage { get; set; }
    }

    public class InsertPurchaseOrder
    {
        public int purchaseOrderMasterNo { get; set; }
        public int branchNo { get; set; }
        public int storeNo { get; set; }
        public int supplierNo { get; set; }
        public int currencyNo { get; set; }
        public string quationNo { get; set; }
        public string quationdate { get; set; }
        public decimal totalbillAmount { get; set; }
        public decimal totaldiscPercentage { get; set; }
        public decimal totaldiscountvalue { get; set; }
        public decimal totalccCharge { get; set; }
        public decimal totalgross { get; set; }
        public decimal totaltax { get; set; }
        public decimal totalothercharges { get; set; }
        public decimal totalnet { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int createdby { get; set; }
        public int modifiedBy { get; set; }
        public bool status { get; set; }
        public List<POProductDetailsDTO> productlist { get; set; }
        public List<TaxList> taxList { get; set; }
        public List<otherChargeModal> oCList { get; set; }
        public List<Termsconditionlist> termsList { get; set; }
        public string menuCode { get; set; }
        public string indentIds { get; set; }
    }
    public class TaxList
    {
        public int taxNo { get; set; }
        public string taxName { get; set; }
        public decimal taxAmount { get; set; }
        public int taxPercentage { get; set; }
    }
    public class otherChargeModal
    {
        public Int64 rowNo { get; set; }
        public int oCNo { get; set; }
        public string oCName { get; set; }
        public decimal oCAmount { get; set; }
        public int grnMasterNo { get; set; }
    }
    public class Termsconditionlist
    {
        public Int64 rowNo { get; set; }
        public int termsNo { get; set; }
        public string termsCode { get; set; }
        public string termsType { get; set; }
        public string termsDescription { get; set; }
        public string termsName { get; set; }
    }

    public class GetPOProductDetailsDTO
    {
        public Int64 RowNo { get; set; }
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ReqDt { get; set; }
        public int Qty { get; set; }
        public bool FreeQty { get; set; }
        public decimal Amount { get; set; }
        public int TaxNo { get; set; }
        public int TaxPercent { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CCCharge { get; set; }

    }
    public class GetTaxDatilsResponse
    {
        public Int64 rowNo { get; set; }
        public int taxNo { get; set; }
        public string taxName { get; set; }
        public decimal taxAmount { get; set; }
        public int taxPercentage { get; set; }
    }
    public class POProductDetailsDTO
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int TaxNo { get; set; }
        public string TaxName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int UnitNo { get; set; }
        public string UnitName { get; set; }
        public int HSNNO { get; set; }
        public string HsnName { get; set; }
        public decimal Amount { get; set; }
        public bool Free { get; set; }
        public int Disc { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal CCCharge { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public int avlquantity { get; set; }
        public DateTime? reqdt { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public decimal MasterRate { get; set; }
        public bool isRateChanged { get; set; }
        public bool isExpDtRequired { get; set; }
        public bool isBatchNoRequired { get; set; }
    }
}
