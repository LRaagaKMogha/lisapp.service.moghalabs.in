using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record MicroQCMasterResponse(
        long Identifier,
        long CultureReagentTestId,
        string PositiveStrainIds,
        string NegativeStrainIds,
        string Frequency,
        string Status,
        long ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool IsActive
    );

}