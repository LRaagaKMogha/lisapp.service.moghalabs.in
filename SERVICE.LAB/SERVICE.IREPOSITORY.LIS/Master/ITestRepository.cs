using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dev.IRepository
{
    public interface ITestRepository
    {
       
        List<lsttest> GetTestList(reqtest req);
        objtest GetEditTest(reqtest req);
         int InsertTest(objtest req);
        rtntemplateNo InsertTemplateText(lstTemplateList req);
        rtntemplateText GetTemplateText(reqtest req);
        int UpdateSequence(Objtestsequence req);

        List<lstgrppkg> GetGroupPackageList(reqtest req);
        objgrppkg GetEditGroupPackage(reqtest req);
        int InsertGroupPackage(objgrppkg req);
        List<lstgrppkgservice> GetSearchService(reqsearchservice req);


        List<lststest> GetSubTestList(reqtest req);
        objsubtest GetEditSubTest(reqtest req);
        int InsertSubTest(objsubtest req);
        int InsertTestFormula(SaveFormulaRequest req);
        List<GetFormulaResponse> GetTestFormula(GetFormulaRequest req);
        CheckTestcodeExistsRes GetAlreadyExisitingTestCode(CheckTestcodeExists req);
        List<restestapprove> GetTestApprove(reqtestapprove req);

        List<restestappHistory> GetApproveHistory(reqtestapprove req);
        List<GetTATRes> GetTATMaster(GetTATReq req);
        InsTATRes InsertTATMaster(InsTATReq req);
        List<GetloincRes> GetLoincMaster(GetloincReq req);
        InsloincRes InsertLoincMaster(InsloincReq req);
        List<GetSnomedRes> GetSnomedMaster(GetSnomedReq req);
        InsSnomedRes InsertSnomedMaster(InsSnomedReq req);
        List<IntegrationPackageRes> GetIntegrationPackage([FromBody] IntegrationPackageReq req);
        IntegrationPackageResult InsertIntegrationPackage(IntegrationPackageReq req);
        objgrppkg GetPackageInstrauction(reqtest req);
        List<PrintPackageDetails> GetPrintPakg(reqsearchservice req);
        List<GetStatinMasterDetailsRes> GetStatinMasterDetails(GetStatinMasterDetailsReq req);
        StainMasterInsertRes InsertStatinMasterDetails(StainMasterInsertReq req);
    }

}
