using System;
using Service.Model;
using DEV.Common;
using Dev.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers.qcmodule
{
    [ApiController]
    public class AnalyzParamController : ControllerBase
    {
        private readonly IAnaParamRepository _AnalyzParamRepository;
        private readonly IAuditService _AuditService;
        public AnalyzParamController(IAnaParamRepository noteRepository, IAuditService auditService)
        {
            _AnalyzParamRepository = noteRepository;
            _AuditService = auditService;
        }

        #region Get Analyzer Parameter Details
        /// <summary>
        /// Get Analyzer Parameter 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/QC/GetAnaParamDetails")]
        public List<AnaParamGetDto> GetAnaParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno)
        {
            List<AnaParamGetDto> result = new List<AnaParamGetDto>();
            try
            {
                result = _AnalyzParamRepository.GetAnaParamDetails(VenueNo, VenueBranchNo, Analyzerno, Sampleno);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzParamController.GetAnaParamDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        #endregion

        #region Insert Analyzer Parameter 
        /// <summary>
        /// Insert Unit 
        /// </summary>
        /// <param name="AnaParamobj"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/QC/InsertAnaParam")]
        public ActionResult<AnaParamDtoResponse> InsertAnaParam([FromBody] AnaParamDto AnaParamobj)
        {
            AnaParamDtoResponse result = new AnaParamDtoResponse();
            try
            {
                using(var auditScoped = new AuditScope<AnaParamDto>(AnaParamobj, _AuditService))
                {
                    var _errormsg = DeviceInterfaceValidation.InsertAnaParam(AnaParamobj);
                    if (!_errormsg.status)
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "PARAMNAME" + AnaParamobj.VenueNo + AnaParamobj.venuebranchno;
                        result = _AnalyzParamRepository.InsertAnaParam(AnaParamobj);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzParamController.InsertAnaParam", ExceptionPriority.Low, ApplicationType.APPSERVICE, AnaParamobj.VenueNo, 0, 0);
            }
            return Ok(result);
        }
        #endregion

        [HttpGet]
        [Route("api/QC/FetchAnalyzerParamDetails")]
        public List<FetchAnaParamDto> FetchAnalyzerParamDetails(int VenueNo, int VenueBranchNo, int Analyzerno, int Sampleno)
        {
            List<FetchAnaParamDto> result = new List<FetchAnaParamDto>();
            try
            {
                result = _AnalyzParamRepository.FetchAnalyzerParamDetails(VenueNo, VenueBranchNo, Analyzerno, Sampleno);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AnalyzParamController.FetchAnalyzerParamDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
    }
}
