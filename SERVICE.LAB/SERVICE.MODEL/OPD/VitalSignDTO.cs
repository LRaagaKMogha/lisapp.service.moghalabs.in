using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    #region Vital Details
    public partial class VitalSignDTO
    {
        public int RowNo { get; set; }
        public int vitalmasterno { get; set; }
        public int VitalDetailNo { get; set; }
        public int patientno { get; set; }
        public int patientType { get; set; }
        public string entryon { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public string testName { get; set; }
        public string subTestName { get; set; }
        public int tSeqNo { get; set; }
        public int sSeqNo { get; set; }
        public string resultType { get; set; }
        public string resultFlag { get; set; }
        public string resultComments { get; set; }
        public string result { get; set; }
        public int unitNo { get; set; }
        public string unitsName { get; set; }
        public string lLColumn { get; set; }
        public string hLCoulumn { get; set; }
        public string displayRR { get; set; }
        public string testCode { get; set; }
        public string formulaText { get; set; }
        public bool isFormulaParameter { get; set; }
    }
    public partial class VitalSignDTORequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int vitalno { get; set; }
        public int patientno { get; set; }
        public int userNo { get; set; }
    }
    public partial class SaveVitalSignDTORequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int userno { get; set; }
        public int patientno { get; set; }
        public int opdpatientno { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public List<SaveVitalSignDTO> lstSaveVitalSignDTO { get; set; }
    }
    public partial class SaveVitalSignDTO
    {
        public int vitalmasterNo { get; set; }
        public int vitalDetailNo { get; set; }
        public int patientType { get; set; }
        public int patientNo { get; set; }
        public DateTime vitalDateTime { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public int testSeqNo { get; set; }
        public int subTestSeqNo { get; set; }
        public string resultType { get; set; }
        public string resultFlag { get; set; }
        public string result { get; set; }
        public string resultComment { get; set; }
        public int unitNo { get; set; }
        public string llColumn { get; set; }
        public string hlColumn { get; set; }
        public string displayRR { get; set; }


    }
    public class SaveVitalSignDTOResponse
    {
        public int VitalMasterNo { get; set; }
    }
    public partial class VitalSignMastersRequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int mastertype { get; set; }
        public int patientno { get; set; }
        public int userno { get; set; }
    }
    public partial class VitalSignMastersResponse
    {
        public int mastercode { get; set; }
        public string mastervalue { get; set; }
        public string master { get; set; }
        public byte SeqNo { get; set; }
        public Int16 Typeno { get; set; }
        public string ICDCode { get; set; }
    }
    #endregion

    #region Allergy Details
    public partial class GetAllergyRequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int patientno { get; set; }
        public int OPDAppointmentNo { get; set; }
        public int userno { get; set; }
    }
    public partial class GetAllergyResponse
    {
        public int RowNo { get; set; }
        public int AllergyRecordingNo { get; set; }
        public int OPDAppointmentNo { get; set; }
        public int PatientNo { get; set; }
        public Int16 AllergyTypeNo { get; set; }
        public string AllergyType { get; set; }
        public Int16 AllergyNo { get; set; }
        public string Allergy { get; set; }
        public Int16 AllergyReactionNo { get; set; }
        public string AllergyReaction { get; set; }
        public Int16 AllergyStatusNo { get; set; }
        public string AllergyStatus { get; set; }
        public string Comments { get; set; }
        public string? Others { get; set; }
        public bool Status { get; set; }
    }
    public partial class SaveAllergyRequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int patientno { get; set; }
        public int userno { get; set; }
        public int opdpatientno { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public List<SaveAllergyDeatileRequest> lstAllergyDetails { get; set; }
    }
    public partial class SaveAllergyDeatileRequest
    {
        public int AllergyRecordingNo { get; set; }
        public int PatientNo { get; set; }
        public Int16 AllergyTypeNo { get; set; }
        public Int16 AllergyMasterNo { get; set; }
        public Int16 AllergyReactionNo { get; set; }
        public Int16 AllergyStatusNo { get; set; }
        public string Comments { get; set; }
        public string? Others { get; set; }
        public bool Status { get; set; }
    }
    public partial class SaveAllergyResponse
    {
        public int allergyRecordingno { get; set; }
    }
    #endregion

    #region Diseases Details
    public partial class GetDiseasesRequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int patientno { get; set; }
        public int OPDAppointmentNo { get; set; }
        public int userno { get; set; }
    }
    public partial class GetDiseasesResponse
    {
        public int RowNo { get; set; }
        public int DiseaseRecordingNo { get; set; }
        public int OPDAppointmentNo { get; set; }
        public int PatientNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }
        public string DiseaseName { get; set; }
        public string Comments { get; set; }
        public bool Status { get; set; }
        public string ICDCode { get; set; }
    }
    public partial class SaveDiseasesRequest
    {
        public int venueno { get; set; }
        public int venuebno { get; set; }
        public int patientno { get; set; }
        public int userno { get; set; }
        public int opdpatientno { get; set; }
        public int OPDPatientAppointmentNo { get; set; }
        public List<SaveDiseasesDeatilRequest> lstDiseasesDetails { get; set; }
    }
    public partial class SaveDiseasesDeatilRequest
    {
        public int DiseaseRecordingNo { get; set; }
        public int PatientNo { get; set; }
        public Int16 DiseaseMasterNo { get; set; }        
        public string Comments { get; set; }
        public bool Status { get; set; }
    }
    public partial class SaveDiseasesResponse
    {
        public int diseaserecordingno { get; set; }
    }
    #endregion
    #region Vaccine Details
    public partial class GetVaccineScheduleRequest
    {
        public int? PatientNo { get; set; }
        public bool IsAdult { get; set; }
    }

    public partial class lstVaccineSchedule
    {
        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public string? Description { get; set; }
        public int? Stage { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateOfVaccination { get; set; }
    }

    public partial class SaveVaccineRecordRequest
    {
        public int PatientNo { get; set; }
        public bool IsAdult { get; set; }
        public int VaccineId { get; set; }
        public DateTime DateOfVaccination { get; set; }
        public DateTime? DueDate { get; set; }
        public int VaccinatedBy { get; set; }
    }

    public partial class SaveVaccineRecordDTORequest
    {
        public List<SaveVaccineRecordRequest> lstVaccineRecord { get; set; }
    }

    public partial class GetLatestPatientVisitRequest
    {
        public int PatientNo { get; set; }
    }

    public partial class lstPatientLatestVisit
    {
        public int PatientNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
    }

    #endregion

    public partial class GetVitalResultResponse
    {
        public string? Vitaldatetime { get; set; }
    }
}

