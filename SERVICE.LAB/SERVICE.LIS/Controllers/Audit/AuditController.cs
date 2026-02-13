using Service.Model.Integration;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Service.Model.Audit;
using Microsoft.Office.Interop.Word;
using Shared.Audit;
using AutoMapper;
using Service.IRepository;
using Service.IRepository.Audit;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers.Audit
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _auditRepository;
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
