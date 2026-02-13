using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface IMultiPriceListRepository
    {
        List<GetmultiPriceListResponse> GetMultiPriceListDetails(GetmultiPriceListRequest getRequest);
        InsertMultiPriceListResponse InsertMultiPriceListDetails(InsertMultiPriceListRequest tariffMasteritem);
    }
}
