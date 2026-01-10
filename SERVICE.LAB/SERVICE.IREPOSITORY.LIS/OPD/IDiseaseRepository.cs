using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IDiseaseRepository
    {
        List<lstDiseaseCategory> GetDiseaseCategorys(reqDiseaseCategory disCat);
        rtnDiseaseCategory InsertDiseaseCategorys(TblDiseaseCategory resq);
        List<lstDiseaseMaster> GetDiseaseMasters(reqDiseaseMaster disName);
        rtnDiseaseMaster InsertDiseaseMasters(TblDiseaseMaster ress);
        int InsertDiseaseTemplateText(lstDiseaseTemplateList ress);
        reqresponse GetDiseaseTemplateText(lstDiseaseTemplateList ress);
        List<lstDiseaseTemplateList> GetTemplateList(int VenueNo, int VenueBranchNo, int TemplateNo, int TempDiseaseNo);
        List<DiseaseVsProductMapping> GetDiseaseVsDrugMaster(reqDiseaseMaster disName);
        List<DiseaseVsTestMapping> GetDiseaseVsTestMaster(reqDiseaseMaster disName);
        rtnDisVsDrugMaster InsertDisVsDrugMaster(reqDisVsDrugMaster res);
        rtnDisVsInvMaster InsertDisVsInvMaster(reqDisVsInvMaster res);
        List<MachineMasterDTO> GetMachineMaster(reqMachineMaster param);
        reqMachineMasterResponse InsertMachineResult(InvMachineMasterRequest res);
    }
}