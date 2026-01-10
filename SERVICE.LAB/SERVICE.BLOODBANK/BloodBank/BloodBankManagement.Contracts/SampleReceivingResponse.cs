using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record SampleReceiving
    (
        Int64 Identifier,
        string NRICNumber,
        string PatientName,
        DateTime PatientDOB,
        Int64 GenderId,
        bool IsActive,
        DateTime LastModifiedDateTime
    );


}