using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;
using Azure.Core;
//using BloodBankManagement.Contracts;
using Newtonsoft.Json;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _TestRepository;
        private readonly IAuditService _auditService;
        public TestController(ITestRepository noteRepository, IAuditService auditService)
        {
            _TestRepository = noteRepository;
            _auditService = auditService;
        }

        #region Test Master      

       
        [HttpPost]
        [Route("api/Test/GetTestList")]
        public List<lsttest> GetTestList([FromBody] reqtest req)
        {
            List<lsttest> lst = new List<lsttest>();
            try
            {
                lst = _TestRepository.GetTestList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetTestList", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetEditTest")]
        public objtest GetEditTest([FromBody] reqtest req)
        {
            objtest obj = new objtest();
            try
            {
                obj = _TestRepository.GetEditTest(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetEditTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertTest")]
        public ActionResult InsertTest(objtest req)
        {
            int testno = 0;
            try
            {
                var _errormsg = TestMasterValidation.InsertTest(req);
                if (!_errormsg.status)
                {
                    testno = _TestRepository.InsertTest(req);
                    string _CacheKey = CacheKeys.CommonMaster + "TEST" + req.venueNo + req.venueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                    //language text details
                    string _CacheKeyN = CacheKeys.CommonMaster + "LanguageTextDet" + req.venueNo + req.venueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CacheKeyN);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertTest", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(testno);
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetTemplateText")]
        public rtntemplateText GetTemplateText(reqtest req)
        {
            rtntemplateText obj = new rtntemplateText();
            try
            {
                obj = _TestRepository.GetTemplateText(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetTemplateText", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertTemplateText")]
        public ActionResult<rtntemplateNo> InsertTemplateText(lstTemplateList req)
        {
            rtntemplateNo obj = new rtntemplateNo();
            //int i = 0;
            try
            {
                var _errormsg = TestMasterValidation.InsertTemplateText(req);
                if (!_errormsg.status)
                {
                    obj = _TestRepository.InsertTemplateText(req);
                    string _CommonCommonCatch = CacheKeys.CommonMaster + "TEMPLATE" + req.venueNo + req.venueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CommonCommonCatch);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertTemplateText", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return Ok(obj);
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/UpdateSequence")]
        public int UpdateSequence(Objtestsequence req)
        {
            int i = 0;
            try
            {
                i = _TestRepository.UpdateSequence(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.UpdateSequence", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return i;
        }
        
        #endregion

        #region  Group Package Master
        
        #region GetGroupPackageList
        [HttpPost]
        [Route("api/Test/GetGroupPackageList")]
        public List<lstgrppkg> GetGroupPackageList([FromBody] reqtest req)
        {
            List<lstgrppkg> lst = new List<lstgrppkg>();
            try
            {
                lst = _TestRepository.GetGroupPackageList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetGroupPackageList", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region GetEditGroupPackage
        [HttpPost]
        [Route("api/Test/GetEditGroupPackage")]
        public objgrppkg GetEditGroupPackage([FromBody] reqtest req)
        {
            objgrppkg obj = new objgrppkg();
            try
            {
                obj = _TestRepository.GetEditGroupPackage(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetEditGroupPackage", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion

        #region InsertGroupPackage
        [HttpPost]
        [Route("api/Test/InsertGroupPackage")]
        public ActionResult InsertGroupPackage(objgrppkg req)
        {
            int groupno = 0;
            try
            {
                var _errormsg = TestMasterValidation.InsertGroupPackage(req);
                if (!_errormsg.status)
                {
                    var reqClone = JsonConvert.DeserializeObject<objgrppkg>(JsonConvert.SerializeObject(req));
                    using (var auditScope = new AuditScope<objgrppkg>(reqClone, _auditService, req.pageCode, req.pageCode == "GRPMAS" ? new string[] { "Group Master Save" } : new string[] { "Package Master Save" }))
                    {
                        groupno = _TestRepository.InsertGroupPackage(req);
                        auditScope.IsRollBack = groupno == 0 ? true : false;
                    }
                    //language text details
                    string _CacheKeyN = CacheKeys.CommonMaster + "LanguageTextDet" + req.venueNo + req.venueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CacheKeyN);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertGroupPackage - " + req.pageCode, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(groupno);
        }
        #endregion

        #region GetSearchService
        [HttpPost]
        [Route("api/Test/GetSearchService")]
        public List<lstgrppkgservice> GetSearchService([FromBody] reqsearchservice req)
        {
            List<lstgrppkgservice> lst = new List<lstgrppkgservice>();
            try
            {
                lst = _TestRepository.GetSearchService(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetSearchService", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #endregion

        #region Sub Test Master
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetSubTestList")]
        public List<lststest> GetSubTestList([FromBody] reqtest req)
        {
            List<lststest> lst = new List<lststest>();
            try
            {
                lst = _TestRepository.GetSubTestList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetSubTestList", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetEditSubTest")]
        public objsubtest GetEditSubTest(reqtest req)
        {
            objsubtest obj = new objsubtest();
            try
            {
                obj = _TestRepository.GetEditSubTest(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetEditSubTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertSubTest")]
        public ActionResult InsertSubTest(objsubtest req)
        {
            int testno = 0;
            try
            {
                var _errormsg = TestMasterValidation.InsertSubTest(req);
                if (!_errormsg.status)
                {
                    testno = _TestRepository.InsertSubTest(req);
                    //language text details
                    string _CacheKeyN = CacheKeys.CommonMaster + "LanguageTextDet" + req.venueNo + req.venueBranchNo;
                    MemoryCacheRepository.RemoveItem(_CacheKeyN);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertSubTest", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return Ok(testno);
        }
        #endregion

        #region InsertTestFormula
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertTestFormula")]
        public ActionResult InsertTestFormula(SaveFormulaRequest req)
        {
            int iOutput = 0;
            try
            {
                var _errormsg = TestMasterValidation.InsertTestFormula(req);
                if (!_errormsg.status)
                {
                    iOutput = _TestRepository.InsertTestFormula(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertTestFormula", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return Ok(iOutput);
        }
        #endregion

        #region GetTestFormula
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetTestFormula")]
        public List<GetFormulaResponse> GetTestFormula(GetFormulaRequest req)
        {
           List<GetFormulaResponse> obj = new List<GetFormulaResponse>();
            try
            {
                obj = _TestRepository.GetTestFormula(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetTestFormula", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion

        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetAlreadyExisitingTestCode")]
        public CheckTestcodeExistsRes GetAlreadyExisitingTestCode(CheckTestcodeExists req)
        {
            CheckTestcodeExistsRes outp = new CheckTestcodeExistsRes();
            try
            {
                outp = _TestRepository.GetAlreadyExisitingTestCode(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetAlreadyExisitingTestCode", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return outp;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetTestApprove")]
        public List<restestapprove> GetTestApprove([FromBody] reqtestapprove req)
        {
            List<restestapprove> lst = new List<restestapprove>();
            try
            {
                lst = _TestRepository.GetTestApprove(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetTestApprove", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetApproveHistory")]
        public List<restestappHistory> GetApproveHistory([FromBody] reqtestapprove req)
        {
            List<restestappHistory> lst = new List<restestappHistory>();
            try
            {
                lst = _TestRepository.GetApproveHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetApproveHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetTATMaster")]
        public List<GetTATRes> GetTATMaster([FromBody] GetTATReq req)
        {
            List<GetTATRes> lst = new List<GetTATRes>();
            try
            {
                lst = _TestRepository.GetTATMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetTATMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertTATMaster")]
        public InsTATRes InsertTATMaster(InsTATReq req)
        {
            using (var auditScoped = new AuditScope<ServiceDTO>(req.testXML, _auditService))
            {
                InsTATRes lst = new InsTATRes();
                try
                {
                    lst = _TestRepository.InsertTATMaster(req);
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "TestController.InsertTATMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userNo);
                }
                return lst;
            }            
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetLoincMaster")]
        public List<GetloincRes> GetLoincMaster([FromBody] GetloincReq req)
        {
            List<GetloincRes> lst = new List<GetloincRes>();
            try
            {
                lst = _TestRepository.GetLoincMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetLoincMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertLoincMaster")]
        public ActionResult<InsloincRes> InsertLoincMaster(InsloincReq req)
        {
            InsloincRes lst = new InsloincRes();
            try
            {
                using (var auditScoped = new AuditScope<InsloincReq>(req, _auditService))
                {                
                    var _errormsg = LaboratoryMasterValidation.InsertLoincMaster(req);
                    if (!_errormsg.status)
                    {
                        lst = _TestRepository.InsertLoincMaster(req);
                        string _CacheKey = CacheKeys.CommonMaster + "LOINCMASTER" + req.VenueNo + req.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertLoincMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(lst);
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetSnomedMaster")]
        public List<GetSnomedRes> GetSnomedMaster([FromBody] GetSnomedReq req)
        {
            List<GetSnomedRes> lst = new List<GetSnomedRes>();
            try
            {
                lst = _TestRepository.GetSnomedMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetSnomedMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0, 0);
            }
            return lst;
        }
       
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertSnomedMaster")]
        public ActionResult<InsSnomedRes> InsertSnomedMaster(InsSnomedReq req)
        {
            InsSnomedRes lst = new InsSnomedRes();
            try
            {
                using (var auditScoped = new AuditScope<InsSnomedReq>(req, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertSnomedMaster(req);
                    if (!_errormsg.status)
                    {
                        lst = _TestRepository.InsertSnomedMaster(req);
                        string _CacheKey = CacheKeys.CommonMaster + "SNOMEDMASTER" + req.VenueNo + req.VenueBranchNo;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertSnomedMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(lst);
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetIntegrationPackage")]
        public List<IntegrationPackageRes> GetIntegrationPackage([FromBody] IntegrationPackageReq req)
        {
            List<IntegrationPackageRes> lst = new List<IntegrationPackageRes>();
            try
            {
                using(var auditScoped = new AuditScope<IntegrationPackageReq>(req, _auditService))
                {
                    lst = _TestRepository.GetIntegrationPackage(req);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetIntegrationPackage", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertIntegrationPackage")]
        public ActionResult<IntegrationPackageResult> InsertIntegrationPackage(IntegrationPackageReq req)
        {
            IntegrationPackageResult lst = new IntegrationPackageResult();
            try
            {
                using(var auditScoped = new AuditScope<IntegrationPackageReq>(req, _auditService))
                {
                    var _errormsg = TestMasterValidation.InsertIntegrationPackage(req);
                    if (!_errormsg.status)
                    {
                        lst = _TestRepository.InsertIntegrationPackage(req);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertIntegrationPackage", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(lst);
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/GetStainTypeDetails")]
        public List<GetStatinMasterDetailsRes> GetStainTypeDetails([FromBody] GetStatinMasterDetailsReq req)
        {
            List<GetStatinMasterDetailsRes> lst = new List<GetStatinMasterDetailsRes>();
            try
            {
                lst = _TestRepository.GetStatinMasterDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetStainTypeDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lst;
        }
        
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/Test/InsertStainTypeDetails")]
        public StainMasterInsertRes InsertStainTypeDetails([FromBody] StainMasterInsertReq req)
        {
            StainMasterInsertRes lst = new StainMasterInsertRes();
            try
            {
                lst = _TestRepository.InsertStatinMasterDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.InsertStainTypeDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lst;
        }

        #region GetPackageInstrauction
        [HttpPost]
        [Route("api/Test/GetPackageInstrauction")]
        public objgrppkg GetPackageInstrauction([FromBody] reqtest req)
        {
            objgrppkg obj = new objgrppkg();
            try
            {
                obj = _TestRepository.GetPackageInstrauction(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetEditGroupPackage", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion
        
        #region PacagePrint
        [HttpPost]
        [Route("api/Test/GetPrintPakg")]
        public List<PrintPackageDetails> GetPrintPakg([FromBody] reqsearchservice req)
        {
            List<PrintPackageDetails> lst = new List<PrintPackageDetails>();
            try
            {
                lst = _TestRepository.GetPrintPakg(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestController.GetPrintPakg", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion
    }
}