using Service.Model;
using Service.Model.FrontOffice.PatientDue;
using Service.Model.PatientInfo;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
   public interface IPatientDueRepository
    {
        List<PatientDueResponse> GetDuePatientInfoDetails(CommonFilterRequestDTO RequestItem);
        CreatePatientDueResponse InsertPatientDue(CreatePatientDueRequest createPatientDueRequest);

        //List<CustomSearchResponse> GetCustomSearch(CommonSearchRequest searchRequest);
        CancelVisit GetPatientCancelTestInfo(getrequest Req);
        rtnCancelTest InsertCancelTest(CancelVisit Req);
        CreatePatientDueResponse Insertbulkpatientdue(List<CreatePatientDueRequest> createPatientDueRequest);
        List<GetReqCancelResponse> GetRefundCancelRequest(GetReqCancelParam RequestItem);
        UpdateReqCancelResponse ApproveRefundCancel(UpdateReqCancelParam RequestItem);
    }
}

