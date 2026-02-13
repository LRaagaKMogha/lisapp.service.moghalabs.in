using Service.Model.Inventory;
using Service.Model.Inventory.Report;
using System.Threading.Tasks;

namespace Service.IRepository.Inventory.Report
{
    public interface IInventoryReportRepository
    {
        Task<InventoryReportOutput> GetInventoryReport(InventoryReportDTO ReportItem);
        string GetGridInventoryReport(InventoryReportDTO ReportItem);
    }
}
