using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface ITestMastRepository
    {
        List<LstTestInfo> GetTestList(int a, int b);
    }
}
