using Service.Model;
using Service.Model.Inventory.Master;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IManufacturerMasterRepository
    {
        int InsertManufacturerDetails(postManufacturerMasterDTO objManuDTO);
        List<GetManufacturerMasterResponse> GetManufacturersDetail(ManufacturerMasterRequest masterRequest);
    }
} 