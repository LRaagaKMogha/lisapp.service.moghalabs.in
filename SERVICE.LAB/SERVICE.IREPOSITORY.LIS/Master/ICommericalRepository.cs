using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface ICommericalRepository
    {

        List<CommericalGetRes> Getcompanymaster(CommericalGetReq getReq);
        CommericalInsRes Insertcompanymaster(CommericalInsReq insReq);
        List<GSTGetRes> GetGSTMaster(GSTGetReq getReq);
        GSTInsRes InsertGSTMaster(GSTInsReq insReq);

    }

}
