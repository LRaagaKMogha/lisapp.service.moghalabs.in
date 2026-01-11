using Dev.IRepository.UserManagement;
using DEV.Common;
using Dev.Repository.UserManagement;
using Service.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.UserManagement
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class VenueVsMenuController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IVenueVsMenuRepository _VenueVsMenuRepository;
        public VenueVsMenuController(IVenueVsMenuRepository VenueVsMenuRepository, IConfiguration config)
        {
            _VenueVsMenuRepository = VenueVsMenuRepository;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/VenueVsMenu/GetVenueVsMenu")]
        public List<VenueVsMenuResponseDTO> GetVenueVsMenu(VenueVsMenuRequestDTO request)
        {
            List<VenueVsMenuResponseDTO> objResult = new List<VenueVsMenuResponseDTO>();
            try
            {
                objResult = _VenueVsMenuRepository.GetVenueVsMenu(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VenueVsMenuController.GetVenueVsMenu",
                    ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objResult;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/VenueVsMenu/InsertVenueVsMenu")]
        public int InsertVenueVsMenu([FromBody] VenueVsMenuInsertDTO request)
        {
            int result = 0;
            try
            {
                result = _VenueVsMenuRepository.InsertVenueVsMenu(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VenueVsMenuController.InsertVenueVsMenu",
                    ExceptionPriority.High, ApplicationType.APPSERVICE, 0,0,0);
            }
            return result;
        }
    }
}
