using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblUnits
    {
        public int UnitsNo { get; set; }
        public string? UnitsCode { get; set; }
        public string? UnitsName { get; set; }
        public bool? Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsInventory { get; set; }
       
    }
    public partial class rtnUnit
    {   
        public int unitNo { get; set; }

        public int LastPageIndex { get; set; }
    }
    public partial class reqUnits
    {
        public int unitsNo { get; set; }        
        public bool status { get; set; }     
        public bool isinventory { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class lstunits
    {
        public int unitsno { get; set; }
        public string? unitscode { get; set; }
        public string? unitsname { get; set; }
        public bool? status { get; set; }
        public int venueNo { get; set; }
        public int VenueBranchNo { get; set; }        
        public bool? isinventory { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
}
