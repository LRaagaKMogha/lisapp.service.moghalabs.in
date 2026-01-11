using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
   public interface IUnitRepository
    {
        List<lstunits> GetUnits(reqUnits req);
        rtnUnit InsertUnitDetails(TblUnits Unititem);
    }
}

