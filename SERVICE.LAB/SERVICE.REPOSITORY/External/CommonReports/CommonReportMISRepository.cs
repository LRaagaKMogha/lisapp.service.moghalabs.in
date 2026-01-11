using Dev.IRepository.External.CommonReports;
using DEV.Common;
using Service.Model.EF;
using Service.Model.EF.External.CommonReports;
using Service.Model.External.CommonReports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.External.CommonReports
{
    public class CommonReportMISRepository : ICommonReportMISRepository
    {
        private IConfiguration _config;
        public CommonReportMISRepository(IConfiguration config) { _config = config; }
        public List<LstPaidInformation> GetPaymentCollection(int pVenueNo, int pVenueBranchNo, string pDtFrom, string pDtTo)
        {
            List<LstPaidInformation> objResponse = new List<LstPaidInformation>();

            try
            {
                using (var context = new MISReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", pVenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", pVenueBranchNo);
                    var _dtFrom = new SqlParameter("FromDate", pDtFrom);
                    var _dtTo = new SqlParameter("ToDate", pDtTo);

                    objResponse = context.FetchPaidInformation.FromSqlRaw(
                           "Execute dbo.pro_Ex_GetCollectionMIS" +
                           " @VenueNo, @VenueBranchNo, @FromDate, @ToDate",
                             _venueNo, _venueBranchNo, _dtFrom, _dtTo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonReportRepository.GetPaymentCollection", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objResponse;
        }
    }
}
