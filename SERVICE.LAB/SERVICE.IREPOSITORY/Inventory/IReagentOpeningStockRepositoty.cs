using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IReagentOpeningStockRepositoty
    {
        List<ReagentOpeningStockResponse> GetAllReagentOpeningStock(GetReagentStockRequest request);
        CommonAdminResponse InsertReagentOpeningStock(InsertReagentOpeningStockRequest insertConsumption);
    }
}


