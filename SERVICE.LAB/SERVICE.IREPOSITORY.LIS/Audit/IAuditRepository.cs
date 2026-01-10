using DEV.Model.Integration;
using DEV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEV.Model.Audit;
using Shared.Audit;

namespace Dev.IRepository.Audit
{
    public  interface IAuditRepository
    {
        Task<List<AuditLogEntry>> GetAuditDetails(AuditRequest request, UserClaimsIdentity user);
    }
}
