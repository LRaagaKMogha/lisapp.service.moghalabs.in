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
using Azure.Core;
//using MasterManagement.Contracts;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _UnitRepository;
        private readonly IAuditService _auditService;
        public UnitController(IUnitRepository noteRepository, IAuditService auditService)
        {
            _UnitRepository = noteRepository;
            _auditService = auditService;
        }

        #region Get Unit Details
        /// <summary>
        /// Get Unit Details
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Unit/GetUnitDetails")]
        public IEnumerable<lstunits> GetUnitDetails(reqUnits req)
        {
            List<lstunits> objresult = new List<lstunits>();
            try
            {
             
                objresult = _UnitRepository.GetUnits(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UnitController.GetUnitDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return objresult;
        }

        #endregion

        #region Insert Unit 
        /// <summary>
        /// Insert Unit 
        /// </summary>
        /// <param name="Unititem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/Unit/InsertUnitDetails")]
        public ActionResult<rtnUnit> InsertUnitDetails([FromBody] TblUnits Unititem)
        {
            rtnUnit result = new rtnUnit();
            try
            {
                using (var auditScope = new AuditScope<TblUnits>(Unititem, _auditService))
                {

                    var _errormsg = LaboratoryMasterValidation.InsertUnitDetails(Unititem);
                    if (!_errormsg.status)
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "UNITS" + Unititem.VenueNo + Unititem.VenueBranchNo;
                        result = _UnitRepository.InsertUnitDetails(Unititem);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UnitController.GetUnitDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, Unititem.VenueNo, Unititem.VenueBranchNo, 0);
            }
            return Ok(result);
        }
        #endregion
    }
}