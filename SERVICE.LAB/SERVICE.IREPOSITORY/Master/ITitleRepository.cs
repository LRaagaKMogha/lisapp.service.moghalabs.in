using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface ITitleRepository
    {
        List<TblTitle> GettitleDetails(TitlemasterRequest titlemaster);
        Titlemasterresponse InsertTitlemaster(TblName tbltitle);
    }
}
