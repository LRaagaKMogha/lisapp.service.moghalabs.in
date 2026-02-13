using Service.Model.External.WhatsAppChatBot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IRepository.External.WhatsAppChatBot
{
    public interface ICommonReportRepository
    {
        Task<List<PatientReportResponse>> GetPatientReport(PatientReportRequest objReq);
    }
}
