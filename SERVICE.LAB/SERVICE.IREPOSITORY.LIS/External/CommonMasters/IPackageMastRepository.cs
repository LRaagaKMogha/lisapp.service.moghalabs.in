using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface IPackageMastRepository
    {
        List<LstPackageInfo> GetPackageInfo(int a, int b);
        LstPackageBreakUpInfo GetPackageBreakUp(int a, int b, int c);
    }
}
