using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
   public class GetPatientDetailsResponse
    {
        public int Sno { get; set; }
        public Int32 PatientNo { get; set; }
        public string PatientID { get; set; }
        public string FullName { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public int ageYears { get; set; }
        public int ageMonths { get; set; }
        public int ageDays { get; set; }
        public string dOB { get; set; }
        public int Gender { get; set; }
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
         public short maritalStatus { get; set; }
        public string uRNID { get; set; }
        public string uRNType { get; set; }
        public string ExternalVisitID { get; set; }
        public int RefferralTypeNo { get; set; }
        public int PhysicianNo { get; set; }
        public int WardNo { get; set; }
        public string RefferralName { get; set; }
        public string VaccinationDate { get; set; }
        public string VaccinationType { get; set; }
        public Int32 HCPatientNo { get; set; }
        public int RateListNo { get; set; }
        public decimal Amount { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceCode { get; set; }
        public string? NRICNumber { get; set; }
        public int RaceNo { get; set; }
        public string? PatientBlock { get; set; }
        public string? PatientUnitNo { get; set; }
        public string? PatientFloor { get; set; }
        public string? PatientBuilding { get; set; }
        public string? PatientHomeNo { get; set; }
        public string AlternateIdType { get; set; }
        public string AlternateId { get; set; }
        public int? NationalityNo { get; set; }
        public string? loyalcardno { get; set; }

    }

    public class GetPatientDetailsWithServices: GetPatientDetailsResponse
    {        
        public List<ServiceSearchDTO> serviceRateLists { get; set; }

    }
}
