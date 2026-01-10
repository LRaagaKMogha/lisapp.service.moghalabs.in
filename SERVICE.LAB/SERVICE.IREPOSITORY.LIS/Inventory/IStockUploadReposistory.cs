using DEV.Model;
using DEV.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IStockUploadReposistory
    {
        List<GetStockProductListResponse> GetProductListByDepartment(int venueNo, int venueBranchNo, int branchNo, int StoreNo);
        //List<GetProductsByPOResponse> GetProductByPO(int venueNo, int venueBranchNo, int poNumber);
        CommonAdminResponse InsertStockUpload(InsertStockUploadRequest insertStockUpload);
        List<GetProductMainbyDeptRes> GetProductSubyMaindept(GetProductMainbyDeptReq Req);
    }
}
