using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.CommonMasters
{
   
    public partial class LstPhysician
    {
        public string? doctorName { get; set; }
        public int doctorNo { get; set; }
        public string? doctorQualif { get; set; }
    }

    public partial class LstInternalPhysician
    {
        public string? doctorName { get; set; }
        public int doctorNo { get; set; }
        public string? doctorQualif { get; set; }
    }
}
