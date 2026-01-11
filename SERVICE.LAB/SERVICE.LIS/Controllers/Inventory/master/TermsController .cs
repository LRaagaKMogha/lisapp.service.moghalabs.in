using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TermsController : ControllerBase
    {
        private readonly ITermsRepository _TermsRepository;

        public TermsController(ITermsRepository TermsRepository)
        {
            _TermsRepository = TermsRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Terms/GettermsDetails")]
        public IEnumerable<TblTerms> GettermsDetails(TermsmasterRequest getterms)
        {
            List<TblTerms> termsresult = new List<TblTerms>();
            try
            {                
                termsresult = _TermsRepository.GettermsDetails(getterms);                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TermsController.GettermsDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getterms.venueNo, 0, 0);
            }
            return termsresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Terms/InsertTermsmaster")]
        public Termsmasterresponse InsertTermsmaster(TblTerms tblTerms)
        {
            Termsmasterresponse objresult = new Termsmasterresponse();
            try
            {
                objresult = _TermsRepository.InsertTermsmaster(tblTerms);
                string _CacheKey = CacheKeys.CommonMaster + "TERMSMASTER" + tblTerms.venueNo + tblTerms.venuebranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TermsController.InsertTermsmaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblTerms.venueNo, tblTerms.venuebranchno, 0);
            }
            return objresult;
        }
    }
}

