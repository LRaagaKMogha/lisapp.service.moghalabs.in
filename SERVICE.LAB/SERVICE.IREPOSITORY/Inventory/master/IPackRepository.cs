using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IPackRepository
    {
        List<TblPack> Getpackmaster(PackMasterRequest packRequest);
        PackMasterResponse Insertpackmaster(TblPack tblPack);
    }
}