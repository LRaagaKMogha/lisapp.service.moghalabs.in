using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
   public interface IMultiPriceListRepository
    {
        List<GetmultiPriceListResponse> GetMultiPriceListDetails(GetmultiPriceListRequest getRequest);
        InsertMultiPriceListResponse InsertMultiPriceListDetails(InsertMultiPriceListRequest tariffMasteritem);
    }
}



