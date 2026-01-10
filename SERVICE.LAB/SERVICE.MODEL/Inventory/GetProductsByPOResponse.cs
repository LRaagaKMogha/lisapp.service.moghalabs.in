using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace DEV.Model
{
    public class GetProductsByPOResponse
    {
        public Int64 RowNo { get; set; }
        public int GRNMasterNo { get; set; }
        public Int64 GRNProductNo { get; set; }
        public Int64 POProductNo { get; set; }
        public Int64 ProductNo { get; set; }
        public string ProductName { get; set; }
        public bool Free { get; set; }
        public int FreeQty { get; set; }
        public int UnitNo { get; set; }
        public string UnitsName { get; set; }
        public int PackNo { get; set; }
        public string PackName { get; set; }
        public int HSNNo { get; set; }
        public string HSNName { get; set; }
        public int POQty { get; set; }
        public Int32 Received { get; set; }
        public int Balance { get; set; }
        public int CurrentValue { get; set; }
        public decimal Rate { get; set; }
        public decimal purchaseOutRate { get; set; }
        public decimal Total { get; set; }
        public int DiscPercent { get; set; }
        public decimal DiscAmount { get; set; }
        public int TaxNo { get; set; }
        public string TaxName { get; set; }
        public int TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Gross { get; set; }
        public decimal CCCharge { get; set; }
        public decimal Net { get; set; }        
        public string IndentIds { get; set; }
        public bool IsRateChanged { get; set; }
        public decimal MasterRate { get; set; }
        public decimal PORate { get; set; }
        [NotMapped]
        public List<GetProductbatchdetailsbyPO> Batchdetails { get;set; }
        public bool IsExpDtRequired { get; set; }
        public bool IsBatchNoRequired { get; set; }
    }
}
