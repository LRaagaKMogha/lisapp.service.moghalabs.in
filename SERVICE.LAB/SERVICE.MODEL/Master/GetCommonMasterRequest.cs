using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetCommonMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int masterNo { get; set; }
        public int TotalRecords { get; set; }
        public int SampleNo { get; set; }
        public int MethodNo { get; set; }
        public string? MethodName { get; set; }
        public string? MethodDisplayText { get; set; }
        public bool Status { get; set; }
        public int ManufacturerNo { get; set; }
        public int ProductCategoryNo { get; set; }
        public int ProductTypeNo { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Type { get; set; }
        public int SupplierNo { get; set; }
        public int? viewvenuebranchno { get; set; }
        public string MenuType { get; set; }
    }
    public class GetDeptMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public int masterNo { get; set; }
        public int type { get; set; }
        public int Departmentnumber { get; set; }
        public Int16 MainDeptNo { get; set; }
        public string? searchtext { get; set; }
        public string? DepartmentName { get; set; }
    }
    public class GetAllGRNRequest : GetCommonMasterRequest
    {
        public string MenuType { get; set; }
        public int GRNStatus { get; set; }
        public string InvoiceNo { get; set; }
    }
    public class GetAllPORequest : GetCommonMasterRequest
    {
        public string MenuType { get; set; }
    }
    public class GetAllGRNReturnRequest : GetCommonMasterRequest
    {
        public string MenuType { get; set; }
    }
}
