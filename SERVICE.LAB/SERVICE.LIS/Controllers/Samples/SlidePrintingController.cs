using Dev.IRepository;
using Service.Model.Sample;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Service.Model;
using Service.Model.FrontOffice.PatientDue;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using DEV.Common;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Dev.IRepository.Samples;

namespace DEV.API.SERVICE.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class SlidePrintingController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ISlidePrintingRepository _SlidePrintingRepository;
        public SlidePrintingController(ISlidePrintingRepository SlidePrintingRepository, IConfiguration config)
        {
            _SlidePrintingRepository = SlidePrintingRepository;
            _config = config;
        }

        //[CustomAuthorize("LIMSSAMPLEMNTC")]
        [HttpPost]
        [Route("api/SlidePrinting/GetSlidePrintingDetails")]
        public ActionResult GetSlidePrintingDetails(SlidePrintingRequest RequestItem)
        {
            List<GetSlidePrintingResponse> objresult = new List<GetSlidePrintingResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetSlidePrintingDetails(RequestItem);
                if (!_errormsg.status)
                {
                    objresult = _SlidePrintingRepository.GetSlidePrintingDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/SlidePrinting/GetSlidePrintingPatientDetails")]
        public SlidePrintPatientDetailsResponse GetSlidePrintingPatientDetails(CommonFilterRequestDTO RequestItem)
        {
            SlidePrintPatientDetailsResponse objresult = new SlidePrintPatientDetailsResponse();
            try
            {
                objresult = _SlidePrintingRepository.GetSlidePrintingPatientDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingPatientDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/SlidePrinting/SaveSlidePrintingDetails")]
        public ActionResult<CommonTokenResponse> SaveSlidePrintingDetails(SlidePrintPatientDetailsResponse slidePrintPatientDetails)
        {
            CommonTokenResponse objresult = new CommonTokenResponse();
            try
            {
                var _errormsg = SampleMaintainenceValidation.SaveSlidePrintingDetails(slidePrintPatientDetails);
                if (!_errormsg.status)
                {
                    objresult = _SlidePrintingRepository.SaveSlidePrintingDetails(slidePrintPatientDetails);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, slidePrintPatientDetails.VenueNo, slidePrintPatientDetails.VenueBranchNo, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/SlidePrinting/GenerateSlideNumber")]
        public CommonTokenResponse GenerateSlideNUmber(CommonFilterRequestDTO RequestItem)
        {
            CommonTokenResponse objresult = new CommonTokenResponse();
            try
            {
                objresult = _SlidePrintingRepository.GenerateSlideNumber(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/SlidePrinting/GetExistingRCHNoDetails")]
        public List<ExistingRCHNoResponse> GetExistingRCHNoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<ExistingRCHNoResponse> objresult = new List<ExistingRCHNoResponse>();
            try
            {
                objresult = _SlidePrintingRepository.GetExistingRCHNoDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/SlidePrinting/GetBulkSlidePrintDetails")]
        public List<GetBulkSlidePrintingDetails> GetBulkSlidePrintDetails(GetBulkSlidePrintingRequest RequestItem)
        {
            List<GetBulkSlidePrintingDetails> objresult = new List<GetBulkSlidePrintingDetails>();
            try
            {
                objresult = _SlidePrintingRepository.GetBulkSlidePrintDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

    }
}

