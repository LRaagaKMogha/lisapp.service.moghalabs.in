using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{
    public class WorkListResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public int PatientNo { get; set; }
        public string PatientName { get; set; }
        public string PrimaryId { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string SampleCollectionDate { get; set; }
        public string RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string BarcodeNo { get; set; }
        public string CustomerName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int OrderListNo { get; set; }
        public int Sno { get; set; }
        public string TatStatus { get; set; }
        public string NricNo { get; set; }

        public bool isVipIndication { get; set; }
        public string venueBranchName { get; set; }
        public string orderListStatusText { get; set; }
        public string referralType { get; set; }

    }
    public class HistoWorlkListRes
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public int PatientNo { get; set; }
        public string PatientName { get; set; }
        public string PrimaryId { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string SampleCollectionDate { get; set; }
        public string RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string BarcodeNo { get; set; }
        public string CustomerName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int OrderListNo { get; set; }
        public int Sno { get; set; }
        public string nricNo {  get; set; }
        //   public string userno {  get; set; }
        public string printedby { get; set; }
        public string approvedby { get; set; }
        public string tatdate { get; set; }
        public string rhNum { get; set; }
        public string trimmedby { get; set; }
        public string blockedby { get; set; }
        public string sectionedby { get; set; }
        public string blockandslide { get; set; }
        public string typeOfProcess { get; set; }
        public string remarks { get; set; }
        public string dateReported { get; set; }
        public string tissueProcessor { get; set; }
        public string comments { get; set; }
    }

    public class WorkListHistoryReq
    {
        public string OrderList { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int GridShowFilter { get; set; }
        public string Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int reportFormat { get; set; }

    }
    public class WorkListHistoryRes
    {
        public int WorklistNo { get; set; }
        public int worklistType { get; set; }

    }
    public class GetWorkListHistoryReq
    {
        public int WorklistNo { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int departno {  get; set; }
        public int testno { get; set; }
        public string testType { get; set; }
        public int gridStatus {  get; set; }
        public int userNo { get; set; }
        public int isDenguTest { get; set; }

    }
    public class GetWorkListHistoryRes
    {
        public Int32 TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public int RowNo { get; set; }
        public string TranNo { get; set; }
        public string TranDate { get; set; }
        public bool status { get; set; }
        public int CreatedBy { get; set; }
        public int WorklistNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int GridShowFilter { get; set; }

        public string TestDetails {  get; set; }
        public int worklistType { get; set; }
    }

    public class getUserNo
    { 
        public Int32 userNo { get; set; }
        public Int32 venueNo { get; set; }
        public Int32 venueBranchNo { get; set; }
    }

    public class UserDeptmentDetails
    { 
        public Int64 id { get; set; }
        public Int32 deptno { get; set; }
    }
    public class SingleTestCheck
    {  
        public int testno {  get; set; }
        public string testType { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get;set; }
    }
    public class SingleTestCheckRes
    { 
        public int subTestCheck { get; set; }
    }

    public class DenguTestReq
    {
        public int venuno { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class DenguTestRes
    {
        public Int64 rowNo { get; set; }
        public int testNo { get; set; }
        public string testType { get; set; }
    }
}

