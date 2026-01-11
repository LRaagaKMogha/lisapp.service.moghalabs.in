using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Integration
{

    public class ExternalPatientDetailsResponse
    {
        public string NricNumber { get; set; }  
        public string IdNumber { get; set; }
        public string PatientName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SexId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth {  get; set; }
        public int GenderId { get; set; }
        public int NationalityId { get; set; }
        public int RaceId { get; set; }
        public int ResidenceId { get; set; }
    }
    public class ExternalPatientDetails
    {
        public string EmailID { get; set; }
        public string CountryName { get; set; }
        public string MobileNumer { get; set; }
        public string Address { get; set;  }
        public string PatientHomeNo {  get; set; }
        public string PatientUnitNo { get; set; }
        public string PatientFloor { get; set;  }
        public string PatientBuilding { get; set;  }
        public string PatientBlock { get; set; }
        public string AltMobileNumber {  get; set; }
        public string AreaName { get; set; }
        public int CityNo {  get; set; }
        public string maritalStatus { get; set; }
        public string FirstName { get; set;  }
        public string LastName { get; set;  }
        public string DateOfBirth { get; set; }
        public string MiddleName { get; set; }
        public string SexId { get; set; }
        public string AlternativeIdType { get; set; }
        public string AlternativeIdNumber { get; set; }
        public string RaceCode { get; set; }
        public string RaceDescription { get; set; }
        public string NationalityCode { get; set; }
        public string NationalityDescription { get; set;  }
        public bool IsVip { get; set; }
        public string NursingOU { get; set; }
        public string Room { get; set;  }
        public string Bed { get; set; }
        public bool IsAllergy { get; set; }
        public string AllergyDetails { get; set; }
        public string PostalCode { get; set; }
        public string DocumentNumber { get; set; }
        public string PatientNumber { get; set; }
        public string PatientOccupation { get; set; }
        public string RoomNumber { get; set; }
    }
}
