using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;
namespace Dev.IRepository
{
    public interface IProcessingbranchRepository
    {

        List<responsebranch> GetProcessingbranch(reqbranch req);


        Storeprocessingbranch InsertProcessingbranch(insertbranch obj1);




    }


}
