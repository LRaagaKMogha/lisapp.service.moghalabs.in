using Service.Model.Inventory;
using System.Threading.Tasks;

namespace Service.IRepository.Inventory
{
    public interface IStoreProductMappingRepository
    {
        Task<object> GetStoreProductMappingAsync(StoreProductMappingRequestDTO request);
        Task<int> InsertStoreProductMappingAsync(StoreProductMappingInsertDTO dto);
    }
}
