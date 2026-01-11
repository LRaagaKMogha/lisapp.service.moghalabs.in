using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IConsumptionMappingRepositoty
    {
        List<GetConsumptionMappingResponse> GetAllConsumptionMapping(GetAllConsumptionMappingRequest request);
        CommonAdminResponse InsertConsumptionMapping(InsertConsumptionMapping insertConsumption);
    }
}

