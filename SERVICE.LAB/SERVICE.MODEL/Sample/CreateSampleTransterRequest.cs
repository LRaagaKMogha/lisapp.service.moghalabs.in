using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{
    public class CreateSampleTransterRequest
    {
        public int transferBranchNo { get; set; }
        public int visitNo { get; set; }
        public int orderListNo { get; set; }
        public int orderTransactionNo { get; set; }
        public string expDate { get; set; }
        public string comments { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }

    }
    public class SampleReportResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string patientID { get; set; }
        public string PrimaryId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string RegistrationDate { get; set; }
        public string RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string CustomerName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string TestType { get; set; }
        public int ResultTypeNo { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public int gender { get; set; }
        public string mobile { get; set; }
        public string emailID { get; set; }
        public string sampleName { get; set; }
        public string ReceivedDTTM { get; set; }
        public bool IsAccept { get; set; }
        public bool IsReject { get; set; }
        public string VenueBranchName { get; set; }

    }
}

