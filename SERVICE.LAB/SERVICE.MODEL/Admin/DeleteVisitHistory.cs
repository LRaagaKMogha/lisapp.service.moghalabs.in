using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Admin
{
    public class visitRequest
    {
        public string? visitId { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool? status { get; set; }
        public string? patientID { get; set; }
        public string? MobileNumber { get; set; }
        public int? ReferalType { get; set; }
        public int? CustomerNo { get; set; }
        public int? PhysicianNo { get; set; }
        public string? type { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public int? userNo { get; set; }
        public int? branchNo { get; set; }
        public int? visitNo { get; set; }
        public int? pageIndex { get; set; }
    }

    public class responsehistory
    {
        public string? visitId { get; set; }
        public string? venueBranchName { get; set; }
        public string? visitDtTm { get; set; }
        public string? patientID { get; set; }
        public string? patientName { get; set; }
        public string? referer { get; set; }
        public int? userNo { get; set; }
        public string? DeletedDtTm { get; set; }
        public int? VenueNo { get; set; }
        public bool? status { get; set; }
        public string? userName { get; set; }
        public int? pageIndex { get; set; }
        public int? TotalRecords { get; set; }
    }
}
