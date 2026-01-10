using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DEV.Model
{
    public class CommentGetReq
    {
        public int? CommentsMastNo { get; set; }
        public Int16? venueNo { get; set; }
        public Int16? CategoryNo { get; set; }
        public int? pageIndex { get; set; }
        public Int32 SubCatyNo { get; set; }
    }
    public partial class CommentGetRes
    {
        public int? CommentsMastNo { get; set; }
        public Int16 CategoryNo { get; set; }
        public string? ShortCode { get; set; }
        public string? Description { get; set; }
        public int seqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int userNo { get; set; }
        public int updateseqNo { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public Int16 SubCatyNo { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public Int16 DeptNo { get; set; }
        public string DeptName { get; set; }
        public bool? Abnormal { get; set; }
    }
    public partial class CommentInsReq
    {
        public int CommentsMastNo { get; set; }
        public Int16 CategoryNo { get; set; }
        public string? ShortCode { get; set; }
        public string? Description { get; set; }
        public int SeqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public Int16 SubCatyNo { get; set; }
        public bool? Abnormal { get; set; }
    }
    public partial class InsertCommentSubCategoryReqest
    {
        public Int16 CategoryNo { get; set; }
        public Int16 SubCatyNo { get; set; }
        public string? SubCatyDesc { get; set; }
        public Int16 DeptNo { get; set; }
        public int SeqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int userNo { get; set; }
    }
    public partial class FetchCommentSubCategoryReqest
    {
        public Int16 CategoryNo { get; set; }
        public Int16 SubCatyNo { get; set; }
        public Int16 VenueNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class FetchCommentSubCategoryResponse
    {
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public Int16 CategoryNo { get; set; }
        public string CatyDesc { get; set; }
        public Int16 SubCatyNo { get; set; }
        public string SubCatyDesc { get; set; }
        public Int16 DeptNo { get; set; }
        public string DeptDesc { get; set; }
        public Int16 SeqNo { get; set; }
        public bool? Status { get; set; }
        public int updateseqNo { get; set; }
    }
    public class CommentInsRes
    {
        public int? CommentsMastNo { get; set; }
    }
    public class CommentSubCatyInsResponse
    {
        public Int16 SubCatyNo { get; set; }
    }

    public partial class GetNationRaceReq
    {
        public int CommonNo { get; set; }
        public string Type { get; set; }
        public int? pageIndex { get; set; }

    }
    public partial class GetNationRaceRes
    {
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public string Type { get; set; }
        public Int64 Id { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

    }
    public partial class InsNationRaceReq
    {
        public int CommonNo { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

    }
    public partial class InsNationRaceRes
    {
        public int CommonNo { get; set; }
        public int LastPageIndex { get; set; }  

    }
    public partial class TemplateComment
    {
        public long CH_QcNo { get; set; }
        public int PatientVisitNo { get; set; }
        public Int16 TestNo { get; set; }
        public int CommentNo { get; set; }
        public string? CommentShortCode { get; set; }
        public string? CommentDesc { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchno { get; set; }
        public int UserNo { get; set; }
        public string? Type { get; set; }
        public string? PageCode { get; set; }
    }

    public partial class TemplateCommentRes
    {
        public long CH_QcNo { get; set; }
        public int PatientVisitNo { get; set; }
        public Int16 TestNo { get; set; }
        public int CommentNo { get; set; }
        public string? CommentShortCode { get; set; }
        public string? CommentDesc { get; set; }
    }

    public partial class InsertBankMasterr
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankShortName { get; set; }
        public string BankType { get; set; }
        public string HeadOfficeAddress { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string PostalCode { get; set; }
        public int CreatedBy { get; set; }
        public int VenueNo { get; set; }
        //public string VenueBranchNo { get; set; }
        public bool? Status {  get; set; }
        public string StatusCode { get; set; }
        public int PageIndex { get; set; }
    }

    public partial class BankMasterResponse
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankShortName { get; set; }
        public string BankType { get; set; }
        public string HeadOfficeAddress { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string PostalCode { get; set; }
        public int CreatedBy { get; set; }
        public int VenueNo { get; set; }
        //public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
    public partial class InsertBankMastereq
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankShortName { get; set; }
        public string BankType { get; set; }
        public string HeadOfficeAddress { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string PostalCode { get; set; }
        public int CreatedBy { get; set; }
        public int VenueNo { get; set; }
        public bool? Status { get; set; }

    }
    public partial class InsertBankMasteres
    {
        public int BankID { get; set; }
        public bool? Status { get; set; }

    }

    public partial class InsertBankbranchreq
    {
        public int BranchID { get; set; }
        public int BankID { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string IFSCcode { get; set; }
        public string BranchAddress { get; set; }
        public string ContactNumbers { get; set; }
        public string EmailID { get; set; }
        public int CreatedBy { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int PageIndex { get; set; }
    }
    public partial class BankBranchResponse
    {
        public int BranchID { get; set; }
        public int BankID { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string IFSCcode { get; set; }
        public string BranchAddress { get; set; }
        public string ContactNumbers { get; set; }
        public string EmailID { get; set; }
        public int CreatedBy { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public string BankName { get; set; }
    }

    public partial class GetBankbranchreq
    {
        public int BranchID { get; set; }
        public int VenueNo { get; set; }
        public int PageIndex { get; set; }
    }

}
