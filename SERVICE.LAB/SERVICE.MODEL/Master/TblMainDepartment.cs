using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblMainDepartment
    { 
        public Int16 maindeptno { get; set; }
        public Int16 venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }       
        public string departmentname { get; set; }       
        public string displayname { get; set; }
        public string shortcode { get; set; }
        public byte sequenceno { get; set; }
        public bool? status { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }
    public class MainDepartmentmasterRequest
    {       
        public Int16 venueno { get; set; }
        public Int16 maindeptno { get; set; }
        //  public int venuebranchno { get; set; }
        public int pageIndex { get; set; }
        public int updateseqNo { get; set; }
    }
    public class MainDepartmentMasterResponse
    {
        public Int16 maindeptno { get; set; }
        public int LastPageIndex { get; set; }
    }   
}
