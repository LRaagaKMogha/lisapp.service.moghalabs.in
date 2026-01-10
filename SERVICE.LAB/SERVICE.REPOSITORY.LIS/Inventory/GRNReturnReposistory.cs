using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Inventory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Repository.Inventory
{
    public class GRNReturnReposistory: IGRNReturnReposistory
    {
        private IConfiguration _config;
        public GRNReturnReposistory(IConfiguration config) { _config = config; }

        public List<GetAllGRNReturnResponse> GetAllGRNReturn(GetAllGRNReturnRequest masterRequest)
        {
            List<GetAllGRNReturnResponse> objresult = new List<GetAllGRNReturnResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _fromDate = new SqlParameter("FromDate", masterRequest?.FromDate);
                    var _toDate = new SqlParameter("ToDate", masterRequest?.ToDate);
                    var _type = new SqlParameter("Type", masterRequest?.Type);
                    var _venueNo = new SqlParameter("VenueNo", masterRequest.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", masterRequest.venuebranchno);
                    var _grnRtnNo = new SqlParameter("GRNReturnNo", masterRequest.masterNo);
                    var _userNo = new SqlParameter("UserNo", masterRequest.userno);
                    var _pageIndex = new SqlParameter("PageIndex", masterRequest.pageIndex);
                    var _supplierNo = new SqlParameter("SupplierNo", masterRequest.SupplierNo);
                    var _MenuType = new SqlParameter("MenuType", masterRequest?.MenuType);

                    objresult = context.GetAllGRNReturnDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllGRNReturn @Type, @FromDate, @ToDate, @VenueNo, @VenueBranchNo, @GRNReturnNo, @SupplierNo, @UserNo, @PageIndex, @MenuType",
                    _type, _fromDate, _toDate, _venueNo, _venueBranchNo, _grnRtnNo, _supplierNo, _userNo, _pageIndex, _MenuType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.GetAllGRNReturn", ExceptionPriority.High, ApplicationType.REPOSITORY, masterRequest.venueno, (int)masterRequest.venuebranchno, (int)masterRequest.masterNo);
            }
            return objresult;
        }

        public List<GetGRNBySupplierResponse> GetGRNBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo)
        {
            List<GetGRNBySupplierResponse> objresult = new List<GetGRNBySupplierResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _SupplierNo = new SqlParameter("SupplierNo", supplierNo);   
                    
                    objresult = context.GetGRNBySupplierDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetGRNBySupplier @VenueNo, @VenueBranchNo, @SupplierNo",
                    _VenueNo, _VenueBranchNo, _SupplierNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.GetGRNBySupplierDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, supplierNo);
            }
            return objresult;
        }

        public List<GetProductsByGRNResponse> GetProductByGRN(int venueNo, int venueBranchNo, int grnNo)
        {
            List<GetProductsByGRNResponse> objresult = new List<GetProductsByGRNResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _grnNo = new SqlParameter("GRNNo", grnNo);

                    objresult = context.GetProductByGRNDTO.FromSqlRaw(
                    "Execute dbo.pro_IV_GetProductsByGRN @VenueNo, @VenueBranchNo, @GRNNo",
                    _VenueNo, _VenueBranchNo, _grnNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.GetProductByGRN", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, Convert.ToInt16(grnNo));
            }
            return objresult;
        }

        public List<GetProductsByGRNNo> GetGRNReturnProduct(int venueNo, int venueBranchNo, int grnRtnNo)
        {
            List<GetProductsByGRNNo> objresult = new List<GetProductsByGRNNo>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _grnRtnNo = new SqlParameter("GRNRtnNo", grnRtnNo);

                    objresult = context.GetGRNReturnProductDTO.FromSqlRaw(
                    "Execute dbo.pro_IV_GetGRNReturnProduct @VenueNo, @VenueBranchNo, @GRNRtnNo",
                    _VenueNo, _VenueBranchNo, _grnRtnNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.GetGRNReturnProduct", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, Convert.ToInt16(grnRtnNo));
            }
            return objresult;
        }

        public CommonAdminResponse InsertGRNReturn(PostGRN insertGRNReturn)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var GRNReturnXML = commonUtility.ToXML(insertGRNReturn);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertGRNReturn?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertGRNReturn?.VenueBranchNo);
                    var _GRNReturnXML = new SqlParameter("GRNReturnXML", GRNReturnXML);
                    var _UserNo = new SqlParameter("UserNo", insertGRNReturn?.Createdby);
                    var _MenuType = new SqlParameter("MenuType", insertGRNReturn?.MenuType);                    

                    var objresult = context.CreateGRNReturnDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertGRNReturn @VenueNo, @VenueBranchNo, @GRNReturnXML, @UserNo, @MenuType",
                    _VenueNo, _VenueBranchNo, _GRNReturnXML, _UserNo, _MenuType).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.InsertGRNReturn", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertGRNReturn.VenueNo, insertGRNReturn.VenueBranchNo, insertGRNReturn.Createdby);
            }
            return response;
        }

        public List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int GRNReturnNo)
        {
            List<otherChargeModal> objresult = new List<otherChargeModal>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", venueBranchNo);
                    var _GRNReturnNo = new SqlParameter("GRNReturnNo", GRNReturnNo);

                    objresult = context.GetGRNOCDetailsDTO.FromSqlRaw(
                    "Execute dbo.pro_GetGRNOCDetailsById @VenueNo,@VenueBranchNo,@GRNReturnNo",
                    _VenueNo, _VenueBranchNo, _GRNReturnNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GRNReturnReposistory.GetGRNOCDetailsById", ExceptionPriority.High, ApplicationType.REPOSITORY, venueNo, venueBranchNo, GRNReturnNo);
            }
            return objresult;
        }
    }    
}
