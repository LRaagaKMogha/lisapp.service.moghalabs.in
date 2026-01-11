using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
   public class DashBoardRepository : IDashBoardRepository
    {
        private IConfiguration _config;
        public DashBoardRepository(IConfiguration config) { _config = config; }

        public List<DashBoardResponse> GetDashBoards(DashBoardRequest RequestItem)
        {
            List<DashBoardResponse> response = new List<DashBoardResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _UserType = new SqlParameter("UserType", RequestItem?.UserType);
                    var _UserNo = new SqlParameter("UserNo", RequestItem?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _DateType = new SqlParameter("DateType", RequestItem?.DateType);
                    var _fromDate = new SqlParameter("fromDate", RequestItem?.fromdate);
                    var _toDate = new SqlParameter("toDate", RequestItem?.todate);

                    var lstResponse = context.GetDashBoardsDTO.FromSqlRaw("Execute dbo.Pro_GetDashBoard @UserType,@UserNo,@VenueNo,@VenueBranchNo,@DateType,@fromDate,@toDate",
                    _UserType, _UserNo, _VenueNo, _VenueBranchNo, _DateType,_fromDate,_toDate).ToList();
                    response = lstResponse;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DashBoardRepository.GetDashBoards/UserNo-" + RequestItem?.UserNo, ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return response;
        }

    }
}
