using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IDashBoardRepository
    {
        List<DashBoardResponse> GetDashBoards(DashBoardRequest RequestItem);
    }
}
