using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IContainerRepository
    {
        List<TblContainer> Getcontainermaster(ContainerMasterRequest containerRequest);
        ContainerMasterResponse Insertcontainermaster(TblContainer tblContainer);
    }
}
