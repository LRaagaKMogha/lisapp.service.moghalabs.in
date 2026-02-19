using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IInvoiceRepository
    {
        List<lstcustomerVisit> GetCustomerVisit(reqinvoice req);
        Rtninvoice InsertInvoiceCreate(ObjInvoiceCreate req);
        List<LstCustomerInvoice> GetCustomerInvoice(reqinvoice req);
        ObjInvoice GetInvoiceInfo(reqinvoice req);
        RtninvoicePayment InsertInvoicePayment(ObjInvoicePayment req);
        List<lstSearchInvoice> SearchInvoiceNo(reqinvoice req);
        List<lstInvoicePayment> GetInvoicePayment(reqinvoice req);
        rtnCancelInvoice InvoiceCancel(ObjInvoiceCancel req);
        List<lstCreditNoteVisit> GetCreditNoteVisit(reqinvoice req);
        RtninvoiceCredit InsertInvoiceCreditNote(ObjInvoiceCreditNote req);
        List<CreditNoteReport> GetCreditNoteReport(reqinvoice req);
        InvoiceTDSUpdateResponse UpdateTDSFlag(InvoiceTDSUpdateRequest req);
        List<VenueDetails> InvoiceVenueDetails();
    }
}
