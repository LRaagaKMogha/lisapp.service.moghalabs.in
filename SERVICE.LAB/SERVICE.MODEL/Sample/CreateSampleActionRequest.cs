using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
   public class CreateSampleActionRequest
    {
        public int PatientVisitNo { get; set; }
        public int TestNo { get; set; }
        public string ServiceType { get; set; }
        public int IntegrationOrderNo { get; set; }
        public int SampleNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
        public bool isAccept { get; set; }
        public bool isReject { get; set; }
        public string barCodeNo { set; get; }
        public string remarks { get; set; }
        public int orderListNo { get; set; }
        public bool IsOnHold { get; set; }
        public int IntegrationOrderTestNo { get; set; }
        public bool IsFasting { get; set; }
        public int sourceofspecimenno { get; set; }
        public string rejectioncomments { get; set; }
        public bool IsOutSource { get; set; }
    }


    public class CreateSampleActionResponse
    {
        public Int32 resultStatus { get; set; }
    }
}




