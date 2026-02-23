using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Common;
using Service.IRepository;
using Service.Model;
using Service.Model.EF;
using Service.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Service.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private IConfiguration _config;
        public MasterRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get CommonMaster List
        /// </summary>
        /// <param name="CommonKey"></param>
        /// <returns></returns>
        public List<CommonMasterDto> GetCommonMasterList(int venueno, int venuebranchno, string MasterKey)
        {
            List<CommonMasterDto> Objresult = new List<CommonMasterDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.CommonMaster + MasterKey + venueno + venuebranchno;
                Objresult = MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                if (Objresult == null || MasterKey == "MediaLab" || MasterKey == "ANALYZER" || MasterKey == "PARAMNAME" || MasterKey == "PRODUCTMASTER" || MasterKey == "CountryName" || MasterKey == "StateName" || MasterKey == "CityName" || MasterKey == "countrymaster" || Objresult.Count()==0)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("venueno", venueno);
                        var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                        var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                        Objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_CommonDetails @MasterKey,@venueno,@venuebranchno", _MasterKey, _venueno, _venuebranchno).ToList();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetCommonMasterList-" + MasterKey, ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return Objresult;   
        }


        /// <summary>
        /// GetConfigurationList
        /// </summary>
        /// <param name="venueno"></param>
        /// <param name="venuebranchno"></param>
        /// <returns></returns>
        public List<ConfigurationDto> GetConfigurationList(int venueno, int venuebranchno)
        {
            List<ConfigurationDto> Objresult = new List<ConfigurationDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.ConfigurationMaster  + venueno + venuebranchno;
                Objresult = MemoryCacheRepository.GetCacheItem<List<ConfigurationDto>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("VenueNo", venueno);
                        var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                        Objresult = context.ConfigurationDTO.FromSqlRaw("Execute dbo.pro_GetConfiguration @VenueNo,@VenueBranchNo", _venueno, _venuebranchno).ToList();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetConfigurationList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return Objresult;
        }

        /// <summary>
        /// Get Search CommonMaster List
        /// </summary>
        /// <param name="CommonKey"></param>
        /// <returns></returns>
        public List<CommonMasterDto> GetSearchCommonMasterList(int venueno, int venuebranchno, string MasterKey, string MasterValue)
        {
            List<CommonMasterDto> Objresult = new List<CommonMasterDto>();
            try
            {

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                    var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                    var _MasterValue = new SqlParameter("MasterValue", MasterValue);
                    Objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_SearchCommonDetails @MasterKey,@MasterValue,@venueno,@venuebranchno", _MasterKey, _MasterValue, _venueno, _venuebranchno).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetSearchCommonMasterList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return Objresult;
        }

        public List<TblDepartment> GetDepartmentList(int VenueNo, int VenueBranchNo)
        {
            List<TblDepartment> Objresult = new List<TblDepartment>();
            try
            {
                string _CacheKey = CacheKeys.tblDepartmentList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<TblDepartment>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        Objresult = context.TblDepartment.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetDepartmentList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<TblMethod> GetMethodList(int VenueNo, int VenueBranchNo)
        {
            List<TblMethod> Objresult = new List<TblMethod>();
            try
            {
                string _CacheKey = CacheKeys.tblMethodList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<TblMethod>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        Objresult = context.TblMethod.Where(a => a.VenueNo == VenueNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetMethodList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<TblUnits> GetUnitsList(int VenueNo, int VenueBranchNo)
        {
            List<TblUnits> Objresult = new List<TblUnits>();
            try
            {
                string _CacheKey = CacheKeys.tblUnitsList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<TblUnits>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        Objresult = context.TblUnits.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetUnitsList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<TblOrganism> GetOrganismList(int VenueNo, int VenueBranchNo)
        {
            List<TblOrganism> Objresult = new List<TblOrganism>();
            try
            {
                string _CacheKey = CacheKeys.tblOrganismList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<TblOrganism>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        Objresult = context.TblOrganism.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo ==VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetOrganismList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<Lstotdrugmap> GetOrgTypeAntiMapList(int VenueNo, int VenueBranchNo)
        {
            List<Lstotdrugmap> Objresult = new List<Lstotdrugmap>();
            try
            {
                string _CacheKey = CacheKeys.tblOrgTypeAntiMapList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<Lstotdrugmap>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var lst = context.GetOrgTypeAntiMapList.FromSqlRaw("Execute dbo.pro_GetOrgTypeAntiMap").ToList();

                        Objresult = lst.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetOrgTypeAntiMapList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<TblTemplate> GetTemplateList(int VenueNo, int VenueBranchNo)
        {
            List<TblTemplate> Objresult = new List<TblTemplate>();
            try
            {
                string _CacheKey = CacheKeys.tblTemplateList + VenueNo + VenueBranchNo;
                Objresult = MemoryCacheRepository.GetCacheItem<List<TblTemplate>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var lst = context.GetTemplateList.FromSqlRaw("Execute dbo.pro_GetTemplateMaster").ToList();

                        Objresult = lst.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTemplateList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        public List<CommonMasterDto> GetVenueDetails(int venueno, int venuebranchno, string MasterKey)
        {
            List<CommonMasterDto> Objresult = new List<CommonMasterDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                    var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                    Objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_CommonDetails @MasterKey,@venueno,@venuebranchno", _MasterKey, _venueno, _venuebranchno).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetVenueDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return Objresult;
        }

        public ConfigurationDto GetSingleConfiguration(int? venueno, int? venuebranchno, string configkey)
        {
            ConfigurationDto objOutput = new ConfigurationDto();         
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", venueno);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                    var _configKey = new SqlParameter("ConfigKey", configkey);

                    objOutput = context.GetSingleConfiguration.FromSqlRaw("Execute dbo.pro_GetSingleConfiguration @VenueNo, @VenueBranchNo, @ConfigKey", _venueno, _venuebranchno, _configKey)
                        .AsEnumerable()?.SingleOrDefault();

                    return objOutput ?? new ConfigurationDto();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetSingleConfiguration - (" + configkey + ")", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objOutput;
        }
        public AppSettingResponse GetSingleAppSetting(string configkey)
        {
            AppSettingResponse objOutput = new AppSettingResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _configKey = new SqlParameter("ConfigKey", configkey);
                    objOutput = context.GetSingleAppSettings.FromSqlRaw("Execute dbo.pro_GetSingleAppSetting @ConfigKey", _configKey).AsEnumerable()?.SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetSingleAppSetting", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objOutput;
        }

        public List<RefTypeCommonMasterDto> GetRefTypeList(int venueno, int venuebranchno)
        {
            List<RefTypeCommonMasterDto> Objresult = new List<RefTypeCommonMasterDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.RefTypeList + venueno + venuebranchno;
                Objresult = MemoryCacheRepository.GetCacheItem<List<RefTypeCommonMasterDto>>(_CacheKey);
                if (Objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("VenueNo", venueno);
                        var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);

                        Objresult = context.RefTypeListDTO.FromSqlRaw("Execute dbo.pro_RefTypeSettings @VenueNo, @VenueBranchNo", _venueno, _venuebranchno).ToList();
                        
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        
                        MemoryCacheRepository.AddItem(_CacheKey, Objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetRefTypeList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return Objresult;
        }

        public TreatmentPlanMaster GetTreatmentMasterDetails(reqTreatmentMaster disName)
        {
            TreatmentPlanMaster Objresult = new TreatmentPlanMaster();
            List<TreatmentPlanProMaster> resultPRO = new List<TreatmentPlanProMaster>();
            List<TreatmentPlanPrmMaster> resultPRM = new List<TreatmentPlanPrmMaster>();
            try
            {
                var _TreatmentNo = new SqlParameter("TreatmentNo", disName?.treatmentNo);
                var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", "PRO");

                    resultPRO = context.GetTreatmentMasterDetailsPRO.FromSqlRaw(
                    "Execute dbo.pro_GetTreatmentMasterDetailsByPRO @TreatmentNo,@Type, @VenueNo, @VenueBranchNo",
                    _TreatmentNo, _type, _VenueNo, _VenueBranchNo).ToList();
                }
                
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", "PRM");

                    resultPRM = context.GetTreatmentMasterDetailsPRM.FromSqlRaw(
                    "Execute dbo.pro_GetTreatmentMasterDetailsByPRM @TreatmentNo,@Type, @VenueNo, @VenueBranchNo",
                    _TreatmentNo, _type, _VenueNo, _VenueBranchNo).ToList();
                }
                Objresult.treatmentNo = disName.treatmentNo;
                Objresult.treatmentName = disName.treatmentName;
                Objresult.diseaseNo = disName.diseaseNo;
                Objresult.diseaseName = disName.diseaseName;
                Objresult.VenueNo = disName.VenueNo;
                Objresult.VenueBranchNo = disName.VenueBranchNo;
                Objresult.lstpharmacy = resultPRM;
                Objresult.lstProcedures = resultPRO;
                Objresult.rate = disName.Rate;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTreatmentMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return Objresult;
        }
        public List<reqTreatmentMaster> GetTreatmentMaster(reqTreatmentMaster disName)
        {
            List<reqTreatmentMaster> Objresult = new List<reqTreatmentMaster>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TreatmentNo = new SqlParameter("TreatmentNo", disName?.treatmentNo);
                    var _DiseaseNo = new SqlParameter("DiseaseNo", disName?.diseaseNo);
                    var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disName?.PageIndex);

                    Objresult = context.GetTreatmentMaster.FromSqlRaw(
                    "Execute dbo.pro_GetTreatmentMaster @TreatmentNo, @DiseaseNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _TreatmentNo, _DiseaseNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTreatmentMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return Objresult;
        }

        public TreatmentPlanMasterResponse DeleteTreatmentplan(int treatmentNo, int VenueNo, int VenueBranchNo, int UserNo)
        {
            TreatmentPlanMasterResponse result = new TreatmentPlanMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", "DELETE");
                    var _TreatmentNo = new SqlParameter("TreatmentNo", treatmentNo);
                    var _TreatmentName = new SqlParameter("TreatmentName", "");
                    var _DiseaseNo = new SqlParameter("DiseaseNo", 0);
                    var _DiseaseName = new SqlParameter("DiseaseName", "");
                    var _TreatmentProxml = new SqlParameter("TreatmentProxml", "");
                    var _TreatmentPrmxml = new SqlParameter("TreatmentPrmxml", "");
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", UserNo);
                    var dbResponse = context.InsertTreatmentplan.FromSqlRaw(
                    "Execute dbo.Pro_InsertTreatmentPlan @Type,@TreatmentNo,@TreatmentName,@DiseaseNo,@DiseaseName,@TreatmentProxml,@TreatmentPrmxml,@VenueNo,@VenueBranchNo,@UserNo",
                    _Type, _TreatmentNo, _TreatmentName, _DiseaseNo, _DiseaseName, _TreatmentProxml, _TreatmentPrmxml, _VenueNo, _VenueBranchNo, _UserID).FirstOrDefault();

                    result.treatmentNo = dbResponse.treatmentNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.DeleteTreatmentplan", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, UserNo);
            }
            return result;
        }

        public TreatmentPlanMasterResponse InsertTreatmentplan(TreatmentPlanMaster objDTO)
        {
            TreatmentPlanMasterResponse result = new TreatmentPlanMasterResponse();
            try
            {
                XDocument TreatmentProXML = new XDocument(new XElement("TreatmentProxml", from Item in objDTO.lstProcedures
                                                                                          select
                                                                                          new XElement("TreatmentProList",
                                                                                          new XElement("type", Item.type),
                                                                                          new XElement("testNo", Item.testNo),
                                                                                          new XElement("testName", Item.testName),
                                                                                          new XElement("scheduleEveryNo", Item.scheduleEveryNo),
                                                                                          new XElement("frequencyNo", Item.frequencyNo),
                                                                                          new XElement("daySunday", Item.daySunday),
                                                                                          new XElement("dayMonday", Item.dayMonday),
                                                                                          new XElement("dayTuesday", Item.dayTuesday),
                                                                                          new XElement("dayWednesday", Item.dayWednesday),
                                                                                          new XElement("dayThursday", Item.dayThursday),
                                                                                          new XElement("dayFriday", Item.dayFriday),
                                                                                          new XElement("daySaturday", Item.daySaturday),
                                                                                          new XElement("totalTreatments", Item.totalTreatments)
                                                                                          )));

                XDocument TreatmentPrmXML = new XDocument(new XElement("TreatmentPrmXML", from Item in objDTO.lstpharmacy
                                                                                          select
                                                                                          new XElement("TreatmentPrmList",
                                                                                           new XElement("type", Item.type),
                                                                                           new XElement("productMasterNo", Item.productMasterNo),
                                                                                           new XElement("productMasterName", Item.productMasterName),
                                                                                           new XElement("daily", Item.daily),
                                                                                           new XElement("am", Item.am),
                                                                                           new XElement("pm", Item.pm),
                                                                                           new XElement("weekly", Item.weekly),
                                                                                           new XElement("asNeeded", Item.asNeeded)
                                                                                          )));


                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("Type", (objDTO.treatmentNo == 0 ? "INSERT" : "UPDATE"));
                    var _TreatmentNo = new SqlParameter("TreatmentNo", objDTO.treatmentNo);
                    var _TreatmentName = new SqlParameter("TreatmentName", objDTO.treatmentName);
                    var _DiseaseNo = new SqlParameter("DiseaseNo", objDTO.diseaseNo);
                    var _DiseaseName = new SqlParameter("DiseaseName", objDTO.diseaseName);
                    var _TreatmentProxml = new SqlParameter("TreatmentProxml", TreatmentProXML.ToString());
                    var _TreatmentPrmxml = new SqlParameter("TreatmentPrmxml", TreatmentPrmXML.ToString());
                    var _VenueNo = new SqlParameter("VenueNo", objDTO.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", objDTO.VenueBranchNo);
                    var _UserID = new SqlParameter("UserNo", objDTO.UserNo.ToString());
                    var _Rate = new SqlParameter("Rate", objDTO.rate);
                    var dbResponse = context.InsertTreatmentplan.FromSqlRaw(
                    "Execute dbo.Pro_InsertTreatmentPlan @Type, @TreatmentNo,@TreatmentName,@DiseaseNo,@DiseaseName,@TreatmentProxml,@TreatmentPrmxml,@VenueNo,@VenueBranchNo,@UserNo,@Rate",
                    _Type, _TreatmentNo, _TreatmentName, _DiseaseNo, _DiseaseName, _TreatmentProxml, _TreatmentPrmxml, _VenueNo, _VenueBranchNo, _UserID,_Rate).AsEnumerable().FirstOrDefault();

                    result.treatmentNo = dbResponse.treatmentNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.InsertTreatmentplan", ExceptionPriority.High, ApplicationType.REPOSITORY, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }
    }
}

