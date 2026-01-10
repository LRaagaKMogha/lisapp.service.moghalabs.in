using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface IGroupMastRepository
    {
        List<LstGroupInfo> GetGroupInfo(int a, int b);
    }
}
