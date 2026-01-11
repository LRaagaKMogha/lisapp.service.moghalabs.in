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
    public class MicrobiologyMasterController : ControllerBase
    {
        private readonly IMicrobiologyMasterRepository _MicrobiologyMasterRepository;
        private readonly IAuditService _auditService;
        public MicrobiologyMasterController(IMicrobiologyMasterRepository noteRepository, IAuditService auditService)
        {
            _MicrobiologyMasterRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetOrgAntibioticRange")]
        public List<lstorgAntiRange> GetOrgAntibioticRange(reqorgAntiRange req)
        {
            List<lstorgAntiRange> lst = new List<lstorgAntiRange>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetOrgAntibioticRange(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetOrgAntibioticRange" + req.organismno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }

        #region SaveOrganismAntibioticRange
        [HttpPost]
        [Route("api/MicrobiologyMaster/SaveOrganismAntibioticRange")]
        public ActionResult SaveOrganismAntibioticRange(orgAntiRange req)
        {
            int testno = 0;
            try
            {
                using(var auditScoped = new AuditScope<lstorgAntiRange>(req.lstorgAntiRange, _auditService))
                {
                    var _errormsg = MBMasterValidation.SaveOrganismAntibioticRange(req);
                    if (!_errormsg.status)
                    {
                        testno = _MicrobiologyMasterRepository.SaveOrganismAntibioticRange(req);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.SaveOrganismAntibioticRange", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, req.userno);
            }
            return Ok(testno);
        }
        #endregion

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetOrgmaster")]
        public List<orggetresponse> GetOrgmaster(reqorgAntiRange orggetreq)
        {
            List<orggetresponse> lst = new List<orggetresponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetOrgmaster(orggetreq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetOrgmaster" + orggetreq.organismno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orggetreq.venueno, orggetreq.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetOrgGrpmaster")]
        public List<orgGrpresponse> GetOrgGrpmaster(reqorgGroupAntiRange orggetreq)
        {
            List<orgGrpresponse> lst = new List<orgGrpresponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetOrgGrpmaster(orggetreq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetOrgGrpmaster" + orggetreq.organismGrpno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orggetreq.venueno, orggetreq.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/InsertOrgmaster")]
        public ActionResult<orginsertresponse> InsertOrgmaster(orgresponse orginsertreq)
        {
            orginsertresponse objresult = new orginsertresponse();
            try
            {
                using (var auditScoped = new AuditScope<orgresponse>(orginsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertOrgmaster(orginsertreq);
                    if (!_errormsg.status)
                    {
                        objresult = _MicrobiologyMasterRepository.InsertOrgmaster(orginsertreq);
                        string _CacheKey = CacheKeys.CommonMaster + "ORGANISM" + orginsertreq.venueno + orginsertreq.venuebranchno;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.InsertOrgmaster - " + orginsertreq.organismno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orginsertreq.venueno, orginsertreq.venuebranchno, orginsertreq.userno);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/InsertOrgGrpmaster")]
        public orginsertGrpresponse InsertOrgGrpmaster(orggrpresponse orginsertreq)
        {
            orginsertGrpresponse objresult = new orginsertGrpresponse();
            try
            {
                objresult = _MicrobiologyMasterRepository.InsertOrgGrpmaster(orginsertreq);
                string _CacheKey = CacheKeys.CommonMaster + "ORGANISMGROUP" + orginsertreq.venueno + orginsertreq.venuebranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.InsertOrgGrpmaster" + orginsertreq.organismgrpno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orginsertreq.venueno, orginsertreq.venuebranchno, orginsertreq.userno);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetOrgtypemaster")]
        public List<orgtyperesponse> GetOrgtypemaster(orgtypereq orgtygetreq)
        {
            List<orgtyperesponse> lst = new List<orgtyperesponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetOrgtypemaster(orgtygetreq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetOrgtypemaster" + orgtygetreq.organismtypeno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orgtygetreq.venueno, orgtygetreq.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/InsertOrgtypemaster")]
        public ActionResult<orgtypeinsertresponse> InsertOrgtypemaster(orgtyperesponse orgtyinsertreq)
        {
            orgtypeinsertresponse objresult = new orgtypeinsertresponse();
            try
            {
                using(var auditScoped = new AuditScope<orgtyperesponse>(orgtyinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertOrgtypemaster(orgtyinsertreq);
                    if (!_errormsg.status)
                    {
                        objresult = _MicrobiologyMasterRepository.InsertOrgtypemaster(orgtyinsertreq);
                        string _CacheKey = CacheKeys.CommonMaster + "ORGANISMTYPE" + orgtyinsertreq.venueno + orgtyinsertreq.venuebranchno;
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.InsertOrgtypemaster - " + orgtyinsertreq.organismtypeno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orgtyinsertreq.venueno, orgtyinsertreq.venuebranchno, orgtyinsertreq.userno);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetAntimaster")]
        public List<antiresponse> GetAntimaster(antireq antireq)
        {
            List<antiresponse> lst = new List<antiresponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetAntimaster(antireq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetAntimaster" + antireq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, antireq.venueno, antireq.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/Insertantimaster")]
        public ActionResult<antinsertresponse> Insertantimaster(antiresponse antinsertreq)
        {
            antinsertresponse objresult = new antinsertresponse();
            try
            {
                using(var auditScoped = new AuditScope<antiresponse>(antinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.Insertantimaster(antinsertreq);
                    if (!_errormsg.status)
                    {
                        objresult = _MicrobiologyMasterRepository.Insertantimaster(antinsertreq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.Insertantibioticmaster - " + antinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, antinsertreq.venueno, antinsertreq.venuebranchno, antinsertreq.userno);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetorgAntimaster")]
        public List<orgAntiresponse> GetorgAntimaster(orgAntirequest reqorgAnti)
        {
            List<orgAntiresponse> lst = new List<orgAntiresponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetorgAntimaster(reqorgAnti);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetAntimaster" + reqorgAnti.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, reqorgAnti.venueno, reqorgAnti.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/Insertorgantimaster")]
        public ActionResult<organtinsertresponse> InsertorgAntimaster(orgAntinsertresponse orgAntinsertreq)
        {
            organtinsertresponse objresult = new organtinsertresponse();
            try
            {
                using (var auditScoped = new AuditScope<orgAntinsertresponse>(orgAntinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertorgAntimaster(orgAntinsertreq);
                    if (!_errormsg.status)
                    {
                        objresult = _MicrobiologyMasterRepository.InsertorgAntimaster(orgAntinsertreq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.Insertantimaster" + orgAntinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orgAntinsertreq.venueno, orgAntinsertreq.venuebranchno, orgAntinsertreq.userno);
            }
            return Ok(objresult);
        }
    }
}