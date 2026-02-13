using Service.Model.Master;
using System.Collections.Generic;

namespace Service.IRepository.Master
{
    public  interface ITestTemplateMasterRepository
    {
        List<GetTestTemplateMasterRes> GetTestTemplateMasterList(GetTestTemplateMasterReq req);
        GetEditTemplateTestMasterResponseDto GetEditTemplateTestMaster(GetEditTemplateTestMasterRequestDto req);
        TemplatePathRes InsertTemplatePath(TemplatePathReq req);
        InsertTestTemplateMasterRes InsertTestTemplateMaster(InsertTestTemplateMasterReq req);
        GetTestTemplateTextMasterRes GetTextTemplateTextMaster(GetTestTemplateTextMasterReq req);
        List<GetTemplateApprovalRes> GetTemplateApprovalList(GetTemplateApprovalReq req);
    }
}
