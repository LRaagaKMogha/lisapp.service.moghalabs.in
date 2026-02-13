using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IMainDepartmentRepository
    {
        List<TblMainDepartment> GetMainDepartmentDetails(MainDepartmentmasterRequest maindeptmaster);
        MainDepartmentMasterResponse InsertMainDepartmentmaster(TblMainDepartment tblmaindepartment);           
    }
}
