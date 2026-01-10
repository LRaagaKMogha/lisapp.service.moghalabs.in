using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
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
