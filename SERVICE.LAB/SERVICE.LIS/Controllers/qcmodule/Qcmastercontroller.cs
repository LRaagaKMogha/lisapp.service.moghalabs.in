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
    public class QcmasterController : ControllerBase
    {
        private readonly IQcmasterRepository _QcmasterRepository;

        public QcmasterController(IQcmasterRepository noteRepository)
        {
            _QcmasterRepository = noteRepository;

        }
        [HttpPost]
        [Route("api/Qcmaster/GetqcmasterDetails")]
        public List<GetTblqcmaster> GetqcmasterDetails(qcmasterRequest qcmaster)
        {
            List<GetTblqcmaster> qcresult = new List<GetTblqcmaster>();
            try
            {
                qcresult = _QcmasterRepository.GetqcmasterDetails(qcmaster);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcmasterRepository.GetqcmasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, qcmaster.venueNo, 0, 0);
            }
            return qcresult;
        }

        [HttpPost]
        [Route("api/Qcmaster/InsertqcmasterDetails")]
        public QcMasterResponse InsertqcmasterDetails(saveqcDTO req)
        {
            QcMasterResponse objresult = new QcMasterResponse();
            try
            {
                objresult = _QcmasterRepository.InsertqcmasterDetails(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "QcmasterRepository.InsertqcmasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/Qcmaster/EditqcmasterDetails")]
        public saveqcDTO editqcmasterDetails(EditqcDTO req)
        {
            saveqcDTO lstv = new saveqcDTO();
            try
            {
                lstv = _QcmasterRepository.editqcmasterDetails(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.editqcmasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return lstv;
        }

        [HttpPost]
        [Route("api/Qcmaster/Getqclot")]
        public List<Qclotresponse> Getqclot(Qclotreq req)
        {
            List<Qclotresponse> objlot = new List<Qclotresponse>();
            try
            {
                objlot = _QcmasterRepository.Getqclot(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.Getqclot", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objlot;
        }
        [HttpPost]
        [Route("api/Qcmaster/Getqclevel")]
        public List<Qclevelresponse> Getqclevel(Qclevelreq req)
        {
            List<Qclevelresponse> objlevel = new List<Qclevelresponse>();
            try
            {
                objlevel = _QcmasterRepository.Getqclevel(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.Getqclevel", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objlevel;
        }
        [HttpPost]
        [Route("api/Qcmaster/Getqclowhighvalue")]
        public List<Qclowhighresponse> Getqclowhighvalue(Qclowhighreq req)
        {
            List<Qclowhighresponse> objvalue = new List<Qclowhighresponse>();
            try
            {
                objvalue = _QcmasterRepository.Getqclowhighvalue(req);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, " QcmasterRepository.Getqclowhighvalue", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, 0, 0);
            }
            return objvalue;
        }




    }
}