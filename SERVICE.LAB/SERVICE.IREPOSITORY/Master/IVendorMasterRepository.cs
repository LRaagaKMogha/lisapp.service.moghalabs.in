using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IVendorMasterRepository
    {
        List<responsegetvendor> GetVendorMaster(requestvendor req);
        StoreVendorMaster InsertVendorMaster(responsevendor req1);
        List<getcontactlst> GetVendorvsContactmaster(getcontact creq);
        int InsertVendorContactmaster(savecontact creq1);
        List<getservicelst> GetVendorvsservices(getservice sobj);
        int InsertVendorService(saveservice serviceobj);
    }
}
