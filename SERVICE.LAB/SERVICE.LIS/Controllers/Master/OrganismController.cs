using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
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
        public List<Lstorganism> GetOrganismMaster(Reqsearchorganism req)
        {
            List<Lstorganism> lst = new List<Lstorganism>();
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