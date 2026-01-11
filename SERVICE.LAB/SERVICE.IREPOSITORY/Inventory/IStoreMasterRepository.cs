using Service.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;
using Service.Model.Inventory.Master;

namespace Dev.IRepository.Inventory
{
    public interface IStoreMasterRepository
    {
        List<StoreMasterResponseDTO> GetStoreMasterDetails(StoreMasterRequestDTO storeMasterRequest);
        StoreMasterInsertResponseDTO InsertStoreMaster(StoreMasterInsertDTO req);
        List<StoreDetails> GetAllStoreByBranch(int VenueNo,int VenueBranchNo);
    }
}