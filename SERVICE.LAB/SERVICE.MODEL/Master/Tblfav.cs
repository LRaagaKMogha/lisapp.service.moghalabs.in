using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class Tblfav
    {
        public int FavoriteServiceNo { get; set; }

        public Int16 SequenceNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        // public int sequenceNo { get; set; }
        public bool? Status { get; set; }

        public Int16 VenueNo { get; set; }

        public int VenueBranchNo { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public string ServiceName { get; set; }

        public string sType { get ; set; }

        public int sCode { get; set; }

        //public DateTime ModifiedOn { get; set; }

    }
    public partial class Tblgroup
    {
        public int GroupNo { get; set; }

        public string GroupDisplayName { get; set; }

        public bool? Status { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }

        public int GSequenceNo { get; set; }

    }

    public partial class Tblpack
    {
        public int PackageNo { get; set; }
        public string PackageDisplayName { get; set; }

        public bool? Status { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }

        public int PSequenceNo { get; set; }

    }
}




