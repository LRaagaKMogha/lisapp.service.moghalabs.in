using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IInventoryDashBoardRepository
    {
        List<InventoryDashBoardRes> GetInventoryDashBoard(InventoryDashBoardReq RequestItem);
    }
}
