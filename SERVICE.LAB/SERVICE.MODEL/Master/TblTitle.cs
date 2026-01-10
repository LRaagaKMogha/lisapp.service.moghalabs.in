using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class TblTitle

    {
        public int CommonNo { get; set; }
        public int CommonCode { get; set; }
        public bool? IsDefault { get; set; }
        public int commonBranchNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string commonName { get; set; }
        public string commonValue { get; set; }
        public int sequenceNo { get; set; }
        public bool? status { get; set; }

        public bool? isUpdfromscreen { get; set; }

    }

    public partial class TblName

    {
        public int CommonNo { get; set; }
        public int CommonCode { get; set; }
        public bool? IsDefault { get; set; }
        public int commonBranchNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string commonValue { get; set; }
        public int sequenceNo { get; set; }
        public bool? status { get; set; }
    }

    public class TitlemasterRequest
    {

        public int venueNo { get; set; }
        public int commonBranchNo { get; set; }

        public int venueBranchno { get; set; }
        public int CommonNo { get; set; }

    }
    public class Titlemasterresponse
    {
        public int commonBranchNo { get; set; }

    }
}


