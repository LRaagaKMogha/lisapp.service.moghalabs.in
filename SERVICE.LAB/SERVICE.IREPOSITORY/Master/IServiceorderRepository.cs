using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface IServiceOrderRepository
    {

        List<GetServiceDetails> GetServiceOrderMaster(ServiceOrderMasterRequest serviceOrderItem);
        ServiceOrderMasterResponse InsertServiceOrderMaster(TblServiceOrder resultItem);
    }

}
