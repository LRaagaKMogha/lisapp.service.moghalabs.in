using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record ControlMasterResponse(
        Int64 Identifier,
        string ControlName,
        string ControlType,
        string LotNumber,
        DateTime ExpirationDate,
        Int64 ManufacturerID,
        Int64 DistributorID,
        string Notes,
        string Form, 
        Int64 Level,
        Int64 ReagentID,
        string Status,   
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        List<TestControlSamplesResponse> TestControlSamples,
        DateTime? PreparationDateTime,
        Int64? PreparedBy,
        string? PreparedByUserName,
        Int64? StorageTemperature,
        Int64? AliquoteCount,
        bool IsActive,
        bool IsQualitative,
        bool IsAntibiotic
    );
}