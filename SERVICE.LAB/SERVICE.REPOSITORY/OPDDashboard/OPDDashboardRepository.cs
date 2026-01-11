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
    public class OPDDashBoardRepository : IOPDDashBoardRepository
    {
        private IConfiguration _config;
        public OPDDashBoardRepository(IConfiguration config) { _config = config; }

        public List<OPDDashBoardRes> GetOPDDashBoard(OPDDashBoardReq RequestItem)
        {
            List<OPDDashBoardRes> response = new List<OPDDashBoardRes>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _UserType = new SqlParameter("UserType", RequestItem?.UserType);
                    var _UserNo = new SqlParameter("UserNo", RequestItem?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem?.VenueBranchNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem?.PhysicianNo);
                    var _SpecializationNo = new SqlParameter("SpecializationNo", RequestItem?.SpecializationNo);
                    var _DateType = new SqlParameter("DateType", RequestItem?.DateType);
                    var _fromDate = new SqlParameter("fromDate", RequestItem?.fromdate);
                    var _toDate = new SqlParameter("toDate", RequestItem?.todate);

                    var lstResponse = context.GetOPDDashBoardDTO.FromSqlRaw("Execute dbo.Pro_GetOPDDashBoard @UserType,@UserNo,@VenueNo,@VenueBranchNo,@PhysicianNo,@SpecializationNo,@DateType,@fromDate,@toDate",
                    _UserType, _UserNo, _VenueNo, _VenueBranchNo, _PhysicianNo, _SpecializationNo, _DateType, _fromDate, _toDate).ToList();
                    response = lstResponse;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDDashBoardRepository.GetOPDDashBoard/UserNo-" + RequestItem?.UserNo, ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return response;
        }

    }
}
