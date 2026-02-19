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
    public class SubtestheaderController : ControllerBase
    {
        private readonly ISubtestheaderRepository _SubtestheaderRepository;
        private readonly IAuditService _auditService;
        public SubtestheaderController(ISubtestheaderRepository noteRepository, IAuditService auditService)
        {
            _SubtestheaderRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Subtestheadermaster/GetSubtestheadermaster")]
        public IEnumerable<TblSubtestheader> GetSubtestheadermaster(SubtestheaderMasterRequest testheader)
        {
            List<TblSubtestheader> result = new List<TblSubtestheader>();
            try
            {
                result = _SubtestheaderRepository.GetSubtestheadermaster(testheader);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SubtestheaderController.GetSubtestheadermaster" + testheader.headerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, testheader.venueNo, testheader.venueBranchno, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Subtestheadermaster/InsertSubtestheadermaster")]
        public ActionResult<SubtestheaderMasterResponse> InsertSubtestheadermaster(TblSubtestheader testheader)
        {
            SubtestheaderMasterResponse Objresult = new SubtestheaderMasterResponse();
            try
            {
                using (var auditScoped = new AuditScope<TblSubtestheader>(testheader, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertSubtestheadermaster(testheader);
                    if (!_errormsg.status)
                    {
                        Objresult = _SubtestheaderRepository.InsertSubtestheadermaster(testheader);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SubtestheaderController.InsertSubtestheadermaster - " + testheader.headerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, testheader.venueNo, testheader.venueBranchno, 0);
            }
            return Ok(Objresult);
        }
    }
}