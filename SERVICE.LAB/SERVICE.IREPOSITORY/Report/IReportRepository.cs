using Service.Model;
using Service.Model.Report;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository
{
    public interface IReportRepository
    {
        Task<ReportOutput> GetReport(ReportDTO ReportItem);
        string GetGridReport(ReportDTO ReportItem);
        List<TATResponse> GetTATReport(CommonFilterRequestDTO ReportItem);
        List<TATResponseNew> GetTATReportNew(CommonFilterRequestDTO ReportItem);
        List<TATReportDetailsResponse> GetTATReportDetails(CommonFilterRequestDTO ReportItem);
        List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO RequestItem);
        List<AuditLogResponse> GetAudioLog(CommonFilterRequestDTO RequestItem);
        List<AdvancePaymentList> GetAdvancePayment(CommonFilterRequestDTO RequestItem);
        List<AuditHistory> GetAuditHistory(int FirstAuditLogNo, int SecondAuditLogNo, int Type);
        AdvancePaymentListResponse InsertAdvancePayment(AdvancePaymentListRequest RequestItem);

        List<CashExpenseDTO> GetCashExpenses(GetCashExpenseParam RequestItem);
        InsertCashExpenseDTO InsertCashExpenses(SaveCashExpenseDTO RequestItem);
        Task<ReportOutput> GetReportbylist(ReportDTO ReportItem);
        ReportOutput GetSensitivityReport(CommonFilterRequestDTO ReportItem);
        ReportOutput GetWorkloadReport(CommonFilterRequestDTO ReportItem);
        ReportOutput GetNonGynaeWorkLoadReport(CommonFilterRequestDTO ReportItem);
        ReportOutput GetCytoQCReport(CommonFilterRequestDTO ReportItem);
        ReportOutput GetStatisticalReport(CommonFilterRequestDTO ReportItem);
        ReportOutput GetCytoWorkloadReport(SlidePrintingRequest ReportItem);
        InsertCashExpenseDTO ApproveExpenses(ApproveExpenses RequestItem);
        List<GetReqExpensesResponse> GetReqExpenses(GetReqExpensesParam RequestItem);
        string GetStaffBillingDetailsMIS(UserFrontOfficeMIS RequestItem);
        string GetUserwiseFrontOfficeMIS(UserFrontOfficeMIS RequestItem);
    }
}




