using DEV.Model.Integration;
using DEV.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DEV.Model.Audit;
using Microsoft.Office.Interop.Word;
using Shared.Audit;
using AutoMapper;
using Dev.IRepository;
using Dev.IRepository.Audit;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.Audit
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _auditRepository;
        private IMapper _mapper;

        public AuditController(IMapper mapper, IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        [HttpPost]
        [Route("api/audit/getauditdata")]
        public async Task<List<AuditLogEntry>> GetAuditDetails(AuditRequest request)
        {
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            var response = await _auditRepository.GetAuditDetails(request, user);
            return response;
        }
    }
}
