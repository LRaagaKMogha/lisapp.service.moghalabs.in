using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model
{
    public class DocumentUploadDTO
    {
        public DocumentUploadDTO() { }

        public class TestTemplateRequest
        {
            public int ContentId { get; set; }
            public int TemplateNo { get; set; }
            public string? TemplateName { get; set; }
            public string? ContentBody { get; set; }
            public Int16 VenueNo { get; set; }
            public DateTime CreatedOn { get; set; }
            public Int16 CreatedBy { get; set; }
            public bool Status { get; set; }
        }
        public class TestTemplateApprovalRequest
        {
            public int ContentApprovalId { get; set; }
            public int TemplateApprovalNo { get; set; }
            public int? TemplateNo { get; set; }
            public string? TemplateName { get; set; }
            public string? ContentBody { get; set; }
            public Int16 VenueNo { get; set; }
            public DateTime CreatedOn { get; set; }
            public Int16 CreatedBy { get; set; }
            public bool Status { get; set; }
        }
    }
}
