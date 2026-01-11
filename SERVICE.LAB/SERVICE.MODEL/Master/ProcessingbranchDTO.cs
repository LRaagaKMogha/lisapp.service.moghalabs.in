using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class reqbranch
    {
        public int venueNo { get; set; }
        public int processingBranchMapNo { get; set; }
        public int pageIndex { get; set; }
        public int billedBranchNo { get; set; }
        public int processingNo { get; set; }
        public int deptNo { get; set; }
        public int testNo { get; set; }
    }

    public partial class responsebranch
    {
        public int totalRecords { get; set; }
        public int pageIndex { get; set; }
        public int processingBranchMapNo { get; set; }
        public int billedBranchNo { get; set; }
        public int processingNo { get; set; }
        public int deptNo { get; set; }
        public int testNo { get; set; }
        public bool status { get; set; }
        public Int16 venueNo { get; set; }
        public int userNo { get; set; }
        public string deptName { get; set; }
        public string testName { get; set; }
        public string venuebranchName { get; set; }
        public string venueName { get; set; }
    }
    public partial class insertbranch
    {
        public int processingBranchMapNo { get; set; }
        public int billedBranchNo { get; set; }
        public int processingNo { get; set; }
        public int testNo { get; set; }
        public int deptNo { get; set; }
        public Int16 venueNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }

    }
    public class Storeprocessingbranch
    {
        public int processingBranchMapNo { get; set; }
    }


}