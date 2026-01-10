using DEV.Model;
using DEV.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IProductMasterRepository
    {
        List<GetProductMasterResponse> GetProductMasters(GetCommonMasterRequest masterRequest);
        List<GetSupplierMappingDTO> GetSupplierMapping(int ProductNo, int VenueNo, int VenueBranchNo);
        CommonAdminResponse InsertProductMasterDetails(postProductMasterDTO productMasterDTO);
        List<GetDepartmentMappingDTO> GetDepartmentMapping(int ProductNo, int VenueNo, int VenueBranchNo);
        List<IndentDetailsResponse> GetIndentDetails(GetIndentDetailsRequest indent);
        List<IndentProductDetailsNewResponse> GetIndentProductDetails(GetIndentDetailsRequest indent);
        IndentDetailsSaveResponse InsertIndentDetails(IndentDetailsSaveRequest indent);
        List<GetIssueProductResponse> GetIssueProductlst(GetIssueProductRequest issue);
        List<GetIssueProductByIssueNoResponse> GetIssuedProductsByIssueNo(GetIssueProductRequest issue);
        SaveIssueProductResponse InsertIssueProductlst(IssueProductRequest issue);
        SaveIssueProductResponse InsertIssueReceivedProductlst(IssueProductRequest issue);
        List<FetchProductListResponse> FetchProductList(ProductMasterRequest masterRequest);
        List<Fetchlookalike> Getlookalike(Getlookalikeresponse obj);

        List<Fetchsoundalike> GetSoundalike(GetSoundalikeresponse obj);
        List<GetDeptIssueProductResponse> GetDeptIssueProductlst(GetDeptIssueProductRequest issue);
        List<SubProductRes> GetSubProduct(SubProductReq obj); 
        List<lstdrugresponse> GetProdVsDrug(lstdrugreq obj); 
        int InsertProdVsDrugs(savedruglstreq creq1);
        List<ProductUnitDTO> GetProductUnitList(int VenueNo);
        List<BOMMappingDTO> GetBOMMapping(int VenueNo, int TestNo, string TestType);
        BOMMappingResponse InsertBOMMapping(List<BOMMappingRequest> req);

    }
}