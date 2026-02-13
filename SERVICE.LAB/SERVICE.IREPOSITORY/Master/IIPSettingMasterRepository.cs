using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface IIPSettingMasterRepository
    {
        List<RCPriceList> GetEditIpSettings(int venueNo, int venueBranchNo, int physicianNo, int rcNo);

        List<GetIPSettingResponse> GetIpSettings(int venueNo, int venueBranchNo, int pageIndex, int IPSettingNo);
        InsertTariffMasterResponse InsertIpSettingMasterDetails(List<IPSettingRequest> ipSettingRequest);
    }
}


