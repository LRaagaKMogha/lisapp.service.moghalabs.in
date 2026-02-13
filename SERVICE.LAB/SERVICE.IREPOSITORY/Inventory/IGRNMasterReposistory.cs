using Service.Model;
using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IGRNMasterReposistory
    {
        List<GetAllGRNResponse> GetAllGRN(GetAllGRNRequest masterRequest);
        List<GetPOBySupplierResponse> GetPOBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo);
        List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber);
        CommonAdminResponse InsertGRNMaster(InsertGRNMasterRequest insertGRNMaster);
        List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int grnMasterNo);
        List<GetProductsByPOResponse> GetGRNProductDetails(int venueNo, int venueBranchNo, int grnMasterNo);
        CommonAdminResponse UpdateInvoiceDetails(InvoiceUpdateRequest req);
    }
}
