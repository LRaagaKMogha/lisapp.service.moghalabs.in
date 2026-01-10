using DEV.Model;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository
{
    public interface IResultRepository
    {
        Task<resultrtn> AutoApprovalResult(objresult originalReq, resultrtn req);
        Task<BulkResultSaveResponse> AutoApprovalBulkResult(List<objbulkresult> req);

        List<lstsearchresultvisit> SearchResultVisit(requestsearchresultvisit req);
        List<lstresultvisit> GetResultVisit(requestresultvisit req);
        Task<objresult> GetResult(requestresult req);
        objresult GetVisitHistoy(requestdeltaresult req);
        List<deltaresult> GetDeltaResult(requestdeltaresult req);
        Task<resultrtn> InsertResult(objresult req);
        objresultmb GetResultMB(requestresult req);
        resultrtn InsertResultMB(objresultmb req);
        List<orgtypeantibiotic> GetOrgTypeAntibiotic(requestresult req);
        objresulttemplate GetResultTemplate(requestresult req);
        resultrtn InsertResultTemplate(objresulttemplate req);

        objrecall GetRecall(requestresult req);
        recallResponse InsertRecall(objrecall req);

        List<objresulttemplate> GetBulkResult(requestresultvisit req);
        resultrtn InsertBulkResult(objbulkresulttemplate req);

        List<covidresult> GetCovidWorkOrder(covidWorkOrderreq req);
        resultrtn InsertCovidWorkOrder(covidWorkOrder req);
        List<ApprovalDoctorResponse> ApprovalDoctorList(ApprovalDoctorRequest req);
        List<PatientImpressionResponse> GetPatientImpression(CommonFilterRequestDTO RequestItem);
        Task<objresult> GetResultExceptUserMapped(requestresult req);
        List<mergeresultresponse> GetMergedResult(mergeresultrequest RequestItem);
        savemergeresultresponse InsertMergedResult(savemergeresultrequest req);
        List<culturehistoryreponse> GetCultureHistory(culturehistoryrequest req);
        Task<objresult> GetAnalyserResult(analyserrequestresult req);
        Task<resultrtn> InsertAnalyserResult(objresult req);
        List<objbulkresult> GetBulkResultEtry(analyserrequestresult req);
        BulkResultSaveResponse SaveBulkResultEtry(List<objbulkresult> req);
        List<BulkCultureResultResponse> GetCultureBulkResultEtry(GetBulkCultureResultRequest req);
        BulkCultureResultSaveResponse SaveCultureBulkResultEtry(SaveBulkCUltureResultRequest req);
        Task<ReportOutput> GetPatientImpressionReport(GetImpressionReportRequest RequestItem);
        List<GetResultforVisitMergeResponse> GetVisitMerge(VisitMergeRequest req);
        InsertVisitMergeResponse SaveVisitMerge(SaveResultforVisitMergeResponse req);
        List<logicCommentsRespose> GetLogicComments(logicCommentsRequest req);
        extrasubtestflagbasedformularesponse GetExtrasubtestbasedformula(extrasubtestflagbasedformularequest req);
        List<GetOldResultThroughDIResponse> GetOldResultThroughDIs(GetOldResultThroughDIRequest req);
        Task<objUpdPartialEntryFlagResponse> UpdatePartialResultFlag(objUpdPartialEntryFlagRequest req);
        List<PendingVisitDetailsRes> GetPendingVisitDetails(PendingVisitDetailsReq req);

    }
}

