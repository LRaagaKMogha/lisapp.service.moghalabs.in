using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Dev.Repository;
using Service.Model.EF;
using Service.Model.EF.External.CommonMasters;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ClientMasterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IClientMasterRepository _ClientMasterRepository;
        private readonly IFinanceIntegrationRepository _financeIntegrationRepository;
        private readonly IAuditService _auditService;
        public ClientMasterController(IClientMasterRepository noteRepository, IConfiguration config, IFinanceIntegrationRepository financeIntegrationRepository, IAuditService auditService)
        {
            _ClientMasterRepository = noteRepository;
            _financeIntegrationRepository = financeIntegrationRepository;
            _config = config;
            _auditService = auditService;
        }

        #region Get ClientMaster Details
        /// <summary>
        /// Get ClientMaster Details
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/GetClientMasterDetails")]
        public IEnumerable<CustomerResponse> GetClientMasterDetails(GetCustomerRequest getCustomerRequest)
        {
            List<CustomerResponse> objresult = new List<CustomerResponse>();
            try
            {
                objresult = _ClientMasterRepository.GetClientMasterDetails(getCustomerRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetClientMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getCustomerRequest.venueNo, getCustomerRequest.venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion

        #region Get ClientMaster Details
        /// <summary>
        /// Get ClientMaster Details
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/GetclientSubUser")]
        public IEnumerable<ClientSubUserResponse> GetclientSubUser(GetCustomerRequest getCustomerRequest)
        {
            List<ClientSubUserResponse> objresult = new List<ClientSubUserResponse>();
            try
            {
                objresult = _ClientMasterRepository.GetclientSubUser(getCustomerRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetclientSubUser", ExceptionPriority.Low, ApplicationType.APPSERVICE, getCustomerRequest.venueNo, getCustomerRequest.venueBranchNo, 0);
            }
            return objresult;
        }

        #endregion

        #region Insert ClientMaster 
        /// <summary>
        /// Insert ClientMaster 
        /// </summary>
        /// <param name="ClientMasteritem"></param>
        /// <returns></returns>        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/InsertClientMasterDetails")]
        public ActionResult InsertClientMasterDetails([FromBody] PostCustomerMaster postcustomerDTO)
        {
            InsertCustomerResponse result = new InsertCustomerResponse();
            string ExistingCustomerCode = string.Empty;
            try
            {
                using (var auditScoped = new AuditScope<TblCustomer>(postcustomerDTO.tblcustomer, _auditService))
                {
                    var _errormsg = ClientMasterValidation.InsertClientMasterDetails(postcustomerDTO);
                    if (!_errormsg.status)
                    {
                        string IsCustomerApproval = "IsCustomerApproval";

                        var user = HttpContext.Items["User"] as UserClaimsIdentity;
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        var objAppSettingResponse = _IMasterRepository.GetSingleConfiguration(user.VenueNo, user.VenueBranchNo, IsCustomerApproval);
                        
                        if (postcustomerDTO.tblcustomer.CustomerNo > 0)
                        {
                            List<CustomerResponse> objresult = new List<CustomerResponse>();

                            objresult = _ClientMasterRepository.GetClientMasterDetails(new GetCustomerRequest()
                            {
                                customerNumber = postcustomerDTO.tblcustomer.CustomerNo,
                                venueNo = user.VenueNo,
                                venueBranchNo = user.VenueBranchNo,
                                pageIndex = 1
                            });

                            ExistingCustomerCode = objresult.Count > 0 ? objresult[0].CustomerCode : "";
                        }

                        result = _ClientMasterRepository.InsertClientMasterDetails(postcustomerDTO);
                        string _CacheKey = CacheKeys.UserMenu + result.CustomerNo + postcustomerDTO.tblcustomer.VenueNo + postcustomerDTO.tblcustomer.VenueBranchNo + "1";
                        if (postcustomerDTO.subclient.Count > 0)
                        {
                            _ClientMasterRepository.InsertSubClientMasterDetails(postcustomerDTO.subclient, postcustomerDTO.tblcustomer.VenueNo, postcustomerDTO.tblcustomer.VenueBranchNo, postcustomerDTO.tblcustomer.CreatedBy, result.CustomerNo, postcustomerDTO.tblcustomer.IsApproval, postcustomerDTO.tblcustomer.IsReject);
                        }
                        MemoryCacheRepository.RemoveItem(CacheKeys.CustomerMaster);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                        if (postcustomerDTO.isDocUpdModified == true && postcustomerDTO.documentUploadlst.Count > 0 && postcustomerDTO.documentUploadlst != null)
                        {
                            _ClientMasterRepository.DocumentUploadDetails(postcustomerDTO.documentUploadlst, postcustomerDTO.tblcustomer.VenueNo, postcustomerDTO.tblcustomer.VenueBranchNo, postcustomerDTO.tblcustomer.CreatedBy, postcustomerDTO.tblcustomer.CustomerNo);
                        }
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.InsertClientMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, postcustomerDTO.tblcustomer.VenueNo, postcustomerDTO.tblcustomer.VenueBranchNo, 0);
            }
            return Ok(result);
        }
        #endregion
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/InsertClientSubMaster")]
        public ActionResult InsertClientSubMaster([FromBody] PostCustomersubuserMaster postcustomerDTO)
        {
            int result = 0;
            try
            {
                using(var auditScoped = new AuditScope<PostCustomersubuserMaster>(postcustomerDTO, _auditService))
                {
                    var _errormsg = ClientMasterValidation.InsertClientSubMaster(postcustomerDTO);
                    if (!_errormsg.status)
                    {
                        result = _ClientMasterRepository.InsertClientSubMaster(postcustomerDTO);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.InsertClientSubMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, postcustomerDTO.VenueNo, postcustomerDTO.VenueBranchNo, 0);
            }
            return Ok(result);
        }   

        #region Get SubCustomer Detail
        /// <summary>
        /// GetSubCustomerDetailbyCustomer
        /// </summary>
        /// <param name="CustomerNo"></param>
        /// <param name="VenueNo"></param>
        /// <param name="VenueBranchNo"></param>
        /// <returns></returns>
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("api/ClientMaster/GetSubCustomerDetail")]
        public List<CustomerMappingDTO> GetSubCustomerDetailbyCustomer(int CustomerNo, int VenueNo, int VenueBranchNo, int IsApproval)
        {
            List<CustomerMappingDTO> objresult = new List<CustomerMappingDTO>();
            try
            {
                objresult = _ClientMasterRepository.GetSubCustomerDetailbyCustomer(CustomerNo,VenueNo, VenueBranchNo, IsApproval);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetSubCustomerDetailbyCustomer - " + CustomerNo, ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("api/ClientMaster/GetSubClinic")]
        public List<CustomerMappingDTO> GetSubClinic(int CustomerNo, int VenueNo, int VenueBranchNo)
        {
            List<CustomerMappingDTO> objresult = new List<CustomerMappingDTO>();
            try
            {
                objresult = _ClientMasterRepository.GetSubClinic(CustomerNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetSubClinic - " + CustomerNo, ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo,0);
            }
            return objresult;
        }
        #endregion

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/GetClientRestrictionDayIsValid")]
        public ClientRestrictionDayResponse GetClientRestrictionDayIsValid(ClientRestrictionDay ObjRequest)
        {
            int clientno = ObjRequest.ClientNumber;
            int venueno = ObjRequest.ClientNumber;
            int venuebranchno = ObjRequest.ClientNumber;
            ClientRestrictionDayResponse objresult = new ClientRestrictionDayResponse();
            try
            {
                objresult = _ClientMasterRepository.GetClientRestrictionDayIsValid(ObjRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetClientRestrictionDayIsValid/Clientno - " + clientno, ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueno, venuebranchno, 0);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/GetClientDocumentDetails")]
        public IEnumerable<ClientDocUploadDetailRes> GetClientDocumentDetails(ClientDocUploadReq Req)
        {
            List<ClientDocUploadDetailRes> objresult = new List<ClientDocUploadDetailRes>();
            try
            {
                objresult = _ClientMasterRepository.GetClientDocumentDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientMasterController.GetClientDocumentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);
            }
            return objresult;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/UploadFile")]
        public ActionResult<FrontOffficeResponse> UploadFile([FromBody] List<BulkFileUpload> lstjDTO)
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
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppUploadClientDoc = "UploadClientDoc";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadClientDoc);
                    string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = uplodpathinit;
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
                MyDevException.Error(ex, "ClientMasterController.UploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/ClientMaster/RemoveUploadFile")]
        public ActionResult<FrontOffficeResponse> RemoveUploadFile([FromBody] List<BulkFileUpload> lstjDTO)
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
                    MasterRepository _IMasterRepository = new MasterRepository(_config);
                    AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                    objAppSettingResponse = new AppSettingResponse();
                    string AppUploadClientDoc = "UploadClientDoc";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadClientDoc);
                    string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = uplodpathinit;
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
                MyDevException.Error(ex, "ClientMasterController.RemoveUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return Ok(result);
        }
    }
}
