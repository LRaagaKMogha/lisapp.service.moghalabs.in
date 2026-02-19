using System.Collections.Generic;
using Service.Model;
using Service.Model.Admin;

namespace Service.IRepository
{
    public interface IAdminRepository
    {
        CommonAdminResponse DeleteVisitId(DeleteVisitRequest deleteVisitRequest);
        CommonAdminResponse UpdateCustomerDetails(UpdateCustomerDetails RequestItem);
        List<SearchVisitdetailsResponse> SearchVisitId(DeleteVisitRequest deleteVisitRequest);
        List<SearchUpdateDatesResponse> SearchUpdateDates(DeleteVisitRequest deleteVisitRequest);
        CommonAdminResponse UpdateOrderDates(UpdateOrderDatesRequest RequestItem);
        List<ResponseDataScrollText> SearchScrollText(RequestDataScrollText reqItem);
        List<PaymentMode> GetPaymentMode(GetPaymentModeRequest RequestItem);
        SavePaymentModeResponse UpdateVisitPaymentModes(SavePaymentModeRequest RequestItem);
        List<Responsehistory> DeleteHistory(visitRequest obj);
    }
}

