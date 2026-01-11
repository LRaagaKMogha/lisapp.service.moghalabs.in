using Service.Model;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IOPDPatientRepository
    {
        OPDPatientDTOResponse InsertOPDPatient(OPDPatientOfficeDTO req);
        List<OPDPatientDTOList> GetOPDPatientList(CommonFilterRequestDTO RequestItem);
        List<OPDPatientBookingList> GetPatientBookingList(OPDPatientBookingRequest RequestItem);
        List<SearchOPDPatient> GetPatientData(SearchOPDPatientRequest RequestItem);
        List<OPDPatientVitalList> GetPatientVitalData(SearchOPDPatientVitalRequest RequestItem);
        List<ServiceSearchDTO> GetOPDService(int VenueNo, int VenueBranchNo, int doctorNo, int type);
        List<OPDPatientMedicineList> GetOPDMedicineData(int VenueNo, int VenueBranchNo, int doctorNo);
        OPDPatientOPDData GetPatientOPDData(SearchOPDPatientDataRequest RequestItem);
        List<OPDPatientOPDTestData> GetPatientOPDTestData(SearchOPDPatientDataRequest RequestItem);
        List<OPDPatientOPDDrugData> GetPatientOPDDrugData(SearchOPDPatientDataRequest RequestItem);
        OPDDiagnosisDTOResponse InsertPhysicianDiagnosis(OPDDiagnosisDTORequest objDTO);
        List<OPDOPDPatientHistory> OpDPatientHistory(SearchOPDPatientRequest RequestItem);
        List<OPDPatientDoctorDTOList> GetOPDDoctorPatientList(CommonFilterRequestDTO RequestItem);
        List<OPDApptDetails> GetOPDApptDetails(OPDApptDetailsreq RequestItem);
        List<OPDDoctorMainList> GetOPDDoctorList(OPDPatientBookingRequest RequestItem);
        int GetOPDPhysicianAmount(OPDPatientOfficeDTO RequestItem);
        List<Humanbodyparts> Gethumanbodyparts(int VenueNo, int VenueBranchNo, int type);
        List<OPDBeforeAfterImageList> GetOPDBeforeANDAfterImageList(int VenueNo, int VenueBranchNo, int OPDPatientNo, int PatientNo, int VisitNo);
        List<OPDPatientDisVsDrugDetails> GetOPDPatientMasterDefinedDrugDetails(string type, int PatientNo, int VenueNo, int VenueBranchNo);
        List<OPDPatientDisVsInvDetails> GetOPDPatientMasterDefinedInvDetails(string type, int PatientNo, int VenueNo, int VenueBranchNo);
        OPDTreatmentPlan GetOPDTreatmentPlanDetails(OPDTreatmentPlan req);
        List<OPDBulkFileUpload> GetPatientDocumentDetails(PatientDocUploadReq Req);
        List<DocumentInfo> GetPatientDocumentAll(PatientDocUploadReq obj);
        List<drugresponse> GetDrugDetails(drugreq RequestItem);
        List<ClinicalHistory> GetSkinHistory(SkinHistoryReq req);
        List<ClinicalHistory> GetopdclinicalHistory(SkinHistoryReq req);
        OPDDiagnosisDTOFollowupResponse InsertFollowUpAppointment(OPDDiagnosisDTORequest req);
        CommonAdminResponse InsertopdclinicHistory(InsertSkinHistory insertSkinHistory);
        CommonAdminResponse InsertSkinHistory(InsertSkinHistory insertSkinHistory);
        OPDBeforeAfterImageList OPDImagingFile(OPDBeforeAfterImageList objDTO);
        OPDBeforeAfterImageListResponse InserOPDImaging(OPDBeforeAfterImageList objDTO);
        TreatmentPlanResponse InsertTreatmentPlan(OPDTreatmentPlan req);
        ImageListResponse OPDImagingIncludingreport(OPDBeforeAfterImageList objDTO);
        List<displaylist> GetDisplayView(int VenueNo, int VenueBranchNo, int type);
        List<OPDStatusLogListResponse> GetOPDStatusLogList(OPDStatusLogListRequest RequestItem);
        List<SearchOPDMachinePatient> GetPatientMachineData(SearchOPDPatientRequest RequestItem);
        List<OPDPatientMachineBookingList> GetPatientMachineBookingList(OPDPatientBookingRequest RequestItem);
        List<OPDPatientMachineDTOList> GetOPDPatientMachineList(CommonFilterRequestDTO RequestItem);
        OPDPatientMachineResponse InsertOPDMachinePatient(OPDPatientOfficeDTO req);

    }
}
