using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class GetbranchSampleReceiveResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string PrimaryId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string CustomerName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public string BarcodeNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int orderListNo { get; set; }
        public int orderTransactionNo { get; set; }
        public string VisitDTTM { get; set; }
        public string TransferedDTTM { get; set; }
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public bool isAccept { get; set; }
        public bool isReject { get; set; }
        public int TATFlag { get; set; }
    }
}
