using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface IDeptMastRepository
    {
        List<LstDepartment> GetDepartment(int a, int b);
        List<LstMainDepartment> GetMainDepartment(int a, int b);
    }
}
