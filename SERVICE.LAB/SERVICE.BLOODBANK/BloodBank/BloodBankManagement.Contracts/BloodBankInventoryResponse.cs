using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record BloodBankInventoryResponse
   (
        Int64 Identifier,
        string BatchId,
        string DonationId,
        string CalculatedDonationId,
        Int64 ProductCode,
        DateTime ExpirationDateAndTime,
        string AboOnLabel,
        string Volume,
        string AntiAGrading,
        string AntiBGrading,
        string AntiABGrading,
        string AboResult,
        string AboPerformedByUserName,
        DateTime? AboPerformedByDateTime,
        string Status,
        bool IsRejected,
        bool IsVisualInspectionPassed,
        string Comments,
        string Temprature,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        string Antibodies,
        Int64 ModifiedProductId,
        DateTime LastModifiedDateTime,
        DateTime? CreatedDateTime
   );
}
