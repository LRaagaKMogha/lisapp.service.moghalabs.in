using System;
using System.Collections.Generic;

namespace DEV.Model
{

    public partial class GetTblqcmaster

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
        public string branchName { get; set; }



    }

    public partial class Tblqcmaster

    { 
        public int  qcmasterNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public string lotNo { get; set; }
        public string levelNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public decimal meanValue { get; set; }
        public decimal lowValue { get; set; }
        public decimal highValue { get; set; }
        public Int16 venueNo { get; set; }
        public bool? status { get; set; }
        public  string branchName { get; set; }
        public string analyzerName { get; set; }
        public string paramName { get; set; }
     
    }

    public class EditqcDTO
    {
        public int qcmasterNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public Int16 venueNo { get; set; }
        public string lotNo { get; set; }
        public int venueBranchno { get; set; }
       // public string levelNo { get; set; }

    }

    public class qcmasterRequest
    {
       
        public int qcmasterNo { get; set; }
        public Int16 venueNo { get; set; }

    }
    public class QcMasterResponse
     {
         public int status { get; set; }
    }
    public class saveqcDTO
    {

        public Int16 venueNo { get; set; }
        public int userNo { get; set; }
        public int venueBranchno { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public bool? status { get; set; }
        public string lotNo { get; set; }     
        public List<Fetchlot> newlst { get; set; }
        public List<Fetchlevel> levellst { get; set; }
    }
    public class Fetchlot
    {
       
        public string lotNo { get; set; }
       
        public bool? status { get; set; }

     

    }
    public  class Fetchlevel
    {
        public int qcmasterNo { get; set; }

        public string lotNo { get; set; }

        public string levelNo { get; set; }
      
        public decimal meanValue { get; set; }
        public decimal lowValue { get; set; }
        public decimal highValue { get; set; }
       
       
    }
    public class Qclotresponse
    {
     
        public string lotNo { get; set; }
        public int rowNo { get; set; }
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }


    }
    public class Qclotreq
    {
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }
    }
    public class Qclevelreq
    {
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }
        public string lotNo { get; set; }

    }
    public class Qclevelresponse
    {
       
        public int rowNo { get; set; }
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }
        public string levelNo { get; set; }
        public string lotNo { get; set; }


    }
    public class Qclowhighreq
    {
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }
        public string lotNo { get; set; }
        public string levelNo { get; set; }

    }
    public class Qclowhighresponse
    {

        public int rowNo { get; set; }
        public Int16 venueNo { get; set; }
        public Int16 analyzerNo { get; set; }
        public Int16 paramNo { get; set; }
        public int venueBranchno { get; set; }
        public string levelNo { get; set; }
        public string lotNo { get; set; }
        public decimal lowValue { get; set; }
        public decimal highValue { get; set; }


    }
}
