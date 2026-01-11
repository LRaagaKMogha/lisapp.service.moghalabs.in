using Dev.IRepository;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Serilog;
using Service.Model;
using System.IO;
using DEV.Common;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Dev.Repository;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ManageSampleController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IManageSampleRepository _manageSampleRepository;
        public ManageSampleController(IManageSampleRepository manageSampleRepository, IConfiguration config)
        {
            _manageSampleRepository = manageSampleRepository;
            _config = config;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetManageSampleDetails")]
        public ActionResult GetManageSampleDetails(CommonFilterRequestDTO RequestItem)
        {
            List<GetManagesampleResponse> objresult = new List<GetManagesampleResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetManageSampleDetails(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _manageSampleRepository.GetManageSampleDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetManageSampleDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/ManageOptionalTests")]
        public ActionResult<CreateManageOptionalResponse> ManageOptionalTestinPackage(List<CreateManageOptionalTestRequest> manageoptionals)
        {
            CreateManageOptionalResponse response = new CreateManageOptionalResponse();
            try
            {
                foreach (var data in manageoptionals)
                {
                    var output = _manageSampleRepository.ManageOptionalTestPackage(data);
                    response.result = output.result;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.ManageOptionalTests", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);

        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateManageSample")]
        public ActionResult<CreateManageSampleResponse> CreateManageSample(List<CreateManageSampleRequest> createManageSample)
        {
            List<CreateManageSampleResponse> response = new List<CreateManageSampleResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.CreateManageSample(createManageSample);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.CreateManageSample(createManageSample);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateManageSample", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetSampleActionDetails")]
        public ActionResult GetSampleActionDetails([FromBody] SampleActionRequest req)
        {
            List<SampleActionDTO> response = new List<SampleActionDTO>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetSampleActionDetails(req);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.GetSampleActionDetails(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSampleActionDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return Ok(response);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateSampleACK")]
        public ActionResult<CreateSampleActionResponse> CreateSampleACK([FromBody] List<CreateSampleActionRequest> insertActionDTOs)
        {
            CreateSampleActionResponse response = new CreateSampleActionResponse();

            try
            {
                var _errormsg = SampleMaintainenceValidation.CreateSampleACK(insertActionDTOs);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.CreateSampleACK(insertActionDTOs);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateSampleACK", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/ManageSample/GetInterfaceTest")]
        public List<ExternalOrderDTO> GetInterfaceTest([FromBody] List<CreateManageSampleRequest> req)
        {
            List<ExternalOrderDTO> lst = new List<ExternalOrderDTO>();
            try
            {
                lst = _manageSampleRepository.GetInterfaceTest(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetInterfaceTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return lst;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/ManageSample/ACKInterfaceTest")]
        public int? ACKInterfaceTest(int venueNo, int venueBranchNo, string barcode, string testNo)
        {
            Log.Information(string.Format("ACKInterfaceTest (Controller)- Inputresults - VenueNo - {0} , VenueBranchNo - {1} , barcode - {2} , testno - {3}", venueNo, venueBranchNo, barcode, testNo));
            int? result = 0;
            try
            {
                result = _manageSampleRepository.ACKInterfaceTest(venueNo, venueBranchNo, barcode, testNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.ACKInterfaceTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetSampleOutSource")]
        public ActionResult GetSampleOutSource(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleOutsourceResponse> objresult = new List<GetSampleOutsourceResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetSampleOutSource(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _manageSampleRepository.GetSampleOutSource(RequestItem);
                }
                else
                    return BadRequest(_errormsg);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSampleOutSource", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateSampleOutsource")]
        public ActionResult CreateSampleOutsource([FromBody] List<CreateSampleOutSourceRequest> createOutSource)
        {
            Int64 response = 0;
            try
            {
                var _errormsg = SampleMaintainenceValidation.CreateSampleOutsource(createOutSource);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.CreateSampleOutsource(createOutSource);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateSampleOutsource", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetSampleOutSourceHistory")]
        public ActionResult<GetSampleOutSourceHistory> GetSampleOutsourceHistory([FromBody] GetSampleOutSourceHistoryRequest createOutSource)
        {
            List<GetSampleOutSourceHistory> response = new List<GetSampleOutSourceHistory>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetSampleOutsourceHistory(createOutSource);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.GetSampleOutsourceHistory(createOutSource);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSampleOutsourceHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetResultACK")]
        public ActionResult<GetSampleOutsourceResponse> GetResultACK(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleOutsourceResponse> objresult = new List<GetSampleOutsourceResponse>();
            try
            {
                var _errormsg = ResultAckValidation.GetResultACK(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _manageSampleRepository.GetResultACK(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetResultACK", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost, DisableRequestSizeLimit]
        [Route("api/ManageSample/UploadFile")]
        public ActionResult UploadFile()
        {
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppResultAckUpload = "ResultAckUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                               ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = resultackuplod;
                var file = Request.Form.Files[0];
                var requestInputs = Request.Form.Keys;

                string[] returnArray = new string[requestInputs.Count];
                requestInputs.CopyTo(returnArray, 0);

                var visitId = returnArray[0];
                var serviceId = returnArray[1];
                var venueNo = returnArray[2].Replace("V", "");
                var venueBNo = returnArray[3].Replace("B", "");
                var isincludedinreport = returnArray[4] != null && returnArray[4].ToString() == "true" ? "Yes" : "No";
                var fileformat = returnArray[5];
                var actualfilename = returnArray[6];
                string folderName = venueNo + "//" + visitId + "//" + serviceId;
                string webRootPath = Pathinit;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string[] files = Directory.GetFiles(newPath);
                if (files != null && files.Length > 0)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(newPath);
                    foreach (FileInfo item in di.GetFiles())
                    {
                        item.Delete();
                    }
                }
                if (file.Length > 0)
                {
                    string fileName = visitId + "$$" + serviceId + "$$" + venueNo + "$$" + actualfilename + "$$" + isincludedinreport + "." + fileformat;
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.UploadFile", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                return Ok("Upload Failed: " + ex.Message);
            }
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateResultACK")]
        public ActionResult<CreateOutSourceResponse> CreateResultACK([FromBody] List<CreateSampleOutSourceRequest> createOutSource)
        {
            CreateOutSourceResponse response = new CreateOutSourceResponse();
            try
            {
                var _errormsg = ResultAckValidation.CreateResultACK(createOutSource);
                if (!_errormsg.status)
                {
                    response = _manageSampleRepository.CreateResultACK(createOutSource);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateResultACK", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(response);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetSampletransfer")]
        public List<GetSampleTransferResponse> GetSampletransfer(GetSampleOutsourceRequest RequestItem)
        {
            List<GetSampleTransferResponse> objresult = new List<GetSampleTransferResponse>();
            try
            {
                objresult = _manageSampleRepository.GetSampleTransfer(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSampletransfer", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateSampleTransfer")]
        public Int64 CreateSampleTransfer([FromBody] List<CreateSampleTransterRequest> createOutSource)
        {
            Int64 response = 0;
            try
            {
                response = _manageSampleRepository.CreateSampleTransfer(createOutSource);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateSampleTransfer", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetBranchSampleReceive")]
        public List<GetbranchSampleReceiveResponse> GetBranchSampleReceive(GetBranchSampleReceiveRequest RequestItem)
        {
            List<GetbranchSampleReceiveResponse> objresult = new List<GetbranchSampleReceiveResponse>();
            try
            {
                objresult = _manageSampleRepository.GetBranchSampleReceive(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetBranchSampleReceive", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetSampleTransferReport")]
        public List<SampleReportResponse> GetSampleTransferReport(CommonFilterRequestDTO RequestItem)
        {
            List<SampleReportResponse> objresult = new List<SampleReportResponse>();
            try
            {
                objresult = _manageSampleRepository.GetSampleTransferReport(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSampleTransferReport", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CreateBranchSampleReceive")]
        public Int64 CreateBranchSampleReceive([FromBody] List<CreateBranchSampleReceiveRequest> createBranchReceive)
        {
            Int64 response = 0;
            try
            {
                response = _manageSampleRepository.CreateBranchSampleReceive(createBranchReceive);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CreateBranchSampleReceive", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpGet]
        [Route("api/ManageSample/ValidateBarcodeNo")]
        public bool ValidateBarcodeNo(string BarcodeNo, int venueNo, int venueBranchNo)
        {
            bool response = false;
            try
            {
                response = _manageSampleRepository.ValidateBarcodeNo(BarcodeNo, venueNo, venueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.ValidateBarcodeNo", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/UpdateMultiSampleRefRange")]
        public UpdateRefRangeResponse UpdateMultiSampleRefRange(UpdateRefRangeRequest req)
        {
            UpdateRefRangeResponse response = new UpdateRefRangeResponse();
            try
            {
                response = _manageSampleRepository.UpdateMultiSampleRefRange(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.UpdateMultiSampleRefRange", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/CheckPDFExists")]
        public int CheckPDFExists(GetSampleOutsourceRequest req)
        {
            int iOut = 2;
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppResultAckUpload = "ResultAckUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string ackpath = resultackuplod;
                ackpath = ackpath + "//" + req.VenueNo + "//" + req.SearchValue + "//" + req.ServiceNo;
                if (Directory.Exists(ackpath))
                {
                    string[] files = Directory.GetFiles(ackpath);
                    if (files != null && files.Length > 0)
                    {
                        string extension = Path.GetExtension(files[0]);

                        if (extension != null && extension.ToLower() == ".pdf")
                        {
                            iOut = 1;
                        }
                        else
                        {
                            iOut = 0;
                        }
                    }
                    else
                    {
                        iOut = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.CheckPDFExists", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return iOut;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/DeleteFileExists")]
        public int DeleteFileExists(GetSampleOutsourceRequest req)
        {
            int iOut = 2;
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppResultAckUpload = "ResultAckUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string ackpath = resultackuplod;
                ackpath = ackpath + "//" + req.VenueNo + "//" + req.SearchValue + "//" + req.ServiceNo;
                if (Directory.Exists(ackpath))
                {
                    string[] files = Directory.GetFiles(ackpath);
                    if (files != null && files.Length > 0)
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(ackpath);
                        foreach (FileInfo item in di.GetFiles())
                        {
                            item.Delete();
                        }
                    }
                    else
                    {
                        iOut = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.DeleteFileExists", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return iOut;
        }

        //multi upload
        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/ConvertToBase64")]
        public List<BulkFileUpload> ConvertToBase64(GetSampleOutsourceRequest req)
        {
            List<BulkFileUpload> lstresult = new List<BulkFileUpload>();
            BulkFileUpload result = new BulkFileUpload();
            try
            {
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppResultAckUpload = "ResultAckUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = resultackuplod;
                var visitId = !String.IsNullOrEmpty(req.SearchValue) ? req.SearchValue.Split(' ')[0] : "";
                var venueNo = req.VenueNo;
                var venuebNo = req.VenueBranchNo;
                var serviceno = req.ServiceNo;
                string folderName = venueNo + "\\" + visitId + "\\" + serviceno;
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
                            int splitcount = filePaths[f].ToString().Split('.').Count();
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount - 1];
                            var filenamesplit = Path.GetFileName(filePaths[f].ToString()).Split("$$");
                            result.ActualFileName = filenamesplit[4];
                            result.ManualFileName = filenamesplit[0] + "_" + filenamesplit[1] + "_" + filenamesplit[2] + "_" + filenamesplit[4];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.ConvertToBase64", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lstresult;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/SearchByBarcode")]
        public List<SearchBarcodeResponse> SearchByBarcode(RequestCommonSearch req)
        {
            List<SearchBarcodeResponse> response = new List<SearchBarcodeResponse>();
            try
            {
                response = _manageSampleRepository.SearchByBarcode(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.SearchByBarcode", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/InsertSpecimenMappingResult")]
        public SpecimenMappingResponse InsertSpecimenMappingResult(RequestSpecimenMedia req)
        {
            SpecimenMappingResponse response = new SpecimenMappingResponse();
            try
            {
                response = _manageSampleRepository.InsertSpecimenMappingResult(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.InsertSpecimenMappingResult", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return response;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpGet]
        [Route("api/ManageSample/GetSpecimenMappingResult")]
        public SpecimenMappingoutput GetSpecimenMappingResult(int venueno, int venuebranchno, int SpecimenNo, int type = 0)
        {
            SpecimenMappingoutput response = new SpecimenMappingoutput();
            try
            {
                response = _manageSampleRepository.GetSpecimenMappingResult(venueno, venuebranchno, SpecimenNo, type);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetSpecimenMappingResult", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueno, venuebranchno, 0);
            }
            return response;
        }
     
        //upload multiple files
        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/ManageSample/BulkUploadFile")]
        public ActionResult<FrontOffficeResponse> BulkUploadFile([FromBody] List<BulkFileUpload> lstjDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            int venueno = 0;
            int venuebno = 0;
            try
            {
                var _errormsg = BulkFileUploadValidation.BulkUploadFile(lstjDTO);
                if (!_errormsg.status)
                {
                    foreach (var objDTO in lstjDTO)
                    {
                        venueno = objDTO.VenueNo;
                        venuebno = objDTO.VenueBranchNo;

                        var base64data = objDTO.ActualBinaryData;
                        var visitId = objDTO.ExternalVisitID;
                        var venueNo = objDTO.VenueNo;
                        var venuebNo = objDTO.VenueBranchNo;
                        var format = objDTO.FileType;
                        var actualfilename = objDTO.ActualFileName + "." + format;
                        var manualfilename = objDTO.ManualFileName;
                        var serviceId = objDTO.FileBinaryData;
                        var isincludedinreport = objDTO.FilePath;
                        string folderName = venueNo + "\\" + visitId + "\\" + serviceId;
                        //
                        objAppSettingResponse = new AppSettingResponse();
                        string AppUploadPathInit = "ResultAckUpload";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                        string uploadpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                        string webRootPath = uploadpath;
                        string newPath = Path.Combine(webRootPath, folderName);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        if (base64data != null && base64data.Length > 0)
                        {
                            string fileName = manualfilename + "$$" + visitId + "$$" + serviceId + "$$" + venueNo + "$$" + actualfilename + "$$" + isincludedinreport + "." + format;
                            string fullPath = Path.Combine(newPath, fileName);

                            byte[] imageBytes = Convert.FromBase64String(base64data);
                            System.IO.File.WriteAllBytes(fullPath, imageBytes);
                        }
                    }
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.BulkUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/ChangeCheckBox")]
        public int ChangeCheckBox(GetSampleOutsourceRequest req)
        {
            int iOutput = 0;
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppResultAckUpload = "ResultAckUpload";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppResultAckUpload);
                string resultackuplod = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                                                                ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = resultackuplod;
                var visitId = req.SearchValue;
                var venueNo = req.VenueNo;
                var venuebNo = req.VenueBranchNo;
                var serviceno = req.ServiceNo;
                var checkbox = req.ServiceType;
                string folderName = venueNo + "\\" + visitId + "\\" + serviceno;
                string newPath = Path.Combine(Pathinit, folderName);

                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            string path = filePaths[f].ToString();

                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            string chkbx = checkbox == "Yes" ? "No" : "Yes";
                            string newfilepath = path.Replace(chkbx, checkbox);
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            string fullPath = newfilepath;
                            {
                                byte[] imageBytes = Convert.FromBase64String(base64String);
                                System.IO.File.WriteAllBytes(fullPath, imageBytes);
                            }
                            iOutput = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.ChangeCheckBox", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return iOutput;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/RemoveMultiUpload")]
        public int RemoveMultiUpload(GetSampleOutsourceRequest req)
        {
            int iOutput = 0;
            try
            {
                if (System.IO.File.Exists(req.ServiceType))
                {
                    System.IO.File.Delete(req.ServiceType);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.RemoveMultiUpload", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return iOutput;
        }

        [CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/ManageSample/GetBarcodePrintDetails")]
        public ActionResult GetBarcodePrintDetails(BarcodePrintRequest RequestItem)
        {
            List<BarcodePrintResponse> objResult = new List<BarcodePrintResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetBarcodePrintDetailsValidation(RequestItem);
                if (!_errormsg.status)
                {
                    objResult = _manageSampleRepository.GetBarcodePrintDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetBarcodePrintDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objResult);
        }

        [HttpPost]
        [Route("api/ManageSample/BranchSampleReceiveByBarcode")]
        public List<SearchBranchSampleBarcodeResponse> BranchSampleReceiveByBarcode(requestCommonSearch req)
        {
            List<SearchBranchSampleBarcodeResponse> response = new List<SearchBranchSampleBarcodeResponse>();
            try
            {
                response = _manageSampleRepository.SearchBranchSampleByBarcode(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.BranchSampleReceiveByBarcode", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return response;
        }

        [HttpPost]
        [Route("api/ManageSample/GetBranchSampleActionDetails")]
        public List<BranchSampleActionDTO> GetBranchSampleActionDetails([FromBody] SampleActionRequest req)
        {
            List<BranchSampleActionDTO> response = new List<BranchSampleActionDTO>();
            try
            {
                response = _manageSampleRepository.GetBranchSampleActionDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetBranchSampleActionDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return response;
        }

        [HttpGet]
        [Route("api/ManageSample/GetPrePrintBarcodelist")]
        public List<PrePrintBarcodeOrderResponse> GetPrePrintBarcodelist(int VenueNo, int VenueBranchNo, long visitNo)
        {
            List<PrePrintBarcodeOrderResponse> objresult = new List<PrePrintBarcodeOrderResponse>();
            try
            {
                objresult = _manageSampleRepository.GetPrePrintBarcodelist(visitNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManageSampleController.GetPrePrintBarcodelist", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}

