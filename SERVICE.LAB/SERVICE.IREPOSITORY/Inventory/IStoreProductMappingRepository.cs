using Service.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Inventory
{
    public interface IStoreProductMappingRepository
    {
        Task<object> GetStoreProductMappingAsync(StoreProductMappingRequestDTO request);
        Task<int> InsertStoreProductMappingAsync(StoreProductMappingInsertDTO dto);
    }
}
