using DEV.Model;
using DEV.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IInsuranceRepository
    {
        List<NetworkMasterDTO> GetNetworkMasterDetails(int venueNo, int venueBranchNo, int pageIndex);
        NetworkMasterDTOResponse InsertNetworkMasterDetails(NetworkMasterRequest objDTO);
        List<CompanyMasterDTO> GetCompanyMasterDetails(int venueNo, int venueBranchNo, int pageIndex);
        CompanyMasterDTOResponse InsertCompanyMasterDetails(CompanyMasterRequest objDTO);
        DeductionDTOResponse InsertDeductionMaster(DeductionMasterDTO objDTO);
        List<DeductionResponse> GetDeductionMaster(int venueNo, int venueBranchNo, int pageIndex);
    }
}
