using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetReagentStockRequest : GetCommonMasterRequest
    {        
        public int branchNo { get; set; }
        public int storeNo { get; set; }
        public int productNo { get; set; }
    }
}
