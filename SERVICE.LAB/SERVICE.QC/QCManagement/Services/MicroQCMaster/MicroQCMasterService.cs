using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.ServiceErrors;
using QC.Helpers;
using QCManagement.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QCManagement.Services.MicroQCMaster
{
    public class MicroQCMasterService : IMicroQCMasterService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration configuration;

        public MicroQCMasterService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<ErrorOr<List<Models.MicroQCMaster>>> CreateMicroQCMaster(List<Models.MicroQCMaster> microQCMasters)
        {
            if (microQCMasters != null)
            {
                await dataContext.MicroQCMasters.AddRangeAsync(microQCMasters);
                await dataContext.SaveChangesAsync();
                return microQCMasters;
            }
            return Errors.MicroQCMaster.NotFound;
        }

        public async Task<ErrorOr<Models.MicroQCMaster>> GetMicroQCMaster(Int64 id)
        {
            var data = await dataContext.MicroQCMasters.FindAsync(id);
            if (data != null) return data;
            return Errors.MicroQCMaster.NotFound;
        }

        public async Task<ErrorOr<List<Models.MicroQCMaster>>> GetMicroQCMasters(MicroQCMasterFilterRequest request)
        {
            try
            {
                Expression<Func<Models.MicroQCMaster, bool>> reagentId = e => e.CultureReagentTestId == request.reagentId;
                Expression<Func<Models.MicroQCMaster, bool>> activeStatus = e => e.IsActive == true;
                Expression<Func<Models.MicroQCMaster, bool>> inactiveStatus = e => e.IsActive == false;

                var query = dataContext.MicroQCMasters.AsQueryable();
                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.MicroQCMasters.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive && string.IsNullOrEmpty(request.ActiveStatus))
                    query = dataContext.MicroQCMasters.Where(x => x.IsActive == true);
                if (request.reagentId > 0) query = query.Where(reagentId);
                if (!string.IsNullOrEmpty(request.ActiveStatus))
                {
                    if (request.ActiveStatus == "Active") query = query.Where(activeStatus);
                    if (request.ActiveStatus == "InActive") query = query.Where(inactiveStatus);
                }

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.MicroQCMaster.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.MicroQCMaster.NotFound;
        }

        public async Task<ErrorOr<bool>> UpdateMicroQCMasterStatus(List<Int64> ids, string status, Int64 modifiedBy, string modifiedByUserName, DateTime lastModifiedDateTime, string comments = "", bool updateComments = false)
        {
            ids.ForEach(x =>
            {
                var microQCMaster = dataContext.MicroQCMasters.Find(x);
                if (microQCMaster != null && microQCMaster.Status != status)
                {
                    microQCMaster.Status = status;
                    microQCMaster.ModifiedBy = modifiedBy;
                    microQCMaster.ModifiedByUserName = modifiedByUserName;
                    microQCMaster.LastModifiedDateTime = lastModifiedDateTime;
                    dataContext.MicroQCMasters.Update(microQCMaster);
                }
            });

            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<ErrorOr<List<Models.MicroQCMaster>>> UpsertMicroQCMasters(List<Models.MicroQCMaster> microQCMasters)
        {
            microQCMasters.ForEach(async microQCMaster =>
            {
                var currentMicroQCMaster = dataContext.MicroQCMasters.FirstOrDefault(x => x.Identifier == microQCMaster.Identifier);
                if (currentMicroQCMaster != null)
                {
                    currentMicroQCMaster.CultureReagentTestId = microQCMaster.CultureReagentTestId;
                    currentMicroQCMaster.PositiveStrainIds = microQCMaster.PositiveStrainIds;
                    currentMicroQCMaster.NegativeStrainIds = microQCMaster.NegativeStrainIds;
                    currentMicroQCMaster.Frequency = microQCMaster.Frequency;
                    currentMicroQCMaster.Status = microQCMaster.Status;
                    currentMicroQCMaster.ModifiedBy = microQCMaster.ModifiedBy;
                    currentMicroQCMaster.ModifiedByUserName = microQCMaster.ModifiedByUserName;
                    currentMicroQCMaster.LastModifiedDateTime = microQCMaster.LastModifiedDateTime;
                    currentMicroQCMaster.IsActive = microQCMaster.IsActive;
                    dataContext.MicroQCMasters.Update(currentMicroQCMaster);
                }
                else
                {
                    await dataContext.MicroQCMasters.AddAsync(microQCMaster);
                }
            });

            await this.dataContext.SaveChangesAsync();
            return microQCMasters;
        }
    }
}
