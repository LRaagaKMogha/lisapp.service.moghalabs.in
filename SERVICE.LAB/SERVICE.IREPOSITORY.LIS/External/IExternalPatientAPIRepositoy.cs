using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IExternalPatientAPIRepositoy
    {
        ExternalPatientLoginResponse Login(ExternalPatientLoginRequest results);
        ExternalPatientOTPResponse OTPVerify(ExternalPatientOTPRequest results);
        ExternalPatientSignupResponse Signup(ExternalPatientSignupRequest results);
        ExternalPatientAppResponse addMember(ExternalPatientAddmember results);
        ExternalPatientMasterData getMasterRecord(ExternalPatientCommonRequest results);
        ExternalPatientAppServiceResponse GetService(int VenueNo, int VenueBranchNo,int IsApproval);
        ServiceRateList GetServiceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo);
        ExternalPatientEditResponse editprofile(ExternalPatientEditRequest results);
        ExternalPatientUserDetail getUserDetail(ExternalPatientCommonRequest results);
      
        //List<ExternalPatientfamilyResponse> myfamily(ExternalPatientCommonRequest results);
        //List<ExternalPatientService> getServices(ExternalPatientCommonRequest results);
        //List<ExternalPatientAppResponse> addMember(ExternalPatientAddmember results);
        //List<ExternalPatientAppResponse> insertPayments(ExternalPatientPayment results);
    }

}

