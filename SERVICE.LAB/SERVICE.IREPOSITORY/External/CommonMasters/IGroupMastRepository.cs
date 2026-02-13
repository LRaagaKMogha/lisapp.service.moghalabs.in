using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface IGroupMastRepository
    {
        List<LstGroupInfo> GetGroupInfo(int a, int b);
    }
}
