using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers.Archive
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
        [Route("api/Archive/GetArchivePatientdetails")]
        public List<GetArchivePatientResponse> GetArchivePatientdetails(GetArchivePatientRequest req)
        {
            List<GetArchivePatientResponse> lst = new List<GetArchivePatientResponse>();
            try
            {
                lst = _ArchiveRepository.GetArchivePatientdetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ArchiveController.GetArchivePatientdetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }
    }
}