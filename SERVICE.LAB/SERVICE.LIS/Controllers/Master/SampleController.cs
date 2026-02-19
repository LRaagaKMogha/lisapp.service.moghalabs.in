using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Service.IRepository;
using Service.Common;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers
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
            List<sampleMasterResponse> Objresult = new List<sampleMasterResponse>();
            try
            {
                using (var auditScope = new AuditScope<TblSample>(Sampleitem, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertSampleDetails(Sampleitem);
                    if (!_errormsg.status)
                    {
                        Objresult = _SampleRepository.InsertSampleDetails(Sampleitem);
                        string _CacheKey = CacheKeys.CommonMaster + "SAMPLE" + Sampleitem.VenueNo + Sampleitem.VenueBranchNo;

                        MemoryCacheRepository.RemoveItem(CacheKeys.SampleMaster);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                    {
                        return BadRequest(_errormsg);
                    }
                    auditScope.IsRollBack = Objresult[0].SampleNo == -1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertSampleDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Sampleitem.VenueNo, Sampleitem.VenueBranchNo, 0);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/Sample/GetSampleDetails")]
        public IEnumerable<TblSample> GetSampleDetails(GetCommonMasterRequest sampleMasterRequest)
        {
            List<TblSample> Objresult = new List<TblSample>();
            try
            {
                Objresult = _SampleRepository.GetSampleDetails(sampleMasterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SampleRepository.GetSampleDetails" + sampleMasterRequest.SampleNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, sampleMasterRequest.venueno, sampleMasterRequest.venuebranchno, 0);
            }
            return Objresult;
        }
    }
}