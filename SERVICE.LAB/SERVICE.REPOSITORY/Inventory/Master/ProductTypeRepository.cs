using System;
using System.Collections.Generic;
using System.Text;
using Service.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Service.Common;
using Microsoft.Data.SqlClient;

namespace Service.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private IConfiguration _config;
        public ProductTypeRepository(IConfiguration config) { _config = config; }

        public List<TblProductType> Getproducttypemaster(ProductTypeMasterRequest protypRequest)
        {
            List<TblProductType> Objresult = new List<TblProductType>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _productTypeno = new SqlParameter("productTypeno", protypRequest?.productTypeno);
                    var _venueNo = new SqlParameter("venueNo", protypRequest?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", protypRequest?.pageIndex);

                    Objresult = context.Getproducttype.FromSqlRaw(
                    "Execute dbo.pro_GetproductType @productTypeno, @venueNo,@pageIndex",
                    _productTypeno, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeRepository.Getproducttypemaster" + protypRequest.productTypeno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, protypRequest.venueNo, protypRequest.venueBranchno, 0);
            }
            return Objresult;
        }

        public ProductTypeMasterResponse Insertproducttypemaster(TblProductType tblProtyp)
        {
            ProductTypeMasterResponse Objresult = new ProductTypeMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _productTypeno = new SqlParameter("productTypeno", tblProtyp?.productTypeno);
                    var _productTypename = new SqlParameter("productTypename", tblProtyp?.productTypename);           
                    var _sequenceNo = new SqlParameter("sequenceNo", tblProtyp?.sequenceNo);
                    var _status = new SqlParameter("status", tblProtyp?.status);
                    var _venueNo = new SqlParameter("venueNo", tblProtyp?.venueNo);
                    var _userNo = new SqlParameter("userNo", tblProtyp?.userNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblProtyp?.venueBranchno);

                    var obj = context.Insertproducttype.FromSqlRaw(
                    "Execute dbo.pro_InsertProductType @productTypeno,@productTypename,@sequenceNo," +
                    "@status,@venueNo,@userNo,@VenueBranchNo",
                    _productTypeno, _productTypename, _sequenceNo, _status,_venueNo, _userNo, _venueBranchno).ToList();

                    Objresult.productTypeno = obj[0].productTypeno;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeRepository.Insertproducttypemaster" + tblProtyp.productTypeno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblProtyp.venueNo, tblProtyp.venueBranchno, tblProtyp.userNo);
            }
            return Objresult;
        }
        public List<TblProductCategory> GetProductCategory(ProductcategoryRequest ProductcategoryRequest)
        {
            List<TblProductCategory> Objresult = new List<TblProductCategory>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _categoryNo = new SqlParameter("categoryNo", ProductcategoryRequest?.categoryNo);
                    var _venueNo = new SqlParameter("venueNo", ProductcategoryRequest?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", ProductcategoryRequest?.pageIndex);

                    Objresult = context.GetProductCategory.FromSqlRaw(
                    "Execute dbo.pro_GetProductCategory @categoryNo,@venueNo,@pageIndex",
                    _categoryNo, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeRepository.GetProductCategory" + ProductcategoryRequest.categoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, ProductcategoryRequest.venueNo,0, 0);
            }
            return Objresult;
        }
        public ProductcategoryResponse InsertproductCategory(TblProductCategory TblProductCategory)
        {
            ProductcategoryResponse Objresult = new ProductcategoryResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _categoryNo = new SqlParameter("categoryNo", TblProductCategory?.categoryNo);
                    var _categoryCode = new SqlParameter("categoryCode", TblProductCategory?.categoryCode);
                    var _categoryName = new SqlParameter("categoryName", TblProductCategory?.categoryName);
                    var _venueNo = new SqlParameter("venueNo", TblProductCategory?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", TblProductCategory?.venueBranchno);
                    var _categorystatus = new SqlParameter("categorystatus", TblProductCategory?.categorystatus);
                    var _userNo = new SqlParameter("userNo", TblProductCategory?.userNo);                   

                    var obj = context.InsertproductCategory.FromSqlRaw(
                    "Execute dbo.pro_InsertProductCategory @categoryNo,@categoryCode,@categoryName,@venueNo,@venueBranchNo,@categorystatus,@userNo",
                    _categoryNo,_categoryCode,_categoryName,_venueNo,_venueBranchno,_categorystatus, _userNo).ToList();

                    Objresult.categoryNo = obj[0].categoryNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeRepository.InsertproductCategory" + TblProductCategory.categoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, TblProductCategory.venueNo, TblProductCategory.venueBranchno, TblProductCategory.userNo);
            }
            return Objresult;
        }
    }
}