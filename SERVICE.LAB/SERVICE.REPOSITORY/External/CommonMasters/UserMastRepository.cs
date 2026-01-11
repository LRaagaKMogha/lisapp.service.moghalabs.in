using Dev.IRepository.External.CommonMasters;
using DEV.Common;
using Service.Model.EF.External.CommonMasters;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.CommonMasters
{
    public class UserMastRepository : IUserMastRepository
    {
        private IConfiguration _config;
        
        public UserMastRepository(IConfiguration config)
        {
            _config = config;
        }

        public List<LstUser> GetUserList(int pVenueNo, int pVenueBranchNo)
        {
            List<LstUser> objResponse = new List<LstUser>();

            try
            {
                using (var context = new UserContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.FetchUser.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetUserList" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "UserMastRepository.GetUserList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
