using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IVendorMasterRepository
    {
        List<Responsegetvendor> GetVendorMaster(Requestvendor req);
        StoreVendorMaster InsertVendorMaster(Responsevendor req1);
        List<Getcontactlst> GetVendorvsContactmaster(Getcontact creq);
        int InsertVendorContactmaster(Savecontact creq1);
        List<Getservicelst> GetVendorvsservices(Getservice sobj);
        int InsertVendorService(Saveservice serviceobj);
    }
}
