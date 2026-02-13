using Service.Model;
using Service.Model.Sample;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IICMRResponseRepository
    {
        List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO RequestItem);
    }
}
