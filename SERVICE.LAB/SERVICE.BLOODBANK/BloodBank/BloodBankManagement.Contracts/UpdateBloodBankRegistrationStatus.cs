using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record UpdateBloodBankRegistrationStatusRequest
    (
        List<UpdateRegistration> Registrations,
        string? Action
    );

    public record UpdateRegistration 
    (
        Int64 RegistrationId,
        string RegistrationStatus,
        string? RejectedReason,
        int IsActive,
        Int64 ModifiedBy,
        string ModifiedByUserName,
        DateTime? LastModifiedDateTime,
        DateTime? SampleReceivedDateTime
    );
}