using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IInvoiceRepository
    {
        List<lstcustomerVisit> GetCustomerVisit(reqinvoice req);
        rtninvoice InsertInvoiceCreate(objInvoiceCreate req);
        List<lstCustomerInvoice> GetCustomerInvoice(reqinvoice req);
        objInvoice GetInvoiceInfo(reqinvoice req);
        rtninvoicePayment InsertInvoicePayment(objInvoicePayment req);
        List<lstSearchInvoice> SearchInvoiceNo(reqinvoice req);
        List<lstInvoicePayment> GetInvoicePayment(reqinvoice req);
        rtnCancelInvoice InvoiceCancel(objInvoiceCancel req);
        List<lstCreditNoteVisit> GetCreditNoteVisit(reqinvoice req);
        rtninvoiceCredit InsertInvoiceCreditNote(objInvoiceCreditNote req);
        List<CreditNoteReport> GetCreditNoteReport(reqinvoice req);
        InvoiceTDSUpdateResponse UpdateTDSFlag(InvoiceTDSUpdateRequest req);
    }
}
