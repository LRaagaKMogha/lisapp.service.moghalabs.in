using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface IUserMastRepository
    {
        List<LstUser> GetUserList(int a, int b);
    }
}
