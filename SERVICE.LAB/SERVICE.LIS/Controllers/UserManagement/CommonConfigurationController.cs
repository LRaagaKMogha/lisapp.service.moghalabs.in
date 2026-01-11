using Dev.IRepository.UserManagement;
using Dev.Repository.UserManagement;
using DEV.Common;
using Service.Model;
using Service.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
namespace DEV.API.SERVICE.Controllers.UserManagement
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CommonConfigurationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICommonConfigurationRepository _CommonConfigurationRepository;
        public CommonConfigurationController(IConfiguration config, ICommonConfigurationRepository CommonConfigurationRepository)
        {
            _config = config;
            _CommonConfigurationRepository = CommonConfigurationRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/CommonConfiguration/GetCommonConfiguration")]
        public List<CommonConfigurationResponseDTO> GetCommonConfiguration(CommonConfigurationRequestDTO request)
        {
            List<CommonConfigurationResponseDTO> objResult = new List<CommonConfigurationResponseDTO>();
            try
            {
                objResult = _CommonConfigurationRepository.GetCommonConfiguration(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonConfigurationController.GetCommonConfiguration",
                    ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objResult;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/CommonConfiguration/InsertCommonConfiguration")]
        public int InsertCommonConfiguration([FromBody] CommonConfigurationInsertDTO request)
        {
            int result = 0;
            try
            {
                result = _CommonConfigurationRepository.InsertCommonConfiguration(request);
                int venuno = request != null && request.CommonConfigurationList != null
                    && request.CommonConfigurationList.Count > 0 ? request.CommonConfigurationList[0].VenueNo : 0;
                int venuebranchno = request != null && request.CommonConfigurationList != null
                    && request.CommonConfigurationList.Count > 0 ? request.CommonConfigurationList[0].VenueBranchNo : 0;
                string _CacheKey = CacheKeys.ConfigurationMaster + venuno + venuebranchno;
                MemoryCacheRepository.GetCacheItem<List<CommonMasterDto>>(_CacheKey);
                MemoryCacheRepository.RemoveItem(_CacheKey);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonConfigurationController.InsertCommonConfiguration",
                    ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/CommonConfiguration/GetAllBranches")]
        public List<CommonMasterDto> GetAllBranches([FromBody] CommonMasterRequestDTO request)
        {
            List<CommonMasterDto> objResult = new List<CommonMasterDto>();
            try
            {
                objResult = _CommonConfigurationRepository.GetAllBranches(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonConfigurationController.GetAllBranches",
                    ExceptionPriority.Low, ApplicationType.APPSERVICE, request.VenueNo, 0, 0);
            }
            return objResult;
        }

    }
}
