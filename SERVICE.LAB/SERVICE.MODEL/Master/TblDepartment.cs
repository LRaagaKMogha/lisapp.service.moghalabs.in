using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblDepartment
    {
        public int DepartmentNo { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentDisplayText { get; set; }
        public int DeptSequenceNo { get; set; }
        public bool IsSample { get; set; }
        public bool IsCytology { get; set; }
        public bool IsHisto { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int MainDeptNo { get; set; }
        public string? DoctorName1 { get; set; }
        public string? DoctorSign1 { get; set; }
        public string? DoctorDescription1 { get; set; }
        public string? DoctorName2 { get; set; }
        public string? DoctorSign2 { get; set; }
        public string? DoctorDescription2 { get; set; }
        public string? DoctorName3 { get; set; }
        public string? DoctorSign3 { get; set; }
        public string? DoctorDescription3 { get; set; }
        public string? LanguageText { get; set; }
    }

    public partial class GetMaindepartment
    {
        public Int32 PageIndex { get; set; }
        public int? DepartmentNo { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }
        public int? MainDeptNo { get; set; }
        public string? MainDeptName { get; set; }
        public string? DepartmentDisplayText { get; set; }
        public int? DeptSequenceNo { get; set; }
        public bool? IsSample { get; set; }

        public string? DoctorName1 { get; set; }
        public string? DoctorSign1 { get; set; }

        public string? DoctorDescription1 { get; set; }

        public string? DoctorName2 { get; set; }
        public string? DoctorSign2 { get; set; }

        public string? DoctorDescription2 { get; set; }

        public string? DoctorName3 { get; set; }
        public string? DoctorSign3 { get; set; }

        public string? DoctorDescription3 { get; set; }
        public int VenueNo { get; set; }
        public bool? Status { get; set; }
        public bool? IsCytology { get; set; }
        public bool? IsHisto { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? totalRecords { get; set; }
        public string? LanguageText { get; set; }
    }

    public partial class DepartMentLangCodeReq
    {
        public int DeptNo { get; set; }
        public string? DeptType { get; set; }
        public int LanguageCode { get; set; }
        public string? LanguageText { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int UserNo { get; set; }

        public bool? IsEdit { get; set; }


    }
    public partial class DepartMentLangCodeRes
    {

        public int LanguageId { get; set; }

        public string? Message { get; set; }

    }
    public partial class GetDeptLangCodeReq
    {
        public int DeptNo { get; set; }
        public string? DeptType { get; set; }

        public int VenueNo { get; set; }

        public int VenueBranchNo { get; set; }
    }

    public partial class GetDeptLangCodeRes
    {
        public int TotalRecords { get; set; }
        public int DeptNo { get; set; }

        public int LanguageCode { get; set; }

        public string? LanguageText { get; set; }
        public bool? LangStatus { get; set; }
    
    }

    public partial class InsertDeptRes
    {
        public int DeptNo { get; set; }

    }
}