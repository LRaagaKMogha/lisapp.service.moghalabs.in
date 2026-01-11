using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorMasterRepository _VendorMasterRepository;
        private readonly IAuditService _AuditService;
        public VendorController(IVendorMasterRepository noteRepository, IAuditService auditService)
        {
            _VendorMasterRepository = noteRepository;
            _AuditService = auditService;
        }

        [HttpPost]
        [Route("api/VendorMaster/GetVendorMaster")]
        public List<responsegetvendor> GetVendorMaster(requestvendor req)
        {
            List<responsegetvendor> lst = new List<responsegetvendor>();
            try
            {
                lst = _VendorMasterRepository.GetVendorMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.GetVendorMaster" + req.vendorno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        
        [HttpPost]
        [Route("api/VendorMaster/InsertVendorMaster")]
        public ActionResult<StoreVendorMaster> InsertVendorMaster(responsevendor obj1)
        {
            StoreVendorMaster objresult = new StoreVendorMaster();
            try
            {
                using(var auditScoped = new AuditScope<responsevendor>(obj1, _AuditService))
                {
                    var _errormsg = VendorMasterValidation.InsertVendorMaster(obj1);
                    if (!_errormsg.status)
                    {
                        objresult = _VendorMasterRepository.InsertVendorMaster(obj1);
                        string _CacheKey = CacheKeys.CommonMaster + "VENDOR" + obj1.venueno + obj1.venuebranchno;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.InsertVendorMaster" + obj1.vendorno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, obj1.venueno, obj1.venuebranchno, 0);
            }
            return Ok(objresult);
        }
        
        [HttpPost]
        [Route("api/VendorMaster/GetVendorvsContactmaster")]
        public List<getcontactlst> GetVendorvsContactmaster(getcontact creq)
        {
            List<getcontactlst> Contactlst = new List<getcontactlst>();
            try
            {
                Contactlst = _VendorMasterRepository.GetVendorvsContactmaster(creq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.GetVendorvsContactmaster" + creq.vendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq.venueno, 0, 0);
            }
            return Contactlst;
        }
        
        [HttpPost]
        [Route("api/VendorMaster/InsertVendorContactmaster")]
        public ActionResult InsertVendorContactmaster(savecontact creq1)
        {
            int VendorContactNo = 0;
            try
            {
                using(var auditScoped = new AuditScope<getcontactlst>(creq1.getcontactlst, _AuditService))
                {
                    var _errormsg = VendorMasterValidation.InsertVendorContactmaster(creq1);
                    if (!_errormsg.status)
                    {
                        VendorContactNo = _VendorMasterRepository.InsertVendorContactmaster(creq1);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.InsertVendorContactmaster - " + creq1.vendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq1.venueno, creq1.userNo, 0);
            }
            return Ok(VendorContactNo);
        }
       
        [HttpPost]
        [Route("api/VendorMaster/GetVendorvsServices")]
        public List<getservicelst> GetVendorvsservices(getservice sobj)
        {
            List<getservicelst> servicelst = new List<getservicelst>();
            try
            {
                servicelst = _VendorMasterRepository.GetVendorvsservices(sobj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.GetVendorvsServices" + sobj.VendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, sobj.VenueNo, 0, 0);
            }
            return servicelst;
        }
        
        [HttpPost]
        [Route("api/VendorMaster/InsertVendorVsServices")]
        public ActionResult InsertVendorService(saveservice serviceobj)
        {
            int VendorServiceNo = 0;
            try
            {
                using(var auditScoped = new AuditScope<getservicelst>(serviceobj.getservicelst, _AuditService))
                {
                    var _errormsg = VendorMasterValidation.InsertVendorService(serviceobj);
                    if (!_errormsg.status)
                    {
                        VendorServiceNo = _VendorMasterRepository.InsertVendorService(serviceobj);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorController.InsertVendorService - " + serviceobj.VendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, serviceobj.venueno, serviceobj.userNo, 0);
            }
            return Ok(VendorServiceNo);
        }
    }
}