using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QCManagement.Contracts
{
    public record ControlMasterRequest(
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
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        List<TestControlSamplesRequest> TestControlSamples,
        bool IsActive,
        string Status,
        bool IsQualitative,
        bool IsAntibiotic
    );
}