using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository.Master
{
    public interface IDiscountRepository
    {
        List<GetDiscountDetails> GetDiscountMasters(DiscountMasterRequest discountItem);
        DiscountMasterReponse InsertDiscountMasters(DiscountInsertData disResponse);
    }
}
