using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{
    public class WorkListRequest : CommonFilterRequestDTO
    {
        public int SampleNo { get; set; }
        public int TestNo { get; set; }
        public List<TestDetailss> lstsearch { get; set; }
    }
    public class TestDetailss
    {
        public int testNo { get; set; }
        public string testType { get; set; }
    }
}
