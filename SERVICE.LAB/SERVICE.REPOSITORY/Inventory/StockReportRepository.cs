using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Inventory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using RtfPipe.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Dev.Repository.Inventory
{
    public class StockReportRepository :IStockReportReposistory
    {
        private IConfiguration _config;
        public StockReportRepository(IConfiguration config) { _config = config; }

        public List<GetStockReportResponse> GetStockReport(GetStockReportRequest stockreport)
        {
            List<GetStockReportResponse> objresult = new List<GetStockReportResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", stockreport.venueNo);
                    var _VenueBranchNo = new SqlParameter("@VenueBranchNo", stockreport.venueBranchNo);
                    var _BranchNo = new SqlParameter("@BranchNo", stockreport.branchNo);
                    var _StoreNo = new SqlParameter("@StoreNo", stockreport.storeNo);
                    var _CategoryNo = new SqlParameter("@CategoryNo", stockreport.productCategoryNo);
                    var _ProductTypeNo = new SqlParameter("@ProductTypeNo", stockreport.productTypeNo);
                    var _ProductMasterNo = new SqlParameter("@ProductMasterNo", stockreport.productNo);
                    var _FromDate = new SqlParameter("@FromDate", (object)stockreport.fromDate ?? DBNull.Value);
                    var _ToDate = new SqlParameter("@ToDate", (object)stockreport.toDate ?? DBNull.Value);
                    var _Type = new SqlParameter("@Type", stockreport.Type);
                    var _PageIndex = new SqlParameter("PageIndex", stockreport.pageIndex);
                    var _pageCount = new SqlParameter("pageCount", stockreport.pageCount);

                    objresult = context.GetStockReport.FromSqlRaw(
                        "EXEC dbo.pro_GetStockReport @VenueNo, @VenueBranchNo, @BranchNo, @StoreNo , @CategoryNo,  @ProductTypeNo ,@ProductMasterNo,   @FromDate, @ToDate, @Type, @PageIndex, @pageCount",
                        _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo, _CategoryNo, _ProductTypeNo, _ProductMasterNo,_FromDate, _ToDate, _Type, _PageIndex, _pageCount
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockReportReposistory.GetStockReportResponse/VenuNo-" + stockreport.venueNo, ExceptionPriority.High, ApplicationType.REPOSITORY,
                    stockreport.venueNo, stockreport.venueBranchNo, stockreport.userNo);
            }
            return objresult;
        }
}
    }
