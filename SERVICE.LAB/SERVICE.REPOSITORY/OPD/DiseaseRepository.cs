using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Dev.Repository
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private IConfiguration _config;
        public DiseaseRepository(IConfiguration config) { _config = config; }

        public List<lstDiseaseCategory> GetDiseaseCategorys(reqDiseaseCategory disCat)
        {
            List<lstDiseaseCategory> objresult = new List<lstDiseaseCategory>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseCategoryNo = new SqlParameter("DiseaseCategoryNo", disCat?.DiseaseCategoryNo);
                    var _VenueNo = new SqlParameter("VenueNo", disCat?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disCat?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disCat?.PageIndex);

                    objresult = context.GetDiseaseCategoryData.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseCategory @DiseaseCategoryNo,@VenueNo,@VenueBranchNo,@PageIndex",
                    _DiseaseCategoryNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetDiseaseCategorys", ExceptionPriority.Low, ApplicationType.REPOSITORY, disCat.VenueNo, disCat.VenueBranchNo, 0);
            }
            return objresult;
        }
        public rtnDiseaseCategory InsertDiseaseCategorys(TblDiseaseCategory resq)
        {
            rtnDiseaseCategory result = new rtnDiseaseCategory();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseCategoryNo = new SqlParameter("DiseaseCategoryNo", resq?.DiseaseCategoryNo);
                    var _DiseaseDescription = new SqlParameter("DiseaseDescription", resq?.DiseaseDescription);
                    var _DisSequenceNo = new SqlParameter("DisSequenceNo", resq?.DisSequenceNo);
                    var _DisCatStatus = new SqlParameter("DisCatStatus", resq?.DisCatStatus);
                    var _VenueNo = new SqlParameter("VenueNo", resq?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", resq?.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", resq?.UserNo);
                    var _PageIndex = new SqlParameter("PageIndex", resq?.PageIndex);

                    var obj = context.InsertDiseaseCategoryData.FromSqlRaw(
                    "Execute dbo.pro_InsertDiseaseCategory @DiseaseCategoryNo, @DiseaseDescription, @DisSequenceNo, @DisCatStatus, @VenueNo, @VenueBranchNo, @UserNo, @PageIndex",
                    _DiseaseCategoryNo, _DiseaseDescription, _DisSequenceNo, _DisCatStatus, _VenueNo, _VenueBranchNo, _UserNo, _PageIndex).ToList();
                    
                    result.DiseaseCategoryNo = obj[0].DiseaseCategoryNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertDiseaseCategorys", ExceptionPriority.Low, ApplicationType.REPOSITORY, resq.VenueNo, resq.VenueBranchNo, 0);
            }
            return result;
        }

        public List<lstDiseaseMaster> GetDiseaseMasters(reqDiseaseMaster disName)
        {
            List<lstDiseaseMaster> objresult = new List<lstDiseaseMaster>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", disName?.DiseaseMasterNo);
                    var _DiseaseCategoryNo = new SqlParameter("DiseaseCategoryNo", disName?.DiseaseCategoryNo);
                    var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disName?.PageIndex);

                    objresult = context.GetDiseaseMasterData.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseMaster @DiseaseMasterNo, @DiseaseCategoryNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _DiseaseMasterNo, _DiseaseCategoryNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetDiseaseMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return objresult;
        }
        public rtnDiseaseMaster InsertDiseaseMasters(TblDiseaseMaster ress)
        {
            rtnDiseaseMaster result = new rtnDiseaseMaster();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", ress?.DiseaseMasterNo);
                    var _DiseaseCategoryNo = new SqlParameter("DiseaseCategoryNo", ress?.DiseaseCategoryNo);
                    var _DisDescription = new SqlParameter("DisDescription", ress?.DisDescription);
                    var _DisMasSequenceNo = new SqlParameter("DisMasSequenceNo", ress?.DisMasSequenceNo);
                    var _DisStatus = new SqlParameter("DisStatus", ress?.DisStatus);
                    var _ICDCode = new SqlParameter("ICDCode", ress?.ICDCode);
                    var _IsConfidential = new SqlParameter("IsConfidential", ress?.IsConfidential);
                    var _UserNo = new SqlParameter("UserNo", ress?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", ress?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", ress?.VenueBranchNo);

                    var obj = context.InsertDiseaseMasterData.FromSqlRaw(
                    "Execute dbo.pro_InsertDiseaseMaster @DiseaseMasterNo, @DiseaseCategoryNo, @DisDescription, @DisMasSequenceNo, @DisStatus, @ICDCode, @IsConfidential, @UserNo, @VenueNo, @VenueBranchNo",
                    _DiseaseMasterNo, _DiseaseCategoryNo, _DisDescription, _DisMasSequenceNo, _DisStatus, _ICDCode, _IsConfidential, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.DiseaseMasterNo = obj[0].DiseaseMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertDiseaseMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, ress.VenueNo, ress.VenueBranchNo, 0);
            }
            return result;
        }
        public int InsertDiseaseTemplateText(lstDiseaseTemplateList req)
        {
            int templateNo = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _tempdiseaseNo = new SqlParameter("tempdiseaseNo", req.tempdiseaseNo);
                    var _templateNo = new SqlParameter("templateNo", req.templateNo);
                    var _templateName = new SqlParameter("templateName", req.templateName);
                    var _isDefault = new SqlParameter("isDefault", req.isDefault);
                    var _sequenceNo = new SqlParameter("sequenceNo", req.sequenceNo);
                    var _status = new SqlParameter("status", req.status);
                    var _userno = new SqlParameter("userno", req.userNo);
                    var _venueno = new SqlParameter("venueno", req.venueNo);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);
                    var _templateText = new SqlParameter("templateText", req.templateText);

                    var lst = context.InsertDiseaseTemplateText.FromSqlRaw(
                    "Execute dbo.pro_InsertDiseaseTemplateText @tempdiseaseNo,@templateNo,@templateName,@isDefault,@sequenceNo,@status,@userno,@venueno,@venuebranchno,@templateText",
                    _tempdiseaseNo, _templateNo, _templateName, _isDefault, _sequenceNo, _status, _userno, _venueno, _venuebranchno, _templateText).ToList();

                    templateNo = lst[0].templateNo;
                    if (templateNo > 0)
                    {
                        MasterRepository _IMasterRepository = new MasterRepository(_config);
                        AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                        objAppSettingResponse = new AppSettingResponse();

                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("OPDMasterTemplateFilePath");
                        string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                        path = path + req.venueNo.ToString() + "/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string createText = req.templateText + Environment.NewLine;
                        File.WriteAllText(path + templateNo + ".ym", createText);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertDiseaseTemplateText" + req.tempdiseaseNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return templateNo;
        }
        public reqresponse GetDiseaseTemplateText(lstDiseaseTemplateList req)
        {
            reqresponse obj = new reqresponse();
            try
            {
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();

                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting("OPDMasterTemplateFilePath");
                string path = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                path = path + req.venueNo.ToString() + "/" + req.templateNo.ToString() + ".ym";
                obj.templateText = File.ReadAllText(path).ToString();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetDiseaseTemplateText", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        public List<lstDiseaseTemplateList> GetTemplateList(int VenueNo, int VenueBranchNo,int TemplateNo, int TempDiseaseNo)
        {
            List<lstDiseaseTemplateList> objresult = new List<lstDiseaseTemplateList>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TemplateNo = new SqlParameter("TemplateNo", TemplateNo);
                    var _TempDiseaseNo = new SqlParameter("TempDiseaseNo", TempDiseaseNo);
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);

                    objresult = context.GetTemplateList.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseTemplate @TemplateNo, @TempDiseaseNo, @VenueNo, @VenueBranchNo",
                    _TemplateNo,_TempDiseaseNo, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetTemplateList", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<DiseaseVsProductMapping> GetDiseaseVsDrugMaster(reqDiseaseMaster disName)
        {
            List<DiseaseVsProductMapping> objresult = new List<DiseaseVsProductMapping>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", disName?.DiseaseMasterNo);
                    var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disName?.PageIndex);

                    objresult = context.GetDiseaseVsDrugMaster.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseVsDrugMaster @DiseaseMasterNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _DiseaseMasterNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetDiseaseVsDrugMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<DiseaseVsTestMapping> GetDiseaseVsTestMaster(reqDiseaseMaster disName)
        {
            List<DiseaseVsTestMapping> objresult = new List<DiseaseVsTestMapping>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", disName?.DiseaseMasterNo);
                    var _VenueNo = new SqlParameter("VenueNo", disName?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", disName?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", disName?.PageIndex);

                    objresult = context.GetDiseaseVsTestMaster.FromSqlRaw(
                    "Execute dbo.pro_GetDiseaseVsTestMaster @DiseaseMasterNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _DiseaseMasterNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetDiseaseVsTestMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return objresult;
        }
        public rtnDisVsDrugMaster InsertDisVsDrugMaster(reqDisVsDrugMaster res)
        {
            rtnDisVsDrugMaster result = new rtnDisVsDrugMaster();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseVsProductMappingNo = new SqlParameter("DiseaseVsProductMappingNo", res?.DiseaseVsProductMappingNo);
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", res?.DiseaseMasterNo);
                    var _ProductMasterNo = new SqlParameter("ProductMasterNo", res?.ProductMasterNo);
                    var _Status = new SqlParameter("Status", res?.Status);
                    var _UserNo = new SqlParameter("UserNo", res?.CreatedBy);
                    var _VenueNo = new SqlParameter("VenueNo", res?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", res?.VenueBranchNo);

                    var obj = context.InsertDisVsDrugMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertDisVsDrugMaster @DiseaseVsProductMappingNo, @DiseaseMasterNo, @ProductMasterNo, @Status, @UserNo, @VenueNo, @VenueBranchNo",
                    _DiseaseVsProductMappingNo, _DiseaseMasterNo, _ProductMasterNo, _Status, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.DiseaseVsProductMappingNo = obj[0].DiseaseVsProductMappingNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertDisVsDrugMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return result;
        }

        public rtnDisVsInvMaster InsertDisVsInvMaster(reqDisVsInvMaster res)
        {
            rtnDisVsInvMaster result = new rtnDisVsInvMaster();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _DiseaseVsTestMappingNo = new SqlParameter("DiseaseVsTestMappingNo", res?.DiseaseVsTestMappingNo);
                    var _DiseaseMasterNo = new SqlParameter("DiseaseMasterNo", res?.DiseaseMasterNo);
                    var _TestNo = new SqlParameter("TestNo", res?.TestNo);
                    var _Status = new SqlParameter("Status", res?.Status);
                    var _UserNo = new SqlParameter("UserNo", res?.CreatedBy);
                    var _VenueNo = new SqlParameter("VenueNo", res?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", res?.VenueBranchNo);

                    var obj = context.InsertDisVsInvMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertDisVsInvMaster @DiseaseVsTestMappingNo, @DiseaseMasterNo, @TestNo, @Status, @UserNo, @VenueNo, @VenueBranchNo",
                    _DiseaseVsTestMappingNo, _DiseaseMasterNo, _TestNo, _Status, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.DiseaseVsTestMappingNo = obj[0].DiseaseVsTestMappingNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertDisVsInvMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return result;
        }

        public List<MachineMasterDTO> GetMachineMaster(reqMachineMaster param)
        {
            List<MachineMasterDTO> objresult = new List<MachineMasterDTO>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", param?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", param?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", param?.PageIndex);

                    objresult = context.GetMachineResult.FromSqlRaw(
                    "Execute dbo.pro_GetMachineMaster @VenueNo, @VenueBranchNo, @PageIndex",
                    _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.GetMachineMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, param.VenueNo, param.VenueBranchNo, 0);
            }
            return objresult;
        }
        public reqMachineMasterResponse InsertMachineResult(InvMachineMasterRequest res)
        {
            reqMachineMasterResponse result = new reqMachineMasterResponse();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    XDocument timexml = new XDocument(new XElement("TimeXML", from Item in res.timeslst
                                                                                    select
                    new XElement("TimeList",
                    new XElement("DayNo", Item.DayNo),
                    new XElement("SessionCode", Item.SessionCode),
                    new XElement("StartTime", Item.StartTime),
                    new XElement("EndTime", Item.EndTime)
                    )));
                    var _machineNo = new SqlParameter("machineNo", res?.machineNo);
                    var _machineName = new SqlParameter("machineName", res?.machineName);
                    var _duration = new SqlParameter("duration", res?.duration);
                    var _TimeXML = new SqlParameter("TimeXML", timexml.ToString());
                    var _Status = new SqlParameter("Status", res?.Status);
                    var _UserNo = new SqlParameter("UserNo", res?.CreatedBy);
                    var _VenueNo = new SqlParameter("VenueNo", res?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", res?.VenueBranchNo);

                    var obj = context.InsertMachineResult.FromSqlRaw(
                    "Execute dbo.pro_InsertMachineMaster @machineNo, @machineName,@duration,@TimeXML, @Status, @UserNo, @VenueNo, @VenueBranchNo",
                    _machineNo, _machineName, _duration, _TimeXML,_Status, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.machineNo = obj[0].machineNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseRepository.InsertMachineResult", ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return result;
        }
    }
}