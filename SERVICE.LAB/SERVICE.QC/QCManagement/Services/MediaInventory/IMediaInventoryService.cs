using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.MediaInventory
{
    public interface IMediaInventoryService
{
    Task<ErrorOr<List<Models.MediaInventory>>> CreateMediaInventory(List<Models.MediaInventory> mediaInventory);
    Task<ErrorOr<List<Models.MediaInventory>>> UpsertMediaInventories(List<Models.MediaInventory> inventories);
    Task<ErrorOr<Models.MediaInventory>> GetMediaInventory(Int64 id);
    Task<ErrorOr<List<Models.MediaInventory>>> GetMediaInventories(MediaInventoryFilterRequest request);
    Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false);
}

}