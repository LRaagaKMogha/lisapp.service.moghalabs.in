using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MainDepartmentController : ControllerBase
    {
        private readonly IMainDepartmentRepository _MainDepartmentRepository;
        private readonly IAuditService _auditService;
        
        public MainDepartmentController(IMainDepartmentRepository MainRepository, IAuditService auditService)
        {
            _MainDepartmentRepository = MainRepository;
            _auditService = auditService;            
        }

        [HttpPost]
        [Route("api/MainDepartment/GetMainDepartmentDetails")]
        public IEnumerable<TblMainDepartment> GetMainDepartmentDetails(MainDepartmentmasterRequest getMaindept)
        {
            List<TblMainDepartment> Maindeptresult = new List<TblMainDepartment>();
            try
            {
                Maindeptresult = _MainDepartmentRepository.GetMainDepartmentDetails(getMaindept);
                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MainDepartmentController.GetMainDepartmentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getMaindept.venueno,0,0);
            }
            return Maindeptresult;
        }

        [HttpPost]
        [Route("api/MainDepartment/InsertMainDepartmentmaster")]
        public ActionResult<MainDepartmentMasterResponse> InsertMainDepartmentmaster(TblMainDepartment tblmaindepartment)
        {
            MainDepartmentMasterResponse objresult = new MainDepartmentMasterResponse();
            try
            {
                using (var auditScope = new AuditScope<TblMainDepartment>(tblmaindepartment, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertMainDepartmentmaster(tblmaindepartment);
                    if (!_errormsg.status)
                    {
                        objresult = _MainDepartmentRepository.InsertMainDepartmentmaster(tblmaindepartment);

                        string _CacheKey = CacheKeys.CommonMaster + "MAINDEPARTMENT" + tblmaindepartment.venueno + tblmaindepartment.venuebranchno;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MainDepartmentController.InsertMainDepartmentmaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblmaindepartment.venueno, tblmaindepartment.venuebranchno, 0);
            }
            return Ok(objresult);
        }
    }
}