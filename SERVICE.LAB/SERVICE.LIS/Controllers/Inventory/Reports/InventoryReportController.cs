using System;
using System.Collections.Generic;
using Dev.IRepository.Inventory.Report;
using DEV.Common;
using Service.Model.Inventory;
using Service.Model.Inventory.Report;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers.Inventory.Reports
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class InventoryReportController : ControllerBase
    {
        private readonly IInventoryReportRepository _IInventoryReportRepository;
        public InventoryReportController(IInventoryReportRepository inventoryRepository)
        {
            _IInventoryReportRepository = inventoryRepository;
        }

        #region GetGridInventoryReport
        [CustomAuthorize("INVREPORTS")]
        [HttpPost]
        [Route("api/Report/Inventory/GetGridInventoryReport")]
        public string GetGridInventoryReport(InventoryReportDTO req)
        {
            string result = String.Empty;
            try
            {
                result = _IInventoryReportRepository.GetGridInventoryReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InventoryReportController.GetGridInventoryReport/ReportKey - " + req.ReportKey, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userID);
            }
            return result;
        }
        #endregion

        #region GetReport
        [CustomAuthorize("INVREPORTS")]
        [HttpPost]
        [Route("api/Report/Inventory/GetInventoryReport")]
        public async Task<InventoryReportOutput> GetInventoryReport(InventoryReportDTO req)
        {
            InventoryReportOutput result = new InventoryReportOutput();
            try
            {
                result = await _IInventoryReportRepository.GetInventoryReport(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InventoryReportController.GetInventoryReport/ReportKey - " + req.ReportKey, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, req.userID);
            }
            return result;
        }
        #endregion
    }
}
