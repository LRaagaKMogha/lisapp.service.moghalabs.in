using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IquotationRepository
    {
         List<returnquotationlst> Getquotation(requestquotation req);
         int Insertquotation(responselst req1);
    }
}