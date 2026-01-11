using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface IContainerRepository
    {

        List<TblContainer> Getcontainermaster(ContainerMasterRequest containerRequest);
        ContainerMasterResponse Insertcontainermaster(TblContainer tblContainer);

        //int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest);
    }

}
