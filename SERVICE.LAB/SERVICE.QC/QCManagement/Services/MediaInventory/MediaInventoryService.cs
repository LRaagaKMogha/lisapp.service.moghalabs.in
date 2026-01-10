using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QC.Helpers;
using QCManagement.Contracts;
using QCManagement.ServiceErrors;

namespace QCManagement.Services.MediaInventory
{
    public class MediaInventoryService : IMediaInventoryService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration Configuration;

        public MediaInventoryService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }


        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR MediaInventorySequence", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

        public async Task<ErrorOr<List<Models.MediaInventory>>> CreateMediaInventory(List<Models.MediaInventory> inventory)
        {
            if (inventory != null)
            {
                try
                {
                    await dataContext.MediaInventories.AddRangeAsync(inventory);
                    await dataContext.SaveChangesAsync();
                    return inventory;
                }
                catch (Exception exp)
                {

                }

            }
            return Errors.MediaInventory.NotFound;
        }

        public async Task<ErrorOr<Models.MediaInventory>> GetMediaInventory(Int64 id)
        {
            var data = await dataContext.MediaInventories.FindAsync(id);
            if (data != null) return data;
            return Errors.MediaInventory.NotFound;
        }

        public async Task<ErrorOr<List<Models.MediaInventory>>> GetMediaInventories(MediaInventoryFilterRequest request)
        {
            try
            {
                var query = dataContext.MediaInventories.AsQueryable();
                Expression<Func<Models.MediaInventory, bool>> mediaName = e => e.MediaId == request.MediaId;
                Expression<Func<Models.MediaInventory, bool>> mediaLotNumber = e => e.MediaLotNumber.Contains(request.MediaLotNo ?? "") ;
                Expression<Func<Models.MediaInventory, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.MediaInventory, bool>> inactiveStatus = e => e.IsActive == false;


                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.MediaInventories.Where(x => x.ReceivedDateAndTime >= request.StartDate && x.ReceivedDateAndTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.MediaInventories.Where(x => x.IsActive == true);
                if (request.MediaId.GetValueOrDefault() > 0) query = query.Where(mediaName);
                if (!string.IsNullOrEmpty(request.MediaLotNo)) query = query.Where(mediaLotNumber);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.MediaInventory.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.MediaInventory.NotFound;
        }

        public async Task<ErrorOr<bool>> UpdateInventoryStatus(List<Int64> InventoryIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool UpdateInventoryComments = false)
        {
            InventoryIds.ForEach(x =>
                               {
                                   var inventory = dataContext.MediaInventories.Find(x);
                                   if (inventory != null && inventory.Status != status)
                                   {
                                       inventory.Status = status;
                                       inventory.ModifiedBy = modifiedBy;
                                       inventory.ModifiedByUserName = modifiedByUserName;
                                       inventory.LastModifiedDateTime = lastModifiedDateTime;
                                       if (comments != "" && UpdateInventoryComments)
                                           inventory.Remarks = comments;
                                       dataContext.MediaInventories.Update(inventory);
                                   }
                               });

            await dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<ErrorOr<List<Models.MediaInventory>>> UpsertMediaInventories(List<Models.MediaInventory> inventories)
        {
            var batchId = 0;
            if (inventories.Any(x => x.Identifier == 0))
                batchId = GetNextSequenceNumber();
            inventories.ForEach(async inventory =>
                       {
                           var currentinventory = dataContext.MediaInventories.FirstOrDefault(x => x.Identifier == inventory.Identifier);
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
                               currentinventory.MediaId = inventory.MediaId;
                               currentinventory.MediaLotNumber = inventory.MediaLotNumber;
                               currentinventory.ReceivedDateAndTime = inventory.ReceivedDateAndTime;
                               currentinventory.ExpirationDateAndTime = inventory.ExpirationDateAndTime;
                               currentinventory.Colour = inventory.Colour;
                               currentinventory.Crack = inventory.Crack;
                               currentinventory.Contaminate = inventory.Contaminate;
                               currentinventory.Leakage = inventory.Leakage;
                               currentinventory.Turbid = inventory.Turbid;
                               currentinventory.VolumeOrAgarThickness = inventory.VolumeOrAgarThickness;
                               currentinventory.Sterlity = inventory.Sterlity;
                               currentinventory.Vividity = inventory.Vividity;
                               currentinventory.Remarks = inventory.Remarks;
                               currentinventory.Status = inventory.Status;
                               currentinventory.ModifiedBy = inventory.ModifiedBy;
                               currentinventory.ModifiedByUserName = inventory.ModifiedByUserName;
                               currentinventory.LastModifiedDateTime = inventory.LastModifiedDateTime;
                               currentinventory.IsActive = inventory.IsActive;
                               dataContext.MediaInventories.Update(currentinventory);
                           }
                           else
                           {
                               inventory.BatchId = batchId;
                               await dataContext.MediaInventories.AddAsync(inventory);
                           }


                       });

            await this.dataContext.SaveChangesAsync();
            return inventories;
        }

    }
}
