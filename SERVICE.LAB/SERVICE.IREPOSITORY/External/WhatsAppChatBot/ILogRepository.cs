using Service.Model.External.WhatsAppChatBot;
using System.Collections.Generic;

namespace Service.IRepository.External.WhatsAppChatBot
{
    public interface ILogRepository
    {
        int InsertLog(InsertLogRequest RequestItem);
        List<FetchLogResponse> GetLogDetails(FetchLogRequest RequestItem);
    }
}
