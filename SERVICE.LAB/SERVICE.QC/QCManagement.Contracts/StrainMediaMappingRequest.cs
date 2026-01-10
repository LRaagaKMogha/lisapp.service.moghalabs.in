using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record UpsertStrainMediaMappingsRequest(
        List<StrainMediaMappingRequest> Mappings
    );
    public record StrainMediaMappingRequest(
    long Identifier,
    DateTime ReceivedDateAndTime,
    long StrainInventoryId,
    long MediaInventoryId,
    string Remarks,
    string Status,
    long ModifiedBy,
    string ModifiedByUserName,
    DateTime LastModifiedDateTime,
    bool IsActive
);
}