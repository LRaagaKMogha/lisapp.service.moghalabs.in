using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface IUnitRepository
    {
        List<Lstunits> GetUnits(ReqUnits req);
        RtnUnit InsertUnitDetails(TblUnits Unititem);
    }
}
