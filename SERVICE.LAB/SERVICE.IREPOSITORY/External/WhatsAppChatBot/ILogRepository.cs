using Service.Model.External.WhatsAppChatBot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.WhatsAppChatBot
{
    public interface ILogRepository
    {
        int InsertLog(InsertLogRequest RequestItem);
        List<FetchLogResponse> GetLogDetails(FetchLogRequest RequestItem);
    }
}
