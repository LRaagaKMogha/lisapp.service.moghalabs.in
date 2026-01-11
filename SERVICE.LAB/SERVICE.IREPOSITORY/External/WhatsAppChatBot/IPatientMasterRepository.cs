using Service.Model.External.WhatsAppChatBot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.WhatsAppChatBot
{
    public interface IPatientMasterRepository
    {
        GetPatientResponse GetPatientMaster(GetPatientRequest RequestItem);
        int UpdatePatientMaster(UpdatePatientRequest RequestItem);
        List<PatientInformationResponse> GetPatientList(GetPatientRequest RequestItem);
        PatientVisitResponse GetPatientVisit(GetPatientVisitRequest RequestItem);
    }
}
