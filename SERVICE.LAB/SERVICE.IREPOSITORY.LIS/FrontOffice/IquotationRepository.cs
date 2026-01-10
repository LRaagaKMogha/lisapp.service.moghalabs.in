using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;
namespace Dev.IRepository
{
    public interface IquotationRepository
    {
         List<returnquotationlst> Getquotation(requestquotation req);

         int Insertquotation(responselst req1);

    }

}
