using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IProcessingbranchRepository
    {
        List<responsebranch> GetProcessingbranch(reqbranch req);
        Storeprocessingbranch InsertProcessingbranch(insertbranch obj1);
    }
}
