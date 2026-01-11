using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class reqtest
    {
        public string? pageCode { get; set; }
        public int deptNo { get; set; }
        public int maindeptNo { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public int templateNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int masterNo { get; set; }
        public int mastertype { get; set; }
        public int pageIndex { get; set; }
        public bool IsApproval { get; set; }
        public int pageSize { get; set; }
        public int Userstatus { get; set; }
    }

    public partial class lsttest
    {
        public int rowNo { get; set; }
        public int testNo { get; set; }
        public string testShortName { get; set; }
        public string testName { get; set; }
        public string testDisplayName { get; set; }
        public string departmentName { get; set; }
        public bool IsDeptInactive { get; set; }
        public string methodName { get; set; }
        public bool IsMethodInactive { get; set; }
        public string sampleName { get; set; }
        public bool IsSampleInactive { get; set; }
        public string containerName { get; set; }
        public bool IsContainerInactive { get; set; }
        public string unitName { get; set; }
        public bool IsUnitInactive { get; set; }
        public int tsequenceNo { get; set; }
        public bool isActive { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public bool isSelectMultiSample { get; set; }
        public List<MultiSamplesReferenceList>? lstmultisamplesreferencelist { get; set; }
    }
    public partial class objTestList
    {
        public int rowNo { get; set; }
        public int testNo { get; set; }
        public string testShortName { get; set; }
        public string testName { get; set; }
        public string testDisplayName { get; set; }
        public string departmentName { get; set; }
        public bool IsDeptInactive { get; set; }
        public string methodName { get; set; }
        public bool IsMethodInactive { get; set; }
        public string sampleName { get; set; }
        public bool IsSampleInactive { get; set; }
        public string containerName { get; set; }
        public bool IsContainerInactive { get; set; }
        public string unitName { get; set; }
        public bool IsUnitInactive { get; set; }
        public int tsequenceNo { get; set; }
        public bool isActive { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public bool isSelectMultiSample { get; set; }
        public string multisampleXml { get; set; }
    }
    public partial class Objtestsequence
    {
        public string testType { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public List<lsttestsequence> lsttestsequence { get; set; }
    }
    public partial class lsttestsequence
    {
        public int testNo { get; set; }
        public int sequenceNo { get; set; }
    }
    public partial class objtestdbl
    {
        public int testNo { get; set; }
        public string machineCode { get; set; }
        public string testShortName { get; set; }
        public string testName { get; set; }
        public string testDisplayName { get; set; }
        public int deptNo { get; set; }
        public int methodNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int unitNo { get; set; }
        public string volume { get; set; }
        public int tsequenceNo { get; set; }
        public bool isAgeBased { get; set; }
        public string gender { get; set; }
        public decimal rate { get; set; }
        public string barcodeSuffix { get; set; }
        public string barcodePrefix { get; set; }
        public string resultType { get; set; }
        public int decimalPoint { get; set; }
        public bool isRoundOff { get; set; }
        public string cutoffTime { get; set; }
        public int processingMinutes { get; set; }
        public int statMinutes { get; set; }
        public string processingDays { get; set; }

        public bool isNonBillable { get; set; }
        public bool isQtyChange { get; set; }
        public bool isRateEditable { get; set; }
        public bool isAllowDiscount { get; set; }
        public bool isInterface { get; set; }
        public bool isautoapproval { get; set; }
        public bool isOutsource { get; set; }
        public bool isNonMandatory { get; set; }
        public bool isNonReportable { get; set; }
        public bool isIndiviual { get; set; }
        public bool isNABL { get; set; }
        public bool isInterNotes { get; set; }
        public bool isBillDisclaimer { get; set; }
        public bool isReportDisclaimer { get; set; }
        public bool isConsent { get; set; }
        public bool isComments { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string interNotes { get; set; }
        public string billDisclaimer { get; set; }
        public string reportDisclaimer { get; set; }
        public string consentNotes { get; set; }
        public string comments { get; set; }
        public string testrefrange { get; set; }
        public string testPickList { get; set; }
        public string testTemplate { get; set; }
        public int tcNo { get; set; }
        //    public string selectMultiSampleJson { get; set; }
        public bool isSelectMultiSample { get; set; }
        public bool isNonConcurrentTest { get; set; }
        public string multisampleXml { get; set; }
        public bool isUploadOption { get; set; }
        public int isMultiEditor { get; set; }
        public bool isFormulaFor { get; set; }
        public decimal samplequantity { get; set; }
        public bool isunacceptable { get; set; }
        public string? unacceptcondition { get; set; }
        public bool isincludeinstruction { get; set; }
        public string? includeinstruction { get; set; }
        public bool isSecondReview { get; set; }
        public string testCode { get; set; }
        public string languageText { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public string testProcessTime { get; set; }
        public string testanlyrange { get; set; }
        public bool isResultInWL { get; set; }
        public bool IsSampleAct { get; set; }
        public bool isSensitiveData { get; set; }
        public string loincCode { get; set; }
        public int loincNo { get; set; }
        public int OldServiceNo { get; set; }
        public bool IsDeltaApproval { get; set; }
        public decimal DeltaRange { get; set; }
        public decimal RestrictedValue { get; set; }
        public int ReptDeptHeaderNo { get; set; }
        public bool IsInfectionCtrlRept { get; set; }
        public bool IsNEHR { get; set; }
        public Int16 languagecode { get; set; }
        public bool IsNoPrintInRpt { get; set; }
        public string BarShortName { get; set; }
        public Int16 SecDeptNo { get; set; }
        public bool IsSubTestAvailable { get; set; }
        public string nehrinternotes { get; set; }
        public bool IsSpecialCategory { get; set; }
    }

    public partial class objtest
    {
        public int testNo { get; set; }
        public string machineCode { get; set; }
        public string testShortName { get; set; }
        public string testName { get; set; }
        public string testDisplayName { get; set; }
        public int deptNo { get; set; }
        public int methodNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int unitNo { get; set; }
        public string volume { get; set; }
        public int tsequenceNo { get; set; }
        public bool isAgeBased { get; set; }
        public string gender { get; set; }
        public decimal rate { get; set; }
        public string barcodeSuffix { get; set; }
        public string barcodePrefix { get; set; }
        public string resultType { get; set; }
        public int decimalPoint { get; set; }
        public bool isRoundOff { get; set; }
        public int statMinutes { get; set; }
        public bool isNonBillable { get; set; }
        public bool isQtyChange { get; set; }
        public bool isRateEditable { get; set; }
        public bool isAllowDiscount { get; set; }
        public bool isInterface { get; set; }
        public bool isautoapproval { get; set; }
        public bool isOutsource { get; set; }
        public bool isNonMandatory { get; set; }
        public bool isNonReportable { get; set; }
        public bool isIndiviual { get; set; }
        public bool isNABL { get; set; }
        public bool isInterNotes { get; set; }
        public bool isBillDisclaimer { get; set; }
        public bool isReportDisclaimer { get; set; }
        public bool isConsent { get; set; }
        public bool isComments { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string interNotes { get; set; }
        public string billDisclaimer { get; set; }
        public string reportDisclaimer { get; set; }
        public string consentNotes { get; set; }
        public string comments { get; set; }
        public string testrefrange { get; set; }
        public string testPickList { get; set; }
        public int tcNo { get; set; }
        public List<lsttestrefrange> lsttestrefrange { get; set; }
        public List<lsttestPickList> lsttestPickList { get; set; }
        public string? testTemplate { get; set; }
        public List<lstTemplateList> lstTemplateList { get; set; }
        public string? selectMultiSampleJson { get; set; }
        public bool isSelectMultiSample { get; set; }
        public bool isNonConcurrentTest { get; set; }
        public List<MultiSampleList>? lstMultiSampleList { get; set; }
        public List<MultiSamplesReferenceList>? lstmultisamplesreferencelist { get; set; }
        public bool isUploadOption { get; set; }
        public int isMultiEditor { get; set; }
        public bool isFormulaFor { get; set; }
        public decimal samplequantity { get; set; }
        public bool isunacceptable { get; set; }
        public string? unacceptcondition { get; set; }
        public bool isincludeinstruction { get; set; }
        public string? includeinstruction { get; set; }
        public string interNotesHigh { get; set; }
        public string interNotesLow { get; set; }
        public bool isSecondReview { get; set; }
        public string testCode { get; set; }
        public string languageText { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public string testProcessTime { get; set; }
        public List<lsttestanalyrange> lsttestanalyrange { get; set; }
        public bool isResultInWL { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReject { get; set; }
        public string RejectReason { get; set; }
        public int OldServiceNo { get; set; }
        public bool IsSampleAct { get; set; }
        public bool isSensitiveData { get; set; }
        public int loincNo { get; set; }
        public string loincCode { get; set; }
        public bool IsDeltaApproval { get; set; }
        public decimal DeltaRange { get; set; }
        public decimal RestrictedValue { get; set; }
        public int ReptDeptHeaderNo { get; set; }
        public bool IsInfectionCtrlRept { get; set; }
        public bool IsNEHR { get; set; }
        public Int16 languagecode { get; set; }
        public bool IsNoPrintInRpt { get; set; }
        public string BarShortName { get; set; }
        public Int16 SecDeptNo { get; set; }
        public bool IsSubTestAvailable { get; set; }
        public string nehrInterpreditationnotes { get; set; }
        public bool IsSpecialCategory { get; set; }
    }
    public partial class lsttestanalyrange
    {
        public int analyticalRangeNo { get; set; }
        public int testNo { get; set; }
        public string genderCode { get; set; }
        public int ageFrom { get; set; }
        public string ageFromType { get; set; }
        public int ageTo { get; set; }
        public string ageToType { get; set; }
        public Decimal lesserValue { get; set; }
        public Decimal greaterValue { get; set; }
        public bool status { get; set; }
        public int oldAnalRangeNo { get; set; }
        public int fastingOrNonfasting { get; set; }
    }

    public class MultiSamplesReferenceList
    {
        public int ID { get; set; }
        public int multiSampleDetailsNo { get; set; }
        public int methodNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int unitNo { get; set; }
        public string volume { get; set; }
        public bool isSelect { get; set; }
        public bool isPriority { get; set; }
        public int ageFrom { get; set; }
        public string ageFromType { get; set; }
        public int ageTo { get; set; }
        public string ageToType { get; set; }
        public string crRangeFrom { get; set; }
        public string crRangeTo { get; set; }
        public string displayCRRR { get; set; }
        public string displayMMRR { get; set; }
        public string displayRR { get; set; }
        public string genderCode { get; set; }
        public bool isAgeBased { get; set; }
        public string maxRange { get; set; }
        public string minRange { get; set; }
        public string rangeFrom { get; set; }
        public string rangeTo { get; set; }
        public int referrenceRangeNo { get; set; }
        public bool status { get; set; }
        public int subTestNo { get; set; }
        public int testNo { get; set; }
        public decimal samplequantity { get; set; }
        public bool isunacceptable { get; set; }
        public string unacceptcondition { get; set; }
        public bool isincludeinstruction { get; set; }
        public string includeinstruction { get; set; }
        public int fastingOrNonfasting { get; set; }

    }
    public class MultiSampleList
    {
        public int Id { get; set; }
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public int MethodNo { get; set; }
        public string MethodName { get; set; }
        public string SampleName { get; set; }
        public bool IsSelect { get; set; }
        public bool IsPriority { get; set; }

    }
    public class GetManageOptionalResponse
    {
        public int result { get; set; }
    }
    public class GetMultiplsSampleResponse
    {
        public int RowNo { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public int MethodNo { get; set; }
        public string MethodName { get; set; }
        public string SelectMultiSampleJson { get; set; }
    }
    public class GetMultiplsSampleRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string Type { get; set; }
        public int TestNo { get; set; }
    }
    public class UpdateRefRangeRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int VisitNo { get; set; }
        public int TestNo { get; set; }
        public int PatientNo { get; set; }
        public int OrdersNo { get; set; }
        public int OrderlistNo { get; set; }
        public int MethodNo { get; set; }
        public int ContainerNo { get; set; }
        public int SampleNo { get; set; }
        public int UserNo { get; set; }
    }
    public class UpdateRefRangeResponse
    {
        public int TestNo { get; set; }
    }
    public partial class lsttestrefrange
    {
        public int referrenceRangeNo { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public string genderCode { get; set; }
        public int ageFrom { get; set; }
        public string ageFromType { get; set; }
        //public int ageFromdays { get; set; }
        public int ageTo { get; set; }
        public string ageToType { get; set; }
        //public int ageTodays { get; set; }
        public string rangeFrom { get; set; }
        public string rangeTo { get; set; }
        public string displayRR { get; set; }
        public string crRangeFrom { get; set; }
        public string crRangeTo { get; set; }
        public string displayCRRR { get; set; }
        public string minRange { get; set; }
        public string maxRange { get; set; }
        public string displayMMRR { get; set; }
        public bool isAgeBased { get; set; }
        public bool status { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int methodNo { get; set; }
        public bool isSelectMultiSampleRecord { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public int unitNo { get; set; }
        public int oldRefRangeNo { get; set; }
        public int fastingOrNonfasting { get; set; }
        public int processingBranch { get; set; }
    }
    public partial class lsttestPickList
    {
        public int testPickListNo { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public string pickCode { get; set; }
        public string pickvalue { get; set; }
        public bool isdefault { get; set; }
        public bool isabnormal { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public string comments { get; set; }
    }
    public partial class lstTemplateList
    {
        public int templateNo { get; set; }
        public int testNo { get; set; }
        public string templateName { get; set; }
        public string templateText { get; set; }
        public bool isDefault { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int OldServiceNo { get; set; }
        public int OldTemplateNo { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReject { get; set; }
        public bool? IsApprovalTestTemplate {  get; set; }
    }
    public partial class rtntemplateNo
    {
        public int templateNo { get; set; }
        public int templateApprovalNo { get; set; }
    }
    public partial class returntemplateNo
    {
        public int templateNo { get; set; }
        public int templateApprovalNo { get; set; }
        public string? templateText { get; set; }
    }
    public partial class templateNoresponse
    {
        public int templateNo { get; set; }
        public int templateApprovalNo { get; set; }
    }
    public partial class rtntest
    {
        public int testNo { get; set; }
    }
    public partial class rtntemplateText
    {
        public string templateText { get; set; }
        public int templateNo { get; set; }
    }
    public partial class lstgrppkg
    {
        public int rowNo { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int serviceNo { get; set; }
        public string shortName { get; set; }
        public string serviceName { get; set; }
        public string displayName { get; set; }
        public string departmentName { get; set; }
        public int sequenceNo { get; set; }
        public decimal rate { get; set; }
        public bool isActive { get; set; }
        public int updateseqNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public Int16 bufferDays { get; set; }
        public DateTime? bufferDate { get; set; }
    }

    public partial class objgrppkgdbl
    {
        public int serviceNo { get; set; }
        public int tcNo { get; set; }
        public string shortName { get; set; }
        public string serviceName { get; set; }
        public string displayName { get; set; }
        public int deptNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int sequenceNo { get; set; }
        public decimal rate { get; set; }
        public string cutoffTime { get; set; }
        public int processingMinutes { get; set; }
        public string processingDays { get; set; }
        public bool isRateEditable { get; set; }
        public bool isAllowDiscount { get; set; }
        public bool isOutsource { get; set; }
        public bool isInterNotes { get; set; }
        public bool isBillDisclaimer { get; set; }
        public bool isReportDisclaimer { get; set; }
        public bool isConsent { get; set; }
        public bool isComments { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string grppkgtests { get; set; }
        public bool isunacceptable { get; set; }
        public bool isincludeinstruction { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public bool isSecondReview { get; set; }
        public string testCode { get; set; }
        public string languageText { get; set; }
        public int loincNo { get; set; }
        public string loincCode { get; set; }
        public bool IsInfectionCtrlRept { get; set; }
        public string gender { get; set; }
        public int ReptDeptHeaderNo { get; set; }
        public Int16 languagecode { get; set; }
        public int OldServiceNo { get; set; }
        public string BarShortName { get; set; }
        public bool IsChoice { get; set; }
        public int ChoiceCount { get; set; }
        public string nehrInterPnotes { get; set; }
        public Int16 bufferDays { get; set; }
        public string? bufferDate { get; set; }
        public bool isdisplayinreport { get; set; }
        public bool isSpecimen { get; set; }
        public bool IsSpecialCategory { get; set; }
        public string includeinstruction { get; set; }
        public bool isUploadOption { get; set; }
    }
    public partial class objgrppkg
    {
        public string pageCode { get; set; }
        public int serviceNo { get; set; }
        public int tcNo { get; set; }
        public string shortName { get; set; }
        public string serviceName { get; set; }
        public string displayName { get; set; }
        public int deptNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int sequenceNo { get; set; }
        public decimal rate { get; set; }
        public string cutoffTime { get; set; }
        public int processingMinutes { get; set; }
        public string processingDays { get; set; }
        public bool isRateEditable { get; set; }
        public bool isAllowDiscount { get; set; }
        public bool isOutsource { get; set; }
        public bool isInterNotes { get; set; }
        public bool isBillDisclaimer { get; set; }
        public bool isReportDisclaimer { get; set; }
        public bool isConsent { get; set; }
        public bool isComments { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string interNotes { get; set; }
        public string billDisclaimer { get; set; }
        public string reportDisclaimer { get; set; }
        public string consentNotes { get; set; }
        public string comments { get; set; }
        public decimal samplequantity { get; set; }
        public bool isunacceptable { get; set; }
        public string unacceptcondition { get; set; }
        public bool isincludeinstruction { get; set; }
        public string includeinstruction { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public List<lstgrppkgservice> lstgrppkgservice { get; set; }
        public bool isSecondReview { get; set; }
        public string testCode { get; set; }
        public string languageText { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReject { get; set; }
        public string RejectReason { get; set; }
        public int OldServiceNo { get; set; }
        public int loincNo { get; set; }
        public string loincCode { get; set; }
        public bool IsInfectionCtrlRept { get; set; }
        public string gender { get; set; }
        public int ReptDeptHeaderNo { get; set; }
        public Int16 languagecode { get; set; }
        public string BarShortName { get; set; }
        public bool? IsChoice { get; set; }
        public int ChoiceCount { get; set; }
        public string nehrInterpreditationnotes { get; set; }
        public Int16 bufferDays { get; set; }
        public string bufferDate { get; set; }
        public bool isdisplayinreport { get; set; }
        public bool IsSpecimen { get; set; }
        public string Specimen { get; set; }
        public bool IsSpecialCategory { get; set; }
        public bool isUploadOption { get; set; }
    }
    public partial class lstgrppkgservice
    {
        public int rowNo { get; set; }
        public int parentNo { get; set; }
        public int childNo { get; set; }
        public int serviceNo { get; set; }
        public string serviceShortName { get; set; }
        public string serviceName { get; set; }
        public string serviceType { get; set; }
        public int childsequenceNo { get; set; }
        public bool status { get; set; }
        public decimal Amount { get; set; }
        public bool isOptional { get; set; }
    }
    public partial class reqsearchservice
    {
        public int deptNo { get; set; }
        public int searchby { get; set; }
        public string searchtext { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public partial class lststestdbl
    {
        public int rowNo { get; set; }
        public int testNo { get; set; }
        public string testName { get; set; }
        public string departmentName { get; set; }
        public int departmentNo { get; set; }
        public string subTestDept { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public int subTestNo { get; set; }
        public string subTestName { get; set; }
        public string methodName { get; set; }
        public string unitName { get; set; }
        public int stSequenceNo { get; set; }
        public bool isActive { get; set; }
    }
    public partial class lststest
    {
        public int rowNo { get; set; }
        public int testNo { get; set; }
        public string testName { get; set; }
        public string departmentName { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public List<lstsubtest> lstsubtest { get; set; }
    }
    public partial class lstsubtest
    {
        public int testNo { get; set; }
        public int departmentNo { get; set; }
        public string departmentName { get; set; }
        public string subTestDept { get; set; }
        public int subTestNo { get; set; }
        public string subTestName { get; set; }
        public string methodName { get; set; }
        public string unitName { get; set; }
        public int stSequenceNo { get; set; }
        public bool isActive { get; set; }
    }
    public partial class objsubtestdbl
    {
        public int testNo { get; set; }
        public string testName { get; set; }
        public int departmentNo { get; set; }
        public int subTestNo { get; set; }
        public string machineCode { get; set; }
        public string testShortName { get; set; }
        public string subTestName { get; set; }
        public string testDisplayName { get; set; }
        public int methodNo { get; set; }
        public int unitNo { get; set; }
        public int headerNo { get; set; }
        public int stSequenceNo { get; set; }
        public bool isAgeBased { get; set; }
        public string resultType { get; set; }
        public int decimalPoint { get; set; }
        public bool isRoundOff { get; set; }
        public bool isNonMandatory { get; set; }
        public bool isNonReportable { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string testrefrange { get; set; }
        public string testPickList { get; set; }
        public string languageText { get; set; }
        public bool isExtraSubTest { get; set; }
        public bool IsDeltaApproval { get; set; }
        public decimal DeltaRange { get; set; }
        public decimal RestrictedValue { get; set; }
        public Int16 languagecode { get; set; }
        public string testCode { get; set; }
        public bool IsNoPrintInRpt { get; set; }
        public string testanlyrange { get; set; }
    }
    public partial class objsubtest
    {
        public int testNo { get; set; }
        public string testName { get; set; }
        public int departmentNo { get; set; }
        public int subTestNo { get; set; }
        public string machineCode { get; set; }
        public string testShortName { get; set; }
        public string subTestName { get; set; }
        public string testDisplayName { get; set; }
        public int methodNo { get; set; }
        public int unitNo { get; set; }
        public int headerNo { get; set; }
        public int stSequenceNo { get; set; }
        public bool isAgeBased { get; set; }
        public string resultType { get; set; }
        public int decimalPoint { get; set; }
        public bool isRoundOff { get; set; }
        public bool isNonMandatory { get; set; }
        public bool isNonReportable { get; set; }
        public bool isActive { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public List<lsttestrefrange> lsttestrefrange { get; set; }
        public List<lsttestPickList> lsttestPickList { get; set; }
        public string languageText { get; set; }
        public bool isExtraSubTest { get; set; }
        public bool IsDeltaApproval { get; set; }
        public decimal DeltaRange { get; set; }
        public decimal RestrictedValue { get; set; }
        public Int16 languagecode { get; set; }
        public string testCode { get; set; }
        public bool IsNoPrintInRpt { get; set; }
        public List<lsttestanalyrange> lsttestanalyrange { get; set; }
    }
    public class SaveFormulaRequest
    {
        public int formulaNo { get; set; }
        public string formula { get; set; }
        public char parameterType { get; set; }
        public char serviceType { get; set; }
        public int formulaFor { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public char GenderCode { get; set; }
    }
    public class SaveFormulaResponse
    {
        public int formulaNo { get; set; }
    }
    public class GetFormulaRequest
    {
        public int testNo { get; set; }
        public char testtype { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class GetFormulaResponse
    {
        public int formulafor { get; set; }
        public string formulatext { get; set; }
        public string actualformulatext { get; set; }
        public int ID { get; set; }
        public string GenderCode { get; set; }
        public string GenderDesc { get; set; }
        public bool IsEdit { get; set; }
    }
    public class CheckTestcodeExists
    {
        public int testNo { get; set; }
        public string testCode { get; set; }
        public string testtype { get; set; }
        public int vendorNo { get; set; }
        public string vendorCode { get; set; }
        public string vendor { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }

    public class CheckTestcodeExistsRes
    {
        public int outNo { get; set; }
        public string existing { get; set; }
    }
    public partial class reqtestapprove
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int serviceNo { get; set; }
        public string serviceType { get; set; }
        public int venueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class restestapprove
    {
        public int RowNo { get; set; }
        public string ServiceTypeName { get; set; }
        public string serviceType { get; set; }
        public Int32 TotalRecords { get; set; }
        public int ServiceNo { get; set; }
        public string TestName { get; set; }
        public int pageIndex { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int TypeOrder { get; set; }
        public string UserName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string DepartmentName { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public string MethodName { get; set; }
        public string UnitName { get; set; }
        public int STSequenceNo { get; set; }
        public bool IsActive { get; set; }
        public int OldServiceNo { get; set; }
        public List<lstsubtestApp> lstsubtestApp { get; set; }
    }
    public partial class lstsubtestApp
    {
        public int Id { get; set; }
        public int ServiceNo { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string MethodName { get; set; }
        public string UnitName { get; set; }
        public int STSequenceNo { get; set; }
        public bool IsActive { get; set; }
    }

    public partial class restestappHistory
    {
        public int RowNo { get; set; }
        public string ServiceTypeName { get; set; }
        public string serviceType { get; set; }
        public int ServiceNo { get; set; }
        public string TestName { get; set; }
        public Int32 TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedOn { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string RejectReason { get; set; }
    }
    public partial class GetTATRes
    {
        public int RowNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypeName { get; set; }
        public int ServiceNo { get; set; }
        public string TestName { get; set; }
        public string MainDeptName { get; set; }
        public Int16 MainDeptNo { get; set; }
        public string DepartmentName { get; set; }
        public int DeptNo { get; set; }
        public string processingDays { get; set; }
        public string cutoffTime { get; set; }
        public int processingMinutes { get; set; }
        public Byte TypeOrder { get; set; }
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
    }

    public partial class GetTATReq
    {
        public Int16 MainDeptNo { get; set; }
        public int DeptNo { get; set; }
        public int serviceNo { get; set; }
        public string serviceType { get; set; }
        public Int16 venueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class InsTATReq
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public List<ServiceDTO> testXML { get; set; }
    }

    public class ServiceDTO
    {
        public string cutoffTime { get; set; }
        public Int16 MainDeptNo { get; set; }
        public int DeptNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string processingDays { get; set; }
        public int processingMinutes { get; set; }
    }

    public class InsTATRes
    {
        public bool? Status { get; set; }
    }

    public partial class GetloincReq
    {
        public int LoincNo { get; set; }
        public Int16 VenueNo { get; set; }
        public int pageIndex { get; set; }
        public int ServiceNo { get; set; }
        public int DeptNo { get; set; }
    }
    public partial class GetloincRes
    {
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public int LoincNo { get; set; }
        public string LoincCode { get; set; }
        public bool status { get; set; }
        public string ComponentName { get; set; }
        public string ShortName { get; set; }
        public string Hl7FieldID { get; set; }
        public string Method { get; set; }
        public string DeptName { get; set; }
        public string TestName { get; set; }
        public int TestNo { get; set; }
        public int DeptNo { get; set; }
        public string ServiceType { get; set; }
        public string SubTestName { get; set; }
        public int SubTestNo { get; set; }
    }
    public partial class InsloincReq
    {
        public int LoincNo { get; set; }
        public string LoincCode { get; set; }
        public bool status { get; set; }
        public string ComponentName { get; set; }
        public string ShortName { get; set; }
        public string Hl7FieldID { get; set; }
        public string Method { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public int SubtestNo { get; set; }
    }
    public partial class InsloincRes
    {
        public int LoincNo { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class GetSnomedReq
    {
        public int SnomedNo { get; set; }
        public int VenueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class GetSnomedRes
    {
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public int SnomedNo { get; set; }
        public string SnomedCode { get; set; }
        public bool status { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
    public partial class InsSnomedReq
    {
        public int SnomedNo { get; set; }
        public string SnomedCode { get; set; }
        public bool status { get; set; }
        public string Description { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public bool IsDefault { get; set; }
    }
    public partial class InsSnomedRes
    {
        public int SnomedNo { get; set; }
        public int LastPageIndex { get; set; }
    }
    public partial class IntegrationPackageReq
    {
        public int Id { get; set; }
        public string SourceCode { get; set; }  
        public int deptNo { get; set; }
        public int TestNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int masterNo { get; set; }
        public int Type { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
    public partial class IntegrationPackageResult
    {
        public int Result { get; set; }
    }
    public partial class IntegrationPackageRes
    {
        public short Id { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public string SourcePkgCode { get; set; }
        public string createdOn { get; set; }
        public string LISPkgCode { get; set; }
        public string packageName { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
    public class PrintPackageDetails
    {
        public int RowNo { get; }
        public string PackageName { get; set; }
        public int PackageNo { get; }
        public string PackageCode { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentCode { get; set; }
        public int DeptSequence { get; set; }
        public string GroupName { get; set; }
        public int GroupNo { get; set; }
        public string GroupCode { get; set; }
        public int GroupSequence { get; set; }
        public int GroupMapSequence { get; set; }
        public string TestName { get; set; }
        public int TestNo { get; set; }
        public string TestCode { get; set; }
        public int TestSequence { get; set; }
        public string SubTestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestCode { get; set; }
        public int SubTestSequence { get; set; }
        public string SourcePkgCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsComments { get; set; }
        public bool IsSpecimen { get; set; }
        public string Comments { get; set; }
        public string Specimen { get; set; }
        public string VenueBranchDisplayText { get; set; }
        public string Address { get; set; }

    }
    public class StainMasterInsertReq
    {
        public int stainId { get; set; }
        public string stainName { get; set; }
        public string stainCode { get; set; }
        public string description { get; set; }
        public Boolean status { get; set; }
        public int venueNo { get; set; }
    }
    public class StainMasterInsertRes
    { 
        public int status { get; set; }
    }
    public class GetStatinMasterDetailsReq {
        public int venueNo { get; set; }
        public int pageIndex { get; set; }
        public int searchBy { get; set; }
    }
    public class GetStatinMasterDetailsRes
    { 
        public Int64 id { get; set; }
        public string StatinName { get; set; }
        public string StainCode { get; set; }
        public bool status { get; set; }
        public string description { get; set; }
        public int totalRecords { get; set; }
    }
}

