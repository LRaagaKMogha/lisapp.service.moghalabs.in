using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class TblPack
    {
        public int packNo { get; set; }
        public string description { get; set; }       
        public Int16  quantity{ get; set; }
        public Int16 sequenceNo { get; set; }
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int currentseqNo { get; set; }

    }

    public class PackMasterRequest
    {
        public int packNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int pageIndex { get; set; }
    }
    public class PackMasterResponse
    {
        public int packNo { get; set; }      
    }

}
