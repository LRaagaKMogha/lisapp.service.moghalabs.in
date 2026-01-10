using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public  class GetAllConsumptionMappingRequest : GetCommonMasterRequest
    {
        public int AnalyzerNo { get; set; }
        public int ParameterNo { get; set; }
        public int UnitNo { get; set; }
        public int ProductNo { get; set; }
    }
    public class GetAllConsumptionEntryRequest : GetCommonMasterRequest
    {
        public int BranchNo { get; set; }
        public int StoreNo { get; set; }
        public int ConsumptionNo { get; set; }
        public string MenuType { get; set; }
    }
}
