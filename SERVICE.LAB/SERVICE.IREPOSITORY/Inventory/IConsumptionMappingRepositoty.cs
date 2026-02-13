using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IConsumptionMappingRepositoty
    {
        List<GetConsumptionMappingResponse> GetAllConsumptionMapping(GetAllConsumptionMappingRequest request);
        CommonAdminResponse InsertConsumptionMapping(InsertConsumptionMapping insertConsumption);
    }
}

