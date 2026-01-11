using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Inventory
{
    public class StoreMasterRequestDTO
    {
        public int StoreID { get; set; }
        public int VenueNo { get; set; }
        public int Venuebranchno { get; set; }
        public int PageIndex { get; set; } = 1;
    }
    public class StoreMasterResponseDTO
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int StoreID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public bool? IsCenterStore { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
    }
    public class StoreDetails
    {
        public int StoreID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public bool? IsCenterStore { get; set; }
        public bool? Status { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
    }
    public class StoreMasterInsertDTO
    {
        public int StoreID { get; set; } = 0;
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public bool IsCenterStore { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? Status { get; set; }
    }
    public class StoreMasterInsertResponseDTO
    {
        public int StoreID { get; set; }
    }
}

