using DEV.Model;
using DEV.Model.Inventory;
using DEV.Model.Inventory.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface ISupplierMasterRepository
    {
        //List<tbl_IV_SupplierMaster> GetSupplierMasters(GetCommonMasterRequest masterRequest);
        int InsertSupplierMasterDetails(postSupplierMasterDTO supplierMaster);
        List<GetSupplierMasterResponse> GetSupplierDetails(SupplierMasterRequest masterRequest);
        EditSupplierresponse GetEditSuppiler(UpdateSupplierMaster req);
    }
}


