using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEV.Model
{
    public partial class requestpatientreport
    {
        public Int16 maindeptNo { get; set; }
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int viewvenuebranchno { get; set; }
        public int pageindex { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public int refferraltypeno { get; set; }
        public int customerno { get; set; }
        public int physicianno { get; set; }
        public int riderno { get; set; }
        public int excutiveno { get; set; }
        public bool isstat { get; set; }
        public bool isSTATFilter { get; set; }
        public bool isdue { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        public int whatsappstatus { get; set; }
        public int loginType { get; set; }
        public int routeNo { get; set; }
        public int deliverymode { get; set; }
        public int wardNo { get; set; }
        public string searchKey { get; set; }
        public int pageCount { get; set; }
        public string multiFieldsSearch { get; set; }
    }
    public partial class lstpatientreportdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public string rhNo { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agegender { get; set; }
        public bool ispatientimage { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public int refferraltypeno { get; set; }
        public string referraltype { get; set; }
        public int customerno { get; set; }
        public string customername { get; set; }
        public bool islabheader { get; set; }
        public bool isreportblocked { get; set; }
        public bool isinternotes { get; set; }
        public int physicianno { get; set; }
        public string physicianname { get; set; }
        public int riderno { get; set; }
        public string ridername { get; set; }
        public int excutiveno { get; set; }
        public string excutivename { get; set; }
        public bool isstat { get; set; }
        public string rctdttm { get; set; }
        public string modeofdispatch { get; set; }
        public bool isvisitstatus { get; set; }
        public string taskdttm { get; set; }
        public bool isdue { get; set; }
        public int cancelled { get; set; }
        public bool visabnormal { get; set; }
        public bool viscritical { get; set; }
        public bool vistat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        public int whatsappstatus { get; set; }
        public bool visremarks { get; set; }
        public bool viscpremarks { get; set; }
        public int orderno { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicecode { get; set; }
        public string servicename { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string colorcode { get; set; }
        public string barcodeno { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecall { get; set; }
        public bool iscancelled { get; set; }
        public string enteredby { get; set; }
        public string enteredon { get; set; }
        public string validatedby { get; set; }
        public string validatedon { get; set; }
        public string approvedby { get; set; }
        public string approvedon { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public bool issms { get; set; }
        public string smsdttm { get; set; }
        public bool isemail { get; set; }
        public string emaildttm { get; set; }
        public bool isprint { get; set; }
        public string printdttm { get; set; }
        public bool iscpreportshow { get; set; }
        public string cpreportshowdttm { get; set; }
        public bool iscpprint { get; set; }
        public string cpprintdttm { get; set; }
        public bool isremarks { get; set; }
        public bool iscpremarks { get; set; }
        public bool isservicestatus { get; set; }
        public Int32 TotalRecords { get; set; }
        public int DuePrint { get; set; }
        public bool Deliverymodes { get; set; }
        public string VenueBranchName { get; set; }
        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
        public bool istrand { get; set; }
        public bool iswsms { get; set; }
        public string whatsappdttm { get; set; }
        public string communicationPtName { get; set; }
        public string communicationVisitId { get; set; }
        public string communicationVisitDate { get; set; }
        public string patientmobile { get; set; }
        public string patientemailid { get; set; }
        public string customeremailid { get; set; }
        public string customermobile { get; set; }
        public string physicianemailid { get; set; }
        public string physicianmobile { get; set; }
        public bool isPatientReportSMS { get; set; }
        public bool isPatientReportEmail { get; set; }
        public bool isPatientReportWhatsapp { get; set; }
        public bool isCustomerReportWhatsapp { get; set; }
        public bool isCustomerReportEmail { get; set; }
        public bool isCustomerReportSMS { get; set; }
        public bool isPhysicianreportEmail { get; set; }
        public bool isPhysicianreportSms { get; set; }
        public bool isPhysicianreportWhatsapp { get; set; }
        public string emailIdToShow { get; set; }
        public string mobileNumberToShow { get; set; }
    }

    public partial class lstpatientreport
    {
        public Int16 maindeptNo { get; set; }
        public bool ischecked { get; set; }
        public string rhNo { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public int patientno { get; set; }
        public string fullname { get; set; }
        public string agegender { get; set; }
        public bool ispatientimage { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public int refferraltypeno { get; set; }
        public string referraltype { get; set; }
        public int customerno { get; set; }
        public string customername { get; set; }
        public int physicianno { get; set; }
        public string physicianname { get; set; }
        public bool islabheader { get; set; }
        public bool isreportblocked { get; set; }
        public bool isinternotes { get; set; }
        public int riderno { get; set; }
        public string ridername { get; set; }
        public int excutiveno { get; set; }
        public string excutivename { get; set; }
        public bool isstat { get; set; }
        public string rctdttm { get; set; }
        public string modeofdispatch { get; set; }
        public bool isvisitstatus { get; set; }
        public string taskdttm { get; set; }
        public bool isdue { get; set; }
        public int cancelled { get; set; }
        public bool visabnormal { get; set; }
        public bool viscritical { get; set; }
        public bool vistat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        public int whatsappstatus {  get; set; }
        public bool visremarks { get; set; }
        public bool viscpremarks { get; set; }
        public Int32 TotalRecords { get; set; }
        public List<lstreportorderlist> lstreportorderlist { get; set; }
        public int DuePrint { get; set; }
        public bool Deliverymodes { get; set; }
        public string VenueBranchName { get; set; }
        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
        public string communicationPtName { get; set; }
        public string communicationVisitId { get; set; }
        public string communicationVisitDate { get; set; }
        public string patientmobile { get; set; }
        public string patientemailid { get; set; }
        public string customeremailid { get; set; }
        public string customermobile { get; set; }
        public string physicianemailid { get; set; }
        public string physicianmobile { get; set; }
        public bool isPatientReportSMS { get; set; }
        public bool isPatientReportEmail { get; set; }
        public bool isPatientReportWhatsapp { get; set; }
        public bool isCustomerReportWhatsapp { get; set; }
        public bool isCustomerReportEmail { get; set; }
        public bool isCustomerReportSMS { get; set; }
        public bool isPhysicianreportEmail { get; set; }
        public bool isPhysicianreportSms { get; set; }
        public bool isPhysicianreportWhatsapp { get; set; }
        public string emailIdToShow { get; set; }
        public string mobileNumberToShow { get; set; }
        //public string pageCount { get; set; }
    }
    public partial class lstreportorderlist
    {
        public bool ischecked { get; set; }
        public int orderno { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicecode { get; set; }
        public string servicename { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string colorcode { get; set; }
        public string barcodeno { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecall { get; set; }
        public bool iscancelled { get; set; }
        public string enteredby { get; set; }
        public string enteredon { get; set; }
        public string validatedby { get; set; }
        public string validatedon { get; set; }
        public string approvedby { get; set; }
        public string approvedon { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public bool issms { get; set; }
        public string smsdttm { get; set; }
        public bool isemail { get; set; }
        public string emaildttm { get; set; }
        public bool isprint { get; set; }
        public string printdttm { get; set; }
        public bool iscpreportshow { get; set; }
        public string cpreportshowdttm { get; set; }
        public bool iscpprint { get; set; }
        public string cpprintdttm { get; set; }
        public bool isremarks { get; set; }
        public bool iscpremarks { get; set; }
        public bool isservicestatus { get; set; }
        public bool istrand { get; set; }
        public bool iswsms { get; set; }
        public string whatsappdttm { get; set; }
    }
    public partial class PatientReportDTO
    {
        public string fullname { get; set; }
        public bool isheaderfooter { get; set; }
        public bool isNABLlogo { get; set; }
        public string orderlistnos { get; set; }
        public string pagecode { get; set; }
        public string patientvisitno { get; set; }
        public int process { get; set; }
        public string resulttypenos { get; set; }
        public int userno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public bool isdefault { get; set; }
        public int patientreportwithbill { get; set; }
        public bool isProvisional { get; set; }
        public int reportstatus { get; set; }
        public Int16 pritlanguagetype { get; set; }
        public int pageCount { get; set; }
    }
    public partial class PatientReportLog
    {
        public int ReportLogNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public int VisitTestNo { get; set; }
        public string? VisitTestType { get; set; }
        public string? LogType { get; set; }
        public int ReportUserNo { get; set; }
        public int UserType { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }

    public partial class PatientReportLogRespose
    {
        public int LogNo { get; set; }
        public string ReportLogDTTM { get; set; }
        public string LogType { get; set; }
        public string ReportUser { get; set; }
        public string UserType { get; set; }
        public DateTime LogDTTM { get; set; }
    }
    public class TempalteSearchResponse
    {
        public int Row_Num { get; set; }
        public string patientvisitno { get; set; }
        public string orderlistno { get; set; }
    }
    public class GetAuditReportReq
    {
        public string FROMDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public byte ATTypeNo { get; set; }
        public Int16 ATCatyNo { get; set; }
        public Int16 ATSubCatyNo { get; set; }
        public Int16 ATFieldNo { get; set; }
        public int UserNo { get; set; }
        public int PageIndex { get; set; }
        public int Patientvisitno { get; set; }
        public int pageCount { get; set; }
        public string searchTypeCode { get; set; } 
        public int searchTypeValue { get; set; }
    }
    public class GetAuditReportRes
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 LogNo { get; set; }
        public byte TypeNo { get; set; }
        public string ShortCode { get; set; }
        public string TypeDesc { get; set; }
        public Int16 CatyNo { get; set; }
        public string CatyDesc { get; set; }
        public Int16 SubCatyNo { get; set; }
        public string SubCatyDesc { get; set; }
        public Int16 FieldNo { get; set; }
        public string FieldDesc { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string OldTemplateValue { get; set; }
        public string NewTemplateValue { get; set; }
        public string OldTemplateUrl { get; set; }
        public string NewTemplateUrl { get; set; }
        public List<AuditTrailListValue> ListValue { get; set; }
        public string TranOn { get; set; }
        public int TranByNo { get; set; }
        public string TranByName { get; set; }
        public string Comments { get; set; }
        public int PatientNo { get; set; }
        public string PatientId { get; set; }
        public int PatientVisitNo { get; set; }
        public string CatyGroup { get; set; }
        public string EntityName { get; set; }
        public string TranDesc { get; set; }
        public Byte ResultTypeNo { get; set; }
        public string OutputFolderName { get; set; }
    }
    public class GetAuditTrailVisitReq
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Patientvisitno { get; set; }
        public int UserNo { get; set; }
    }
    public class GetAuditTrailVisitHistory
    {
        public int RowNo { get; set; }
        public string PtId { get; set; }
        public int PtNo { get; set; }
        public string PtName { get; set; }
        public string PtAgeType { get; set; }
        public string PtGender { get; set; }
        public string PtAgeGender { get; set; }
        public string PtMobileNo { get; set; }
        public string PtEmailId { get; set; }
        public string RefTypeDesc { get; set; }
        public string RefTypeName { get; set; }
        public string Physician { get; set; }
        public int PatientVisitNo { get; set; }
        public string LabAccessionNo { get; set; }
        public string RegDtTm { get; set; }
        public string IdNumber { get; set; }
        public string lstRegistration { get; set; }
        public string lstEditRegistration { get; set; }
        public string lstSampleCollection { get; set; }
        public string lstSampleAccession { get; set; }
        public string lstSampleRejection { get; set; }
        public string lstResultEntry { get; set; }
        public string lstResultEntrySecondReview { get; set; }
        public string lstRerunResult { get; set; }
        public string lstRecallResult { get; set; }
        public string lstResultValidation { get; set; }
        public string lstReportPrint { get; set; }
        public string lstCancelRegistration { get; set; }
        public string lstSendOut { get; set; }
    }
    public class AuditTrailVisitHistoryResponse
    {
        public int RowNo { get; set; }
        public string PtId { get; set; }
        public int PtNo { get; set; }
        public string PtName { get; set; }
        public string PtAgeType { get; set; }
        public string PtGender { get; set; }
        public string PtAgeGender { get; set; }
        public string PtMobileNo { get; set; }
        public string PtEmailId { get; set; }
        public string RefTypeDesc { get; set; }
        public string RefTypeName { get; set; }
        public string Physician { get; set; }
        public int PatientVisitNo { get; set; }
        public string LabAccessionNo { get; set; }
        public string RegDtTm { get; set; }
        public string IdNumber { get; set; }
        public List<AuditTrailRegistration> RegistrationList { get; set; }
        public List<AuditTrailEditRegistration> EditRegistrationList { get; set; }
        public List<AuditTrailSampleCollection> SampleCollectionList { get; set; }
        public List<AuditTrailSampleAccession> SampleAccessionList { get; set; }
        public List<AuditTrailSampleRejection> SampleRejectionList { get; set; }
        public List<AuditTrailResultEntry> ResultEntryList { get; set; }
        public List<AuditTrailResultSecondReview> ResultEntrySecondReview { get; set; }
        public List<AuditTrailRerunResult> RerunResultList { get; set; }
        public List<AuditTrailRecallResult> ResultRecallList { get; set; }
        public List<AuditTrailResultValidation> ResultValidationList { get; set; }
        public List<AuditTrailReportPrint> ReportPrintList { get; set; }
        public List<AuditTrailCancelRegistration> CancelRegistrationList { get; set; }
        public List<AuditTrailSendOut> SendOutList { get; set; }
    }
    public partial class AuditTrailRegistration
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Net { get; set; }
        public bool RecStatus { get; set; }
        public string RegisteredBy { get; set; }
        public string RegisteredOn { get; set; }
    }
    public partial class AuditTrailEditRegistration
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Net { get; set; }
        public string Remarks { get; set; }
        public bool RecStatus { get; set; }
        public string EditRegisteredBy { get; set; }
        public string EditRegisteredOn { get; set; }
    }
    public partial class AuditTrailCancelRegistration
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Net { get; set; }
        public bool RecStatus { get; set; }
        public string CancelledBy { get; set; }
        public string CancelledOn { get; set; }
        public string CancelledReason { get; set; }
    }
    public class AuditTrailSampleCollection
    {
        public Int64 RowNo { get; set; }
        public int SpecimenNo { get; set; }
        public string SpecimenName { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string BarcodeNo { get; set; }
        public string CollectedBy { get; set; }
        public string CollectedOn { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public bool RecStatus { get; set; }
        public int SpecimenQty { get; set; }
        public string SampleSource { get; set; }
        public int SampleSourceNo { get; set; }
        public string ActionDesc { get; set; }
    }
    public class AuditTrailSampleAccession
    {
        public Int64 RowNo { get; set; }
        public int SpecimenNo { get; set; }
        public string SpecimenName { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string BarcodeNo { get; set; }
        public string AccessionBy { get; set; }
        public string AccessionOn { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailSampleRejection
    {
        public Int64 RowNo { get; set; }
        public int SpecimenNo { get; set; }
        public string SpecimenName { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string BarcodeNo { get; set; }
        public string ActionBy { get; set; }
        public string ActionOn { get; set; }
        public string ActionDesc { get; set; }
        public string ActionReason { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailResultEntry
    {
        public Int64 RowNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string EnteredBy { get; set; }
        public string EnteredOn { get; set; }
        public bool RecStatus { get; set; }
        public string ResultComments { get; set; }
        public string Result { get; set; }
        public string ActionDesc { get; set; }
    }
    public class AuditTrailResultSecondReview
    {
        public Int64 RowNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewedOn { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailRerunResult
    {
        public Int64 RowNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string RerunBy { get; set; }
        public string RerunOn { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailRecallResult
    {
        public Int64 RowNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string RecalledBy { get; set; }
        public string RecalledOn { get; set; }
        public string Comments { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailResultValidation
    {
        public Int64 RowNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public string ValidatedBy { get; set; }
        public string ValidatedOn { get; set; }
        public bool RecStatus { get; set; }
        public string Result { get; set; }
        public string ResultComments { get; set; }
        public string ActionDesc { get; set; }
    }
    public class AuditTrailReportPrint
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string DispatchedBy { get; set; }
        public string DispatchedOn { get; set; }
        public bool RecStatus { get; set; }
        public string ActionDesc { get; set; }
    }
    public class AuditTrailSendOut
    {
        public Int64 RowNo { get; set; }
        public string VendorName { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string TranNo { get; set; }
        public string TranOn { get; set; }
        public string TranBy { get; set; }
        public string ExpectedOn { get; set; }
        public string OutSourceComments { get; set; }
        public bool RecStatus { get; set; }
    }
    public class AuditTrailListValue
    {
        public Int64 RowNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool RecStatus { get; set; }
    }
    public class FetchAuditReportRes
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public Int64 RowNum { get; set; }
        public Int64 LogNo { get; set; }
        public byte TypeNo { get; set; }
        public string ShortCode { get; set; }
        public string TypeDesc { get; set; }
        public Int16 CatyNo { get; set; }
        public string CatyDesc { get; set; }
        public Int16 SubCatyNo { get; set; }
        public string SubCatyDesc { get; set; }
        public Int16 FieldNo { get; set; }
        public string FieldDesc { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string OldTemplateValue { get; set; }
        public string NewTemplateValue { get; set; }
        public string OldTemplateUrl { get; set; }
        public string NewTemplateUrl { get; set; }
        public string ListValue { get; set; }
        public string TranOn { get; set; }
        public int TranByNo { get; set; }
        public string TranByName { get; set; }
        public string Comments { get; set; }
        public int PatientNo { get; set; }
        public string PatientId { get; set; }
        public int PatientVisitNo { get; set; }
        public string CatyGroup { get; set; }
        public string EntityName { get; set; }
        public string TranDesc { get; set; }
        public Byte ResultTypeNo { get; set; }
        public string OutputFolderName { get; set; }
    }
    public partial class lstamendedpatientreport
    {
        public Int16 maindeptNo { get; set; }
        public bool ischecked { get; set; }
        public string rhNo { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public int patientno { get; set; }
        public string fullname { get; set; }
        public string agegender { get; set; }
        public bool ispatientimage { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public int refferraltypeno { get; set; }
        public string referraltype { get; set; }
        public int customerno { get; set; }
        public string customername { get; set; }
        public bool islabheader { get; set; }
        public bool isreportblocked { get; set; }
        public bool isinternotes { get; set; }
        public int physicianno { get; set; }
        public string physicianname { get; set; }
        public bool isstat { get; set; }
        public string rctdttm { get; set; }
        public string modeofdispatch { get; set; }
        public bool isvisitstatus { get; set; }
        public string taskdttm { get; set; }
        public bool isdue { get; set; }
        public int cancelled { get; set; }
        public bool visabnormal { get; set; }
        public bool viscritical { get; set; }
        public bool vistat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        public bool visremarks { get; set; }
        public bool viscpremarks { get; set; }
        public Int32 TotalRecords { get; set; }
        public List<lstamendedreportorderlist> lstamendedreportorderlist { get; set; }
        public int DuePrint { get; set; }
        public bool Deliverymodes { get; set; }
        public string VenueBranchName { get; set; }
        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
        public string? amendmentcode { get; set; }
        public int amendmentno { get; set; }
        public string? amendmenton { get; set; }
        public string? amendmentby { get; set; }
        public string patientmobile { get; set; }
        public string patientemailid { get; set; }
        public string customeremailid { get; set; }
        public string customermobile { get; set; }
        public string physicianemailid { get; set; }
        public string physicianmobile { get; set; }
        public bool isPatientReportSMS { get; set; }
        public bool isPatientReportEmail { get; set; }
        public bool isPatientReportWhatsapp { get; set; }
        public bool isCustomerReportWhatsapp { get; set; }
        public bool isCustomerReportEmail { get; set; }
        public bool isCustomerReportSMS { get; set; }
        public bool isPhysicianreportEmail { get; set; }
        public bool isPhysicianreportSms { get; set; }
        public bool isPhysicianreportWhatsapp { get; set; }
        public string emailIdToShow { get; set; }
        public string mobileNumberToShow { get; set; }
    }
    public partial class requestamendedpatientreport
    {
        public Int16 maindeptNo { get; set; }
        public string pagecode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int viewvenuebranchno { get; set; }
        public int pageindex { get; set; }
        public string type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int patientno { get; set; }
        public int patientvisitno { get; set; }
        public int deptno { get; set; }
        public int serviceno { get; set; }
        public string servicetype { get; set; }
        public int refferraltypeno { get; set; }
        public int customerno { get; set; }
        public int physicianno { get; set; }
        public int riderno { get; set; }
        public int excutiveno { get; set; }
        public bool isstat { get; set; }
        public bool isdue { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        //public int pageIndex { get; set; }
        public int loginType { get; set; }
        public int routeNo { get; set; }
        public int deliverymode { get; set; }
        public int wardNo { get; set; }
        public string searchKey { get; set; }
    }

    public partial class lstamendedpatientreportdbl
    {
        public int rowno { get; set; }
        public int patientno { get; set; }
        public string rhNo { get; set; }
        public int patientvisitno { get; set; }
        public string patientid { get; set; }
        public string fullname { get; set; }
        public string agegender { get; set; }
        public bool ispatientimage { get; set; }
        public string visitid { get; set; }
        public string extenalvisitid { get; set; }
        public string visitdttm { get; set; }
        public int refferraltypeno { get; set; }
        public string referraltype { get; set; }
        public int customerno { get; set; }
        public string customername { get; set; }
        public bool islabheader { get; set; }
        public bool isreportblocked { get; set; }
        public bool isinternotes { get; set; }
        public int physicianno { get; set; }
        public string physicianname { get; set; }
        public bool isstat { get; set; }
        public string rctdttm { get; set; }
        public string modeofdispatch { get; set; }
        public bool isvisitstatus { get; set; }
        public string taskdttm { get; set; }
        public bool isdue { get; set; }
        public int cancelled { get; set; }
        public bool visabnormal { get; set; }
        public bool viscritical { get; set; }
        public bool vistat { get; set; }
        public int orderstatus { get; set; }
        public int printstatus { get; set; }
        public int cpprintstatus { get; set; }
        public int smsstatus { get; set; }
        public int emailstatus { get; set; }
        public bool visremarks { get; set; }
        public bool viscpremarks { get; set; }
        public int orderno { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicecode { get; set; }
        public string servicename { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string colorcode { get; set; }
        public string barcodeno { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecall { get; set; }
        public bool iscancelled { get; set; }
        public string enteredby { get; set; }
        public string enteredon { get; set; }
        public string validatedby { get; set; }
        public string validatedon { get; set; }
        public string approvedby { get; set; }
        public string approvedon { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public bool issms { get; set; }
        public string smsdttm { get; set; }
        public bool isemail { get; set; }
        public string emaildttm { get; set; }
        public bool isprint { get; set; }
        public string printdttm { get; set; }
        public bool iscpreportshow { get; set; }
        public string cpreportshowdttm { get; set; }
        public bool iscpprint { get; set; }
        public string cpprintdttm { get; set; }
        public bool isremarks { get; set; }
        public bool iscpremarks { get; set; }
        public bool isservicestatus { get; set; }
        public Int32 TotalRecords { get; set; }
        public int DuePrint { get; set; }
        public bool Deliverymodes { get; set; }
        public string VenueBranchName { get; set; }
        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
        public bool istrand { get; set; }
        public string? amendmentcode { get; set; }
        public int amendmentno { get; set; }
        public string? amendmenton { get; set; }
        public string? amendmentby { get; set; }
        public string? amendmentreason { get; set; }
        public string patientmobile { get; set; }
        public string patientemailid { get; set; }
        public string customeremailid { get; set; }
        public string customermobile { get; set; }
        public string physicianemailid { get; set; }
        public string physicianmobile { get; set; }
        public bool isPatientReportSMS { get; set; }
        public bool isPatientReportEmail { get; set; }
        public bool isPatientReportWhatsapp { get; set; }
        public bool isCustomerReportWhatsapp { get; set; }
        public bool isCustomerReportEmail { get; set; }
        public bool isCustomerReportSMS { get; set; }
        public bool isPhysicianreportEmail { get; set; }
        public bool isPhysicianreportSms { get; set; }
        public bool isPhysicianreportWhatsapp { get; set; }
        public string emailIdToShow { get; set; }
        public string mobileNumberToShow { get; set; }
    }
    public partial class lstamendedreportorderlist
    {
        public bool ischecked { get; set; }
        public int orderno { get; set; }
        public int orderlistno { get; set; }
        public string servicetype { get; set; }
        public int serviceno { get; set; }
        public string servicecode { get; set; }
        public string servicename { get; set; }
        public int departmentno { get; set; }
        public string departmentname { get; set; }
        public int resulttypeno { get; set; }
        public int orderliststatus { get; set; }
        public string orderliststatustext { get; set; }
        public string colorcode { get; set; }
        public string barcodeno { get; set; }
        public bool isrecollect { get; set; }
        public bool isrecall { get; set; }
        public bool iscancelled { get; set; }
        public string enteredby { get; set; }
        public string enteredon { get; set; }
        public string validatedby { get; set; }
        public string validatedon { get; set; }
        public string approvedby { get; set; }
        public string approvedon { get; set; }
        public bool isabnormal { get; set; }
        public bool iscritical { get; set; }
        public bool istat { get; set; }
        public bool issms { get; set; }
        public string smsdttm { get; set; }
        public bool isemail { get; set; }
        public string emaildttm { get; set; }
        public bool isprint { get; set; }
        public string printdttm { get; set; }
        public bool iscpreportshow { get; set; }
        public string cpreportshowdttm { get; set; }
        public bool iscpprint { get; set; }
        public string cpprintdttm { get; set; }
        public bool isremarks { get; set; }
        public bool iscpremarks { get; set; }
        public bool isservicestatus { get; set; }
        public bool istrand { get; set; }
        public string? amendmentreason { get; set; }
    }

    public partial class AmendedPatientReportDTO
    {
        public string fullname { get; set; }
        public bool isheaderfooter { get; set; }
        public bool isNABLlogo { get; set; }
        public string orderlistnos { get; set; }
        public string pagecode { get; set; }
        public string patientvisitno { get; set; }
        public int process { get; set; }
        public string resulttypenos { get; set; }
        public int userno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public bool isdefault { get; set; }
        public int patientreportwithbill { get; set; }
        public bool isProvisional { get; set; }
        public int reportstatus { get; set; }
        public Int16 pritlanguagetype { get; set; }
        public int amendmentno { get; set; }
    }
    public class GetATSubCatyMasterSearchReq
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string pageCode { get; set; }
        public string searchByCode { get; set; }
        public string searchByText { get; set; }
    }
    public class GetATSubCatyMasterSearchResponse
    {
        public int rowNo { get; set; }
        public string fieldCode { get; set; }
        public int fieldValue { get; set; }
        public string fieldName { get; set; }
    }

    public partial class PatientReportOPDDTO
    {
        public string? AppointmentNo { get; set; }
        public string? AppointmentDate { get; set; }
        public int PhysicianNo { get; set; }
        public string? OutputTypeNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsHeaderFooter { get; set; }
        public int Process { get; set; }
    }
}
