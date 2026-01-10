using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Common
{
    public class CommonResponse
    {
        public int status { get; set; }
    }
    public class ErrorResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
    }
}