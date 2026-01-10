using DEV.Model;
using DEV.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
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
