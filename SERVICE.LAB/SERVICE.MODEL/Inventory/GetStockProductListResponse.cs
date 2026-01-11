using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Inventory
{
    public class GetStockProductListResponse
    {
        public Int64 rowNo { get; set; }
        public int StoreNo { get; set; }
        public string productName { get; set; }
        public int productNo { get; set; }
        public string StoreName { get; set; }
        public string batchNo { get; set; }
        public string expireDate { get; set; }
        public int qty { get; set; }
        public decimal rate { get; set; }
        public decimal mrp { get; set; }
    }

    public class InsertStockUploadRequest
    {
        public List<GetStockProductListResponse> productList { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int createdby { get; set; }
        public int modifiedBy { get; set; }
        public bool status { get; set; }
    }
    public class GetProductMainbyDeptReq
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int StoreNo { get; set; }
        public int BranchNo { get; set; }
    }
    public class GetProductMainbyDeptRes
    {
        public Int64 RowNo { get; set; }
        public int ProductNo { get; set; }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public string? ProductName { get; set; }
    }
    public class GetStockReportRequest
    {
        public int pageIndex { get; set; }
        public int pageCount { get; set; }
        public int storeNo { get; set; }
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
        public int branchNo { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string Type { get; set; }
        public int CategoryNo { get; set; }

    }
    public class GetStockReportResponse
    {
        
        public decimal GrandTotalStockQuantity { get; set; }
        public Int64? rowNo { get; set; }   

        public string StoreName { get; set; } = string.Empty;

        public Int64 ProductMasterNo { get; set; }  

        public string ProductName { get; set; }  


        public decimal OpenValue { get; set; }  


        public decimal ReceiptValue { get; set; }   


        public decimal ReturnValue { get; set; }   


        public decimal IssueValue { get; set; }  


        public decimal ConsumValue { get; set; }   
        public decimal AdjustQty { get; set; }   

        public decimal AdjustValue { get; set; }  

        public decimal  CloseValue { get; set; }
        public decimal  OpenQty { get; set; }
        public decimal  ReceiptQty { get; set; }
        public decimal  ReturnQty { get; set; }
        public decimal  IssueQty { get; set; }
        public decimal  ConsumQty { get; set; }
        public decimal  CloseQty { get; set; }
                        
        public decimal  GrandTotalOutOfStock { get; set; }
            public int  TotalRecords {  get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpDate   { get; set; }



    }
}
