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
    public class OrganismController : ControllerBase
    {
        private readonly IOrganismRepository _OrganismRepository;       
        public OrganismController(IOrganismRepository noteRepository)
        {
            _OrganismRepository = noteRepository;
        }

        #region GetOrganismMaster
        [HttpPost]
        [Route("api/Organism/GetOrganismMaster")]
        public List<lstorganism> GetOrganismMaster(reqsearchorganism req)
        {
            List<lstorganism> lst = new List<lstorganism>();
            try
            {
                lst = _OrganismRepository.GetOrganismMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OrganismController.GetOrganismMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        
    }
}