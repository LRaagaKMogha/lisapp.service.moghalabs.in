using Service.Model;
using Service.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dev.IRepository
{
    public interface IMasterRepository
    {
        List<CommonMasterDto> GetCommonMasterList(int venueno, int venuebranchno, string MasterKey);
        List<ConfigurationDto> GetConfigurationList(int venueno, int venuebranchno);
        List<CommonMasterDto> GetSearchCommonMasterList(int venueno, int venuebranchno, string MasterKey, string MasterValue);
        List<TblDepartment> GetDepartmentList(int VenueNo, int VenueBranchNo);
        List<TblMethod> GetMethodList(int VenueNo, int VenueBranchNo);
        List<TblUnits> GetUnitsList(int VenueNo, int VenueBranchNo);
        List<TblOrganism> GetOrganismList(int VenueNo, int VenueBranchNo);
        List<lstotdrugmap> GetOrgTypeAntiMapList(int VenueNo, int VenueBranchNo);
        List<TblTemplate> GetTemplateList(int VenueNo, int VenueBranchNo);
        List<CommonMasterDto> GetVenueDetails(int venueno, int venuebranchno, string MasterKey);
        ConfigurationDto GetSingleConfiguration(int? venueno, int? venuebranchno, string configkey);
        AppSettingResponse GetSingleAppSetting(string configkey);
        List<RefTypeCommonMasterDto> GetRefTypeList(int venueno, int venuebranchno);
        TreatmentPlanMasterResponse InsertTreatmentplan(TreatmentPlanMaster requestitem);
        TreatmentPlanMasterResponse DeleteTreatmentplan(int treatmentNo, int VenueNo, int VenueBranchNo, int UserNo);
        List<reqTreatmentMaster> GetTreatmentMaster(reqTreatmentMaster disName);
        TreatmentPlanMaster GetTreatmentMasterDetails(reqTreatmentMaster disName);
    }
}
