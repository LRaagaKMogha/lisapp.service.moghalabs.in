using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Service.IRepository;
using Service.Common;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CommericalController : ControllerBase
    {
        private readonly ICommericalRepository _commericalRepository;
        private readonly IAuditService _auditService;
        public CommericalController(ICommericalRepository noteRepository, IAuditService auditService)
        {
            _commericalRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Commerical/Getcompanymaster")]
        public List<CommericalGetRes> Getcompanymaster(CommericalGetReq getReq)
        {
            List<CommericalGetRes> result = new List<CommericalGetRes>();
            try
            {
                result = _commericalRepository.Getcompanymaster(getReq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.Getcompanymaster" + getReq.CompanyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.venueNo,0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Commerical/Insertcompanymaster")]
        public ActionResult<CommericalInsRes> Insertcompanymaster(CommericalInsReq insReq)
        {
            CommericalInsRes Objresult = new CommericalInsRes();
            try
            {
                using(var auditScoped = new AuditScope<CommericalInsReq>(insReq, _auditService))
                {
                    var _errormsg = CommercialMasterValidation.Insertcompanymaster(insReq);
                    if (!_errormsg.status)
                    {
                        Objresult = _commericalRepository.Insertcompanymaster(insReq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.Insertcompanymaster - " + insReq.CompanyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, insReq.venueBranchno, 0);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/Commerical/GetGSTMaster")]
        public List<GSTGetRes> GetGSTMaster(GSTGetReq getReq)
        {
            List<GSTGetRes> result = new List<GSTGetRes>();
            try
            {
                result = _commericalRepository.GetGSTMaster(getReq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.GetGSTMaster" + getReq.TaxMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.VenueNo, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Commerical/InsertGSTMaster")]
        public ActionResult<GSTInsRes> InsertGSTMaster(GSTInsReq insReq)
        {
            GSTInsRes Objresult = new GSTInsRes();
            try
            {
                using(var auditScoped = new AuditScope<GSTInsReq>(insReq, _auditService)) 
                {
                    var _errormsg = CommercialMasterValidation.InsertGSTMaster(insReq);
                    if (!_errormsg.status)
                    {
                        Objresult = _commericalRepository.InsertGSTMaster(insReq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.InsertGSTMaster - " + insReq.TaxMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, 0, 0);
            }
            return Ok(Objresult);
        }
    }
}