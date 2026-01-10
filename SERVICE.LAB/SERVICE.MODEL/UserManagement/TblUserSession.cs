using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblUserSession
    {
        public int UserSessionNo { get; set; }
        public int UserNo { get; set; }
        public string? Ipaddress { get; set; }
        public string? ClientSysteminfo { get; set; }
        public DateTime? LogInDateTime { get; set; }
        public DateTime? LogOutdateTime { get; set; }
        public bool? IsClosed { get; set; }
        public int? LoginType { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public bool? IsProvisional { get; set; }
        public string Token { get; set; }
        public bool? IsEditLabResults { get; set; }
        public bool? IsResultEntryHIV { get; set; }
        public bool? IsResultEntryVIP { get; set; }
        public bool? isLock { get; set; }
        public bool? IsAbnormalAvail { get; set; }
        public bool? IsPOApproval { get; set; }
        public bool? IsGrnApproval { get; set; }
        public bool? IsGrnReturnApproval { get; set; }
        public bool? IsStockAdjustmentApproval { get; set; }
        public bool? IsConsumptionApproval { get; set; }
        public bool? IsClientApproval { get; set; }
    }
}
