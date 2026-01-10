using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEV.Model.PatientInfo
{
    public class PatientInfoResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string RHNo { get; set; }
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
        public int OrderListNo { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string TestType { get; set; }
        public string TestShortNames { get; set; }
        public int ResultTypeNo { get; set; }
        public string OrderStatus { get; set; }
        public string colorcode { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public int ageDays { get; set; }
        public int ageMonths { get; set; }
        public int ageYears { get; set; }
        public int gender { get; set; }
        public string mobile { get; set; }
        public string emailID { get; set; }
        public string dob { get; set; }
        public string sampleCollectedDTTM { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public string BarcodeNo { get; set; }
        public bool IsBarcodeYes { get; set; }
        public bool IsMastSyncYes { get; set; }
        public bool IsOrderSyncYes { get; set; }
        public string URNType { get; set; }
        public string URNID { get; set; }
        public string Address { get; set; }
        public int PatientEdit { get; set; }
        public int DuePrint { get; set; }
        public bool IsDue { get; set; }
        public bool IsStat { get; set; }
        public int NotifyCount { get; set; }
        public bool IsPatientEdit { get; set; }
        public string CollectionCenterCode { get; set; }
        public string externalvisitid { get; set; }
        public string AccessionNo { get; set; }
        public int Nationality { get; set; }
        public string NationName { get; set; }
        public string AlternateId { get; set; }
        public string AlternateIdType { get; set; }
        public int RaceNo { get; set; }
        public string RaceName { get; set; }
        public string Pincode { get; set; }
        public string PatientOfficeNumber { get; set; }
        public string PatientHomeNo { get; set; }
        public string AllergyInfo { get; set; }
        public int BedNo { get; set; }
        public string BedName { get; set; }
        public int CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public int WardNo { get; set; }
        public string WardName { get; set; }
        public string MobileNumber { get; set; }
        public string CaseNumber { get; set; }
        public bool IsVipIndication { get; set; }
        public string NURNType { get; set; }
        public string NURNID { get; set; }
        public string PhysicianName { get; set; }
        public string TubeNumber { get; set; }
        public string BarcodeShortNames { get; set; }
        public Int16 Quantity {  get; set; }
        public bool IsVIP { get; set; }
        public string GenderCode { get; set; }
        public bool IsDocument {  get; set; }
        public int TubeQty { get; set; }
        [NotMapped]
        public bool IsShowDocument { get; set; }
        [NotMapped]
        public bool IsShowSendOutDocument { get; set; }
        public string BranchName { get; set; }
        public string marketingExec { get; set; }
        public string collectionExec { get; set; }
        public string? loyalcardno { get; set; }
    }
    public class CustomSearchResponse
    {
        public int VisitNo { get; set; }
        public string SearchValue { get; set; }
    }
    public class ReasonDetailsRequest
    {
        public int VisitNo { get; set; }
        public int OrderListNo { get; set; }
        public int OrderDetailsNo { get; set; }
        public int ServiceNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int ViewVenueBranchNo { get; set; }
        public string PageCode { get; set; }
        public string ServieType { get; set; }
        public string SearchValue { get; set; }
        public int PatientNo { get; set; }
        public int SearchStatus { get; set; }
    }
    public class ReasonDetailsResponse
    {
        public int Id { get; set; }        
        public string ServiceName { get; set; }
        public string ReasonType { get; set; }
        public string DidBy { get; set; }
        public string DidOn { get; set; }
        public string Reason { get; set; }
        public string GroupName { get; set; }
    }

    public class EditSampleRequest
    {
        public int specimenQty { get; set; }
        public int PatientSamplesNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int UserNo  { get; set; }
        public int VenueNo { get; set; }

    }
    public class GetSampleRequest
    {
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }

    }

    public class GetSampleResponse 
    {
        public Int64 RowNo { get; set; }
        public int PatientSamplesNo { get; set; }
        public int specimenQty { get; set; }
        public string SampleName { get; set; }
        public int SampleNo { get; set; }
        public string ContainerName { get; set; }
        public int ContainerNo { get; set; }
        public int SampleSourceNo { get; set; }
        public string sampleSourceName { get; set; }
        public string BarcodeNo { get; set; }
        public bool isEdit { get; set; }
        public int ServiceNo { get; set; }
    }
    public class EditSampleRequestNew
    {
        public int specimenQty { get; set; }
        public int PatientSamplesNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
        public int SampleSourceNo { get; set; }
        public string SampleSource { get; set; }
        public int ServiceNo { get; set; }
        public string BarcodeNo { get; set; }
        public string VisitID { get; set; }

    }
    public class EditPatientResponseNew
    {
        public string statusCode { get; set; }
    }

    public class SyncMasterRequestDTO
    {
        public int visitNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string syncType { get; set; }
        public List<SyncMasterServiceDTO> serviceList { get; set; }
    }
    public class SyncMasterServiceDTO
    {
        public int orderListNo { get; set; }
        public int testNo { get; set; }
        public string testType { get; set; }
    }
    public class GetPatientVisitActionHistoryResponse
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ActionOn { get; set; }
        public string ActionBy { get; set; }
        public string ActionBranch { get; set; }
        public string ColorCode { get; set; }
    }
    public class PatientInfoListResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string RHNo { get; set; }
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
        public int OrderListNo { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string TestType { get; set; }
        public string TestShortNames { get; set; }
        public int ResultTypeNo { get; set; }
        public string OrderStatus { get; set; }
        public string colorcode { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public int gender { get; set; }
        public string mobile { get; set; }
        public string emailID { get; set; }
        public string dob { get; set; }
        public string sampleCollectedDTTM { get; set; }
        public string sampleName { get; set; }
        public string containerName { get; set; }
        public string BarcodeNo { get; set; }
        public bool IsBarcodeYes { get; set; }
        public bool IsMastSyncYes { get; set; }
        public bool IsOrderSyncYes { get; set; }
        public string URNType { get; set; }
        public string URNID { get; set; }
        public string Address { get; set; }
        public int PatientEdit { get; set; }
        public int DuePrint { get; set; }
        public bool IsDue { get; set; }
        public bool IsStat { get; set; }
        public int NotifyCount { get; set; }
        public bool IsPatientEdit { get; set; }
        public string CollectionCenterCode { get; set; }
        public string externalvisitid { get; set; }
        public string AccessionNo { get; set; }
        public int Nationality { get; set; }
        public string NationName { get; set; }
        public string AlternateId { get; set; }
        public string AlternateIdType { get; set; }
        public int RaceNo { get; set; }
        public string RaceName { get; set; }
        public string Pincode { get; set; }
        public string PatientOfficeNumber { get; set; }
        public string PatientHomeNo { get; set; }
        public string AllergyInfo { get; set; }
        public int BedNo { get; set; }
        public string BedName { get; set; }
        public int CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public int WardNo { get; set; }
        public string WardName { get; set; }
        public string MobileNumber { get; set; }
        public string CaseNumber { get; set; }
        public bool IsVipIndication { get; set; }
        public string NURNType { get; set; }
        public string NURNID { get; set; }
        public string PhysicianName { get; set; }
        public string TubeNumber { get; set; }
        public string BarcodeShortNames { get; set; }
        public Int16 Quantity { get; set; }
        public bool IsVIP { get; set; }
        public string GenderCode { get; set; }
        public bool IsDocument { get; set; }
        public int TubeQty { get; set; }
        [NotMapped]
        public bool IsShowDocument { get; set; }
        [NotMapped]
        public bool IsShowSendOutDocument { get; set; }
        public string BranchName { get; set; }
        public string marketingExec { get; set; }
        public string collectionExec { get; set; }
        public string? loyalcardno { get; set; }
    }
    public class ReportOutputhc
    {
        public string PatientExportFile { get; set; }
        public string PatientExportFolderPath { get; set; }
        public string ExportURL { get; set; }
    }

}



