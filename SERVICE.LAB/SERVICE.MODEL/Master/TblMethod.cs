using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblMethod
    {
        public int MethodNo { get; set; }
        public string MethodName { get; set; }
        public string? MethodDisplayText { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class MethodResponse
    {
        public int MethodNo { get; set; }
        public int LastPageIndex { get; set; }
    }
}
