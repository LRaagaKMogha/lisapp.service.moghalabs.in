using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.Model
{
    public class GetRCMasterResponse
    {
        [Key]
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int RCNo { get; set; }
        public string RCName { get; set; }
        public string CreatedOn { get; set; }
        public string UserName { get; set; }
        public int UserNo { get; set; }
        public bool status { get; set; }
    }
}