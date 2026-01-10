using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IPackRepository
    {

        List<TblPack> Getpackmaster(PackMasterRequest packRequest);
        PackMasterResponse Insertpackmaster(TblPack tblPack);

        //int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest);
    }

}
