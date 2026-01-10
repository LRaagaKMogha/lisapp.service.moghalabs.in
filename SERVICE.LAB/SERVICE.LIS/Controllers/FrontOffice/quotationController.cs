using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IquotationRepository _quotationRepository;
        public QuotationController(IquotationRepository noteRepository)
        {
            _quotationRepository = noteRepository;
        }
        [HttpPost]
        [Route("api/servicequotation/Getquotation")]
        public List<returnquotationlst> Getquotation(requestquotation req)
        {
            List<returnquotationlst> lst = new List<returnquotationlst>();
            try
            {
                lst = _quotationRepository.Getquotation(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "quotationRepository.Getquotation" + req.quotationMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        [HttpPost]
        [Route("api/servicequotation/Insertquotation")]
        public int Insertquotation(responselst req1)
        {
            int quotationOrderListNo = 0;
            try
            {
                quotationOrderListNo = _quotationRepository.Insertquotation(req1);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "quotationRepository.Insertquotation" + req1.quotationMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req1.venueno, req1.venuebranchno, req1.userno);
            }
            return quotationOrderListNo;
        }
    }
}