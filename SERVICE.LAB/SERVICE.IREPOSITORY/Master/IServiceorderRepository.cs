using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IServiceOrderRepository
    {
        List<GetserviceDetails> GetserviceOrderMaster(ServiceOrderMasterRequest serviceOrderItem);
        ServiceOrderMasterResponse InsertServiceOrderMaster(TblServiceOrder resultItem);
    }
}
