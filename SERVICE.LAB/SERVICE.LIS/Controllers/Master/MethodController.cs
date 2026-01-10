using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers.Master
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
            List<TblMethod> objresult = new List<TblMethod>();
            try
            {
                objresult = _MethodRepository.GetMethods(masterRequest);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MethodController.GetMethodDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, masterRequest.venuebranchno, 0);
            }
            return objresult;
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
            List<MethodResponse> objresult = new List<MethodResponse>();
            try
            {
                using (var auditScope = new AuditScope<TblMethod>(Methoditem, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertMethodDetails(Methoditem);
                    if (!_errormsg.status)
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "METHOD" + Methoditem.VenueNo;
                        objresult = _MethodRepository.InsertMethodDetails(Methoditem);
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
            return Ok(objresult);
        }
        #endregion
    }
}