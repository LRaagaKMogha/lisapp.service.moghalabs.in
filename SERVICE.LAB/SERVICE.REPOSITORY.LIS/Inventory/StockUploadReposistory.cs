using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.Inventory
{
    public class StockUploadReposistory: IStockUploadReposistory
    {
        private IConfiguration _config;
        public StockUploadReposistory(IConfiguration config) { _config = config; }
        
        public List<GetStockProductListResponse> GetProductListByDepartment(int venueNo, int venueBranchNo, int branchNo,int StoreNo)
        {
            List<GetStockProductListResponse> objresult = new List<GetStockProductListResponse>();
            try
            {              
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _BranchNo = new SqlParameter("BranchNo", branchNo);
                    var _StoreNo = new SqlParameter("StoreNo", StoreNo);

                    objresult = context.GetProductListByDeapartmentDTO.FromSqlRaw(
                    "Execute dbo.pro_GetStockUploadProducts @VenueNo,@VenueBranchNo,@BranchNo,@StoreNo",
                    _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadReposistory.GetProductListByDepartment", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, branchNo);
            }
            return objresult;
        }

        //public List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber)
        //{
        //    List<GetProductsByPOResponse> objresult = new List<GetProductsByPOResponse>();
        //    try
        //    {
        //        using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
        //        {
        //            var _VenueNo = new SqlParameter("VenueNo", venueNo);
        //            var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
        //            var _poNumber = new SqlParameter("PONo", poNumber);
        //            objresult = context.GetProductByPODTO.FromSql(
        //                "Execute dbo.pro_IV_GetProductsByPO @VenueNo,@VenueBranchNo,@PONo",
        //             _VenueNo, _VenueBranchNo, _poNumber).ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MyDevException.Error(ex, "StockUploadReposistory.GetProductByPO", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, poNumber);
        //    }
        //    return objresult;
        //}

        public CommonAdminResponse InsertStockUpload(InsertStockUploadRequest insertStockUpload)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var StockUploadXML = commonUtility.ToXML(insertStockUpload);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertStockUpload?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertStockUpload?.venueBranchNo);
                    var _StockUploadXML = new SqlParameter("ProductStockXML", StockUploadXML);
                    var _UserNo = new SqlParameter("UserNo", insertStockUpload?.createdby);

                    var objresult = context.CreateStockUploadDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertProductStock @VenueNo, @VenueBranchNo, @ProductStockXML, @UserNo",
                    _VenueNo, _VenueBranchNo, _StockUploadXML, _UserNo).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadReposistory.InsertStockUpload", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertStockUpload.venueNo, insertStockUpload.venueBranchNo, insertStockUpload.createdby);
            }
            return response;
        }
        public List<GetProductMainbyDeptRes> GetProductSubyMaindept(GetProductMainbyDeptReq Req)
        {
            List<GetProductMainbyDeptRes> objresult = new List<GetProductMainbyDeptRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", Req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Req?.VenueBranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", Req?.StoreNo);
                    var _BranchNo = new SqlParameter("BranchNo", Req?.BranchNo);

                    objresult = context.GetProductSubyMaindept.FromSqlRaw(
                    "Execute dbo.pro_GetProductSubandMaindept @VenueNo,@VenueBranchNo,@StoreNo,@BranchNo",
                    _VenueNo, _VenueBranchNo, _StoreNo, _BranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadReposistory.GetProductSubyMaindept", ExceptionPriority.High, ApplicationType.REPOSITORY, Req.VenueNo,Req.VenueBranchNo, Req.StoreNo);
            }
            return objresult;
        }
    }
}