using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblCountryList
    {
        public int CountryNo { get; set; }
        public string CountryName { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
    }
}
