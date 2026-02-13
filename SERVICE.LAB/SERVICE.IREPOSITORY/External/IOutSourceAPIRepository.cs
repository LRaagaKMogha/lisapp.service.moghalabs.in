using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface IOutSourceAPIRepository
    {
        List<OutSourceAPIDTOResponse> GetOutSourceAPIList(OutSourceAPIDTORequest results);
        int AckOutSourceAPIList(AckOutSourceAPIDTORequest results);
    }
}