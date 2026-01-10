using DEV.Model.External.WhatsAppChatBot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.External.WhatsAppChatBot
{
    public interface ICommonReportRepository
    {
        Task<List<PatientReportResponse>> GetPatientReport(PatientReportRequest objReq);
    }
}
