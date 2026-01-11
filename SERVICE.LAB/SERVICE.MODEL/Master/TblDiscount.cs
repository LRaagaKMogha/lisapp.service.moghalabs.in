using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblDiscount
    {
        public int DiscountNo { get; set; }
        public string DiscountName { get; set; }
        public string DiscountType { get; set; }
        public decimal Amount { get; set; }
        public bool IsChange { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public partial class GetDiscountDetails
    {
        public int discountNo { get; set; }
        public string DiscountName { get; set; }
        public string DiscountType { get; set; }
        public string DiscountTypeValue { get; set; }
        public int DiscountFor { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public string Gender { get; set; }
        public int AgeRange { get; set; }
        public string AgeRangeValue { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public bool IsRebate { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public partial class DiscountMasterRequest
    {
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int discountNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class DiscountMasterReponse
    {
        public int discountNo { get; set; }
    }
    public partial class DiscountInsertData
    {
        public int discountNo { get; set; }
        public string discountName { get; set; }
        public int DiscountFor { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public string Gender { get; set; }
        public int AgeRange { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public bool IsRebate { get; set; }
        public bool IsPercentage { get; set; }
        public int pageIndex { get; set; }
        public string DiscountType { get; set; }
        public int UserNo { get; set; }
    }
}
