using Service.Model;
using Service.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IPurchaseOrderReposistory
    {
        List<GetPurchaseOrderResponse> GetPurchaseOrders(GetAllPORequest masterRequest);
        List<GetSupplierServiceDTO> GetSupplierServiceDetails(int venueNo, int venueBranchNo, int supplierNo, int StoreNo, string type);
        CommonAdminResponse InsertPurchaseOrder(InsertPurchaseOrder insertPurchaseOrder);
        List<GetPurchaseDetailsDTO> GetPurchaseDetailsById(int venueNo, int venueBranchNo, int PurchaseNo);
        List<POProductDetailsDTO> GetPOProductDetailsById(int venueNo, int venueBranchNo, int PurchaseNo);
        List<GetTaxDatilsResponse> GetPOTaxDetailsById(int venueNo, int venueBranchNo, int PurchaseNo);
        List<otherChargeModal> GetPOOCDetailsById(int venueNo, int venueBranchNo, int PurchaseNo);
        List<Termsconditionlist> GetPOTermsDetailsById(int venueNo, int venueBranchNo, int PurchaseNo);
    }
}
