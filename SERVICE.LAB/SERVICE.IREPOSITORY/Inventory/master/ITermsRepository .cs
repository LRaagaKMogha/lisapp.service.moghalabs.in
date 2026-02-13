using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
    public interface ITermsRepository
    {
        List<TblTerms> GettermsDetails(TermsmasterRequest termsmaster);
        Termsmasterresponse InsertTermsmaster(TblTerms tblterms);
    }
}
