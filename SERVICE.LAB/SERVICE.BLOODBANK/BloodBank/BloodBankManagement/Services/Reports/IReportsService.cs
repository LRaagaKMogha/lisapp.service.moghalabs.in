using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.Reports
{
    public interface IReportsService
    {
        Task<ErrorOr<List<BloodBankInventory>>> GetBloodBankInventories(Contracts.FetchBloodBankInventoriesRequest request);
        Task<ErrorOr<List<BloodBankInventoryTransaction>>> GetBloodBankInventoryTransactions(Int64 inventoryId);
        Task<ErrorOr<List<Contracts.BloodBankRegistration>>> GetRequestsHistory(Contracts.FetchPatientHistoryRequest request);
        Task<ErrorOr<List<Models.BloodBankRegistration>>> GetBillingHistory(FetchPatientBillingRequest request);

    }
}