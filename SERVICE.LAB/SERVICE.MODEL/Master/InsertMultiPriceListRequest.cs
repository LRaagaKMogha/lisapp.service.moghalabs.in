using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class InsertMultiPriceListRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

        
        public List<Multipricelist> serviceDetails { get; set; }

    }
    public partial class Multipricelist
    {
        public int rateListNo { get; set; }
        public int serviceNo { get; set; }
        public decimal discountAmount { get; set; }
        public decimal modifiedAmount { get; set; }
        public int departmentNo { get; set; }
        public bool isChecked { get; set; }
    }

    //public class lstMultiPriceList
    //{
    //    [JsonProperty("departmentNo")]
    //    public long DepartmentNo { get; set; }

    //    [JsonProperty("serviceNo")]
    //    public long ServiceNo { get; set; }

    //    [JsonProperty("rateListNo")]
    //    public long RateListNo { get; set; }

    //    [JsonProperty("modifiedAmount")]
    //    public long ModifiedAmount { get; set; }

    //    [JsonProperty("discountAmount")]
    //    public long DiscountAmount { get; set; }

    //    [JsonProperty("isChecked")]
    //    public bool IsChecked { get; set; }

    //    //public int rateListNo { get; set; }
    //    //public int serviceNo { get; set; }
    //    //public decimal discountAmount { get; set; }
    //    //public decimal modifiedAmount { get; set; }
    //    //public int departmentNo { get; set; }
    //    //public bool isChecked { get; set; }
    //}

    public class InsertMultiPriceListResponse
    {
        public int resultStatus { get; set; }
    }
}

