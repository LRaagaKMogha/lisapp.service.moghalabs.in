using Service.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface ITestMastRepository
    {
        List<LstTestInfo> GetTestList(int a, int b);
    }
}
