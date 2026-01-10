using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IOrganismRepository
    {
        List<lstorganism> GetOrganismMaster(reqsearchorganism req);

    }
}
