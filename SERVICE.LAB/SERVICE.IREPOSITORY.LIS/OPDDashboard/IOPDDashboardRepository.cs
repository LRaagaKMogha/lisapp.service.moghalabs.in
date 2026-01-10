using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IOPDDashBoardRepository
    {
        List<OPDDashBoardRes> GetOPDDashBoard(OPDDashBoardReq RequestItem);
    }
}
