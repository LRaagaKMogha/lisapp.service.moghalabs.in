using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class CreateSampleOutSourceRequest
    {
        public int vendorNo { get; set; }
        public int visitNo { get; set; }
        public int orderListNo { get; set; }
        public int orderTransactionNo { get; set; }
        public string expDate { get; set; }
        public string comments { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public bool directApproval { get; set; }
        public string actualResultAckDTTM { get; set; }
        public int resultAckType { get; set; }
    }
}
