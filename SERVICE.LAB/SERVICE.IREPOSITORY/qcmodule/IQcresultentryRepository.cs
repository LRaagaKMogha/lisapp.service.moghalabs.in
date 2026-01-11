using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IQcresultentryRepository
    {
        List<GetTblqcresult> GetqcresultDetails(QcresultRequest req);
        QcresultResponse InsertqcresultDetails(SaveqcresDTO req);
        SaveqcresDTO EditqcresultDetails(EditqcresDTO req);
    }
}