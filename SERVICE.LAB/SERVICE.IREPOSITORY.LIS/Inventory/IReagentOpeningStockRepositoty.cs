using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IReagentOpeningStockRepositoty
    {
        List<ReagentOpeningStockResponse> GetAllReagentOpeningStock(GetReagentStockRequest request);
        CommonAdminResponse InsertReagentOpeningStock(InsertReagentOpeningStockRequest insertConsumption);
    }
}


