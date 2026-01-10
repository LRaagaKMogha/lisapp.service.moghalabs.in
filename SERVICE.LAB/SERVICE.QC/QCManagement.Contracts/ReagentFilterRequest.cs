using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record ReagentFilterRequest
    (
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime? ExpirationDate,
        bool showAllActive,
        Int64? DepartmentId,
        string? ReagentName,
        bool Status = true
   );
}
