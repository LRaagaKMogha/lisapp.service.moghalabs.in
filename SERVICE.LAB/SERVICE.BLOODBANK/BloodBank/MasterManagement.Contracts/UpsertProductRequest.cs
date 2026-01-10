using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record UpsertProductRequest(
        string Code,
        string Description,
        Int64 CategoryId,
        DateTime EffectiveFromDate,
        DateTime EffectiveToDate, 
        int MinCount, 
        int MaxCount, 
        int ThresholdCount,
        Int64[] SpecialRequirementIds,
        bool IsActive,
        bool IsThawed
    );
}        