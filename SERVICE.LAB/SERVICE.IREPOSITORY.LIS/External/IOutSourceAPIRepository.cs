using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IOutSourceAPIRepository
    {
        List<OutSourceAPIDTOResponse> GetOutSourceAPIList(OutSourceAPIDTORequest results);
        int AckOutSourceAPIList(AckOutSourceAPIDTORequest results);
    }
}