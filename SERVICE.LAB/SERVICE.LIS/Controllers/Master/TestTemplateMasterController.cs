using Dev.IRepository.Master;
using Dev.Repository.Master;
using DEV.Common;
using Service.Model;
using Service.Model.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TestTemplateMasterController : ControllerBase
    {
        private readonly ITestTemplateMasterRepository _TestTemplateMasterRepository;
        private readonly IConfiguration _config;
        public TestTemplateMasterController(ITestTemplateMasterRepository TestTemplateMasterRepository, IConfiguration config)
        {
            _TestTemplateMasterRepository = TestTemplateMasterRepository;
            _config = config;
        }
        [HttpPost]
        [Route("api/TestTemplateMaster/GetTestTemplateMasterList")]
        public List<GetTestTemplateMasterRes> GetTestTemplateMasterList(GetTestTemplateMasterReq req)
        {
            List<GetTestTemplateMasterRes> GetTestTemplateMasterListResult = new List<GetTestTemplateMasterRes>();
            try
            {

                GetTestTemplateMasterListResult = _TestTemplateMasterRepository.GetTestTemplateMasterList(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTestTemplateMasterList " + req.TestNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return GetTestTemplateMasterListResult;
        }
        [HttpPost]
        [Route("api/TestTemplateMaster/GetEditTemplateTestMaster")]
        public GetEditTemplateTestMasterResponseDto GetEditTemplateTestMaster(GetEditTemplateTestMasterRequestDto req)
        {
            var result = new GetEditTemplateTestMasterResponseDto();
            try
            {
                result = _TestTemplateMasterRepository.GetEditTemplateTestMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetEditTemplateTestMaster " + req.TestNo,ExceptionPriority.Low,
                ApplicationType.REPOSITORY,req.VenueNo,req.VenueBranchNo,0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/TestTemplateMaster/InsertTemplatePath")]
        public TemplatePathRes InsertTemplatePath([FromBody] TemplatePathReq req)
        {
            var result = new TemplatePathRes();
            try
            {
                result = _TestTemplateMasterRepository.InsertTemplatePath(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex,"TemplateTestRepository.InsertTemplatePath " + req.templateNo,ExceptionPriority.Low,ApplicationType.REPOSITORY,req.VenueNo,req.VenueBranchNo,req.testNo
                );
                result.status = -1;
                result.message = ex.Message;
            }
            return result;
        }

        ////[CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/TestTemplateMaster/GetTestTemplateText")]
        public GetTestTemplateTextMasterRes GetTextTemplateTextMaster([FromBody] GetTestTemplateTextMasterReq req)
        {
            GetTestTemplateTextMasterRes obj = new GetTestTemplateTextMasterRes();
            try
            {
                obj = _TestTemplateMasterRepository.GetTextTemplateTextMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTextTemplateTextMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }

        ////[CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("api/TestTemplateMaster/InsertTestTemplateText")]
        public ActionResult<InsertTestTemplateMasterRes> InsertTestTemplateMaster([FromBody] InsertTestTemplateMasterReq req)
        {
            InsertTestTemplateMasterRes obj = new InsertTestTemplateMasterRes();
            try
            {
                obj = _TestTemplateMasterRepository.InsertTestTemplateMaster(req);
                string _CommonCommonCatch = CacheKeys.CommonMaster + "TEMPLATE" + req.venueNo + req.venueBranchNo;
                MemoryCacheRepository.RemoveItem(_CommonCommonCatch);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.InsertTestTemplateMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return Ok(obj);
        }
        [HttpPost]
        [Route("api/TestTemplateMaster/GetTemplateApprovalList")]
        public List<GetTemplateApprovalRes> GetTemplateApprovalList(GetTemplateApprovalReq req)
        {
            List<GetTemplateApprovalRes> GetTemplateApprovalListResult = new List<GetTemplateApprovalRes>();
            try
            {
                GetTemplateApprovalListResult = _TestTemplateMasterRepository.GetTemplateApprovalList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TemplateTestRepository.GetTemplateApprovalList " + req.TestNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return GetTemplateApprovalListResult;
        }

    }
}
