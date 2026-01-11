using System;
using System.Collections.Generic;

namespace Service.Model
{
    public class ReferrerDueDTO
    {

    }

    public class RefWiseDueRequest
    {
        public Byte RefTypeNo { get; set; }
        public Int16 ReferrerNo { get; set; }
        public Int16 PageIndex { get; set; }
        public Int16 UserNo { get; set; }
        public Int16 VenueNo { get; set; }
        public Int16 BranchNo { get; set; }
        public Int16 FilterBranchNo { get; set; }
    }
    public class RefWiseDueResponse
    {
        public Int16 TotalRecords { get; set; }
        public Int16 PageIndex { get; set; }
        public Int16 PageVisitCount { get; set; }
        public decimal PageDueAmount { get; set; }
        public Int16 ReportVisitCount { get; set; }
        public decimal ReportDueAmount { get; set; }
        public List<RefWiseDueResponseList> RefWiseDueResponseList { get; set; }
    }
    public class RefWiseDueResponseList
    {
        public Int16 RowNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public string ReferralType { get; set; }
        public Byte RefTypeNo { get; set; }
        public Int16 ReferrerNo { get; set; }
        public string ReferrerName { get; set; }
        public Int16 VisitCount { get; set; }
        public decimal DueAmount { get; set; }
    }
    public class RefWiseDueResponseData
    {
        public Int16 TotalRecords { get; set; }
        public Int16 PageIndex { get; set; }
        public Int16 PageVisitCount { get; set; }
        public decimal PageDueAmount { get; set; }
        public Int16 ReportVisitCount { get; set; }
        public decimal ReportDueAmount { get; set; }
        public Int16 RowNo { get; set; }
        public Int16 BranchNo { get; set; }
        public string BranchName { get; set; }
        public string ReferralType { get; set; }
        public Byte RefTypeNo { get; set; }
        public Int16 ReferrerNo { get; set; }
        public string ReferrerName { get; set; }
        public Int16 VisitCount { get; set; }
        public decimal DueAmount { get; set; }
    }
}
