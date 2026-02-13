using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        private readonly IDashBoardRepository _dashBoardRepository;
        public DashBoardController(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }


        [HttpPost]
        [Route("api/DashBoard/GetDashBoards")]
        public List<DashBoardResponse> GetDashBoards(DashBoardRequest RequestItem)
        {
            List<DashBoardResponse> response = new List<DashBoardResponse>();
            try
            {
                response = _dashBoardRepository.GetDashBoards(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DashBoard.GetDashBoards-ClientNo" + RequestItem.UserNo, ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }
    }
}