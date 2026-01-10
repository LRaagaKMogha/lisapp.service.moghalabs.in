using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.PatientInfo
{
    public class PatientsMasterResponse
    {
        public int RowNo { get; set; }
        public int PatientNo { get; set; }
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PatientID { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public byte AgeMonths { get; set; }
        public byte AgeDays { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNumber { get; set; }
        public string LandlineNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; }
        public int CountryNo { get; set; }
        public string StateName { get; set; }
        public int StateNo { get; set; }
        public string CityName { get; set; }
        public int CityNo { get; set; }
        public string Place { get; set; }
        public string Pincode { get; set; }
        public string MaritalText { get; set; }
        public Int16 MaritalStatus { get; set; }
        public string RemarksHistory { get; set; }
        public string NationalityText { get; set; }
        public Int16 NationalityNo { get; set; }
        public string BloodGroupText { get; set; }
        public Int16 BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public bool IsPatientMaster { get; set; }
        public string? LoyalCardNo { get; set; }
        public int SaveType { get; set; }
    }
    public class PatientsMasterRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientNo { get; set; }
        public int PageIndex { get; set; }
        public bool IsPatientMaster { get; set; }
    }
    public partial class rtnpatient
    {
        public int PatientNo { get; set; }
    }
    public class PatientsMasterSave
    {       
        public int PatientNo { get; set; }     
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public int Age { get; set; }
        public int AgeMonths { get; set; }
        public int AgeDays { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNumber { get; set; }
        public string LandlineNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string Place { get; set; }
        public string Pincode { get; set; }
        public int MaritalStatus { get; set; }
        public string RemarksHistory { get; set; }
        public int NationalityNo { get; set; }
        public int BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public bool IsPatientMaster { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserID { get; set; }
        public string LoyalCardNo { get; set; }
        public int SaveType { get; set; }
    }
    public class PatientDocUpload
    {
        public int DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string ExpiryDate { get; set; }
        public string PlaceofIssue { get; set; }
        public string ActualFileName { get; set; }
        public string ManualFileName { get; set; }
        public string FileBinaryData { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string ExternalVisitID { get; set; }
        public int PatientNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string ActualBinaryData { get; set; }
        public bool Status { get; set; }
    }
}



