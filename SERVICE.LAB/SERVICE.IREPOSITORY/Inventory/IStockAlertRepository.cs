using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IStockAlertRepository
    {
        List<GetStockAlertResponse> GetStockAlertsDetails(StockAlertRequest stockAlertRequest);
    }
}
