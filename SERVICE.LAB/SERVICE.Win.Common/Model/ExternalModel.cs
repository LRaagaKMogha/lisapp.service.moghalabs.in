using System;
using System.Collections.Generic;

namespace Service.Win.Common
{
    public class Tokenresponse
    {
        public string token { get; set; }
    }
    public class ExternalUserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class ExternalPatientDTO
    {
        public string icmr_id { get; set; }
        public string patient_id { get; set; }
        public string patient_name { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string age_in { get; set; }
        public string contact_number { get; set; }
        public string contact_number_belongs_to { get; set; }
        public string fathers_name { get; set; }
        public string nationality { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string pincode { get; set; }
        public string aadhar_number { get; set; }
        public string passport_number { get; set; }
        public string patient_category { get; set; }
        public string address { get; set; }
        public string occupation { get; set; }
        public string aarogya_setu_app_downloaded { get; set; }
        public string contact_with_lab_confirmed_patient { get; set; }
        public string srf_id { get; set; }
        public string sample_cdate { get; set; }
        public string sample_rdate { get; set; }
        public string sample_type { get; set; }
        public string sample_id { get; set; }
        public string sample_collected_from { get; set; }
        public string status { get; set; }
        public string symptoms { get; set; }
        public string other_symptoms { get; set; }
        public string date_of_onset_of_symptoms { get; set; }
        public string underlying_medical_condition { get; set; }
        public string other_underlying_medical_conditions { get; set; }
        public string hospitalized { get; set; }
        public string hospital_name { get; set; }
        public string hospitalization_date { get; set; }
        public string hospital_state { get; set; }
        public string hospital_district { get; set; }
        public string sample_tdate { get; set; }
        public string testing_kit_used { get; set; }
        public string covid19_result_egene { get; set; }
        public string ct_value_screening { get; set; }
        public string orf1b_confirmatory { get; set; }
        public string ct_value_orf1b { get; set; }
        public string rdrp_confirmatory { get; set; }
        public string ct_value_rdrp { get; set; }
        public string final_result_of_sample { get; set; }
        public string transport_mode_used_to_visit_testing_facility { get; set; }
        public string repeat_sample { get; set; }
        public string remarks { get; set; }
        public string lab_id { get; set; }
        public string lab_code { get; set; }
        public int Is_record { get; set; }

    }
    public class ExteranlPatientResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public string success_msg { get; set; }
        public int icmr_id { get; set; }
        public string surveillance_id { get; set; }
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
    public class OPDDoctorMainBranchList
    {
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public Decimal Amount { get; set; }
        public List<OPDDoctorMainDayList> DayList { get; set; }
    }
    public class OPDDoctorMainDayList
    {
        public int DayNo { get; set; }
        public string DayName { get; set; }
        public string DayDate { get; set; }
    }
}
