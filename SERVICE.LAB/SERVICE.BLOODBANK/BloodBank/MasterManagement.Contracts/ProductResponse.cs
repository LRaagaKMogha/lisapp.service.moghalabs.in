using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Contracts
{
    public record Product(
        Int64 Identifier,
        string Description,
        string Code,
        Int64 CategoryId,
        DateTime EffectiveFromDate,
        DateTime? EffectiveToDate, 
        int MinCount, 
        int MaxCount, 
        int ThresholdCount,        
        List<Int64> SpecialRequirementIds,
        bool IsActive,
        bool IsThawed,
        DateTime LastModifiedDateTime        
    );

}