using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _InvoiceRepository;
        public InvoiceController(IInvoiceRepository noteRepository)
        {
            _InvoiceRepository = noteRepository;
        }

        #region GetCustomerVisit
        [HttpPost]
        [Route("api/Invoice/GetCustomerVisit")]
        public ActionResult<lstcustomerVisit> GetCustomerVisit(reqinvoice req)
        {
            List<lstcustomerVisit> lst = new List<lstcustomerVisit>();
            try
            {
                var _errormsg = InvoiceValidation.GetCustomerVisit(req);
                if (!_errormsg.status)
                {
                    lst = _InvoiceRepository.GetCustomerVisit(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.GetCustomerVisit", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return Ok(lst);
        }
        #endregion
        #region GetCreditNoteVisit
        [HttpPost]
        [Route("api/Invoice/GetCreditNoteVisit")]
        public List<lstCreditNoteVisit> GetCreditNoteVisit(reqinvoice req)
        {
            List<lstCreditNoteVisit> lst = new List<lstCreditNoteVisit>();
            try
            {
                lst = _InvoiceRepository.GetCreditNoteVisit(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.GetCreditNoteVisit", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region CreditNoteReport
        [HttpPost]
        [Route("api/Invoice/GetCreditNoteReport")]
        public List<CreditNoteReport> GetCreditNoteReport(reqinvoice req)
        {
            List<CreditNoteReport> lst = new List<CreditNoteReport>();
            try
            {
                lst = _InvoiceRepository.GetCreditNoteReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.CreditNoteReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region InsertInvoiceCreate
        [HttpPost]
        [Route("api/Invoice/InsertInvoiceCreate")]
        public ActionResult<rtninvoice> InsertInvoiceCreate(objInvoiceCreate req)
        {
            rtninvoice obj = new rtninvoice();
            try
            {
                var _errormsg = InvoiceValidation.InsertInvoiceCreate(req);
                if (!_errormsg.status)
                {
                    obj = _InvoiceRepository.InsertInvoiceCreate(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.InsertInvoiceCreate-" + req.invoiceNo.ToString(), ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(obj);
        }
        #endregion

        #region InsertInvoiceCreditNote
        [HttpPost]
        [Route("api/Invoice/InsertInvoiceCreditNote")]
        public ActionResult<rtninvoiceCredit> InsertInvoiceCreditNote(objInvoiceCreditNote req)
        {
            rtninvoiceCredit obj = new rtninvoiceCredit();
            try
            {
                var _errormsg = InvoiceValidation.InsertInvoiceCreditNote(req);
                if (!_errormsg.status)
                {
                    obj = _InvoiceRepository.InsertInvoiceCreditNote(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.InsertInvoiceCreditNote-" + req.invoiceNo.ToString(), ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(obj);
        }
        #endregion    

        #region GetCustomerInvoice
        [HttpPost]
        [Route("api/Invoice/GetCustomerInvoice")]
        public ActionResult<lstCustomerInvoice> GetCustomerInvoice(reqinvoice req)
        {
            List<lstCustomerInvoice> lst = new List<lstCustomerInvoice>();
            try
            {
                //var _errormsg = InvoiceValidation.GetCustomerInvoice(req);
                //if (!_errormsg.status)
                //{
                    lst = _InvoiceRepository.GetCustomerInvoice(req);
                //}
                //else
                //    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.GetCustomerInvoice-" + req.customerNo.ToString(), ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return Ok(lst);
        }
        #endregion

        #region GetInvoiceInfo
        [HttpPost]
        [Route("api/Invoice/GetInvoiceInfo")]
        public objInvoice GetInvoiceInfo(reqinvoice req)
        {
            objInvoice obj = new objInvoice();
            try
            {
                obj = _InvoiceRepository.GetInvoiceInfo(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.GetInvoiceInfo-" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion

        #region InsertInvoicePayment
        [HttpPost]
        [Route("api/Invoice/InsertInvoicePayment")]
        public ActionResult<rtninvoicePayment> InsertInvoicePayment(objInvoicePayment req)
        {
            rtninvoicePayment obj = new rtninvoicePayment();
            try
            {
                var _errormsg = InvoiceValidation.InsertInvoicePayment(req);
                if (!_errormsg.status)
                {
                    obj = _InvoiceRepository.InsertInvoicePayment(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.InsertInvoicePayment-" + req.invoiceNo.ToString(), ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(obj);
        }
        #endregion

        #region SearchInvoiceNo
        [HttpPost]
        [Route("api/Invoice/SearchInvoiceNo")]
        public List<lstSearchInvoice> SearchInvoiceNo(reqinvoice req)
        {
            List<lstSearchInvoice> lst = new List<lstSearchInvoice>();
            try
            {
                lst = _InvoiceRepository.SearchInvoiceNo(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.SearchInvoiceNo-" + req.invoiceNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region GetInvoicePayment
        [HttpPost]
        [Route("api/Invoice/GetInvoicePayment")]
        public List<lstInvoicePayment> GetInvoicePayment(reqinvoice req)
        {
            List<lstInvoicePayment> lst = new List<lstInvoicePayment>();
            try
            {
                lst = _InvoiceRepository.GetInvoicePayment(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.GetInvoicePayment-" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region CancelInvoice
        [HttpPost]
        [Route("api/Invoice/InvoiceCancel")]
        public ActionResult<rtnCancelInvoice> InvoiceCancel(objInvoiceCancel req)
        {
            rtnCancelInvoice obj = new rtnCancelInvoice();
            try
            {
                var _errormsg = InvoiceValidation.InvoiceCancel(req);
                if (!_errormsg.status)
                {
                    obj = _InvoiceRepository.InvoiceCancel(req);
                }
                else
                {
                    return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.InsertInvoiceCreate-" + req.invoiceNo.ToString(), ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(obj);
        }
        #endregion

        [HttpPost]
        [Route("api/Invoice/UpdateTDSFlag")]
        public ActionResult<InvoiceTDSUpdateResponse> UpdateTDSFlag(InvoiceTDSUpdateRequest req)
        {
            InvoiceTDSUpdateResponse obj = new InvoiceTDSUpdateResponse();
            try
            {
                var _errormsg = InvoiceValidation.UpdateTDSFlag(req);
                if (!_errormsg.status)
                {
                    obj = _InvoiceRepository.UpdateTDSFlag(req);
                }
                else
                {
                    return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceController.UpdateTDSFlag-" + req.invoiceNo.ToString(), ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(obj);
        }
    }
}