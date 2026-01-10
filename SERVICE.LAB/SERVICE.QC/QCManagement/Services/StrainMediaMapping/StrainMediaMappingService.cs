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

namespace QCManagement.Services.StrainMediaMapping
{
    public class StrainMediaMappingService : IStrainMediaMappingService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration configuration;

        public StrainMediaMappingService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<ErrorOr<List<Models.StrainMediaMapping>>> CreateStrainMediaMapping(List<Models.StrainMediaMapping> mappings)
        {
            if (mappings != null)
            {
                await dataContext.StrainMediaMappings.AddRangeAsync(mappings);
                await dataContext.SaveChangesAsync();
                return mappings;
            }
            return Errors.StrainMediaMapping.NotFound;
        }

        public async Task<ErrorOr<Models.StrainMediaMapping>> GetStrainMediaMapping(Int64 id)
        {
            var data = await dataContext.StrainMediaMappings.FindAsync(id);
            if (data != null) return data;
            return Errors.StrainMediaMapping.NotFound;
        }

        public async Task<ErrorOr<List<Models.StrainMediaMapping>>> GetStrainMediaMappings(StrainMediaMappingFilterRequest request)
        {
            try
            {
                Expression<Func<Models.StrainMediaMapping, bool>> strainInventoryId = e => e.StrainInventoryId == request.StrainInventoryId;
                Expression<Func<Models.StrainMediaMapping, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.StrainMediaMapping, bool>> inactiveStatus = e => e.IsActive == false;
                Expression<Func<Models.StrainMediaMapping, bool>> passmappingStatus = e => e.Status == "0";
                Expression<Func<Models.StrainMediaMapping, bool>> failmappingStatus = e => e.Status == "1";
                Expression<Func<Models.StrainMediaMapping, bool>> mediaInventoryId = e => e.MediaInventoryId == request.MediaInventoryId;

                var query = dataContext.StrainMediaMappings.AsQueryable();
                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.StrainMediaMappings.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.StrainMediaMappings.Where(x => x.IsActive == true);
                if (request.StrainInventoryId != null && request.StrainInventoryId > 0)
                    query = query.Where(strainInventoryId);
                if (request.MediaInventoryId != null && request.MediaInventoryId > 0)
                    query = query.Where(mediaInventoryId);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }

                if (!string.IsNullOrEmpty(request.MappingStatus))
                {
                    if (request.MappingStatus == "0") query = query.Where(passmappingStatus);
                    if (request.MappingStatus == "1") query = query.Where(failmappingStatus);
                }

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.StrainMediaMapping.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.StrainMediaMapping.NotFound;
        }

        public async Task<ErrorOr<bool>> UpdateMappingStatus(List<Int64> mappingIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateMappingComments = false)
        {
            mappingIds.ForEach(x =>
            {
                var mapping = dataContext.StrainMediaMappings.Find(x);
                if (mapping != null && mapping.Status != status)
                {
                    mapping.Status = status;
                    mapping.ModifiedBy = modifiedBy;
                    mapping.ModifiedByUserName = modifiedByUserName;
                    mapping.LastModifiedDateTime = lastModifiedDateTime;
                    if (comments != "" && updateMappingComments)
                        mapping.Remarks = comments;
                    dataContext.StrainMediaMappings.Update(mapping);
                }
            });

            await dataContext.SaveChangesAsync();
            return true;
        }
        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR StrainMediaMappingsSequence", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }
        public async Task<ErrorOr<List<Models.StrainMediaMapping>>> UpsertStrainMediaMappings(List<Models.StrainMediaMapping> mappings)
        {
            var batchId = 0;
            if (mappings.Any(x => x.Identifier == 0))
                batchId = GetNextSequenceNumber();
            mappings.ForEach(async mapping =>
            {
                var currentMapping = dataContext.StrainMediaMappings.FirstOrDefault(x => x.Identifier == mapping.Identifier);
                if (currentMapping != null)
                {
                    currentMapping.StrainInventoryId = mapping.StrainInventoryId;
                    currentMapping.MediaInventoryId = mapping.MediaInventoryId;
                    currentMapping.ReceivedDateAndTime = mapping.ReceivedDateAndTime;
                    currentMapping.Remarks = mapping.Remarks;
                    currentMapping.Status = mapping.Status;
                    currentMapping.ModifiedBy = mapping.ModifiedBy;
                    currentMapping.ModifiedByUserName = mapping.ModifiedByUserName;
                    currentMapping.LastModifiedDateTime = mapping.LastModifiedDateTime;
                    currentMapping.IsActive = mapping.IsActive;
                    if (mapping.Identifier == 0)
                    {
                        mapping.Identifier = currentMapping.Identifier;
                        currentMapping.BatchId = batchId;
                    }
                    dataContext.StrainMediaMappings.Update(currentMapping);
                }
                else
                {
                    mapping.BatchId = batchId;
                    await dataContext.StrainMediaMappings.AddAsync(mapping);
                }
            });

            await this.dataContext.SaveChangesAsync();
            return mappings;
        }
    }
}
