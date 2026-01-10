using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Audit
{
    public interface IAuditService
    {
        User User { get; set; }
        void LogChanges(List<AuditLogEntry> entries);
        Dictionary<string, Dictionary<string, object>> Query(string query, string entityIdProperty, object ids);
    }
}
