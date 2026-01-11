using Service.Model;
using Service.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Master
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
