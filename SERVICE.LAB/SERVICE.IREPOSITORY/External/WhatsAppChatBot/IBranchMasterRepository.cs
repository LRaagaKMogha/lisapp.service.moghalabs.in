using Service.Model.External.WhatsAppChatBot;
using System.Collections.Generic;

namespace Service.IRepository.External.WhatsAppChatBot
{
    public interface IBranchMasterRepository
    {
        List<lstBranch> GetBranchList(int a, int b);
    }
}
