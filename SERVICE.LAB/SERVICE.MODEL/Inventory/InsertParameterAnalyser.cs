using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class InsertParameterAnalyser
    {
        public int ParameterNo { get; set; }
        public int TestNo { get; set; }
        public int SubTestNo { get; set; }
        public bool Status { get; set; }
        public int BranchNo { get; set; }
        public int AnalyserNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int createdby { get; set; }
        public int modifiedBy { get; set; }
    }
}
