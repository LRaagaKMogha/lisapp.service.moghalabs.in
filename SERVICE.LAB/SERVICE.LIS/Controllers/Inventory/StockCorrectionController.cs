using Dev.IRepository.Inventory;
using Dev.Repository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StockCorrectionController : ControllerBase
    {
        private readonly IStockCorrectionRepositoty _StockCorrectionRepositoty;
        public StockCorrectionController(IStockCorrectionRepositoty StockCorrectionRepositoty)
        {
            _StockCorrectionRepositoty = StockCorrectionRepositoty;           
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/GetStockCorrection")]
        public IEnumerable<GetStockCorrectionResponse> GetAllStockCorrection(GetStockCorrectionRequest request)
        {
            List<GetStockCorrectionResponse> result = new List<GetStockCorrectionResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetAllStockCorrection(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetAllStockCorrection", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.venueno, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/InsertStockCorrection")]
        public CommonAdminResponse InsertStockCorrection(InsertStockCorrection saveConsumption)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _StockCorrectionRepositoty.InsertStockCorrection(saveConsumption);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.InsertStockCorrection", ExceptionPriority.Low, ApplicationType.APPSERVICE, saveConsumption.VenueNo, saveConsumption.VenueBranchNo, saveConsumption.UserNo);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/GetProductStock")]
        public IEnumerable<GetProductStockResponse> GetProductStock(GetProductStockRequest req)
        {
            List<GetProductStockResponse> result = new List<GetProductStockResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetProductStock(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetProductStock", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.BranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/InsertStockAdjustment")]
        public CommonAdminResponse InsertStockAdjustment(InsertStockAdjustment req)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _StockCorrectionRepositoty.InsertStockAdjustment(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.InsertStockAdjustment", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/GetAllStockAdjustment")]
        public IEnumerable<GetStockAdjustmentResponse> GetAllStockAdjustment(GetStockAdjustmentRequest request)
        {
            List<GetStockAdjustmentResponse> result = new List<GetStockAdjustmentResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetAllStockAdjustment(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetAllStockAdjustment", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.venueno, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/StockCorrection/GetStockAdjustProductDetails")]
        public GetStockAdjustProductDetailsResponse GetStockAdjustProductDetails(int VenueNo, int BranchNo, int StkadjNo)
        {
            GetStockAdjustProductDetailsResponse result = new GetStockAdjustProductDetailsResponse();
            try
            {
                result = _StockCorrectionRepositoty.GetStockAdjustProductDetails(VenueNo, BranchNo, StkadjNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetStockAdjustProductDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, BranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpGet]
        [Route("api/StockCorrection/GetStoreStockProductList")]
        public IEnumerable<GetStoreStockProductListResponse> GetStoreStockProductList(int VenueNo, int BranchNo, int StoreNo)
        {
            List<GetStoreStockProductListResponse> result = new List<GetStoreStockProductListResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetStoreStockProductList(VenueNo, BranchNo, StoreNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetStoreStockProductList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, BranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/InsertStockConsumption")]
        public CommonAdminResponse InsertStockConsumption(InsertStockConsumption req)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _StockCorrectionRepositoty.InsertStockConsumption(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.InsertStockConsumption", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/GetConsumptionEntryList")]
        public IEnumerable<GetAllConsumptionListResponse> GetConsumptionEntryList(GetAllConsumptionEntryRequest request)
        {
            List<GetAllConsumptionListResponse> result = new List<GetAllConsumptionListResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetAllConsumptionList(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ConsumptionMappingController.GetConsumptionEntryList", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.venueno, request.BranchNo, request.userno);
            }
            return result;
        }

        [CustomAuthorize("INVOPERATIONS")]
        [HttpPost]
        [Route("api/StockCorrection/GetStockConsumptionDetails")]
        public IEnumerable<ConsumptionDetailsResponse> GetStockConsumptionDetails(GetConsumptionListRequest req)
        {
            List<ConsumptionDetailsResponse> result = new List<ConsumptionDetailsResponse>();
            try
            {
                result = _StockCorrectionRepositoty.GetStockConsumptionDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StockCorrectionController.GetStockConsumptionDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return result;
        }
    }
}
