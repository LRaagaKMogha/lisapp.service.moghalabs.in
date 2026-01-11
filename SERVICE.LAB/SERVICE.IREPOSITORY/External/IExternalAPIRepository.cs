using Service.Model;
using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IExternalAPIRepository
    {
        ExternalLoginResponse Login(ExternalLogin results);
        ExternalCommonResponse LocationUpdate(Location results);
        ExternalAppointmentResponse LoadAppointment(AppointmentRequest results);
        ExternalCommonResponse UploadPrescription(ExternalPrescription results);
        ExternalAddTestResponse AddNewTest(ExternalAddTest results);
        ExternalCommonResponse DeleteServiceTest(ExternalDeleteTestRequest results);
        ExternalCommonResponse ValidatePrePrintedBarcode(ExternalBarcode results);
        ExternalCommonResponse InsertPayment(ExternalBookingPayment results);
        ExternalCommonResponse SignOut(ExternalSignout results);
        ExternalApiReferralResponse GetReferralDetails(ExternalApiReferralRequest results);
        ExternalApiServiceResponse GetServiceDetails(ExternalServiceRequest serviceRequest);
        ExternalCommonResponse InsertBooking(ExternalBookingDto results);
        ExternalCommonResponse UpdateRiderStatus(ExternalRiderStatusRequest results);
        ExternalCommonResponse UpdatePatientStatus(ExternalPatientStatusRequest results);
        List<ExternalHCAppointment> GetHCAppointsList(CommonFilterRequestDTO RequestItem);
        ExternalupdateCommonResponse UpdateHCPatientDetails(UpdateHcpatient results);
        UpdateStatusApptDateResponse UpdateStatusApptDate(UpdateStatusApptDateRequest results);
        List<TestSlotBookingDTO> GetSlotBooking(CommonFilterRequestDTO RequestItem);
        SlotBookingupdateCResponse UpdateSlotBooking(UpdateHcpatient results);
        TestSlotCommonResponse InsertTestSlotBooking(ExternalBookingDTO objDTO);
    }

}

