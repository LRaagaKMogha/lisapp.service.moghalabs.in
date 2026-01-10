using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.IO;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IResultRepository _ResultRepository;
        public ResultController(IResultRepository noteRepository, IConfiguration config)
        {
            _ResultRepository = noteRepository;
            _config = config;
        }

        #region Common Result

        #region SearchResultVisit
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/SearchResultVisit")]
        public List<lstsearchresultvisit> SearchResultVisit([FromBody] requestsearchresultvisit req)
        {
            List<lstsearchresultvisit> lst = new List<lstsearchresultvisit>();
            try
            {
                lst = _ResultRepository.SearchResultVisit(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.SearchResultVisit", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        #region GetResultVisit
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetResultVisit")]
        public ActionResult GetResultVisit([FromBody] requestresultvisit req)
        {
            List<lstresultvisit> lst = new List<lstresultvisit>();
            try
            {
                var _errormsg = PatientResultValidation.GetResultVisit(req);
                if (!_errormsg.status)
                {
                    lst = _ResultRepository.GetResultVisit(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResultVisit", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(lst);
        }
        #endregion

        #region GetResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetResult")]
        public async Task<IActionResult> GetResult([FromBody] requestresult req)
        {
            objresult obj = new objresult();
            try
            {
                obj = await _ResultRepository.GetResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }

            return Ok(obj);
        }
        #endregion

        #region GetResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetNewResult")]
        public async Task<IActionResult> GetNewResult([FromBody] List<requestresult> req, int serviceNo = 0, string serviceType = "")
        {
            List<objresult> lstobj = new List<objresult>();
            try
            {
                foreach (requestresult item in req)
                {
                    objresult obj = await _ResultRepository.GetResult(item);
                    if (serviceNo != 0 && !string.IsNullOrEmpty(serviceType))
                    {
                        List<lstorderlist> lstorder = obj.lstvisit[0].lstorderlist.Where(x => x.serviceno == serviceNo && x.servicetype == serviceType).ToList();
                        if (lstorder.Any())
                        {
                            obj.lstvisit[0].lstorderlist = new List<lstorderlist>();
                            obj.lstvisit[0].lstorderlist.AddRange(lstorder);
                            lstobj.Add(obj);
                        }
                    }
                    else
                    {
                        lstobj.Add(obj);
                    }

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req[0].venueno, req[0].venuebranchno, req[0].userno);
            }
            return Ok(lstobj);
        }
        #endregion

        #region GetDeltaResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetDeltaResult")]
        public List<deltaresult> GetDeltaResult([FromBody] requestdeltaresult req)
        {
            List<deltaresult> lst = new List<deltaresult>();
            try
            {
                lst = _ResultRepository.GetDeltaResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetDeltaResult", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        #endregion

        #region InsertResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertResult")]
        public async Task<IActionResult> InsertResult(objresult req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                obj = await _ResultRepository.InsertResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
            //resultrtn obj = new resultrtn();
            //try
            //{
            //    var _errormsg = PatientResultValidation.InsertResult(req);
            //    if (!_errormsg.status)
            //    {
            //        obj = await _ResultRepository.InsertResult(req);
            //    }
            //    else
            //        return BadRequest(_errormsg);
            //}
            //catch (Exception ex)
            //{
            //    MyDevException.Error(ex, "ResultController.InsertResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            //}
            //return Ok(obj);
        }
        #endregion

        #region Insert New Result
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertNewResult")]
        public async Task<IActionResult> InsertNewResult([FromBody] List<objresult> req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                foreach (objresult item in req)
                {
                    obj = await _ResultRepository.InsertResult(item);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req[0].venueno, req[0].venuebranchno, req[0].userno);
            }
            return Ok(obj);
        }
        #endregion

        #region GetVisitHistoy
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetVisitHistoy")]
        public objresult GetVisitHistoy([FromBody] requestdeltaresult req)
        {
            objresult obj = new objresult();
            try
            {
                obj = _ResultRepository.GetVisitHistoy(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetVisitHistoy", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion
        #endregion

        #region Microbiology Result

        #region GetResultMB
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetResultMB")]
        public objresultmb GetResultMB(requestresult req)
        {
            objresultmb obj = new objresultmb();
            try
            {
                obj = _ResultRepository.GetResultMB(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResultMB", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion

        #region InsertResultMB
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertResultMB")]
        public ActionResult<resultrtn> InsertResultMB(objresultmb req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                var _errormsg = PatientResultValidation.InsertResultMB(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.InsertResultMB(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertResultMB", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
        }

        #endregion

        #region GetOrgTypeAntibiotic
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetOrgTypeAntibiotic")]
        public List<orgtypeantibiotic> GetOrgTypeAntibiotic(requestresult req)
        {
            List<orgtypeantibiotic> lst = new List<orgtypeantibiotic>();
            try
            {
                lst = _ResultRepository.GetOrgTypeAntibiotic(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetOrgTypeAntibiotic", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }
        #endregion

        #endregion

        #region Template Result

        #region GetResultTemplate
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetResultTemplate")]
        public objresulttemplate GetResultTemplate(requestresult req)
        {
            objresulttemplate obj = new objresulttemplate();
            try
            {
                obj = _ResultRepository.GetResultTemplate(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResultTemplate", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion

        #region InsertResultTemplate
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertResultTemplate")]
        public ActionResult<resultrtn> InsertResultTemplate(objresulttemplate req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                obj = _ResultRepository.InsertResultTemplate(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertResultTemplate", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion

        #endregion

        #region Recall

        #region GetRecall
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetRecall")]
        public objrecall GetRecall(requestresult req)
        {
            objrecall obj = new objrecall();
            try
            {
                obj = _ResultRepository.GetRecall(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetRecall", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return obj;
        }
        #endregion

        #region InsertRecall
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertRecall")]
        public ActionResult<recallResponse> InsertRecall(objrecall req)
        {
            recallResponse obj = new recallResponse();
            try
            {
                var _errormsg = ResultAckValidation.InsertRecall(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.InsertRecall(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertRecall", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
        }
        #endregion

        #endregion

        #region GetBulkResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetBulkResult")]
        public List<objresulttemplate> GetBulkResult(requestresultvisit req)
        {
            List<objresulttemplate> lst = new List<objresulttemplate>();
            try
            {
                lst = _ResultRepository.GetBulkResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetBulkResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return lst;
        }

        #region InsertBulkResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertBulkResult")]
        public resultrtn InsertBulkResult(objbulkresulttemplate req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                obj = _ResultRepository.InsertBulkResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertBulkResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion
        #endregion

        #region CovidWorkOrder

        #region GetCovidWorkOrder
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetCovidWorkOrder")]
        public List<covidresult> GetCovidWorkOrder(covidWorkOrderreq req)
        {
            List<covidresult> lst = new List<covidresult>();
            try
            {
                lst = _ResultRepository.GetCovidWorkOrder(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetCovidWorkOrder", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        #region InsertCovidWorkOrder
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertCovidWorkOrder")]
        public resultrtn InsertCovidWorkOrder(covidWorkOrder req)
        {
            resultrtn obj = new resultrtn();
            try
            {
                obj = _ResultRepository.InsertCovidWorkOrder(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertCovidWorkOrder", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return obj;
        }
        #endregion

        #endregion

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/BulkDocumentUpload")]
        public FrontOffficeResponse BulkDocumentUpload([FromBody] List<BulkDocumentUpload> lstjDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            int venueno = 0;
            int venuebno = 0;
            try
            {
                foreach (var objDTO in lstjDTO)
                {
                    venueno = objDTO.VenueNo;
                    venuebno = objDTO.VenueBranchNo;

                    var base64data = objDTO.ActualBinaryData;
                    var visitId = objDTO.ExternalVisitID;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var serviceNo = objDTO.ServiceNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.ActualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = objDTO.InsertPath;
                    string ConfigValuePath = objDTO.ConfigValuePath;
                    string webRootPath = _config.GetValue<string>(ConfigValuePath);
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = venueNo + "$$" + venuebNo + "$$" + visitId + "$$" + serviceNo + "$$" + actualfilename + "$$" + manualfilename + "." + format;
                        string fullPath = Path.Combine(newPath, fileName);

                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.BulkDocumentUpload", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetBulkDocumentUploaded")]
        public List<BulkDocumentUpload> GetBulkDocumentUploaded([FromBody] BulkDocumentUpload objDTO)
        {
            List<BulkDocumentUpload> lstresult = new List<BulkDocumentUpload>();
            BulkDocumentUpload result = new BulkDocumentUpload();
            try
            {
                string Pathinit = _config.GetValue<string>(objDTO.ConfigValuePath);
                var getpath = objDTO.InsertPath;
                var visitno = objDTO.PatientVisitNo;
                string newPath = Path.Combine(Pathinit, getpath);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new BulkDocumentUpload();
                            string path = filePaths[f].ToString();

                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);

                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            result.FileType = filePaths[f].ToString().Split('.')[1];
                            result.ExternalVisitID = filePaths[f].ToString().Split("$$")[2];
                            result.PatientVisitNo = visitno;
                            result.ServiceNo = Convert.ToInt32(filePaths[f].ToString().Split("$$")[3]);
                            result.ActualFileName = filePaths[f].ToString().Split("$$")[4];
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[5];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.UploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return lstresult;
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/DeleteBulkDocumentUploaded")]
        public int DeleteBulkDocumentUploaded([FromBody] BulkDocumentUpload objDTO)
        {
            int iResult = 0;
            try
            {
                if (System.IO.File.Exists(objDTO.FilePath))
                {
                    System.IO.File.Delete(objDTO.FilePath);
                    iResult = 1;
                }
                else
                {
                    iResult = 3;
                }
            }
            catch (Exception ex)
            {
                iResult = 2;
                MyDevException.Error(ex, "Result.DeleteBulkDocumentUploaded", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return iResult;
        }

        #region ApprovalDoctorList
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/ApprovalDoctorList")]
        public List<ApprovalDoctorResponse> ApprovalDoctorList(ApprovalDoctorRequest req)
        {
            List<ApprovalDoctorResponse> lst = new List<ApprovalDoctorResponse>();
            try
            {
                lst = _ResultRepository.ApprovalDoctorList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.ApprovalDoctorList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetPatientImpression")]
        public ActionResult<PatientImpressionResponse> GetPatientImpression(CommonFilterRequestDTO RequestItem)
        {
            List<PatientImpressionResponse> lstPatientInfoResponse = new List<PatientImpressionResponse>();
            try
            {
                var _errormsg = ReportValidation.GetPatientImpression(RequestItem);
                if (!_errormsg.status)
                {
                    lstPatientInfoResponse = _ResultRepository.GetPatientImpression(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.GetPatientImpression", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(lstPatientInfoResponse);
        }

        #region GetResultExceptUserMapped
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetResultExceptUserMapped")]
        public async Task<IActionResult> GetResultExceptUserMapped([FromBody] requestresult req)
        {
            objresult obj = new objresult();
            try
            {
                obj = await _ResultRepository.GetResultExceptUserMapped(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetResultExceptUserMapped", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }

            return Ok(obj);
        }
        #endregion

        //merging concept
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetMergedResult")]
        public List<mergeresultresponse> GetMergedResult(mergeresultrequest req)
        {
            List<mergeresultresponse> lst = new List<mergeresultresponse>();
            try
            {
                lst = _ResultRepository.GetMergedResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.GetMergedResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertMergedResult")]
        public savemergeresultresponse InsertMergedResult(savemergeresultrequest req)
        {
            savemergeresultresponse obj = new savemergeresultresponse();
            try
            {
                obj = _ResultRepository.InsertMergedResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.InsertMergedResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        //
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetCultureHistory")]
        public List<culturehistoryreponse> GetCultureHistory(culturehistoryrequest req)
        {
            List<culturehistoryreponse> obj = new List<culturehistoryreponse>();
            try
            {
                obj = _ResultRepository.GetCultureHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.GetCultureHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #region GetAnalyserResult
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetAnalyserResult")]
        public async Task<IActionResult> GetAnalyserResult(analyserrequestresult req)
        {
            objresult obj = new objresult();
            try
            {
                var _errormsg = AnalyserResultValidation.GetAnalyserResult(req);
                if (!_errormsg.status)
                {
                    obj = await _ResultRepository.GetAnalyserResult(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetAnalyserResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
        }
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/InsertAnalyserResult")]
        public async Task<IActionResult> InsertAnalyserResult(objresult req)
        {
            resultrtn obj = new resultrtn();
            try
            {
               // var _errormsg = AnalyserResultValidation.InsertAnalyserResult(req);
                //if (!_errormsg.status)

                //{
                    obj = await _ResultRepository.InsertAnalyserResult(req);
                //}
                //else
                  //  return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertAnalyserResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
        }
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetBulkResultEtry")]
        public ActionResult<objbulkresult> GetBulkResultEtry(analyserrequestresult req)
        {
            List<objbulkresult> obj = new List<objbulkresult>();
            try
            {
                var _errormsg = BulkResultValidation.GetBulkResultEtry(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.GetBulkResultEtry(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetBulkResultEtry", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(obj);
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/SaveBulkResultEtry")]
        public ActionResult<BulkResultSaveResponse> SaveBulkResultEtry(List<objbulkresult> req)
        {
            BulkResultSaveResponse obj = new BulkResultSaveResponse();
            try
            {
                var _errormsg = BulkResultValidation.SaveBulkResultEtry(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.SaveBulkResultEtry(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.SaveBulkResultEtry", ExceptionPriority.High, ApplicationType.APPSERVICE, req[0].venueno, req[0].venuebranchno, 0);
            }
            return Ok(obj);
        }
        #endregion

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/SaveCultureBulkResultEtry")]
        public ActionResult<BulkCultureResultSaveResponse> SaveCultureBulkResultEtry(SaveBulkCUltureResultRequest req)
        {
            BulkCultureResultSaveResponse obj = new BulkCultureResultSaveResponse();
            try
            {
                var _errormsg = BulkResultCultureValidation.SaveCultureBulkResultEtry(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.SaveCultureBulkResultEtry(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.SaveCultureBulkResultEtry", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return Ok(obj);
        }

        //
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetCultureBulkResultEtry")]
        public ActionResult<BulkCultureResultResponse> GetCultureBulkResultEtry(GetBulkCultureResultRequest req)
        {
            List<BulkCultureResultResponse> obj = new List<BulkCultureResultResponse>();
            try
            {
                var _errormsg = BulkResultCultureValidation.GetCultureBulkResultEtry(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.GetCultureBulkResultEtry(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.GetCultureBulkResultEtry", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return Ok(obj);
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetPatientImpressionReport")]
        public async Task<ActionResult<ReportOutput>> GetPatientImpressionReport(GetImpressionReportRequest RequestItem)
        {
            ReportOutput obj = new ReportOutput();
            try
            {
                var _errormsg = ReportValidation.GetPatientImpressionReport(RequestItem);
                if (!_errormsg.status)
                {
                    obj = await _ResultRepository.GetPatientImpressionReport(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "Result.GetPatientImpressionReport", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(obj);
        }
        #region VisitMerge 
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/SaveVisitMerge")]
        public ActionResult<InsertVisitMergeResponse> SaveVisitMerge(SaveResultforVisitMergeResponse req)
        {
            InsertVisitMergeResponse obj = new InsertVisitMergeResponse();
            try
            {
                var _errormsg = ResultAckValidation.SaveVisitMerge(req);
                if (!_errormsg.status)
                {
                    obj = _ResultRepository.SaveVisitMerge(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.SaveVisitMerge", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebno, 0);
            }
            return Ok(obj);
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetVisitMerge")]
        public ActionResult<GetResultforVisitMergeResponse> GetVisitMerge(VisitMergeRequest req)
        {
            List<GetResultforVisitMergeResponse> lstVisitMerge = new List<GetResultforVisitMergeResponse>();
            try
            {
                var _errormsg = ResultAckValidation.GetVisitMerge(req);
                if (!_errormsg.status)
                {
                    lstVisitMerge = _ResultRepository.GetVisitMerge(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetVisitMerge", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return Ok(lstVisitMerge);
        }

        #endregion
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetLogicComments")]
        public List<logicCommentsRespose> GetLogicComments(logicCommentsRequest req)
        {
            List<logicCommentsRespose> lst = new List<logicCommentsRespose>();
            try
            {
                lst = _ResultRepository.GetLogicComments(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultRepository.GetLogicComments", ExceptionPriority.High, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetExtrasubtestbasedformula")]
        public extrasubtestflagbasedformularesponse GetExtrasubtestbasedformula(extrasubtestflagbasedformularequest req)
        {
            extrasubtestflagbasedformularesponse obj = new extrasubtestflagbasedformularesponse();
            try
            {
                obj = _ResultRepository.GetExtrasubtestbasedformula(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetExtrasubtestbasedformula", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }

        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/GetOldResultThroughDIs")]
        public List<GetOldResultThroughDIResponse> GetOldResultThroughDIs(GetOldResultThroughDIRequest req)
        {
            List<GetOldResultThroughDIResponse> lst = new List<GetOldResultThroughDIResponse>();
            try
            {
                lst = _ResultRepository.GetOldResultThroughDIs(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetOldResultThroughDIs", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return lst;
        }

        #region Formula calculation
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Result/Calculator")]
        public List<ExternalResultCalculation> Calculator(List<ExternalResultCalculation> req)
        {
            List<ExternalResultCalculation> lst = null;
            try
            {
                var formulaTest = "";
                //var val = 0;

                formulaTest = "(";
                //for (var i = 0; i < req.formulajson.Count(); i++)
                //{
                //    if (req.formulajson[i].value == 0)
                //    {
                //        if (req.formulajson[i].parameterservicetype == "T")
                //        {

                //        }
                //        else if (req.formulajson[i].parameterservicetype == "S")
                //        {

                //        }

                //    }

                //    //for differential count - when i enterd result for 1st subtest, it should calculate and show the formula result, but its not shown because of empty result is not taken as 0
                //    if (req.formulajson[i].foperator == "")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "+")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "-")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "*")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "/")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "(")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == ")")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val;
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //    else if (req.formulajson[i].foperator == "^")
                //    {
                //        if (val != 0)
                //            formulaTest += req.formulajson[i].foperator + val; //Math.pow(height, 0.725)
                //        else
                //            formulaTest += req.formulajson[i].foperator;
                //    }
                //}
                formulaTest += ")";


            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.formula", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lst;
        }
        #endregion

        #region To Update Partial Entry/Validation Flag
        [CustomAuthorize("LIMSPATIENTRESULTS")]
        [HttpPost]
        [Route("api/Result/UpdatePartialResultFlag")]
        public async Task<IActionResult> UpdatePartialResultFlag(objUpdPartialEntryFlagRequest req)
        {
            objUpdPartialEntryFlagResponse objResponse = new objUpdPartialEntryFlagResponse();
            try
            {
                objResponse = await _ResultRepository.UpdatePartialResultFlag(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.InsertResult", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(objResponse);
        }
        #endregion
        #region Patient Result - Pending Test
        [HttpPost]
        [Route("api/Result/GetPendingVisitDetails")]
        public List<PendingVisitDetailsRes> GetPendingVisitDetails(PendingVisitDetailsReq req)
        {
            List<PendingVisitDetailsRes> lst = new List<PendingVisitDetailsRes>();
            try
            {
                lst = _ResultRepository.GetPendingVisitDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ResultController.GetPendingVisitDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }

            return lst;
        }
        #endregion
    }
}
