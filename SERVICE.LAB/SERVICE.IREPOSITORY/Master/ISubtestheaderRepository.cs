using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface ISubtestheaderRepository
    {
        List<TblSubtestheader> GetSubtestheadermaster(SubtestheaderMasterRequest subtestheaderMasterRequest);
        SubtestheaderMasterResponse InsertSubtestheadermaster(TblSubtestheader testheader);
    }
}