using Service.Model.External.Billing;
using Service.Model.External.Patient;
using System.Collections.Generic;

namespace Service.IRepository.External.Patient
{
    public interface IPatientBillingRepository
    {
        LstPatientBillingInfo GetPatientBillInfo(int a, int b, int pVisitNo);
        LstPatientCancelBillingInfo GetPatientCancelBillInfo(int a, int b, int pVisitNo);
        List<LstCancelPatientInfo> GetCancelServiceDetails(int a, int b, string pdtFrom, string pdtTo);
    }     
}
