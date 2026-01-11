using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.FrontOffice
{
    public class ClientBranchSamplePickupRequest
    {
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public short? SPType { get; set; }
        public int? SPTypeNo { get; set; }
        public int? RiderNo { get; set; }
        public int PageIndex { get; set; } = 1;
    }
    public class ClientBranchSamplePickupResponse
    {
        public int PageIndex { get; set; }
        public int Row_Num { get; set; }
        public int TotalRecords { get; set; }
        public int Sno { get; set; }
        public int SamplePickupNo { get; set; }
        public string SPTranNo { get; set; }
        public string SPTranDate { get; set; }
        public short SPType { get; set; }
        public int SPTypeNo { get; set; }
        public short SampleCount { get; set; }
        public string PickupDateTime { get; set; }
        public string RequesterInfo { get; set; }
        public int? RiderNo { get; set; }
        public string RiderName { get; set; }
        public string RiderAssignedByName { get; set; }
        public string RiderAssignedOn { get; set; }
        public int? RiderAssignedBy { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string ClientBranchName { get; set; }
    }

    public class ClientBranchSamplePickupInsertRequest
    {
        public int SamplePickupNo { get; set; }
        public short SPType { get; set; }
        public int SPTypeNo { get; set; }
        public short SampleCount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string RequesterInfo { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }

    public class ClientBranchSamplePickupInsertResponse
    {
        public int SamplePickupNo { get; set; }
    }
    public class ClientBranchSamplePickupRiderInsertRequest
    {
        public int SamplePickupNo { get; set; }
        public int RiderNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }

    public class ClientBranchSamplePickupRiderInsertResponse
    {
        public int SamplePickupNo { get; set; }
    }
}
