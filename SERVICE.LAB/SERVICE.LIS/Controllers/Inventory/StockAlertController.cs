using Service.IRepository.Inventory;
using Service.Common;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StockAlertController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IStockAlertRepository _stockAlertRepository;
        public StockAlertController(IStockAlertRepository StockAlertRepository, IConfiguration config)
        {
            _stockAlertRepository = StockAlertRepository;
            _config = config;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Stock/GetStockAlertsDetails")]
        public List<GetStockAlertResponse> GetStockAlertsDetails(StockAlertRequest stockAlertRequest)
        {
            List<GetStockAlertResponse> Objresult = new List<GetStockAlertResponse>();
            try
            {
                Objresult = _stockAlertRepository.GetStockAlertsDetails(stockAlertRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetStockAlertsDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, stockAlertRequest.VenueNo, 0, 0);
            }
            return Objresult;
        }

    }
}

