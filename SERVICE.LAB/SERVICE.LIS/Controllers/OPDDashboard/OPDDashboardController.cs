using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
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