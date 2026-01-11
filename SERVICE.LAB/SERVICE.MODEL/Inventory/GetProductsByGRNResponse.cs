using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetProductsByGRNResponse
    {
        public Int64 RowNo { get; set; }
        public int GRNMasterNo { get; set; }
        public Int64 GRNProductNo { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public bool Free { get; set; }
        public int UnitNo { get; set; }
        public string UnitsName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int HSNNo { get; set; }
        public string HSNName { get; set; }
        public int GRNQty { get; set; }
        public int GrnRtnQty { get; set; }
        public int ReceivedQty { get; set; }
        public int BalanceQty { get; set; }
        public int CurrentValue { get; set; }
        public int RtnQty { get; set; }
        public int FreeQty { get; set; }
        public decimal RtnGrossAmount { get; set; }
        public decimal RtnNetAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public int DiscPercent { get; set; }
        public decimal DiscAmount { get; set; }
        public int TaxNo { get; set; }
        public string TaxName { get; set; }
        public int TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Gross { get; set; }
        public decimal Net { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
        public string BatchNo { get; set; }
        public string ExpireDate { get; set; }
    }

    public class PostGRN
    {
        public int GRNReturnNo { get; set; }
        public int StoreNo { get; set; }
        public int SupplierNo { get; set; }
        public string GrnDate { get; set; }
        public int GrnNo { get; set; }
        public List<GetProductsByGRNNo> ProductList { get; set; }
        public string Remarks { get; set; }
        public string Refno { get; set; }
        public decimal Cccharge { get; set; }
        public decimal GrnGrossAmount { get; set; }
        public decimal GrnTotalDiscAmount { get; set; }
        public decimal GrnTotalTaxAmount { get; set; }
        public decimal GrnNetAmount { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public int ModifiedBy { get; set; }
        public bool Status { get; set; }
        public string ReasonDesc { get; set; }
        public string MenuType { get; set; }
    }

    public class GetGRNBySupplierResponse
    {
        public Int64 RowNo { get; set; }
        public string GRNNo { get; set; }
        public int GRNMasterNo { get; set; }
        public string GRNName { get; set; }
    }
    public class GetProductsByGRNNo
    {
        public Int64 RowNo { get; set; }
        public int GRNMasterNo { get; set; }
        public Int64 GrnProductNo { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public bool Free { get; set; }
        public int UnitNo { get; set; }
        public string UnitsName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int HSNNo { get; set; }
        public string HSNName { get; set; }
        public int GRNQty { get; set; }
        public int GrnRtnQty { get; set; }
        public decimal Net { get; set; }
        public int Balance { get; set; }
        public int RtnQty { get; set; }
        public decimal Gross { get; set; }
        public decimal RtnGrossAmount { get; set; }
        public decimal RtnNetAmount { get; set; }
        public decimal RtnTotalAmount { get; set; }
        public decimal Rate { get; set; }
        public int DiscPercent { get; set; }
        public decimal DiscAmount { get; set; }
        public int TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public string Remarks { get; set; }
        public string ExpireDate { get; set; }
        public string BatchNo { get; set; }
    }
}

