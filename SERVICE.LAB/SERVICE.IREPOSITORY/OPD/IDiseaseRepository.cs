using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IDiseaseRepository
    {
        List<LstDiseaseCategory> GetDiseaseCategorys(ReqDiseaseCategory disCat);
        RtnDiseaseCategory InsertDiseaseCategorys(TblDiseaseCategory resq);
        List<LstDiseaseMaster> GetDiseaseMasters(ReqDiseaseMaster disName);
        RtnDiseaseMaster InsertDiseaseMasters(TblDiseaseMaster ress);
        int InsertDiseaseTemplateText(LstDiseaseTemplateList ress);
        Reqresponse GetDiseaseTemplateText(LstDiseaseTemplateList ress);
        List<LstDiseaseTemplateList> GetTemplateList(int VenueNo, int VenueBranchNo, int TemplateNo, int TempDiseaseNo);
        List<DiseaseVsProductMapping> GetDiseaseVsDrugMaster(ReqDiseaseMaster disName);
        List<DiseaseVsTestMapping> GetDiseaseVsTestMaster(ReqDiseaseMaster disName);
        RtnDisVsDrugMaster InsertDisVsDrugMaster(ReqDisVsDrugMaster res);
        RtnDisVsInvMaster InsertDisVsInvMaster(ReqDisVsInvMaster res);
        List<MachineMasterDTO> GetMachineMaster(ReqMachineMaster param);
        ReqMachineMasterResponse InsertMachineResult(InvMachineMasterRequest res);
    }
}