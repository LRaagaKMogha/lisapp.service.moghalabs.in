using Service.Model.Inventory;
using System.Threading.Tasks;

namespace Service.IRepository.Inventory
{
    public interface ICommonProductSupplierMappingRepository
    {
        Task<object> GetProductSupplierMappingAsync(ProductSupplierMappingRequestDTO request);
        Task<int> InsertProductSupplierMappingAsync(ProductSupplierMappingInsertDTO dto);
    }
}
