using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class DocVsSerRequest
    {
        public int DoctorNo { get; set; }
        public Int16 venueNo { get; set; }
        public int pageIndex { get; set; }
    }

    public class DocVsSerResponse
    {
        public int DoctorNo { get; set; }
        public string? DoctorName { get; set; }
        public string? CreatedOn { get; set; }
        public Int16 venueNo { get; set; }
        public string? ModifiedOn { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }
    public partial class DocVsSerGetReq
    {
        public Int16 venueNo { get; set; }
        public int DoctorNo { get; set; }
        public int DeptNo { get; set; }
        public string? ServiceType { get; set; }
        public int ServiceNo { get; set; }
    }
    public partial class DocVsSerGetRes
    {
        public int DoctorServiceNo { get; set; }
        public int DoctorNo { get; set; }
        public int DeptNo { get; set; }
        public string? ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string? DepartmentName { get; set; }
        public string? TestName { get; set; }
        public char Type { get; set; }
        public decimal Value { get; set; }
        public string? ServiceTypeName { get; set; }
        public bool isChecked { get; set; }
        public int RowNo { get; set; }
    }
    public partial class DocVsSerInsReq
    {
        public int DoctorNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int Mode { get; set; }
        public List<getdoclst> getdoclst { get; set; }
    }
    public partial class getdoclst
    {
        public int DoctorServiceNo { get; set; }
        public bool isChecked { get; set; }
        public string? AType { get; set; }
        public decimal Value { get; set; }
        public string? ServiceType { get; set; }
        public int ServiceNo { get; set; }

    }
    public partial class DocVsSerInsRes
    {
        public int DoctorServiceNo { get; set; }
    }
    public class DocVsSerAppReq
    {
        public int ApprovedBy { get; set; }
        public int VenueBranchNo { get; set; }
        public string? Type { get; set; }
        public Int16 VenueNo { get; set; }
        public string? Fromdate { get; set; }
        public string? ToDate { get; set; }

    }
    public class DocVsSerAppRes
    {
        public string? VisitID { get; set; }
        public string? VisitDTTM { get; set; }
        public string? FullName { get; set; }
        public string? DeptName { get; set; }
        public int DeptNo { get; set; }
        public string? ServiceName { get; set; }
        public int ServiceNo { get; set; }
        public decimal BillAmount { get; set; }
        public decimal AAmount { get; set; }
        public string? ServiceType { get; set; }
        public int OrderListNo { get; set; }
        public string? AgeGender { get; set; }
        public int PatientVisitNo { get; set; }
    }
    public partial class DocVsSerAppdetailsReq
    {
        public int DoctorNo { get; set; }
        public Int16 VenueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public class DocVsSerAppdetailsRes
    {
        public string? DoctorName { get; set; }
        public int DoctorProfMastNo { get; set; }
        public string? TranDtTm { get; set; }
        public string? TranNo { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }
        public int DoctorNo { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }

    }
    public partial class DocVsSerProfInsReq
    {
        public int DoctorNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public List<getdocVsSerlst> getdocVsSerlst { get; set; }
    }
    public partial class getdocVsSerlst
    {
        public int PatientVisitNo { get; set; }
        public int OrderListNo { get; set; }
        public string? ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public int DeptNo { get; set; }
        public decimal Amount { get; set; }

    }
    public partial class DocVsSerProfInsRes
    {
        public int DoctorProfMastNo { get; set; }
    }
}

