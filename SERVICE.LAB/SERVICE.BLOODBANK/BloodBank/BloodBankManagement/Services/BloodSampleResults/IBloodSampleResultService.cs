using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.BloodSampleResults
{
    public interface IBloodSampleResultService
    {
        Task<ErrorOr<List<BloodSampleResult>>> UpsertBloodSampleResultStatus(List<Int64> bloodSampleResultIds, string status);
        Task<ErrorOr<List<BloodSampleResult>>> UpsertBloodSampleResult(List<BloodSampleResult> results);
        Task<ErrorOr<List<BloodSampleResult>>> AddBloodSampleResults(List<BloodSampleResult> request);
        Task<bool> UpdateBloodSampleInventoriesStatus(Int64 registrationId);
    }
}
