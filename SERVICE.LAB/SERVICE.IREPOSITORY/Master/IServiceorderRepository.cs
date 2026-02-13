using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IServiceOrderRepository
    {
        List<GetServiceDetails> GetServiceOrderMaster(ServiceOrderMasterRequest serviceOrderItem);
        ServiceOrderMasterResponse InsertServiceOrderMaster(TblServiceOrder resultItem);
    }
}
