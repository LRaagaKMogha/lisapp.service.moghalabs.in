using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class InsertConsumptionMapping
    {
        public int ConsumptionNo { get; set; }
        public int AnalyzerNo { get; set; }
        public int ParameterNo { get; set; }
        public int ProductNo { get; set; }
        public int UnitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public int ModifiedBy { get; set; }
        public bool Status { get; set; }

    }
}

