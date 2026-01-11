using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleRepository _SampleRepository;
        private readonly IAuditService _auditService;
        public SampleController(ISampleRepository noteRepository, IAuditService auditService)
        {
            _SampleRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Sample/InsertSampleDetails")]
        public IActionResult InsertSampleDetails([FromBody] TblSample Sampleitem)
        {
            List<sampleMasterResponse> objresult = new List<sampleMasterResponse>();
            try
            {
                using (var auditScope = new AuditScope<TblSample>(Sampleitem, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertSampleDetails(Sampleitem);
                    if (!_errormsg.status)
                    {
                        objresult = _SampleRepository.InsertSampleDetails(Sampleitem);
                        string _CacheKey = CacheKeys.CommonMaster + "SAMPLE" + Sampleitem.VenueNo + Sampleitem.VenueBranchNo;

                        MemoryCacheRepository.RemoveItem(CacheKeys.SampleMaster);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                    {
                        return BadRequest(_errormsg);
                    }
                    auditScope.IsRollBack = objresult[0].SampleNo == -1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertSampleDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Sampleitem.VenueNo, Sampleitem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/Sample/GetSampleDetails")]
        public IEnumerable<TblSample> GetSampleDetails(GetCommonMasterRequest sampleMasterRequest)
        {
            List<TblSample> objresult = new List<TblSample>();
            try
            {
                objresult = _SampleRepository.GetSampleDetails(sampleMasterRequest);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SampleRepository.GetSampleDetails" + sampleMasterRequest.SampleNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, sampleMasterRequest.venueno, sampleMasterRequest.venuebranchno, 0);
            }
            return objresult;
        }
    }
}