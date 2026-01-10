using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using DEV.Model.Master;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterRepository _IMasterRepository;
        public MasterController(IMasterRepository noteRepository)
        {
            _IMasterRepository = noteRepository;
        }

        /// <summary>
        /// Get CommonMaster List
        /// </summary>
        /// <param name="CommonKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/GetCommonMasterList")]
        public List<CommonMasterDto> GetCommonMasterList(int VenueNo, int VenueBranchNo, string MasterKey)
        {
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            try
            {
                objresult = _IMasterRepository.GetCommonMasterList(VenueNo, VenueBranchNo, MasterKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetCommonMasterList-" + MasterKey, ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        /// <summary>
        /// GetConfigurationList
        /// </summary>
        /// <param name="venueno"></param>
        /// <param name="venuebranchno"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/GetConfigurationList")]       
        public List<ConfigurationDto> GetConfigurationList(int venueno, int venuebranchno)
        {
            List<ConfigurationDto> objresult = new List<ConfigurationDto>();
            try
            {
                objresult = _IMasterRepository.GetConfigurationList(venueno, venuebranchno);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetConfigurationList", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueno, venuebranchno, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Get Venue details List
        /// </summary>
        /// <param name="CommonKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/GetVenueDetails")]
        public List<CommonMasterDto> GetVenueDetails(int VenueNo, int VenueBranchNo, string MasterKey)
        {
            List<CommonMasterDto> objresult = new List<CommonMasterDto>();
            try
            {
                objresult = _IMasterRepository.GetVenueDetails(VenueNo, VenueBranchNo, MasterKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetVenueDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/Master/GetDepartmentList")]
        public List<TblDepartment> GetDepartmentList(int VenueNo, int VenueBranchNo)
        {
            List<TblDepartment> objresult = new List<TblDepartment>();
            try
            {
                objresult = _IMasterRepository.GetDepartmentList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetDepartmentList" , ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }


        [HttpGet]
        [Route("api/Master/GetMethodList")]
        public List<TblMethod> GetMethodList(int VenueNo, int VenueBranchNo)
        {
            List<TblMethod> objresult = new List<TblMethod>();
            try
            {
                objresult = _IMasterRepository.GetMethodList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetMethodList" , ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/Master/GetUnitsList")]
        public List<TblUnits> GetUnitsList(int VenueNo, int VenueBranchNo)
        {
            List<TblUnits> objresult = new List<TblUnits>();
            try
            {
                objresult = _IMasterRepository.GetUnitsList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetUnitsList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/Master/GetOrganismList")]
        public List<TblOrganism> GetOrganismList(int VenueNo, int VenueBranchNo)
        {
            List<TblOrganism> objresult = new List<TblOrganism>();
            try
            {
                objresult = _IMasterRepository.GetOrganismList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetOrganismList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/Master/GetOrgTypeAntiMapList")]
        public List<lstotdrugmap> GetOrgTypeAntiMapList(int VenueNo, int VenueBranchNo)
        {
            List<lstotdrugmap> objresult = new List<lstotdrugmap>();
            try
            {
                objresult = _IMasterRepository.GetOrgTypeAntiMapList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetOrgTypeAntiMapList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/Master/GetTemplateList")]
        public List<TblTemplate> GetTemplateList(int VenueNo, int VenueBranchNo)
        {
            List<TblTemplate> objresult = new List<TblTemplate>();
            try
            {
                objresult = _IMasterRepository.GetTemplateList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetTemplateList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        //single configuration 
        [HttpGet]
        [Route("api/Master/GetSingleConfiguration")]
        public ConfigurationDto GetSingleConfiguration(int venueno, int venuebranchno,string configkey)
        {
            ConfigurationDto objOutput = new ConfigurationDto();            
            try
            {
                objOutput = _IMasterRepository.GetSingleConfiguration(venueno, venuebranchno, configkey);              
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetSingleConfiguration", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueno, venuebranchno, 0);
            }
            return objOutput;
        }
        //single ap setting
        [HttpGet]
        [Route("api/Master/GetSingleAppSetting")]
        public AppSettingResponse GetSingleAppSetting(string configkey)
        {
            AppSettingResponse objOutput = new AppSettingResponse();
            try
            {
                objOutput = _IMasterRepository.GetSingleAppSetting(configkey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetSingleAppSetting", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objOutput;
        }

        [HttpGet]
        [Route("api/Master/GetRefTypeList")]
        public List<RefTypeCommonMasterDto> GetRefTypeList(int VenueNo, int VenueBranchNo)
        {
            List<RefTypeCommonMasterDto> objresult = new List<RefTypeCommonMasterDto>();
            try
            {
                objresult = _IMasterRepository.GetRefTypeList(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetRefTypeList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Master/InsertTreatmentplan")]
        public TreatmentPlanMasterResponse InsertTreatmentplan(TreatmentPlanMaster req)
        {
            TreatmentPlanMasterResponse result = new TreatmentPlanMasterResponse();
            try
            {
                result = _IMasterRepository.InsertTreatmentplan(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.InsertTreatmentplan", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Master/DeleteTreatmentplan")]
        public TreatmentPlanMasterResponse DeleteTreatmentplan(TreatmentDelMaster req)
        {
            TreatmentPlanMasterResponse result = new TreatmentPlanMasterResponse();
            try
            {
                result = _IMasterRepository.DeleteTreatmentplan(req.treatmentNo, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.DeleteTreatmentplan", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Master/GetTreatmentMaster")]
        public List<reqTreatmentMaster> GetTreatmentMaster(reqTreatmentMaster disName)
        {
            List<reqTreatmentMaster> result = new List<reqTreatmentMaster>();
            try
            {
                result = _IMasterRepository.GetTreatmentMaster(disName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetTreatmentMaster" + disName.treatmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Master/GetTreatmentMasterDetails")]
        public TreatmentPlanMaster GetTreatmentMasterDetails(reqTreatmentMaster disName)
        {
            TreatmentPlanMaster result = new TreatmentPlanMaster();
            try
            {
                result = _IMasterRepository.GetTreatmentMasterDetails(disName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MasterController.GetTreatmentMasterDetails" + disName.treatmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return result;
        }
    }

}