using Service.Model;
using System.Threading.Tasks;

namespace Service.IRepository
{
    public interface IEditBillingRepository
    {
        Task<FrontOffficeResponse> InsertEditBilling(FrontOffficeDTO objDTO);
        Task<GetEditPatientDetailsFinalResponse> GetEditPatientDetails(long visitNo,int VenueNo, int VenueBranchNo);
        dynamic ValidatePTTTest(int ServiceNo, string ServiceType, int VisitNo, int VenueNo, int VenueBranchNo);
    }
}

