using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblTerms
    {
        public int termsNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
        public string termstype { get; set; }
        public string termsname { get; set; }
        public string termsdescription { get; set; }
        public string termstypedescription { get; set; }
        public  Int16 sequenceno { get; set; }
        public bool? status { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int currentseqNo { get; set; }
    }
}
    public class TermsmasterRequest
    {
       
       public Int16 venueNo { get; set; }
       public int termsNo { get; set; }
       public string termstype { get; set; }
       public int pageIndex { get; set; }
       public int currentseqNo { get; set; }
}
    public class Termsmasterresponse
    {
        public int termsNo  { get; set; }

    }
   


