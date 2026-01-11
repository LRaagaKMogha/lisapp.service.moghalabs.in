using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.External.CommonMasters
{
 
    public partial class LstTestInfo
    {
        public string? mainDeptName { get; set; }
        public string? deptName { get; set; }
        public int testNo { get; set; }
        public string? testName { get; set; }
        public string? methodName { get; set; }
        public string? sampleName { get; set; }
        public string? containerName { get; set; }
    }
}
