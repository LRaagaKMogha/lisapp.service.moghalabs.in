using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.FrontOffice.PatientDue;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PatientDueController : ControllerBase
    {
        private readonly IPatientDueRepository _dueCancelRepository;    
        public PatientDueController(IPatientDueRepository dueCancelRepository)
        {
            _dueCancelRepository = dueCancelRepository;
        }
        #region Due Clear
        [HttpPost]       
        [Route("api/PatientDue/GetDuePatientInfoDetails")]
        public List<PatientDueResponse> GetDuePatientInfoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientDueResponse> objresult = new List<PatientDueResponse>();
            try
            {
                objresult = _dueCancelRepository.GetDuePatientInfoDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.GetDuePatientInfoDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/PatientDue/InsertPatientDue")]
        public CreatePatientDueResponse InsertPatientDue(CreatePatientDueRequest createPatientDueRequest)
        {
            CreatePatientDueResponse objresult = new CreatePatientDueResponse();
            try
            {
                objresult = _dueCancelRepository.InsertPatientDue(createPatientDueRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.InsertPatientDue", ExceptionPriority.Low, ApplicationType.APPSERVICE, createPatientDueRequest.venueNo, createPatientDueRequest.venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion

        #region Cancel Test
        [HttpPost]
        [Route("api/PatientDue/GetPatientCancelTestInfo")]
        public CancelVisit GetPatientCancelTestInfo(getrequest Req)
        {
            CancelVisit obj = new CancelVisit();
            try
            {
                obj = _dueCancelRepository.GetPatientCancelTestInfo(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetPatientCancelTestInfo" , ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueno, Req.venuebranchno, 0);
            }
            return obj;
        }

        [HttpPost]
        [Route("api/PatientDue/InsertCancelTest")]
        public ActionResult<rtnCancelTest> InsertCancelTest(CancelVisit Req)
        {
            rtnCancelTest obj = new rtnCancelTest();
            try
            {
                var _errormsg = MassRegistrationValidation.InsertCancelTest(Req);
                if (!_errormsg.status)
                {
                    obj = _dueCancelRepository.InsertCancelTest(Req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.InsertCancelTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchBo, 0);
            }
            return Ok(obj);
        }


        [HttpPost]
        [Route("api/PatientDue/Insertbulkpatientdue")]
        public CreatePatientDueResponse Insertbulkpatientdue(List<CreatePatientDueRequest> createPatientDueRequest)
        {
            CreatePatientDueResponse objresult = new CreatePatientDueResponse();
            try
            {
                objresult = _dueCancelRepository.Insertbulkpatientdue(createPatientDueRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.Insertbulkpatientdue", ExceptionPriority.Low, ApplicationType.APPSERVICE, createPatientDueRequest[0].venueNo, createPatientDueRequest[0].venueBranchNo, createPatientDueRequest[0].userID);
            }
            return objresult;
        }

        #endregion

        #region Refund/Cancel Approval 
        [HttpPost]
        [Route("api/PatientDue/GetRefundCancelRequest")]
        public List<GetReqCancelResponse> GetRefundCancelRequest(GetReqCancelParam RequestItem)
        {
            List<GetReqCancelResponse> objresult = new List<GetReqCancelResponse>();
            try
            {
                objresult = _dueCancelRepository.GetRefundCancelRequest(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.GetRefundCancelRequest", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebranchno, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/PatientDue/ApproveRefundCancel")]
        public UpdateReqCancelResponse ApproveRefundCancel(UpdateReqCancelParam RequestItem)
        {
            UpdateReqCancelResponse obj = new UpdateReqCancelResponse();
            try
            {
                obj = _dueCancelRepository.ApproveRefundCancel(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueController.ApproveRefundCancel", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.venueno, RequestItem.venuebranchno, 0);
            }
            return obj;
        }
        #endregion
    }
}