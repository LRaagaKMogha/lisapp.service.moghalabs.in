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
    public class PurchaseOrderReposistory: IPurchaseOrderReposistory
    {
        private IConfiguration _config;
        public PurchaseOrderReposistory(IConfiguration config) { _config = config; }


        public List<GetPurchaseOrderResponse> GetPurchaseOrders(GetAllPORequest masterRequest)
        {
            List<GetPurchaseOrderResponse> objresult = new List<GetPurchaseOrderResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ProductNo = new SqlParameter("PurchaseOrderNo", masterRequest?.masterNo);
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest?.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", masterRequest?.venuebranchno);
                    var _UserNo = new SqlParameter("UserNo", masterRequest?.userno);
                    var _PageIndex = new SqlParameter("PageIndex", masterRequest?.pageIndex);
                    var _FromDate = new SqlParameter("FromDate", masterRequest?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", masterRequest?.ToDate);
                    var _Type = new SqlParameter("Type", masterRequest?.Type);
                    var _SupplierNo = new SqlParameter("SupplierNo", masterRequest?.SupplierNo);
                    var _MenuType = new SqlParameter("MenuType", masterRequest?.MenuType);

                    objresult = context.GetPurchaseOrderDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetPurchaseOrder @PurchaseOrderNo,@VenueNo,@VenueBranchNo,@UserNo,@PageIndex,@FromDate,@ToDate,@Type,@SupplierNo,@MenuType",
                    _ProductNo, _VenueNo, _VenueBranchNo, _UserNo, _PageIndex, _FromDate, _ToDate, _Type, _SupplierNo, _MenuType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetPurchaseOrders", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueno, (int)masterRequest.venuebranchno, 0);
            }
            return objresult;
        }

        public List<GetSupplierServiceDTO> GetSupplierServiceDetails(int venueNo, int venueBranchNo, int supplierNo, int StoreNo, string type)
        {
            List<GetSupplierServiceDTO> objresult = new List<GetSupplierServiceDTO>();
            try
            {               
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _SupplierNo = new SqlParameter("SupplierNo", supplierNo);
                    var _StoreNo = new SqlParameter("StoreNo", StoreNo);
                    var _type = new SqlParameter("type", type);
                        
                    objresult = context.GetSupplierServiceDTO.FromSqlRaw(
                    "Execute dbo.pro_GetSupplierServiceDetails @VenueNo, @VenueBranchNo, @SupplierNo, @StoreNo, @type",
                    _VenueNo, _VenueBranchNo, _SupplierNo, _StoreNo, _type).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetSupplierServiceDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, supplierNo);
            }
            return objresult;
        }

        public CommonAdminResponse InsertPurchaseOrder(InsertPurchaseOrder insertPurchaseOrder)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var purchaseOrderXML = commonUtility.ToXML(insertPurchaseOrder);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertPurchaseOrder?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertPurchaseOrder?.venueBranchNo);
                    var _PurchaseOrderNo = new SqlParameter("PurchaseOrderNo", insertPurchaseOrder?.purchaseOrderMasterNo);
                    var _PurchaseOrderXML = new SqlParameter("PurchaseOrderXML", purchaseOrderXML);
                    var _UserNo = new SqlParameter("UserNo", insertPurchaseOrder?.createdby);
                    var _MenuCode = new SqlParameter("MenuCode", insertPurchaseOrder?.menuCode);

                    var objresult = context.CreatePurchaseOrderDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertPurchaseOrder @VenueNo,@VenueBranchNo,@PurchaseOrderNo,@PurchaseOrderXML,@UserNo,@MenuCode",
                    _VenueNo, _VenueBranchNo, _PurchaseOrderNo, _PurchaseOrderXML, _UserNo, _MenuCode).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.InsertPurchaseOrder", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertPurchaseOrder.venueNo, insertPurchaseOrder.venueBranchNo, insertPurchaseOrder.createdby);
            }
            return response;
        }

        public List<GetPurchaseDetailsDTO> GetPurchaseDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<GetPurchaseDetailsDTO> objresult = new List<GetPurchaseDetailsDTO>();
            try
            {               
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PurchaseNo = new SqlParameter("PurchaseNo", PurchaseNo);
                    
                    objresult = context.GetPurchaseDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPurchaseDetailsById @VenueNo,@VenueBranchNo,@PurchaseNo",
                    _VenueNo, _VenueBranchNo, _PurchaseNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetPurchaseDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }

        public List<POProductDetailsDTO> GetPOProductDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<POProductDetailsDTO> objresult = new List<POProductDetailsDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PurchaseNo = new SqlParameter("PurchaseNo", PurchaseNo);

                    objresult = context.GetPOProductDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPOProductDetailsById @VenueNo, @VenueBranchNo, @PurchaseNo",
                    _VenueNo, _VenueBranchNo, _PurchaseNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetPOProductDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }

        public List<GetTaxDatilsResponse> GetPOTaxDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<GetTaxDatilsResponse> objresult = new List<GetTaxDatilsResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PurchaseNo = new SqlParameter("PurchaseNo", PurchaseNo);

                    objresult = context.GetPOTaxDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPOTaxDetailsById @VenueNo,@VenueBranchNo,@PurchaseNo",
                    _VenueNo, _VenueBranchNo, _PurchaseNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetPOTaxDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }

        public List<otherChargeModal> GetPOOCDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PurchaseNo = new SqlParameter("PurchaseNo", PurchaseNo);

                    objresult = context.GetPOOCDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPOOCDetailsById @VenueNo,@VenueBranchNo,@PurchaseNo",
                    _VenueNo, _VenueBranchNo, _PurchaseNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.GetPOOCDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }

        public List<Termsconditionlist> GetPOTermsDetailsById(int venueNo, int venueBranchNo, int PurchaseNo)
        {
            List<Termsconditionlist> objresult = new List<Termsconditionlist>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _PurchaseNo = new SqlParameter("PurchaseNo", PurchaseNo);
                    
                    objresult = context.GetPOTermsDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPOTermsDetailsById @VenueNo,@VenueBranchNo,@PurchaseNo",
                    _VenueNo, _VenueBranchNo, _PurchaseNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PurchaseOrderReposistory.pro_GetPOOCDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, PurchaseNo);
            }
            return objresult;
        }
    }
}
