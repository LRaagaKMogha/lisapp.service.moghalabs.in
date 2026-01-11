using Service.Model;
using Service.Model.PatientInfo;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.PatientInfo
{
    public interface IPatientInfoRepository
    {
        List<PatientInfoResponse> GetPatientInfoDetails(CommonFilterRequestDTO RequestItem);
        List<PatientInfoListResponse> GetPatientListDetails(CommonFilterRequestDTO RequestItem);
        List<CustomSearchResponse> GetCustomSearch(CommonSearchRequest searchRequest);
        EditPatientResponse UpdatePatientDetails(EditPatientRequest editPatientRequest);
        Task<ReportOutput> PrintPatientReport(ReportRequestDTO requestDTO);
        List<PatientInfoResponse> GetPatientVisitHistory(CommonFilterRequestDTO RequestItem);
        List<ReasonDetailsResponse> GetServiceRejectReason(ReasonDetailsRequest RequestItem);
        int UpdateMasterData(SyncMasterRequestDTO RequestItem);
        List<PatientsMasterResponse> GetPatientsMaster(PatientsMasterRequest RequestItem);
        int SavePatientsMaster(PatientsMasterSave RequestItem);
        PatientmergeResponseDTO SavePatientMerge(PatientmergeDTO RequestItem);
        EditPatientResponse UpdateSampleDetails(List<EditSampleRequest> editSampleRequest);
        List<GetSampleResponse> GetPatientSampleInfo(GetSampleRequest RequestItem);
        EditPatientResponseNew UpdateSampleDetailsNew(EditSampleRequestNew editSampleRequest);
        List<GetPatientVisitActionHistoryResponse> GetPatientVisitActionHistory(CommonFilterRequestDTO RequestItem);
        Task<ReportOutputhc> PrintHcBill(ReportRequestDTO req);
        List<GetHcDocumentsDetailsResponse> GetHcDocumentsDetails(GetHcDocumentsDetailsRequest request);
        List<PatientInfoeLabResponseDTO> GeteLabPatientInfoList(PatientInfoRequestDTO RequestItem);
    }
}


