using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository
{
    public interface IPatientReportRepository
    {
        List<lstpatientreport> GetPatientReport(requestpatientreport req);
        Task<List<ReportOutput>> PrintPatientReport(PatientReportDTO PatientItem);
        List<TblCsatransaction> GetCsaTransaction(CsaRequest req);
        int InsertCSAAcknowledgement(TblCsatransaction req);
        int InsertReportLog(PatientReportLog req);
        List<PatientReportLogRespose> GetReportLog(PatientReportLog req);
       Task<List<ReportOutput>> PrintDelateReport(PatientReportDTO requestDTO);
        List<GetAuditReportRes> GetAuditTrailReport(GetAuditReportReq req);
        AuditTrailVisitHistoryResponse GetAuditTrailVisitHistory(GetAuditTrailVisitReq req);
        List<lstamendedpatientreport> GetAmendedPatientReport(requestamendedpatientreport req);
        Task<List<ReportOutput>> PrintAmendedPatientReport(AmendedPatientReportDTO PatientItem);
        List<GetATSubCatyMasterSearchResponse> GetATSubCatyMasters(GetATSubCatyMasterSearchReq req);
        Task<string> GetPdfFileName(List<PatientReportDTO> PatientItem);
        Task<List<OPDReportOutput>> PrintOPDPatientReport(PatientReportOPDDTO PatientOPDItem);
    }
}
