using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class CashExpenseDTO
    {
        public int ExpenseEntryNo { get; set; }
        public Int16 ExpenseTypeNo { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDtTm { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public int IssueFromUserNo { get; set; }
        public char IssueToType { get; set; }
        public int IssueToUserNo { get; set; }
        public string IssueToUserName { get; set; }
        public string ExpenseCategory { get; set; }
        public string IssuedBy { get; set; }
        public string IssueToTypeDesc { get; set; }
        public string IssuedToDesc { get; set; }
        public bool Status { get; set; }
        public int totalRecord {  get; set; }
    }
    public class SaveCashExpenseDTO
    {
        public int VenueNo { get; set; }
        public int ExpenseTypeNo { get; set; }
        public int ExpenseEntryNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int CreatedBy { get; set; }
        public string IssuedDate { get; set; }
        public int IssueFrom { get; set; }
        public int IssueToType { get; set; }
        public int IssueToUserNo { get; set; }
        public string IssueToUserName { get; set; }
        public int ExpenseCate { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public bool Status { get; set; }
        public string IssuedToDesc { get; set; }
        public string TransactionID { get; set; }
    }
    public class GetCashExpenseParam
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int IssuedByNo { get; set; }
        public int IssuedToTypeNo { get; set; }
        public int IssuedTo { get; set; }
        public int ExpenseCategory { get; set; }
        public int pageIndex {  get; set; }
        public string Type { get; set; }
        public string Fromdate { get; set; }
        public string ToDate { get; set; }
        public int UserNo {  get; set; }
    }
    public class InsertCashExpenseDTO
    {
     public int ExpenseEntryNo { get; set; }
     public string TransactionID { get; set; }
    }

    //cash expense approval request
    public class GetReqExpensesParam
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int? IssuedByNo { get; set; }
        public int? IssuedToTypeNo { get; set; }
        public int? IssuedTo { get; set; }
        public int? ExpenseCategory { get; set; }
        public int PageIndex { get; set; }
        public string? Type { get; set; }
        public string? Fromdate { get; set; }
        public string? Todate { get; set; }
        public Int16 ApprovalReq { get; set; }
    }
    public class GetReqExpensesResponse
    {
        public int RowNo { get; set; }
        public int ExpenseEntryNo { get; set; }
        public Int16? ExpenseTypeNo { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDtTm { get; set; }
        public string? Reason { get; set; }
        public decimal? Amount { get; set; }
        public int? IssueFromUserNo { get; set; }
        public char? IssueToType { get; set; }
        public int? IssueToUserNo { get; set; }
        public string? IssueToUserName { get; set; }
        public string? ExpenseCategory { get; set; }
        public string? IssuedBy { get; set; }
        public string? IssueToTypeDesc { get; set; }
        public string? IssuedToDesc { get; set; }
        public bool? Status { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string? BranchName { get; set; }
        public string? ApprovalReason { get; set; }
        public string? ApprovedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public bool? IsApproved { get; set; }
    }
    public class ApproveExpenses
    {
        public int VenueNo { get; set; }
        public int? ExpenseEntryNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int? CreatedBy { get; set; }
        public decimal? Amount { get; set; }
        public string? Reason { get; set; }
        public bool? IsApprove { get; set; }
    }
    //
}