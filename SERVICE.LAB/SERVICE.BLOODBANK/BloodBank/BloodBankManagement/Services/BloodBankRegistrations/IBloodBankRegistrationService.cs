using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using BloodBankManagement.Models;

namespace BloodBankManagement.Services.BloodBankRegistrations
{
    public interface IBloodBankRegistrationService
    {
        Task<ErrorOr<BloodBankRegistration>> CreatePatientRegistration(BloodBankRegistration incomingRegistration);
        Task<ErrorOr<BloodBankRegistration>> GetPatientRegistration(Int64 id);
        Task<ErrorOr<List<string>>> RecallRegistration(Int64 id);
        Task<ErrorOr<List<RegistrationTransaction>>> GetPatientRegistrationTransaction(Int64 id);
        Task<ErrorOr<List<Contracts.UpdateRegistration>>> UpdateRegistrations(Contracts.UpdateBloodBankRegistrationStatusRequest request);
        Task<bool> UpdateRegistrationStatus(List<Int64> registrationIds, string status, Int64 modifiedBy, string modifiedByUserName = "");
        Task<ErrorOr<BloodBankRegistration>> UpdateIssueProduct(Contracts.UpdateBloodSampleInventoryStatusRequest request);
        Task<ErrorOr<List<Contracts.TestResponse>>> GetBloodBankTests();
        Task<ErrorOr<List<Contracts.GroupResponse>>> GetBloodBankGroups();
        Task<ErrorOr<List<Contracts.SubTestResponse>>> GetBloodBankSubTests();
        Task<ErrorOr<List<Contracts.TestPickListResponse>>> GetBloodBankSubTestsPickList();
        Task<ErrorOr<List<BloodBankRegistration>>> GetBloodBankRegistrationsForResult(Contracts.FetchBloodSampleResultRequest request);
        Task<ErrorOr<Contracts.UploadDocResponse>> BulkUploadFile(List<Contracts.BulkFileUpload> lstjDTO);
        Task<ErrorOr<List<Contracts.BulkFileUpload>>> ConvertToBase64(Contracts.BulkFileUpload objjDTO);
    }
}