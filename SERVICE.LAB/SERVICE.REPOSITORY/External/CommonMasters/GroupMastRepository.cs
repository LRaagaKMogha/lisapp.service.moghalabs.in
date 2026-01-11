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
    public class GroupMastRepository : IGroupMastRepository
    {
        private IConfiguration _config;
        public GroupMastRepository(IConfiguration config)
        {
            _config = config;
        }
        public List<LstGroupInfo> GetGroupInfo(int pVenueNo, int pVenueBranchNo)
        {
            List<LstGroupInfo> objResponse = new List<LstGroupInfo>();

            try
            {
                using (var context = new GroupMastContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetGroupList.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetGroupListInfo" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GroupMastRepository.GetGroupList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
