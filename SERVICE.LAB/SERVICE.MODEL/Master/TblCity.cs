using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblCity
    {
        public int CityNo { get; set; }
        public string CityName { get; set; }
        public int StateNo { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
    }
}
