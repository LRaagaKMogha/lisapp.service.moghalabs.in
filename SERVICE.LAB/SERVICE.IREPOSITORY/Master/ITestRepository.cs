using System.Collections.Generic;
using Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace Service.IRepository
{
    public interface ITestRepository
    {       
        List<Lsttest> GetTestList(Reqtest req);
        Objtest GetEditTest(Reqtest req);
         int InsertTest(Objtest req);
        RtntemplateNo InsertTemplateText(LstTemplateList req);
        RtntemplateText GetTemplateText(Reqtest req);
        int UpdateSequence(Objtestsequence req);

        List<Lstgrppkg> GetGroupPackageList(Reqtest req);
        Objgrppkg GetEditGroupPackage(Reqtest req);
        int InsertGroupPackage(Objgrppkg req);
        List<Lstgrppkgservice> GetSearchService(Reqsearchservice req);
        List<Lststest> GetSubTestList(Reqtest req);
        Objsubtest GetEditSubTest(Reqtest req);
        int InsertSubTest(Objsubtest req);
        int InsertTestFormula(SaveFormulaRequest req);
        List<GetFormulaResponse> GetTestFormula(GetFormulaRequest req);
        CheckTestcodeExistsRes GetAlreadyExisitingTestCode(CheckTestcodeExists req);
        List<Restestapprove> GetTestApprove(Reqtestapprove req);
        List<RestestappHistory> GetApproveHistory(Reqtestapprove req);
        List<GetTATRes> GetTATMaster(GetTATReq req);
        InsTATRes InsertTATMaster(InsTATReq req);
        List<GetloincRes> GetLoincMaster(GetloincReq req);
        InsloincRes InsertLoincMaster(InsloincReq req);
        List<GetSnomedRes> GetSnomedMaster(GetSnomedReq req);
        InsSnomedRes InsertSnomedMaster(InsSnomedReq req);
        List<IntegrationPackageRes> GetIntegrationPackage([FromBody] IntegrationPackageReq req);
        IntegrationPackageResult InsertIntegrationPackage(IntegrationPackageReq req);
        Objgrppkg GetPackageInstrauction(Reqtest req);
        List<PrintPackageDetails> GetPrintPakg(Reqsearchservice req);
        List<GetStatinMasterDetailsRes> GetStatinMasterDetails(GetStatinMasterDetailsReq req);
        StainMasterInsertRes InsertStatinMasterDetails(StainMasterInsertReq req);
    }
}