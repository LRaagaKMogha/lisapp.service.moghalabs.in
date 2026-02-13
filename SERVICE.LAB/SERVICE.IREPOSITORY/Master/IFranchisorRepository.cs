using Service.Model.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IRepository.Master
{
    public interface IFranchisorRepository
    {
        List<GetFranchiseResponse> GetFranchises(int VenueNo, int VenueBranchNo);
        List<FranchiseRevenueSharingServiceDto> GetFranchiseRevenueSharingByService(GetFranchiseRevenueSharingByServiceRequest request);
        Task<int> InsertFranchiseRevenueSharingAsync(FranchiseRevenueSharingInsertDTO dto);
    }
}
