using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetIssueProductResponse
    {
        public int RowNo { get; set; }
        public Int32 ProductNo { get; set; }
       public string ProductName { get; set; }
        public int UnitNo { get; set; }
        public string UnitName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal Rate { get; set; }
        public int Stock { get; set; }
        public int Approval { get; set; }
        public int Issue { get; set; }
        public int Balance { get; set; }
        public string IndentId { get; set; }

        public int? IndentNo { get; set; }
        public DateTime? IndendDate { get; set; }
        public string Store { get; set; }
        public int? StoreNo { get; set; }
        public int? ProdProfileNo { get; set; }

        public string FromVenueBranchName { get; set; }
        public string ToVenueBranchName { get; set; }
        public string ToStoreName { get; set; }

        public int ToVenueBranchNo { get; set; }
        public int FromVenueBranchNo { get; set; }
        public int ToStoreNo { get; set; }
       // public int IssueNo { get; set; }
        public int TotalCloseQty { get; set; }

    }

    public class GetIssueProductByIssueNoResponse
    {
        public int RowNo { get; set; }
        public int IssueNo { get; set; }
        public string IssueCode { get; set; }
        public string Indent_Code { get; set; }
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public string PackName { get; set; }
        public string UnitsName { get; set; }
        public short IssuedQty { get; set; }
        public string BatchNo { get; set; }
        public string ExpDate { get; set; } // From SP it's a string (VARCHAR(10))
        public string FromBranch { get; set; }
        public string FromStore { get; set; }
        public string ToBranch { get; set; }
        public string ToStore { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; } // From SP it's a string (VARCHAR(10))
        public int FromVenueBranchNo { get; set; }
        public int ToVenueBranchNo { get; set; }
        public int FromStoreNo { get; set; }
        public int ToStoreNo { get; set; }
        public int StatusCode { get; set; }
        public decimal AlreadyReceivedQty { get; set; }
        

    }



    public class GetIssueProductRequest
    {
        public int fromBranch { get; set; }
        public int fromStore { get; set; }
        public int toBranch { get; set; }
        public int toStore { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool againstIndent { get; set; }
        public int productNo { get; set; }
        public int indentNo { get; set; }
        public int IssueNo { get; set; }

    }
    public class IssueProductRequest
    {
        public List<SaveIssueProductRequest> lstIssueProduct { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int IssueNo { get; set; }
        public string ActionMode { get; set; }
    }
    public class SaveIssueProductRequest { 
        public int frombNo { get; set; }
        public int fromStoreNo { get; set; }
        public int tobNo { get; set; }
        public int toStoreNo { get; set; }
        public int indentNo { get; set; }
        public string indentID { get; set; }
        public DateTime? indentDate { get; set; }
        public int indentDept { get; set; }
        public int productNo { get; set; }
        public int pquantity { get; set; }
        public int prodUnit { get; set; }
        public int prodPack { get; set; }
        public string prodBatch { get; set; }
        public DateTime? prodExpiry { get; set; }
        public int profileNo { get; set; }
        public int qtyStock { get; set; }
        public int qtyApproval { get; set; }
        public int qtyIssue { get; set; }
        public int qtyBalance { get; set; }
        public int issueQuantity { get; set; }
        public decimal mRP { get; set; }
        public bool isClosed { get; set; }
        public int rejectedQty { get; set; }
        public int alreadyAcceptedQty { get; set; }

    }
    public class SaveIssueProductResponse
    {
        public int IssueNo { get; set; }
    }
    public class GetDeptIssueProductRequest
    {
        public int fromBranch { get; set; }
        public int toBranch { get; set; }
        public int fromStore { get; set; }
        public int toStore { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public string? type { get; set; }
    }
    public class GetDeptIssueProductResponse
    {
        public int RowNo { get; set; }
        public string fromBranch { get; set; }
        public string fromStore { get; set; }
        public string toBranch { get; set; }
        public string toStore { get; set; }
        public string issueDate { get; set; }
        public string issueCode { get; set; }
        public string totalQty { get; set; }
        public int issueNo { get; set; }
        public int totalRecords { get; set; }
        public int pageIndex { get; set; }
        public int statusCode { get; set; }
    }
}



