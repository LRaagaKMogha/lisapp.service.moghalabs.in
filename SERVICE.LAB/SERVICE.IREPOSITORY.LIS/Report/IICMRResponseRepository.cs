using DEV.Model;
using DEV.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IICMRResponseRepository
    {
        List<GetICMRResponse> GetICMRResult(CommonFilterRequestDTO RequestItem);
    }
}
