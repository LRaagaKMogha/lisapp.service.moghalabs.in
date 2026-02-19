using System;
using System.Collections.Generic;
using Service.IRepository.Samples;
using Service.Common;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers.Samples
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
      List<WorkListResponse> Objresult = new List<WorkListResponse>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetWorkListDetails(RequestItem);
        if (!_errormsg.status)
        {
          Objresult = _workListRepository.GetWorkList(RequestItem);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
      }
      return Ok(Objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetHistoWorkListDetails")]
    public ActionResult<HistoWorlkListRes> GetHistoWorkListDetails(WorkListRequest RequestItem)
    {
      List<HistoWorlkListRes> Objresult = new List<HistoWorlkListRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetHistoWorkListDetails(RequestItem);
        if (!_errormsg.status)
        {
          Objresult = _workListRepository.GetHistoWorkList(RequestItem);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
      }
      return Ok(Objresult);
    }

    [HttpPost]
    [Route("api/WorkList/InsertWorkListHistory")]
    public ActionResult<WorkListHistoryRes> InsertWorkListHistory(WorkListHistoryReq Req)
    {
      List<WorkListHistoryRes> Objresult = new List<WorkListHistoryRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.InsertWorkListHistory(Req);
        if (!_errormsg.status)
        {
          Objresult = _workListRepository.InsertWorkListHistory(Req);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.InsertWorkListHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, 0);
      }
      return Ok(Objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetWorkListHistory")]
    public ActionResult<GetWorkListHistoryRes> GetWorkListHistory(GetWorkListHistoryReq Req)
    {
      List<GetWorkListHistoryRes> Objresult = new List<GetWorkListHistoryRes>();
      try
      {
        var _errormsg = SampleMaintainenceValidation.GetWorkListHistory(Req);
        if (!_errormsg.status)
        {
          Objresult = _workListRepository.GetWorkListHistory(Req);
        }
        else
          return BadRequest(_errormsg);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetWorkListHistory", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, 0);
      }
      return Ok(Objresult);
    }

    [HttpPost]
    [Route("api/WorkList/GetUserDeptDetails")]
    public List<UserDeptmentDetails> GetUserDeptDetails(getUserNo Req)
    {
      List<UserDeptmentDetails> Objresult = new List<UserDeptmentDetails>();
      try
      {
        Objresult = _workListRepository.GetUserDeptDetails(Req);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetUserDeptDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);
      }
      return Objresult;
    }
    [HttpPost]
    [Route("api/WorkList/GetSubTestCheck")]
    public SingleTestCheckRes GetTestCheck(SingleTestCheck Req)
    {
      SingleTestCheckRes Objresult = new SingleTestCheckRes();
      try
      {
        Objresult = _workListRepository.getTestCheck(Req);
      }
      catch (Exception ex)
      {
        MyDevException.Error(ex, "WorkListController.GetSubTestCheck", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venueNo, Req.venueBranchNo, 0);

      }
      return Objresult;
    }
    [HttpPost]
    [Route("api/WorkList/GetDenguTest")]
    public List<DenguTestRes> GetDenguTest(DenguTestReq Req)
    {
        List<DenguTestRes> Objresult = new List<DenguTestRes>();
        try
        {
            Objresult = _workListRepository.getDenguTest(Req);
        }
        catch (Exception ex)
        {
            MyDevException.Error(ex, "WorkListController.GetDenguTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.venuno, Req.venueBranchNo, 0);

        }
        return Objresult;
    }
  }
}