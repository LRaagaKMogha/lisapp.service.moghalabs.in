using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IDashBoardRepository
    {
        List<DashBoardResponse> GetDashBoards(DashBoardRequest RequestItem);
    }
}
