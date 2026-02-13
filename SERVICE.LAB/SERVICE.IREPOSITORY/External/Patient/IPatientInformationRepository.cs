using Service.Model.External.Patient;
using System.Collections.Generic;

namespace Service.IRepository.External.Patient
{
    public interface IPatientInformationRepository
    {
        List<LstPatientInfo> GetPatientInfo(int a, int b, string pdtFrom, string pdtTo);
    }
}
