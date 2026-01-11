using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Inventory
{
    public class InsertGRNMasterRequest
    {
        public int grnMasterNo { get; set; }
        public int deptNo { get; set; }
        public int supplierNo { get; set; }
        public bool withPO { get; set; }
        public int grnType { get; set; }
        public string grnDate { get; set; }
        public string grnNo { get; set; }
        public int poNumber { get; set; }
        public List<GetProductsByPOResponse> productList { get; set; }
        public List<TaxList> taxList { get; set; }
        public List<otherChargeModal> oCList { get; set; }
        public decimal totalbillAmount { get; set; }
        public decimal totaldiscPercentage { get; set; }
        public decimal totaldiscountvalue { get; set; }
        public decimal totalccCharge { get; set; }
        public decimal totalgross { get; set; }
        public decimal totaltax { get; set; }
        public decimal totalothercharges { get; set; }
        public decimal totalnet { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int createdby { get; set; }
        public int modifiedBy { get; set; }
        public bool status { get; set; }
        public decimal RoundOff { get; set; }
        public decimal RoundOffInCurrency { get; set; }
        public string MenuType { get; set; }
        public string IndentIds { get; set; }
        public int StoreNo { get; set; }
        public string invDate { get; set; }
        public string invNo { get; set; }
    }
    public class InvoiceUpdateRequest
    {
        public Int16 VenueNo { get; set; }
        public Int16 BranchNo { get; set; }
        public int GrnNo { get; set; }
        public int UserNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
    }
}
