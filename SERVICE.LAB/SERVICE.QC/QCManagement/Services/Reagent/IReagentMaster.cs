using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using QCManagement.Contracts;

namespace QCManagement.Services.Reagent
{
    public interface IReagentService
    {
        Task<ErrorOr<List<Models.Reagent>>> CreateReagent(List<Models.Reagent> Reagent);
        Task<ErrorOr<Models.Reagent>> UpdateReagent(Models.Reagent Reagent);
        Task<ErrorOr<Models.Reagent>> GetReagent(Int64 id);
        Task<ErrorOr<List<Models.Reagent>>> GetReagents(ReagentFilterRequest request);
      
    }
}