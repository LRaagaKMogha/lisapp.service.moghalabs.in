using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{
    public class CreateBranchSampleReceiveRequest
    {
       
        public int visitNo { get; set; }
        public int orderListNo { get; set; }
        public int orderTransactionNo { get; set; }
        public bool isAccept { get; set; }
        public bool isReject { get; set; }
        public string comments { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
    }
}
