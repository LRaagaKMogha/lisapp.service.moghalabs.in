using System;
using System.Collections.Generic;
using Dev.IRepository.Samples;
using DEV.Common;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Samples
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  public class WorkListController : ControllerBase
  {
    private readonly IWorkListRepository _workListRepository;

    public WorkListController(IWorkListRepository workListRepository)
    {
      _workListRepository = workListRepository;
    }

    [HttpPost]
    [Route("api/WorkList/GetWorkListDetails")]
    public ActionResult<WorkListResponse> GetWorkListDetails(WorkListRequest RequestItem)
    {
      List<WorkListResponse> objresult = new List<WorkListResponse>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetWorkListDetails(RequestItem);
        if (!_errormsg.status)
        {
          objresult = _workListRepository.GetWorkList(RequestItem);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
      }
      return Ok(objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetHistoWorkListDetails")]
    public ActionResult<HistoWorlkListRes> GetHistoWorkListDetails(WorkListRequest RequestItem)
    {
      List<HistoWorlkListRes> objresult = new List<HistoWorlkListRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetHistoWorkListDetails(RequestItem);
        if (!_errormsg.status)
        {
          objresult = _workListRepository.GetHistoWorkList(RequestItem);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
      }
      return Ok(objresult);
    }

    [HttpPost]
    [Route("api/WorkList/InsertWorkListHistory")]
    public ActionResult<WorkListHistoryRes> InsertWorkListHistory(WorkListHistoryReq Req)
    {
      List<WorkListHistoryRes> objresult = new List<WorkListHistoryRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.InsertWorkListHistory(Req);
        if (!_errormsg.status)
        {
          objresult = _workListRepository.InsertWorkListHistory(Req);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.InsertWorkListHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, 0);
      }
      return Ok(objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetWorkListHistory")]
    public ActionResult<GetWorkListHistoryRes> GetWorkListHistory(GetWorkListHistoryReq Req)
    {
      List<GetWorkListHistoryRes> objresult = new List<GetWorkListHistoryRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetWorkListHistory(Req);
        if (!_errormsg.status)
        {
          objresult = _workListRepository.GetWorkListHistory(Req);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, 0);
      }
      return Ok(objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetUserDeptDetails")]
    public List<UserDeptmentDetails> GetUserDeptDetails(getUserNo Req)
    {
      List<UserDeptmentDetails> objresult = new List<UserDeptmentDetails>();
      try
      {
        objresult = _workListRepository.GetUserDeptDetails(Req);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetUserDeptDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);
      }
      return objresult;
    }
    [HttpPost]
    [Route("api/WorkList/GetSubTestCheck")]
    public SingleTestCheckRes GetTestCheck(SingleTestCheck Req)
    {
      SingleTestCheckRes objresult = new SingleTestCheckRes();
      try
      {
        objresult = _workListRepository.getTestCheck(Req);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetSubTestCheck", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);

      }
      return objresult;
    }
    [HttpPost]
    [Route("api/WorkList/GetDenguTest")]
    public List<DenguTestRes> GetDenguTest(DenguTestReq Req)
    {
        List<DenguTestRes> objresult = new List<DenguTestRes>();
        try
        {
            objresult = _workListRepository.getDenguTest(Req);
        }
        catch (Exception ex)
        {
            MyDevException.Error(ex, "WorkListController.GetDenguTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venuno, Req.venueBranchNo, 0);

        }
        return objresult;
    }
  }
}