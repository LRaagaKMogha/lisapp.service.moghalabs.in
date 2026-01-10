using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetAllParameterAnalyserRequest : GetCommonMasterRequest
    {
        public int ParameterNo { get; set; }
        public int TestNo { get; set; }
        public int SubTestNo { get; set; }
        public bool Status { get; set; }
        public int BranchNo { get; set; }
        public int AnalyserNo { get; set; }
    }
}
