using Service.IRepository.Master;
using Service.Common;
using Service.Model.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ScrollTextMasterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IScrollTextMasterRepository _ScrollTextMasterRepository;
        public ScrollTextMasterController(IScrollTextMasterRepository ScrollTextMasterRepository, IConfiguration config)
        {
            _ScrollTextMasterRepository = ScrollTextMasterRepository;
            _config = config;
        }
        [HttpPost]
        [Route("api/ScrollTextMaster/GetScrollTextMasterDetails")]
        public List<ScrollTextMasterResponse> GetScrollTextMaster(GetScrollTextMasterRequest scrollMaster)
        {
            List<ScrollTextMasterResponse> ScrollTextMasterresult = new List<ScrollTextMasterResponse>();
            try
            {
                ScrollTextMasterresult = _ScrollTextMasterRepository.GetScrollTextMaster(scrollMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ScrollTextMasterController.GetScrollTextMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, scrollMaster.venueNo, 0, 0);
            }
            return ScrollTextMasterresult;
        }

        [HttpPost]
        [Route("api/ScrollTextMaster/InsertScrollTextMaster")]
        public SaveScrollTextMasterResponse InsertScrollTextMaster(SaveScrollTextMasterRequest request)
        {
            SaveScrollTextMasterResponse Objresult = new SaveScrollTextMasterResponse();
            try
            {
                Objresult = _ScrollTextMasterRepository.InsertScrollTextMaster(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ScrollTextMasterController.InsertScrollTextMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.VenueNo, request.VenueBranchNo, 0);
            }
            return Objresult;
        }
    }
}