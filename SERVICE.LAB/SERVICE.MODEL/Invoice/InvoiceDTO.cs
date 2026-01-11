using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class reqinvoice
    {
        public int flag { get; set; }
        public int invoiceNo { get; set; }
        public string fromdate { get; set; }
        public int searchby { get; set; }
        public string searchtext { get; set; }
        public string todate { get; set; }
        public int customerNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int loginType { get; set; }
        public string filterCode { get; set; }
        public int isAutoInvoiceGenerate { get; set; }
        public string type { get; set; }
        public string pageCode { get; set; }
    }
    public partial class lstSearchInvoice
    {
        public int invoiceNo { get; set; }
        public string displaytext { get; set; }
        public string searchdisplaytext { get; set; }
    }
    public partial class lstcustomerVisit
    {
        public int rowNo { get; set; }
        public bool isChecked { get; set; }
        public int patientBillTransactionNo { get; set; }
        public int patientBillNo { get; set; }
        public int patientVisitNo { get; set; }
        public string patientId { get; set; }
        public string fullName { get; set; }
        public string patientAge { get; set; }
        public string visitID { get; set; }
        public string visitDTTM { get; set; }
        public decimal grossAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal netAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public decimal dueAmount { get; set; }
        public string prevDueVisitDate { get; set; }
        public Int32 prevDueVisitCount { get; set; }
        public int customerNo { get; set; }
        public string CustomerName { get; set; }
        //public string ServiceName { get; set; }
        //public int testNo { get; set; }
    }
    public partial class lstCreditNoteVisit
    {
        public int rowNo { get; set; }
        public bool isChecked { get; set; }
        public int patientBillTransactionNo { get; set; }
        public int patientBillNo { get; set; }
        public int patientVisitNo { get; set; }
        public string patientId { get; set; }
        public string fullName { get; set; }
        public string patientAge { get; set; }
        public string visitID { get; set; }
        public string visitDTTM { get; set; }
        public decimal grossAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal netAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public decimal dueAmount { get; set; }
        public string prevDueVisitDate { get; set; }
        public Int32 prevDueVisitCount { get; set; }
        public int customerNo { get; set; }
        public string CustomerName { get; set; }
        public int InvoiceVisitsNo { get; set; }
    }
    public partial class objInvoiceCreate
    {
        public int invoiceNo { get; set; }
        public int customerNo { get; set; }
        public decimal billGross { get; set; }
        public decimal billDiscount { get; set; }
        public decimal billNet { get; set; }
        public decimal billCollected { get; set; }
        public decimal invoiceGross { get; set; }
        public int discountNo { get; set; }
        public int discountApprovedBy { get; set; }
        public string discountDescription { get; set; }
        public string discountType { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal invoiceDiscount { get; set; }
        public decimal invoiceNet { get; set; }
        public string invoiceDescription { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public List<lstcustomerVisit> lstcustomerVisit { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string datetype {  get; set; }
    }
    public partial class objInvoiceCreditNote
    {
        public string invoiceNo { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public List<lstCreditNoteVisit> lstCreditNoteVisit { get; set; }
    }
    public partial class rtninvoice
    {
        public int invoiceNo { get; set; }
        public string receiptNo { get; set; }
    }
    public partial class rtninvoiceCredit
    {
        public string CreditNo { get; set; }
        public string receiptNo { get; set; }
    }
    public partial class lstCustomerInvoice
    {
        public int rowNo { get; set; }
        public bool isChecked { get; set; }
        public int invoiceNo { get; set; }
        public int customerNo { get; set; }
        public string customerName { get; set; }
        public string receiptNo { get; set; }
        public string generateDTTM { get; set; }
        public string generateBy { get; set; }
        public decimal billGross { get; set; }
        public decimal billDiscount { get; set; }
        public decimal billNet { get; set; }
        public decimal billCollected { get; set; }
        public decimal invoiceGross { get; set; }
        public decimal invoiceDiscount { get; set; }
        public decimal invoiceNet { get; set; }
        public decimal totalPaymentDiscount { get; set; }
        public decimal totalTDS { get; set; }
        public decimal totalCreditNotes { get; set; }
        public decimal totalAdvanceAdjusted { get; set; }
        public decimal totalPaymentCollected { get; set; }
        public decimal balanceInvoicePayable { get; set; }
        public string invoiceDescription { get; set; }
        public int invoiceStatus { get; set; }
        public string invoiceStatusText { get; set; }
        public string dueDate { get; set; }
        public bool dueFlag { get; set; }
        public bool isTDSCollected { get; set; }
    }
    public partial class objInvoice
    {
        public int rowNo { get; set; }
        public int invoiceNo { get; set; }
        public int customerNo { get; set; }
        public string receiptNo { get; set; }
        //public string generateDTTM { get; set; }
        //public int generateBy { get; set; }
        //public decimal billGross { get; set; }
        //public decimal billDiscount { get; set; }
        //public decimal billNet { get; set; }
        //public decimal billCollected { get; set; }
        public decimal invoiceGross { get; set; }
        public decimal invoiceDiscount { get; set; }
        public decimal invoiceNet { get; set; }
        // public string notes { get; set; }
        public decimal totalPaymentDiscount { get; set; }
        public decimal totalPaymentCollected { get; set; }
        public decimal totalTDS { get; set; }
        public decimal totalCreditNotes { get; set; }
        public decimal totalAdvanceAdjusted { get; set; }
        public decimal totalInvoicePayable { get; set; }
        public decimal advanceOpeningBalance { get; set; }
        public decimal creditNotesBalance { get; set; }
    }
    public partial class objInvoicePayment
    {
        public int rowNo { get; set; }
        public int invoiceNo { get; set; }
        public int invoicePaymentNo { get; set; }
        public int customerNo { get; set; }
        public string paymentReceiptNo { get; set; }
        public string paymentDTTM { get; set; }
        public string paymentBy { get; set; }
        public string invoiceReceiptNo { get; set; }
        public decimal totalInvoicePayable { get; set; }
        public int discountNo { get; set; }
        public int discountApprovedBy { get; set; }
        public string discountDescription { get; set; }
        public string discountType { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal paymentDiscount { get; set; }
        public decimal paymentCollected { get; set; }
        public string paymentDescription { get; set; }
        public decimal tds { get; set; }
        public string tdsDescription { get; set; }
        public decimal creditNotes { get; set; }
        public string creditNotesDescription { get; set; }
        public decimal advanceAdjusted { get; set; }
        public decimal invoicePayable { get; set; }
        public decimal excess { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public List<invoicePaymentMode> lstinvoicePaymentMode { get; set; }
    }
    public partial class invoicePaymentMode
    {
        public int rowNo { get; set; }
        public string paymentMode { get; set; }
        public string ModeOfType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
    public partial class rtninvoicePayment
    {
        public int invoicePaymentNo { get; set; }
        public string receiptNo { get; set; }
    }
    public partial class lstInvoicePayment
    {
        public int rowNo { get; set; }
        public int invoicePaymentNo { get; set; }
        public string paymentReceiptNo { get; set; }
        public string paymentDTTM { get; set; }
        public string paymentBy { get; set; }
        public decimal totalInvoicePayable { get; set; }
        public decimal paymentDiscount { get; set; }
        public string discountDescription { get; set; }
        public decimal paymentCollected { get; set; }
        public decimal tds { get; set; }
        public string tdsDescription { get; set; }
        public decimal creditNotes { get; set; }
        public string creditNotesDescription { get; set; }
        public decimal advanceAdjusted { get; set; }
        public decimal invoicePayable { get; set; }
        public decimal excess { get; set; }
        public string paymentDescription { get; set; }
    }
    public partial class objInvoiceCancel
    {
        public int invoiceNo { get; set; }
        public int customerNo { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public string cancelReason { get; set; }
    }
    public partial class rtnCancelInvoice
    {
        public int invoiceNo { get; set; }
    }
    public class CreditNoteReport
    {
        public string CreditReceiptNo { get; set; }
        public decimal CreditBalance { get; set; }
        public DateTime CreditNoteDttm { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime GenerateDTTM { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }

    }
    public class VenueDetails
    { 
        public int venueNo { get; set; } 
        public int venueBranchNo { get; set; }
    }
    public class InvoiceTDSUpdateRequest
    {
        public int invoiceNo { get; set; }
        public bool isTDSCollected { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class InvoiceTDSUpdateResponse
    {
        public int status { get; set; }        
    }
}


