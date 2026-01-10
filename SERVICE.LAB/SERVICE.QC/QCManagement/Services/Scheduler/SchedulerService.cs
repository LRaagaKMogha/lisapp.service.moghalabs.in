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
using QCManagement.Services.Scheduler;

namespace QCManagement.Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly QCDataContext dataContext;

        public SchedulerService(QCDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR QCBatchId", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

        public async Task<ErrorOr<List<Models.Scheduler>>> CreateScheduler(List<Models.Scheduler> Schedulers)
        {
            if (Schedulers != null)
            {
                try
                {
                    var batchId = 0;
                    if (Schedulers.Any(x => x.Identifier == 0))
                        batchId = GetNextSequenceNumber();
                    Schedulers.ForEach(row => row.BatchId = batchId);
                    await dataContext.Schedulers.AddRangeAsync(Schedulers);
                    await this.dataContext.SaveChangesAsync();
                    return Schedulers;
                }
                catch (Exception expection)
                {

                }

            }
            return Errors.Scheduler.NotFound;
        }

        public async Task<ErrorOr<Models.Scheduler>> GetScheduler(Int64 id)
        {
            try
            {
                var data = await dataContext.Schedulers.FirstOrDefaultAsync(x => x.Identifier == id);
                if (data != null) return data;

            }
            catch (Exception exp)
            {

            }
            return Errors.Scheduler.NotFound;
        }

        public async Task<ErrorOr<List<Models.Scheduler>>> GetSchedulers(SchedulerFilterRequest request)
        {
            Expression<Func<Models.Scheduler, bool>> dateRange = x => x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate;
            Expression<Func<Models.Scheduler, bool>> analyzerID = x => x.AnalyzerId == request.AnalyzerID;
            Expression<Func<Models.Scheduler, bool>> testID = x => x.TestId == request.TestID;

            var query = dataContext.Schedulers.AsQueryable();
            if (request.StartDate != null && request.EndDate != null) query = query.Where(dateRange);
            if (request.AnalyzerID != 0) query = query.Where(analyzerID);
            if (request.TestID != 0) query = query.Where(testID);

            var data = await query.ToListAsync();
            if (data != null) return data;
            return Errors.Scheduler.NotFound;
        }

        public async Task<ErrorOr<Models.Scheduler>> UpdateScheduler(Models.Scheduler Scheduler)
        {
            if (Scheduler != null)
            {
                dataContext.Schedulers.Update(Scheduler);
                await this.dataContext.SaveChangesAsync();
                return Scheduler;
            }
            return Errors.Scheduler.NotFound;
        }

        public async Task<ErrorOr<DeleteSchedulerRequest>> DeleteSchedulers(DeleteSchedulerRequest request)
        {
            var schedulerDetail = dataContext.Schedulers.Find(request.Identifier);
            if (schedulerDetail != null)
            {
                if (request.DeleteBatch)
                {
                    var itemsToRemove = await dataContext.Schedulers.Where(x => x.BatchId == schedulerDetail.BatchId).ToListAsync();
                    dataContext.Schedulers.RemoveRange(itemsToRemove);
                }
                else
                {
                    dataContext.Schedulers.Remove(schedulerDetail);
                }
                await dataContext.SaveChangesAsync();
            }
            return request;
        }
    }
}