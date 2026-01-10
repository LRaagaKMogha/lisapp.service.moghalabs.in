using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.SampleReceiving
{
    public interface ISampleReceivingService
    {
        Task<ErrorOr<List<BloodBankRegistration>>> GetSampleReceivingList();
        Task<ErrorOr<List<string>>> GetBarCodes(Int64 registrationId, int numberOfBarCodes);
        Task<ErrorOr<List<BloodSample>>> GetActiveSamplesForPatient(Int64 patientId);
        Task<ErrorOr<List<BloodSample>>> SaveBloodSamplesList(List<BloodSample> request);
    }
}