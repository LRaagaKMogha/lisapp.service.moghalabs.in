using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class OPDPatientOfficeDTO
    {
        public int OPDPatientNo { get; set; }
        public int oPDPatientAppointmentNo { get; set; }
        public Int64 PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? WhatsappNo { get; set; }
        public string? EmailID { get; set; }
        public string? SecondaryEmailID { get; set; }
        public string? Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string? AreaName { get; set; }
        public string? Pincode { get; set; }
        public int AppointmentMode { get; set; }
        public int SpecializationNo { get; set; }
        public int PhysicianNo { get; set; }
        public string? Reason { get; set; }
        public string AppointmentDateTime { get; set; }
        public string? ArrivedDateTime { get; set; }
        public int appointmentStatus { get; set; }
        public bool IsNew { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsVIP { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string? EmiratesId { get; set; }
        public bool IsAutoEmail { get; set; }
        public bool IsAutoSMS { get; set; }
        public bool IsAutoWhatsApp { get; set; }
        public bool IsSysCalDOB { get; set; }
        public int PatientVisitID { get; set; }
        public int ReasonType { get; set; }
        public int RefferalType { get; set; }
        public string? RefTypeothers { get; set; }
        public int PhysNo { get; set; }
        public string? LastVisitDate { get; set; }
        public decimal LastConfees { get; set; }
        public List<prevApptProcedureDtl>? prevApptProcedureDtl { get; set; }
        public string CancelReason { get; set; }
        public string RescheduleReason { get; set; }
        public int? MachineNo {  get; set; }
    }
    public class prevApptProcedureDtl
    {
        public int ServiceNo { get; set; }
        public string? ServiceType { get; set; }
        public decimal Amount { get; set; }
    }
    public class OPDPatientDTOResponse
    {
        public string? AppointmentNo { get; set; }
        public int OPDPatientNo { get; set; }
        public int PatientNo { get; set; }
        public string? PatientName { get; set; }
        public string? PhysicianName { get; set; }
        public int VenBranchNo { get; set; }
    }
    public class OPDDiagnosisDTOResponse
    {
        public int PhysicianDiagnosisNo { get; set; }
        public string? AppointmentDateTime { get; set; }
        public string? AppointmentNo { get; set; }
        public int PhysicianNo { get; set; }
        public Int16 OutputTypeNo { get; set; }
        public int VenBranchNo { get; set; }
        public string? AppointmentDate { get; set; }
    }


    public class OPDDiagnosisDTORequest
    {
        public int PatientNo { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public int TemplateNo { get; set; }
        public int TempDiseaseNo { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public List<OPDDrugsList> DrugsList { get; set; }
        public List<OPDServiceList> TestList { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int PhysicianNo { get; set; }
        public int ReferralPhysicianNo { get; set; }
        public string CheifComplaints { get; set; }
        public string PresentingComplaints { get; set; }
        public string PastHistory { get; set; }
        public string PhysicalExamination { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string SystemicExamination { get; set; }
        public string NutritionalSpec { get; set; }
        public string DiagnosisFollowup { get; set; }
        public string followUpCommands { get; set; }
        public string followUpDate { get; set; }
        public int FollowOPDNo { get; set; }
        public Int16 OutputTypeNo { get; set; }
        public string GeneralCommands { get; set; }
        public string CheifComplaintslst { get; set; }
    }

    public class OPDServiceList
    {
        public string TestType { get; set; }
        public int TestNo { get; set; }
        public string TestDate { get; set; }
        public string Remarks { get; set; }
        public int IsRadiology { get; set; }
    }
    public class OPDDrugsList
    {
        public int ProductMasterNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FrequencyNo { get; set; }
        public string MedicineintakeNo { get; set; }
        public string RootNo { get; set; }
        public string DosageNo { get; set; }
        public string Remarks { get; set; }
        public string Instruction { get; set; }
    }
    public class OPDPatientBookingList
    {
        public Int64 Row_Num { get; set; }
        public string AppointmentDate { get; set; }
        public string SessionCode { get; set; }
        public string sessionDesc { get; set; }
        public string AppointmentTime { get; set; }
        public int BookingStatus { get; set; }
        public string classname { get; set; }
    }

    public class OPDPatientMedicineList
    {
        public Int64 Row_Num { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public string MedicineType { get; set; }

    }
    public class OPDOPDPatientHistory
    {
        public int OPDPatientAppointmentNo { get; set; }
        public string AppointmentNo { get; set; }
        public string AppointmentDateTime { get; set; }

    }
    public class OPDPatientVitalList
    {
        public Int64 Row_Num { get; set; }
        public Int32 VitalMasterNo { get; set; }
        public int TestNo { get; set; }
        public int SubTestNo { get; set; }
        public string VitalDatetime { get; set; }
        public string TestName { get; set; }
        public string result { get; set; }
        public string UnitsName { get; set; }
    }
    public class SearchOPDPatientRequest
    {
        public int Searchkey { get; set; }
        public string? Searchvalue { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Userno { get; set; }
    }
    public class SearchOPDPatientDataRequest
    {
        public int Searchkey { get; set; }
        public int Searchvalue { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class SearchOPDPatientVitalRequest
    {
        public int PatientNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }

    public class OPDPatientBookingRequest
    {
        public string AppointmentDate { get; set; }
        public int MachineNo { get; set; }
        public int PhysicianNo { get; set; }
        public int SpecializationNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PhysicianVenueBranchNo { get; set; }
        public int BookingType { get; set; }
    }
    public class OPDPatientDTOList
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public int OPDPatientNo { get; set; }
        public int oPDPatientAppointmentNo { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string patientID { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public bool IsSysCalDOB { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public int AppointmentMode { get; set; }
        public int SpecializationNo { get; set; }
        public int PhysicianNo { get; set; }
        public string Reason { get; set; }
        public string AppointmentDateTime { get; set; }
        public string AppointmentDate { get; set; }
        public string Appointmenttime { get; set; }
        public string ArrivedDateTime { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsVIP { get; set; }
        public int AppointmentStatus { get; set; }
        public string AppointmentNo { get; set; }
        public string specializationName { get; set; }
        public string PhysicianName { get; set; }
        public bool IsNew { get; set; }
        public string? PaymentStatus { get; set; }
        public string? EmiratesId { get; set; }
        public bool IsAutoEmail { get; set; }
        public bool IsAutoSMS { get; set; }
        public bool IsAutoWhatsApp { get; set; }
        public Int16 OutputTypeNo { get; set; }
        public string BookingDateTime { get; set; }
        public int VenBranchNo { get; set; }
        public string? VenueBranchName { get; set; }
        public string? VisitId { get; set; }
        public int PatientVisitID { get; set; }
        public int ReasonType { get; set; }
        public int RefferalType { get; set; }
        public string? RefTypeothers { get; set; }
        public int PhysNo { get; set; }
        public string? physName { get; set; }
        public string? cancelReason { get; set; } = string.Empty;
    }

    public class OPDPatientOPDData
    {
        public int PhysicianDiagnosisNo { get; set; }
        public int PatientNo { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public string Remarks { get; set; }
        public int TemplateNo { get; set; }
        public int TempDiseaseNo { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public string CheifComplaints { get; set; }
        public string PresentingComplaints { get; set; }
        public string PastHistory { get; set; }
        public string PhysicalExamination { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string SystemicExamination { get; set; }
        public string NutritionalSpec { get; set; }
        public string DiagnosisFollowup { get; set; }
        public string followUpCommands { get; set; }
        public string followUpDate { get; set; }
        public int FollowOPDNo { get; set; }
        public string GeneralCommands { get; set; }
        public string CheifComplaintslst { get; set; }
    }
    public class OPDPatientoutputData
    {
        public int OPDAppointmentNo { get; set; }
        public int PatientNo { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public string Remarks { get; set; }
        public int TemplateNo { get; set; }
        public int TempDiseaseNo { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public string CheifComplaints { get; set; }
        public string PresentingComplaints { get; set; }
        public string PastHistory { get; set; }
        public string PhysicalExamination { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string SystemicExamination { get; set; }
        public string NutritionalSpec { get; set; }
        public string DiagnosisFollowup { get; set; }
        public List<OPDPatientOPDDrugData> DrugsList { get; set; }
        public List<OPDPatientOPDTestData> TestList { get; set; }
        public string followUpCommands { get; set; }
        public string followUpDate { get; set; }
        public int FollowOPDNo { get; set; }
        public string generalCommands { get; set; }
        public string CheifComplaintslst { get; set; }
    }
    public class OPDPatientOPDDrugData
    {
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int FrequencyNo { get; set; }
        public int RootNo { get; set; }
        public int DosageNo { get; set; }
        public int MedicineintakeNo { get; set; }
        public string Remarks { get; set; }
        public string Instruction { get; set; }
    }
    public class OPDPatientOPDTestData
    {
        public int TestNo { get; set; }
        public string TestDisplayName { get; set; }
        public string TestType { get; set; }
        public int IsRadiology { get; set; }
        public string TestDate { get; set; }
        public string Remarks { get; set; }
    }
    public class ProductInsReq
    {
        public string? ProductName { get; set; }
        public int Venueno { get; set; }
        public int Venuebranchno { get; set; }
        public int UserNo { get; set; }
    }

    public class ProductInsRes
    {
        public int ProductNo { get; set; }
    }
    public partial class OPDTreatmentPlanProAppointmentlist
    {
        public int performingPhyAppointmentNo { get; set; }
        public int oPDPROCNo { get; set; }
        public int oPDTreatmentNo { get; set; }
        public int testNo { get; set; }
        public string? testName { get; set; }
        public string? appointmentDateTime { get; set; }
        public int performPhysicianNo { get; set; }
        public string? performPhysicianName { get; set; }
        public int specializationNo { get; set; }
        public string? bookingStatus { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class ResOPDperformPhysicianAPPT
    {
        public int AppointmentNo { get; set; }
    }
    public partial class OPDTreatmentPlanPharmacy
    {
        public int oPDTreatmentPlanPharmacyNo { get; set; }
        public int oPDTreatmentNo { get; set; }
        public int productMasterNo { get; set; }
        public string? productMasterName { get; set; }
        public int daily { get; set; }
        public int am { get; set; }
        public int pm { get; set; }
        public int weekly { get; set; }
        public int asNeeded { get; set; }
    }
    public partial class OPDTreatmentPlanProcedures
    {
        public int oPDTreatmentPlanProceduresNo { get; set; }
        public int oPDTreatmentNo { get; set; }
        public int testNo { get; set; }
        public string? testName { get; set; }
        public int scheduleEveryNo { get; set; }
        public string? scheduleEveryName { get; set; }
        public int frequencyNo { get; set; }
        public string? frequencyName { get; set; }
        public int daySunday { get; set; }
        public int dayMonday { get; set; }
        public int dayTuesday { get; set; }
        public int dayWednesday { get; set; }
        public int dayThursday { get; set; }
        public int dayFriday { get; set; }
        public int daySaturday { get; set; }
        public int totalTreatments { get; set; }
        public int performPhysicianNo { get; set; }
        public string? performPhysicianName { get; set; }
        public string? nextAppointmentDate { get; set; }
        public int specializationNo { get; set; }
        public decimal rate { get; set; }
    }
    public partial class OPDTreatmentPlanRes
    {
        public int oPDTreatmentNo { get; set; }
        public int appointmentNo { get; set; }
        public int patientNo { get; set; }
        public string? nextAppointmentDate { get; set; }
    }
    public partial class TreatmentPlanResponse
    {
        public int oPDTreatmentNo { get; set; }
    }
    public class OPDDiagnosisDTOFollowupResponse
    {
        public string? AppointmentNo { get; set; }
    }
    public class OPDBeforeAfterImageListResponse
    {
        public int ResultNo { get; set; }
    }
    public partial class ImageListResponse
    {
        public int ResultNo { get; set; }
    }
    public class OPDApptDetails
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 Row_num { get; set; }
        public Int32 Sno { get; set; }
        public string? PatientID { get; set; }
        public int PatientVisitID { get; set; }
        public int PatientNo { get; set; }
        public string? PrimaryId { get; set; }
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public string? ageType { get; set; }
        public string? gender { get; set; }
        public string? ageGender { get; set; }
        public string? AppointmentDateTime { get; set; }
        public string? AppointmentDate { get; set; }
        public string? Appointmenttime { get; set; }
        public string? ArrivedDateTime { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public string? AppointmentNo { get; set; }
        public bool IsNew { get; set; }
        public int ProcedureNo { get; set; }
        public string? ProcedureName { get; set; }
        public decimal ProcedureAmount { get; set; }
        public string? MobileNumber { get; set; }
        public int ApptStatus { get; set; }
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public int VenBranchNo { get; set; }

        public string? PrevApptNo { get; set; }
        public string? PrevApptDate { get; set; }
        public decimal PrevApptConsAmount { get; set; }
        public int PrevProcedureNo { get; set; }
        public string? PrevProcedureName { get; set; }
        public decimal PrevProcedureAmount { get; set; }
        public int PrevPhysicianNo { get; set; }
        public string? PrevPhysicianName { get; set; }
        public Int16 OutputTypeNo { get; set; }
    }
    public class OPDPatientDisVsDrugDetails
    {
        public int diseaseVsProductMappingNo { get; set; }
        public Int16 diseaseMasterNo { get; set; }
        public int masterNo { get; set; }
        public string? diseaseName { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public int rootNo { get; set; }
        public int frequencyNo { get; set; }
        public int dosageNo { get; set; }
        public int intakeNo { get; set; }
    }
    public class OPDPatientDisVsInvDetails
    {
        public int diseaseVsTestMappingNo { get; set; }
        public Int16 diseaseMasterNo { get; set; }
        public int masterNo { get; set; }
        public string? diseaseName { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public string? testCode { get; set; }
        public string? testName { get; set; }
        public string? testType { get; set; }
        public int deptNo { get; set; }
    }
    public class OPDBeforeAfterImageList
    {
        public int imagingNo { get; set; }
        public int physicianDiagnosisNo { get; set; }
        public int appointmentNo { get; set; }
        public int patientNo { get; set; }
        public string? b_Type { get; set; }
        public string? b_FileName { get; set; }
        public string? b_FileType { get; set; }
        public string? b_PathName { get; set; }
        public string? b_Src { get; set; }
        public string? a_Type { get; set; }
        public string? a_FileName { get; set; }
        public string? a_FileType { get; set; }
        public string? a_PathName { get; set; }
        public string? a_Src { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public Boolean Status { get; set; }
        public string? createDateTime { get; set; }
        public Boolean Includingreport { get; set; }
    }
    public class drugresponse
    {
        public int productNo { get; set; }
        public int rootNo { get; set; }
        public int frequencyNo { get; set; }
        public int dosageNo { get; set; }
        public int intakeNo { get; set; }
    }
    public partial class PhysicianAmount
    {
        public int PhysicianNo { get; set; }
        public decimal Amount { get; set; }
    }
    public class SearchOPDMachinePatient
    {
        public Int64 Row_Num { get; set; }
        public int OPDPatientNo { get; set; }
        public string description { get; set; }
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
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentNo { get; set; }
        public int MachineNo { get; set; }
        public int PatientVisitID { get; set; }
        //public bool? isNew { get; set; }
    }
    public class OPDDoctorList
    {
        public Int64 Row_Num { get; set; }
        public int PhysicianNo { get; set; }
        public string PhysicianName { get; set; }
        public string Qualification { get; set; }
        public int SpecializationNo { get; set; }
        public string SpecializationName { get; set; }
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public int DayNo { get; set; }
        public string DayDate { get; set; }
        public int NoofVisits { get; set; }
        public int NoofBooked { get; set; }
        public Decimal Amount { get; set; }
        public string OpdNotes { get; set; }
    }
    public class OPDPatientMachineBookingList
    {
        public Int64 Row_Num { get; set; }
        public string AppointmentDate { get; set; }
        public string SessionCode { get; set; }
        public string AppointmentTime { get; set; }
        public int BookingStatus { get; set; }
        public string classname { get; set; }
    }
    public class OPDPatientMachineDTOList
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public int OPDPatientNo { get; set; }
        public int oPDPatientAppointmentNo { get; set; }
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
        public int AppointmentMode { get; set; }
        public int MachineNo { get; set; }
        public string Reason { get; set; }
        public string AppointmentDateTime { get; set; }
        public string AppointmentDate { get; set; }
        public string Appointmenttime { get; set; }
        public string ArrivedDateTime { get; set; }
        public int AppointmentStatus { get; set; }
        public string AppointmentNo { get; set; }
        public string? MachineName { get; set; }
        public string? VenueBranchName { get; set; }
        public int PatientVisitID { get; set; }
        public string? PatientID { get; set; }
        public int VenBranchNo { get; set; }
        public int ReasonType { get; set; }
        public int RefferalType { get; set; }
        public string? RefTypeothers { get; set; }
        public int PhysNo { get; set; }
        public string? physName { get; set; }
        public string BookingDateTime { get; set; }
        public bool? isNew { get; set; }
    }
    public class OPDPatientMachineResponse
    {
        public string? AppointmentNo { get; set; }
        public int OPDPatientNo { get; set; }
    }
    public class SearchOPDPatient
    {
        public Int64 Row_Num { get; set; }
        public int PatientNo { get; set; }
        public int OPDPatientNo { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public string description { get; set; }
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
        public DateTime AppointmentDateTime { get; set; }
        public string? AppointmentNo { get; set; }
        public int PhysicianNo { get; set; }
        public bool IsSysCalDOB { get; set; }
        public int PatientVisitID { get; set; }
    }
    public class OPDApptDetailsreq
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public int userNo { get; set; }
        public string? MobileNo { get; set; }
        public string? AppointmentNo { get; set; }
        public string? PatientName { get; set; }
        public int PatientVisitNo { get; set; }
        public string? PatientID { get; set; }
    }
    public class OPDDoctorMainDayList
    {
        public int DayNo { get; set; }
        public string DayName { get; set; }
        public string DayDate { get; set; }
    }
    public class OPDDoctorMainBranchList
    {
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public Decimal Amount { get; set; }
        public List<OPDDoctorMainDayList> DayList { get; set; }
    }
    public class OPDDoctorMainList
    {
        public int PhysicianNo { get; set; }
        public string PhysicianName { get; set; }
        public string Qualification { get; set; }
        public int SpecializationNo { get; set; }
        public string SpecializationName { get; set; }
        public int NoofVisits { get; set; }
        public int NoofBooked { get; set; }
        public Decimal Amount { get; set; }
        public string OpdNotes { get; set; }
        public List<OPDDoctorMainBranchList> BranchList { get; set; }
    }
    public class OPDPatientDoctorDTOList
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public int OPDPatientNo { get; set; }
        public int oPDPatientAppointmentNo { get; set; }
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
        public int AppointmentMode { get; set; }
        public int SpecializationNo { get; set; }
        public int PhysicianNo { get; set; }
        public string Reason { get; set; }
        public string AppointmentDateTime { get; set; }
        public string AppointmentDate { get; set; }
        public string Appointmenttime { get; set; }
        public string ArrivedDateTime { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsVIP { get; set; }
        public int AppointmentStatus { get; set; }
        public string AppointmentNo { get; set; }
        public string specializationName { get; set; }
        public string PhysicianName { get; set; }
        public bool IsNew { get; set; }
        public string? VenueBranchName { get; set; }
        public Int16 OutputTypeNo { get; set; }
        public int venBranchNo { get; set; }
        public string BookingDateTime { get; set; }
    }
    public partial class OPDTreatmentPlan
    {
        public int oPDTreatmentNo { get; set; }
        public int appointmentNo { get; set; }
        public int patientNo { get; set; }
        public string? nextAppointmentDate { get; set; }
        public decimal totalAmount { get; set; }
        public List<OPDTreatmentPlanProcedures> lstProcedures { get; set; }
        public List<OPDTreatmentPlanPharmacy> lstpharmacy { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public partial class PatientDocUploadReq
    {
        public string? ApptNo { get; set; }
        public int? patientNumber { get; set; }
        public Int16? venueNo { get; set; }
        public int? venueBranchNo { get; set; }
        public int? docType { get; set; }
    }
    public partial class drugreq
    {
        public int venueNo { get; set; }
        public int productNo { get; set; }

    }
    public partial class SkinHistoryReq
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string? ApptNo { get; set; }
    }
    public class OPDStatusLogListRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string AppointmentNo { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public int Userno { get; set; }
        public int PageIndex { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int SpecialisationNo { get; set; }
        public int PhysicianNo { get; set; }
        public int Status { get; set; }
    }
    public class OPDStatusLogListResponse
    {
        public int RowNo { get; set; }
        public int OPDStatusLogNo { get; set; }
        public string AppointmentNo { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public int OPDPatientNo { get; set; }
        public string AppointmentBookedOn { get; set; }
        public int AppointmentBookedBy { get; set; }
        public string AppointmentBookedUser { get; set; }
        public string PatientArrivedOn { get; set; }
        public int PatientArrivedBy { get; set; }
        public string PatientArrivedUser { get; set; }
        public string AppointmentCancelledOn { get; set; }
        public int AppointmentCancelledBy { get; set; }
        public string AppointmentCancelledUser { get; set; }
        public string AppointmentRescheduledOn { get; set; }
        public int AppointmentRescheduledBy { get; set; }
        public string AppointmentRescheduledUser { get; set; }
        public string DiagnosisCompletedOn { get; set; }
        public int DiagnosisCompletedBy { get; set; }
        public string DiagnosisCompletedUser { get; set; }
        public string ReferralInprogressOn { get; set; }
        public int ReferralInprogressBy { get; set; }
        public string ReferralInprogressUser { get; set; }
        public string VitalCompletedOn { get; set; }
        public int VitalCompletedBy { get; set; }
        public string VitalCompletedUser { get; set; }
        public string PatientName { get; set; }
        public string PhysicianName { get; set; }
        public string BooktoArriavalTime { get; set; }
        public string ArrivaltoVitalTime { get; set; }
        public string VitaltoDiagnosisTime { get; set; }
        public string TATTime { get; set; }
        public string AgeGender { get; set; }
        public string Specialization { get; set; }
        public string VenueBranchName { get; set; }
    }
}