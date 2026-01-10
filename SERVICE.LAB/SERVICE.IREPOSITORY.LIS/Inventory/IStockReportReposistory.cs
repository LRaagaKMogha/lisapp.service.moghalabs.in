using DEV.Model.Inventory;
using System.Collections.Generic;

namespace Dev.IRepository.Inventory
{
    public interface IStockReportReposistory
    {
        List<GetStockReportResponse> GetStockReport(GetStockReportRequest stockReport);
    }
}
