using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class Analyzemaster : ControllerBase
    {
        private readonly IAnalyzerMasterRepository _AnalyzerMasterRepository;
        private readonly IAuditService _auditService;
        public Analyzemaster(IAnalyzerMasterRepository analyzerMasterRepository, IAuditService auditService)
        {
            _AnalyzerMasterRepository = analyzerMasterRepository;
            _auditService = auditService;
        }
     
        [HttpPost]
        [Route("api/AnalyzerMaster/GetAnalyzerMasterDetails")]
        public IEnumerable<TblAnalyzer> GetAnalyzerMasterDetails(GetCommonMasterRequest getanalyzer)
        {
            List<TblAnalyzer> Objresult = new List<TblAnalyzer>();
            try
            {                
                Objresult = _AnalyzerMasterRepository.GetAnalyzerMasterDetails(getanalyzer); 
            }
            catch (Exception ex)
            {
               MyDevException.Error(ex, "AnalyzemasterController.GetAnalyzerMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getanalyzer.venueno, getanalyzer.venuebranchno, 0);
            }
            return Objresult;
        }       
       
        [HttpPost]
        [Route("api/AnalyzerMaster/InsertAnalyzerDetails")]
        public ActionResult<TblAnalyzerdata> InsertAnalyzerDetails([FromBody] TblAnalyzerresponse analyzeritem)
        {
            TblAnalyzerdata Objresult = new TblAnalyzerdata();
            try
            {
                using(var auditScoped = new AuditScope<TblAnalyzerresponse>(analyzeritem, _auditService))
                {
                    var _errormsg = DeviceInterfaceValidation.InsertAnalyzerDetails(analyzeritem);
                    if (!_errormsg.status)
                    {
                        Objresult = _AnalyzerMasterRepository.InsertAnalyzerDetails(analyzeritem);
                        string _CacheKey = CacheKeys.CommonMaster + "AnalyzerMaster" + analyzeritem.venueNo + analyzeritem.venuebranchNo;
                        MemoryCacheRepository.RemoveItem(CacheKeys.AnalyzerMaster);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzemasterController.InsertAnalyzerDetails - ", ExceptionPriority.Low, ApplicationType.APPSERVICE, analyzeritem.venueNo, analyzeritem.venuebranchNo, 0);
            }
            return Ok(Objresult);
        }

        [HttpGet]
        [Route("api/AnalyzerMaster/GetAnaParamDetails")]
        public List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int analyzerParamNo, int Analyzerno, int Sampleno)
        {
            List<AnaParamGetDto> result = new List<AnaParamGetDto>();
            try
            {
                result = _AnalyzerMasterRepository.GetAnaParamDetails(VenueNo, VenueBranchNo, analyzerParamNo, Analyzerno, Sampleno);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzemasterController.GetAnaParamDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
    
        [HttpPost]
        [Route("api/AnalyzerMaster/InsertAnaParam")]
        public AnaParamDtoResponse InsertAnaParam([FromBody] AnaParamDto AnaParamobj)
        {
            AnaParamDtoResponse result = new AnaParamDtoResponse();
            try
            {
                using(var auditScoped = new AuditScope<AnaParamDto>(AnaParamobj, _auditService))
                {
                    _AnalyzerMasterRepository.InsertAnaParam(AnaParamobj);
                    string _CacheKey = CacheKeys.CommonMaster + "PARAMNAME" + AnaParamobj.VenueNo + AnaParamobj.venuebranchno;
                    MemoryCacheRepository.RemoveItem(CacheKeys.PARAMNAME);
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzemasterController.InsertAnaParam - ", ExceptionPriority.Low, ApplicationType.APPSERVICE, AnaParamobj.VenueNo, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/AnalyzerMaster/GetAnalVsParamVsTest")]
        public IEnumerable<TbltestMap> GetAnalVsParamVsTest(TestmapRequest TestmapRequest)
        {
            List<TbltestMap> Objresult = new List<TbltestMap>();
            try
            {
                Objresult = _AnalyzerMasterRepository.GetAnalVsParamVsTest(TestmapRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAnalVsParamVsTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, TestmapRequest.venueNo, 0, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/AnalyzerMaster/InsertAnalVsParamVsTest")]
        public ActionResult<AnalVsparamVstestMap> InsertAnalVsParamVsTest(ResponseTest ResponseTest)
        {
            AnalVsparamVstestMap Objresult = new AnalVsparamVstestMap();
            try
            {
                using(var auditScoped = new AuditScope<ResponseTest>(ResponseTest, _auditService))
                {
                    var _errormsg = DeviceInterfaceValidation.InsertAnalVsParamVsTest(ResponseTest);
                    if (!_errormsg.status)
                    {
                        Objresult = _AnalyzerMasterRepository.InsertAnalVsParamVsTest(ResponseTest);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzemasterController.InsertAnalVsParamVsTest - " + ResponseTest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, ResponseTest.venueNo, 0, 0);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/AnalyzerMaster/GetSubTest")]
        public List<Subresponse> GetSubTest(Subrequest Subrequest)
        {
            List<Subresponse> Objresult = new List<Subresponse>();
            try
            {
                Objresult = _AnalyzerMasterRepository.GetSubTest(Subrequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSubTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, Subrequest.venueNo, 0, 0);
            }
            return Objresult;
        }
    }
}  

