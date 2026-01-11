using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class IndentDetailsResponse
    {
        public int RowNo { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int IndentNo { get; set; }
        public string IndentCode { get; set; }
        public string IndentDate { get; set; }
        public int FromVenueBranchNo { get; set; }
        public string FromVenueBranch { get; set; }
        public int FromStoreNo { get; set; }
        public string FromStore { get; set; }
        public int ToVenueBranchNo { get; set; }
        public string ToVenueBranch { get; set; }
        public int ToStoreNo { get; set; }
        public string ToStore { get; set; }
        public Int16 TotalQty { get; set; }
        public bool IsEmergency { get; set; }
        public DateTime VerifiedOn { get; set; }
        public int VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        public DateTime AuthorisedOn { get; set; }
        public int AuthorisedBy { get; set; }
        public string AuthorisedByName { get; set; }
        public string Remarks { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public int IndType { get; set; }
        public bool isDraft { get; set; }
        public string RaisedOn { get; set; }
        public string RaisedBy { get; set; }
    }
    public class IndentProductDetailsResponse
    {
        public int rowNo { get; set; }
        public int indentProductNo { get; set; }
        public int indentNo { get; set; }
        public int productNo { get; set; }
        public int productProfileNo { get; set; }
        public string productSpecification { get; set; }
        public string requiredDate { get; set; }
        public Int16 requiredQty { get; set; }
        public Int16 verifiedQty { get; set; }
        public Int16 approvedQty { get; set; }
        public Int16 pRQty { get; set; }
        public Int16 pOQty { get; set; }
        public Int16 issueQty { get; set; }
        public Int16 receivedQty { get; set; }
        public int purchaseOrderNo { get; set; }
        public int purchaseRequestNo { get; set; }
        public string verifiedOn { get; set; }
        public int verifiedBy { get; set; }
        public string approvedOn { get; set; }
        public int approvedBy { get; set; }
        public string cancelledOn { get; set; }
        public int cancelledBy { get; set; }
        public bool status { get; set; }
        public string unit { get; set; }
        public string pack { get; set; }


    }
    public class IndentProductDetailsNewResponse
    {
        public int rowNo { get; set; }
        public int indentProductNo { get; set; }
        public int indentNo { get; set; }
        public int productNo { get; set; }
        public int productProfileNo { get; set; }
        public string productSpecification { get; set; }
        public DateTime requiredDate { get; set; }
        public Int16 requiredQty { get; set; }
        public Int16 verifiedQty { get; set; }
        public Int16 approvedQty { get; set; }
        public Int16 pRQty { get; set; }
        public Int16 pOQty { get; set; }
        public Int16 issueQty { get; set; }
        public Int16 receivedQty { get; set; }
        public int purchaseOrderNo { get; set; }
        public int purchaseRequestNo { get; set; }
        public DateTime verifiedOn { get; set; }
        public int verifiedBy { get; set; }
        public DateTime approvedOn { get; set; }
        public int approvedBy { get; set; }
        public DateTime cancelledOn { get; set; }
        public int cancelledBy { get; set; }
        public bool status { get; set; }
        public string unit { get; set; }
        public string pack { get; set; }
    }

    public class GetIndentDetailsRequest
    {
        public int indentno { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool status { get; set; }
        public int pageIndex { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Type { get; set; }
    }
    public class IndentDetailsSaveRequest
    {
        public int indentno { get; set; }
        public string indentCode { get; set; }
        public string indentDate { get; set; }
        public List<IndentProductDetailsResponse> lstIndentProductDetails { get; set; }
        public int fromVenueBranchNo { get; set; } 
        public int fromStoreNo { get; set; }
        public int toVenueBranchNo { get; set; }
        public int toStoreNo { get; set; }
        public int totalQty { get; set; }
        public bool isEmergency { get; set; }
        public string remarks { get; set; }
        public string verifiedOn { get; set; }
        public int verifiedBy { get; set; }
        public string authorisedOn { get; set; }
        public int authorisedBy { get; set; }
        public int userno { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool status { get; set; }
        public bool isDraft { get; set; }
    }
    public class IndentDetailsSaveResponse
    {
        public int indentno { get; set; }       
    }
}