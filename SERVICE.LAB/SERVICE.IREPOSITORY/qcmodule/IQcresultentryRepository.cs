using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IQcresultentryRepository
    {
        List<GetTblqcresult> GetqcresultDetails(QcresultRequest req);
        QcresultResponse InsertqcresultDetails(SaveqcresDTO req);
        SaveqcresDTO EditqcresultDetails(EditqcresDTO req);
    }
}