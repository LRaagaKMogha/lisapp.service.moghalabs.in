using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Win.Common
{   
    public class MessageHostDTO
    {
        public string HostAddress { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        public string FromAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool SSLStatus { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string CommunicationType { get; set; }
        public int MessageQueueNo { get; set; }
        public MessageContent MessageContent { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class MessageContent
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<MessageAttachment> Attachment { get; set; }
    }
    public class MessageAttachment
    {
        public string AttachmentName { get; set; }
        public string AttachmentURL { get; set; }
        public string AttachmentFormat { get; set; }
        public bool IsEmbed { get; set; }
    }
    public class ReportParamDTO
    {
        public List<Dictionary<string, object>> datatable { get; set; }
        public Dictionary<string, string> paramerter { get; set; }
        public string ReportPath { get; set; }
        public string ExportPath { get; set; }
        public string ExportFormat { get; set; }
    }
}
