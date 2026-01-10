using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Inventory;
using DEV.Model.Inventory.Master;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Repository.Inventory
{
    public class StockAlertRepository:IStockAlertRepository
    {
        private IConfiguration _config;
        public StockAlertRepository(IConfiguration config) { _config = config; }

        public List<GetStockAlertResponse> GetStockAlertsDetails(StockAlertRequest stockAlertRequest)
        {
            List<GetStockAlertResponse>objResult =new List<GetStockAlertResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", stockAlertRequest.VenueNo);
                    var _Venuebranchno = new SqlParameter("Venuebranchno", stockAlertRequest.Venuebranchno);
                    var _PageIndex = new SqlParameter("PageIndex", stockAlertRequest.PageIndex);
                    var _BranchNo = new SqlParameter("BranchNo", stockAlertRequest.BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", stockAlertRequest.StoreNo);
                    var _ProductNo = new SqlParameter("ProductNo", stockAlertRequest.ProductNo);

                    objResult = context.GetStockAlertsDetails.FromSqlRaw(
                    "Execute dbo.Pro_Getstockalert @VenueNo,@VenueBranchNo,@PageIndex,@BranchNo,@StoreNo,@ProductNo", _VenueNo, _Venuebranchno, _PageIndex, _BranchNo, _StoreNo, _ProductNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetStockAlertsDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, stockAlertRequest.VenueNo, 0, 0);
            }
            return objResult;

        }
    }
}
