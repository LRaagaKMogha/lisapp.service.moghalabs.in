using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;
namespace Dev.IRepository
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
