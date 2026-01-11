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
    public class PhysicianMastRepository : IPhysicianMastRepository
    {
        private IConfiguration _config;
        public PhysicianMastRepository(IConfiguration config) { _config = config; }
        public List<LstPhysician> GetPhysician(int pVenueNo, int pVenueBranchNo)
        {
            List<LstPhysician> objResponse = new List<LstPhysician>();

            try
            {
                using (var context = new PhysicianContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetPhysician.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetPhysician" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianMastRepository.GetPhysican", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }

        public List<LstInternalPhysician> GetInternalPhysician(int pVenueNo, int pVenueBranchNo)
        {
            List<LstInternalPhysician> objResponse = new List<LstInternalPhysician>();

            try
            {
                using (var context = new InternalPhysicianContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetInternalPhysician.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetInternalPhysician" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PhysicianMastRepository.GetInternalPhysican", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
