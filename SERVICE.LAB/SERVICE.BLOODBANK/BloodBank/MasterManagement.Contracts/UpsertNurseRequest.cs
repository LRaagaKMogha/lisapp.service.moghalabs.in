using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record UpsertNurseRequest(
        string Name,
        string Description,
        string EmployeeId,
        bool IsActive,
        Int64 ModifiedBy
    );

}