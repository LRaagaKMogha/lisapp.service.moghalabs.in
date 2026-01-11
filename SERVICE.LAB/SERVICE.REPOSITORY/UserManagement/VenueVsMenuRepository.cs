using Dev.IRepository.UserManagement;
using DEV.Common;
using Service.Model.EF;
using Service.Model.UserManagement;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Repository.UserManagement
{
    public class VenueVsMenuRepository : IVenueVsMenuRepository
    {
        private readonly IConfiguration _config;

        public VenueVsMenuRepository(IConfiguration config)
        {
            _config = config;
        }

        public List<VenueVsMenuResponseDTO> GetVenueVsMenu(VenueVsMenuRequestDTO request)
        {
            List<VenueVsMenuResponseDTO> objResult = new List<VenueVsMenuResponseDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ModuleId = new SqlParameter("@ModuleId", request.ModuleId);
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _UserNo = new SqlParameter("@UserNo", request.UserNo);

                    objResult = context.GetVenueVsMenu.FromSqlRaw(
                        "EXEC dbo.pro_GetVenueVsMenu @ModuleId, @VenueNo, @UserNo",
                        _ModuleId, _VenueNo, _UserNo
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VenueVsMenuRepository.GetVenueVsMenu", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResult;
        }

        public int InsertVenueVsMenu(VenueVsMenuInsertDTO dto)
        {
            try
            {
                var helper = new CommonHelper();
                var VenueVsMenu = helper.ToXML(dto.MenuVenueList);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueVsMenu = new SqlParameter("@VenueVsMenu", VenueVsMenu);
                    var _UserNo = new SqlParameter("@UserNo", dto.UserNo);

                    context.Database.ExecuteSqlRaw(
                        "EXEC dbo.pro_InsertVenueVsMenu @VenueVsMenu, @UserNo",
                        _VenueVsMenu, _UserNo
                    );

                    return 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VenueVsMenuRepository.InsertVenueVsMenu",
                    ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
                return 0;
            }
        }
    }
}
