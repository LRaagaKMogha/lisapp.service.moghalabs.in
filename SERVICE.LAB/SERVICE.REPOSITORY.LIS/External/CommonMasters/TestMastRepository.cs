using Dev.IRepository.External.CommonMasters;
using DEV.Common;
using DEV.Model.EF.External.CommonMasters;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.CommonMasters
{
    public class TestMastRepository : ITestMastRepository
    {
        private IConfiguration _config;
        public TestMastRepository(IConfiguration config)
        {
            _config = config;
        }
        public List<LstTestInfo> GetTestList(int pVenueNo, int pVenueBranchNo)
        {
            List<LstTestInfo> objResponse = new List<LstTestInfo>();

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
