using Service.Model;
using Service.Model.FrontOffice.PatientDue;
using Service.Model.Sample;
using System.Collections.Generic;

namespace Service.IRepository
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

