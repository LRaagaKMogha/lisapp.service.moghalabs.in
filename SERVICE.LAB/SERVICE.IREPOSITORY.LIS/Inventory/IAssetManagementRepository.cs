using DEV.Model;
using DEV.Model.Inventory.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Inventory
{
    public interface IAssetManagementRepository
    {
        int InsertInstrumentDetails(postAssetManagementDTO objManuDTO);
        List<GetAssetManagementResponse> GetInstrumentDetail(AssetManagementRequest masterRequest);

    }
}
