using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Inventory
{
    public class ProductSupplierMappingRequestDTO
    {
        public int VenueNo { get; set; }
        public int ProductNo { get; set; } = 0;
        public int SupplierNo { get; set; } = 0;
        public string CommonCodeMapping { get; set; }
        public bool? IsActiveorNot { get; set; }
        public int PageIndex { get; set; } = 1;
    }
    public class SupplierToProductMappingAddDTO
    {
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public int LeadPeriod { get; set; }
        public int CreditDays { get; set; }
        public int ProductSupplierMappingNo { get; set; }
        public bool Status { get; set; }

    }

    public class ProductToSupplierMappingAddDTO
    {
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public int LeadPeriod { get; set; }
        public int CreditDays { get; set; }
        public int ProductSupplierMappingNo { get; set; }
        public bool Status { get; set; }
    }

    public class SupplierToProductMappingDTO
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public long RowNo { get; set; }
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public int LeadPeriod { get; set; }
        public int CreditDays { get; set; }
        public bool Status { get; set; }
    }

    public class ProductToSupplierMappingDTO
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public long RowNo { get; set; }
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public int LeadPeriod { get; set; }
        public int CreditDays { get; set; }
        public bool Status { get; set; }
    }
    public class ProductSupplierMappingInsertDTO
    {
        public int VenueNo { get; set; }
        public int UserNo { get; set; }
        public List<ProductSupplierItemDTO> ProductSupplierList { get; set; }
    }

    public class ProductSupplierItemDTO
    {
        public int ProductNo { get; set; }
        public int SupplierNo { get; set; }
        public int LeadInDays { get; set; }
        public decimal PurchaseOutRate { get; set; }
        public int CreditDays { get; set; }
        public int ProductSupplierMappingNo { get; set; }
        public bool Status { get; set; }
    }
}
