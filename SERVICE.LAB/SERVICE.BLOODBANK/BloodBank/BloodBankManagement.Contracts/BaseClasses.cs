using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    [NotMapped]
    public record SampleResponse
   (
        Int64 Identifier,
        Int64 PatientId,
        Int64 RegistrationId,
        Int64 SampleTypeId,
        Int64 UnitCount,
        string TubeNo,
        string BarCode,
        bool IsActive,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        Int64? ParentRegistrationId,
        string Tests
   );

    public record PatientRegisteredProducts
    (
        Int64 Identifier,
        Int64 ProductId,
        decimal MRP,
        int Unit,
        decimal Price,
        List<BloodSampleInventoryResponse>? BloodSampleInventories = null
    );

}
