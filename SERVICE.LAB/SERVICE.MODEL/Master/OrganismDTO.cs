using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class Reqsearchorganism
    {
        public int flag { get; set; }
        public string searchtext { get; set; }
        public int organismtypeno { get; set; }
        public int organismno { get; set; }
        public int venueno { get; set; }       
        public int venuebranchno { get; set; }
        public bool status { get; set; }
    }
    public partial class Lstorganism
    {
        public int organismno { get; set; }
        public string organismmccode { get; set; }
        public int organismtypeno { get; set; }
        public string organismcode { get; set; }
        public string organismname { get; set; }
        public string notes { get; set; }
        public int sequenceno { get; set; }
        public bool status { get; set; }
    }
    public partial class Lstotdrugmap
    {
        public int antibioticno { get; set; }
        public string antibioticcode { get; set; }
        public string antibioticname { get; set; }
        public int sequenceno { get; set; }
    }
}