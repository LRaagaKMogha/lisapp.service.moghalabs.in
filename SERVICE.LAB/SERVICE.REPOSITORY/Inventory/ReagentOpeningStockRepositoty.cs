using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.Inventory
{
    public class ReagentOpeningStockRepositoty: IReagentOpeningStockRepositoty
    {
        private IConfiguration _config;
        public ReagentOpeningStockRepositoty(IConfiguration config) { _config = config; }
        public List<ReagentOpeningStockResponse> GetAllReagentOpeningStock(GetReagentStockRequest request)
        {
            List<ReagentOpeningStockResponse> objresult = new List<ReagentOpeningStockResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _masterNo = new SqlParameter("MasterNo", request.masterNo);
                    var _userNo = new SqlParameter("UserNo", request.userno);
                    var _pageIndex = new SqlParameter("PageIndex", request.pageIndex);
                    var _branchNo = new SqlParameter("BranchNo", request.branchNo);
                    var _storeNo = new SqlParameter("MainDeptNo", request.storeNo);
                    var _productNo = new SqlParameter("ProductNo", request.productNo);

                    objresult = context.GetReagentOpeningStockDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllReagentOpeningStock @VenueNo,@VenueBranchNo,@MasterNo,@UserNo,@PageIndex,@BranchNo,@MainDeptNo,@SubdeptNo,@ProductNo",
                    _venueNo, _venueBranchNo, _masterNo, _userNo, _pageIndex, _branchNo, _storeNo, _productNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReagentOpeningStockRepositoty.GetAllReagentOpeningStock", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, (int)request.venuebranchno, (int)request.masterNo);
            }
            return objresult;
        }

        public CommonAdminResponse InsertReagentOpeningStock(InsertReagentOpeningStockRequest insertReagentStock)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var reagentXML = commonUtility.ToXML(insertReagentStock);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertReagentStock?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertReagentStock?.VenueBranchNo);
                    var _ReagentXML = new SqlParameter("ReagentXML", reagentXML);
                    var _UserNo = new SqlParameter("UserNo", insertReagentStock?.Createdby);

                    var objresult = context.CreateReagentOpeningStockDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertReagentOpeningStock @VenueNo,@VenueBranchNo,@ReagentXML,@UserNo",
                    _VenueNo, _VenueBranchNo, _ReagentXML, _UserNo).ToList();

                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ReagentOpeningStockRepositoty.InsertReagentOpeningStock", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertReagentStock.VenueNo, insertReagentStock.VenueBranchNo, insertReagentStock.Createdby);
            }
            return response;
        }
    }
}
