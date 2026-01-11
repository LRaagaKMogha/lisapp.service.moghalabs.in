using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;
using Serilog;
using Newtonsoft.Json;
using System.IO;

namespace Dev.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private IConfiguration _config;
        public InvoiceRepository(IConfiguration config) { _config = config; }
        public List<lstcustomerVisit> GetCustomerVisit(reqinvoice req)
        {
            List<lstcustomerVisit> lst = new List<lstcustomerVisit>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _customerNo = new SqlParameter("customerNo", req.customerNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _isAutoInviceGenerate = new SqlParameter("isAutoInvoiceGenerate", req.isAutoInvoiceGenerate == null ? 0 : req.isAutoInvoiceGenerate);
                    lst = context.GetCustomerVisit.FromSqlRaw(
                    "Execute dbo.pro_GetCustomerVisit @fromdate, @todate, @customerNo, @venueno, @venuebranchno, @isAutoInvoiceGenerate",
                    _fromdate, _todate, _customerNo, _venueno, _venuebranchno, _isAutoInviceGenerate).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetCustomerVisit - " + req.customerNo, ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public List<lstCreditNoteVisit> GetCreditNoteVisit(reqinvoice req)
        {
            List<lstCreditNoteVisit> lst = new List<lstCreditNoteVisit>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _InvoiceNo = new SqlParameter("InvoiceNo", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    lst = context.GetCreditNoteResponse.FromSqlRaw(
                    "Execute dbo.pro_GetCreditNoteDetails @InvoiceNo,@venueno,@venuebranchno",
                          _InvoiceNo, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetCreditNoteVisit", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public rtninvoice InsertInvoiceCreate(objInvoiceCreate req)
        {
            rtninvoice obj = new rtninvoice();
            CommonHelper commonUtility = new CommonHelper();
            try
            {
                string invoiceVisitsXML = "";
                if (req.lstcustomerVisit.Count > 0)
                {
                    invoiceVisitsXML = commonUtility.ToXML(req.lstcustomerVisit);
                }
                req.lstcustomerVisit.Clear();
                string invoiceCreateXML = "";
                invoiceCreateXML = commonUtility.ToXML(req);
            
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _customerNo = new SqlParameter("customerNo", req.customerNo);
                    var _invoiceCreateXML = new SqlParameter("invoiceCreateXML", invoiceCreateXML);
                    var _invoiceVisitsXML = new SqlParameter("invoiceVisitsXML", invoiceVisitsXML);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var lst = context.InsertInvoiceCreate.FromSqlRaw(
                        "Execute dbo.pro_InsertInvoiceCreate @customerNo,@invoiceCreateXML,@invoiceVisitsXML,@userNo,@venueno,@venuebranchno",
                          _customerNo, _invoiceCreateXML, _invoiceVisitsXML, _userNo, _venueno, _venuebranchno).ToList();

                    obj = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.InsertInvoiceCreate", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
        public rtninvoiceCredit InsertInvoiceCreditNote(objInvoiceCreditNote req)
        {
            rtninvoiceCredit obj = new rtninvoiceCredit();
            CommonHelper commonUtility = new CommonHelper();
            try
            {
                string invoiceVisitsXML = "";
                if (req.lstCreditNoteVisit.Count > 0)
                {
                    invoiceVisitsXML = commonUtility.ToXML(req.lstCreditNoteVisit);
                }

                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _invoiceVisitsXML = new SqlParameter("invoiceVisitsXML", invoiceVisitsXML);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var lst = context.InsertInvoiceCreditNote.FromSqlRaw(
                        "Execute dbo.pro_InsertInvoiceCreditNote @invoiceNo,@invoiceVisitsXML,@userNo,@venueno,@venuebranchno",
                          _invoiceNo, _invoiceVisitsXML, _userNo, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.InsertInvoiceCreditNote", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
        public List<lstCustomerInvoice> GetCustomerInvoice(reqinvoice req)
        {
            List<lstCustomerInvoice> lst = new List<lstCustomerInvoice>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("fromdate", req.fromdate);
                    var _todate = new SqlParameter("todate", req.todate);
                    var _flag = new SqlParameter("flag", req.flag);
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _customerNo = new SqlParameter("customerNo", req.customerNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _loginType = new SqlParameter("loginType", req.loginType);
                    var _filterCode = new SqlParameter("filterCode", req.filterCode);

                    lst = context.GetCustomerInvoice.FromSqlRaw(
                        "Execute dbo.pro_GetCustomerInvoice @Type, @FromDate, @ToDate, @Flag, @CustomerNo, " +
                        "@InvoiceNo, @VenueNo, @VenueBranchNo, @LoginType, @FilterCode",
                        _type, _fromdate, _todate, _flag, _customerNo, _invoiceNo,_venueno, _venuebranchno, _loginType, _filterCode).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetCustomerInvoice"+ req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public objInvoice GetInvoiceInfo(reqinvoice req)
        {
            objInvoice obj = new objInvoice();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var rtndblst = context.GetInvoiceInfo.FromSqlRaw(
                        "Execute dbo.pro_GetInvoiceInfo @invoiceNo,@venueno,@venuebranchno",
                           _invoiceNo, _venueno, _venuebranchno).ToList();
                    obj = rtndblst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetInvoiceInfo" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public rtninvoicePayment InsertInvoicePayment(objInvoicePayment req)
        {
            rtninvoicePayment obj = new rtninvoicePayment();
            CommonHelper commonUtility = new CommonHelper();

            string paymentModeXML = "";
            if (req.lstinvoicePaymentMode.Count > 0)
            {
                paymentModeXML = commonUtility.ToXML(req.lstinvoicePaymentMode);
            }
            req.lstinvoicePaymentMode.Clear();

            string invoicePaymentXML = "";
            invoicePaymentXML = commonUtility.ToXML(req);
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _customerNo = new SqlParameter("customerNo", req.customerNo);
                    var _invoicePaymentXML = new SqlParameter("invoicePaymentXML", invoicePaymentXML);
                    var _paymentModeXML = new SqlParameter("paymentModeXML", paymentModeXML);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var lst = context.InsertInvoicePayment.FromSqlRaw(
                        "Execute dbo.pro_InsertInvoicePayment @invoiceNo,@customerNo,@invoicePaymentXML,@paymentModeXML,@userNo,@venueno,@venuebranchno",
                          _invoiceNo, _customerNo, _invoicePaymentXML, _paymentModeXML, _userNo, _venueno, _venuebranchno).ToList();

                    obj = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.InsertInvoicePayment" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
        public List<lstSearchInvoice> SearchInvoiceNo(reqinvoice req)
        {
            List<lstSearchInvoice> lst = new List<lstSearchInvoice>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    lst = context.SearchInvoiceNo.FromSqlRaw(
                        "Execute dbo.pro_SearchInvoiceNo @searchby,@searchtext,@venueno,@venuebranchno",
                          _searchby, _searchtext, _venueno, _venuebranchno).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.SearchInvoiceNo", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        public List<lstInvoicePayment> GetInvoicePayment(reqinvoice req)
        {
            List<lstInvoicePayment> lst = new List<lstInvoicePayment>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    lst = context.GetInvoicePayment.FromSqlRaw(
                        "Execute dbo.pro_GetInvoicePayment @invoiceNo,@venueno,@venuebranchno",
                          _invoiceNo, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetInvoicePayment" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        public List<CreditNoteReport> GetCreditNoteReport(reqinvoice req)
        {
            List<CreditNoteReport> lst = new List<CreditNoteReport>();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("type", req.type);
                    var _fromdate = new SqlParameter("FromDate", req.invoiceNo);
                    var _todate = new SqlParameter("ToDate", req.invoiceNo);
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    lst = context.GetCreditNoteReport.FromSqlRaw(
                        "Execute dbo.Pro_GetCreditNoteReport @type,@FromDate,@ToDate,@invoiceNo,@venueno,@venuebranchno",
                          _type, _fromdate, _todate, _invoiceNo, _venueno, _venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.GetCreditNoteReport" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        public rtnCancelInvoice InvoiceCancel(objInvoiceCancel req)
        {
            rtnCancelInvoice obj = new rtnCancelInvoice();
            CommonHelper commonUtility = new CommonHelper();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _customerNo = new SqlParameter("customerNo", req.customerNo);
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _cancelReason = new SqlParameter("cancelReason", req.cancelReason);
                    
                    var lst = context.InvoiceCancel.FromSqlRaw(
                        "Execute dbo.pro_InvoiceCancel @InvoiceNo, @CustomerNo, @UserNo, @VenueNo, @VenueBranchNo, @CancelReason",
                          _invoiceNo, _customerNo, _userNo,  _venueno, _venuebranchno, _cancelReason).ToList();

                    obj = lst[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.InvoiceCancel", ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
        public List<VenueDetails> InvoiceVenueDetails()
        {
            List<VenueDetails> obj = new List<VenueDetails>();
            CommonHelper commonUtility = new CommonHelper();
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    obj = context.getInvoiceCreateVenueDetails.FromSqlRaw(
                        "Execute dbo.Pro_getInvoiceGenerateVenueDetails").ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.InvoiceGenerateVenueDetails", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0,0);
            }
            return obj;
        }

        public InvoiceTDSUpdateResponse UpdateTDSFlag(InvoiceTDSUpdateRequest req)
        {
            InvoiceTDSUpdateResponse obj = new InvoiceTDSUpdateResponse();
  
            try
            {
                using (var context = new InvoiceContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _invoiceNo = new SqlParameter("invoiceNo", req.invoiceNo);
                    var _isTDSCollected = new SqlParameter("isTDSCollected", req.isTDSCollected);                    
                    var _userNo = new SqlParameter("userNo", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var lst = context.UpdateTDSFlag.FromSqlRaw(
                        "Execute dbo.pro_UpdateTDSFlag @invoiceNo,@isTDSCollected,@userNo,@venueno,@venuebranchno",
                          _invoiceNo, _isTDSCollected, _userNo, _venueno, _venuebranchno).ToList();
                    if(lst != null)
                    obj.status = lst[0].status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InvoiceRepository.UpdateTDSFlag" + req.invoiceNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
    }
}
