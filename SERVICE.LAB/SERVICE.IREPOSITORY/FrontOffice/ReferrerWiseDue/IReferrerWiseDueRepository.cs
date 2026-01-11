using Service.Model;
using System.Collections.Generic;

namespace Dev.IRepository.FrontOffice
{
    public interface IReferrerWiseDueRepository
    {
        RefWiseDueResponse GetRefWiseDueResponses(RefWiseDueRequest request);
    }
}
