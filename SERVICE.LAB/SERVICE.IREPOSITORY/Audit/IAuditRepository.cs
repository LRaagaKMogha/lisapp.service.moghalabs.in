using Service.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Model.Audit;
using Shared.Audit;

namespace Service.IRepository.Audit
{
    public  interface IAuditRepository
    {
        Task<List<AuditLogEntry>> GetAuditDetails(AuditRequest request, UserClaimsIdentity user);
    }
}
