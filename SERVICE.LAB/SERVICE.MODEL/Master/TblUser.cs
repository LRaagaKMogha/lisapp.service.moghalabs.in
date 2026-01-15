using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Service.Model
{
    public partial class TblUser
    {
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsRider { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? otp { get; set; }
        public DateTime? otpexpiry { get; set; }
        public int IsAdmin { get; set; }
        public bool? IsProvisional { get; set; }
        public int LoginAttempt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool? IsEditLabResults { get; set; }
        public bool? IsResultEntryHIV { get; set; }
        public bool? IsResultEntryVIP { get; set; }
        public bool? IsPriceShow { get; set; }
        public bool? isLock { get; set; }
        public bool? Isadaccess { get; set; }
        public bool? IsadmultifactorAccess { get; set; }
        public string? ladpsecretkey { get; set; }
        public bool? IsAbnormalAvail { get; set; }
        public bool? IsPOApproval { get; set; }
        public bool? IsGrnApproval { get; set; }
        public bool? IsGrnReturnApproval { get; set; }
        public bool? IsStockAdjustmentApproval { get; set; }
        public bool? IsConsumptionApproval { get; set; }
        public bool? IsClientApproval { get; set; }
        public string? Gender { get; set; }
    }
}
