using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Inventory
{
    public class StoreProductMappingRequestDTO
    {
        public int VenueNo { get; set; }
        public int ProductNo { get; set; } = 0;
        public int StoreNo { get; set; } = 0;
        public string CommonCodeMapping { get; set; }
        public bool? IsActiveorNot { get; set; }
        public int PageIndex { get; set; } = 1;
    }

    public class StoreToProductMappingAddDTO
    {
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public short MinQty { get; set; }
        public short MaxQty { get; set; }
        public short ReOrderQty { get; set; }
        public int ProductStoreNo { get; set; }
        public bool Status { get; set; }
        public short Aging { get; set; }
    }

    public class StoreToProductMappingDTO
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public long RowNo { get; set; }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public short MinQty { get; set; }
        public short MaxQty { get; set; }
        public short ReOrderQty { get; set; }
        public bool Status { get; set; }
        public short Aging { get; set; }
    }

    public class StoreProductMappingInsertDTO
    {
        public int VenueNo { get; set; }
        public int UserNo { get; set; }
        public List<StoreProductItemDTO> StoreProductList { get; set; }
    }

    public class StoreProductItemDTO
    {
        public int ProductNo { get; set; }
        public int StoreNo { get; set; }
        public short MinQty { get; set; }
        public short MaxQty { get; set; }
        public short ReOrderQty { get; set; }
        public int ProductStoreNo { get; set; }
        public bool Status { get; set; }
        public short Aging { get; set; }
    }

}
