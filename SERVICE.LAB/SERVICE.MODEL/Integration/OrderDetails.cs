using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.Model.Integration
{

    public class WaitingListMessage : PatientNotifyLog
    {
        public Int16 OrderId { get; set; }
    }

    public class EditPatientDetailsRequest
    {
        public int OrderId { get; set; }
        public int VisitNo { get; set; }
    }
    public class waitinglistrequest
    {
        public string NRICNumber { get; set; }
        public int MassRegistrationNo { get; set; }
        public string ClinicOrClientName { get; set; }
        public string CompanyName { get; set; }
        public string PatientName { get; set; }
        public int SampleTypeId { get; set; }
        public int SourceSystemId { get; set; }
        public int TestId { get; set; }
        public int GroupId { get; set; }
        public int PackageId { get; set; }
        public int DepartmentId { get; set; }
        public int MainDepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CaseOrVisitId { get; set; }

        public bool IsMassRegistration { get; set; }
        public string ReasonCode { get; set; }
        public bool DowntimeOrders { get; set; }
        public string BarCode { get; set; }
        public long OrderId { get; set; }

    }
    public class TestValidation
    {
        public Int64 RowNo { get; set; }
        public int TestId { get; set; }
        public int OrderId { get; set; }
        public string Gender { get; set; }
        public string ServiceType { get; set; }
    }
    public class TestDetailsRetrieval
    {
        public string Code { get; set; }
        public string TestType { get; set; }
    }

    public class TestAdditionalInformation
    {
        public Int64 ID { get; set; }
        public string Details { get; set; }
        public string DataType { get; set; }
    }

    public class GroupResponse
    {
        public int RowNo { get; set; }
        public int TestNo { get; set; }
        public string TestShortName { get; set; }
        public string TestName { get; set; }
        public string TestDisplayName { get; set; }
        public string DepartmentName { get; set; }
        public string MethodName { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public string UnitName { get; set; }
        public int TsequenceNo { get; set; }
        public string ResultType { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TestCode { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public decimal GroupRate { get; set; }
        public string GroupCode { get; set; }
        public string GroupDisplayName { get; set; }
        public string GroupShortName { get; set; }
    }
    public class TestResponse
    {
        public int RowNo { get; set; }
        public int TestNo { get; set; }
        public string TestShortName { get; set; }
        public string TestName { get; set; }
        public string TestDisplayName { get; set; }
        public string DepartmentName { get; set; }
        public string MethodName { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public string UnitName { get; set; }
        public int TsequenceNo { get; set; }
        public string ResultType { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TestCode { get; set; }
    }
    public class TestInformation
    {
        public Int64 RowNo { get; set; }
        public int TestNo { get; set; }
        public string TestShortName { get; set; }
        public string TestCode { get; set; }
        public string TestDisplayName { get; set; }
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string DepartmentName { get; set; }
        public string SampleName { get; set; }
        public decimal Rate { get; set; }
        public string TestType { get; set; }
        public Int32 DepartmentNo { get; set; }
        public Int32 MainDeptNo { get; set; }

    }

    public class OnHoldRegistrationRequest
    {
        public List<int> OrderId { get; set; }
        public string ReasonCode { get; set; }
    }

    public class AddTestRequest
    {
        public long OrderId { get; set; }
        public List<IntegrationOrderTestDetailsResponse> Test { get; set; }
    }
    public class WaitingListSaveRequest
    {
        public List<WaitingListCreateManageSampleRequest> manageSamples { get; set; }
        public List<WaitingListCreateManageSampleResponse> Response { get; set; }
        public bool IsMassRegistration { get; set; }
        public bool IsDowntimeOrderRegistration { get; set;  }
        public List<OrderSaveRequest> Orders { get; set; }
    }
    public class TubeDetails
    {
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public int TubeCount { get; set; }
    }
    public class OrderSaveRequest
    {
        public int OrderId { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsVip { get; set; }
        public bool IsStat { get; set; }
        public int GenderId { get; set; }
        public bool IsFasting { get; set; }
        public int VisitId { get; set;  }
        public bool PatientDetailsUpdated { get; set; }
        public List<TubeDetails> TubeDetails { get; set; }
    }
    public class WaitingListCreateManageSampleResponse : CreateManageSampleResponse
    {
        public long OrderId { get; set; } = 0;
    }
    public class WaitingListCreateManageSampleRequest : CreateManageSampleRequest
    {
        public int OrderId { get; set; }
        public string TestCode { get; set; }
        public bool IsRejected { get; set; }
        public bool IsOnHold { get; set; }
        public int IntegrationOrderId { get; set; }
        public int PackageId { get; set; }
        public bool IsFasting { get; set; }
        public string LabAccessionNo { get; set; }
        public string BBLabAccessionNo { get; set; }
        public string DowntimeLabAccessionNo { get; set; }
        public string ServiceType { get; set; }
        public string Gender { get; set;  }
        public string BarcodeShortNames { get; set; }
        public string SampleName { get; set;  }
        public string ContainerName { get; set; }
        public List<int> SelectedTestsIds { get; set; }

    }

    public class TestOrderDetails
    {
        public string Code { get; set; }
        public int Quantity { get; set; } = 1;
    }


    public class OrderDetails
    {
        public int OrderDetailsNo { get; set; }
        public int OrderListNo { get; set; }
        public int PatientVisiNo { get; set; }
        public string TestType { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int TSeqNo { get; set; }
        public bool IsTFormula { get; set; }
        public int? SubTestNo { get; set; }
        public string? SubTestName { get; set; }
        public int? SSeqNo { get; set; }
        public bool IsSFormula { get; set; }
        public int? MethodNo { get; set; }
        public string? MethodName { get; set; }
        public int? UnitNo { get; set; }
        public string? UnitName { get; set; }
        public int ProcessingBranchNo { get; set; }
        public string? LLColumn { get; set; }
        public string? HLColumn { get; set; }
        public string? DisplayRR { get; set; }
        public string? CRLLColumn { get; set; }
        public string? CRHLColumn { get; set; }
        public string? DisplayCRRR { get; set; }
        public string? MinRange { get; set; }
        public string? MaxRange { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool IsInterface { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string? IntegrationID { get; set; }
        public string? IntegrationCode { get; set; }
        public string ResultType { get; set; }
        public bool IsNonMandatory { get; set; }
        public bool IsDelta { get; set; }
        public int? DecimalPoint { get; set; }
        public bool IsRoundOff { get; set; }
        public bool IsFormulaParameter { get; set; }
        public int? FormulaServiceNo { get; set; }
        public string? FormulaServiceType { get; set; }
        public bool IsEdit { get; set; }
        public int HeaderNo { get; set; }
        public bool IsInterNotes { get; set; }
        public bool IsInterNotesEdit { get; set; }
        public int TestInter { get; set; }
        public bool? IsIndiviual { get; set; }
        public bool? IsNABL { get; set; }
        public string? HeaderName { get; set; }
        public bool IsInterfacePicked { get; set; }
        public DateTime? IsInterfacePickedDTTM { get; set; }
        public bool IsReRun { get; set; }
        public bool? IsUploadOption { get; set; }
        public string? UploadedFile { get; set; }
        public int? IsMultiEditor { get; set; }
        public string? DepCode { get; set; }
        public bool Isautoapproval { get; set; }
        public string? LanguageText { get; set; }
        public string? SubTestLanguageText { get; set; }
        public int? LesserValue { get; set; }
        public int? GreaterValue { get; set; }
        public bool? IsSensitiveData { get; set; }
        public bool? IsExtraSubTest { get; set; }
        public string? SnomedCode { get; set; }
        public int? SnomedCodeId { get; set; }
        public int? SubTestDepartmentNo { get; set; }
    }

    public class OrderList
    {
        public int OrderListNo { get; set; }
        public int OrderNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string TestType { get; set; }
        public int TestNo { get; set; }
        public int SequenceNo { get; set; }
        public int DepartmentNo { get; set; }
        public bool IsSample { get; set; }
        public int? SampleNo { get; set; }
        public int? ContainerNo { get; set; }
        public bool IsConsentForm { get; set; }
        public bool IsStat { get; set; }
        public int? ProcessingMin { get; set; }
        public DateTime? TATDTTM { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string? IntegrationID { get; set; }
        public string? IntegrationCode { get; set; }
        public int? PackageNo { get; set; }
        public int GroupInter { get; set; }
        public bool? IsSecondReview { get; set; }
        public bool? IsSampleAct { get; set; }
    }
    public class OrderTransaction
    {
        public int OrderTransactionNo { get; set; }
        public int OrderNo { get; set; }
        public int OrderListNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int SequenceNo { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public bool IsSample { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public bool IsConsentForm { get; set; }
        public bool IsStat { get; set; }
        public int? ProcessingMin { get; set; }
        public DateTime? TATDTTM { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? PackageNo { get; set; }
        public string? PackageName { get; set; }
        public string ODJson { get; set; }
        public int DepartmentSeqNo { get; set; }
        public int ResultTypeNo { get; set; }
        public bool IsNoResult { get; set; }
        public int GroupInter { get; set; }
        public int IsMultiEditor { get; set; }
        public bool? IsHigTemprature { get; set; }
        public bool? IsBarcodeNotReq { get; set; }
        public string? HigTempValue { get; set; }
        public bool? Collectatsource { get; set; }
        public bool? IsSecondReview { get; set; }
        public int? SecondReviewReqBy { get; set; }
        public bool? IsPrint { get; set; }
        public int? WorklistNo { get; set; }
    }
    public class waitinglistresponse
    {
        public List<IntegrationOrderDetailsResponse> Response { get; set; }
        public List<WaitingListMessage> Messages { get; set; }
    }
    public class OrderDetailsResponse : FrontOffficeResponse
    {
        public string BBLabAccesionNumber { get; set; }
        public int BBOrderId { get; set; }
    }

    public class IntegrationOrderDetailsResponse : IntegrationOrderDetailsBase
    {
        public IntegrationOrderVisitDetailsResponse IntegrationOrderVisitDetails { get; set; }
        public IntegrationOrderPatientDetailsResponse IntegrationOrderPatientDetails { get; set; }
        public IntegrationOrderClientDetailsResponse IntegrationOrderClientDetails { get; set; }
        public IntegrationOrderDoctorDetailsResponse IntegrationOrderDoctorDetails { get; set; }
        public List<IntegrationOrderTestDetailsResponse> IntegrationOrderTestDetails { get; set; }
        public IntegrationOrderWardDetailsResponse IntegrationOrderWardDetails { get; set; }
        public List<IntegrationOrderAllergyDetailsResponse> IntegrationOrderAllergyDetails { get; set; }

    }
    public class IntegrationOrderVisitDetailsResponse : IntegrationOrderVisitDetailsBase
    {

    }

    public class IntegrationOrderPatientDetailsResponse : IntegrationOrderPatientDetailsBase
    {

    }

    public class IntegrationOrderClientDetailsResponse : IntegrationOrderClientDetailsBase
    {

    }

    public class IntegrationOrderDoctorDetailsResponse : IntegrationOrderDoctorDetailsBase
    {

    }

    public class IntegrationOrderTestDetailsResponse : IntegrationOrderTestDetailsBase
    {
        public string ContainerName { get; set; }
        public string SampleName { get; set; }
        public string PackageName { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set;  }
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
    }
    public class IntegrationOrderWardDetailsResponse : IntegrationOrderWardDetailsBase
    {

    }
    public class IntegrationOrderAllergyDetailsResponse : IntegrationOrderAllergyDetailsBase
    {

    }
    public class IntegrationOrderDetailsBase
    {
        public Int16 OrderId { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public string Orders { get; set; }
        public string SourceSystem { get; set; }
        public string LabOrderId { get; set; }
        public bool isDowntimeOrder { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }
        public Int64? PatientVisitNo { get; set; }

        public string? ReferenceNo { get; set; }
        public string? LabAccessionNo { get; set; }
        public string? VisitNo { get; set; }
        public string? CaseNumber { get; set; }
        public string? HoldReason { get; set; }
        public string? DownTimeLabAccessionNo { get; set; }
        public string? BBLabAccessionNo { get; set; }
        public string? Messages { get; set; }

    }
    public class IntegrationOrderDetails : IntegrationOrderDetailsBase
    {

        public IntegrationOrderVisitDetails IntegrationOrderVisitDetails { get; set; }
        public IntegrationOrderPatientDetails IntegrationOrderPatientDetails { get; set; }
        public IntegrationOrderClientDetails IntegrationOrderClientDetails { get; set; }
        public IntegrationOrderDoctorDetails IntegrationOrderDoctorDetails { get; set; }
        public List<IntegrationOrderTestDetails> IntegrationOrderTestDetails { get; set; }
        public IntegrationOrderWardDetails IntegrationOrderWardDetails { get; set; }
        public List<IntegrationOrderAllergyDetails> IntegrationOrderAllergyDetails { get; set; }

    }
    public class IntegrationOrderVisitDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string? visitno { get; set; }
        public string? casenumber { get; set; }
        public string idtype { get; set; }
        public string? idnumber { get; set; }
        public string sourcerequestId { get; set; }
        public DateTime registrationdttm { get; set; }
        public string rccasenumber { get; set; }
        public string? rcordernumber { get; set; }
        public string? contractcode { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }
    }
    public class IntegrationOrderVisitDetails : IntegrationOrderVisitDetailsBase
    {
        public IntegrationOrderDetails Order { get; set; }
    }
    public class IntegrationOrderPatientDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string patientid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string? alternateIdtype { get; set; }
        public string? alternateIdnumber { get; set; }
        public string gender { get; set; }
        public DateTime dateofbirth { get; set; }
        public bool isVIP { get; set; }
        public string? diagnosiscode { get; set; }
        public string? diagnosisdescription { get; set; }
        public string? transfusionindication { get; set; }
        public string? height { get; set; }
        public string? weight { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }

    }
    public class IntegrationOrderPatientDetails : IntegrationOrderPatientDetailsBase
    {
        public IntegrationOrderDetails Order { get; set; }
    }
    public class IntegrationOrderClientDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string? clientcode { get; set; }
        public string? clientname { get; set; }
        public string? cliniccode { get; set; }
        public string? clinicname { get; set; }
        public string institutioncode { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }

    }
    public class IntegrationOrderClientDetails : IntegrationOrderClientDetailsBase
    {

        public IntegrationOrderDetails Order { get; set; }
    }
    public class IntegrationOrderDoctorDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string? doctormcr { get; set; }
        public string? doctorname { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }

    }

    public class IntegrationOrderDoctorDetails : IntegrationOrderDoctorDetailsBase
    {
        public IntegrationOrderDetails Order { get; set; }
    }
    public class IntegrationOrderTestDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string code { get; set; }
        public string itemtype { get; set; }
        public string description { get; set; }
        public string? quantity { get; set; }
        public string? natureofrequest { get; set; }
        public string? natureofspecimen { get; set; }
        public DateTime collectiondttm { get; set; }
        public string? sourceofspecimen { get; set; }
        public string? remarks { get; set; }
        public DateTime? createddttm { get; set; }
        public string? barcodenumber { get; set; }
        public bool? Status { get; set; }
        public int SampleTypeId { get; set; }
        public int TestId { get; set; }
        public int GroupId { get; set; }
        public int PackageId { get; set; }
        public int DepartmentId { get; set; }
        public int MainDepartmentId { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }
        public bool? IsRejected { get; set; }
        public string? RejectedReason { get; set; }
        public bool? IsOnHold { get; set; }
        public bool? IsNotGiven {get;set;}
        public string? RejectedReasonDesc { get; set; }
        public string? LabAccessionNo { get; set; }
        public Int64? PatientVisitNo { get; set; } = 0;

    }
    public class IntegrationOrderTestDetails : IntegrationOrderTestDetailsBase
    {
        public IntegrationOrderDetails Order { get; set; }
    }

    public class IntegrationOrderWardDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public string? nursingOU { get; set; }
        public string? room { get; set; }
        public string? bed { get; set; }
        public string? ward { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }

    }
    public class PatientTransactions
    {
        public int PatientTransactionNo { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string PatientID { get; set; }
        public string FullName { get; set; }
        public string AgeType { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string URNID { get; set; }
        public string URNType { get; set; }
        public bool IsPatientImage { get; set; }
        public bool IsPatientSMS { get; set; }
        public bool IsPatientEmail { get; set; }
        public string VisitID { get; set; }
        public string ExtenalVisitID { get; set; }
        public DateTime VisitDTTM { get; set; }
        public int RefferralTypeNo { get; set; }
        public int? CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobileNumber { get; set; }
        public bool IsCustomerReportEmail { get; set; }
        public bool IsCustomerReportSMS { get; set; }
        public bool IsLabHeader { get; set; }
        public bool IsReportBlocked { get; set; }
        public bool IsInterNotes { get; set; }
        public int? PhysicianNo { get; set; }
        public string PhysicianName { get; set; }
        public string PhysicianQualification { get; set; }
        public string PhysicianEmail { get; set; }
        public string PhysicianMobileNumber { get; set; }
        public bool IsphysicianReportEmail { get; set; }
        public bool IsphysicianReportSMS { get; set; }
        public int? RiderNo { get; set; }
        public string RiderName { get; set; }
        public int? ExcutiveNo { get; set; }
        public string ExcutiveName { get; set; }
        public bool IsStat { get; set; }
        public DateTime? RCTDTTM { get; set; }
        public string ClinicalHistory { get; set; }
        public string ID { get; set; }
        public string IDType { get; set; }
        public string ModeofDispatch { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public string CreatedFrom { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ReferralType { get; set; }
        public int? MarketingNo { get; set; }
        public string MarketingName { get; set; }
        public int InvoiceNo { get; set; }
        public int InvoiceStatus { get; set; }
        public string InvoiceStatusText { get; set; }
        public string VaccinationType { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public int? RouteNo { get; set; }
        public bool? IsSelf { get; set; }
        public string NURNID { get; set; }
        public string NURNType { get; set; }
        public bool? Deliverymode { get; set; }
        public string ExternalPatientId { get; set; }
        public bool? IsOverAllTestApproved { get; set; }
        public int? WardNo { get; set; }
        public string WardName { get; set; }
        public string OPIPNumber { get; set; }
        public bool? IsFranchise { get; set; }
        public string PhysicianWhatsAppNo { get; set; }
        public bool IsPhysicianReportWhatsAppNo { get; set; }
        public bool IsAutoWhatsApp { get; set; }
        public string NRICNumber { get; set; }
        public bool? DietaryNo { get; set; }
        public bool? IsFasting { get; set; }
        public bool? IsDocuments { get; set; }
    }

    public class IntegrationOrderWardDetails : IntegrationOrderWardDetailsBase
    {

        public IntegrationOrderDetails Order { get; set; }
    }
    public class IntegrationOrderAllergyDetailsBase
    {
        public Int16 Id { get; set; }
        public Int16? VenueNo { get; set; }
        public Int16? VenueBranchNo { get; set; }
        public Int16 OrderID { get; set; }
        public bool IsAllergy { get; set; }
        public string? Allergy { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int32? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Int32? ModifiedBy { get; set; }

    }
    public class IntegrationOrderAllergyDetails : IntegrationOrderAllergyDetailsBase
    {
        public IntegrationOrderDetails Order { get; set; }
    }

    public class UpdateMassRegistrationRequest
    {
        public int OrderId { get; set; }
        public int VisitNo { get; set; }
        public string LabAccessionNumber { get; set; }
    }

    public class orderrequestdetails
    {
        [Required]
        public nsourcesystem sourcesystem { get; set; }
        public visitdetails visitdetails { get; set; }
        public patientdetails patientdetails { get; set; }
        public clientdetails clientdetails { get; set; }
        public doctordetails doctordetails { get; set; }
        public labdetails labdetails { get; set; }
        public warddetails warddetails { get; set; }
        public List<allergydetails> allergydetails { get; set; }
    }

    public class orderrespondetails
    {
        public string referenceno { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }
    public class massregistrationresponse
    {
        public List<massregistration> massregistrations { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }

    public class MassRegistrationList
    {
        public string? PatientName { get; set; }
        public int? PatientVisitNo { get; set; }
        public string? LabAccessionNo { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public bool? Status { get; set; }
        public bool? SampleStatus { get; set; }
        public int MassRegistrationSampleNo { get; set; }
        public int? MassRegistrationNo { get; set; }
        public int? ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int SampleId { get; set; }
        public int TestId { get; set; }
        public int GroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PackageId { get; set; }
        public int DepartmentId { get; set; }
        public int MainDeptId { get; set; }
        public int PackageNo { get; set; }
        public int ClientNo { get; set; }
        public string? email { get; set; }
        public string? Contact { get; set; }
        public string? Street { get; set; }
        public string? Block { get; set; }
        public string? BuildinName { get; set; }
        public string? PostalCode { get; set; }
        public string? Alternate_email { get; set; }
        public string? Nationality { get; set; }
        public string? BarCodeNo { get; set; }
        public int? CustomerNo { get; set; }
        public int? PhysicianNo { get; set; }
    }

    public class MassRegistration
    {
        public int MassRegistrationNo { get; set; }
        public int? MassFileNo { get; set; }
        public string? PatientName { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public bool? Status { get; set; }
        public Int16? VenueNo { get; set; }
        public int? VenueBranchNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? PatientVisitNo { get; set; }
        public string? LabAccessionNo { get; set; }
        public string? email { get; set; }
        public string? Contact { get; set; }
        public string? Street { get; set; }
        public string? Block { get; set; }
        public string? BuildinName { get; set; }
        public string? PostalCode { get; set; }
        public string? Alternate_email { get; set; }
        public string? Nationality { get; set; }

    }

    public class MassRegistrationSample
    {
        public int MassRegistrationSampleNo { get; set; }
        public int MassRegistrationNo { get; set; }
        public int? MassFileNo { get; set; }
        public int? ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public int? SampleNo { get; set; }
        public int? ContainerNo { get; set; }
        public string? BarCodeNo { get; set; }
        public bool? Status { get; set; }
        public Int16? VenueNo { get; set; }
        public int? VenueBranchNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }

    public class massregistration
    {
        public string referenceno { get; set; }
        public patientdetails patientdetails { get; set; }
    }
    public class orderresponse
    {
        public string referenceno { get; set; }
    }
    public class visitdetails
    {
        //[RequiredRCMS]
        [MaxLength(20)]
        public string visitno { get; set; }

        //[RequiredEMR]
        [MaxLength(20)]
        public string casenumber { get; set; }
        [Required]
        [MaxLength(2)]
        public string idtype { get; set; }
        [Required]
        [MaxLength(20)]
        public string idnumber { get; set; }        
        public DateTime registrationdttm { get; set; }

        [Required]
        [MaxLength(20)]
        public string sourcerequestId { get; set; }
        [MaxLength(20)]
        public string rccasenumber { get; set; }
        [MaxLength(20)]
        public string rcordernumber { get; set; }
        [MaxLength(20)]
        public string contractcode { get; set; }
        [MaxLength(50)]
        public string lisreferenceno { get; set; }

    }
    public class clientdetails
    {
        [Required]
        [MaxLength(15)]
        public string clientcode { get; set; }

        [Required]
        [MaxLength(50)]
        public string clientname { get; set; }

        [Required]
        [MaxLength(15)]
        public string cliniccode { get; set; }

        [Required]
        [MaxLength(50)]
        public string clinicname { get; set; }
        //[RequiredEMR]
        [MaxLength(4)]
        public string institutioncode { get; set; }

    }
    public class doctordetails
    {
        [Required]
        [MaxLength(10)]
        public string doctormcr { get; set; }
        [Required]
        [MaxLength(60)]
        public string doctorname { get; set; }
    }
    public class patientdetails
    {
        [Required]
        [MaxLength(20)]
        public string patientid { get; set; }
        [Required]
        [MaxLength(80)]
        public string firstname { get; set; }
        
        [MaxLength(80)]
        public string lastname { get; set; }
        [MaxLength(20)]
        public string alternateIdtype { get; set; }
        [MaxLength(20)]
        public string alternateIdnumber { get; set; }

        [Required]
        public ngender gender { get; set; }
        [Required]
        public DateTime dateofbirth { get; set; }
        public bool isVIP { get; set; }
        [MaxLength(120)]
        public string diagnosiscode { get; set; }
        [MaxLength(3200)]
        public string diagnosisdescription { get; set; }
        [MaxLength(100)]
        //[RequiredBB]
        public string transfusionindication { get; set; }
        [MaxLength(6)]
        public string height { get; set; }
        [MaxLength(6)]
        public string weight { get; set; }
        public string patientno { get; set; }

    }
    public class labdetails
    {
        public labdetails Clone()
        {
            return (labdetails)this.MemberwiseClone();
        }
        [Required]
        [MaxLength(10)]
        public string orderid { get; set; }

        public bool isDowntimeOrder { get; set; } = false;

        [Required]
        public List<laborderdetails> orderitems { get; set; }

    }
    public class laborderdetails
    {
        [Required]
        [MaxLength(10)]
        public string code { get; set; }
        [Required]
        public nitemtype itemtype { get; set; }
        [Required]
        [MaxLength(80)]
        public string description { get; set; }
        [Required]
        [MaxLength(2)]
        public string quantity { get; set; }
        [Required]
        public nnatureofrequest natureofrequest { get; set; }
        [Required]
        public nnatureofspecimen natureofspecimen { get; set; }
        [Required]
        public DateTime collectiondttm { get; set; }
        //[RequiredEMR]
        [MaxLength(80)]
        public string sourceofspecimen { get; set; }
        [MaxLength(100)]
        public string remarks { get; set; }
        public DateTime createddttm { get; set; }
        [Required]
        [MaxLength(20)]
        public string barcodenumber { get; set; }
    }
    public class allergydetails
    {
        public bool isallergy { get; set; }
        [MaxLength(200)]
        public string allergy { get; set; }
    }
    public class warddetails
    {
        //[RequiredEMR]
        [MaxLength(8)]
        public string nursingOU { get; set; }
        //[RequiredEMR]
        [MaxLength(8)]
        public string room { get; set; }
        //[RequiredEMR]
        [MaxLength(8)]
        public string bed { get; set; }
        //[RequiredEMR]
        [MaxLength(8)]
        public string ward { get; set; }

    }

}