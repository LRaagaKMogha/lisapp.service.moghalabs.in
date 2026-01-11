using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class Tblqcresult

    {
        public int qcresultNo { get; set; }
        public Int16 analyzerNo { get; set; }

        public Int16 paramNo { get; set; }
        public string lotNo { get; set; }
        public bool? status { get; set; }
        public int venueBranchno { get; set; }
        public string analyzerName { get; set; }
        public Int16 venueNo { get; set; }
        public string paramName { get; set; }
        public string result { get; set; }
        public string resultTime { get; set; }
        public string resultDate { get; set; }
        public string levelNo { get; set; }

    }
    public partial class GetTblqcresult

    {
        public int rowNo { get; set; }
        public Int16 analyzerNo { get; set; }

        public Int16 paramNo { get; set; }
        public string lotNo { get; set; }
        public bool? status { get; set; }
        public int venueBranchno { get; set; }
        public string analyzerName { get; set; }
        public Int16 venueNo { get; set; }
        public string paramName { get; set; }
        public string result { get; set; }
        public string resultTime { get; set; }
        public string resultDate { get; set; }
        public string levelNo { get; set; }

    }
    public class QcresultRequest

    {
        public int qcresultNo { get; set; }
        public Int16 venueNo { get; set; }

    }

    public class SaveqcresDTO
    {
        public Int16 venueNo { get; set; }
        public int userNo { get; set; }
        public int venueBranchno { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public string lotNo { get; set; }
        public string resultDate { get; set; }       
        public List<Fetchresult> resultlst { get; set; }
    }

    public class Fetchresult
    {
     public string levelNo { get; set; }
     public string result { get; set; }
     public string resultTime { get; set; }
     public bool? status { get; set; }
      public int qcresultNo { get; set; }
    }
    public class QcresultResponse
    {
        public int status { get; set; }
    }

    public class EditqcresDTO
    {
        public string resultDate { get; set; }
        public int qcresultNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public Int16 venueNo { get; set; }
        public string lotNo { get; set; }
        public int venueBranchno { get; set; }

    }

}
