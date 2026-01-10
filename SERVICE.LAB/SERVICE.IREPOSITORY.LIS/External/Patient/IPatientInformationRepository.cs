using DEV.Model.External.Billing;
using DEV.Model.External.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.Patient
{
    public interface IPatientInformationRepository
    {
        List<LstPatientInfo> GetPatientInfo(int a, int b, string pdtFrom, string pdtTo);
    }
}
