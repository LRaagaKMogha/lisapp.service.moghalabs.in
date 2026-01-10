using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.WhatsAppChatBot
{
    public class ExternalAPICommonResponse
    {
        public bool success { get; set; }
        public ExternalResponse? response { get; set; }
        public List<ExternalError>? errors { get; set; }
    }
   
}