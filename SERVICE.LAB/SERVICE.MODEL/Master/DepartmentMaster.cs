using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class DepartmentMaster
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
    }
}
