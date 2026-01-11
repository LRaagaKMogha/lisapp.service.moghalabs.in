using Service.Model;
using Service.Model.Inventory;
using Service.Model.Inventory.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IManufacturerMasterRepository
    {
        //List<tbl_IV_Manufacturer> GetManufacturers(GetCommonMasterRequest masterRequest);
        int InsertManufacturerDetails(postManufacturerMasterDTO objManuDTO);
        List<GetManufacturerMasterResponse> GetManufacturersDetail(ManufacturerMasterRequest masterRequest);
    }
} 