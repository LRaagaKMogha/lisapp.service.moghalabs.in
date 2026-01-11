using Service.Model.Integration;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model.Audit;
using Shared.Audit;

namespace Dev.IRepository.Audit
{
    public  interface IAuditRepository
    {
        Task<List<AuditLogEntry>> GetAuditDetails(AuditRequest request, UserClaimsIdentity user);
    }
}
