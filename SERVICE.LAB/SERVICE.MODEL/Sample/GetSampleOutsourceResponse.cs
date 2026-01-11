using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetSampleOutsourceResponse
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
        public string sampleCollectedDTTM { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public string BarcodeNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int orderListNo { get; set; }
        public int orderTransactionNo { get; set; }
        public int TATFlag { get; set; }
        public bool isincludedinreport { get; set; }
        public bool directApproval { get; set; }
        public string sampleOutsourceDTTM { get; set; }
        public string actualResultAckDTTM { get; set; }
        public bool IsVipIndication { get; set; }
        public int ResultAckType { get; set; }
        public string rhNo { get; set; }
        public string idNumber { get; set; }
        public int PhysicianNo { get; set; }
    }
    public class GetSampleOutSourceHistory
    {
        public Int64 rowNo { get; set; }
        public string ReceiptNo { get; set; }
        public string OutsourceDTTM { get; set; }
        public string CreatedOn { get; set; }
        public Int32 OutsourceNo {  get; set; }
        public string vendorName { get; set; }
        public string userName { get; set; }
        public int totalrecords { get; set; }
        public int pageindex { get; set; }
    }
}

