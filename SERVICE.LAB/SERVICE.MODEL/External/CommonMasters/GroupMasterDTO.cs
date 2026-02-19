using System.Collections.Generic;

namespace Service.Model.External.CommonMasters
{ 
    public partial class LstGroupInfo
    {
        public string deptName { get; set; }
        public int groupNo { get; set; }
        public string groupName { get; set; }
        public List<LstGroupServiceList> servicesList { get; set; }
    }
    public partial class LstGroupServiceList
    {
        public string serviceType { get; set; }
        public int serviceNo { get; set; }
        public string serviceName { get; set; }
    }
}
