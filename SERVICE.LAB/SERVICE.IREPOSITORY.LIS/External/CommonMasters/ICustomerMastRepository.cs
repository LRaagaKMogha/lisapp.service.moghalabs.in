using DEV.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface ICustomerMastRepository
    {
        List<LstCustomer> GetCustomer(int a, int b);
    }
}
