using System;
using System.Collections.Generic;
using System.Linq;
using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StockUploadController : ControllerBase
    {
        private readonly IStockUploadReposistory _StockUploadRepository;
        public StockUploadController(IStockUploadReposistory noteRepository)
        {
            _StockUploadRepository = noteRepository;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/StockUpload/GetProductListByDepartment")]
        public List<GetStockProductListResponse> GetProductListByDepartment(int venueNo, int venueBranchNo, int branchNo, int StoreNo)
        {
            List<GetStockProductListResponse> objresult = new List<GetStockProductListResponse>();
            try
            {
                objresult = _StockUploadRepository.GetProductListByDepartment(venueNo, venueBranchNo, branchNo, StoreNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadController.GetProductListByDepartment", ExceptionPriority.Medium, ApplicationType.APPSERVICE, venueNo, venueBranchNo, branchNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockUpload/InsertStockUpload")]
        public CommonAdminResponse InsertStockUpload(InsertStockUploadRequest postProductStockRequest)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _StockUploadRepository.InsertStockUpload(postProductStockRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadController.InsertStockUpload", ExceptionPriority.Low, ApplicationType.APPSERVICE, postProductStockRequest.venueNo, postProductStockRequest.venueBranchNo, postProductStockRequest.createdby);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockUpload/GetProductSubMaindept")]
        public List<GetProductMainbyDeptRes> GetProductSubyMaindept(GetProductMainbyDeptReq Req)
        {
            List<GetProductMainbyDeptRes> objresult = new List<GetProductMainbyDeptRes>();
            try
            {
                objresult = _StockUploadRepository.GetProductSubyMaindept(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockUploadController.GetProductSubyMaindept-", ExceptionPriority.Low, ApplicationType.APPSERVICE, Req.VenueNo, Req.VenueBranchNo, Req.StoreNo);
            }
            return objresult;
        }
    }
}