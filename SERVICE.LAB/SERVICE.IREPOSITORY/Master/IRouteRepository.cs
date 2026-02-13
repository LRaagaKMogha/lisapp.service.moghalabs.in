using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IRouteRepository
    {
        List<TblRoute> GetRouteDetails(GetCommonMasterRequest getCommonMaster);
        List<TblRoute> SearchRoute(string RouteName);
        List<Routelst> GetrouteMaster(RouteMasterRequest routeitems);
        RouteMasterResponse InsertRouteMaster(Routelst route);
        int InsertRouteDetails(TblRoute Routeitem);
    }
}
