using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.CommonMasters
{
   
    public partial class LstDepartment
    {
        public string? mainDeptName { get; set; }
        public int deptNo { get; set; }
        public string? deptName { get; set; }
    }

    public partial class LstMainDepartment
    {
        public string? mainDeptName { get; set; }
        public Int16 mainDeptNo { get; set; }
    }
}
