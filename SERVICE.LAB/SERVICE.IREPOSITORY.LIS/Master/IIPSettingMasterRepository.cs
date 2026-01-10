using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
   public interface IIPSettingMasterRepository
    {
        List<RCPriceList> GetEditIpSettings(int venueNo, int venueBranchNo, int physicianNo, int rcNo);

        List<GetIPSettingResponse> GetIpSettings(int venueNo, int venueBranchNo, int pageIndex, int IPSettingNo);
        InsertTariffMasterResponse InsertIpSettingMasterDetails(List<IPSettingRequest> ipSettingRequest);
    }
}


