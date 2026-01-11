using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
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
            List<GetIPSettingResponse> objresult = new List<GetIPSettingResponse>();
            try
            {
                objresult = _ipSettingMasterRepository.GetIpSettings(venueNo, venueBranchNo, pageIndex, IPSettingNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion

        #region GetEditIpSettings
        [CustomAuthorize("LIMSMasters")]
        [HttpGet]
        [Route("edit")]
        public IEnumerable<RCPriceList> GetEditIpSettings(int venueNo, int venueBranchNo, int physicianNo, int rcNo)
        {
            List<RCPriceList> objresult = new List<RCPriceList>();
            try
            {
                objresult = _ipSettingMasterRepository.GetEditIpSettings(venueNo, venueBranchNo, physicianNo, rcNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetEditIpSettings", ExceptionPriority.Low, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return objresult;
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