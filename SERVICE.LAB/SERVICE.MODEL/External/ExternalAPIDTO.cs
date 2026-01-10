using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class ExternalLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? DeviceType { get; set; }
        public string? RequestType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
    public class ExternalLoginResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public int UserNo { get; set; }
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int IsOffline { get; set; }
        public int IsPreprintedBarcode { get; set; }
    }
    public class Location
    {
        public string? DeviceType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalCommonResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public string? BookingID { get; set; }
        public int PatientNo { get; set; }
    }
    public class AppointmentRequest
    {
        public string? BookingID { get; set; }
        public string? MobileNumber { get; set; }
        public string? type { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalAppointmentResponse
    {
        public List<ExternalPatientResponse>? lstPatientResponse { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
        public int TotalCountTest { get; set; }
    }
    public class ExternalPatientResponse
    {
        public int BookingNo { get; set; }
        public string? BookingId { get; set; }
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? Pincode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? TestNames { get; set; }
        public int NoofTest { get; set; }
        public string? RegisteredDateTime { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int CurrentStatusNo { get; set; }
        public string? CurrentStatusName { get; set; }
        public string? FromSlot { get; set; }
        public string? ToSlot { get; set; }
        public string? UserImage { get; set; }
        public decimal TotalTestAmount { get; set; }
        public int PhysicianNo { get; set; }
        public bool IsStart { get; set; }
        public bool IsPaid { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<LstTestDetail>? lstTestDetails { get; set; }
        public List<LstTestSampleWise>? lstTestSampleWise { get; set; }
    }
    public class LstTestDetail
    {
        public int BookingNo { get; set; }
        public string? BookingId { get; set; }
        public int TestNo { get; set; }
        public string? TestCode { get; set; }
        public string? TestType { get; set; }
        public string? TestName { get; set; }
        public int SampleNo { get; set; }
        public string? SampleName { get; set; }
        public string? ContainerName { get; set; }
        public decimal Amount { get; set; }
    }
    public class LstTestSampleWise
    {
        public int BookingNo { get; set; }
        public string? BookingId { get; set; }
        public int TestNo { get; set; }
        public string? TestCode { get; set; }
        public string? TestType { get; set; }
        public string? TestName { get; set; }
        public int SampleNo { get; set; }
        public string? SampleName { get; set; }
        public string? ContainerName { get; set; }
        public decimal Amount { get; set; }
    }
    public class ExternalPatientTempResponse
    {
        public Int64 Row_Num { get; set; }
        public int BookingNo { get; set; }
        public string? BookingId { get; set; }
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? Pincode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? TestNames { get; set; }
        public int NoofTest { get; set; }
        public string? RegisteredDateTime { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int CurrentStatusNo { get; set; }
        public string? CurrentStatusName { get; set; }
        public string? FromSlot { get; set; }
        public string? ToSlot { get; set; }
        public string? UserImage { get; set; }
        public int TotalTestAmount { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public bool IsStart { get; set; }
        public bool IsPaid { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int TestNo { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public string? TestType { get; set; }
        public int SampleNo { get; set; }
        public string? SampleName { get; set; }
        public string? ContainerName { get; set; }
        public decimal Amount { get; set; }
    }

    public class ExternalPrescription
    {
        public string? BookingID { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<ExternalPrescriptionlst>? PrescriptionImglst { get; set; }
    }
    public class ExternalPrescriptionlst
    {
        public string? ImageType { get; set; }
        public string? ImageName { get; set; }
        public string? base64 { get; set; }
    }

    public class ExternalAddTest
    {
        public string? BookingID { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<LstCartList>? lstCartList { get; set; }
    }

    public class LstCartList
    {
        public int TestNo { get; set; }        
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public string? TestType { get; set; }
        public int RateListNo { get; set; }
        public decimal Amount { get; set; }
        public bool IsFasting { get; set; }
        public string? Remarks { get; set; }
    }
    public class ExternalAddTestResponse
    {
      
        public int Status { get; set; }
        public string? Message { get; set; }
    }
    public class ExternalDeleteTestRequest
    {
        public string? BookingID { get; set; }
        public int BookingTestNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }

    public class ExternalLstBarcode
    {
        public int SampleNo { get; set; }
        public string? BarcodeNo { get; set; }
    }
    public class ExternalResult
    {
        public int result { get; set; }
    }
    public class ExternalDeleteTest
    {
        public int result { get; set; }
    }
    public class ExternalBarcodeTest
    {
        public int result { get; set; }
    }

    public class ExternalBarcode
    {
        public string? BookingID { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<ExternalLstBarcode>? lstBarcode { get; set; }
    }


    public class ExternalBookingPayment
    {
        public string? BookingID { get; set; }
        public int GrossAmount { get; set; }
        public int DiscountAmount { get; set; }
        public int NetAmount { get; set; }
        public int PaidAmount { get; set; }
        public int CollectedAmount { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<LstPaymentDetail>? lstPaymentDetails { get; set; }
    }

    public class ExternalSampleList
    {
        public Int64 Row_Num { get; set; }
        public int OrderNo { get; set; }
        public int OrderListNo { get; set; }
        public int SampleNo { get; set; }
        public int ContainerNo { get; set; }
        public int ServiceNo { get; set; }

    }
    public class LstPaymentDetail
    {
        public int Amount { get; set; }
        public string? PaymentType { get; set; }
    }
    public class ExternalSignout
    {
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class ExternalHCResponse
    {
        public Int64 Row_Num { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? AgeType { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? SecondaryEmailID { get; set; }
        public string? Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string? AreaName { get; set; }
        public string? Pincode { get; set; }
        public int RefferralTypeNo { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public int RiderNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int ServiceNo { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public int SampleNo { get; set; }
        public string? BarcodeNo { get; set; }

    }
    public class ExternalApiReferralRequest
    {
        public string? SearchKey { get; set; }
        public string? SearchType { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }

    public class ExternalApiReferralResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public List<CommonMasterDto>? lstReferral { get; set; }
    }
    public partial class ExternalBookingDto
    {

        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? AgeType { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsappNo { get; set; }
        public string? EmailID { get; set; }
        public string? Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string? AreaName { get; set; }
        public string? Pincode { get; set; }
        public int RiderNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string? registeredDateTime { get; set; }
        public int RefferralTypeNo { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public string registeredfrom { get; set; }
        public List<LstCartList>? lstCartList { get; set; }
        public int PatientNo {  get; set; }
    }

    public class ExternalBookingResponse
    {
        public string? result { get; set; }
        public int PatientNo { get; set; }
    }
    public class ExternalHCRiderResponse
    {
        public int result { get; set; }
    }
    public class ExternalHCPatientResponse
    {
        public int result { get; set; }
    }

    public class ExternalRiderStatusRequest
    {
        public string? BookingNo { get; set; }
        public int RiderNo { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int IsOffline { get; set; }
    }
    public class ExternalPatientStatusRequest
    {
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? Reason { get; set; }
        public string? BookingID { get; set; }
        public string? BookingStatus { get; set; }
    }
    public class ExternalServiceRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int ClientNo { get; set; }
        public int PhysicianNo { get; set; }
        public string? ServiceName { get; set; }
        public int PageIndex { get; set; }
    }
    public class ExternalServiceDto
    {
        public Int64 Row_Num { get; set; }
        public int ServiceNo { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public string? TestType { get; set; }
        public int RateListNo { get; set; }
        public decimal Amount { get; set; }
    }
    public class ExternalApiServiceResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public List<ExternalServiceDto>? lstService { get; set; }
    }
    public class ExternalHCAppointment
    {
        public int PageIndex { get; set; }
        public Int64 Row_Num { get; set; }
        public int TotalRecords { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public int HCPatientNo { get; set; }
        public string? HCPatientID { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? lastName { get; set; }
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public int patientAge { get; set; }
        public string? AgeType { get; set; }
        public string? gender { get; set; }
        public string? dob { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsappNo { get; set; }
        public string? EmailID { get; set; }
        public string? RegisteredDateTime { get; set; }
        public string? TestNames { get; set; }
        public int HCStatus { get; set; }
        public string? RiderName { get; set; }
        public int RiderNo { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? AreaName { get; set; }
        public string? registeredfrom { get; set; }
        public int VisitNo { get; set; }
        public string CancelledDesc { get; set; }

    }
    public class UpdateHcpatient
    {
        public int bioHCPatientNo { get; set; }
        public string biotitleCode { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }
        public string biodOB { get; set; }
        public int bioage { get; set; }
        public string biogender { get; set; }
        public string bioAgeType { get; set; }
        public string biomobileNumber { get; set; }
        public string bioemailID { get; set; }
        public string bioaddress { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class ExternalupdateCommonResponse
    {
        //public int Status { get; set; }
        //public string Message { get; set; }
        //public string BookingID { get; set; }
        public int bioHCPatientNo { get; set; }
    }

    public class UpdateStatusApptDateResponse
    {
        public int Status { get; set; }   
        public string Message { get; set; }
    }
    public class UpdateStatusApptDateRequest
    {
        public int HCPatientNo { get; set; }
        public bool IsCancelled { get; set; }
        public string AppointDDTM { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string ApptDateChangeDesc { get; set; }
        public string CancelledDesc { get; set; }
    }
    public class TestSlotBookingDTO
    {
        public int PageIndex { get; set; }
        public Int64 Row_Num { get; set; }
        public int TotalRecords { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public int SlotBookingPatientNo { get; set; }
        public string SlotBookingPatientID { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string lastName { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int patientAge { get; set; }
        public string AgeType { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string RegisteredDateTime { get; set; }
        public string TestNames { get; set; }
        public string address { get; set; }
        public string areaName { get; set; }
        public string pincode { get; set; }
        public int HCStatus { get; set; }
    }
    public partial class ExternalBookingDTO
    {
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public int RiderNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string registeredDateTime { get; set; }
        public string registeredfrom { get; set; }
        public int RefferralTypeNo { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public List<LstCartList> lstCartList { get; set; }
        public int PatientNo { get; set; }
    }
    public class TestSlotCommonResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string BookingID { get; set; }
        public int PatientNo { get; set; }
    }
    public class SlotBookingupdateCResponse
    {
        //public int Status { get; set; }
        //public string Message { get; set; }
        //public string BookingID { get; set; }
        public int bioPatientNo { get; set; }
    }
}