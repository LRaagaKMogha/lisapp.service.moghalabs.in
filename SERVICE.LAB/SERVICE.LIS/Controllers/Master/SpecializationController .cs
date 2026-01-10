using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEV.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly IspecializationRepository _specializationRepository;
        private readonly IAuditService _auditService;
        public SpecializationController(IspecializationRepository noteRepository, IAuditService auditService)
        {
            _specializationRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Specialization/Getspecializationmaster")]
        public IEnumerable<Tblspecialization> Getspecializationmaster(SpecializationMasterRequest specializationitem)
        {
             List<Tblspecialization> result = new List<Tblspecialization>();
            try
            {               
                result= _specializationRepository.Getspecializationmaster(specializationitem);             
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "SpecializationController.Getspecializationmaster - " + specializationitem.specializationNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, specializationitem.venueNo,0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Specialization/Insertspecializatiomaster")]
        public ActionResult<SpecializationMasterResponse> Insertspecializatiomaster(Tblspecialization tblspecialization)
        {
            SpecializationMasterResponse objresult = new SpecializationMasterResponse();
            try
            {
                using(var auditScoped = new AuditScope<Tblspecialization>(tblspecialization, _auditService))
                {
                    var _errormsg = MasterValidation.Insertspecializatiomaster(tblspecialization);
                    if (!_errormsg.status)
                    {
                        objresult = _specializationRepository.Insertspecializatiomaster(tblspecialization);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SpecializationController.Insertspecializatiomaster - " + tblspecialization.specializationNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tblspecialization.venueNo, tblspecialization.venueBranchno, tblspecialization.userNo);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/Specialization/CheckMasterNameExists")]
        public int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest)
        {
            int iresult = 0;
            try
            {
                iresult = _specializationRepository.CheckMasterNameExists(checkMasterNameExistsRequest);
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "SpecializationController.CheckMasterNameExists - " + 0, ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return iresult;
        }
    }
}