using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class PatientDueResponse
    {
        public bool isChecked { get; set; } 
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int Row_num { get; set; }
        public int PatientNo { get; set; }
        public string PrimaryId { get; set; }
        public string PatientName { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string RegistrationDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal newDiscountAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal CancelledAmount { get; set; }
        public string RefferedBy { get; set; }
        public string branchName { get; set; }
        public Int16 IsDiscountApprovalReq { get; set; }
        public decimal OldDueAmount { get; set; }
        public string fontColor { get; set; }
        public string fontWeight { get; set; }
    }

    public partial class getrequest
    {
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int patientvisitno { get; set; }
    }


    public class dblCancelVisit
    {
        public int rowNo { get; set; }
        public int patientVisitNo { get; set; }
        public string patientId { get; set; }
        public string fullName { get; set; }       
        public string visitId { get; set; }
        public string visitDTTM { get; set; }
        public string referralType { get; set; }
        public string referredBy { get; set; }
        public int cancelled { get; set; }
        public int patientBillNo { get; set; }
        public decimal grossAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal netAmount { get; set; }        
        public decimal collectedAmount { get; set; }
        public decimal dueAmount { get; set; }
        public decimal cancelAmount { get; set; }
        public decimal refundAmount { get; set; }
        
        public int patientBillDetailsNo { get; set; }
        public int orderNo { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }       
        public string serviceName { get; set; }
        public decimal serviceGrossAmount { get; set; }
        public decimal serviceDiscountAmount { get; set; }
        public decimal blServiceDiscountAmount { get; set; }        
        public decimal serviceNetAmount { get; set; }
        public bool isCancelled { get; set; }
        public string cancelReason { get; set; }
        public string venueBranchName { get; set; }
        public decimal currDiscountAmount { get; set; }
    }
    public class CancelVisit
    {
        public int patientVisitNo { get; set; }
        public string patientId { get; set; }
        public string fullName { get; set; }
        public string visitId { get; set; }
        public string visitDTTM { get; set; }
        public string referralType { get; set; }
        public string referredBy { get; set; }
        public int cancelled { get; set; }
        public int patientBillNo { get; set; }
        public decimal grossAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal netAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public decimal dueAmount { get; set; }
        public decimal preCancelAmount { get; set; }
        public decimal preRefundAmount { get; set; }
        public decimal cancelAmount { get; set; }
        public decimal refundAmount { get; set; }        
        public List<CancelVisitTest> lstCancelVisitTest { get; set; }
        public int venueNo { get; set; }
        public int venueBranchBo { get; set; }
        public int userNo { get; set; }
        public string venueBranchName { get; set; }
        public bool IsExistsInvoice { get; set; }
        public decimal currDiscountAmount { get; set; }
    }
    public class CancelVisitTest
    {
        public int patientVisitNo { get; set; }
        public int patientBillDetailsNo { get; set; }
        public int orderNo { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public string serviceName { get; set; }
        public decimal serviceGrossAmount { get; set; }
        public decimal serviceDiscountAmount { get; set; }
        public decimal blServiceDiscountAmount { get; set; }
        public decimal serviceNetAmount { get; set; }
        public bool isPreCancelled { get; set; }
        public bool isCancelled { get; set; }
        public string cancelReason { get; set; }
    }

    public partial class rtnCancelTest
    {
        public int cancelTestNo { get; set; }
        public int patientVisitNo { get; set; }        
        public string receiptNo { get; set; }
    }

    public class GetReqCancelParam
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int didByUser { get; set; }
        public Int16 isApproved { get; set; }
        public int pageIndex { get; set; }
        public string? type { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
    }
    public class GetReqCancelResponse
    {
        public int RowNo { get; set; }
        public string RefundCancelLogNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int PatientBillNo { get; set; }
        public string PatientBillDetailsNo { get; set; }
        public decimal DueAmount { get; set; }
        public decimal CancelAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public string OrderNo { get; set; }
        public string OrderListNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public string CanceledBy { get; set; }
        public string ServiceGrossAmount { get; set; }
        public string ServiceDiscountAmount { get; set; }
        public decimal BlServiceDiscountAmount { get; set; }
        public string ServiceNetAmount { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalReason { get; set; }
        public string CancelReason { get; set; }
        public string VisitID { get; set; }
        public string PatientID { get; set; }
        public string Age { get; set; }
        public string ReferralType { get; set; }
        public string ReferrarName { get; set; }
        public string VisitDTTM { get; set; }
        public string FullName { get; set; }
        public string BillReceiptNo { get; set; }
        public decimal BillNetAmount { get; set; }
        public decimal BillGrossAmount { get; set; }
        public decimal BillDiscountAmount { get; set; }
        public decimal BillCancelAmount { get; set; }
        public decimal BillRefundAmount { get; set; }
        public int TotalRecords { get; set; }
        public int CanceledByUser { get; set; }
        public string cancellogid { get; set; }
        public string? CancelRequestdOn { get; set; }
        public string? ActionBy { get; set; }
        public string? ActionOn { get; set; }

    }
    public class UpdateReqCancelParam
    {
        public string refundCancelLogNo { get; set; }
        public int patientVisitNo { get; set; }
        public int patientBillNo { get; set; }
        public string patientBillDetailsNo { get; set; }
        public string orderNo { get; set; }
        public string orderListNo { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int didByUser { get; set; }
        public bool isApproved { get; set; }
        public string approvalReason { get; set; }
        public string cancellogid { get; set; }
    }
    public class UpdateReqCancelResponse
    {
        public string RefundCancelLogNo { get; set; }
        public int ApprovedStatus { get; set; }
    }
}



