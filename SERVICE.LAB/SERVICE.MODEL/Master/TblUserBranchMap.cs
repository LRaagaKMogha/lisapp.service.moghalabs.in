using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblUserBranchMap
    {
        public int UserBranchMapNo { get; set; }
        public int UserNo { get; set; }
        public int MappingBranchNo { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? Isdefault { get; set; }
    }
}
