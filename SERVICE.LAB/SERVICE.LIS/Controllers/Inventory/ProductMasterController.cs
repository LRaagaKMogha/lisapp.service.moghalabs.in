using System;
using System.Collections.Generic;
using Service.IRepository.Inventory;
using Service.Common;
using Service.Model;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ProductMasterController : ControllerBase
    {
        private readonly IProductMasterRepository _ProductMasterRepository;
        public ProductMasterController(IProductMasterRepository noteRepository)
        {
            _ProductMasterRepository = noteRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/GetProductMasterDetails")]
        public List<GetProductMasterResponse> GetProductMasters(GetCommonMasterRequest masterRequest)
        {
            List<GetProductMasterResponse> Objresult = new List<GetProductMasterResponse>();
            try
            {             
                Objresult = _ProductMasterRepository.GetProductMasters(masterRequest);                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetProductMasterDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, (int)masterRequest.venuebranchno, 0);
            }
            return Objresult;
        }
        
        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertProductMasterDetails")]        
        public CommonAdminResponse InsertProductMasterDetails(postProductMasterDTO postProductMasterDTO)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _ProductMasterRepository.InsertProductMasterDetails(postProductMasterDTO);
                var productMaster = postProductMasterDTO?.tblproductMaster;
                string _CacheKey = CacheKeys.CommonMaster + "PRODUCTMASTER" + productMaster?.VenueNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.InsertProductMasterDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, postProductMasterDTO.tblproductMaster.VenueNo, postProductMasterDTO.tblproductMaster.VenueBranchNo, postProductMasterDTO.userNo);
            }
            return result;
        }
     
        [CustomAuthorize("INVMASTERS")]
        [HttpGet]
        [Route("api/ProductMaster/GetSupplierMapping")]
        public List<GetSupplierMappingDTO> GetSupplierMapping(int ProductNo, int VenueNo, int VenueBranchNo)
        {
            List<GetSupplierMappingDTO> Objresult = new List<GetSupplierMappingDTO>();
            try
            {
                Objresult = _ProductMasterRepository.GetSupplierMapping(ProductNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetSubCustomerDetailbyCustomer-" + ProductNo, ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpGet]
        [Route("api/ProductMaster/GetDepartmentMapping")]
        public List<GetDepartmentMappingDTO> GetDepartmentMapping(int ProductNo, int VenueNo, int VenueBranchNo)
        {
            List<GetDepartmentMappingDTO> Objresult = new List<GetDepartmentMappingDTO>();
            try
            {
                Objresult = _ProductMasterRepository.GetDepartmentMapping(ProductNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetDepartmentMapping-ProductNo" + ProductNo, ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/GetIndentDetails")]
        public List<IndentDetailsResponse> GetIndentDetails(GetIndentDetailsRequest indent)
        {
            List<IndentDetailsResponse> Objresult = new List<IndentDetailsResponse>();
            try
            {
                Objresult = _ProductMasterRepository.GetIndentDetails(indent);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetIndentDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, indent.venueNo, indent.venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/GetIndentProductDetails")]
        public List<IndentProductDetailsNewResponse> GetIndentProductDetails(GetIndentDetailsRequest indent)
        {
            List<IndentProductDetailsNewResponse> Objresult = new List<IndentProductDetailsNewResponse>();
            try
            {
                Objresult = _ProductMasterRepository.GetIndentProductDetails(indent);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetIndentProductDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, indent.venueNo, indent.venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertIndentDetails")]
        public IndentDetailsSaveResponse InsertIndentDetails(IndentDetailsSaveRequest indent)
        {
            IndentDetailsSaveResponse Objresult = new IndentDetailsSaveResponse();
            try
            {
                Objresult = _ProductMasterRepository.InsertIndentDetails(indent);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.InsertIndentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, indent.venueNo, indent.venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/GetIssueProductlst")]
        public List<GetIssueProductResponse> GetIssueProductlst(GetIssueProductRequest issue)
        {
            List<GetIssueProductResponse> Objresult = new List<GetIssueProductResponse>();
            try
            {
                Objresult = _ProductMasterRepository.GetIssueProductlst(issue);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetIssueProductlst-", ExceptionPriority.Low, ApplicationType.APPSERVICE, issue.venueNo, issue.venueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/GetIssuedProductsByIssueNo")]
        public List<GetIssueProductByIssueNoResponse> GetIssuedProductsByIssueNo(GetIssueProductRequest issue)
        {
            List<GetIssueProductByIssueNoResponse> Objresult = new List<GetIssueProductByIssueNoResponse>();
            try
            {
                Objresult = _ProductMasterRepository.GetIssuedProductsByIssueNo(issue);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetIssuedProductsByIssueNo-", ExceptionPriority.Low, ApplicationType.APPSERVICE, issue.venueNo, issue.venueBranchNo, 0);
            }
            return Objresult;
        }


        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertIssueProductlst")]
        public SaveIssueProductResponse InsertIssueProductlst(IssueProductRequest issue)
        {
            SaveIssueProductResponse Objresult = new SaveIssueProductResponse();
            try
            {
                Objresult = _ProductMasterRepository.InsertIssueProductlst(issue);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertIssueProductlst-", ExceptionPriority.Low, ApplicationType.APPSERVICE, issue.VenueNo, issue.VenueBranchNo, 0);
            }
            return Objresult;
        }


        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertIssueReceivedProductlst")]
        public SaveIssueProductResponse InsertIssueReceivedProductlst(IssueProductRequest issue)
        {
            SaveIssueProductResponse Objresult = new SaveIssueProductResponse();
            try
            {
                Objresult = _ProductMasterRepository.InsertIssueReceivedProductlst(issue);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertIssueReceivedProductlst-", ExceptionPriority.Low, ApplicationType.APPSERVICE, issue.VenueNo, issue.VenueBranchNo, 0);
            }
            return Objresult;
        }


        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/FetchProductDetails")]
        public List<FetchProductListResponse> FetchProductList(ProductMasterRequest productList)
        {
            List<FetchProductListResponse> Objresult = new List<FetchProductListResponse>();
            try
            {
                Objresult = _ProductMasterRepository.FetchProductList(productList);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.FetchProductList-", ExceptionPriority.Low, ApplicationType.APPSERVICE, productList.venueNo, productList.venueBranchNo, productList.userNo);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/Getlookalike")]
        public List<Fetchlookalike> Getlookalike(Getlookalikeresponse obj)
        {
            List<Fetchlookalike> Objresult = new List<Fetchlookalike>();
            try
            {
                Objresult = _ProductMasterRepository.Getlookalike(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.Getlookalike", ExceptionPriority.Low, ApplicationType.APPSERVICE,obj.VenueNo,0, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/GetSoundalike")]
        public List<Fetchsoundalike> GetSoundalike(GetSoundalikeresponse obj)
        {
            List<Fetchsoundalike> Objresult = new List<Fetchsoundalike>();
            try
            {
                Objresult = _ProductMasterRepository.GetSoundalike(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetSoundalike", ExceptionPriority.Low, ApplicationType.APPSERVICE, obj.VenueNo, 0, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/ProductMaster/GetDeptIssueProductlst")]
        public List<GetDeptIssueProductResponse> GetDeptIssueProductlst(GetDeptIssueProductRequest issue)
        {
            List<GetDeptIssueProductResponse> lstresult = new List<GetDeptIssueProductResponse>();
            try
            {
                lstresult = _ProductMasterRepository.GetDeptIssueProductlst(issue);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetDeptIssueProductlst", ExceptionPriority.Low, ApplicationType.APPSERVICE, issue.venueNo, 0, 0);
            }
            return lstresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/GetSubProduct")]
        public List<SubProductRes> GetSubProduct(SubProductReq obj)
        {
            List<SubProductRes> Objresult = new List<SubProductRes>();
            try
            {
                Objresult = _ProductMasterRepository.GetSubProduct(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetSubProduct", ExceptionPriority.Low, ApplicationType.APPSERVICE, obj.VenueNo, obj.subProductNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/GetProdVsDrug")]
        public List<LstDrugresponse> GetProdVsDrug(LstDrugreq obj)
        {
            List<LstDrugresponse> Objresult = new List<LstDrugresponse>();
            try
            {
                Objresult = _ProductMasterRepository.GetProdVsDrug(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetProdVsDrug", ExceptionPriority.Low, ApplicationType.APPSERVICE, obj.venueNo, obj.productNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertProdVsDrugs")]
        public int InsertProdVsDrugs(Savedruglstreq creq1)
        {
            int drugPresTempNo = 0;
            try
            {
                drugPresTempNo = _ProductMasterRepository.InsertProdVsDrugs(creq1);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.InsertProdVsDrugs" + creq1.productNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq1.venueno, creq1.userNo, 0);
            }
            return drugPresTempNo;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpGet]
        [Route("api/ProductMaster/GetProductUnitList")]
        public List<ProductUnitDTO> GetProductUnitList(int VenueNo)
        {
            List<ProductUnitDTO> Objresult = new List<ProductUnitDTO>();
            try
            {
                Objresult = _ProductMasterRepository.GetProductUnitList(VenueNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetProductUnitList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpGet]
        [Route("api/ProductMaster/GetBOMMapping")]
        public List<BOMMappingDTO> GetBOMMapping(int VenueNo, int TestNo, string TestType)
        {
            List<BOMMappingDTO> Objresult = new List<BOMMappingDTO>();
            try
            {
                Objresult = _ProductMasterRepository.GetBOMMapping(VenueNo, TestNo, TestType);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.GetBOMMapping", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ProductMaster/InsertBOMMapping")]
        public BOMMappingResponse InsertBOMMapping(List<BOMMappingRequest> req)
        {
            BOMMappingResponse Objresult = new BOMMappingResponse();
            try
            {
                Objresult = _ProductMasterRepository.InsertBOMMapping(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterController.InsertBOMMapping", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }
    }
}