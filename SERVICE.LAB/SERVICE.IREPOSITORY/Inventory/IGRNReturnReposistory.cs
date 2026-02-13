using Service.Model;
using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IGRNReturnReposistory
    {
        List<GetAllGRNReturnResponse> GetAllGRNReturn(GetAllGRNReturnRequest masterRequest);
        List<GetGRNBySupplierResponse> GetGRNBySupplierDetails(int venueNo, int venueBranchNo, int supplierNo);
        List<GetProductsByGRNResponse> GetProductByGRN(int venueNo, int venueBranchNo, int grnNo);
        List<GetProductsByGRNNo> GetGRNReturnProduct(int venueNo, int venueBranchNo, int grnNumber);        
        CommonAdminResponse InsertGRNReturn(PostGRN insertGRNReturn);
        List<otherChargeModal> GetGRNOCDetailsById(int venueNo, int venueBranchNo, int GRNReturnNo);
    }
}
