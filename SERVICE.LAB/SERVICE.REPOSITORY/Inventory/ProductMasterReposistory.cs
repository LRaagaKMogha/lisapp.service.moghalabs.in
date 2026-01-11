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
using System.Text;

namespace Dev.Repository.Inventory
{
    public class ProductMasterReposistory : IProductMasterRepository
    {
        private IConfiguration _config;
        public ProductMasterReposistory(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get ProductMaster Details
        /// </summary>
        /// <returns></returns>
        public List<GetProductMasterResponse> GetProductMasters(GetCommonMasterRequest masterRequest)
        {
            List<GetProductMasterResponse> objresult = new List<GetProductMasterResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ProductNo = new SqlParameter("ProductNo", masterRequest?.masterNo);
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest?.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", masterRequest?.venuebranchno);
                    var _UserNo = new SqlParameter("UserNo", masterRequest?.userno);
                    var _PageIndex = new SqlParameter("PageIndex", masterRequest?.pageIndex);
                    var _ManufacturerNo = new SqlParameter("ManufacturerNo", masterRequest?.ManufacturerNo);
                    var _ProductCategoryNo = new SqlParameter("ProductCategoryNo", masterRequest?.ProductCategoryNo);
                    var _ProductTypeNo = new SqlParameter("ProductTypeNo", masterRequest?.ProductTypeNo);

                    objresult = context.GetProductMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetProductMaster @ProductNo,@VenueNo,@VenueBranchNo,@UserNo,@PageIndex,@ManufacturerNo,@ProductCategoryNo,@ProductTypeNo",
                    _ProductNo, _VenueNo, _VenueBranchNo, _UserNo, _PageIndex, _ManufacturerNo, _ProductCategoryNo, _ProductTypeNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProductMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueno, (int)masterRequest.venuebranchno, 0);
            }
            return objresult;
        }

        /// <summary>
        /// GetSupplierMapping Details
        /// </summary>
        /// <returns></returns>
        public List<GetSupplierMappingDTO> GetSupplierMapping(int ProductNo , int VenueNo, int VenueBranchNo)
        {
            List<GetSupplierMappingDTO> objresult = new List<GetSupplierMappingDTO>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {                    
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _ProductNo = new SqlParameter("ProductNo",ProductNo);

                    objresult = context.GetSupplierMappingDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetSupplierMapping @VenueNo,@VenueBranchNo,@ProductNo",
                    _VenueNo, _VenueBranchNo, _ProductNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetSupplierMapping", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, ProductNo);
            }
            return objresult;
        }

        /// <summary>
        /// Get DepartmentMapping Details
        /// </summary>
        /// <returns></returns>
        public List<GetDepartmentMappingDTO> GetDepartmentMapping(int ProductNo, int VenueNo, int VenueBranchNo)
        {
            List<GetDepartmentMappingDTO> objresult = new List<GetDepartmentMappingDTO>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _ProductNo = new SqlParameter("ProductNo", ProductNo);

                    objresult = context.GetDepartmentMappingDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetDepartmentMapping @VenueNo, @VenueBranchNo, @ProductNo",
                    _VenueNo, _VenueBranchNo, _ProductNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetDepartmentMapping", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, ProductNo);
            }
            return objresult;
        }

        /// <summary>
        /// Insert ProductMaster Details
        /// </summary>
        /// <param name="productMaster"></param>
        /// <returns></returns>
        public CommonAdminResponse InsertProductMasterDetails(postProductMasterDTO productMasterDTO)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            var productMaster = productMasterDTO?.tblproductMaster;
            var supplierMapping = productMasterDTO?.supplierlist;
            CommonHelper commonUtility = new CommonHelper();
            var productMasterXML = commonUtility.ToXML(productMaster);
            var supplierMappingXML =  commonUtility.ToXML(supplierMapping);
            var departmentMappingXML = commonUtility.ToXML(productMasterDTO?.departmentlist);
            var lookalikeXML = commonUtility.ToXML(productMasterDTO?.lookalikelist);
            var soundalikeXML = commonUtility.ToXML(productMasterDTO?.soundalikelist);
            var subProductXML = commonUtility.ToXML(productMasterDTO?.subProduct);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", productMaster?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", productMaster?.VenueBranchNo);
                    var _ProductMasterXML = new SqlParameter("ProductMasterXML", productMasterXML);
                    var _SupplierMappingXML = new SqlParameter("SupplierMappingXML", supplierMappingXML);
                    var _DepartmentMappingXML = new SqlParameter("DepartmentMappingXML", departmentMappingXML);
                    var _lookalikeXML = new SqlParameter("lookalikeXML",lookalikeXML);
                    var _soundalikeXML = new SqlParameter("soundalikeXML",soundalikeXML);
                    var _UserNo = new SqlParameter("UserNo", productMasterDTO?.userNo);
                    var _subProductXML = new SqlParameter("subProductXML", subProductXML);

                    var objresult = context.CreateProductMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertProductMaster @VenueNo,@VenueBranchNo,@ProductMasterXML,@SupplierMappingXML,@DepartmentMappingXML,@lookalikeXML,@soundalikeXML,@UserNo,@subProductXML",
                    _VenueNo, _VenueBranchNo, _ProductMasterXML, _SupplierMappingXML, _DepartmentMappingXML, _lookalikeXML, _soundalikeXML,_UserNo, _subProductXML).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.InsertProductMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, productMaster.VenueNo, productMaster.VenueBranchNo, 0);
            }
            return response;
        }

        public List<IndentDetailsResponse> GetIndentDetails(GetIndentDetailsRequest indent)
        {
            List<IndentDetailsResponse> objresult = new List<IndentDetailsResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", indent.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", indent.venueBranchNo);
                    var _Indentno = new SqlParameter("Indentno", indent.indentno);
                    var _Status = new SqlParameter("Status", indent.status);
                    var _PageIndex = new SqlParameter("PageIndex", indent.pageIndex);
                    var _fromDate = new SqlParameter("FromDate", indent?.FromDate);
                    var _toDate = new SqlParameter("ToDate", indent?.ToDate);
                    var _type = new SqlParameter("Type", indent?.Type);
                    

                    objresult = context.GetIndentDetails.FromSqlRaw(
                    "Execute dbo.pro_GetIndentDetails @Type, @FromDate, @ToDate, @indentno, @VenueNo, @VenueBranchNo, @status, @pageIndex",
                    _type, _fromDate, _toDate, _Indentno, _VenueNo,_VenueBranchNo, _Status, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetIndentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, indent.venueNo, indent.venueBranchNo, 0);
            }
            return objresult;
        }
        public List<IndentProductDetailsNewResponse> GetIndentProductDetails(GetIndentDetailsRequest indent)
        {
            List<IndentProductDetailsNewResponse> objresult = new List<IndentProductDetailsNewResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", indent.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", indent.venueBranchNo);
                    var _Indentno = new SqlParameter("Indentno", indent.indentno);
                    var _Status = new SqlParameter("Status", indent.status);
                    var _PageIndex = new SqlParameter("PageIndex", indent.pageIndex);

                    objresult = context.GetIndentProductDetailsls.FromSqlRaw(
                    "Execute dbo.pro_GetIndentProductDetails @indentno, @VenueNo, @VenueBranchNo, @status, @pageIndex",
                    _Indentno, _VenueNo, _VenueBranchNo, _Status, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetIndentProductDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, indent.venueNo, indent.venueBranchNo, 0);
            }
            return objresult;
        }


        public List<GetIssueProductByIssueNoResponse> GetIssuedProductsByIssueNo(GetIssueProductRequest issue)
        {
            List<GetIssueProductByIssueNoResponse> objresult = new List<GetIssueProductByIssueNoResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", issue.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", issue.venueBranchNo);
                    //var _fromBranch = new SqlParameter("FromBranch", issue.fromBranch);
                    //var _fromStore = new SqlParameter("FromStore", issue.fromStore);
                    //var _toBranch = new SqlParameter("ToBranch", issue.toBranch);
                    //var _toStore = new SqlParameter("ToStore", issue.toStore);
                    //var _fromDate = new SqlParameter("FromDate", issue.fromDate);
                    //var _toDate = new SqlParameter("ToDate", issue.toDate);
                    var _IssueNo = new SqlParameter("IssueNo", issue.IssueNo);

                    objresult = context.GetIssueProductByIssueNo.FromSqlRaw(
                        "Execute dbo.Pro_IV_StockReceive  @venueNo, @venueBranchNo, @IssueNo",
                    _venueNo, _venueBranchNo, _IssueNo).ToList();
                    //"Execute dbo.Pro_IV_StockReceive @fromBranch, @fromStore, @toBranch, @toStore, @fromDate, @toDate, @venueNo, @venueBranchNo, @againstIndent,@ProductNo,@IssueNo",
                    //_fromBranch, _fromStore, _toBranch, _toStore, _fromDate, _toDate, _venueNo, _venueBranchNo, _againstIndent, _ProductNo, _indentNo,_IssueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetIssuedProductsByIssueNo", ExceptionPriority.Low, ApplicationType.REPOSITORY, issue.venueNo, issue.venueBranchNo, 0);
            }
            return objresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indentsave"></param>
        /// <returns></returns>
        public IndentDetailsSaveResponse InsertIndentDetails(IndentDetailsSaveRequest indent)
        {
            IndentDetailsSaveResponse response = new IndentDetailsSaveResponse();
            var productMaster = indent?.lstIndentProductDetails;
            CommonHelper commonUtility = new CommonHelper();
            var productMasterXML = commonUtility.ToXML(productMaster);
            
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Indentno = new SqlParameter("Indentno", indent?.indentno);
                    var _IndentCode = new SqlParameter("IndentCode", indent?.indentCode);
                    var _IndentDate = new SqlParameter("IndentDate", indent?.indentDate);
                    
                    var _FromVenueBranchNo = new SqlParameter("FromVenueBranchNo", indent?.fromVenueBranchNo);
                    var _FromStoreNo = new SqlParameter("FromStoreNo", indent?.fromStoreNo);

                    var _ToVenueBranchNo = new SqlParameter("ToVenueBranchNo", indent?.toVenueBranchNo);
                    var _ToStoreNo = new SqlParameter("ToStoreNo", indent?.toStoreNo);

                    var _TotalQty = new SqlParameter("TotalQty", indent?.totalQty);
                    var _IsEmergency = new SqlParameter("IsEmergency", indent?.isEmergency);
                    var _Remarks = new SqlParameter("Remarks", indent?.remarks);

                    var _VerifiedOn = new SqlParameter("VerifiedOn", indent?.verifiedOn);
                    var _VerifiedBy = new SqlParameter("VerifiedBy", indent?.verifiedBy);
                    var _AuthorisedOn = new SqlParameter("AuthorisedOn", indent?.authorisedOn);
                    var _AuthorisedBy = new SqlParameter("AuthorisedBy", indent?.authorisedBy);

                    var _VenueNo = new SqlParameter("VenueNo", indent?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", indent?.venueBranchNo);
                    var _Status = new SqlParameter("Status", indent?.status);
                    var _UserNo = new SqlParameter("UserNo", indent?.userno);
                    var _isDraft = new SqlParameter("isDraft", indent?.isDraft);

                    var _ProductMasterXML = new SqlParameter("ProductXML", productMasterXML);
                    
                    var objresult = context.CreateIndentProductDTO.FromSqlRaw(
                    "Execute dbo.pro_InsertIndent " +
                    "@indentno, @IndentCode, @IndentDate, @productXML, @FromVenueBranchNo, @FromStoreNo, @ToVenueBranchNo, @ToStoreNo, " +
                    "@TotalQty, @IsEmergency, @VerifiedOn, @VerifiedBy, @AuthorisedOn, @AuthorisedBy, @Remarks, @userno, @VenueNo, @VenueBranchNo, @status,@isDraft",
                    _Indentno, _IndentCode, _IndentDate, _ProductMasterXML, _FromVenueBranchNo, _FromStoreNo, _ToVenueBranchNo, _ToStoreNo, 
                    _TotalQty, _IsEmergency, _VerifiedOn, _VerifiedBy, _AuthorisedOn, _AuthorisedBy, _Remarks, _UserNo, _VenueNo, _VenueBranchNo, _Status,_isDraft).ToList();
                     
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.InsertIndentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, indent.venueNo, indent.venueBranchNo, 0);
            }
            return response;
        }

        #region Department Issue
        public List<GetIssueProductResponse> GetIssueProductlst(GetIssueProductRequest issue)
        {
            List<GetIssueProductResponse> objresult = new List<GetIssueProductResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", issue.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", issue.venueBranchNo);
                    var _fromBranch = new SqlParameter("FromBranch", issue.fromBranch);
                    var _fromStore = new SqlParameter("FromStore", issue.fromStore);
                    var _toBranch = new SqlParameter("ToBranch", issue.toBranch);
                    var _toStore = new SqlParameter("ToStore", issue.toStore);
                    var _fromDate = new SqlParameter("FromDate", issue.fromDate);
                    var _toDate = new SqlParameter("ToDate", issue.toDate);
                    var _againstIndent = new SqlParameter("AgainstIndent", issue.againstIndent);
                    var _ProductNo= new SqlParameter("ProductNo", issue.productNo);
                    var _indentNo = new SqlParameter("IndentNo", issue.indentNo);

                    objresult = context.GetIssueProduct.FromSqlRaw(
                    "Execute dbo.pro_GetIssueProductlst @fromBranch, @fromStore, @toBranch, @toStore, @fromDate, @toDate, @venueNo, @venueBranchNo, @againstIndent,@ProductNo,@IndentNo",
                    _fromBranch, _fromStore, _toBranch, _toStore, _fromDate, _toDate, _venueNo, _venueBranchNo, _againstIndent, _ProductNo, _indentNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetIssueProductlst", ExceptionPriority.Low, ApplicationType.REPOSITORY, issue.venueNo, issue.venueBranchNo, 0);
            }
            return objresult;
        }
        public SaveIssueProductResponse InsertIssueProductlst(IssueProductRequest issue)
        {
            SaveIssueProductResponse objresult = new SaveIssueProductResponse();
            var issueProduct = issue?.lstIssueProduct;
            
            CommonHelper commonUtility = new CommonHelper();
            var issueProductlist = commonUtility.ToXML(issueProduct);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", issue.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", issue.VenueBranchNo);
                    var _userNo = new SqlParameter("UserNo", issue.UserNo);                   
                    var _xml = new SqlParameter("Issuedxml", issueProductlist);
                   // var _issueNo = new SqlParameter("IssueNo", issue.IssueNo);
                   // var _actionMode = new SqlParameter("ActionMode", issue.ActionMode);
                    var result = context.InsertIssueProduct.FromSqlRaw(
                    "Execute dbo.pro_InsertIssueProductlst @venueNo, @venueBranchNo, @userNo, @issuedxml",
                    _venueNo, _venueBranchNo, _userNo, _xml).ToList();
                    
                    objresult.IssueNo = result != null && result.Count > 0 ? result[0].IssueNo : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.InsertIssueProductlst", ExceptionPriority.Low, ApplicationType.REPOSITORY, issue.VenueNo, issue.VenueBranchNo, 0);
            }
            return objresult;
        }


        public SaveIssueProductResponse InsertIssueReceivedProductlst(IssueProductRequest issue)
        {
            SaveIssueProductResponse objresult = new SaveIssueProductResponse();
            var issueProduct = issue?.lstIssueProduct;

            CommonHelper commonUtility = new CommonHelper();
            var issueProductlist = commonUtility.ToXML(issueProduct);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", issue.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", issue.VenueBranchNo);
                    var _userNo = new SqlParameter("UserNo", issue.UserNo);
                    var _xml = new SqlParameter("Issuedxml", issueProductlist);
                    var _issueNo = new SqlParameter("IssueNo", issue.IssueNo);
                    //var _actionMode = new SqlParameter("ActionMode", issue.ActionMode);
                    var result = context.InsertIssueProduct.FromSqlRaw(
                    "Execute dbo.pro_ReceiveIssueProductList @venueNo, @venueBranchNo, @userNo, @issuedxml,@IssueNo",
                    _venueNo, _venueBranchNo, _userNo, _xml, _issueNo).ToList();

                    objresult.IssueNo = result != null && result.Count > 0 ? result[0].IssueNo : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.InsertIssueProductlst", ExceptionPriority.Low, ApplicationType.REPOSITORY, issue.VenueNo, issue.VenueBranchNo, 0);
            }
            return objresult;
        }

        public List<GetDeptIssueProductResponse> GetDeptIssueProductlst(GetDeptIssueProductRequest issue)
        {
            List<GetDeptIssueProductResponse> lstresult = new List<GetDeptIssueProductResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", issue.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", issue.venueBranchNo);
                    var _fromBranchNo = new SqlParameter("fromBranchNo", issue.fromBranch);
                    var _toBranchNo = new SqlParameter("toBranchNo", issue.toBranch);
                    var _fromStoreNo = new SqlParameter("fromStoreNo", issue.fromStore);
                    var _toStoreNo = new SqlParameter("toStoreNo", issue.toStore);
                    var _pageIndex = new SqlParameter("pageIndex", issue.pageIndex);
                    var _fromDate = new SqlParameter("FromDate", issue?.fromDate);
                    var _toDate = new SqlParameter("ToDate", issue?.toDate);
                    var _type = new SqlParameter("Type", issue?.type);

                    var result = context.GetDeptIssueProductlst.FromSqlRaw(
                    "Execute dbo.pro_GetDeptIssueProductlst @Type, @FromDate, @ToDate, @fromBranchNo, @toBranchNo, @fromStoreNo, @toStoreNo, @venueNo, @venueBranchNo, @pageIndex",
                    _type, _fromDate, _toDate, _fromBranchNo, _toBranchNo, _fromStoreNo, _toStoreNo, _venueNo, _venueBranchNo, _pageIndex).ToList();
                    
                    if (result != null)
                    {
                        foreach (var v in result)
                        {
                            GetDeptIssueProductResponse obj = new GetDeptIssueProductResponse();
                            obj.RowNo = v.RowNo;
                            obj.fromBranch = v.fromBranch;
                            obj.fromStore = v.fromStore;
                            obj.toBranch = v.toBranch;
                            obj.toStore = v.toStore;
                            obj.issueDate = v.issueDate;
                            obj.issueCode = v.issueCode;
                            obj.totalQty = v.totalQty;
                            obj.issueNo = v.issueNo;
                            obj.pageIndex = v.pageIndex;
                            obj.totalRecords = v.totalRecords;
                            obj.statusCode = v.statusCode;
                            lstresult.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetDeptIssueProductlst", ExceptionPriority.Low, ApplicationType.REPOSITORY, issue.venueNo, issue.venueBranchNo, 0);
            }
            return lstresult;
        }
        #endregion

        #region Fetch Products detail in List
        public List<FetchProductListResponse> FetchProductList(ProductMasterRequest objRequest)
        {
            List<FetchProductListResponse> objResult = new List<FetchProductListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _productNo = new SqlParameter("ProductNo", objRequest.productNo);
                    var _venueNo = new SqlParameter("VenueNo", objRequest.venueNo);
                    var _userNo = new SqlParameter("UserNo", objRequest.userNo);
                    var _pageIndex = new SqlParameter("PageIndex", objRequest.pageIndex);
                    var _productTypeNo = new SqlParameter("ProductTypeNo", objRequest.productTypeNo);
                    var _productCategoryNo = new SqlParameter("ProductCategoryNo", objRequest.productCategoryNo);
                    var _genericNo = new SqlParameter("GenericNo", objRequest.genericNo);
                    var _medicineTypeNo = new SqlParameter("MedicineTypeNo", objRequest.medicineTypeNo);
                    var _medicineStrengthNo = new SqlParameter("MedicineStrengthNo", objRequest.medicineStrengthNo);
                    var _manufacturerNo = new SqlParameter("ManufacturerNo", objRequest.manufacturerNo);
                    var _hsnNo = new SqlParameter("HSNNo", objRequest.hsnNo);

                    objResult = context.FetchProductListDTO.FromSqlRaw(
                    "Execute dbo.Pro_Iv_FetchProductList " +
                    "@ProductNo, @VenueNo, @UserNo, @PageIndex, @ProductTypeNo, @ProductCategoryNo, @GenericNo," +
                    "@MedicineTypeNo, @MedicineStrengthNo, @ManufacturerNo, @HSNNo",
                    _productNo, _venueNo, _userNo, _pageIndex, _productTypeNo, _productCategoryNo, _genericNo, 
                    _medicineTypeNo, _medicineStrengthNo, _manufacturerNo, _hsnNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.FetchProductList", ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.venueNo, objRequest.venueBranchNo, objRequest.userNo);
            }
            return objResult;
        }
        #endregion
         public List<Fetchlookalike> Getlookalike(Getlookalikeresponse obj )
        {
            List<Fetchlookalike> objresult = new List<Fetchlookalike>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo",obj.VenueNo);
                    var _lookproductno = new SqlParameter("lookproductno",obj.lookproductno);
                    
                    objresult = context.lookalike.FromSqlRaw(
                    "Execute dbo.pro_GetLookalike @VenueNo,@lookproductno",
                    _VenueNo,_lookproductno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProductMasters-Getlookalike", ExceptionPriority.Low, ApplicationType.REPOSITORY,obj.VenueNo, 0, 0);
            }
            return objresult;
        }
        
        public List<Fetchsoundalike> GetSoundalike(GetSoundalikeresponse obj)
        {
            List<Fetchsoundalike> objresult = new List<Fetchsoundalike>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", obj.VenueNo);
                    var _soundProductno = new SqlParameter("soundProductno", obj.soundProductno);

                    objresult = context.Soundalike.FromSqlRaw(
                    "Execute dbo.pro_GetSoundalike @VenueNo,@soundProductno",
                    _VenueNo, _soundProductno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProductMasters-GetSoundalike", ExceptionPriority.Low, ApplicationType.REPOSITORY, obj.VenueNo, 0, 0);
            }
            return objresult;
        }

        public List<SubProductRes> GetSubProduct(SubProductReq obj)
        {
            List<SubProductRes> objresult = new List<SubProductRes>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", obj.VenueNo);
                    var _subProductNo = new SqlParameter("subProductNo ", obj.subProductNo);

                    objresult = context.SubProduct.FromSqlRaw(
                    "Execute dbo.pro_GetSubProduct @VenueNo,@subProductNo",
                    _VenueNo, _subProductNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProductMasters-GetSubProduct", ExceptionPriority.Low, ApplicationType.REPOSITORY, obj.VenueNo, obj.subProductNo, 0);
            }
            return objresult;
        }
        public List<lstdrugresponse> GetProdVsDrug(lstdrugreq obj)
        {
            List<lstdrugresponse> objresult = new List<lstdrugresponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", obj.venueNo);
                    var _productNo = new SqlParameter("productNo ", obj.productNo);

                    objresult = context.GetProdVsDrug.FromSqlRaw(
                    "Execute dbo.pro_GetProductVsDrugs @venueNo,@productNo", _venueNo, _productNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProdVsDrug", ExceptionPriority.Low, ApplicationType.REPOSITORY, obj.venueNo, obj.productNo, 0);
            }
            return objresult;
        }
        public int InsertProdVsDrugs(savedruglstreq creq1)
        {

            CommonHelper commonUtility = new CommonHelper();
            string drugsXML = commonUtility.ToXML(creq1.lstdrugresponse);
            int i = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", creq1?.venueno);
                    var _productNo = new SqlParameter("productNo", creq1?.productNo);
                    var _userNo = new SqlParameter("userNo", creq1?.userNo);
                    var _drugsXML = new SqlParameter("drugsXML", drugsXML);

                    var lst = context.InsertProdVsDrugs.FromSqlRaw(
                    "Execute dbo.pro_InsertProductVsDrugs @venueNo,@productNo,@userNo,@drugsXML",
                    _venueno,_productNo,_userNo,_drugsXML).ToList();

                    i = lst[0].DrugPresTempNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.InsertProdVsDrugs" + creq1.productNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq1.venueno, creq1.userNo, 0);
            }
            return i;
        }

        #region GetProductUnitList
        /// <summary>
        /// GetProductUnitList
        /// </summary>
        /// <param name="VenueNo"></param>
        /// <returns></returns>
        public List<ProductUnitDTO> GetProductUnitList(int VenueNo)
        {
            List<ProductUnitDTO> objresult = new List<ProductUnitDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);

                    objresult = context.ProductUnitResponse.FromSqlRaw("Execute dbo.Pro_GetProductDetails @VenueNo",_VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetProductUnitList", ExceptionPriority.Medium, ApplicationType.REPOSITORY, VenueNo, VenueNo, 0);
            }
            return objresult;
        }
        #endregion

        #region GetBOMMapping
        /// <summary>
        /// GetBOMMapping
        /// </summary>
        /// <param name="VenueNo"></param>
        /// <param name="TestNo"></param>
        /// <param name="TestType"></param>
        /// <returns></returns>
        public List<BOMMappingDTO> GetBOMMapping(int VenueNo,int TestNo,string TestType)
        {
            List<BOMMappingDTO> objresult = new List<BOMMappingDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _TestNo = new SqlParameter("TestNo", TestNo);
                    var _TestType = new SqlParameter("TestType", TestType);

                    objresult = context.GetBOMMapping.FromSqlRaw("Execute dbo.Pro_GetBOMDetails @VenueNo,@TestNo,@TestType", 
                        _VenueNo, _TestNo, _TestType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetBOMMapping", ExceptionPriority.Medium, ApplicationType.REPOSITORY, VenueNo, VenueNo, 0);
            }
            return objresult;
        }
        #endregion

        #region InsertBOMMapping
        /// <summary>
        /// InsertBOMMapping
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public BOMMappingResponse InsertBOMMapping(List<BOMMappingRequest> req)
        {
            BOMMappingResponse objresult = new BOMMappingResponse();
           
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    CommonHelper commonUtility = new CommonHelper();
                    var BOMMappingXML = commonUtility.ToXML(req);

                    var _InputXML = new SqlParameter("InputXML", BOMMappingXML);

                    var result= context.InsertBOMMapping.FromSqlRaw("Execute dbo.Upsert_Product_TestMapping @InputXML",
                        _InputXML).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProductMasterReposistory.GetBOMMapping", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
#endregion
    }
}
