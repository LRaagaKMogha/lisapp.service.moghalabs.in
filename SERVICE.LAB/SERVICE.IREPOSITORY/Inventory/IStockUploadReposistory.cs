using Service.Model;
using Service.Model.Inventory;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IStockUploadReposistory
    {
        List<GetStockProductListResponse> GetProductListByDepartment(int venueNo, int venueBranchNo, int branchNo, int StoreNo);
        CommonAdminResponse InsertStockUpload(InsertStockUploadRequest insertStockUpload);
        List<GetProductMainbyDeptRes> GetProductSubyMaindept(GetProductMainbyDeptReq Req);
    }
}
