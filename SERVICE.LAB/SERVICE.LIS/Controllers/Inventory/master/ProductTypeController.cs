using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEV.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _producttypeRepository;
        public ProductTypeController(IProductTypeRepository noteRepository )
        {
            _producttypeRepository = noteRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Producttype/Getproducttypemaster")]
        public IEnumerable<TblProductType> Getproducttypemaster(ProductTypeMasterRequest protypRequest)
        {
             List<TblProductType> result = new List<TblProductType>();
            try
            {               
                result= _producttypeRepository.Getproducttypemaster(protypRequest);             
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeController.Getproducttypemaster" + protypRequest.productTypeno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, protypRequest.venueNo, protypRequest.venueBranchno, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Producttype/Insertproducttypemaster")]
        public ProductTypeMasterResponse Insertproducttypemaster(TblProductType tblProtyp)
        {
            ProductTypeMasterResponse objresult = new ProductTypeMasterResponse();
            try
            {
                objresult = _producttypeRepository.Insertproducttypemaster(tblProtyp);
                string _CacheKey = CacheKeys.CommonMaster + "PRODUCTTYPE" + tblProtyp.venueNo + tblProtyp.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeController.Insertproducttypemaster" + tblProtyp.productTypeno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tblProtyp.venueNo, tblProtyp.venueBranchno, tblProtyp.userNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Producttype/GetProductCategory")]
        public IEnumerable<TblProductCategory> GetProductCategory(ProductcategoryRequest ProductcategoryRequest)
        {
            List<TblProductCategory> result = new List<TblProductCategory>();
            try
            {
                result = _producttypeRepository.GetProductCategory(ProductcategoryRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeController.GetProductCategory" + ProductcategoryRequest.categoryNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, ProductcategoryRequest.venueNo,0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Producttype/InsertproductCategory")]
        public ProductcategoryResponse InsertproductCategory(TblProductCategory TblProductCategory)
        {
            ProductcategoryResponse objresult = new ProductcategoryResponse();
            try
            {
                objresult = _producttypeRepository.InsertproductCategory(TblProductCategory);
                string _CacheKey = CacheKeys.CommonMaster + "PRODUCTCATEGORY" + TblProductCategory.venueNo + TblProductCategory.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductTypeController.InsertproductCategory" + TblProductCategory.categoryNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, TblProductCategory.venueNo, TblProductCategory.venueBranchno,0);
            }
            return objresult;
        }
    }
}