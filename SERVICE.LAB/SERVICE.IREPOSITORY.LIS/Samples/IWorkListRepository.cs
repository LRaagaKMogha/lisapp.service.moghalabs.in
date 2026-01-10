using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DEV.Model.Sample;

namespace Dev.IRepository.Samples
{
   public interface IWorkListRepository
    {
        List<WorkListResponse> GetWorkList(WorkListRequest RequestItem);
        Task<List<WorkListResponse>> PrintPatientReport(WorkListRequest PatientItem);
        List<WorkListHistoryRes> InsertWorkListHistory(WorkListHistoryReq Req);
        List<GetWorkListHistoryRes> GetWorkListHistory(GetWorkListHistoryReq Req);
        List<HistoWorlkListRes> GetHistoWorkList(WorkListRequest RequestItem);
        List<UserDeptmentDetails> GetUserDeptDetails(getUserNo RequestItem);
        SingleTestCheckRes getTestCheck(SingleTestCheck RequestItem);
        List<DenguTestRes> getDenguTest(DenguTestReq RequestItem);
    }
}
