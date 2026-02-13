using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface ICommericalRepository
    {
        List<CommericalGetRes> Getcompanymaster(CommericalGetReq getReq);
        CommericalInsRes Insertcompanymaster(CommericalInsReq insReq);
        List<GSTGetRes> GetGSTMaster(GSTGetReq getReq);
        GSTInsRes InsertGSTMaster(GSTInsReq insReq);
    }
}