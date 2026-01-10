using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.External.WhatsAppChatBot
{
   
    public class InsertLogRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? MobileNo { get; set; }
        public string? MessageType { get; set; }
        public string? Message { get; set; }
        public bool IsBotMessage { get; set; }
        public string? JsonObject { get; set; }
    }
    public partial class returnLogRefNo
    {
        public int LogRefNo { get; set; }
    }
    public class FetchLogRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? MobileNo { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? MessageType { get; set; }
        public bool IsBotMessage { get; set; }
    }
    public class FetchLogResponse
    {
        public Int16 VenueNo { get; set; }
        public Int32 VenueBranchNo { get; set; }
        public string? MobileNo { get; set; }
        public int LogRefNo { get; set; }
        public string? MessageDtTm { get; set; }
        public string? MessageType { get; set; }
        public string? MessageData { get; set; }
        public bool IsBotMessage { get; set; }
        public string? JsonData { get; set; }
    }
}
