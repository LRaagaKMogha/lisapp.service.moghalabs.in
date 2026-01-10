using DEV.Model;
using DEV.Model.EF;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Common;
using Microsoft.Extensions.Configuration;
using Serilog;
using DEV.Model.Master;
using System.Xml.Linq;

namespace Dev.Repository
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
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.CommonMaster + MasterKey + venueno + venuebranchno;
                objresult = MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                if (objresult == null || MasterKey == "MediaLab" || MasterKey == "ANALYZER" || MasterKey == "PARAMNAME" || MasterKey == "PRODUCTMASTER" || MasterKey == "CountryName" || MasterKey == "StateName" || MasterKey == "CityName" || MasterKey == "countrymaster" || objresult.Count()==0)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("venueno", venueno);
                        var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                        var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                        objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_CommonDetails @MasterKey,@venueno,@venuebranchno", _MasterKey, _venueno, _venuebranchno).ToList();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetCommonMasterList-" + MasterKey, ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;   
        }


        /// <summary>
        /// GetConfigurationList
        /// </summary>
        /// <param name="venueno"></param>
        /// <param name="venuebranchno"></param>
        /// <returns></returns>
        public List<ConfigurationDto> GetConfigurationList(int venueno, int venuebranchno)
        {
            List<ConfigurationDto> objresult = new List<ConfigurationDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.ConfigurationMaster  + venueno + venuebranchno;
                objresult = MemoryCacheRepository.GetCacheItem<List<ConfigurationDto>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("VenueNo", venueno);
                        var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);
                        objresult = context.ConfigurationDTO.FromSqlRaw("Execute dbo.pro_GetConfiguration @VenueNo,@VenueBranchNo", _venueno, _venuebranchno).ToList();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetConfigurationList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;
        }

        /// <summary>
        /// Get Search CommonMaster List
        /// </summary>
        /// <param name="CommonKey"></param>
        /// <returns></returns>
        public List<CommonMasterDto> GetSearchCommonMasterList(int venueno, int venuebranchno, string MasterKey, string MasterValue)
        {
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            try
            {

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                    var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                    var _MasterValue = new SqlParameter("MasterValue", MasterValue);
                    objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_SearchCommonDetails @MasterKey,@MasterValue,@venueno,@venuebranchno", _MasterKey, _MasterValue, _venueno, _venuebranchno).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetSearchCommonMasterList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;
        }

        public List<TblDepartment> GetDepartmentList(int VenueNo, int VenueBranchNo)
        {
            List<TblDepartment> objresult = new List<TblDepartment>();
            try
            {
                string _CacheKey = CacheKeys.tblDepartmentList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<TblDepartment>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        objresult = context.TblDepartment.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetDepartmentList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblMethod> GetMethodList(int VenueNo, int VenueBranchNo)
        {
            List<TblMethod> objresult = new List<TblMethod>();
            try
            {
                string _CacheKey = CacheKeys.tblMethodList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<TblMethod>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        objresult = context.TblMethod.Where(a => a.VenueNo == VenueNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetMethodList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblUnits> GetUnitsList(int VenueNo, int VenueBranchNo)
        {
            List<TblUnits> objresult = new List<TblUnits>();
            try
            {
                string _CacheKey = CacheKeys.tblUnitsList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<TblUnits>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        objresult = context.TblUnits.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetUnitsList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblOrganism> GetOrganismList(int VenueNo, int VenueBranchNo)
        {
            List<TblOrganism> objresult = new List<TblOrganism>();
            try
            {
                string _CacheKey = CacheKeys.tblOrganismList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<TblOrganism>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        objresult = context.TblOrganism.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo ==VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetOrganismList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<lstotdrugmap> GetOrgTypeAntiMapList(int VenueNo, int VenueBranchNo)
        {
            List<lstotdrugmap> objresult = new List<lstotdrugmap>();
            try
            {
                string _CacheKey = CacheKeys.tblOrgTypeAntiMapList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<lstotdrugmap>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var lst = context.GetOrgTypeAntiMapList.FromSqlRaw("Execute dbo.pro_GetOrgTypeAntiMap").ToList();

                        objresult = lst.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetOrgTypeAntiMapList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<TblTemplate> GetTemplateList(int VenueNo, int VenueBranchNo)
        {
            List<TblTemplate> objresult = new List<TblTemplate>();
            try
            {
                string _CacheKey = CacheKeys.tblTemplateList + VenueNo + VenueBranchNo;
                objresult = MemoryCacheRepository.GetCacheItem<List<TblTemplate>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var lst = context.GetTemplateList.FromSqlRaw("Execute dbo.pro_GetTemplateMaster").ToList();

                        objresult = lst.Where(a => a.VenueNo == VenueNo
                        && a.VenueBranchNo == VenueBranchNo && a.Status == true).ToList();
                        //
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTemplateList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<CommonMasterDto> GetVenueDetails(int venueno, int venuebranchno, string MasterKey)
        {
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", venuebranchno);
                    var _MasterKey = new SqlParameter("MasterKey", MasterKey);
                    objresult = context.CommonMasterDTO.FromSqlRaw("Execute dbo.pro_CommonDetails @MasterKey,@venueno,@venuebranchno", _MasterKey, _venueno, _venuebranchno).ToList();

                }

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetVenueDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;
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

                    objOutput = context.GetSingleConfiguration.FromSqlRaw("Execute dbo.pro_GetSingleConfiguration @VenueNo, @VenueBranchNo, @ConfigKey", _venueno, _venuebranchno, _configKey).AsEnumerable()?.SingleOrDefault();
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
            List<RefTypeCommonMasterDto> objresult = new List<RefTypeCommonMasterDto>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                string _CacheKey = CacheKeys.RefTypeList + venueno + venuebranchno;
                objresult = MemoryCacheRepository.GetCacheItem<List<RefTypeCommonMasterDto>>(_CacheKey);
                if (objresult == null)
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _venueno = new SqlParameter("VenueNo", venueno);
                        var _venuebranchno = new SqlParameter("VenueBranchNo", venuebranchno);

                        objresult = context.RefTypeListDTO.FromSqlRaw("Execute dbo.pro_RefTypeSettings @VenueNo, @VenueBranchNo", _venueno, _venuebranchno).ToList();
                        
                        objAppSettingResponse = new AppSettingResponse();
                        string AppCacheMemoryTime = "CacheMemoryTime";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);
                        
                        int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? Convert.ToInt32(objAppSettingResponse.ConfigValue) : 0;
                        
                        MemoryCacheRepository.AddItem(_CacheKey, objresult, Convert.ToInt32(cachetime));
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetRefTypeList", ExceptionPriority.Low, ApplicationType.REPOSITORY, venueno, venuebranchno, 0);
            }
            return objresult;
        }

        public TreatmentPlanMaster GetTreatmentMasterDetails(reqTreatmentMaster disName)
        {
            TreatmentPlanMaster objresult = new TreatmentPlanMaster();
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
                objresult.treatmentNo = disName.treatmentNo;
                objresult.treatmentName = disName.treatmentName;
                objresult.diseaseNo = disName.diseaseNo;
                objresult.diseaseName = disName.diseaseName;
                objresult.VenueNo = disName.VenueNo;
                objresult.VenueBranchNo = disName.VenueBranchNo;
                objresult.lstpharmacy = resultPRM;
                objresult.lstProcedures = resultPRO;
                objresult.rate = disName.Rate;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTreatmentMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<reqTreatmentMaster> GetTreatmentMaster(reqTreatmentMaster disName)
        {
            List<reqTreatmentMaster> objresult = new List<reqTreatmentMaster>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TreatmentNo = new SqlParameter("TreatmentNo", disName?.treatmentNo);
                    var _DiseaseNo = new SqlParameter("DiseaseNo", disName?.diseaseNo);
                    var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disName?.PageIndex);

                    objresult = context.GetTreatmentMaster.FromSqlRaw(
                    "Execute dbo.pro_GetTreatmentMaster @TreatmentNo, @DiseaseNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _TreatmentNo, _DiseaseNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterRepository.GetTreatmentMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return objresult;
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

