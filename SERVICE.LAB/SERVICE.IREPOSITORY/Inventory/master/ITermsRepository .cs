using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface ITermsRepository

    {
            List<TblTerms> GettermsDetails(TermsmasterRequest termsmaster);
            Termsmasterresponse InsertTermsmaster(TblTerms tblterms);
    }
}
