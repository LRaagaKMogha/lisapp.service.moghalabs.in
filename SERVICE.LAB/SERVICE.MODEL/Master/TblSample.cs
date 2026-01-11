using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class TblSample
    {
        public int SampleNo { get; set; }
        public string? SampleCode { get; set; }
        public string? SampleName { get; set; }
        public string? SampleDisplayText { get; set; }
        public string? SampleNature { get; set; }
        public string? SampleVolume { get; set; }
        public string? Suffix { get; set; }
        public string? Prefix { get; set; }
        public bool? IsActive { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int SequenceNo { get; set; }
        public int updateseqNo { get; set; }
    }

    public class sampleMasterRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class sampleMasterResponse
    {
        public int SampleNo { get; set; }

        public int LastPageIndex { get; set; }
    }
}
  