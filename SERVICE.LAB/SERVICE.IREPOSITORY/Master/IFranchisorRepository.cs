using Service.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Master
{
    public interface IFranchisorRepository
    {
        List<GetFranchiseResponse> GetFranchises(int VenueNo, int VenueBranchNo);
        List<FranchiseRevenueSharingServiceDto> GetFranchiseRevenueSharingByService(GetFranchiseRevenueSharingByServiceRequest request);
        Task<int> InsertFranchiseRevenueSharingAsync(FranchiseRevenueSharingInsertDTO dto);
    }
}
