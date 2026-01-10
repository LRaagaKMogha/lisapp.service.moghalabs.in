using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers.Archive
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {

        private readonly IArchiveRepository _ArchiveRepository;

        public ArchiveController(IArchiveRepository ArchiveRepository)
        {
            _ArchiveRepository = ArchiveRepository;
        }
        #region CommonSearch 
        [HttpPost]
        [Route("api/Archive/ArchivePatientSearch")]
        public List<LstSearch> ArchivePatientSearch(RequestCommonSearch req)
        {
            List<LstSearch> lst = new List<LstSearch>();
            try
            {
                lst = _ArchiveRepository.ArchivePatientSearch(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ArchiveController.ArchivePatientSearch", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        [HttpPost]
        [Route("api/Archive/GetArchivePatientDetails")]
        public List<GetArchivePatientResponse> GetArchivePatientDetails(GetArchivePatientRequest req)
        {
            List<GetArchivePatientResponse> lst = new List<GetArchivePatientResponse>();
            try
            {
                lst = _ArchiveRepository.GetArchivePatientDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ArchiveController.GetArchivePatientDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }
    }
}