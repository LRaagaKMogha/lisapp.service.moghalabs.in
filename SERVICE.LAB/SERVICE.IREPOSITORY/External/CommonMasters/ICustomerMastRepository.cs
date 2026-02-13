using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface ICustomerMastRepository
    {
        List<LstCustomer> GetCustomer(int a, int b);
    }
}
