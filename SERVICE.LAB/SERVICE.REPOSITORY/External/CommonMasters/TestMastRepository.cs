using Service.IRepository.External.CommonMasters;
using Service.Common;
using Service.Model.EF.External.CommonMasters;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Service.Repository.External.CommonMasters
{
    public class TestMastRepository : ITestMastRepository
    {
        private IConfiguration _config;
        public TestMastRepository(IConfiguration config)
        {
            _config = config;
        }
        public List<LsttestInfo> GetTestList(int pVenueNo, int pVenueBranchNo)
        {
            List<LsttestInfo> objResponse = new List<LsttestInfo>();

            try
            {
                using (var context = new TestMastContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetTestList.FromSqlRaw(
                    "Execute dbo.pro_Ex_GetTestListInfo" +
                    " @VenueNo, @VenueBranchNo",
                    _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TestMastRepository.GetTestList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
