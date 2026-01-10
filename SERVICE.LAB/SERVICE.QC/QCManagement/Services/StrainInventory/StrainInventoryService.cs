using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QCManagement.ServiceErrors;
using QC.Helpers;
using QCManagement.Contracts;
using System.Linq.Expressions;

namespace QCManagement.Services.StrainInventory
{
    public class StrainInventoryService : IStrainInventoryService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration Configuration;

        public StrainInventoryService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR StrainInventorySequence", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

        public async Task<ErrorOr<List<Models.StrainInventory>>> CreateStrainInventory(List<Models.StrainInventory> inventory)
        {
            if (inventory != null)
            {
                await dataContext.StrainInventories.AddRangeAsync(inventory);
                await dataContext.SaveChangesAsync();
                return inventory;
            }
            return Errors.StrainInventory.NotFound;
        }

        public async Task<ErrorOr<Models.StrainInventory>> GetStrainInventory(Int64 id)
        {
            var data = await dataContext.StrainInventories.FindAsync(id);
            if (data != null) return data;
            return Errors.StrainInventory.NotFound;
        }

        public async Task<ErrorOr<List<Models.StrainInventory>>> GetStrainInventories(StrainInventoryFilterRequest request)
        {
            try
            {
                Expression<Func<Models.StrainInventory, bool>> strainMasterIds = e => request.StrainMasterIds != null && request.StrainMasterIds.Any(y => y == e.StrainId);
                Expression<Func<Models.StrainInventory, bool>> StrainId = e => request.StrainId == e.StrainId;
                Expression<Func<Models.StrainInventory, bool>> StrainIds = e => request.StrainIds.Any(y => y == e.Identifier) ;
                Expression<Func<Models.StrainInventory, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.StrainInventory, bool>> inactiveStatus = e => e.IsActive == false;

                var query = dataContext.StrainInventories.AsQueryable();
                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.StrainInventories.Where(x => x.ReceivedDateAndTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.StrainInventories.Where(x => x.IsActive == true);
                if (request.StrainMasterIds != null && request.StrainMasterIds.Count > 0)
                    query = query.Where(strainMasterIds);
                if (request.StrainId.GetValueOrDefault() > 0)
                    query = query.Where(StrainId);
                if (request.StrainIds != null && request.StrainIds.Count > 0)
                    query = query.Where(StrainIds);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.StrainInventory.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.StrainInventory.NotFound;
        }

        public async Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false)
        {
            InventoryIds.ForEach(x =>
            {
                var inventory = dataContext.StrainInventories.Find(x);
                if (inventory != null && inventory.Status != status)
                {
                    inventory.Status = status;
                    inventory.ModifiedBy = modifiedBy;
                    inventory.ModifiedByUserName = modifiedByUserName;
                    inventory.LastModifiedDateTime = lastModifiedDateTime;
                    if (comments != "" && UpdateInventoryComments)
                        inventory.Remarks = comments;
                    dataContext.StrainInventories.Update(inventory);
                }
            });

            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorOr<List<Models.StrainInventory>>> UpsertStrainInventories(List<Models.StrainInventory> inventories)
        {
            var batchId = 0;
            if (inventories.Any(x => x.Identifier == 0))
                batchId = GetNextSequenceNumber();
            inventories.ForEach(async inventory =>
            {
                var currentinventory = dataContext.StrainInventories.FirstOrDefault(x => x.Identifier == inventory.Identifier);
                if (currentinventory != null)
                {
                    if (inventory.Identifier == 0)
                    {
                        inventory.Identifier = currentinventory.Identifier;
                        currentinventory.BatchId = batchId;
                    }
                    else
                    {
                        currentinventory.BatchId = inventory.BatchId;
                    }
                    currentinventory.StrainId = inventory.StrainId;
                    currentinventory.StrainLotNumber = inventory.StrainLotNumber;
                    currentinventory.ReceivedDateAndTime = inventory.ReceivedDateAndTime;
                    currentinventory.ExpirationDateAndTime = inventory.ExpirationDateAndTime;
                    currentinventory.Remarks = inventory.Remarks;
                    currentinventory.Status = inventory.Status;
                    currentinventory.ModifiedBy = inventory.ModifiedBy;
                    currentinventory.ModifiedByUserName = inventory.ModifiedByUserName;
                    currentinventory.LastModifiedDateTime = inventory.LastModifiedDateTime;
                    currentinventory.IsActive = inventory.IsActive;
                    dataContext.StrainInventories.Update(currentinventory);
                }
                else
                {
                    inventory.BatchId = batchId;
                    await dataContext.StrainInventories.AddAsync(inventory);
                }
            });

            await this.dataContext.SaveChangesAsync();
            return inventories;
        }
    }
}
