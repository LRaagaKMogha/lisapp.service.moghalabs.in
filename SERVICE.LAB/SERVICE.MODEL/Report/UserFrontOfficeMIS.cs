using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Report
{
    public class UserFrontOfficeMIS
    {
        public int UserNo { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int ReferralTypeNo { get; set; }
        public int ReferralNo { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public int DeptNo { get; set; }
        public int BillUserNo { get; set; }
        public int ViewVenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string Filter { get; set; }
        public int DiscountTypeNo { get; set; }
    }
}
