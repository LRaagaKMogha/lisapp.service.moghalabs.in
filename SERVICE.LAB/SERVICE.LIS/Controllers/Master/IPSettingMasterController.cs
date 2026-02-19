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
    [Route("api/ipsetting/")]
    public class IPSettingMasterController : ControllerBase
    {
        private readonly IIPSettingMasterRepository _ipSettingMasterRepository;
        public IPSettingMasterController(IIPSettingMasterRepository ipSettingMasterRepository)
        {
            _ipSettingMasterRepository = ipSettingMasterRepository;
        }

        #region Get IpSetting Details
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("getdetails")]
        public IEnumerable<GetIPSettingResponse> GetIpSettings(int venueNo, int venueBranchNo, int pageIndex, int IPSettingNo)
        {
            List<GetIPSettingResponse> Objresult = new List<GetIPSettingResponse>();
            try
            {
                Objresult = _ipSettingMasterRepository.GetIpSettings(venueNo, venueBranchNo, pageIndex, IPSettingNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion

        #region GetEditIpSettings
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("edit")]
        public IEnumerable<RCPriceList> GetEditIpSettings(int venueNo, int venueBranchNo, int physicianNo, int rcNo)
        {
            List<RCPriceList> Objresult = new List<RCPriceList>();
            try
            {
                Objresult = _ipSettingMasterRepository.GetEditIpSettings(venueNo, venueBranchNo, physicianNo, rcNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetEditIpSettings", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return Objresult;
        }

        #endregion

        #region Insert IPSetting 
        [CustomAuthorize("LIMSMasters")]
        [HttpPost]
        [Route("save")]
        public InsertTariffMasterResponse InsertTariffMasterDetails([FromBody] List<IPSettingRequest> ipSettingRequest)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            try
            {
                result = _ipSettingMasterRepository.InsertIpSettingMasterDetails(ipSettingRequest);
                MemoryCacheRepository.RemoveItem(CacheKeys.TariffMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "IPSettingMasterController.InsertTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        #endregion

    }
}