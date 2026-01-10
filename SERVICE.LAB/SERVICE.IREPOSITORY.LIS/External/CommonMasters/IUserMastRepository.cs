using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface IUserMastRepository
    {
        List<LstUser> GetUserList(int a, int b);
    }
}
