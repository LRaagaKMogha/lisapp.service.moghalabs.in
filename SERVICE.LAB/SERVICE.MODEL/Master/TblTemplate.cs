using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblTemplate
    {
        public int templateNo { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public string templateName { get; set; }
        public bool? isdefault { get; set; }
        public int SequenceNo { get; set; }    
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
}
