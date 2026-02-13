using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IStockReportReposistory
    {
        List<GetStockReportResponse> GetStockReport(GetStockReportRequest stockReport);
    }
}
