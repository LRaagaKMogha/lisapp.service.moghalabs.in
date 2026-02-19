using Service.IRepository;
using Service.Model.Sample;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Service.Model;
using Service.Common;
using Microsoft.Extensions.Configuration;

namespace Service.API.SERVICE.Controllers
{
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
            List<GetSlidePrintingResponse> Objresult = new List<GetSlidePrintingResponse>();
            try
            {
                var _errormsg = SampleMaintainenceValidation.GetSlidePrintingDetails(RequestItem);
                if (!_errormsg.status)
                {
                    Objresult = _SlidePrintingRepository.GetSlidePrintingDetails(RequestItem);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/SlidePrinting/GetSlidePrintingPatientdetails")]
        public SlidePrintPatientdetailsResponse GetSlidePrintingPatientdetails(CommonFilterRequestDTO RequestItem)
        {
            SlidePrintPatientdetailsResponse Objresult = new SlidePrintPatientdetailsResponse();
            try
            {
                Objresult = _SlidePrintingRepository.GetSlidePrintingPatientdetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingPatientdetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/SlidePrinting/SaveSlidePrintingDetails")]
        public ActionResult<CommonTokenResponse> SaveSlidePrintingDetails(SlidePrintPatientdetailsResponse slidePrintPatientdetails)
        {
            CommonTokenResponse Objresult = new CommonTokenResponse();
            try
            {
                var _errormsg = SampleMaintainenceValidation.SaveSlidePrintingDetails(slidePrintPatientdetails);
                if (!_errormsg.status)
                {
                    Objresult = _SlidePrintingRepository.SaveSlidePrintingDetails(slidePrintPatientdetails);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SaveSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, slidePrintPatientdetails.VenueNo, slidePrintPatientdetails.VenueBranchNo, 0);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/SlidePrinting/GenerateSlideNumber")]
        public CommonTokenResponse GenerateSlideNUmber(CommonFilterRequestDTO RequestItem)
        {
            CommonTokenResponse Objresult = new CommonTokenResponse();
            try
            {
                Objresult = _SlidePrintingRepository.GenerateSlideNumber(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/SlidePrinting/GetExistingRCHNoDetails")]
        public List<ExistingRCHNoResponse> GetExistingRCHNoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<ExistingRCHNoResponse> Objresult = new List<ExistingRCHNoResponse>();
            try
            {
                Objresult = _SlidePrintingRepository.GetExistingRCHNoDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/SlidePrinting/GetBulkSlidePrintDetails")]
        public List<GetBulkSlidePrintingDetails> GetBulkSlidePrintDetails(GetBulkSlidePrintingRequest RequestItem)
        {
            List<GetBulkSlidePrintingDetails> Objresult = new List<GetBulkSlidePrintingDetails>();
            try
            {
                Objresult = _SlidePrintingRepository.GetBulkSlidePrintDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetSlidePrintingDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return Objresult;
        }

    }
}

