using Service.Model;

namespace Service.IRepository
{
    public interface IExternalPatientAPIRepositoy
    {
        ExternalPatientLoginResponse Login(ExternalPatientLoginRequest results);
        ExternalPatientOTPResponse OTPVerify(ExternalPatientOTPRequest results);
        ExternalPatientSignupResponse Signup(ExternalPatientSignupRequest results);
        ExternalPatientAppResponse addMember(ExternalPatientAddmember results);
        ExternalPatientMasterData getMasterRecord(ExternalPatientCommonRequest results);
        ExternalPatientAppServiceResponse Getservice(int VenueNo, int VenueBranchNo,int IsApproval);
        ServiceRateList GetserviceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo);
        ExternalPatientEditResponse editprofile(ExternalPatientEditRequest results);
        ExternalPatientUserDetail getUserDetail(ExternalPatientCommonRequest results);
    }
}