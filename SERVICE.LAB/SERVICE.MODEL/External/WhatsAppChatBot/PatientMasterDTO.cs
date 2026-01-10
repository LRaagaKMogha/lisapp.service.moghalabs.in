using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.WhatsAppChatBot
{
   
    public class GetPatientRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientNo { get; set; }
        public int PageIndex { get; set; }
        public bool IsPatientMaster { get; set; }
        public string? MobileNo { get; set; }
    }
    public class GetPatientVisitRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientNo { get; set; }
        public string? VisitId { get; set; }
    }
    public class GetPatientResponse
    {
        public int RowNo { get; set; }
        public int PatientNo { get; set; }
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? PatientID { get; set; }
        public DateTime DOB { get; set; }
        public int AgeInYears { get; set; }
        public byte AgeInMonths { get; set; }
        public byte AgeInDays { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Address { get; set; }
        public string? CountryName { get; set; }
        public int CountryNo { get; set; }
        public string? StateName { get; set; }
        public int StateNo { get; set; }
        public string? CityName { get; set; }
        public int CityNo { get; set; }
        public string? Place { get; set; }
        public string? Pincode { get; set; }
        public string? MaritalText { get; set; }
        public Int16 MaritalStatus { get; set; }
        public string? RemarksHistory { get; set; }
        public string? NationalityText { get; set; }
        public Int16 NationalityNo { get; set; }
        public string? BloodGroupText { get; set; }
        public Int16 BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public bool IsPatientMaster { get; set; }
        public int SaveType { get; set; }
    }

    public class UpdatePatientRequest
    {
        public int PatientNo { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? DOB { get; set; }
        public int AgeInYear { get; set; }
        public int AgeInMonths { get; set; }
        public int AgeInDays { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string? Place { get; set; }
        public string? Pincode { get; set; }
        public int MaritalStatus { get; set; }
        public string? RemarksHistory { get; set; }
        public int NationalityNo { get; set; }
        public int BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserID { get; set; }
    }
    public partial class ReturnPatientNo
    {
        public int PatientNo { get; set; }
    }
    public class PatientInformationResponse
    {
        public int RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int PatientNo { get; set; }
        public string? PatientID { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public DateTime DOB { get; set; }
        public Int16 AgeInYears { get; set; }
        public Int16 AgeInMonths { get; set; }
        public Int16 AgeInDays { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? WhatsAppNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public string? PlaceName { get; set; }
        public string? Pincode { get; set; }
    }
    public class PatientVisitResponse
    {
        public int RowNo { get; set; }
        public int PatientNo { get; set; }
        public string? PatientId { get; set; }
        public int VisitNo { get; set; }
        public string? VisitId { get; set; }
        public string? VisitDtTm { get; set; }
        public string? FullName { get; set; }
        public Int32 AgeInYears { get; set; }
        public Int32 AgeInMonths { get; set; }
        public Int32 AgeInDays { get; set; }
        public string? Gender { get; set; }
        public bool IsDue { get; set; }
        public List<PayList>? PayList { get; set; }
    }
    public partial class PayList
    {
        public string? ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
    }
    public class PatientVisitResponseDtl
    {
        public int RowNo { get; set; }
        public int PatientNo { get; set; }
        public string? PatientId { get; set; }
        public int VisitNo { get; set; }
        public string? VisitId { get; set; }
        public string? VisitDtTm { get; set; }
        public string? FullName { get; set; }
        public Int32 AgeInYears { get; set; }
        public Int32 AgeInMonths { get; set; }
        public Int32 AgeInDays { get; set; }
        public string? Gender { get; set; }
        public bool IsDue { get; set; }
        public string? PayList { get; set; }
    }
}
