using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record PrepareControlMasterRequest
    (
        Int64 Identifier,
        DateTime? PreparationDateTime,
        Int64? PreparedBy,
        string? PreparedByUserName,
        Int64? StorageTemperature,
        Int64? AliquoteCount,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime        
    );
}