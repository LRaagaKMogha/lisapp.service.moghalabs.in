using Service.Model;
using Service.Model.Integration;
using Service.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository
{
    public interface IFrontOfficeRepository
    {
        List<TblCountryList> GetCountry(int VenueNo);
        List<TblState> GetState(int VenueNo);
        List<TblCity> GetCity(int VenueNo);
        GetDetailsByPincode GetDetailsByPincode(int VenueNo, int VenueBranchNo, string PinCode);
        List<TblPhysician> GetPhysicianDetails(int VenueNo, int VenueBranchNo);
        List<TblPhysicianSearch> GetPhysicianDetailsbyName(int VenueNo, int VenueBranchNo, string physicianName, int type = 0);
        List<TblDiscount> GetDiscountMaster(int VenueNo, int VenueBranchNo);
        List<CustomerList> GetCustomers(int VenueNo, int VenueBranchNo, int UserNo, int IsFranchisee, bool ExcludePostpaid, bool ExcludePrepaid, bool ExcludeCash, bool IsApproval, int IsClinical = 2, int clientType = 0, bool IsMapping = false);
        CustomerList GetCustomerDetails(long Customerno, int VenueNo, int VenueBranchNo);
        List<GroupTestDTO> GetGrouptest(int ServiceNo, string ServiceType, int VenueNo, int VenueBranchNo);
        List<TblCurrency> GetCurrency(int VenueNo);
        List<ServiceSearchDTO> GetService(int VenueNo, int VenueBranchNo, int IsApproval);
        ServiceRateList GetServiceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo, int physicianNo, int splratelisttype);
        FrontOffficeResponse InsertFrontOfficeMaster(FrontOffficeDTO objDTO);
        MassRegistrationResponse InsertMassRegistration([FromBody] ExternalBulkFile objDTO);
        Task<ReportOutput> PrintBill(ReportRequestDTO req);
        GetPatientDetailsWithServices GetPatientDetails(long visitNo, int VenueNo, int VenueBranchNo, string searchType = null, int PatientNo = 0, int Isprocedure = 0);
        List<QueueOrderDTO> GetQueueOrderDetails(CommonFilterRequestDTO RequestItem);
        List<MassFileDTO> GetMassFileRegistration(CommonFilterRequestDTO RequestItem);
        List<massPatientBarcode> DownloadMassFile(int MassFileNo, int VenueNo, int VenueBranchNo);
        FrontOffficeQueueResponse UpdateQueueOrder(CommonFilterRequestDTO RequestItem);
        List<rescheckExists> checkExists(reqcheckExists req);
        DoctorDetails InsertDoctor(DoctorDetails objDTO);
        int PushNotifyMessage(int patientVisitNo, int venueno, int venuebranchno, int userno, string messagetype, string message);
        int InsertPatientNotifyLog(PatientNotifyLog objDTO);
        List<PatientNotifyLog> GetPatientNotifyLog(PatientNotifyLog req);
        ExternalVisitDetailsResponse CheckExternalVistIdExists(ExternalVisitDetails req);
        List<TestPrePrintDetailsResponse> GetTestPrePrintDetails(TestPrePrintDetailsRequest req);
        List<CreateManageSampleResponse> PrePrintManageSample(List<PrePrintBarcodeRequest> createManageSample);
        FrontOffficeValidatetest getvalidatetest(List<ServiceParamDTO> req);
        CustomerCurrentBalance GetCustomerCurrentBalance(long Customerno, int VenueNo, int VenueBranchNo);
        List<GetDiscountApprovalResponse> GetDiscountApprovalDetails(GetDiscountApprovalDto objDTO);
        SaveDiscountApprovalResponse InsertDiscountApprovalDetails(SaveDiscountApprovalDto objDTO);
        dynamic ValidateNricNo(int ServiceNo, string ServiceType, string NricNo, int VenueNo, int VenueBranchNo, bool IsNonConcurrent = false);
        ClinicalSummary GetPatientClinicalSummary(PatientNotifyLog objDTO);
        List<OptionalTestDTO> GetOptionalSelectedInPackages(int ServiceNo, int VenueNo, int VenueBranchNo, int PatientVisitNo);
        FrontOffficeResponse InsertFrontOfficeRegistration(FrontOffficeDTO objDTO);
        List<ClinicalHistory> GetClinicalHistory(int venueNo, int venueBranchNo, int patientVisitNo);
        CommonAdminResponse InsertClinicalHistory(InsertClinicalHistory insertClinicalHistory);
        List<Tblloyal> getloyalcard(TblloyalReq req);
        PatientVisitPatternIDGenRes GetVisitPatternID(PatientVisitPatternIDGenReq req);
        PreBookingtResponse InsertPreBookingDetails(PreBookingtRequest objDTO);
        List<PreBookingtDTO> GetPreBookingDetails(CommonFilterRequestDTO RequestItem);
        AutoLoyaltyIDGenResponse GetLoyaltyCardPatternID(AutoLoyaltyIDGenRequest req);
    }
}
