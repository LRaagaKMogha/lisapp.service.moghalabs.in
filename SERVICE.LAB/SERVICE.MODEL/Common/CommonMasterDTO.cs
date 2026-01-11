using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class CommonMasterDto
    {
        public int RowNo { get; set; }
        public int CommonNo { get; set; }
        public string? CommonKey { get; set; }
        public string? CommonCode { get; set; }
        public string? CommonName { get; set; }
        public string? CommonValue { get; set; }
        public bool IsDefault { get; set; }
        public int SequenceNo { get; set; }
        public bool? IsAbnormal { get; set; } = false;
    }
    public partial class RequestCommonSearch
    {
        public string? pagecode { get; set; }
        public int viewvenuebranchno { get; set; }
        public int searchby { get; set; }
        public string? searchtext { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
    }
    public partial class RequestCommonMasterSearch
    {
        public int viewvenuebranchno { get; set; }
        public string? searchby { get; set; }
        public string? searchtext { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
    }
    public partial class LstMasterSearch
    {
        public int masterno { get; set; }
        public string? displaytext { get; set; }
        public string? searchdisplaytext { get; set; }
    }
    public partial class TimeDto
    {
        public string? currenttime { get; set; }
    }
    public partial class LstSearch
    {
        public int patientvisitno { get; set; }
        public string? displaytext { get; set; }
        public string? searchdisplaytext { get; set; }
        public int? venueBranchNo { get; set; }
        public int? patientno { get; set; }
    }
    public partial class ConfigurationDto
    {
        public int RowNo { get; set; }
        public string? ConfigurationKey { get; set; }
        public string? Description { get; set; }
        public string? Control { get; set; }
        public string? ConfigType { get; set; }
        public int ConfigValue { get; set; }
    }
    public partial class RequestCommonFilter
    {
        public string? filterKey { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public partial class LstFilter
    {
        public string? filterCode { get; set; }
        public string? filterValue { get; set; }
    }
    public partial class AppSettingResponse
    {
        public int ConfigNo { get; set; }
        public string? ConfigValue { get; set; }
        public string? Description { get; set; }
    }

    public class ApprovalRequestDTO
    {
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class ApprovalResponse
    {
        public Int32 TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int NewNo { get; set; }
        public int OldNo { get; set; }
        public string restype { get; set; }
        public string Description { get; set; }
    }

    public class GetCriticalResultsReq
    {
        public int userNo { get; set; }
        public string? pageCode { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class GetCriticalResultsResponse
    {
        public int rowNo { get; set; }
        public string patientID { get; set; }
        public int patientNo { get; set; }
        public int patientVisitNo { get; set; }
        public string visitID { get; set; }
        public string fullName { get; set; }
        public string ageType { get; set; }
        public string visitDTTM { get; set; }
        public int approvedByNo { get; set; }
        public string approvedBy { get; set; }
        public string approvedOn { get; set; }
        public string referralType { get; set; }
        public int refferralTypeNo { get; set; }
        public int customerNo { get; set; }
        public string customerName { get; set; }
        public int physicianNo { get; set; }
        public string physicianName { get; set; }
        public string nRICNo { get; set; }
        public int orderListNo { get; set; }
        public int orderDetailsNo { get; set; }
        public string testType { get; set; }
        public int testNo { get; set; }
        public string testName { get; set; }
        public int subTestNo { get; set; }
        public string subTestName { get; set; }
        public string result { get; set; }
        public string resultType { get; set; }
        public string resultFlag { get; set; }
        public string dIResult { get; set; }
        public int unitNo { get; set; }
        public string unitName { get; set; }
        public int methodNo { get; set; }
        public string methodName { get; set; }
        public string lLColumn { get; set; }
        public string hLColumn { get; set; }
        public string displayRR { get; set; }
        public string cRHLColumn { get; set; }
        public string cRLLColumn { get; set; }
        public string displayCRRR { get; set; }
        public string comments { get; set; }
        public int departmentNo { get; set; }
        public string departmentName { get; set; }
    }
    public class SaveCriticalResultsReq
    {
        public int userNo { get; set; }
        public int modifyUserNo { get; set; }
        public string? pageCode { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public List<GetCriticalResultsResponse> lstSaveData { get; set; }
    }
    public class SaveCriticalResultNotifyRes
    {
        public int oStatus { get; set; }
    }
    public partial class RequestSpecimenMedia
    {
        public int SpecimenNo { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int Userno { get; set; }
        public List<SpecimenMappingMediaItem> lstmedia { get; set; }
    }
    public class SpecimenMappingMediaItem
    {
        public int commonNo { get; set; }
        public string commonName { get; set; }
        public bool isDefault { get; set; }
    }
    public class SpecimenMappingResponse
    {
        public int result { get; set; }
    }
    public class SpecimenMappingoutput
    {
        public string mediastring { get; set; }
    }
    public partial class requestCommonSearch
    {
        public string pagecode { get; set; }
        public int viewvenuebranchno { get; set; }
        public int searchby { get; set; }
        public string searchtext { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public bool IsAck { get; set; }
    }

    public class RefTypeCommonMasterDto
    {
        public int RowNo { get; set; }
        public int CommonNo { get; set; }
        public string? CommonKey { get; set; }
        public string? CommonCode { get; set; }
        public string? CommonName { get; set; }
        public string? CommonValue { get; set; }
        public bool IsDefault { get; set; }
        public int SequenceNo { get; set; }
        public bool suppressDueInBill { get; set; }
        public bool SuppressDueBillCashPType { get; set; }
        public bool suppressDueBillIP { get; set; }
    }
    public class ReqTransactionSplit
    { 
        public string type { get; set; }
        public string action { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int refferalTypeNo { get; set; }
        public int refferalNo { get; set; }
        public int marketingNo { get; set; }
        public int riderNo { get; set; }
        public int billUserNo { get; set; }
        public int viewVenueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int deptNo { get; set; }
        public Int16 mainDeptNo { get; set; }
        public string filter { get; set; }
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public int clientType { get; set; }
    }
    public class ResTransactionSplit
    {
        public string referralType { get; set; }
        public int refferralTypeNo { get; set; }
        public int customerNo { get; set; }
        public decimal netAmount {  get; set; }
        public decimal dueAmount { get; set; }
        public decimal grossAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public Int32 sno { get; set; }
        public decimal discountAmount { get; set; }
        public string refCode {  get; set; }
    }
    public class ResTransactionSplitById
    {
        public Int64 sNo { get; set; }
        public string visitID { get; set; }
        public string fullName { get; set; }
        public decimal netAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal dueAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public decimal amount { get; set; }
        public string testName { get; set; }
        public string regDttm { get; set; }
    }
    public class FetchPortalResponse
    {
        public string returnUrl { get; set; }
    }
}
