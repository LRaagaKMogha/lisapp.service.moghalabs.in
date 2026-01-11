using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Didstopia.PDFSharp.Pdf;
using Didstopia.PDFSharp.Pdf.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RtfPipe.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PatientReportController : ControllerBase
    {
        private readonly IPatientReportRepository _PatientReportRepository;
        public PatientReportController(IPatientReportRepository noteRepository)
        {
            _PatientReportRepository = noteRepository;
        }

        #region GetPatientReport 
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetPatientReport")]
        public ActionResult<lstpatientreport> GetPatientReport(requestpatientreport req)
        {
            List<lstpatientreport> lst = new List<lstpatientreport>();
            try
            {
                var _errormsg = PatientReportValidation.GetPatientReport(req);
                if (!_errormsg.status)
                {
                    lst = _PatientReportRepository.GetPatientReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetPatientReport/patientvisitno-" + req.patientvisitno, ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(lst);
        }
        #endregion

        #region Print PatientReport 
        /// <summary>
        /// Print Patient Report
        /// </summary>
        /// <param name="PatientItem"></param>
        /// <returns></returns>
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/PrintPatientReport")]
        public async Task<ActionResult<List<ReportOutput>>> PrintPatientReport(PatientReportDTO PatientItem)
        {
            List<ReportOutput> result = new List<ReportOutput>();
            try
            {
                var _errormsg = PatientReportValidation.PrintPatientReport(PatientItem);
                if (!_errormsg.status)
                {
                    result = await _PatientReportRepository.PrintPatientReport(PatientItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.PrintPatientReport/patientvisitno-" + PatientItem.patientvisitno, ExceptionPriority.High, ApplicationType.APPSERVICE, PatientItem.venueno, PatientItem.venuebranchno, PatientItem.userno);
            }
            return Ok(result);
        }
        #endregion
        
        //[CustomAuthorize("LIMSPATIENTREPORTS")]
        //[HttpPost]
        //[Route("api/PatientReport/SinglePrintPatientReport")]
        //public ActionResult<ReportOutput> SinglePrintPatientReport(List<PatientReportDTO> PatientItem)
        //{
        //    ReportOutput result = new ReportOutput();
        //    try
        //    {
        //        var _errormsg = PatientReportValidation.SinglePrintPatientReport(PatientItem);
        //        if (!_errormsg.status)
        //        {
        //            string FolderPath = string.Empty;
        //            string filename = string.Empty;
        //            string URLPath = string.Empty;
        //            List<ReportOutput> outputitem = new List<ReportOutput>();
        //            foreach (var item in PatientItem)
        //            {
        //                var output = _PatientReportRepository.PrintPatientReport(item);
        //                outputitem.AddRange(output);
        //            }
        //            using (PdfDocument outPdf = new PdfDocument())
        //            {
        //                if (outputitem.Count > 0)
        //                {
        //                    FolderPath = Path.GetDirectoryName(outputitem[0].PatientExportFolderPath);
        //                    filename = Path.GetFileName(outputitem[0].PatientExportFolderPath);
        //                    URLPath = outputitem[0].PatientExportFile.Replace(filename, "");
        //                }
        //                foreach (var data in outputitem)
        //                {
        //                    CopyPages(PdfReader.Open(data.PatientExportFolderPath, PdfDocumentOpenMode.Import), outPdf);
        //                }
        //                if (!string.IsNullOrEmpty(FolderPath))
        //                {
        //                    string finalfileName = Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
        //                    result.PatientExportFolderPath = FolderPath + "\\" + finalfileName;
        //                    result.PatientExportFile = URLPath + "/" + finalfileName;
        //                    outPdf.Save(result.PatientExportFolderPath);
        //                }
        //            }
        //            void CopyPages(PdfDocument from, PdfDocument to)
        //            {
        //                for (int i = 0; i < from.PageCount; i++)
        //                {
        //                    to.AddPage(from.Pages[i]);
        //                }
        //            }
        //        }
        //        else
        //            return BadRequest(_errormsg);
        //    }
        //    catch (Exception ex)
        //    {
        //        MyDevException.Error(ex, "PatientReportController.SinglePrintPatientReport", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
        //    }
        //    return result;
        //}
        //#endregion
        
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/SinglePrintPatientReport")]
        public async Task<ReportOutput> SinglePrintPatientReport(List<PatientReportDTO> PatientItem)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                string FolderPath = string.Empty;
                string filename = string.Empty;
                string URLPath = string.Empty;
                List<ReportOutput> outputitem = new List<ReportOutput>();
                foreach (var item in PatientItem)
                {
                    var output = await _PatientReportRepository.PrintPatientReport(item);
                    outputitem.AddRange(output);
                }
                using (PdfDocument outPdf = new PdfDocument())
                {
                    if (outputitem.Count > 0)
                    {
                        FolderPath = Path.GetDirectoryName(outputitem[0].PatientExportFolderPath);
                        filename = Path.GetFileName(outputitem[0].PatientExportFolderPath);
                        URLPath = outputitem[0].PatientExportFile.Replace(filename, "");
                    }
                    foreach (var data in outputitem)
                    {
                        CopyPages(PdfReader.Open(data.PatientExportFolderPath, PdfDocumentOpenMode.Import), outPdf);
                    }
                    if (!string.IsNullOrEmpty(FolderPath))
                    {
                        //string finalfileName = Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                        var finalfileName =  _PatientReportRepository.GetPdfFileName(PatientItem).Result;
                        result.PatientExportFolderPath = FolderPath + "\\" + finalfileName;
                        result.PatientExportFile = URLPath + "/" + finalfileName;
                        outPdf.Save(result.PatientExportFolderPath);
                    }
                }
                void CopyPages(PdfDocument from, PdfDocument to)
                {
                    for (int i = 0; i < from.PageCount; i++)
                    {
                        to.AddPage(from.Pages[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.SinglePrintPatientReport", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        #region Csa Transaction
        /// <summary>
        ///  Csa Transaction
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetCsaTransaction")]
        public List<TblCsatransaction> GetCsaTransaction(CsaRequest req)
        {
            List<TblCsatransaction> result = new List<TblCsatransaction>();
            try
            {
                result = _PatientReportRepository.GetCsaTransaction(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetCsaTransaction", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }
        #endregion

        #region Csa Transaction
        /// <summary>
        ///  Csa Transaction
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/InsertCSAAcknowledgement")]
        public int InsertCSAAcknowledgement(TblCsatransaction req)
        {
            int result = 0;
            try
            {
                result = _PatientReportRepository.InsertCSAAcknowledgement(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.InsertCSAAcknowledgement", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        #endregion

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/InsertReportLog")]
        public int InsertReportLog(PatientReportLog req)
        {
            int result = 0;
            try
            {
                result = _PatientReportRepository.InsertReportLog(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.InsertReportLog", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetReportLog")]
        public List<PatientReportLogRespose> GetReportLog(PatientReportLog req)
        {
            List<PatientReportLogRespose> lst = new List<PatientReportLogRespose>();
            try
            {
                lst = _PatientReportRepository.GetReportLog(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetReportLog/patientvisitno-" + req.PatientVisitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/PrintDelateReport")]
        public async Task<List<ReportOutput>> PrintDelateReport(PatientReportDTO requestDTO)
        {
            List<ReportOutput> result = new List<ReportOutput>();
            try
            {
                result = await _PatientReportRepository.PrintDelateReport(requestDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.PrintDelateReport", ExceptionPriority.Low, ApplicationType.APPSERVICE, requestDTO.venueno, requestDTO.venuebranchno, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetAuditTrailReport")]
        public ActionResult<GetAuditReportRes> GetAuditTrailReport(GetAuditReportReq req)
        {
            List<GetAuditReportRes> result = new List<GetAuditReportRes>();
            try
            {
                var _errormsg = PatientReportValidation.GetAuditTrailReport(req);
                if (!_errormsg.status)
                {
                    result = _PatientReportRepository.GetAuditTrailReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetAuditTrailReport", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetAuditTrailVisitHistory")]
        public AuditTrailVisitHistoryResponse GetAuditTrailVisitHistory(GetAuditTrailVisitReq req)
        {
            AuditTrailVisitHistoryResponse result = new AuditTrailVisitHistoryResponse();
            try
            {
                result = _PatientReportRepository.GetAuditTrailVisitHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetAuditTrailVisitHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        #region GetAmendedPatientReport 
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/GetAmendedPatientReport")]
        public ActionResult<lstamendedpatientreport> GetAmendedPatientReport(requestamendedpatientreport req)
        {
            List<lstamendedpatientreport> lst = new List<lstamendedpatientreport>();
            try
            {
                var _errormsg = PatientReportValidation.GetAmendedPatientReport(req);
                if (!_errormsg.status)
                {
                    lst = _PatientReportRepository.GetAmendedPatientReport(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetAmendedPatientReport/patientvisitno-" + req.patientvisitno, ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(lst);
        }
        #endregion

        #region Print Amended PatientReport 
        /// <summary>
        /// Print Amended Patient Report
        /// </summary>
        /// <param name="PatientItem"></param>
        /// <returns></returns>
        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/PrintAmendedPatientReport")]
        public async Task<ActionResult<ReportOutput>> PrintAmendedPatientReport(AmendedPatientReportDTO RptItem)
        {
            List<ReportOutput> result = new List<ReportOutput>();
            try
            {
                var _errormsg = PatientReportValidation.PrintAmendedPatientReport(RptItem);
                if (!_errormsg.status)
                {
                    result = await _PatientReportRepository.PrintAmendedPatientReport(RptItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.PrintAmendedPatientReport/patientvisitno-" + RptItem.patientvisitno, ExceptionPriority.High, ApplicationType.APPSERVICE, RptItem.venueno, RptItem.venuebranchno, RptItem.userno);
            }
            return Ok(result);
        }
        #endregion

        [CustomAuthorize("LIMSPATIENTREPORTS")]
        [HttpPost]
        [Route("api/PatientReport/SearchATSubCatyMasters")]
        public ActionResult<GetATSubCatyMasterSearchResponse> SearchATSubCatyMasters(GetATSubCatyMasterSearchReq req)
        {
            List<GetATSubCatyMasterSearchResponse> result = new List<GetATSubCatyMasterSearchResponse>();
            try
            {
                var _errormsg = PatientReportValidation.GetATSubCatyMasters(req);
                if (!_errormsg.status)
                {
                    result = _PatientReportRepository.GetATSubCatyMasters(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetAuditTrailReport", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }

        #region Print OPD Patient Report 
        [HttpPost]
        [Route("api/PatientReport/PrintOPDPatientReport")]
        public async Task<List<OPDReportOutput>> PrintOPDPatientReport(PatientReportOPDDTO PatientOPDItem)
        {
            List<OPDReportOutput> resultOPD = new List<OPDReportOutput>();
            try
            {
                resultOPD = await _PatientReportRepository.PrintOPDPatientReport(PatientOPDItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.PrintOPDPatientReport - AppointmentNo : " + PatientOPDItem.AppointmentNo, ExceptionPriority.High, ApplicationType.APPSERVICE, PatientOPDItem.VenueNo, PatientOPDItem.VenueBranchNo, PatientOPDItem.UserNo);
            }
            return resultOPD;
        }
        #endregion
    }
}
