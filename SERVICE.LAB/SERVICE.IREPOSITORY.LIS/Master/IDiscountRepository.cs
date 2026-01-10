using DEV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.Master
{
    public interface IDiscountRepository
    {
        List<GetDiscountDetails> GetDiscountMasters(DiscountMasterRequest discountItem);
        DiscountMasterReponse InsertDiscountMasters(DiscountInsertData disResponse);
    }
}
