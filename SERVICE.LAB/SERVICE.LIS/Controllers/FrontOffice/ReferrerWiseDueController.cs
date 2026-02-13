using Service.IRepository.FrontOffice;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ReferrerWiseDueController : ControllerBase
    {
        private readonly IReferrerWiseDueRepository _IReferrerWiseDueRepository;

        public ReferrerWiseDueController(IReferrerWiseDueRepository referrerWiseDueRepository)
        {
            _IReferrerWiseDueRepository = referrerWiseDueRepository;
        }

        [CustomAuthorize("LIMSFRONTOFFICE,LIMSDEFAULT")]
        [HttpPost]
        [Route("api/ReferrerWiseDue/GetRefWiseDueDetails")]

        public ActionResult<RefWiseDueResponse> GetRefWiseDueDetails(RefWiseDueRequest request)
        {
            RefWiseDueResponse result = new RefWiseDueResponse();
            try
            {
                result = _IReferrerWiseDueRepository.GetRefWiseDueResponses(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReferrerWiseDueController.GetRefWiseDueDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, request.VenueNo, request.BranchNo, request.UserNo);
            }

            return Ok(result);
        }
    }

}
