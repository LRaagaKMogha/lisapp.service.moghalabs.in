using Dev.IRepository;
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
    public class DeptMastRepository : IDeptMastRepository
    {
        private IConfiguration _config;
        public DeptMastRepository(IConfiguration config) { _config = config; }
        public List<LstDepartment> GetDepartment(int pVenueNo, int pVenueBranchNo)
        {
            List<LstDepartment> objResponse = new List<LstDepartment>();

            try
            {
                using (var context = new DepartmentContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetDepartment.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetDepartment" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DepartmentRepository.GetDepartment", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }

        public List<LstMainDepartment> GetMainDepartment(int pVenueNo, int pVenueBranchNo)
        {
            List<LstMainDepartment> objResponse = new List<LstMainDepartment>();

            try
            {
                using (var context = new DepartmentContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetMainDepartment.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetMainDepartment" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DepartmentRepository.GetMainDepartment", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
