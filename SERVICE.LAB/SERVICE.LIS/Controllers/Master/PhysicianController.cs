using System;
using System.Collections.Generic;
using System.Linq;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.Configuration;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PhysicianController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IPhysicianRepository _PhysicianRepository;
        private readonly IAuditService _auditService;
        private readonly IMasterRepository _IMasterRepository;
        public PhysicianController(IPhysicianRepository noteRepository, IConfiguration config, IAuditService auditService, IMasterRepository iMasterRepository)
        {
            _PhysicianRepository = noteRepository;
            _config = config;
            _auditService = auditService;
            _IMasterRepository = iMasterRepository;
        }

        #region Get Physician Details
        /// <summary>
        /// Get Physician Details
        /// </summary>
        /// <returns></returns>

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/GetPhysicianDetails")]
        public IEnumerable<PhysicianDetailsResponse> GetPhysicianDetails(GetCommonMasterRequest masterRequest)
        {
            List<PhysicianDetailsResponse> Objresult = new List<PhysicianDetailsResponse>();
            try
            {               
                Objresult = _PhysicianRepository.GetPhysicianDetails(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPhysicianDetails" , ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, masterRequest.venuebranchno, 0);
            }
            return Objresult;
        }

        #endregion

        #region Insert Physician 
        /// <summary>
        /// Insert Physician 
        /// </summary>
        /// <param name="Physicianitem"></param>
        /// <returns></returns>        
        [CustomAuthorize("LIMSFINMASTERS")]
        [HttpPost]
        [Route("api/Physician/InsertPhysicianDetails")]
        public ActionResult InsertPhysicianDetails([FromBody] PostPhysicianMaster Physicianitem)
        {
            int physicianNo = 0;
            try
            {
                if (Physicianitem?.tblPhysician == null)
                    return BadRequest("Invalid physician data.");

                using (var auditScoped = new AuditScope<TblPhysician>(Physicianitem.tblPhysician, _auditService))
                {
                    var _errormsg = MasterValidation.InsertPhysicianDetails(Physicianitem);
                    if (!_errormsg.status)
                    {
                        physicianNo = _PhysicianRepository.SavePhysicianDetaile(Physicianitem.tblPhysician);
                        _PhysicianRepository.PhysicianMerging(
                            Physicianitem.physicianMapping,
                            Physicianitem.tblPhysician.VenueNo ?? 0,
                            Physicianitem.tblPhysician.VenueBranchNo ?? 0,
                            Physicianitem.tblPhysician.CreatedBy ?? 0,
                            Physicianitem.tblPhysician.PhysicianNo);
                        
                        if (Physicianitem.documentUploadlst != null && Physicianitem.documentUploadlst.Count > 0)
                        {
                            _PhysicianRepository.DocumentUploadDetails(Physicianitem.documentUploadlst, Physicianitem.tblPhysician.VenueNo ?? 0, Physicianitem.tblPhysician.CreatedBy ?? 0, Physicianitem.tblPhysician.PhysicianNo);
                        }

                        if (Physicianitem.opdPhysiciandetail != null && Physicianitem.opdPhysiciandetail.Count > 0)
                        {
                            _PhysicianRepository.OPDPatientdetails(Physicianitem.opdPhysiciandetail, Physicianitem.tblPhysician);
                        }

                        string _CommonCommonCatch = CacheKeys.CommonMaster + "COMMON" + Physicianitem.tblPhysician.VenueNo + Physicianitem.tblPhysician.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CommonCommonCatch);
                        string _CommonPhysicianCatch = CacheKeys.CommonMaster + "Physician" + Physicianitem.tblPhysician.VenueNo + Physicianitem.tblPhysician.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CommonPhysicianCatch);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertPhysicianDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Physicianitem.tblPhysician.VenueNo, Physicianitem.tblPhysician.VenueBranchNo, 0);
            }
            return Ok(physicianNo);
        }
        #endregion

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/PhysicianHaveVisits")]
        public int PhysicianHaveVisits(TblPhysician tblPhysician)
        {
            int iOutput = 0;
            try
            {
                iOutput = _PhysicianRepository.PhysicianHaveVisits(tblPhysician);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianHaveVisits", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblPhysician.VenueNo, tblPhysician.VenueBranchNo, 0);
            }
            return iOutput;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/UploadFile")]
        public FrontOffficeResponse UploadFile([FromBody] List<BulkFileUpload> lstjDTO)
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
                    var visitno = objDTO.PatientVisitNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.ActualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = venueNo + "\\" + venuebNo + "\\" + visitno + "\\" + visitId;
                    //
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("UploadPhysicianDoc");
                    string UploadPhysiciandc = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = UploadPhysiciandc;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = venueNo + "$$" + venuebNo + "$$" + visitno + "$$" + actualfilename + "$$" + manualfilename + "." + format;
                        string fullPath = Path.Combine(newPath, fileName);

                        byte[] imageBytes = Convert.FromBase64String(base64data);
                        System.IO.File.WriteAllBytes(fullPath, imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.UploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/GetPhysicianDocumentDetails")]
        public IEnumerable<PhysicianDocUploadDetailRes> GetPhysicianDocumentDetails(PhysicianDocUploadReq Req)
        {
            List<PhysicianDocUploadDetailRes> Objresult = new List<PhysicianDocUploadDetailRes>();
            try
            {
                Objresult = _PhysicianRepository.GetPhysicianDocumentDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.GetPhysicianDocumentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/RemoveUploadFile")]
        public FrontOffficeResponse RemoveUploadFile([FromBody] List<BulkFileUpload> lstjDTO)
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

                    var visitId = objDTO.ExternalVisitID;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var visitno = objDTO.PatientVisitNo;

                    string folderName = venueNo + "\\" + venuebNo + "\\" + visitno + "\\" + visitId;
                    //
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppUploadPhysicianDoc = "UploadPhysicianDoc";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPhysicianDoc);
                    string UploadPhysiciandc = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = UploadPhysiciandc;
                    string newPath = Path.Combine(webRootPath, folderName);
                    string[] files = Directory.GetFiles(newPath);
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);                        
                    }                   
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.RemoveUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return result;
        }
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/PhysicianController/ConvertToBase64")]
        public List<BulkFileUpload> ConvertToBase64([FromBody] FrontOffficeDTO objDTO)
        {
            List<BulkFileUpload> lstresult = new List<BulkFileUpload>();
            BulkFileUpload result = new BulkFileUpload();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                //
                objAppSettingResponse = new AppSettingResponse();
                string AppUploadPathInit = "UploadPhysicianDoc";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = uplodpathinit;
                var visitId = objDTO.ExternalVisitID;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
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
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[4];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.ConvertToBase64", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return lstresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/GetMachineTimeDetails")]
        public IEnumerable<OPDMachineRes> GetMachineTimeDetails(OPDMachineReq Req)
        {
            List<OPDMachineRes> Objresult = new List<OPDMachineRes>();
            try
            {
                Objresult = _PhysicianRepository.GetMachineTimeDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.GetMachineTimeDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.Venuebno, Req.MachineNo);
            }
            return Objresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Physician/GetPhysicianOPDDetails")]
        public IEnumerable<OPDPhysicianRes> GetPhysicianOPDDetails(OPDPhysicianReq Req)
        {
            List<OPDPhysicianRes> Objresult = new List<OPDPhysicianRes>(); ;
            try
            {
                Objresult = _PhysicianRepository.GetPhysicianOPDDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.GetPhysicianOPDDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.Venuebno, Req.PhysicianNo);
            }
            return Objresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("api/Physician/LastPhysicianCode")]
        public List<PhysicianOrClientCodeResponse> GetLastPhysicianCode(int VenueNo, int VenueBranchNo,string CodeType, string? CodeToCheck = null)
        {
            List<PhysicianOrClientCodeResponse> Objresult = new List<PhysicianOrClientCodeResponse>();
            try
            {
                Objresult = _PhysicianRepository.GetLastPhysicianCode(VenueNo, VenueBranchNo, CodeType, CodeToCheck);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetLastPhysicianCode", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, 0, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/Physician/GetConsultant")]
        public IEnumerable<consultantdetails> GetConsultant(getconsultant getconsultant)
        {
            List<consultantdetails> Objresult = new List<consultantdetails>(); ;
            try
            {
                Objresult = _PhysicianRepository.GetConsultant(getconsultant);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianController.GetConsultant", ExceptionPriority.Low, ApplicationType.APPSERVICE, getconsultant.venueNo, getconsultant.venuebranchNo, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/Physician/SaveConsultant")]
        public int SaveConsultant(saveConsultant saveConsultant)
        {
            int consultantNo = 0;
            try
            {
                consultantNo = _PhysicianRepository.SaveConsultant(saveConsultant);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveConsultant", ExceptionPriority.Low, ApplicationType.APPSERVICE, saveConsultant.venueNo, saveConsultant.venuebranchNo, 0);
            }
            return consultantNo;
        }
    }
}