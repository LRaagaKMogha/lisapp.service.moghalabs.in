using Dev.IRepository.Master;
using DEV.Common;
using DEV.Model.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.Master
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
            SaveScrollTextMasterResponse objresult = new SaveScrollTextMasterResponse();
            try
            {
                objresult = _ScrollTextMasterRepository.InsertScrollTextMaster(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ScrollTextMasterController.InsertScrollTextMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.VenueNo, request.VenueBranchNo, 0);
            }
            return objresult;
        }
    }
}