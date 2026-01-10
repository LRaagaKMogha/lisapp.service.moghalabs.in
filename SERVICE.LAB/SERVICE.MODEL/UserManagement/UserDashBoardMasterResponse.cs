using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class UserDashBoardMasterResponse
    {
        public int DashBoardMasterNo { get; set; }
        public string DashBoardMasterKey { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Style { get; set; }
        public bool IsSelect { get; set; }

    }
}
