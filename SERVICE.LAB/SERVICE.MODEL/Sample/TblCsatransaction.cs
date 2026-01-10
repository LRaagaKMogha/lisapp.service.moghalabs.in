using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblCsatransaction
    {
        public int CsatransactionNo { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int SampleCount { get; set; }
        public decimal CollectedAmount { get; set; }
        public int CollectedStatus { get; set; }
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }

    }
    public partial class CsaRequest
    {
        public int pageIndex { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public partial class CsaResponse
    {
        public int result { get; set; } 
    }
}
