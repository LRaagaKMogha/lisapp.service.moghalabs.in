using Service.Model;
using Service.Model.Integration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IRepository
{
    public interface IIntegrationRepository
    {
        //Task<ExternalPatientDetailsResponse> GetPatientdetailsForEditing(EditPatientDetailsRequest request, UserClaimsIdentity user);
        Task<List<TestValidation>> GetTestValidation(WaitingListSaveRequest testDetails);
        orderresponse SendOrderDetails(orderrequestdetails orderrequestdetails, UserClaimsIdentity user);
        List<TestMasterDetails> GetTestDetails(TestDetailsRequest testDetailsRequest, UserClaimsIdentity user);
        //List<massregistration> massregistration(List<orderrequestdetails> orderlist, UserClaimsIdentity user);
        Task<waitinglistresponse> GetWaitingList(waitinglistrequest request, UserClaimsIdentity user);
        Task<waitinglistresponse> GetMassRegistrationResponse(waitinglistrequest request, UserClaimsIdentity user);
        Task<WaitingListSaveRequest> CreateManageSample(WaitingListSaveRequest createManageSample, UserClaimsIdentity user);
        Task<List<IntegrationOrderTestDetailsResponse>> AddTest(AddTestRequest request, UserClaimsIdentity user, long patientVisitNo);
        Task<bool> updateRegistrations(OnHoldRegistrationRequest request, UserClaimsIdentity user);
        Task<WaitingListMessage> updateMessages(WaitingListMessage request, UserClaimsIdentity user);
        Task<ExternalPatientDetailsResponse> GetPatientInformation(string patientVisitId, string system, UserClaimsIdentity user);
        Task<bool> UpdateMassRegistration(UpdateMassRegistrationRequest request);
        List<Labresponsedetails> GetPDFReportDetails(reportrequestdetails reportrequestdetails, UserClaimsIdentity user);
        List<Labtestdetails> GetPDFReportTestDetails(int visitno, UserClaimsIdentity user);
        void NotifyIntegrationVisitStatus(int visitno, int venueNo, int venueBranchNo);
        List<LabReportTestDetails> GetDiscreetLabData(int visitno, UserClaimsIdentity user);
        List<testdetailsTrendReport> GetTestTrendReportDetails(requestTrendReport requestTrendReport, string TestCode, UserClaimsIdentity user);
    }
}

