using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.CommonMasters
{
  
    public partial class LstSubTestInfo
    {
        public string? mainDeptName { get; set; }
        public string? deptName { get; set; }
        public int testNo { get; set; }
        public string? parentTest { get; set; }
        public int subtestNo { get; set; }
        public string? subTest { get; set; }
        public string? methodName { get; set; }
        public string? sampleName { get; set; }
        public string? containerName { get; set; }
    }
}
