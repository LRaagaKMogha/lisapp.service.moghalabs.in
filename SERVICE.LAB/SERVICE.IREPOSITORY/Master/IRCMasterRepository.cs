using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IRCMasterRepository
    {
        List<RCPriceList> GetEditRCMaster(int venueNo, int venueBranchNo, int rcNo);
        List<GetRCMasterResponse> GetRCDetails(int venueNo, int venueBranchNo, int pageIndex, int RcNo);
        InsertTariffMasterResponse InsertRCMaster(InsertRCMasterRequest rCMasterRequest);
        List<TblRC> GetRCMasterDetails(GetCommonMasterRequest masterRequest);
    }
}


