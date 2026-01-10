using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface ICommonRepository
    {
        List<LstSearch> CommonSearch(RequestCommonSearch req);
        NotificationResponse SendMessage(NotificationDto Notification);
        List<LstMasterSearch> CommonSearchMaster(RequestCommonMasterSearch req);
        List<LstFilter> CommonFilter(string filterKey, int venueNo, int venueBranchNo);
        List<GetCriticalResultsResponse> GetCriticalResultNotify(GetCriticalResultsReq req);
        List<ApprovalResponse> getApprovallist(ApprovalRequestDTO req);
        SaveCriticalResultNotifyRes SaveCriticalResultNotify(SaveCriticalResultsReq req);
        List<ApprovalHistory> GetApprovalHistory(int oldno, int newno, int type, int VenueNo, int VenueBranchNo, int UserNo);
        List<ResTransactionSplit> GetTransactionSplitDetails(ReqTransactionSplit req);
        List<ResTransactionSplitById> GetTransactionSplitDetailsById(ReqTransactionSplit req);
        FetchPortalResponse GetPortalUrl(Int16 VenueNo, string EntityType);
    }
}
