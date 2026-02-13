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
    public class OPDDashBoardController : ControllerBase
    {

        private readonly IOPDDashBoardRepository _OPDDashBoardRepository;
        public OPDDashBoardController(IOPDDashBoardRepository OPDDashBoardRepository)
        {
            _OPDDashBoardRepository = OPDDashBoardRepository;
        }

        [HttpPost]
        [Route("api/OPDDashBoard/GetOPDDashBoard")]
        public List<OPDDashBoardRes> GetOPDDashBoard(OPDDashBoardReq RequestItem)
        {
            List<OPDDashBoardRes> response = new List<OPDDashBoardRes>();
            try
            {
                response = _OPDDashBoardRepository.GetOPDDashBoard(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDDashBoard.GetOPDDashBoard-ClientNo" + RequestItem.UserNo, ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }
    }
}