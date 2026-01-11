using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.Report;
using Service.Model.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _IReportRepository;
        public ReportController(IReportRepository noteRepository)
        {
            _IReportRepository = noteRepository;
        }

        #region GetReport 
        /// <summary>
        /// GetReport
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/GetReport")]
        public async Task<ActionResult<ReportOutput>> GetReport(ReportDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetReport(req);
                if (!_errormsg.status)
                {
                    result = await _IReportRepository.GetReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/ReportKey-" + req.ReportKey, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userID);
            }
            return Ok(result);
        }
        #endregion

        #region GetSensitivityReport 
        /// <summary>
        /// GetReport
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/GetSensitivityReport")]
        public ActionResult<ReportOutput> GetSensitivityReport(CommonFilterRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetSensitivityReport(req);
                if (!_errormsg.status)
                {
                    result = _IReportRepository.GetSensitivityReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }             
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetSensitivityReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }
        #endregion
        #region GetStatisticalReport 
        /// <summary>
        /// GetReport
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/GetStatisticalReport")]
        public ActionResult<ReportOutput> GetStatisticalReport(CommonFilterRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                result = _IReportRepository.GetStatisticalReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetStatisticalReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }
        #endregion
        
        #region GetGridReport 
        /// <summary>
        /// GetGridReport
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/GetGridReport")]
        public string GetGridReport(ReportDTO req)
        {
            string result = String.Empty;
            try
            {
                result = _IReportRepository.GetGridReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetGridReport/ReportKey-" + req.ReportKey, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userID);
            }
            return result;
        }
        #endregion

        [HttpPost]
        [Route("api/Report/GetTATReport")]
        public List<TATResponse> GetTATReport(CommonFilterRequestDTO req)
        {
            List<TATResponse> result = new List<TATResponse>();
            try
            {
                result = _IReportRepository.GetTATReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetTATReport/visitNo-" + req.visitNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/Report/GetTATReportNew")]
        public List<TATResponseNew> GetTATReportNew(CommonFilterRequestDTO req)
        {
            List<TATResponseNew> result = new List<TATResponseNew>();
            try
            {
                result = _IReportRepository.GetTATReportNew(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetTATReportNew/visitNo-" + req.visitNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Report/GetTATReportDetails")]
        public List<TATReportDetailsResponse> GetTATReportDetails(CommonFilterRequestDTO req)
        {
            List<TATReportDetailsResponse> result = new List<TATReportDetailsResponse>();
            try
            {
                result = _IReportRepository.GetTATReportDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetTATReportDetails/visitNo-" + req.visitNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Report/GetICMRResult")]
        public List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO req)
        {
            List<GetICMRResponse> result = new List<GetICMRResponse>();
            try
            {
                result = _IReportRepository.GetICMRResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetICMRResult/CustomerNo-" + req.CustomerNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/Report/GetAudioLog")]
        public List<AuditLogResponse> GetAudioLog(CommonFilterRequestDTO RequestItem)
        {
            List<AuditLogResponse> lstAuditLogDTO = new List<AuditLogResponse>();
            try
            {
                lstAuditLogDTO = _IReportRepository.GetAudioLog(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetICMRResult/PatientNo-" + RequestItem.PatientNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstAuditLogDTO;
        }
        [HttpGet]
        [Route("api/Report/GetAuditHistory")]
        public List<AuditHistory> GetAuditHistory(int FirstAuditLogNo, int SecondAuditLogNo, int Type)
        {
            List<AuditHistory> lstAuditLogDTO = new List<AuditHistory>();
            try
            {
                lstAuditLogDTO = _IReportRepository.GetAuditHistory(FirstAuditLogNo,SecondAuditLogNo,Type);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetAuditHistory" , ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lstAuditLogDTO;
        }
        [HttpPost]
        [Route("api/Report/GetAdvancePayment")]
        public List<AdvancePaymentList> GetAdvancePayment(CommonFilterRequestDTO RequestItem)
        {
            List<AdvancePaymentList> objresult = new List<AdvancePaymentList>();
            try
            {
                objresult = _IReportRepository.GetAdvancePayment(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetAdvancePayment", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Report/InsertAdvancePayment")]
        public AdvancePaymentListResponse InsertAdvancePayment(AdvancePaymentListRequest RequestItem)
        {
            AdvancePaymentListResponse objresult = new AdvancePaymentListResponse();
            try
            {
                objresult = _IReportRepository.InsertAdvancePayment(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.InsertAdvancePayment", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }

        #region Cash Expenses
        [HttpPost]
        [Route("api/Report/GetCashExpenses")]
        public List<CashExpenseDTO> GetCashExpenses(GetCashExpenseParam RequestItem)
        {
            List<CashExpenseDTO> objresult = new List<CashExpenseDTO>();
            try
            {
                objresult = _IReportRepository.GetCashExpenses(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetCashExpenses", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Report/InsertCashExpenses")]
        public InsertCashExpenseDTO InsertCashExpenses(SaveCashExpenseDTO RequestItem)
        {
            InsertCashExpenseDTO objresult = new InsertCashExpenseDTO();
            try
            {
                objresult = _IReportRepository.InsertCashExpenses(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.InsertCashExpenses", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }
        #endregion
        [HttpPost]
        [Route("api/Report/GetReportbylist")]
        public async Task<ReportOutput> GetReportbylist(ReportDTO ReportItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                result = await _IReportRepository.GetReportbylist(ReportItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/ReportKey-" + ReportItem.ReportKey, ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        #region GetWorkloadReport 
        /// <summary>
        /// GetReport
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/GetWorkloadReport")]
        public ActionResult<ReportOutput> GetWorkloadReport(CommonFilterRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetWorkloadReport(req);
                if (!_errormsg.status)
                {
                    result = _IReportRepository.GetWorkloadReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetWorkloadReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Report/GetNonGynaeWorkLoadReport")]
        public ActionResult<ReportOutput> GetNonGynaeWorkLoadReport(CommonFilterRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetNonGynaeWorkLoadReport(req);
                if (!_errormsg.status)
                {
                    result = _IReportRepository.GetNonGynaeWorkLoadReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetNonGynaeWorkLoadReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }
        #endregion

        #region Get Cyto QC Report
        [HttpPost]
        [Route("api/Report/GetCytoQCReport")]
        public ActionResult<ReportOutput> GetCytoQCReport(CommonFilterRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetCytoQCReport(req);
                if (!_errormsg.status)
                {
                    result = _IReportRepository.GetCytoQCReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetCytoQCReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }
        #endregion


        /// <summary>
        /// Get Cyto Workload Report
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/Report/GetCytoWorkloadReport")]
        public ActionResult<ReportOutput> GetCytoWorkloadReport(SlidePrintingRequest req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetCytoWorkloadReport(req);
                if (!_errormsg.status)
                {
                    result = _IReportRepository.GetCytoWorkloadReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReport/GetCytoWorkloadReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Report/GetReqExpenses")]
        public List<GetReqExpensesResponse> GetReqExpenses(GetReqExpensesParam RequestItem)
        {
            List<GetReqExpensesResponse> objresult = new List<GetReqExpensesResponse>();
            try
            {
                objresult = _IReportRepository.GetReqExpenses(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetReqExpenses", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Report/ApproveExpenses")]
        public InsertCashExpenseDTO ApproveExpenses(ApproveExpenses RequestItem)
        {
            InsertCashExpenseDTO objresult = new InsertCashExpenseDTO();
            try
            {
                objresult = _IReportRepository.ApproveExpenses(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.ApproveExpenses", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Report/GetStaffBillingDetailsMIS")]
        public string GetStaffBillingDetailsMIS(UserFrontOfficeMIS RequestItem)
        {
            string result = string.Empty;
            try
            {
                result = _IReportRepository.GetStaffBillingDetailsMIS(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetStaffBillingDetailsMIS", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Report/GetUserwiseFrontOfficeMIS")]
        public string GetUserwiseFrontOfficeMIS(UserFrontOfficeMIS RequestItem)
        {
            string result = string.Empty;
            try
            {
                result = _IReportRepository.GetUserwiseFrontOfficeMIS(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportController.GetUserwiseFrontOfficeMIS", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
    }
}