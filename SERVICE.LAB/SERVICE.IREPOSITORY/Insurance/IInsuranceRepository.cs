using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
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
