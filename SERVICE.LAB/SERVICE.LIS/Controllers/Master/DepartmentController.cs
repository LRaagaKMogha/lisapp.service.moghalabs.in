using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IAuditService _auditService;
        public DepartmentController(IDepartmentRepository noteRepository, IAuditService auditService)
        {
            _DepartmentRepository = noteRepository;
            _auditService = auditService;
        }

        #region Get Department Details
        /// <summary>
        /// Get Department Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Department/GetDepartmentDetails")]
        public IEnumerable<TblDepartment> GetDepartmentDetails(GetCommonMasterRequest getCommonMaster)
        {
            List<TblDepartment> objresult = new List<TblDepartment>();
            try
            {
                objresult = _DepartmentRepository.GetDepartmentDetails(getCommonMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDepartmentDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, getCommonMaster.venueno, getCommonMaster.venuebranchno, 0);
            }
            return objresult;
        }

        #endregion

        #region Insert Department 
        /// <summary>
        /// Insert Department 
        /// </summary>
        /// <param name="Departmentitem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/Department/InsertDepartmentDetails")]
        public ActionResult InsertDepartmentDetails([FromBody] TblDepartment Departmentitem)
        {
            int result = 0;
            try
            {
                using (var auditScoped = new AuditScope<TblDepartment>(Departmentitem, _auditService))
                {
                    var _errormsg = LaboratoryMasterValidation.InsertDepartmentDetails(Departmentitem);
                    if (!_errormsg.status)
                    {
                        string _CacheKey = CacheKeys.CommonMaster + "DEPARTMENT" + Departmentitem.VenueNo + Departmentitem.VenueBranchNo;
                        _DepartmentRepository.InsertDepartmentDetails(Departmentitem);
                        MemoryCacheRepository.RemoveItem(CacheKeys.DepartmentMaster);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertDepartmentDetails-", ExceptionPriority.Low, ApplicationType.APPSERVICE, Departmentitem.VenueNo, Departmentitem.VenueBranchNo, 0);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Department/GetMaindepartmentdetail")]
        public IEnumerable<GetMaindepartment> GetMaindepartmentdetail(GetDeptMasterRequest getCommonMaster)
        {
            List<GetMaindepartment> objresult = new List<GetMaindepartment>();
            try
            {
                objresult = _DepartmentRepository.GetMaindepartmentdetail(getCommonMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetMaindepartment-", ExceptionPriority.Low, ApplicationType.APPSERVICE, getCommonMaster.venueno, getCommonMaster.venuebranchno, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Department/InsertLangCodeDeptMaster")]
        public IEnumerable<DepartMentLangCodeRes> InsertLangCodeDeptMaster(DepartMentLangCodeReq req)
        {
            List<DepartMentLangCodeRes> objresult = new List<DepartMentLangCodeRes>();
            try
            {
                objresult = _DepartmentRepository.InsertLangCodeDeptMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertLangCodeDeptMaster-", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Department/GetLangCodeDeptMaster")]
        public IEnumerable<GetDeptLangCodeRes> GetLangCodeDeptMaster(GetDeptLangCodeReq req)
        {
            List<GetDeptLangCodeRes> objresult = new List<GetDeptLangCodeRes>();
            try
            {
                objresult = _DepartmentRepository.GetLangCodeDeptMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetMaindepartment-", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }
        #endregion
    }
}