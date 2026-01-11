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
    public class ContainerController : ControllerBase
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IAuditService _auditService;
        public ContainerController(IContainerRepository noteRepository, IAuditService auditService)
        {
            _containerRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Container/Getcontainermaster")]
        public IEnumerable<TblContainer> Getcontainermaster(ContainerMasterRequest containerRequest)
        {
            List<TblContainer> result = new List<TblContainer>();
            try
            {
                result = _containerRepository.Getcontainermaster(containerRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ContainerController.GetcontainerDetails" + containerRequest.containerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, containerRequest.venueNo, containerRequest.venueBranchno, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Container/Insertcontainermaster")]
        public ActionResult<ContainerMasterResponse> Insertcontainermaster(TblContainer tblContainer)
        {
            ContainerMasterResponse objresult = new ContainerMasterResponse();
            try
            {
                using (var auditScope = new AuditScope<TblContainer>(tblContainer, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.Insertcontainermaster(tblContainer);
                    if (!_errormsg.status)
                    {
                        objresult = _containerRepository.Insertcontainermaster(tblContainer);
                        string _CacheKey = CacheKeys.CommonMaster + "CONTAINER" + tblContainer.venueNo + tblContainer.venueBranchno;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ContainerController.Insertcontainermaster" + tblContainer.containerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblContainer.venueNo, tblContainer.venueBranchno, 0);
            }
            return Ok(objresult);
        }
    }
}