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

namespace QCManagement.Services.StrainMaster
{
    public class StrainMasterService : IStrainMasterService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration Configuration;

        public StrainMasterService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        public async Task<ErrorOr<List<Models.StrainMaster>>> CreateStrainMaster(List<Models.StrainMaster> strainMasters)
        {
            if (strainMasters != null)
            {
                await dataContext.StrainMasters.AddRangeAsync(strainMasters);
                await dataContext.SaveChangesAsync();
                return strainMasters;
            }
            return Errors.StrainMaster.NotFound;
        }

        public async Task<ErrorOr<Models.StrainMaster>> GetStrainMaster(Int64 id)
        {
            var data = await dataContext.StrainMasters.FindAsync(id);
            if (data != null) return data;
            return Errors.StrainMaster.NotFound;
        }

        public async Task<ErrorOr<List<Models.StrainMaster>>> GetStrainMasters(StrainMasterFilterRequest request)
        {
            try
            {
                Expression<Func<Models.StrainMaster, bool>> strainName = e => e.StrainName.Contains(request.StrainName ?? "");
                Expression<Func<Models.StrainMaster, bool>> strainCategoryId = e => request.StrainCategoryId == e.StrainCategoryId;
                Expression<Func<Models.StrainMaster, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.StrainMaster, bool>> inactiveStatus = e => e.IsActive == false;

                var query = dataContext.StrainMasters.AsQueryable();
                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.StrainMasters.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.StrainMasters.Where(x => x.IsActive == true);
                if (!string.IsNullOrEmpty(request.StrainName))
                    query = query.Where(strainName);
                if (request.StrainCategoryId.GetValueOrDefault() > 0)
                    query = query.Where(strainCategoryId);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.StrainMaster.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.StrainMaster.NotFound;
        }

        public async Task<ErrorOr<bool>> UpdateStrainMasterStatus(List<Int64> strainMasterIds, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateComments = false)
        {
            strainMasterIds.ForEach(x =>
            {
                var strainMaster = dataContext.StrainMasters.Find(x);
                if (strainMaster != null && strainMaster.Status != status)
                {
                    strainMaster.Status = status;
                    strainMaster.ModifiedBy = modifiedBy;
                    strainMaster.ModifiedByUserName = modifiedByUserName;
                    strainMaster.LastModifiedDateTime = lastModifiedDateTime;
                    // if (comments != "" && updateComments)
                    //     strainMaster.Remarks = comments;
                    dataContext.StrainMasters.Update(strainMaster);
                }
            });

            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorOr<List<Models.StrainMaster>>> UpsertStrainMasters(List<Models.StrainMaster> strainMasters)
        {
            strainMasters.ForEach(async strainMaster =>
            {
                var currentStrainMaster = dataContext.StrainMasters.FirstOrDefault(x => x.Identifier == strainMaster.Identifier);
                if (currentStrainMaster != null)
                {
                    currentStrainMaster.StrainCategoryId = strainMaster.StrainCategoryId;
                    currentStrainMaster.StrainName = strainMaster.StrainName;
                    currentStrainMaster.ExpiryAlertPeriod = strainMaster.ExpiryAlertPeriod;
                    currentStrainMaster.LinkedMedias = strainMaster.LinkedMedias;
                    currentStrainMaster.Status = strainMaster.Status;
                    currentStrainMaster.ModifiedBy = strainMaster.ModifiedBy;
                    currentStrainMaster.ModifiedByUserName = strainMaster.ModifiedByUserName;
                    currentStrainMaster.LastModifiedDateTime = strainMaster.LastModifiedDateTime;
                    currentStrainMaster.IsActive = strainMaster.IsActive;
                    dataContext.StrainMasters.Update(currentStrainMaster);
                }
                else
                {
                    await dataContext.StrainMasters.AddAsync(strainMaster);
                }
            });

            await dataContext.SaveChangesAsync();
            return strainMasters;
        }
    }
}
