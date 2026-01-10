using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;
using QCManagement.Models;

namespace QCManagement.Services.Scheduler
{
    public interface ISchedulerService
    {
        Task<ErrorOr<List<Models.Scheduler>>> CreateScheduler(List<Models.Scheduler> Scheduler);
        Task<ErrorOr<Models.Scheduler>> UpdateScheduler(Models.Scheduler Scheduler);
        Task<ErrorOr<DeleteSchedulerRequest>> DeleteSchedulers(DeleteSchedulerRequest request);
        Task<ErrorOr<Models.Scheduler>> GetScheduler(Int64 id);
        Task<ErrorOr<List<Models.Scheduler>>> GetSchedulers(Contracts.SchedulerFilterRequest request);

        

    }
}