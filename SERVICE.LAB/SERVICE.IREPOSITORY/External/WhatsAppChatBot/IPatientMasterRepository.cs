using Service.Model.External.WhatsAppChatBot;
using System.Collections.Generic;

namespace Service.IRepository.External.WhatsAppChatBot
{
    public interface IPatientMasterRepository
    {
        GetPatientResponse GetPatientMaster(GetPatientRequest RequestItem);
        int UpdatePatientMaster(UpdatePatientRequest RequestItem);
        List<PatientInformationResponse> GetPatientList(GetPatientRequest RequestItem);
        PatientVisitResponse GetPatientVisit(GetPatientVisitRequest RequestItem);
    }
}
