using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.StrainInventory
{
    public interface IStrainInventoryService
    {
        Task<ErrorOr<List<Models.StrainInventory>>> CreateStrainInventory(List<Models.StrainInventory> strainInventory);
        Task<ErrorOr<List<Models.StrainInventory>>> UpsertStrainInventories(List<Models.StrainInventory> inventories);
        Task<ErrorOr<Models.StrainInventory>> GetStrainInventory(Int64 id);
        Task<ErrorOr<List<Models.StrainInventory>>> GetStrainInventories(StrainInventoryFilterRequest request);
        Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false);
    }
}
