using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IDepartmentRepository
    {
        List<TblDepartment> GetDepartmentDetails(GetCommonMasterRequest getCommonMaster);
        List<TblDepartment> SearchDepartment(string DepartmentName);
        int InsertDepartmentDetails(TblDepartment Departmentitem);
        List<GetMaindepartment> GetMaindepartmentdetail(GetDeptMasterRequest getCommonMaster);
        List<DepartMentLangCodeRes> InsertLangCodeDeptMaster(DepartMentLangCodeReq req);
        List<GetDeptLangCodeRes> GetLangCodeDeptMaster(GetDeptLangCodeReq req);
    }
}
