using Service.IRepository.Inventory;
using Service.Common;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StockReportController :ControllerBase
    {
        private readonly IStockReportReposistory _StockReport;
        public StockReportController(IStockReportReposistory noteRepository)
        {
            _StockReport = noteRepository;
        }
        [HttpPost]
        [Route("api/stockreport/GetStockReport")]
        public List<GetStockReportResponse> GetStockReport(GetStockReportRequest stockreport)
        {
            List<GetStockReportResponse> objresult = new List<GetStockReportResponse>();
            try
            {
                objresult = _StockReport.GetStockReport(stockreport);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockReport.GetStockReportResponse/VenuNo-" + stockreport.venueNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE,stockreport.venueNo, stockreport.venueBranchNo, stockreport.userNo);

            }
            return objresult;
        }
    }
}
