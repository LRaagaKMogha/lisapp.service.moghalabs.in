using Service.Model;

namespace Service.IRepository.FrontOffice
{
    public interface IReferrerWiseDueRepository
    {
        RefWiseDueResponse GetRefWiseDueResponses(RefWiseDueRequest request);
    }
}
