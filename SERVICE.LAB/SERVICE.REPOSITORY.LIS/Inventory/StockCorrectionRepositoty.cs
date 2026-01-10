using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Repository.Inventory
{
    public class StockCorrectionRepositoty : IStockCorrectionRepositoty
    {
        private IConfiguration _config;
        public StockCorrectionRepositoty(IConfiguration config) { _config = config; }
        public List<GetStockCorrectionResponse> GetAllStockCorrection(GetStockCorrectionRequest request)
        {
            List<GetStockCorrectionResponse> objresult = new List<GetStockCorrectionResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _consumptionNo = new SqlParameter("MasterNo", request.masterNo);
                    var _userNo = new SqlParameter("UserNo", request.userno);
                    var _pageIndex = new SqlParameter("PageIndex", request.pageIndex);
                    var _branchNo = new SqlParameter("BranchNo", request.BranchNo);
                    var _storeNo = new SqlParameter("StoreNo", request.StoreNo);
                    var _productNo = new SqlParameter("ProductNo", request.ProductNo);

                    objresult = context.GetStockCorrectionDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllStockCorrection @VenueNo, @VenueBranchNo, @MasterNo, @BranchNo, @StoreNo, @ProductNo, @UserNo, @PageIndex",
                    _venueNo, _venueBranchNo, _consumptionNo, _branchNo, _storeNo, _productNo, _userNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetAllStockCorrection", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, (int)request.venuebranchno, (int)request.masterNo);
            }
            return objresult;
        }
        public CommonAdminResponse InsertStockCorrection(InsertStockCorrection stockCorrection)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _StockCorrectionNo = new SqlParameter("StockCorrectionNo", stockCorrection?.StockCorrectionNo);
                    var _VenueNo = new SqlParameter("VenueNo", stockCorrection?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", stockCorrection?.VenueBranchNo);
                    var _BranchNo = new SqlParameter("BranchNo", stockCorrection?.BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", stockCorrection?.StoreNo);
                    var _ProductNo = new SqlParameter("ProductNo", stockCorrection?.ProductNo);
                    var _OpenQty = new SqlParameter("OpenQty", stockCorrection?.OpenQty);
                    var _CloseQty = new SqlParameter("CloseQty", stockCorrection?.CloseQty);
                    var _AdjustQty = new SqlParameter("AdjustQty", stockCorrection?.AdjustQty);
                    var _Reason = new SqlParameter("Reason", stockCorrection?.Reason);
                    var _Status = new SqlParameter("Status", stockCorrection?.Status);
                    var _UserNo = new SqlParameter("UserNo", stockCorrection?.UserNo);

                    var objresult = context.CreateStockCorrectionDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertStockCorrection " +
                    "@StockCorrectionNo, @VenueNo, @VenueBranchNo, @BranchNo, @StoreNo, @ProductNo, @OpenQty, @CloseQty, @AdjustQty, @Reason, @Status, @UserNo",
                    _StockCorrectionNo, _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo, _ProductNo, _OpenQty, _CloseQty, _AdjustQty, _Reason, _Status, _UserNo).ToList();

                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.InsertStockCorrection", ExceptionPriority.Low, ApplicationType.REPOSITORY, stockCorrection.VenueNo, stockCorrection.VenueBranchNo, stockCorrection.UserNo);
            }
            return response;
        }
        public List<GetProductStockResponse> GetProductStock(GetProductStockRequest request)
        {
            List<GetProductStockResponse> objresult = new List<GetProductStockResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.VenueNo);
                    var _BranchNo = new SqlParameter("BranchNo", request.BranchNo);
                    var _storeNo = new SqlParameter("StoreNo", request.StoreNo);
                    var _productNo = new SqlParameter("ProductNo", request.ProductNo);

                    objresult = context.GetProductStockDTO.FromSqlRaw(
                    "Execute dbo.pro_GetProductStock @VenueNo, @BranchNo, @StoreNo, @ProductNo",
                    _venueNo, _BranchNo, _storeNo, _productNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetProductStock", ExceptionPriority.High, ApplicationType.REPOSITORY, request.VenueNo, request.BranchNo, 0);
            }
            return objresult;
        }
        public CommonAdminResponse InsertStockAdjustment(InsertStockAdjustment req)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var prodDetailXML = commonUtility.ToXML(req.ProductDetails);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _StockAdjustNo = new SqlParameter("StockAdjustNo", req.StockAdjustNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _BranchNo = new SqlParameter("BranchNo", req?.BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", req?.StoreNo);
                    var _ProductNo = new SqlParameter("ProductNo", req?.ProductNo);
                    var _Reason = new SqlParameter("Reason", req?.Reason);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _prodDetailXML = new SqlParameter("ProdDetailXML", prodDetailXML);
                    var _menuType = new SqlParameter("MenuType", req?.MenuType);

                    var objresult = context.CreateStockAdjustmentDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertStockAdjustment " +
                    "@StockAdjustNo, @VenueNo, @VenueBranchNo, @BranchNo, @StoreNo, @ProductNo, @Reason, @UserNo, @prodDetailXML, @MenuType",
                    _StockAdjustNo, _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo, _ProductNo, _Reason, _UserNo, _prodDetailXML, _menuType).ToList();

                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.InsertStockAdjustment", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return response;
        }
        public List<GetStockAdjustmentResponse> GetAllStockAdjustment(GetStockAdjustmentRequest request)
        {
            List<GetStockAdjustmentResponse> objresult = new List<GetStockAdjustmentResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _adjustmentNo = new SqlParameter("MasterNo", request.masterNo);
                    var _userNo = new SqlParameter("UserNo", request.userno);
                    var _pageIndex = new SqlParameter("PageIndex", request.pageIndex);
                    var _branchNo = new SqlParameter("BranchNo", request.BranchNo);
                    var _storeNo = new SqlParameter("StoreNo", request.StoreNo);
                    var _productNo = new SqlParameter("ProductNo", request.ProductNo);
                    var _menuType = new SqlParameter("MenuType", request?.MenuType);
                    var _type = new SqlParameter("Type", request?.Type);
                    var _fromDate = new SqlParameter("FromDate", request?.FromDate);
                    var _toDate = new SqlParameter("ToDate", request?.ToDate);

                    objresult = context.GetStockAdjustmentDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllStockAdjustment @Type, @FromDate, @ToDate, @VenueNo, @VenueBranchNo, @MasterNo, @BranchNo, @StoreNo, @ProductNo, @UserNo, @MenuType, @PageIndex",
                    _type, _fromDate, _toDate, _venueNo, _venueBranchNo, _adjustmentNo, _branchNo, _storeNo, _productNo, _userNo, _menuType, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetAllStockAdjustment", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, (int)request.venuebranchno, (int)request.masterNo);
            }
            return objresult;
        }
        public GetStockAdjustProductDetailsResponse GetStockAdjustProductDetails(int venueNo, int venueBranchNo, int StkadjNo)
        {
            GetStockAdjustProductDetailsResponse objresult = new GetStockAdjustProductDetailsResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _StkadjNo = new SqlParameter("stkadjNo", StkadjNo);

                    var lst = context.GetStockAdjustmentProductDTO.FromSqlRaw(
                    "Execute dbo.pro_GetStockAdjustmentProductDetails @VenueNo, @VenueBranchNo, @StkadjNo",
                    _VenueNo, _VenueBranchNo, _StkadjNo).AsEnumerable().FirstOrDefault();
                    if (lst != null)
                    {
                        objresult.BranchNo = lst.BranchNo;
                        objresult.BranchName = lst.BranchName;
                        objresult.StoreNo = lst.StoreNo;
                        objresult.StoreName = lst.StoreName;
                        objresult.ProductNo = lst.ProductNo;
                        objresult.ProductName = lst.ProductName;
                        objresult.Reason = lst.Reason;
                        
                        if (!string.IsNullOrEmpty(lst.productDetails))
                        {
                            objresult.productDetails = JsonConvert.DeserializeObject<List<GetProductStockResponse>>(lst.productDetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetStockAdjustProductDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        public List<GetStoreStockProductListResponse> GetStoreStockProductList(int venueNo, int BranchNo, int StoreNo)
        {
            List<GetStoreStockProductListResponse> objresult = new List<GetStoreStockProductListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _BranchNo = new SqlParameter("BranchNo", BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", StoreNo);

                    objresult = context.GetStockStockProductListDTO.FromSqlRaw(
                    "Execute dbo.pro_GetStoreStockProductDetails @VenueNo, @BranchNo, @StoreNo",
                    _VenueNo, _BranchNo, _StoreNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetStoreStockProductList", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, BranchNo, 0);
            }
            return objresult;
        }
        public CommonAdminResponse InsertStockConsumption(InsertStockConsumption req)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var prodDetailXML = commonUtility.ToXML(req.ProductDetails);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ConsMastNo = new SqlParameter("ConsMastNo", req.ConsMastNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _BranchNo = new SqlParameter("BranchNo", req?.BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", req?.StoreNo);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _MenuType = new SqlParameter("MenuType", req?.MenuType);
                    var _ProdDetailXML = new SqlParameter("ProdDetailXML", prodDetailXML);

                    var objresult = context.CreateStockConsumptionDTO.FromSqlRaw(
                    "Execute dbo.Pro_IV_InsertStockConsumption " +
                    "@ConsMastNo, @VenueNo, @VenueBranchNo, @BranchNo, @StoreNo, @UserNo, @MenuType, @ProdDetailXML",
                    _ConsMastNo, _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo, _UserNo, _MenuType, _ProdDetailXML).ToList();

                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.InsertStockConsumption", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return response;
        }
        public List<GetAllConsumptionListResponse> GetAllConsumptionList(GetAllConsumptionEntryRequest request)
        {
            List<GetAllConsumptionListResponse> objresult = new List<GetAllConsumptionListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", request?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", request?.ToDate);
                    var _Type = new SqlParameter("Type", request?.Type);
                    var _VenueNo = new SqlParameter("VenueNo", request.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _BranchNo = new SqlParameter("BranchNo", request.BranchNo);
                    var _StoreNo = new SqlParameter("StoreNo", request.StoreNo);
                    var _ConsumptionNo = new SqlParameter("ConsumptionNo", request.ConsumptionNo);
                    var _UserNo = new SqlParameter("UserNo", request.userno);
                    var _PageIndex = new SqlParameter("PageIndex", request.pageIndex);
                    var _MenuType = new SqlParameter("MenuType", request?.MenuType);

                    objresult = context.GetAllConsumptionListDTO.FromSqlRaw(
                    "Execute dbo.pro_IV_GetAllConsumptionList " +
                    "@Type, @FromDate, @ToDate, @VenueNo, @VenueBranchNo, @BranchNo, @StoreNo, " +
                    "@ConsumptionNo, @UserNo, @PageIndex, @MenuType",
                    _Type, _FromDate, _ToDate, _VenueNo, _VenueBranchNo, _BranchNo, _StoreNo,
                    _ConsumptionNo, _UserNo, _PageIndex, _MenuType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetAllConsumptionList", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, request.BranchNo, request.userno);
            }
            return objresult;
        }

        public List<ConsumptionDetailsResponse> GetStockConsumptionDetails(GetConsumptionListRequest request)
        {
            List<ConsumptionDetailsResponse> objresult = new List<ConsumptionDetailsResponse>();
            List<ConsumptionDetailsInListResponse> objRslt = new List<ConsumptionDetailsInListResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", request.venueno);
                    var _ConsMasterNo = new SqlParameter("ConsMasterNo", request.masterNo);

                    objRslt = context.GetStockConsumptionDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_IV_GetStoreConsumptionProductDetails @VenueNo, @ConsMasterNo",
                    _VenueNo, _ConsMasterNo).ToList();

                    if (objRslt != null && objRslt.Count > 0)
                    {
                        var response = new ConsumptionDetailsResponse
                        {
                            RowNo = objRslt[0].RowNo,
                            BranchNo = objRslt[0].BranchNo,
                            BranchName = objRslt[0].BranchName,
                            StoreNo = objRslt[0].StoreNo,
                            StoreName = objRslt[0].StoreName,
                            prdConsumptionLst = JsonConvert.DeserializeObject<List<GetConsumptionProductListResponse>>(objRslt[0].prdConsumptionLst)
                        };

                        objresult.Add(response);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionRepositoty.GetStockConsumptionDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, request.venuebranchno, request.userno);
            }
            return objresult;
        }
    }
}