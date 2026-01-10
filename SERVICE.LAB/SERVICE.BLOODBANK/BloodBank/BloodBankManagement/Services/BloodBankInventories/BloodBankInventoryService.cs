using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using ErrorOr;
using BloodBankManagement.ServiceErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using BloodBankManagement.Contracts;
using BloodBankManagement.Services.StartupServices;

namespace BloodBankManagement.Services.BloodBankInventories
{
    public class BloodBankInventoryService : IBloodBankInventoryService
    {
        private readonly BloodBankDataContext dataContext;

        public BloodBankInventoryService(BloodBankDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ErrorOr<List<BloodBankInventory>>> GetBloodBankInventories(FetchBloodBankInventoriesRequest request)
        {
            var statuses = request.Statuses != null ? request.Statuses : new List<string>();
            var inventoryIds = request.InventoryIds != null ? request.InventoryIds : new List<long>();
            var expirationStatuses = new List<string>() { "available" };
            var checkForExpirationDate = statuses.Count > 0 && statuses.Any(x => expirationStatuses.Any(y => x == y));
            var expirationDate = request.ExpirationDate ?? DateTime.Now;
            if (!statuses.Any(x => x == "Expired") || request.ExactSearch)
            {
                if (!string.IsNullOrEmpty(request.DonationId))
                {
                    return await dataContext.BloodBankInventories.Where(x => x.DonationId.Contains(request.DonationId)).OrderBy(x => x.ExpirationDateAndTime).ToListAsync();
                }
                else if (inventoryIds.Count > 0)
                {
                    return await dataContext.BloodBankInventories.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate && (inventoryIds.Count == 0 || inventoryIds.Any(id => id == x.Identifier)) && (statuses.Count == 0 || statuses.Any(y => y == x.Status)) && (!checkForExpirationDate || x.ExpirationDateAndTime > expirationDate)).Include(x => x.Transactions).AsSplitQuery().OrderBy(x => x.ExpirationDateAndTime).ToListAsync();
                }
                return await dataContext.BloodBankInventories.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate && (inventoryIds.Count == 0 || inventoryIds.Any(id => id == x.Identifier)) && (statuses.Count == 0 || statuses.Any(y => y == x.Status)) && (!checkForExpirationDate || x.ExpirationDateAndTime > expirationDate)).OrderBy(x => x.ExpirationDateAndTime).ToListAsync();
            }
            else
            {
                var expiredInventories = await dataContext.BloodBankInventories.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate && (statuses.Count == 0 || statuses.Any(y => y == x.Status) || x.ExpirationDateAndTime <= expirationDate)).OrderBy(x => x.ExpirationDateAndTime).ToListAsync();
                expiredInventories.ForEach(x => x.Status = "Expired");
                return expiredInventories;
            }

        }

        public async Task<ErrorOr<BloodBankInventory>> GetBloodBankInventory(long id)
        {
            var data = await dataContext.BloodBankInventories.FirstOrDefaultAsync(x => x.Identifier == id);
            if (data != null) return data;
            return Errors.BloodBankInventory.NotFound;
        }

        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR BatchId", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

        public async Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false)
        {
            for (var i = 0; i < InventoryIds.Count; i++)
            {
                var x = InventoryIds[i];
                var inventory = dataContext.BloodBankInventories.Find(x);
                var chargable = false;
                if (inventory != null && inventory.Status != status)
                {
                    chargable = inventory.Status == "Quarantine" && status == "available";
                    inventory.Status = status;
                    inventory.ModifiedBy = modifiedBy;
                    inventory.ModifiedByUserName = modifiedByUserName;
                    inventory.LastModifiedDateTime = lastModifiedDateTime;
                    if (comments != "" && UpdateInventoryComments)
                        inventory.Comments = comments;
                    dataContext.BloodBankInventories.Update(inventory);
                    var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                    await this.dataContext.bloodBankInventoryTransactions.AddAsync(inventoryTransaction);
                    if (chargable)
                    {
                        var itemToUpdate = dataContext.BloodBankBillings.OrderByDescending(x => x.LastModifiedDateTime).FirstOrDefault(x => x.Status == "ProductIssued" && x.ProductId == inventory.ProductCode && ("," + x.EntityId + ",").Contains("," + inventory.Identifier + ","));
                        if(itemToUpdate != null)
                        {
                            itemToUpdate.Unit = itemToUpdate.Unit - 1;
                            itemToUpdate.Price = itemToUpdate.MRP * itemToUpdate.Unit;
                        }
                    }
                }
            };

            await dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<ErrorOr<List<InventoryValidation>>> ValidationInventories(List<InventoryValidation> inventories)
        {
            var response = new List<InventoryValidation>();
            inventories.ForEach(inventory =>
            {
                var inventoryFromDB = dataContext.BloodBankInventories.FirstOrDefault(row => row.DonationId == inventory.DonationId && row.ProductCode == inventory.ProductId && row.Status != "ReturnedtoBSG");
                if (inventoryFromDB != null) response.Add(new InventoryValidation(inventoryFromDB.DonationId, inventoryFromDB.ProductCode));
            });
            return response;
        }
        public async Task<ErrorOr<List<BloodBankInventory>>> UpsertBloodBankInventories(List<BloodBankInventory> inventories)
        {
            var batchId = 0;
            if (inventories.Any(x => x.Identifier == 0))
                batchId = GetNextSequenceNumber();
            for (var i = 0; i < inventories.Count; i++)
            {
                var inventory = inventories[i];
                var currentinventory = dataContext.BloodBankInventories.FirstOrDefault(x => x.Identifier == inventory.Identifier || (x.Status == "ReturnedToBSG" && x.DonationId == inventory.DonationId));
                if (currentinventory != null)
                {
                    if (inventory.Identifier == 0)
                    {
                        inventory.Identifier = currentinventory.Identifier;
                        currentinventory.BatchId = batchId.ToString();
                    }
                    else
                    {
                        currentinventory.BatchId = inventory.BatchId;
                    }
                    currentinventory.DonationId = inventory.DonationId;
                    currentinventory.CalculatedDonationId = inventory.CalculatedDonationId;
                    currentinventory.ProductCode = inventory.ProductCode;
                    currentinventory.ExpirationDateAndTime = inventory.ExpirationDateAndTime;
                    currentinventory.AboOnLabel = inventory.AboOnLabel;
                    currentinventory.Volume = inventory.Volume;
                    currentinventory.AntiAGrading = inventory.AntiAGrading;
                    currentinventory.AntiBGrading = inventory.AntiBGrading;
                    currentinventory.AntiABGrading = inventory.AntiABGrading;
                    currentinventory.AboResult = inventory.AboResult;
                    if (currentinventory.Status == "Acknowledged" && inventory.Status == "available")
                    {
                        currentinventory.AboPerformedByUserName = inventory.AboPerformedByUserName;
                        currentinventory.AboPerformedByDateTime = inventory.LastModifiedDateTime;
                    }
                    currentinventory.Status = inventory.Status;
                    currentinventory.IsRejected = inventory.IsRejected;
                    currentinventory.Comments = inventory.Comments;
                    currentinventory.Temprature = inventory.Temprature;
                    currentinventory.ModifiedBy = inventory.ModifiedBy;
                    currentinventory.ModifiedByUserName = inventory.ModifiedByUserName;
                    currentinventory.LastModifiedDateTime = inventory.LastModifiedDateTime;
                    currentinventory.Antibodies = inventory.Antibodies;
                    dataContext.BloodBankInventories.Update(currentinventory);
                }
                else
                {
                    inventory.BatchId = batchId.ToString();
                    if ((inventory.Status == "available" && !GlobalConstants.RedCellsProducts.Any(row => row.Identifier == inventory.ProductCode)))
                    {
                        inventory.AboPerformedByDateTime = inventory.LastModifiedDateTime;
                    }
                    await dataContext.BloodBankInventories.AddAsync(inventory);
                }


            };

            await this.dataContext.SaveChangesAsync();
            for (var i = 0; i < inventories.Count; i++)
            {
                var inventory = inventories[i];
                var inventoryTransaction = new BloodBankInventoryTransaction(inventory.Identifier, inventory.Status, inventory.Comments, inventory.ModifiedBy, inventory.ModifiedByUserName, inventory.LastModifiedDateTime);
                await this.dataContext.bloodBankInventoryTransactions.AddAsync(inventoryTransaction);
            };
            await this.dataContext.SaveChangesAsync();
            return inventories;
        }
    }
}
