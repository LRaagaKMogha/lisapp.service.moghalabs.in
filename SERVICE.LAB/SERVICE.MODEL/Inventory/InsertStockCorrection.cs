using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class InsertStockCorrection
    {
        public int StockCorrectionNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
        public int OpenQty { get; set; }
        public int CloseQty { get; set; }
        public int AdjustQty { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int BranchNo { get; set; }
        public int UserNo { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
    }
    public class GetProductStockRequest
    {
        public int VenueNo { get; set; }
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
    }
    public class GetProductStockResponse
    {
        public int RowNo { get; set; }
        public string productName { get; set; }
        public Int64 productNo { get; set; }
        public int consUnitNo { get; set; }
        public string consUnit { get; set; }
        public string BatchNo { get; set; }
        public string expDate { get; set; }
        public int OpenQty { get; set; }
        public int CloseQty { get; set; }
        public int AdjustQty { get; set; }
        public int consQty { get; set; }
        public decimal AvgPurcRateWTX { get; set; }
        public decimal AvgPurcRateWOT { get; set; }
    }
    public class InsertStockAdjustment
    {
        public int StockAdjustNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
        public int UserNo { get; set; }
        public string Reason { get; set; }
        public List<ProductBatchExpiryList> ProductDetails { get; set; }
        public string MenuType { get; set; }
    }
    public class ProductBatchExpiryList
    {
        public string BatchNo { get; set; }
        public string ExpDate { get; set; }
        public int AdjustQty { get; set; }
        public int CloseQty { get; set; }
        public decimal AvgPurcRateWTX { get; set; }
        public decimal AvgPurcRateWOT { get; set; }
    }

    public class GetStockAdjustProductDetailsResponse
    {
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public Int16 StoreNo { get; set; }
        public string StoreName { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public List<GetProductStockResponse> productDetails { get; set; }
    }
    public class LstStockAdjustProductDetailsResponse
    {
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public Int16 StoreNo { get; set; }
        public string StoreName { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public string productDetails { get; set; }
    }
    public class GetStoreStockProductListResponse
    {
        public int RowNo { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
    }
    public class InsertStockConsumption
    {
        public int ConsMastNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int UserNo { get; set; }
        public string MenuType { get; set; }
        public List<ConsumptionProductList> ProductDetails { get; set; }
    }

    public class ConsumptionProductList
    {
        public Int64 ProductNo { get; set; }
        public int ConsUnitNo { get; set; }
        public string BatchNo { get; set; }
        public string ExpDate { get; set; }
        public int ConsQty { get; set; }
        public decimal AvgPurcRateWTX { get; set; }
        public decimal AvgPurcRateWOT { get; set; }
    }
    public class GetConsumptionListRequest : GetCommonMasterRequest
    {
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
    }
    public class GetConsumptionProductListResponse
    {
        public int RowNo { get; set; }
        public Int64 productNo { get; set; }
        public string productName { get; set; }
        public Int16 consUnitNo { get; set; }
        public string consUnit { get; set; }
        public string? batchNo { get; set; }
        public string? expDate { get; set; }
        public Int16 consQty { get; set; } = 0;
        public int closeQty { get; set; } = 0;
        public decimal AvgPurcRateWTX { get; set; }
        public decimal AvgPurcRateWOT { get; set; }
    }
    public class ConsumptionDetailsResponse
    {
        public Int16 RowNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string? BranchName { get; set; }
        public Int16 StoreNo { get; set; }
        public string? StoreName { get; set; }
        public List<GetConsumptionProductListResponse> prdConsumptionLst { get; set; }
    }
    public class ConsumptionDetailsInListResponse
    {
        public Int16 RowNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string? BranchName { get; set; }
        public Int16 StoreNo { get; set; }
        public string? StoreName { get; set; }
        public string? prdConsumptionLst { get; set; }
    }
}
