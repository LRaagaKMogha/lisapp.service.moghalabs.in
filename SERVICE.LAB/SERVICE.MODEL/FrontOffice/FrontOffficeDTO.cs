using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class FrontOffficeDTO
    {
        public Int64 PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public int ageYears { get; set; }
        public int ageMonths { get; set; }
        public int ageDays { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string EmailID { get; set; }
        public string SecondaryEmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string SecondaryAddress { get; set; }
        public int maritalStatus { get; set; }
        public string URNID { get; set; }
        public string URNType { get; set; }
        public int RefferralTypeNo { get; set; }
        public string RefferralType { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public int RiderNo { get; set; }
        public int MarketingNo { get; set; }
        public int RouteNo { get; set; }
        public bool IsStat { get; set; }
        public string ClinicalHistory { get; set; }
        public string RegisteredType { get; set; }
        public string registrationDT { get; set; }
        public int DepartmentNo { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public int discountno { get; set; }
        public string discountDescription { get; set; }        
        public decimal DiscountAmount { get; set; }
        public decimal TdiscountAmt { get; set; }
        public int DiscountApprovedBy { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal DueAmount { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public bool IsAutoEmail { get; set; }
        public bool IsAutoSMS { get; set; }
        public bool IsAutoWhatsApp { get; set; }
        public bool isSelf { get; set; }        
        public List<FrontOfficeOrderList>? Orders { get; set; }
        public List<FrontOfficePayment>? Payments { get; set; }
        public string ExternalVisitID { get; set; }
        public string VaccinationType { get; set; }
        public string VaccinationDate { get; set; }
        public bool IsFranchise { get; set; }

        //arun changes
        public string Base64Data { get; set; }
        public string FileFormat { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }        
        public bool Deliverymode { get; set; }
        public string NURNID { get; set; }
        public string NURNType { get; set; }
        public string DueRemarks { get; set; }
        public string ExternalVisitIdentity { get; set; }
        public int WardNo { get; set; }
        public string WardName { get; set; }
        public string IPOPNumber { get; set; }
        public decimal GivenAmount { get; set; }
        public decimal ToBeReturn { get; set; }
        public Int64 HCPatientNo { get; set; }
        public Int16 IsDiscountApprovalReq { get; set; }
        public string NRICNumber { get; set; }
        public int NationalityNo { get; set; }
        public string AlternateIdType { get; set; }
        public string AlternateId { get; set; }
        public int RaceNo { get; set; }
        public string AllergyInfo { get; set; }
        public string PatientBlock { get; set; }
        public string PatientUnitNo { get; set; }
        public string PatientFloor { get; set; }
        public string PatientBuilding { get; set; }
        public string PatientHomeNo { get; set; }
        public int PhysicianNo2 { get; set; }
        public bool IsVipIndication { get; set; }
        public int BedNo { get; set; }
        public int CompanyNo { get; set; }
        public string CaseNumber { get; set; } 
        public string PatientOfficeNumber { get; set; }
        public bool IsPregnant { get; set; }
        public string Remarks { get; set; }
        public string HomePhoneNo { get; set; }
        public int ClinicalDiagnosis { get; set; }
        public string ClinicalDiagnosisOthers { get; set; }
        public string InternalComments { get;set; }
        public string SampleCollectionDT { get; set;}
        public bool isFasting { get; set; }
        public string ExternalPatientID {  get; set; }
        public string? OPDAppoinmentNo { get; set; }
        public bool? C2PReportSMSPatient { get; set; }
        public bool? C2PReportEmailPatient {  get; set; }
        public bool? C2PReportWhatsappPatient {  get; set; }
        public bool? C2PBillSMSPatient { get; set; }
        public bool? C2PBillEmailPatient { get;set; }
        public bool? C2PBillWhatsappPatient { get; set; }
        public string? loyalcardno { get; set; }
    }
    public class FrontOffficePatientResponse
    {
        public int patientvisitno { get; set; }
    }
    public class FrontOffficeValidatetest
    {
        public int result { get; set; }
    }
    public class FrontOffficeQueueResponse
    {
        public int patientvisitNo { get; set; }
    }
    public class FrontOffficeResetResponse
    {
        public int patientvisitno { get; set; }
    }
    public class FrontOffficeResponse
    {
        public int patientvisitno { get; set; }
        public string visitID { get; set; }
    }
    public class ReportOutput
    {
        public string PatientExportFile { get; set; }
        public string PatientExportFolderPath { get; set; }
        public string ExportURL { get; set; }
    }
    public class FrontOfficeOrderList
    {
        public string TestType { get; set; }
        public string TestCode { get; set; }
        public string TestShortCode { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int DiscountType { get; set; }
        public decimal discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int RateListNo { get; set; }
        public string status { get; set; }
        public string ClientServiceCode { get; set; }
        public string? DiscDescription { get; set; }
    }
    public class FrontOfficePayment
    {
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ModeOfType { get; set; }
        public int CurrencyNo { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? CurrencyAmount { get; set; }
    }

    public class reqcheckExists
    {
        public string check { get; set; }
        public string checkType { get; set; }
        public string checkValue { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class rescheckExists
    {
        public int patientVisitNo { get; set; }
        public string visitDTTM { get; set; }
        public string visitID { get; set; }
        public int patientNo { get; set; }
        public string patientID { get; set; }
        public string fullName { get; set; }
        public string ageSex { get; set; }
        public string mobileNumber { get; set; }
        public string displayText { get; set; }
        public string branchName { get; set; }
        public string idTypeDesc { get; set; }
        public string idTypeNo { get; set; }
    }
    //arun changes multi file uploaded
    public class BulkFileUpload
    {
        public string ActualFileName { get; set; }
        public string ManualFileName { get; set; }
        public string FileBinaryData { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string ExternalVisitID { get; set; }
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string ActualBinaryData { get; set; }
        public string docType { get; set; }
    }
    //add physician/doctor name in registration
    public class DoctorDetails
    {
        public string DoctorName { get; set; }
        public string DoctorQualification { get; set; }
        public string DoctorMobile { get; set; }
        public int DoctorId { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
    }

    public class ClinicalSummary
    {
        public string Summary { get; set; }
    }

    public class PatientNotifyLog
    {
        public int LogNo { get; set; }
        public int PatientNotifyLogNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int VisitTestNo { get; set; }
        public string NotifyLogDTTM { get; set; }
        public string LogType { get; set; }
        public string NotifyContent { get; set; }
        public string LogUserName { get; set; }
        public string LogUserTyped { get; set; }
        public int LogUserNo { get; set; }
        public int LogUserType { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }    
        public DateTime LogDTTM { get; set; }
    }
    public class PatientNotifyLogResponse
    {
        public int PatientNotifyLogNo { get; set; }     
    }
    public class PrePrintBarcodeRequest
    {
        public int visitNo { get; set; }
        public int collectedBy { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int ordersNo { get; set; }
        public int orderListNo { get; set; }
        public string collectedDateTime { get; set; }
        public string pagecode { get; set; }
        public int testNo { get; set; }
        public string barcode { get; set; }
        public bool applyPrefix { get; set; }
        public bool applySuffix { get; set; }
    }
    public class ExternalVisitDetails
    {
        public string Value { get; set; }
        public char ValueType { get; set; }
        public int VisitNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalVisitDetailsResponse
    {
        public string Outpt { get; set; }
    }
    public class ExternalBulkFile
    {
        public string filename { get; set; }
        public int customerno { get; set; }
        public int physicianno { get; set; }
        public int contractno { get; set; }        
        public int testno { get; set; }
        public string testname { get; set; }
        public string testype { get; set; }
        public string validfrom { get; set; }
        public string validto { get; set; }
        public List<ExternalmassPatient>? patientlst { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int iadditionalrecords { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalmassPatient
    {
        public string idNo { get; set; }
        public string patientname { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string street { get; set; }
        public string block { get; set; }
        public string buildingname { get; set; }
        public string postalcode { get; set; }
        public string alternate_email { get; set; }
        public string nationality { get; set; }
    }
    public class MassRegistrationResponse
    {
        public int result { get; set; }
    }
    public partial class MassFileDTO
    {
        public Int64 Row_Num { get; set; }
        public int massfileno { get; set; }
        public int CustomerNo { get; set; }
        public string FileName { get; set; }
        public Int64 Sno { get; set; }
        public string CustomerName { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
    }
    public class massPatientBarcode
    {
        public Int64 Row_Num { get; set; }
        public string IdNumber { get; set; }        
        public string patientname { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public int age { get; set; }
        public string ServiceName { get; set; }
        public string SampleName { get; set; }
        public string referenceNo { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string street { get; set; }
        public string block { get; set; }
        public string buildingname { get; set; }
        public string postalcode { get; set; }
        public string alternate_email { get; set; }
        public string nationality { get; set; }
        public string idtype { get; set; }
    }
    public class OPDBulkFileUpload
    {
        public string actualFileName { get; set; }
        public string ManualFileName { get; set; }
        public string FileBinaryData { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string ExternalVisitID { get; set; }
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string ActualBinaryData { get; set; }
        public int docType { get; set; }
        public int patientNumber { get; set; }
    }
    public class DocumentInfo
    {
        public string AppointmentId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string CreatedDate { get; set; }
        public string FileExtension { get; set; }
        public string BinaryData { get; set; }
    }
    public class OPDReportOutput
    {
        public string? PatientExportFile { get; set; }
        public string? PatientExportFolderPath { get; set; }
        public string? ExportURL { get; set; }
    }
    public class TblloyalReq
    {
        public string? loyalcardno { get; set; }
        public int venueNo { get; set; }
        public int venuebranchno { get; set; }
    }
    public class Tblloyal
    {
        public int rowNo { get; set; }
        public string? loyaltytype { get; set; }
    }
    public class PatientVisitPatternIDGenReq
    {
        public string PatternType { get; set; }       
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class PatientVisitPatternIDGenRes
    {
        public string PatternID { get; set; }
        public int Id { get; set; }      
    }
    public class AutoLoyaltyIDGenRequest
    {
        public int DiscountNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class AutoLoyaltyIDGenResponse
    {
        public int Status { get; set; }
        public string LoyaltyPatternID { get; set; }        
    }     
}
