using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblOrganism
    {
        public int OrganismNo { get; set; }
        public string OrganismMccode { get; set; }
        public int OrganismTypeNo { get; set; }
        public string OrganismCode { get; set; }
        public string OrganismName { get; set; }
        public string Notes { get; set; }
        public int SequenceNo { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }

    public partial class lstotdrugmap
    {
        public int organismantibioticmapno { get; set; }
        public int organismtypeno { get; set; }
     //   public int antibioticno { get; set; }
        public string antibioticmccode { get; set; }
       // public string antibioticname { get; set; }
      //  public int sequenceno { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
    }
}
