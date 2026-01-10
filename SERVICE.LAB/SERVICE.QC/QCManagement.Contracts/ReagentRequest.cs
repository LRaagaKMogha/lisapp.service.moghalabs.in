using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record ReagentsRequest 
        (
            List<ReagentRequest> Reagents
        );
    public record ReagentRequest
   (
        Int64 Identifier,
        string Name,
        string LotNo,
        string SequenceNo,
        DateTime ExpirationDate,
        DateTime? LotSetupOrInstallationDate,
        Int64 ManufacturerID,
        Int64 DistributorID,
        bool Status,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        Int64 DepartmentId
   );
}