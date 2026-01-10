using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IMainDepartmentRepository
    {
            List<TblMainDepartment> GetMainDepartmentDetails(MainDepartmentmasterRequest maindeptmaster);
            MainDepartmentMasterResponse InsertMainDepartmentmaster(TblMainDepartment tblmaindepartment);
           
    }
}
