using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetProductMasterResponse
    {
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int ManufacturerNo { get; set; }
        public string ManufacturerName { get; set; }
        public int CategoryNo { get; set; }
        public string CategoryName { get; set; }
        public int ProductTypeNo { get; set; }
        public string ProductTypeName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int PurchaseUnitNo { get; set; }
        public string PurchaseUnitName { get; set; }
        public Int16 purchaseUnitSize { get; set; }
        public int IssueUnitNo { get; set; }
        public string IssueUnitName { get; set; }
        public Int16 issueUnitSize { get; set; }
        public int ConsumptionUnitNo { get; set; }
        public string ConsumptionUnitName { get; set; }
        public Int16 consumptionUnitSize { get; set; }
        public int GenericNo { get; set; }
        public string GenericName { get; set; }
        public int MedicineTypeNo { get; set; }
        public string MedicineTypeName { get; set; }
        public int StrengthNo { get; set; }
        public string StrengthName { get; set; }
        public int LeadPeriodInDays { get; set; }
        public int TestCount { get; set; }
        public int HSNNo { get; set; }
        public string HSNName { get; set; }
        public bool Status { get; set; }
        public bool IsExpiry { get; set; }
        public bool IsBatchNo { get; set; }
        public bool IsNotified { get; set; }
        public bool IsNarcotic { get; set; }
        public bool IsNoDiscount { get; set; }
        public bool IsMRPRequired { get; set; }
        public bool IsKitLog { get; set; }
        public bool IsAutoIndent { get; set; }
        public bool IsAutoPR { get; set; }
        public bool IsEditPack { get; set; }
        public bool IsConsignment { get; set; }
        public bool IsSoundAlike { get; set; }
        public bool IsLookAlike { get; set; }
        public bool IsScheduleH1Drug { get; set; }
        public bool IsScheduleGDrug { get; set; }
        public bool IsScheduleDrug { get; set; }
        public bool IsHighRisk { get; set; }
        public string Instructions { get; set; }
        public string Specification { get; set; }
        public string VisualInspectionParameters { get; set; }
        public string StorageConditions { get; set; }
        public string Remarks { get; set; }
        public Int16 shelfLife { get; set; }
        public Int16 shelfLifePeriod { get; set; }
        public bool IsProcedure { get; set; }
        public bool IsAgeing { get; set; }
        public Int32 SupplierNo { get; set; }
    }
    public class GetSupplierMappingDTO
    {
        public long RowNum { get; set; }
        public int SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public decimal PurchaseOutrate { get; set; }
        public decimal PurchaseRental { get; set; }
        public decimal IssueOutrate { get; set; }
        public decimal IssueRental { get; set; }
        public int LeadPeriod { get; set; }
        public Int16 creditDays { get; set; }
    }
    public class SupplierMappingRequest
    {
        public int ProductNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class GetDepartmentMappingDTO
    {
        public long RowNum { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public int minQty { get; set; }
        public int maxQty { get; set; }
        public int reorderQty { get; set; }
        public int aging { get; set; }
        public string bin { get; set; }
    }
    public class postProductMasterDTO
    {
        public tbl_IV_ProductMaster tblproductMaster { get; set; }
        public List<GetSupplierMappingDTO> supplierlist { get; set; }
        public List<GetDepartmentMappingDTO> departmentlist { get; set; }

        public List<Fetchlookalike> lookalikelist { get; set; }

        public List<Fetchsoundalike> soundalikelist { get; set; }
        public List<Fetchsubproduct> subProduct { get; set; }

        public int userNo { get; set; }
    }
    public class FetchProductListResponse
    {
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int ManufacturerNo { get; set; }
        public string ManufacturerName { get; set; }
        public int CategoryNo { get; set; }
        public string CategoryName { get; set; }
        public int ProductTypeNo { get; set; }
        public string ProductTypeName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int PurchaseUnitNo { get; set; }
        public string PurchaseUnitName { get; set; }
        public int IssueUnitNo { get; set; }
        public string IssueUnitName { get; set; }
        public int ConsumptionUnitNo { get; set; }
        public string ConsumptionUnitName { get; set; }
        public int GenericNo { get; set; }
        public string GenericName { get; set; }
        public int MedicineTypeNo { get; set; }
        public string MedicineTypeName { get; set; }
        public int StrengthNo { get; set; }
        public string StrengthName { get; set; }
        public int LeadPeriodInDays { get; set; }
        public int TestCount { get; set; }
        public int HSNNo { get; set; }
        public string HSNName { get; set; }
        public bool Status { get; set; }
        public bool IsExpiry { get; set; }
        public bool IsBatchNo { get; set; }
        public bool IsNotified { get; set; }
        public bool IsNarcotic { get; set; }
        public bool IsNoDiscount { get; set; }
        public bool IsMRPRequired { get; set; }
        public bool IsKitLog { get; set; }
        public bool IsAutoIndent { get; set; }
        public bool IsAutoPR { get; set; }
        public bool IsEditPack { get; set; }
        public bool IsConsignment { get; set; }
        public bool IsSoundAlike { get; set; }
        public bool IsLookAlike { get; set; }
        public bool IsScheduleH1Drug { get; set; }
        public bool IsScheduleGDrug { get; set; }
        public bool IsScheduleDrug { get; set; }
        public bool IsHighRisk { get; set; }
        public string Instructions { get; set; }
        public string Specification { get; set; }
        public string VisualInspectionParameters { get; set; }
        public string StorageConditions { get; set; }
        public string Remarks { get; set; }
    }
    public class Fetchlookalike
    {
        public int lookalikemasterno { get; set; }
        public int VenueNo { get; set; }
        public int lookproductno { get; set; }
        public int likeproductno { get; set; }
        public string productname { get; set; }

        public string likeproductname { get; set; }
        public bool status { get; set; }
    }
    public class Getlookalikeresponse
    {
        public int VenueNo { get; set; }
        public int lookproductno { get; set; }

    }
    public class Fetchsoundalike
    {
        public int soundalikemasterno { get; set; }
        public int VenueNo { get; set; }
        public int soundProductno { get; set; }
        public int likeasoundno { get; set; }
        public string lookasoundname { get; set; }
        public string likeasoundname { get; set; }
        public bool status { get; set; }

    }
    public class GetSoundalikeresponse
    {
        public int VenueNo { get; set; }
        public int soundProductno { get; set; }

    }
    public class Fetchsubproduct
    {
        public int subProductMasterNo { get; set; }
        public int productMasterNo { get; set; }
        public bool status { get; set; }

    }
    public class SubProductReq
    {
        public int VenueNo { get; set; }
        public int subProductNo { get; set; }
        public int ProductMasterNo { get; set; }

    }
    public class SubProductRes
    {
        public int VenueNo { get; set; }
        public int subProductMasterNo { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public bool status { get; set; }

    }
    public class lstdrugreq
    {
        public int venueNo { get; set; }
        public int productNo { get; set; }
    }
    public class lstdrugresponse
    {
        public int drugPresTempNo { get; set; }
        public int rootNo { get; set; }
        public int frequencyNo { get; set; }
        public int dosageNo { get; set; }
        public int intakeNo { get; set; }
        public bool isDefault { get; set; }
    }
    public class savedruglstreq
    {
        public int venueno { get; set; }
        public int productNo { get; set; }
        public int userNo { get; set; }
        public List<lstdrugresponse> lstdrugresponse { get; set; }
    }
    public class savedruglstresponse
    {
        public int DrugPresTempNo { get; set; }
    }

    public class ProductUnitDTO
    {
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int UnitsNo { get; set; }
        public string UnitsCode { get; set; }
        public string UnitsName { get; set; }
    }
    public class BOMMappingDTO
    {
        public int BOMNo { get; set; }
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int UnitsNo { get; set; }
        public string UnitsCode { get; set; }
        public string UnitsName { get; set; }
        public int Qty { get; set; }
    }
    public class BOMMappingRequest
    {
        public int TestNo { get; set; }
        public string TestType { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int BOMNo { get; set; }
        public int ProductMasterNo { get; set; }
        public int UnitsNo { get; set; }
        public int Qty { get; set; }

    }
    public class BOMMappingResponse
    {
        public int result { get; set; }
    }
}