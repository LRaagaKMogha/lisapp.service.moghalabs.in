using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Inventory
{
    public partial class GetStockAlertResponse
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int ManufacturerNo { get; set; }
        public string ManufacturerName { get; set; }
        public int MinQty { get; set; }
        public int MaxQty { get; set; }
        public int Opening_TestCnt { get; set; }
        public int Closing_TestCnt { get; set; }
        public int Adjust_TestCnt { get; set; }
        public string Alert {  get; set; }
        public string AgingStatus { get; set; }
        public int Aging { get; set; }
        public string VenueBranchName { get; set; }
        public string StoreName { get; set; }


    }
    public partial class StockAlertRequest
    {
        public int VenueNo { get; set; }
        public int Venuebranchno { get; set; }
        public int PageIndex { get; set; }
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
    }
}
