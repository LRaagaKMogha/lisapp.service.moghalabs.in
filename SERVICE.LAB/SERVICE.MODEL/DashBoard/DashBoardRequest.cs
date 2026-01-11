using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class DashBoardRequest
    {
        public string?  UserType { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? DateType { get; set; }
        public string fromdate {  get; set; }
        public string todate { get; set; }
    }
}


