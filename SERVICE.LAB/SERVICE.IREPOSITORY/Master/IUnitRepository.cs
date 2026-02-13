using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface IUnitRepository
    {
        List<lstunits> GetUnits(reqUnits req);
        rtnUnit InsertUnitDetails(TblUnits Unititem);
    }
}
