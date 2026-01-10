using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface IDeptMastRepository
    {
        List<LstDepartment> GetDepartment(int a, int b);
        List<LstMainDepartment> GetMainDepartment(int a, int b);
    }
}
