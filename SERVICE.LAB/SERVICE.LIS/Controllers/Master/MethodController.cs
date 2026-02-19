using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MethodController : ControllerBase
    {
        private readonly IMethodRepository _MethodRepository;
        private readonly IAuditService _auditService;
        public MethodController(IMethodRepository noteRepository, IAuditService auditService)
        {
            _MethodRepository = noteRepository;
            _auditService = auditService;
        }

        #region Get Method Details
        /// <summary>
        /// Get Method Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Method/GetMethodDetails")]
        public IEnumerable<TblMethod> GetMethodDetails(GetCommonMasterRequest masterRequest)
        {
            List<TblMethod> Objresult = new List<TblMethod>();
            try
            {
                Objresult = _MethodRepository.GetMethods(masterRequest);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MethodController.GetMethodDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, masterRequest.venuebranchno, 0);
            }
            return Objresult;
        }
        #endregion

        #region Insert Method 
        /// <summary>
        /// Insert Method 
        /// </summary>
        /// <param name="Methoditem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/Method/InsertMethodDetails")]
        public IActionResult InsertMethodDetails([FromBody] TblMethod Methoditem)
        {
            List<MethodResponse> Objresult = new List<MethodResponse>();
            try
            {
                using (var auditScope = new AuditScope<TblMethod>(Methoditem, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertMethodDetails(Methoditem);
                    if (!_errormsg.status)
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "METHOD" + Methoditem.VenueNo;
                        Objresult = _MethodRepository.InsertMethodDetails(Methoditem);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MethodController.InsertMethodDetails" + Methoditem.MethodNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Methoditem.VenueNo, Methoditem.VenueBranchNo, 0);
            }
            return Ok(Objresult);
        }
        #endregion
    }
}