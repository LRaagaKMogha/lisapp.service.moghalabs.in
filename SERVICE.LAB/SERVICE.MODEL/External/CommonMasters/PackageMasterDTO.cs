using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DEV.Model.External.CommonMasters
{
     public partial class LstPackageInfo
    {
        public int packageNo { get; set; }
        public string? packageName { get; set; }
    }
    public partial class LstPackageBreakUpInfo
    {
        public int packageNo { get; set; }
        public string? packageName { get; set; }
        [IgnoreDataMember]
        public string? ListOfServices { get; set; }
        public List<LstPackageServiceList>? servicesList {get; set;}
    }
    public partial class LstPackageServiceList
    {
        public string? serviceType { get; set; }
        public int serviceNo { get; set; }
        public string? serviceName { get; set; }
    }
}
