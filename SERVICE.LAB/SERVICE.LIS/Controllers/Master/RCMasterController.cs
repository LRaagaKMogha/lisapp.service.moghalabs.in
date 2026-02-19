using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/rcmaster/")]
    public class RCMasterController : ControllerBase
    {
        private readonly IRCMasterRepository _rcMasterRepository;
        public RCMasterController(IRCMasterRepository rcMasterRepository)
        {
            _rcMasterRepository = rcMasterRepository;
        }

        #region Get RC Details
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("getdetails")]
        public IEnumerable<GetRCMasterResponse> GetRCMaster(int venueNo, int venueBranchNo, int pageIndex, int RcNo)
        {
            List<GetRCMasterResponse> Objresult = new List<GetRCMasterResponse>();
            try
            {
                Objresult = _rcMasterRepository.GetRCDetails(venueNo, venueBranchNo, pageIndex, RcNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterController.GetRCMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion

        #region Get RCMaster Details
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("getrcdetails")]
        public IEnumerable<TblRC> GetRCMasterDetails(GetCommonMasterRequest masterRequest)
        {
            List<TblRC> Objresult = new List<TblRC>();
            try
            {
                Objresult = _rcMasterRepository.GetRCMasterDetails(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterController.GetRCMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueno, masterRequest.venuebranchno, 0);
            }
            return Objresult;
        }
        #endregion

        #region Edit RC Master
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("edit")]
        public IEnumerable<RCPriceList> GetEditRCMaster(int venueNo, int venueBranchNo, int rcNo)
        {
            List<RCPriceList> Objresult = new List<RCPriceList>();
            try
            {
                Objresult = _rcMasterRepository.GetEditRCMaster(venueNo, venueBranchNo, rcNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterController.GetEditRCMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion

        #region Insert RC 
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("save")]        
        public IActionResult InsertRCMaster([FromBody] InsertRCMasterRequest rCMasterRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            try
            {
                result = _rcMasterRepository.InsertRCMaster(rCMasterRequest);
                MemoryCacheRepository.RemoveItem(CacheKeys.TariffMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "RCMasterController.InsertRCMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                return BadRequest(ex);
            }
            return Ok(result);
        }
        #endregion
    }
}