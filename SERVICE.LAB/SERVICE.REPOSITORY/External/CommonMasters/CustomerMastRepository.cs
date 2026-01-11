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
    public class CustomerMastRepository : ICustomerMastRepository
    {
        private IConfiguration _config;
        public CustomerMastRepository(IConfiguration config) { _config = config; }
        public List<LstCustomer> GetCustomer(int pVenueNo, int pVenueBranchNo)
        {
            List<LstCustomer> objResponse = new List<LstCustomer>();

            try
            {
                using (var context = new CustomerContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);

                    objResponse = context.GetCustomer.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetCustomer" +
                           " @VenueNo, @VenueBranchNo",
                             _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CustomerMastRepository.GetCustomer", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
