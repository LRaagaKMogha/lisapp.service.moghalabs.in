using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace Service.API.SERVICE.Controllers
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
        public List<Responsegetvendor> GetVendorMaster(Requestvendor req)
        {
            List<Responsegetvendor> lst = new List<Responsegetvendor>();
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
        public ActionResult<StoreVendorMaster> InsertVendorMaster(Responsevendor obj1)
        {
            StoreVendorMaster Objresult = new StoreVendorMaster();
            try
            {
                using(var auditScoped = new AuditScope<Responsevendor>(obj1, _AuditService))
                {
                    var _errormsg = VendorMasterValidation.InsertVendorMaster(obj1);
                    if (!_errormsg.status)
                    {
                        Objresult = _VendorMasterRepository.InsertVendorMaster(obj1);
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
            return Ok(Objresult);
        }
        
        [HttpPost]
        [Route("api/VendorMaster/GetVendorvsContactmaster")]
        public List<Getcontactlst> GetVendorvsContactmaster(Getcontact creq)
        {
            List<Getcontactlst> Contactlst = new List<Getcontactlst>();
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
        public ActionResult InsertVendorContactmaster(Savecontact creq1)
        {
            int VendorContactNo = 0;
            try
            {
                using(var auditScoped = new AuditScope<Getcontactlst>(creq1.Getcontactlst, _AuditService))
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
        public List<Getservicelst> GetVendorvsservices(Getservice sobj)
        {
            List<Getservicelst> servicelst = new List<Getservicelst>();
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
        public ActionResult InsertVendorService(Saveservice serviceobj)
        {
            int VendorServiceNo = 0;
            try
            {
                using(var auditScoped = new AuditScope<Getservicelst>(serviceobj.Getservicelst, _AuditService))
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