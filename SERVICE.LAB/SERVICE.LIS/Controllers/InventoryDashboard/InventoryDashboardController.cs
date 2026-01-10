using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class InventoryDashBoardController : ControllerBase
    {

        private readonly IInventoryDashBoardRepository _inventoryDashBoardRepository;
        public InventoryDashBoardController(IInventoryDashBoardRepository inventoryDashBoardRepository)
        {
            _inventoryDashBoardRepository = inventoryDashBoardRepository;
        }


        [HttpPost]
        [Route("api/InventoryDashBoard/GetInventoryDashBoard")]
        public List<InventoryDashBoardRes> GetInventoryDashBoard(InventoryDashBoardReq RequestItem)
        {
            List<InventoryDashBoardRes> response = new List<InventoryDashBoardRes>();
            try
            {
                response = _inventoryDashBoardRepository.GetInventoryDashBoard(RequestItem);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InventoryDashBoard.GetInventoryDashBoard-ClientNo" + RequestItem.UserNo, ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }
    }
}