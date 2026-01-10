using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class TblSubtestheader
    {
        public int headerNo { get; set; }
        public string headerName { get; set; }
        public string headerdisplaytext { get; set; }
        public Int16 sequenceNo { get; set; }
        public bool? status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public class SubtestheaderMasterRequest
    {
        public int headerNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public class SubtestheaderMasterResponse
    {
        public int HeaderNo { get; set; }   
        public int LastPageIndex { get;set; }
    }
}