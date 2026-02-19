using Service.Model;
using Service.Model.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IRepository
{
    public interface IResultRepository
    {
        Task<Resultrtn> AutoApprovalResult(Objresult originalReq, Resultrtn req);
        Task<BulkResultSaveResponse> AutoApprovalBulkResult(List<Objbulkresult> req);
        List<Lstsearchresultvisit> SearchResultVisit(Requestsearchresultvisit req);
        List<Lstresultvisit> GetResultVisit(Requestresultvisit req);
        Task<Objresult> GetResult(Requestresult req);
        Objresult GetVisitHistoy(requestDeltaresult req);
        List<Deltaresult> GetDeltaresult(requestDeltaresult req);
        Task<Resultrtn> InsertResult(Objresult req);
        Objresultmb GetResultMB(Requestresult req);
        Task<Resultrtn> InsertResultMB(Objresultmb req);
        List<Orgtypeantibiotic> GetOrgtypeantibiotic(Requestresult req);
        Objresulttemplate GetResultTemplate(Requestresult req);
        Task<Resultrtn> InsertResultTemplate(Objresulttemplate req);
        Objrecall GetRecall(Requestresult req);
        RecallResponse InsertRecall(Objrecall req);
        List<Objresulttemplate> GetBulkResult(Requestresultvisit req);
        Task<Resultrtn> InsertBulkResult(Objbulkresulttemplate req);
        List<Covidresult> GetCovidWorkOrder(CovidWorkOrderreq req);
        Resultrtn InsertCovidWorkOrder(CovidWorkOrder req);
        List<ApprovalDoctorResponse> ApprovalDoctorList(ApprovalDoctorRequest req);
        List<PatientImpressionResponse> GetPatientImpression(CommonFilterRequestDTO RequestItem);
        Task<Objresult> GetResultExceptUserMapped(Requestresult req);
        List<Mergeresultresponse> GetMergedResult(Mergeresultrequest RequestItem);
        Savemergeresultresponse InsertMergedResult(Savemergeresultrequest req);
        List<Culturehistoryreponse> GetCultureHistory(Culturehistoryrequest req);
        Task<Objresult> GetAnalyserResult(AnalyserRequestresult req);
        Task<Resultrtn> InsertAnalyserResult(Objresult req);
        List<Objbulkresult> GetBulkResultEtry(AnalyserRequestresult req);
        BulkResultSaveResponse SaveBulkResultEtry(List<Objbulkresult> req);
        List<BulkCultureResultResponse> GetCultureBulkResultEtry(GetBulkCultureResultRequest req);
        BulkCultureResultSaveResponse SaveCultureBulkResultEtry(SaveBulkCUltureResultRequest req);
        Task<ReportOutput> GetPatientImpressionReport(GetImpressionReportRequest RequestItem);
        List<GetResultforVisitMergeResponse> GetVisitMerge(VisitMergeRequest req);
        InsertVisitMergeResponse SaveVisitMerge(SaveResultforVisitMergeResponse req);
        List<LogicCommentsRespose> GetLogicComments(LogicCommentsRequest req);
        Extrasubtestflagbasedformularesponse GetExtrasubtestbasedformula(Extrasubtestflagbasedformularequest req);
        List<GetOldResultThroughDIResponse> GetOldResultThroughDIs(GetOldResultThroughDIRequest req);
        Task<ObjUpdPartialEntryFlagResponse> UpdatePartialResultFlag(ObjUpdPartialEntryFlagRequest req);
        List<PendingVisitdetailsRes> GetPendingVisitdetails(PendingVisitdetailsReq req);
    }
}