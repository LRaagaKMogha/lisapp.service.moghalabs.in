using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetParameterAnalyserResponse
    {
        public int Sno { get; set; }
        public int  ParameterNo { get; set; }
        public string ParameterName { get; set; }
        public string ParameterCode { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public  string SubTestName  { get; set; }
        public bool Status  { get; set; }
        public int BranchNo { get; set; }
        public string BranchName { get; set; }
        public int AnalyserNo { get; set; }
        public string AnalyserName { get; set; }

    }
}

