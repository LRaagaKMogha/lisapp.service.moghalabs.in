using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IOPDDashBoardRepository
    {
        List<OPDDashBoardRes> GetOPDDashBoard(OPDDashBoardReq RequestItem);
    }
}
