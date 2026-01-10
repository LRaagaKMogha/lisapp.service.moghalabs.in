using Dev.IRepository.UserManagement;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.UserManagement;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Repository.UserManagement
{
    public class CommonConfigurationRepository: ICommonConfigurationRepository
    {
        private readonly IConfiguration _config;
        public CommonConfigurationRepository(IConfiguration config)
        {
            _config = config;
        }

        public List<CommonConfigurationResponseDTO> GetCommonConfiguration(CommonConfigurationRequestDTO request)
        {
            List<CommonConfigurationResponseDTO>objResult = new List<CommonConfigurationResponseDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _VenueBranchNo = new SqlParameter("@VenueBranchNo", request.VenueBranchNo);
                    var _UserNo = new SqlParameter("@UserNo", request.UserNo);
                    objResult = context.GetCommonConfiguration.FromSqlRaw(
                        "EXEC dbo.Pro_GetCommonConfiguration @VenueNo,@VenueBranchNo, @UserNo",
                        _VenueNo, _VenueBranchNo, _UserNo
                    ).ToList();
                }
            }
            catch (Exception ex) 
            {
                MyDevException.Error(ex, "CommonConfigurationRepository.GetCommonConfiguration", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResult;
        }
        public int InsertCommonConfiguration(CommonConfigurationInsertDTO dto)
        {
            try
            {
                var helper = new CommonHelper();
                var CommonConfiguration = helper.ToXML(dto.CommonConfigurationList);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CommonConfiguration = new SqlParameter("@CommonConfiguration", CommonConfiguration);
                    var _UserNo = new SqlParameter("@UserNo", dto.UserNo);

                    context.Database.ExecuteSqlRaw(
                        "EXEC dbo.Pro_InsertCommonConfiguration @CommonConfiguration, @UserNo",
                        _CommonConfiguration, _UserNo
                    );

                    return 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonConfigurationRepository.InsertCommonConfiguration",
                    ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                return 0;
            }
        }
        public List<CommonMasterDto> GetAllBranches(CommonMasterRequestDTO request)
        {
            List<CommonMasterDto> objResult = new List<CommonMasterDto>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    objResult = context.CommonMasterDTO
                        .FromSqlRaw("EXEC dbo.pro_GetAllBranches @VenueNo", _VenueNo)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonConfigurationRepository.GetAllBranches",
                    ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, 0, 0);
            }

            return objResult;
        }

    }
}

