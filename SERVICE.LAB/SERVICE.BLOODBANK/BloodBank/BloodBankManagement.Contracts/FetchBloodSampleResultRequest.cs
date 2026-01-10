using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Contracts
{
    public record FetchBloodSampleResultRequest
    (
        List<string>? Statuses,
        string? NRICNumber,
        int? NumberOfRecords,
        bool IsAttachInSampleReceiving,
        string? DonationId,
        List<Int64>? RegistrationIds,
        DateTime? StartDate,
        DateTime? EndDate,
        bool IsRegistrationsList,
        string? LabAccessionNumber,
        bool IsBloodProductReturnOrBloodTransfusion
    );
}