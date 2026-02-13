using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IInventoryDashBoardRepository
    {
        List<InventoryDashBoardRes> GetInventoryDashBoard(InventoryDashBoardReq RequestItem);
    }
}
