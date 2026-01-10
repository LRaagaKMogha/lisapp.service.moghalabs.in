using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetmultiPriceListResponse
    {
        public int? sNo { get; set; }
        public int rateListNo { get; set; }
        public string rateListName { get; set; }
        public int? serviceNo { get; set; }
        public string serviceCode { get; set; }
        public string serviceName { get; set; }
        public decimal? amount { get; set; }
        public decimal? discountAmount { get; set; }
        public int? departmentNo { get; set; }
        public string departmentName { get; set; }        
        public bool isChecked { get; set; }
    }

    public class GetmultiPriceListRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int rateListNo { get; set; }
        public int departmentNo { get; set; }
        public int serviceNo { get; set; }
    
    }
}
