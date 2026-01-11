using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblState
    {
        public int StateNo { get; set; }
        public string StateName { get; set; }
        public int CountryNo { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
    }
}
