using System;
using System.Collections.Generic;

namespace Service.Model
{
   
    public partial class TblGeneric
    {
        public int genericNo { get; set; }
        public string genericName { get; set; }
        public Int16 sequenceNo { get; set; }
        public bool? isNotified { get; set; }
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }

    public partial class reqgeneric
    {
            public int genericNo { get; set; }
            public Int16 venueNo { get; set; }
            public int pageIndex { get; set; }

    }

    public class GenericMasterResponse
    {
            public int genericNo { get; set; }
           
    }
    

    public partial class TblMedtype
    {
        public int medicineTypeNo { get; set; }
        public string description { get; set; }
        public Int16 sequenceNo { get; set; }
        public string unitName { get; set; }
        public int unitNo { get; set; }
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }

    }
    public partial class reqmedtype
    {
        public int medicineTypeNo { get; set; }
        public Int16 venueNo { get; set; }
        public int unitNo { get; set; }
        public int pageIndex { get; set; }
       
    }

    public class MedtypeMasterResponse
    {
        public int medicineTypeNo { get; set; }
    }
    public partial class TblMedstr
    {
        public int strengthNo { get; set; }
        public string strengthName { get; set; }
        public decimal strengthValue { get; set; }
        public Int16 sequenceNo { get; set; }
    
        public bool? status { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }

    }
    public partial class reqmedstr
    {
        public int strengthNo { get; set; }
        public Int16 venueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public class MedstrMasterResponse
    {
        public int strengthNo { get; set; }
    }
    


}
