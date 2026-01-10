using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record RemoveInventoryAssociationRequest
    (
        Int64 RegistrationId, 
        Int64 InventoryId,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime LastModifiedDateTime,
        bool DeleteEntry
    ); 
   
}