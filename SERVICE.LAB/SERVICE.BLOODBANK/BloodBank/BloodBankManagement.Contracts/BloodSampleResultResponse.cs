using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodSampleResultResponse
(
        Int64 Identifier,
        Int64 RegistrationId,
        Int64 ContainerId,
        Int64 TestId,
        Int64 ParentTestId,
        Int64 InventoryId,
        string TestName,
        string Unit,
        string TestValue,
        string Comments,
        string BarCode,
        string Status,
        bool IsRejected,
        Int64? ParentRegistrationId,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool ReCheck,
        bool SentToHSA,
        bool? interfaceispicked,
        int? IsUploadAvail,
        Int64 GroupId
    );
}
