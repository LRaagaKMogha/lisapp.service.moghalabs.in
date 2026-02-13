using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface IMethodRepository
    {
        List<TblMethod> GetMethods1(GetCommonMasterRequest masterRequest);
        int InsertMethodDetails1 (TblMethod Methoditem);
        List<TblMethod> GetMethods(GetCommonMasterRequest masterRequest);
        List<MethodResponse> InsertMethodDetails(TblMethod Methoditem);
    }
}

