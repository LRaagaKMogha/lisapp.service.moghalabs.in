using Service.Model.External.WhatsAppChatBot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.WhatsAppChatBot
{
    public interface IBranchMasterRepository
    {
        List<lstBranch> GetBranchList(int a, int b);
    }
}
