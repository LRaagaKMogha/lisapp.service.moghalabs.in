using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record UpsertLookupRequest(
        string Name,
        string Description,
        string Code,
        bool IsActive,
        string Type
    );

}