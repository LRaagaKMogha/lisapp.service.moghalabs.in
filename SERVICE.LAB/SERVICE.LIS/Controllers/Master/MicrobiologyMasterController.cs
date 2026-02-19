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
        public List<LstorgAntiRange> GetOrgAntibioticRange(reqorgAntiRange req)
        {
            List<LstorgAntiRange> lst = new List<LstorgAntiRange>();
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
                using(var auditScoped = new AuditScope<LstorgAntiRange>(req.LstorgAntiRange, _auditService))
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
        public List<Orggetresponse> GetOrgmaster(reqorgAntiRange orggetreq)
        {
            List<Orggetresponse> lst = new List<Orggetresponse>();
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
        public List<Orggrpresponse> GetOrgGrpmaster(reqorgGroupAntiRange orggetreq)
        {
            List<Orggrpresponse> lst = new List<Orggrpresponse>();
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
        public ActionResult<Orginsertresponse> InsertOrgmaster(Orgresponse orginsertreq)
        {
            Orginsertresponse Objresult = new Orginsertresponse();
            try
            {
                using (var auditScoped = new AuditScope<Orgresponse>(orginsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertOrgmaster(orginsertreq);
                    if (!_errormsg.status)
                    {
                        Objresult = _MicrobiologyMasterRepository.InsertOrgmaster(orginsertreq);
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
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/InsertOrgGrpmaster")]
        public OrginsertGrpresponse InsertOrgGrpmaster(Orggrpresponse orginsertreq)
        {
            OrginsertGrpresponse Objresult = new OrginsertGrpresponse();
            try
            {
                Objresult = _MicrobiologyMasterRepository.InsertOrgGrpmaster(orginsertreq);
                string _CacheKey = CacheKeys.CommonMaster + "ORGANISMGROUP" + orginsertreq.venueno + orginsertreq.venuebranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.InsertOrgGrpmaster" + orginsertreq.organismgrpno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orginsertreq.venueno, orginsertreq.venuebranchno, orginsertreq.userno);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetOrgtypemaster")]
        public List<Orgtyperesponse> GetOrgtypemaster(Orgtypereq orgtygetreq)
        {
            List<Orgtyperesponse> lst = new List<Orgtyperesponse>();
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
        public ActionResult<Orgtypeinsertresponse> InsertOrgtypemaster(Orgtyperesponse orgtyinsertreq)
        {
            Orgtypeinsertresponse Objresult = new Orgtypeinsertresponse();
            try
            {
                using(var auditScoped = new AuditScope<Orgtyperesponse>(orgtyinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertOrgtypemaster(orgtyinsertreq);
                    if (!_errormsg.status)
                    {
                        Objresult = _MicrobiologyMasterRepository.InsertOrgtypemaster(orgtyinsertreq);
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
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetAntimaster")]
        public List<Antiresponse> GetAntimaster(Antireq Antireq)
        {
            List<Antiresponse> lst = new List<Antiresponse>();
            try
            {
                lst = _MicrobiologyMasterRepository.GetAntimaster(Antireq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.GetAntimaster" + Antireq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, Antireq.venueno, Antireq.venuebranchno, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/Insertantimaster")]
        public ActionResult<Antinsertresponse> Insertantimaster(Antiresponse antinsertreq)
        {
            Antinsertresponse Objresult = new Antinsertresponse();
            try
            {
                using(var auditScoped = new AuditScope<Antiresponse>(antinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.Insertantimaster(antinsertreq);
                    if (!_errormsg.status)
                    {
                        Objresult = _MicrobiologyMasterRepository.Insertantimaster(antinsertreq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.Insertantibioticmaster - " + antinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, antinsertreq.venueno, antinsertreq.venuebranchno, antinsertreq.userno);
            }
            return Ok(Objresult);
        }

        [HttpPost]
        [Route("api/MicrobiologyMaster/GetorgAntimaster")]
        public List<OrgAntiresponse> GetorgAntimaster(OrgAntirequest reqorgAnti)
        {
            List<OrgAntiresponse> lst = new List<OrgAntiresponse>();
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
        public ActionResult<OrgAntinsertresponse> InsertorgAntimaster(OrgAntinsertresponse orgAntinsertreq)
        {
            OrgAntinsertresponse Objresult = new OrgAntinsertresponse();
            try
            {
                using (var auditScoped = new AuditScope<OrgAntinsertresponse>(orgAntinsertreq, _auditService))
                {
                    var _errormsg = MBMasterValidation.InsertorgAntimaster(orgAntinsertreq);
                    if (!_errormsg.status)
                    {
                        Objresult = _MicrobiologyMasterRepository.InsertorgAntimaster(orgAntinsertreq);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterController.Insertantimaster" + orgAntinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, orgAntinsertreq.venueno, orgAntinsertreq.venuebranchno, orgAntinsertreq.userno);
            }
            return Ok(Objresult);
        }
    }
}