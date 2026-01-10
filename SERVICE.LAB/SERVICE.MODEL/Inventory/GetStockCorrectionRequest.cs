using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetStockCorrectionRequest : GetCommonMasterRequest
    {
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
    }
    public class GetStockAdjustmentRequest : GetCommonMasterRequest
    {
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ProductNo { get; set; }
        public string MenuType { get; set; }
    }
}
