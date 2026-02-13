using Service.Model;
using Service.Model.Inventory;
using Service.Model.Inventory.Master;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface ISupplierMasterRepository
    {
        int InsertSupplierMasterDetails(postSupplierMasterDTO supplierMaster);
        List<GetSupplierMasterResponse> GetSupplierDetails(SupplierMasterRequest masterRequest);
        EditSupplierresponse GetEditSuppiler(UpdateSupplierMaster req);
    }
}