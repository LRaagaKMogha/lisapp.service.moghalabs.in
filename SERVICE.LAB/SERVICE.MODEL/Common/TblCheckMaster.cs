using System;
using System.Collections.Generic;

namespace Service.Model
{
    public class CheckMasterNameExistsRequest
    {
        public int masterNo { get; set; }
        public int venueNo { get; set; }
        public string? masterName { get; set; }
        public int masterValueno { get; set; }
        public int masterTypeNo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }

    }
    public class CheckMasterNameExistsResponse
    {
        public int avail { get; set; }
    }


}