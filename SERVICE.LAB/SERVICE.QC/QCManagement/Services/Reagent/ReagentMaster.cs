using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using QC.Helpers;
using QCManagement.Contracts;
using QCManagement.ServiceErrors;

namespace QCManagement.Services.Reagent
{
    public class ReagentService : IReagentService
    {
        private readonly QCDataContext dataContext;
        private readonly IConfiguration Configuration;
        public ReagentService(QCDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        public async Task<ErrorOr<List<Models.Reagent>>> CreateReagent(List<Models.Reagent> Reagent)
        {
            if (Reagent != null)
            {
                await dataContext.Reagents.AddRangeAsync(Reagent);
                await dataContext.SaveChangesAsync();
                return Reagent;
            }
            return Errors.Reagent.NotFound;
        }

        public async Task<ErrorOr<Models.Reagent>> GetReagent(Int64 id)
        {
            var data = await dataContext.Reagents.FindAsync(id);
            if (data != null) return data;
            return Errors.Reagent.NotFound;
        }

        public async Task<ErrorOr<List<Models.Reagent>>> GetReagents(ReagentFilterRequest request)
        {
            try
            {
                Expression<Func<Models.Reagent, bool>> status = e => e.Status == request.Status;
                Expression<Func<Models.Reagent, bool>> department = e => e.DepartmentId == request.DepartmentId;
                Expression<Func<Models.Reagent, bool>> ReagentName = e => e.Name.Contains(request.ReagentName); 

                
                var query = dataContext.Reagents.AsQueryable();
                if (request.StartDate != null && request.EndDate != null)
                    query = dataContext.Reagents.Where(x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
                if (request.showAllActive)
                    query = dataContext.Reagents.Where(x => x.ExpirationDate >= DateTime.Now);
                query = query.Where(status);
                if (request.DepartmentId != null && request.DepartmentId > 0)
                    query = query.Where(department);
                if (!string.IsNullOrEmpty(request.ReagentName))
                    query = query.Where(ReagentName);

                var data = await query.ToListAsync();
                if (data != null) return data;
                return Errors.Reagent.NotFound;
            }
            catch (Exception exp)
            {
                var exp1 = exp;
            }
            return Errors.Reagent.NotFound;
        }

        public async Task<ErrorOr<Models.Reagent>> UpdateReagent(Models.Reagent Reagent)
        {
            var reagentData = dataContext.Reagents.Find(Reagent.Identifier);
            if (Reagent != null && reagentData != null)
            {
                reagentData.ManufacturerID = Reagent.ManufacturerID;
                reagentData.DistributorID = Reagent.DistributorID;
                reagentData.DepartmentId = Reagent.DepartmentId;
                reagentData.Name = Reagent.Name;
                reagentData.LotNo = Reagent.LotNo;
                reagentData.SequenceNo = Reagent.SequenceNo;
                reagentData.ExpirationDate = Reagent.ExpirationDate;
                reagentData.LotSetupOrInstallationDate = Reagent.LotSetupOrInstallationDate;
                reagentData.ModifiedBy = Reagent.ModifiedBy;
                reagentData.ModifiedByUserName = Reagent.ModifiedByUserName;
                reagentData.LastModifiedDateTime = Reagent.LastModifiedDateTime;
                reagentData.Status = Reagent.Status;
                await dataContext.SaveChangesAsync();
                return Reagent;
            }
            return Errors.Reagent.NotFound;
        }


    }
}