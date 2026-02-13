using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IStoreMasterRepository
    {
        List<StoreMasterResponseDTO> GetStoreMasterDetails(StoreMasterRequestDTO storeMasterRequest);
        StoreMasterInsertResponseDTO InsertStoreMaster(StoreMasterInsertDTO req);
        List<StoreDetails> GetAllStoreByBranch(int VenueNo,int VenueBranchNo);
    }
}