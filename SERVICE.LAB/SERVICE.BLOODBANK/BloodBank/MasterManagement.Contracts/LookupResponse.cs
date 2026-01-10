using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record Lookup (
        Int64 Identifier,
        string Name,
        string Description,
        string Code,
          bool IsActive,
        DateTime? LastModifiedDateTime,
        DateTime? CreatedDateTime,
        int? ModifiedBy,
        int? CreatedBy,
        string Type
    );
    
}