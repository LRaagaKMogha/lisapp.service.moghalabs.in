using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
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
            List<TblAnalyzer> objresult = new List<TblAnalyzer>();
            try
            {                
                objresult = _AnalyzerMasterRepository.GetAnalyzerMasterDetails(getanalyzer); 
            }
            catch (Exception ex)
            {
               MyDevException.Error(ex, "AnalyzemasterController.GetAnalyzerMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getanalyzer.venueno, getanalyzer.venuebranchno, 0);
            }
            return objresult;
        }       
       
        [HttpPost]
        [Route("api/AnalyzerMaster/InsertAnalyzerDetails")]
        public ActionResult<TblAnalyzerdata> InsertAnalyzerDetails([FromBody] TblAnalyzerresponse analyzeritem)
        {
            TblAnalyzerdata objresult = new TblAnalyzerdata();
            try
            {
                using(var auditScoped = new AuditScope<TblAnalyzerresponse>(analyzeritem, _auditService))
                {
                    var _errormsg = DeviceInterfaceValidation.InsertAnalyzerDetails(analyzeritem);
                    if (!_errormsg.status)
                    {
                        objresult = _AnalyzerMasterRepository.InsertAnalyzerDetails(analyzeritem);
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
            return Ok(objresult);
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
        public IEnumerable<TbltestMap> GetAnalVsParamVsTest(testmapRequest testmapRequest)
        {
            List<TbltestMap> objresult = new List<TbltestMap>();
            try
            {
                objresult = _AnalyzerMasterRepository.GetAnalVsParamVsTest(testmapRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAnalVsParamVsTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, testmapRequest.venueNo, 0, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/AnalyzerMaster/InsertAnalVsParamVsTest")]
        public ActionResult<analVsparamVstestMap> InsertAnalVsParamVsTest(responseTest responseTest)
        {
            analVsparamVstestMap objresult = new analVsparamVstestMap();
            try
            {
                using(var auditScoped = new AuditScope<responseTest>(responseTest, _auditService))
                {
                    var _errormsg = DeviceInterfaceValidation.InsertAnalVsParamVsTest(responseTest);
                    if (!_errormsg.status)
                    {
                        objresult = _AnalyzerMasterRepository.InsertAnalVsParamVsTest(responseTest);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzemasterController.InsertAnalVsParamVsTest - " + responseTest.analyzerparamTestNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, responseTest.venueNo, 0, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/AnalyzerMaster/GetSubTest")]
        public List<subresponse> GetSubTest(subrequest subrequest)
        {
            List<subresponse> objresult = new List<subresponse>();
            try
            {
                objresult = _AnalyzerMasterRepository.GetSubTest(subrequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSubTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, subrequest.venueNo, 0, 0);
            }
            return objresult;
        }
    }
}  

