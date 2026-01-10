using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblPatient
    {
        public int PatientNo { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PatientId { get; set; }
        public int? Age { get; set; }
        public string AgeType { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string EmailId { get; set; }
        public string SecondaryEmailId { get; set; }
        public string Address { get; set; }
        public int? CountryNo { get; set; }
        public int? StateNo { get; set; }
        public int? CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string SecondaryAddress { get; set; }
        public string Urnid { get; set; }
        public string Urntype { get; set; }
        public string IdentifyId { get; set; }
        public string IdentifyType { get; set; }
        public bool IsPatientImage { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string IntegrationId { get; set; }
        public string IntegrationCode { get; set; }
        public string Password { get; set; }
    }
}
