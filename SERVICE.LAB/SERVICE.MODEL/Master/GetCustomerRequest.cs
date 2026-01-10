using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetCustomerRequest
    {
        public long customerNumber { get; set; }
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int IsFranchisee { get; set; } = 0;
        public int custType { get; set; }
        public int PayType { get; set; }
        public int IsApproval { get; set; }
        public int? viewvenuebranchno { get; set; }
    }
}

