using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class TblAnalyzer
    {
        public Int16 AnalyzerMasterNo { get; set; }

        public string SerialNo { get; set; }
        public string Description { get; set; }
        public string AssetCode { get; set; }
       // public int sequenceNo { get; set; }
        public bool? Status { get; set; }

        public Int16 VenueNo { get; set; }

        public DateTime CreatedOn { get; set;}
        public int CreatedBy { get; set; }

  
    }
   
}



