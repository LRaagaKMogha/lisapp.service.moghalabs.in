using DEV.Model.Inventory;
using DEV.Model.Inventory.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Inventory.Report
{
    public interface IInventoryReportRepository
    {
        Task<InventoryReportOutput> GetInventoryReport(InventoryReportDTO ReportItem);
        string GetGridInventoryReport(InventoryReportDTO ReportItem);
    }
}
