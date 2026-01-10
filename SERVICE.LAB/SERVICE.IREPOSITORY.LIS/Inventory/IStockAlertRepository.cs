using DEV.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Inventory
{
    public interface IStockAlertRepository
    {
        List<GetStockAlertResponse> GetStockAlertsDetails(StockAlertRequest stockAlertRequest);
    }
}
