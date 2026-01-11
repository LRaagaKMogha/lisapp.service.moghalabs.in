using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;
using System.Xml;
using RtfPipe.Tokens;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
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
            CommericalInsRes objresult = new CommericalInsRes();
            try
            {
                using(var auditScoped = new AuditScope<CommericalInsReq>(insReq, _auditService))
                {
                    var _errormsg = CommercialMasterValidation.Insertcompanymaster(insReq);
                    if (!_errormsg.status)
                    {
                        objresult = _commericalRepository.Insertcompanymaster(insReq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.Insertcompanymaster - " + insReq.CompanyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, insReq.venueBranchno, 0);
            }
            return Ok(objresult);
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
            GSTInsRes objresult = new GSTInsRes();
            try
            {
                using(var auditScoped = new AuditScope<GSTInsReq>(insReq, _auditService)) 
                {
                    var _errormsg = CommercialMasterValidation.InsertGSTMaster(insReq);
                    if (!_errormsg.status)
                    {
                        objresult = _commericalRepository.InsertGSTMaster(insReq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalController.InsertGSTMaster - " + insReq.TaxMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, 0, 0);
            }
            return Ok(objresult);
        }
    }
}