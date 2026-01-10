using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ReagentOpeningStockController : ControllerBase
    {
        private readonly IReagentOpeningStockRepositoty _ReagentOpeningStockRepositoty;
        public ReagentOpeningStockController(IReagentOpeningStockRepositoty ReagentOpeningStockRepositoty)
        {
            _ReagentOpeningStockRepositoty = ReagentOpeningStockRepositoty;

        }
        [HttpPost]
        [Route("api/ReagentOpeningStock/GetAllReagentOpeningStock")]
        public List<ReagentOpeningStockResponse> GetAllReagentOpeningStock(GetReagentStockRequest request)
        {
            List<ReagentOpeningStockResponse> result = new List<ReagentOpeningStockResponse>();
            try
            {
                result = _ReagentOpeningStockRepositoty.GetAllReagentOpeningStock(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReagentOpeningStockController.GetAllReagentOpeningStock", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.venueno, 0, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/ReagentOpeningStock/InsertReagentOpeningStock")]
        public CommonAdminResponse InsertReagentOpeningStock(InsertReagentOpeningStockRequest insertReagentOpeningStock)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _ReagentOpeningStockRepositoty.InsertReagentOpeningStock(insertReagentOpeningStock);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReagentOpeningStockController.InsertReagentOpeningStock", ExceptionPriority.Low, ApplicationType.APPSERVICE, insertReagentOpeningStock.VenueNo, insertReagentOpeningStock.VenueBranchNo, insertReagentOpeningStock.Createdby);
            }
            return result;
        }
    }
}
