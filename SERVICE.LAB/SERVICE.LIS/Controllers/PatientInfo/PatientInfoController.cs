using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository.PatientInfo;
using DEV.Common;
using DEV.Model;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Dev.IRepository;
using Dev.Repository;

namespace DEV.API.SERVICE.Controllers.PatientInfo
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PatientInfoController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IExternalAPIRepository _IExternalAPIRepository;

        public PatientInfoController(IPatientInfoRepository patientInfoRepository, IConfiguration config, IExternalAPIRepository externalAPIRepository)
        {
            _patientInfoRepository = patientInfoRepository;
            _IExternalAPIRepository = externalAPIRepository;
            _config = config;
        }

        [CustomAuthorize("LIMSFRONTOFFICE,LIMSDEFAULT")]
        [HttpPost]
        [Route("api/PatientInfo/GetPatientInfoDetails")]
        public ActionResult GetPatientInfoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoResponse> objresult = new List<PatientInfoResponse>();
            try
            {
                var _errormsg = PatientInformationValidation.GetPatientInfoDetails(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _patientInfoRepository.GetPatientInfoDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientInfoDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/PatientInfo/GetPatientListDetails")]
        public ActionResult GetPatientListDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoListResponse> objresult = new List<PatientInfoListResponse>();
            try
            {
                var _errormsg = PatientInformationValidation.GetPatientInfoDetails(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _patientInfoRepository.GetPatientListDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientInfoDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/PatientInfo/GetCommonSearch")]
        public List<CustomSearchResponse> GetCommonSearch(CommonSearchRequest searchRequest)
        {
            List<CustomSearchResponse> objresult = new List<CustomSearchResponse>();
            try
            {
                objresult = _patientInfoRepository.GetCustomSearch(searchRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetCommonSearch Values - " + searchRequest.SearchKey, ExceptionPriority.Low, ApplicationType.APPSERVICE, searchRequest.VenueNo, searchRequest.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/PrintPatientReport")]
        public async Task<ReportOutput> PrintPatientReport(ReportRequestDTO requestDTO)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                result = await _patientInfoRepository.PrintPatientReport(requestDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.PrintPatientReport", ExceptionPriority.Low, ApplicationType.APPSERVICE, requestDTO.VenueNo, requestDTO.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/PatientInfo/UpdatePatientDetails")]
        public ActionResult<EditPatientResponse> UpdatePatientDetails(EditPatientRequest editPatientRequest)
        {
            EditPatientResponse editPatientResponse = new EditPatientResponse();
            try
            {
                var _errormsg = PatientInformationValidation.UpdatePatientDetails(editPatientRequest);
                if (!_errormsg.status)
                {
                    editPatientResponse = _patientInfoRepository.UpdatePatientDetails(editPatientRequest);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdatePatientDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, editPatientRequest.venueNo, editPatientRequest.venueBranchNo, 0);
            }
            return Ok(editPatientResponse);
        }

        [HttpPost]
        [Route("api/PatientInfo/GetPatientVisitHistory")]
        public List<PatientInfoResponse> GetPatientVisitHistory(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoResponse> objresult = new List<PatientInfoResponse>();
            try
            {
                objresult = _patientInfoRepository.GetPatientVisitHistory(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientVisitHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/GetServiceRejectReason")]
        public List<ReasonDetailsResponse> GetServiceRejectReason(ReasonDetailsRequest RequestItem)
        {
            List<ReasonDetailsResponse> objresult = new List<ReasonDetailsResponse>();
            try
            {
                objresult = _patientInfoRepository.GetServiceRejectReason(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetServiceRejectReason", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/UpdateMasterData")]
        public int UpdateMasterData(SyncMasterRequestDTO RequestItem)
        {
            int objresult = 0;
            try
            {
                objresult = _patientInfoRepository.UpdateMasterData(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.MasterDataSync", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.venueNo, RequestItem.venueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/GetPatientsMaster")]
        public List<PatientsMasterResponse> GetPatientsMaster(PatientsMasterRequest RequestItem)
        {
            List<PatientsMasterResponse> lstPatientsMasterResponse = new List<PatientsMasterResponse>();
            try
            {
                lstPatientsMasterResponse = _patientInfoRepository.GetPatientsMaster(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientsMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lstPatientsMasterResponse;
        }

        [HttpPost]
        [Route("api/PatientInfo/SavePatientsMaster")]
        public int SavePatientsMaster(PatientsMasterSave RequestItem)
        {
            int objresult = 0;
            try
            {
                objresult = _patientInfoRepository.SavePatientsMaster(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.SavePatientsMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/BulkUploadFile")]
        public int BulkUploadFile([FromBody] List<PatientDocUpload> lstjDTO)
        {
            int iOutput = 0;
            int venueno = 0;
            int venuebno = 0;
            int isdeleted = 0;
            try
            {
                foreach (var objDTO in lstjDTO)
                {
                    venueno = objDTO.VenueNo;
                    venuebno = objDTO.VenueBranchNo;

                    var base64data = objDTO.ActualBinaryData;
                    var patientNo = objDTO.PatientNo;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.ActualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = venueNo + "\\" + venuebNo + "\\" + patientNo;
                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppPatientMasterUpload = "PatientMasterUpload";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppPatientMasterUpload);
                    string patientmasteruplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                    string webRootPath = patientmasteruplod;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(newPath);
                        if (isdeleted == 0)
                        {
                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                        }
                        isdeleted = 1;
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        isdeleted = 1;
                        string fileName = manualfilename.Split('.')[0] + "###" + actualfilename + "." + format;
                        string fullPath = Path.Combine(newPath, fileName);

                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                }
                iOutput = 1;
            }
            catch (Exception ex)
            {
                iOutput = 100;
                MyDevException.Error(ex, "PatientInfoController.BulkUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return iOutput;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/ConvertToBase64")]
        public List<PatientDocUpload> ConvertToBase64([FromBody] PatientsMasterRequest objDTO)
        {
            List<PatientDocUpload> lstresult = new List<PatientDocUpload>();
            PatientDocUpload result = new PatientDocUpload();
            try
            {
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppPatientMasterUpload = "PatientMasterUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppPatientMasterUpload);
                string patientmastruplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = patientmastruplod;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                var patientno = objDTO.PatientNo;
                string folderName = venueNo + "\\" + venuebNo + "\\" + patientno;
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new PatientDocUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            var filenamesplit = filePaths[f].ToString().Split("###");
                            result.ActualFileName = filePaths[f].ToString().Split("###")[filenamesplit.Length - 1].Split(".")[0];
                            var ManualFileNamesplit = filenamesplit[0].Split("\\");
                            result.ManualFileName = ManualFileNamesplit[ManualFileNamesplit.Length - 1] + "." + result.FileType;
                            result.DocumentType = filePaths[f].ToString().Split("$$")[3] != null ? Convert.ToInt32(filePaths[f].ToString().Split("$$")[3]) : 0;
                            result.DocumentName = filePaths[f].ToString().Split("$$")[4];
                            result.ExpiryDate = filePaths[f].ToString().Split("$$")[5];
                            result.PlaceofIssue = result.ManualFileName.Split("$$")[6].Split('.')[0];
                            result.PatientNo = patientno;
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.ConvertToBase64", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return lstresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/CaptureUploadFile")]
        public int CaptureUploadFile([FromBody] PatientDocUpload objDTO)
        {
            int iOutput = 0;
            int venueno = 0;
            int venuebno = 0;
            int isdeleted = 0;
            try
            {
                if (objDTO != null)
                {
                    venueno = objDTO.VenueNo;
                    venuebno = objDTO.VenueBranchNo;

                    var base64data = objDTO.ActualBinaryData;
                    var patientNo = objDTO.PatientNo;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.ActualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = "PatientImage\\" + venueNo + "\\" + venuebNo + "\\" + patientNo;
                    //
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppPatientMasterUpload = "PatientMasterUpload";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppPatientMasterUpload);
                    string patientmastruplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = patientmastruplod;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(newPath);
                        if (isdeleted == 0)
                        {
                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            isdeleted = 1;
                        }
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = manualfilename.Split('.')[0] + "###" + actualfilename + "." + format;
                        string fullPath = Path.Combine(newPath, fileName);

                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                    iOutput = 1;
                }
            }
            catch (Exception ex)
            {
                iOutput = 100;
                MyDevException.Error(ex, "PatientInfoController.CaptureUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return iOutput;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/ConvertCaptureToBase64")]
        public List<PatientDocUpload> ConvertCaptureToBase64([FromBody] PatientsMasterRequest objDTO)
        {
            List<PatientDocUpload> lstresult = new List<PatientDocUpload>();
            PatientDocUpload result = new PatientDocUpload();
            try
            {
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppPatientMasterUpload = "PatientMasterUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppPatientMasterUpload);
                string patientmastruplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = patientmastruplod;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                var patientno = objDTO.PatientNo;
                string folderName = "PatientImage\\" + venueNo + "\\" + venuebNo + "\\" + patientno;
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new PatientDocUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            var filenamesplit = filePaths[f].ToString().Split("###");
                            result.ActualFileName = filePaths[f].ToString().Split("###")[filenamesplit.Length - 1].Split(".")[0];
                            var ManualFileNamesplit = filenamesplit[0].Split("\\");
                            result.ManualFileName = ManualFileNamesplit[ManualFileNamesplit.Length - 1] + "." + result.FileType;
                            result.DocumentType = 0;
                            result.DocumentName = "";
                            result.ExpiryDate = "";
                            result.PlaceofIssue = "";
                            result.PatientNo = patientno;
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.ConvertCaptureToBase64", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return lstresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/RemoveUploadedDocument")]
        public int RemoveUploadedDocument([FromBody] PatientDocUpload objDTO)
        {
            int iOutput = 0;
            try
            {
                if ((System.IO.File.Exists(objDTO.FilePath)))
                {
                    System.IO.File.Delete(objDTO.FilePath);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.RemoveUploadedDocument", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return iOutput;
        }

        [HttpPost]
        [Route("api/PatientInfo/InsertBooking")]
        public ExternalCommonResponse InsertBooking(ExternalBookingDto results)
        {
            ExternalCommonResponse result = new ExternalCommonResponse();
            try
            {
                result = _IExternalAPIRepository.InsertBooking(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.InsertBooking", ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return result;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/GetHCAppointsList")]
        public List<ExternalHCAppointment> GetHCAppointsList(CommonFilterRequestDTO RequestItem)
        {
            List<ExternalHCAppointment> objresult = new List<ExternalHCAppointment>();
            try
            {
                objresult = _IExternalAPIRepository.GetHCAppointsList(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetHCAppointsList", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/UpdateRiderStatus")]
        public ExternalCommonResponse UpdateRiderStatus(ExternalRiderStatusRequest results)
        {
            ExternalCommonResponse objresult = new ExternalCommonResponse();
            try
            {
                objresult = _IExternalAPIRepository.UpdateRiderStatus(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetHCAppointsList", ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/SavePatientMerge")]
        public ActionResult<PatientmergeResponseDTO> SavePatientMerge(PatientmergeDTO RequestItem)
        {
            PatientmergeResponseDTO objresult = new PatientmergeResponseDTO();
            try
            {
                var _errormsg = MassRegistrationValidation.SavePatientMerge(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _patientInfoRepository.SavePatientMerge(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            } 
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.SavePatientMerge", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebranchno, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/PatientInfo/UpdateSampleDetails")]
        public EditPatientResponse UpdateSampleDetails(List<EditSampleRequest> editSampleRequest)
        {
            EditPatientResponse editPatientResponse = new EditPatientResponse();
            try
            {
                editPatientResponse = _patientInfoRepository.UpdateSampleDetails(editSampleRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdateSampleDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, editSampleRequest[0].PatientVisitNo, editSampleRequest[0].UserNo, editSampleRequest[0].VenueNo);
            }
            return editPatientResponse;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/GetPatientSampleInfo")]
        public List<GetSampleResponse> GetPatientSampleInfo(GetSampleRequest RequestItem)
        {
            List<GetSampleResponse> objresult = new List<GetSampleResponse>();
            try
            {
                objresult = _patientInfoRepository.GetPatientSampleInfo(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientSampleInfo", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, 0, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/UpdateSampleDetailsNew")]
        public EditPatientResponseNew UpdateSampleDetailsNew(List<EditSampleRequestNew> editSampleRequest)
        {
            EditPatientResponseNew editPatientResponse = new EditPatientResponseNew();
            try
            {
                editPatientResponse = _patientInfoRepository.UpdateSampleDetailsNew(editSampleRequest[0]);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdateSampleDetailsNew", ExceptionPriority.Low, ApplicationType.APPSERVICE, editSampleRequest[0].PatientVisitNo, editSampleRequest[0].UserNo, 
                    editSampleRequest[0].VenueNo);
            }
            return editPatientResponse;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/GetPatientVisitActionHistory")]
        public List<GetPatientVisitActionHistoryResponse> GetPatientVisitActionHistory(CommonFilterRequestDTO RequestItem)
        {
            List<GetPatientVisitActionHistoryResponse> objresult = new List<GetPatientVisitActionHistoryResponse>();
            try
            {
                objresult = _patientInfoRepository.GetPatientVisitActionHistory(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetPatientVisitActionHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/PrintHcBill")]
        public async Task<ReportOutputhc> PrintHcBill(ReportRequestDTO req)
        {
            ReportOutputhc result = new ReportOutputhc();
            try
            {
                result = await _patientInfoRepository.PrintHcBill(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.PrintHcBill/visitNo-" + req.visitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/patientInfo/UpdateHCPatientDetails")]
        public ExternalupdateCommonResponse UpdateHCPatientDetails(UpdateHcpatient results)
        {
            ExternalupdateCommonResponse result = new ExternalupdateCommonResponse();
            try
            {
                result = _IExternalAPIRepository.UpdateHCPatientDetails(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdateHcpatient", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)results.VenueNo, (int)results.VenueBranchNo, (int)results.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/PatientInfo/GetHcDocumentsDetails")]
        public List<GetHcDocumentsDetailsResponse> GetHcDocumentsDetails(GetHcDocumentsDetailsRequest requestItem)
        {
            List<GetHcDocumentsDetailsResponse> objresult = new List<GetHcDocumentsDetailsResponse>();
            try
            {
                objresult = _patientInfoRepository.GetHcDocumentsDetails(requestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetHcDocumentsDetails",
                    ExceptionPriority.Low, ApplicationType.APPSERVICE,
                    requestItem.VenueNo, requestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/PatientInfo/HCConvertToBase64")]
        public List<BulkFileUpload> HCConvertToBase64([FromBody] FrontOffficeDTO objDTO)
        {
            List<BulkFileUpload> lstresult = new List<BulkFileUpload>();
            BulkFileUpload result = new BulkFileUpload();
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string HCPatientDocument = "UploadPathInit";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(HCPatientDocument);
                string hcpatientdocument = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";
                string Pathinit = hcpatientdocument;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                var patientno = objDTO.PatientNo;
                var visitId = objDTO.ExternalVisitID;
                var visitno = objDTO.PatientVisitNo;
                var format = objDTO.FileFormat;
                string folderName = venueNo + "\\" + venuebNo + "\\" + patientno + "\\" + visitId + "\\1";
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new BulkFileUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            result.ActualFileName = filePaths[f].ToString().Split("$$")[3];
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[5];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.HCConvertToBase64", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objDTO.VenueNo, (int)objDTO.VenueBranchNo, (int)objDTO.UserNo);
            }
            return lstresult;
        }

        [HttpPost]
        [Route("api/PatientInfo/UpdateStatusApptDate")]
        public UpdateStatusApptDateResponse UpdateStatusApptDate(UpdateStatusApptDateRequest results)
        {
            UpdateStatusApptDateResponse objresult = new UpdateStatusApptDateResponse();
            try
            {
                objresult = _IExternalAPIRepository.UpdateStatusApptDate(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdateStatusApptDate", ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, results.UserNo);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSFRONTOFFICE,LIMSDEFAULT")]
        [HttpPost]
        [Route("api/PatientInfo/GeteLabPatientInfoList")]
        public ActionResult GeteLabPatientInfoList(PatientInfoRequestDTO RequestItem)
        {
            List<PatientInfoeLabResponseDTO> objresult = new List<PatientInfoeLabResponseDTO>();
            try
            {
                var _errormsg = PatientInformationValidation.GeteLabPatientInfoDetails(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _patientInfoRepository.GeteLabPatientInfoList(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GeteLabPatientInfoList", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }
        [HttpPost]
        [Route("api/PatientInfo/GetSlotBooking")]
        public List<TestSlotBookingDTO> GetSlotBooking(CommonFilterRequestDTO RequestItem)
        {
            List<TestSlotBookingDTO> objresult = new List<TestSlotBookingDTO>();
            try
            {
                objresult = _IExternalAPIRepository.GetSlotBooking(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.GetSlotBooking", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/PatientInfo/InsertTestSlotBooking")]
        public TestSlotCommonResponse InsertTestSlotBooking(ExternalBookingDTO objDTO)
        {
            TestSlotCommonResponse result = new TestSlotCommonResponse();
            try
            {
                result = _IExternalAPIRepository.InsertTestSlotBooking(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.InsertTestSlotBooking", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objDTO.VenueNo, (int)objDTO.VenueBranchNo, (int)objDTO.UserNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/patientInfo/UpdateSlotBooking")]
        public SlotBookingupdateCResponse UpdateSlotBooking(UpdateHcpatient results)
        {
            SlotBookingupdateCResponse result = new SlotBookingupdateCResponse();
            try
            {
                result = _IExternalAPIRepository.UpdateSlotBooking(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoController.UpdateSlotBooking", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)results.VenueNo, (int)results.VenueBranchNo, (int)results.userNo);
            }
            return result;
        }
    }
}