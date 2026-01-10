using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IStockCorrectionRepositoty
    {
        List<GetStockCorrectionResponse> GetAllStockCorrection(GetStockCorrectionRequest request);
        CommonAdminResponse InsertStockCorrection(InsertStockCorrection insertConsumption);
        List<GetProductStockResponse> GetProductStock(GetProductStockRequest req);
        CommonAdminResponse InsertStockAdjustment(InsertStockAdjustment req);
        List<GetStockAdjustmentResponse> GetAllStockAdjustment(GetStockAdjustmentRequest req);
        GetStockAdjustProductDetailsResponse GetStockAdjustProductDetails(int VenueNo, int BranchNo, int stkadjNo);
        List<GetStoreStockProductListResponse> GetStoreStockProductList(int VenueNo, int BranchNo, int StoreNo);
        CommonAdminResponse InsertStockConsumption(InsertStockConsumption req);
        List<GetAllConsumptionListResponse> GetAllConsumptionList(GetAllConsumptionEntryRequest request);
        List<ConsumptionDetailsResponse> GetStockConsumptionDetails(GetConsumptionListRequest req);
    }
}

