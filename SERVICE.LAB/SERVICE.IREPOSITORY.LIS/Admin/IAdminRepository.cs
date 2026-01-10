using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;
using DEV.Model.Admin;
using DEV.Model.PatientInfo;

namespace Dev.IRepository
{
    public interface IAdminRepository
    {
        CommonAdminResponse DeleteVisitId(DeleteVisitRequest deleteVisitRequest);
        CommonAdminResponse UpdateCustomerDetails(UpdateCustomerDetails RequestItem);
        List<SearchVisitDetailsResponse> SearchVisitId(DeleteVisitRequest deleteVisitRequest);
        List<SearchUpdateDatesResponse> SearchUpdateDates(DeleteVisitRequest deleteVisitRequest);
        CommonAdminResponse UpdateOrderDates(UpdateOrderDatesRequest RequestItem);
        List<ResponseDataScrollText> SearchScrollText(RequestDataScrollText reqItem);
        List<PaymentMode> GetPaymentMode(GetPaymentModeRequest RequestItem);
        SavePaymentModeResponse UpdateVisitPaymentModes(SavePaymentModeRequest RequestItem);
        List<responsehistory> DeleteHistory(visitRequest obj);
    }
}

