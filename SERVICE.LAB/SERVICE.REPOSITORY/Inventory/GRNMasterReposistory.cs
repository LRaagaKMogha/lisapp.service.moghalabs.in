using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Dev.Repository.Inventory
{
    public class GRNMasterReposistory: IGRNMasterReposistory
    {
        private IConfiguration _config;
        public GRNMasterReposistory(IConfiguration config) { _config = config; }

        public List<GetAllGRNResponse> GetAllGRN(GetAllGRNRequest masterRequest)
        {
            List<GetAllGRNResponse> objresult = new List<GetAllGRNResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", masterRequest?.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", masterRequest?.venuebranchno);
                    var _grnNo = new SqlParameter("GrnNo", masterRequest?.masterNo);
                    var _UserNo = new SqlParameter("UserNo", masterRequest?.userno);
                    var _PageIndex = new SqlParameter("PageIndex", masterRequest?.pageIndex);
                    var _FromDate = new SqlParameter("FromDate", masterRequest?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", masterRequest?.ToDate);
                    var _Type = new SqlParameter("Type", masterRequest?.Type);
                    var _SupplierNo = new SqlParameter("SupplierNo", masterRequest?.SupplierNo);
                    var _MenuType = new SqlParameter("MenuType", masterRequest?.MenuType);
                    var _GRNStatus = new SqlParameter("GRNStatus", masterRequest?.GRNStatus);
                    var _InvoiceNo = new SqlParameter("InvoiceNo", masterRequest?.InvoiceNo);

                    objresult = context.GetAllGRNDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllGRN @VenueNo, @VenueBranchNo, @GRNNo, @UserNo, @PageIndex, @FromDate, @ToDate, @Type, @SupplierNo, @MenuType, @GRNStatus, @InvoiceNo",
                    _venueNo, _venueBranchNo, _grnNo,_UserNo,_PageIndex, _FromDate, _ToDate, _Type, _SupplierNo, _MenuType, _GRNStatus, _InvoiceNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.GetGRNList", ExceptionPriority.High, ApplicationType.REPOSITORY, masterRequest.venueno, (int)masterRequest.venuebranchno, (int)masterRequest.masterNo);
            }
            return objresult;
        }
        public List<GetPOBySupplierResponse> GetPOBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo)
        {
            List<GetPOBySupplierResponse> objresult = new List<GetPOBySupplierResponse>();
            try
            {              
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _SupplierNo = new SqlParameter("SupplierNo", supplierNo);
                    
                    objresult = context.GetPOBySupplierDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPOBySupplier @VenueNo, @VenueBranchNo, @SupplierNo",
                    _VenueNo, _VenueBranchNo, _SupplierNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.GetPOBySupplierDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, supplierNo);
            }
            return objresult;
        }
        public List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber)
        {
            List<GetProductsByPOResponse> objresult = new List<GetProductsByPOResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _poNumber = new SqlParameter("PONo", poNumber);

                    objresult = context.GetProductByPODTO.FromSqlRaw(
                    "Execute dbo.pro_IV_GetProductsByPO @VenueNo, @VenueBranchNo, @PONo",
                    _VenueNo, _VenueBranchNo, _poNumber).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.GetProductByPO", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, poNumber);
            }
            return objresult;
        }
        public CommonAdminResponse InsertGRNMaster(InsertGRNMasterRequest insertGRNMaster)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var grnMasterXML = commonUtility.ToXML(insertGRNMaster);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertGRNMaster?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertGRNMaster?.venueBranchNo);
                    var _grnMasterXML = new SqlParameter("GRNMasterXML", grnMasterXML);
                    var _UserNo = new SqlParameter("UserNo", insertGRNMaster?.createdby);
                    var _MenuCode = new SqlParameter("MenuCode", insertGRNMaster?.MenuType);

                    var objresult = context.CreateGRNMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertGRNMaster @VenueNo, @VenueBranchNo, @GRNMasterXML, @UserNo, @MenuCode",
                    _VenueNo, _VenueBranchNo, _grnMasterXML, _UserNo, _MenuCode).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.InsertGRNMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertGRNMaster.venueNo, insertGRNMaster.venueBranchNo, insertGRNMaster.createdby);
            }
            return response;
        }

        public List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int grnMasterNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _GRNMasterNo = new SqlParameter("GRNMasterNo", grnMasterNo);

                    objresult = context.GetGRNOCDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetGRNOCDetailsById @VenueNo, @VenueBranchNo, @GRNMasterNo",
                    _VenueNo, _VenueBranchNo, _GRNMasterNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.GetGRNOCDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, grnMasterNo);
            }
            return objresult;
        }
        public List<GetProductsByPOResponse> GetGRNProductDetails(int venueNo, int venueBranchNo, int grnMasterNo)
        {
            List<GetProductsByPOResponse> objresult = new List<GetProductsByPOResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _grnMasterNo = new SqlParameter("GRNNo", grnMasterNo);
                    
                    objresult = context.GetGRNDetailsByGRNDTO.FromSqlRaw(
                    "Execute dbo.pro_GetGRNByGRNNo @VenueNo,@VenueBranchNo,@GRNNo",
                    _VenueNo, _VenueBranchNo, _grnMasterNo).ToList();
                }
                try
                {
                    using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                    {
                        var _VenueNo = new SqlParameter("VenueNo", venueNo);
                        var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                        var _grnMasterNo = new SqlParameter("GRNNo", grnMasterNo);

                        var response = context.GetProGRNBatchDetails.FromSqlRaw(
                        "Execute dbo.pro_GetProGRNBatchDetails @VenueNo, @VenueBranchNo, @GRNNo",
                        _VenueNo, _VenueBranchNo, _grnMasterNo).ToList();

                        if (response != null)
                        {
                            objresult.ForEach(product =>
                            {
                                product.Batchdetails = response
                                    .Where(batch => batch.GRNProductNo == product.GRNProductNo && batch.productNo == product.ProductNo)
                                    .ToList();
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyDevException.Error(ex, "GRNMasterReposistory.GetGRNProductBatchDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, grnMasterNo);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.GetGRNProductDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, grnMasterNo);
            }
            return objresult;
        }

        public CommonAdminResponse UpdateInvoiceDetails(InvoiceUpdateRequest req)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _BranchNo = new SqlParameter("BranchNo", req?.BranchNo);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _GrnNo = new SqlParameter("GrnNo", req?.GrnNo);
                    var _InvoiceDate = new SqlParameter("InvoiceDate", req?.InvoiceDate);
                    var _InvoiceNo = new SqlParameter("InvoiceNo", req?.InvoiceNo);

                    var objresult = context.UpdateGRNInvoiceDTO.FromSqlRaw(
                    "Execute dbo.Pro_IV_UpdateGRNInvoiceDetails @VenueNo, @BranchNo, @UserNo, @GrnNo, @InvoiceDate, @InvoiceNo",
                    _VenueNo, _BranchNo, _UserNo, _GrnNo, _InvoiceDate, _InvoiceNo).ToList();

                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNMasterReposistory.UpdateInvoiceDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.BranchNo, req.UserNo);
            }
            return response;
        }
    }
}
