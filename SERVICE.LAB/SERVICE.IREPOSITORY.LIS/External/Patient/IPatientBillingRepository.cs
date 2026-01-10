using DEV.Model.External.Billing;
using DEV.Model.External.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.Patient
{
    public interface IPatientBillingRepository
    {
        LstPatientBillingInfo GetPatientBillInfo(int a, int b, int pVisitNo);
        LstPatientCancelBillingInfo GetPatientCancelBillInfo(int a, int b, int pVisitNo);
        List<LstCancelPatientInfo> GetCancelServiceDetails(int a, int b, string pdtFrom, string pdtTo);
    }     
}
