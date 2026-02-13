using Service.Model;
using Service.Model.Inventory.Master;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IAssetManagementRepository
    {
        int InsertInstrumentDetails(postAssetManagementDTO objManuDTO);
        List<GetAssetManagementResponse> GetInstrumentDetail(AssetManagementRequest masterRequest);
    }
}
