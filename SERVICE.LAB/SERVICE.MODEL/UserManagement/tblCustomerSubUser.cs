using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class tblCustomerSubUser
    {
        public int CustomerSubUserNo { get; set; }
        public string? UserName { get; set; }
        public string? LoginName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? CustomerNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
    }

}