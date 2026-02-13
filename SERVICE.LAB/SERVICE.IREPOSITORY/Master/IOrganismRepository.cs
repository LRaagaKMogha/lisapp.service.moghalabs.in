using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IOrganismRepository
    {
        List<lstorganism> GetOrganismMaster(reqsearchorganism req);
    }
}
