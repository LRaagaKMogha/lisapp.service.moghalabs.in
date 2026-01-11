using System;
using System.Collections.Generic;
using System.Text;
namespace Service.Model
{
    public class NotificationDto
    {
        public string? TemplateKey { get; set; }
        public string? Address { get; set; }
        public string? CCAddress { get; set; }
        public string? BCCAddress { get; set; }
        public Dictionary<string,string>? MessageItem { get; set; }
        public string? MessageType { get; set; }
        public bool IsAttachment { get; set; }
        public Dictionary<string, string>? AttachmentItem { get; set; }
        public DateTime ScheduleTime { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int ClientNo { get; set; }
    }
    public class NotificationResponse
    {     
        public int Status { get; set; }
    }
}
