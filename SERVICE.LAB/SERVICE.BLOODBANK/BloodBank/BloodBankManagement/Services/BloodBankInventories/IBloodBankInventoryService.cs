using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.BloodBankInventories
{
    public interface IBloodBankInventoryService
    {
        Task<ErrorOr<List<BloodBankInventory>>> UpsertBloodBankInventories(List<BloodBankInventory> inventories);
        Task<ErrorOr<List<InventoryValidation>>> ValidationInventories(List<InventoryValidation> inventories);
        Task<ErrorOr<List<BloodBankInventory>>> GetBloodBankInventories(Contracts.FetchBloodBankInventoriesRequest request);
        Task<ErrorOr<BloodBankInventory>> GetBloodBankInventory(Int64 id);

        Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false);
    }
}