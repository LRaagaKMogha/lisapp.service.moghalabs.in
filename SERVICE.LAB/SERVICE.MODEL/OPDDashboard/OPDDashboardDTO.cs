using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class OPDDashBoardReq
    {
        public string? UserType { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PhysicianNo { get; set; }
        public int SpecializationNo { get; set; }
        public string? DateType { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
    }

    public class OPDDashBoardRes
    {
        public int Sno { get; set; }
        public string? Type { get; set; }
        public string? JsonValue { get; set; }
    }
}


