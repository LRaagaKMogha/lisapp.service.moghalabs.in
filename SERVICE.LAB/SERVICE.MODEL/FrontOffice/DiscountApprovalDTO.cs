using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetDiscountApprovalDto
    {
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int DiscountStatus { get; set; }
    }
    public class SaveDiscountApprovalDto
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int DiscountStatus { get; set; }
        public int DiscountApprovalNo { get; set; }
        public string? ApproveReason { get; set; }
        public string? FromScreen { get; set; }
    }
    public class SaveDiscountApprovalResponse
    {
        public int ApproveStatus { get; set; }
        public string? ApproveStatusText { get; set; }
    }
    public class GetDiscountApprovalResponse
    {
        public int RowNo { get; set; }
        public int DiscountApprovalNo { get; set; }
        public int RequestBranch { get; set; }
        public string? VenueBranchName { get; set; }
        public string? VisitID { get; set; }
        public string? VisitDTTM { get; set; }
        public string? PatientName { get; set; }
        public decimal BillAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? DiscountReason { get; set; }
        public int RequestUser { get; set; }
        public string? RequestedBy { get; set; }
        public Int16 DiscountStatus { get; set; }
        public string? AppravalReason { get; set; }
        public string? ApprovedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public string? FromScreen { get; set; }
        public decimal OldDiscount { get; set; }
    }
   
}

