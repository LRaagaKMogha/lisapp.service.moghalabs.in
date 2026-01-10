using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class tbl_IV_ProductMaster
    {
        public int ProductMasterNo { get; set; }
        public string ProductMasterName { get; set; }
        public int ManufacturerNo { get; set; }
        public int CategoryNo { get; set; }
        public int ProductTypeNo { get; set; }
        public int PackNo { get; set; }
        public int PurchaseUnitNo { get; set; }
        public Int16 purchaseUnitSize { get; set; }
        public int IssueUnitNo { get; set; }
        public Int16 issueUnitSize { get; set; }
        public int ConsumptionUnitNo { get; set; }
        public Int16 consumptionUnitSize { get; set; }
        public int GenericNo { get; set; }
        public int MedicineTypeNo { get; set; }
        public int StrengthNo { get; set; }
        public int LeadPeriodInDays { get; set; }
        public int TestCount { get; set; }
        public int HSNNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
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
    }
}
