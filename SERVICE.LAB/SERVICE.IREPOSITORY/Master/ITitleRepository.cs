using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface ITitleRepository

    {
        List<TblTitle> GettitleDetails(TitlemasterRequest titlemaster);
        Titlemasterresponse InsertTitlemaster(TblName tbltitle);
       

    }
}
