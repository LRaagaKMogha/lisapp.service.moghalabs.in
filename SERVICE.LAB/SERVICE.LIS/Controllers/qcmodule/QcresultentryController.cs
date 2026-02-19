using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
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
            List<GetTblqcresult> Objresult = new List<GetTblqcresult>();
            try
            {
                Objresult = _QcresultentryRepository.GetqcresultDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcresultRepository.GetqcresultDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/Qcresult/InsertqcresultDetails")]
        public QcresultResponse InsertqcresultDetails(SaveqcresDTO req)
        {
            QcresultResponse Objresult = new QcresultResponse();
            try
            {
                Objresult = _QcresultentryRepository.InsertqcresultDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcresultRepository.InsertqcresultDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return Objresult;
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