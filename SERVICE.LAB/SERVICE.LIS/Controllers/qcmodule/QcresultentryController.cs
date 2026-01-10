using Dev.IRepository;
using DEV.Common;
using DEV.Model;
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
    public class QcresultentryController : ControllerBase
    {
        private readonly IQcresultentryRepository _QcresultentryRepository;

        public QcresultentryController(IQcresultentryRepository noteRepository)
        {
            _QcresultentryRepository = noteRepository;

        }


        [HttpPost]
        [Route("api/Qcresult/GetqcresultDetails")]
        public List<GetTblqcresult> GetqcresultDetails(QcresultRequest req)
        {
            List<GetTblqcresult> objresult = new List<GetTblqcresult>();
            try
            {
                objresult = _QcresultentryRepository.GetqcresultDetails(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcresultRepository.GetqcresultDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Qcresult/InsertqcresultDetails")]
        public QcresultResponse InsertqcresultDetails(SaveqcresDTO req)
        {
            QcresultResponse objresult = new QcresultResponse();
            try
            {
                objresult = _QcresultentryRepository.InsertqcresultDetails(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcresultRepository.InsertqcresultDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Qcresult/EditqcresultDetails")]
        public SaveqcresDTO EditqcresultDetails(EditqcresDTO req)
        {
            SaveqcresDTO lstv = new SaveqcresDTO();
            try
            {
                lstv = _QcresultentryRepository.EditqcresultDetails(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcresultRepository.EditqcresultDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lstv;
        }



    }
}