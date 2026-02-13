using Service.Model;

namespace Service.IRepository
{
    public interface IEditBillingRepository
    {
        FrontOffficeResponse InsertEditBilling(FrontOffficeDTO objDTO);
        GetEditPatientDetailsFinalResponse GetEditPatientDetails(long visitNo,int VenueNo, int VenueBranchNo);
        dynamic ValidatePTTTest(int ServiceNo, string ServiceType, int VisitNo, int VenueNo, int VenueBranchNo);
    }
}

