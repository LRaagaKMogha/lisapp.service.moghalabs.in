using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Master
{
    public class GetTestTemplateMasterReq
    {
        public int DeptNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int TestNo { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetTestTemplateMasterRes
    {
        public int RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int TestNo { get; set; }
        public string TestShortName { get; set; }
        public string TestName { get; set; }
        public string TestDisplayName { get; set; }
        public string DepartmentName { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public int TSequenceNo { get; set; }
        public int DeptSequenceNo { get; set; }
    }
    public class GetEditTemplateTestMasterRequestDto
    {
        public int TestNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int IsApproval { get; set; }
        public int? TemplateApprovalNo { get; set; }
    }

    public class TemplateItemDto
    {
        public int templateNo { get; set; }
        public int testNo { get; set; }
        public string templateName { get; set; }
        public string templateText { get; set; }
        public bool isDefault { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int OldServiceNo { get; set; }
        public int OldTemplateNo { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReject { get; set; }
    }
    public class GetEditTemplateTestMasterRawDto
    {
        public string testTemplate { get; set; }
    }
    public class GetEditTemplateTestMasterResponseDto
    {
        public List<TemplateItemDto> TestTemplate { get; set; } = new List<TemplateItemDto>();
    }
    public class TemplatePathReq
    {
        public int templateNo { get; set; }
        public int testNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool isApproval { get; set; } 
        public int? templateApprovalNo { get; set; } 
        public bool IsTestTemplateApproveMode { get; set; }
        public string templateName { get; set; }    
        public int userNo { get; set; }             
    }

    public class TemplatePathRes
    {
        public int status { get; set; }
        public string message { get; set; }
    }
    //InsertTestTemplate
    public class InsertTestTemplateMasterReq
    {
        public int templateNo { get; set; }
        public int testNo { get; set; }
        public string templateName { get; set; }
        public string templateText { get; set; }
        public bool isDefault { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int OldServiceNo { get; set; }
        public int OldTemplateNo { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReject { get; set; }
        public bool? IsApprovalTestTemplate { get; set; }
    }
    public class InsertTestTemplateMasterRes
    {
        public int templateNo { get; set; }
        public int templateApprovalNo { get; set; }
    }

    //GetTestTemplatTextMaster
    public class GetTestTemplateTextMasterReq
    {
        public string? pageCode { get; set; }
        public int deptNo { get; set; }
        public int maindeptNo { get; set; }
        public int testNo { get; set; }
        public int subTestNo { get; set; }
        public int templateNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int masterNo { get; set; }
        public int mastertype { get; set; }
        public int pageIndex { get; set; }
        public bool IsApproval { get; set; }
        public int pageSize { get; set; }
        public int Userstatus { get; set; }
    }
    public class GetTestTemplateTextMasterRes
    {
        public string templateText { get; set; }
        public int templateNo { get; set; }
    }
    public class GetTemplateApprovalReq
    {
        public int VenueNo { get; set; } = 0;
        public int VenueBranchNo { get; set; } = 0;
        public int DeptNo { get; set; } = 0;
        public int TestNo { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetTemplateApprovalRes
    {
        public int RowNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int TemplateApprovalNo { get; set; }
        public int? OldTemplateNo { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string DepartmentName { get; set; }
        public string TemplateName { get; set; }
        public bool IsAbnormal { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
