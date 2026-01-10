using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Admin
{
   
    public class RequestDataScrollText
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }

    public class ResponseDataScrollText
    {
        public string? textInformation { get; set; }
    }
}
