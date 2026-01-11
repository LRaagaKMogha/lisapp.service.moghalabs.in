using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class reqCollectDTS
    {
        public int CollectionNo { get; set; }
        public string Type { get; set; }
        public string SelectDate { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        
    }
    public partial class lstCollectDTS
    {
        public int CollectionNo { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string CollectionDate { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public string VenueName { get; set; }
        public string VenueBranchName { get; set; }
    }
    public partial class updateCollectDTS
    {
        public int CollectionNo { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string CollectionDate { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }
    public partial class resCollectDTS
    {
        public int CollectionNo { get; set; }

    }
}
