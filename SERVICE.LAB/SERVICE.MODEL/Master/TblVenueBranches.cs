using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblVenueBranches
    {
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public string VenueBranchDisplayText { get; set; }
        public string Address { get; set; }
        public int? CountryNo { get; set; }
        public int? StateNo { get; set; }
        public int? CityNo { get; set; }
        public int? AreaNo { get; set; }
        public int? Pincode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long? FaxNo { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobile { get; set; }
        public string TimeZone { get; set; }
        public bool? IsProcessing { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string IntegrationId { get; set; }
        public string IntegrationCode { get; set; }
        public int? ParentVenueBranchNo { get; set; }
        public string Domaincode { get; set; }
    }
}
