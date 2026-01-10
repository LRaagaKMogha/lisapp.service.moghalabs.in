using Dev.IRepository.External.CommonMasters;
using DEV.Common;
using DEV.Model.EF.External.CommonMasters;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.CommonMasters
{
    public class PackageMastRepository : IPackageMastRepository
    {
        private IConfiguration _config;
        public PackageMastRepository(IConfiguration config)
        {
            _config = config;
        }
        public List<LstPackageInfo> GetPackageInfo(int pVenueNo, int pVenueBranchNo)
        {
            List<LstPackageInfo> objResponse = new List<LstPackageInfo>();

            try
            {
                using (var context = new PackageMastContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetPackageList.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetPackageListInfo" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PackageMastRepository.GetPackageList", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }

        public LstPackageBreakUpInfo GetPackageBreakUp(int pVenueNo, int pVenueBranchNo, int pPackageNo)
        {
            LstPackageBreakUpInfo objResponse = new LstPackageBreakUpInfo();

            try
            {
                using (var context = new PackageMastContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _packageNo = new SqlParameter("PackageNo", pPackageNo); 

                    var lstObj = context.GetPackageBreakUp.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetPackageBreakUp" +
                           " @VenueNo, @VenueBranchNo, @PackageNo",
                             _venueNo, _venueBranchNo, _packageNo).ToList();

                    objResponse.packageNo = lstObj[0].packageNo;
                    objResponse.packageName = lstObj[0].packageName;
                    objResponse.servicesList = JsonConvert.DeserializeObject<List<LstPackageServiceList>>(lstObj[0].ListOfServices);                     
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PackageMastRepository.GetPackageBreakUp", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
