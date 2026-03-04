using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Win.Common
{
    public class EIDModal
    {
        public string _IdNumberText { get; set; }
        public string _CardNumberText { get; set; }
        public NonModifiablePersonalData _NonModifiablePersonalData { get; set; }
        public ModifiableData _ModifiableData { get; set; }
        public HomeAddressData _HomeAddressData { get; set; }
        public WorkAddressData _WorkAddressData { get; set; }
        public ImageData _ImageData { get; set; }
    }
    public class HomeAddressData
    {
        public string Home_AddressTypeCodeText { get; set; }
        public string Home_LocationCodeText { get; set; }
        public string Home_EmirateCodeText { get; set; }
        public string Home_EmirateArabicText { get; set; }
        public string Home_EmirateEnglishText { get; set; }
        public string Home_CityCodeText { get; set; }
        public string Home_CityArabicText { get; set; }
        public string Home_CityEnglishText { get; set; }
        public string Home_StreetArabicText { get; set; }
        public string Home_StreetEnglishText { get; set; }
        public string Home_PoBoxText { get; set; }
        public string Home_AreaCodeText { get; set; }
        public string Home_AreaArabicText { get; set; }
        public string Home_AreaEnglishText { get; set; }
        public string Home_BuildingNameArabicText { get; set; }
        public string Home_BuildingNameEnglishText { get; set; }
        public string Home_FlatNumberText { get; set; }
        public string Home_LandPhoneNumberText { get; set; }
        public string Home_MobilePhoneNumberText { get; set; }
        public string Home_EmailText { get; set; }
    }
    public class ImageData
    {
        public string PublicDataImage { get; set; }
        public string SignatureDataImage { get; set; }
    }
    public class ModifiableData
    {
        public string OccupationCodeText { get; set; }
        public string OccupationArabicText { get; set; }
        public string OccupationEnglishText { get; set; }
        public string OccupationTypeArabicText { get; set; }
        public string OccupationTypeEnglishText { get; set; }
        public string OccupationFieldCodeText { get; set; }
        public string CompanyNameArabicText { get; set; }
        public string CompanyNameEnglishText { get; set; }
        public string MaritalStatusCodeText { get; set; }
        public string FamilyIdText { get; set; }
        public string HusbandIdNumberText { get; set; }
        public string SponsorTypeCodeText { get; set; }
        public string SponserUnifiedNumberText { get; set; }
        public string SponsorNameText { get; set; }
        public string ResidencyTypeCodeText { get; set; }
        public string ResidencyNumberText { get; set; }
        public string ResidencyExpiryDateText { get; set; }
        public string PassportNumberText { get; set; }
        public string PassportTypeCodeText { get; set; }
        public string PassportCountryCodeText { get; set; }
        public string PassportCountryArabicText { get; set; }
        public string PassportCountryEnglishText { get; set; }
        public string PassportIssueDateText { get; set; }
        public string PassportExpiryDateText { get; set; }
        public string QualificationLevelCodeText { get; set; }
        public string QualificationLevelArabicText { get; set; }
        public string QualificationLevelEnglishText { get; set; }
        public string DegreeDescriptionArabicText { get; set; }
        public string DegreeDescriptionEnglishText { get; set; }
        public string FieldOfStudyCodeText { get; set; }
        public string FieldOfStudyArabicText { get; set; }
        public string FieldOfStudyEnglishText { get; set; }
        public string PlaceOfStudyArabicText { get; set; }
        public string PlaceOfStudyEnglishText { get; set; }
        public string DateOfGraduationText { get; set; }
        public string MotherFullNameArabicText { get; set; }
        public string MotherFullNameEnglishText { get; set; }
    }
    public class NonModifiablePersonalData
    {
        public string IdTypeText { get; set; }
        public string IssueDateText { get; set; }
        public string ExpiryDateText { get; set; }
        public string TitleArabicText { get; set; }
        public string TitleEnglishText { get; set; }
        public string FullNameArabicText { get; set; }
        public string FullNameEnglishText { get; set; }
        public string GenderText { get; set; }
        public string NationalityArabicText { get; set; }
        public string NationalityEnglishText { get; set; }
        public string NationalityCodeText { get; set; }
        public string DateOfBirthText { get; set; }
        public string PlaceOfBirthArabicText { get; set; }
        public string PlaceOfBirthEnglishText { get; set; }
    } 
    public class WorkAddressData
    {
        public string Work_AddressTypeCodeText { get; set; }
        public string Work_LocationCodeText { get; set; }
        public string Work_CompanyNameArabicText { get; set; }
        public string Work_CompanyNameEnglishText { get; set; }
        public string Work_EmirateCodeText { get; set; }
        public string Work_EmirateArabicText { get; set; }
        public string Work_EmirateEnglishText { get; set; }
        public string Work_CityCodeText { get; set; }
        public string Work_CityArabicText { get; set; }
        public string Work_CityEnglishText { get; set; }
        public string Work_StreetArabicText { get; set; }
        public string Work_StreetEnglishText { get; set; }
        public string Work_PoBoxText { get; set; }
        public string Work_AreaCodeText { get; set; }
        public string Work_AreaArabicText { get; set; }
        public string Work_AreaEnglishText { get; set; }
        public string Work_BuildingNameArabicText { get; set; }
        public string Work_BuildingNameEnglishText { get; set; }
        public string Work_LandPhoneNumberText { get; set; }
        public string Work_MobilePhoneNumberText { get; set; }
        public string Work_EmailText { get; set; }
    }
    public class EMIDResponse
    {   
        public string EMID { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
    }
}
