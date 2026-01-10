using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class ICMRResponseRepository : IICMRResponseRepository
    {
        private IConfiguration _config;
        public ICMRResponseRepository(IConfiguration config) { _config = config; }


        public List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO RequestItem)
        {
            List<GetICMRResponse> lstGetICMRResponse = new List<GetICMRResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);                    
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);                    
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);

                    lstGetICMRResponse = context.GetICMRResponseDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetTATReport @FROMDate,@ToDate,@VenueNo,@VenueBranchNo,@RefferalType,@CustomerNo,@PhysicianNo,@DepartmentNo,@ServiceNo,@ServiceType,@OrderStatus",
                    _FromDate, _ToDate, _VenueNo, _VenueBranchNo, _CustomerNo).ToList();


                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReportRepository.GetICMRResult/CustomerNo-" + RequestItem.CustomerNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstGetICMRResponse;
        }
    }
}
