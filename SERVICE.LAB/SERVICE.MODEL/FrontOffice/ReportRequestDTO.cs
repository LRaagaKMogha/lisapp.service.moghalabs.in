using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
   public class ReportRequestDTO
    {
        public int visitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
        public string print { get; set; }
        public int testNo { get; set; }
        public string testType { get; set; }
        public string hcAppNo { get; set; }
    }
}
