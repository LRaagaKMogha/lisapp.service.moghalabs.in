using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface IPackageMastRepository
    {
        List<LstPackageInfo> GetPackageInfo(int a, int b);
        LstPackageBreakUpInfo GetPackageBreakUp(int a, int b, int c);
    }
}
