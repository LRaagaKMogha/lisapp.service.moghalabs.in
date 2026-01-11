using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.External.CommonMasters
{
    public class ExternalApiMasterResponse
    {
        public bool success { get; set; }
        public ExternalResponse? response { get; set; }
        public List<ExternalError>? errors { get; set; }
    }
    public class ExternalResponse
    {
        public object? data { get; set; }
        public string? message { get; set; }
    }
    public class ExternalError
    {
        public int code { get; set; }
        public string? message { get; set; }
    }
}
