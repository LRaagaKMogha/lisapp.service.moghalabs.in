using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TariffMasterController : ControllerBase
    {
        private readonly ITariffMasterRepository _tariffMasterRepository;
        private readonly IAuditService _auditService;
        public TariffMasterController(ITariffMasterRepository tariffMasterRepository, IAuditService auditService)
        {
            _tariffMasterRepository = tariffMasterRepository;
            _auditService = auditService;
        }

        #region Get TariffMaster Details
        /// <summary>
        /// Get TariffMaster Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetTariffMasterDetails")]
        public IEnumerable<GetTariffMasterResponse> GetTariffMasterDetails(GetTariffMasterRequest getRequest)
        {
            List<GetTariffMasterResponse> objresult = new List<GetTariffMasterResponse>();
            try
            {

                objresult = _tariffMasterRepository.GetTariffMasterDetails(getRequest);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion

        #region Insert TariffMaster 
        /// <summary>
        /// Insert TariffMaster 
        /// </summary>
        /// <param name="TariffMasteritem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/TariffMaster/InsertTariffMasterDetails")]
        
        public ActionResult<InsertTariffMasterResponse> InsertTariffMasterDetails([FromBody] InsertTariffMasterRequest tariffMasteritem)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            try
            {
                var _errormsg = TariffMasterValidation.InsertTariffMasterDetails(tariffMasteritem);
                if (!_errormsg.status)
                {
                    result = _tariffMasterRepository.InsertTariffMasterDetails(tariffMasteritem);
                    MemoryCacheRepository.RemoveItem(CacheKeys.TariffMaster);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(result);
        }
        #endregion

        #region Get TariffService Details
        /// <summary>
        /// Get TariffMaster Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetServiceDetails")]
        public IEnumerable<GetServices> GetServiceDetails(GetTariffMasterRequest getRequest)
        {
            List<GetServices> objresult = new List<GetServices>();
            try
            {
                objresult = _tariffMasterRepository.GetTariffService(getRequest);                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion

        #region Get TariffMaster List
        /// <summary>
        /// Get TariffMaster List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetTariffMasterList")]
        public IEnumerable<GetTariffMasterListResponse> GetTariffMasterList(GetTariffMasterListRequest getRequest)
        {
            List<GetTariffMasterListResponse> objresult = new List<GetTariffMasterListResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetTariffMasterList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        #endregion

        #region Get Tariff Master Service Details
        /// <summary>
        /// Get Tariff Master Service Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetTariffMasterServiceList")]
        public IEnumerable<TariffMastServicesResponse> GetTariffMasterServiceList(GetTariffMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetTariffMasterServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - TariffMasterServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        #endregion

        #region Insert TariffMaster 
        /// <summary>
        /// Insert TariffMaster 
        /// </summary>
        /// <param name="TariffMasteritem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/TariffMaster/InsertTariffMaster")]
        public ActionResult<TariffMasterInsertResponse> InsertTariffMaster([FromBody] InsertTariffMasterRequest tariffMasteritem)
        {
            TariffMasterInsertResponse result = new TariffMasterInsertResponse();
            try
            {
                var _errormsg = TariffMasterValidation.InsertTariffMaster(tariffMasteritem);
                if (!_errormsg.status)
                {
                    string _CacheKey = CacheKeys.CommonMaster + "PRICELIST" + tariffMasteritem.VenueNo + tariffMasteritem.VenueBranchNo;
                    result = _tariffMasterRepository.InsertTariffMaster(tariffMasteritem);
                    MemoryCacheRepository.RemoveItem(CacheKeys.TariffMaster);
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertTariffMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Ok(result);
        }
        #endregion

        #region Get TariffMaster Details
        /// <summary>
        /// Get TariffMaster Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetClientTariffMasterList")]
        public IEnumerable<GetClientTariffMasterListResponse> GetClientTariffMasterList(GetClientTariffMasterRequest getRequest)
        {
            List<GetClientTariffMasterListResponse> objresult = new List<GetClientTariffMasterListResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetClientTariffMasterList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetClientTariffMasterList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        #endregion

        #region Insert Client TariffMaster 
        /// <summary>
        /// Insert Client Tariff Master 
        /// </summary>
        /// <param name="TariffMasteritem"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/TariffMaster/InsertClientTariffMaster")]
        public CTMInsertResponse InsertClientTariffMaster([FromBody] InsertCTMRequest tariffMasteritem)
        {
            CTMInsertResponse result = new CTMInsertResponse();
            try
            {
                result = _tariffMasterRepository.InsertClientTariffMaster(tariffMasteritem);
                MemoryCacheRepository.RemoveItem(CacheKeys.TariffMaster);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertClientTariffMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, tariffMasteritem.VenueNo, tariffMasteritem.VenueBranchNo, 0);
            }
            return result;
        }
        #endregion

        #region Get Client Tariff Master Service Details
        /// <summary>
        /// Get Tariff Master Service Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/TariffMaster/GetClientTariffServiceList")]
        public IEnumerable<ClientTariffServicesResponse> GetClientTariffServiceList(GetClientTariffMasterRequest getRequest)
        {
            List<ClientTariffServicesResponse> objresult = new List<ClientTariffServicesResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetClientTariffServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientTariff - ServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/TariffMaster/GetTariffupdateList")]
        public GetTariffupdateResponse GetTariffupdateList(GetTariffupdateRequest getRequest)
        {
            GetTariffupdateResponse objresult = new GetTariffupdateResponse();
            try
            {
                objresult = _tariffMasterRepository.GetTariffupdateList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientTariff - ServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return objresult;
        }
        #endregion
        
        [HttpPost]
        [Route("api/TariffMaster/GetContractMaster")]
        public IEnumerable<GetContractRes> GetContractMaster(GetContractReq req)
        {
            List<GetContractRes> objresult = new List<GetContractRes>();
            try
            {
                objresult = _tariffMasterRepository.GetContractMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetContractMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo,0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/TariffMaster/InserContractMaster")]
        public ActionResult<InsertContractRes> InserContractMaster([FromBody] InsertContractReq req)
        {
            InsertContractRes result = new InsertContractRes();
            try
            {
                var _errormsg = CommercialMasterValidation.InserContractMaster(req);
                if (!_errormsg.status)
                {
                    string _CacheKey = CacheKeys.CommonMaster + "CONTRACTMASTER" + req.VenueNo + req.VenueBranchNo;
                    result = _tariffMasterRepository.InserContractMaster(req);
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InserContractMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetContractMasterServiceList")]
        public IEnumerable<TariffMastServicesResponse> GetContractMasterServiceList(GetContractMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetContractMasterServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetContractMasterServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetContractVsClient")]
        public IEnumerable<ContractVsCustomerMap> GetContractVsClient(GetContractVsClientReq getRequest)
        {
            List<ContractVsCustomerMap> objresult = new List<ContractVsCustomerMap>();
            try
            {
                objresult = _tariffMasterRepository.GetContractVsClient(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetContractMasterServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.VenueNo, getRequest.ContractNo,0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/InsertClienttTariffMap")]
        public ActionResult<InsTariffRes> InsertClienttTariffMap([FromBody] InsTariffReq req)
        {
            InsTariffRes result = new InsTariffRes();
            try
            {
                using(var auditScoped = new AuditScope<InsTariffReq>(req, _auditService))
                {
                    var _errormsg = TariffMasterValidation.InsertClienttTariffMap(req);
                    if (!_errormsg.status)
                    {
                        result = _tariffMasterRepository.InsertClienttTariffMap(req);
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterController.InsertReferrerTariffMap", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetClienttTariffMap")]
        public IEnumerable<GetTariffRes> GetClienttTariffMap (GetTariffReq req)
        {
            List<GetTariffRes> objresult = new List<GetTariffRes>();
            try
            {
                objresult = _tariffMasterRepository.GetClienttTariffMap(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetClienttTariffMap", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetRefSplRateServiceList")]
        public IEnumerable<TariffMastServicesResponse> GetRefSplRateServiceList(GetContractMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetRefSplRateServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetRefSplRateServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetReflst")]
        public IEnumerable<GetReflstRes> GetReflst(GetContractReq req)
        {
            List<GetReflstRes> objresult = new List<GetReflstRes>();
            try
            {
                objresult = _tariffMasterRepository.GetReflst(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetReflst", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/InsertReferrerlst")]
        public ActionResult<InsertContractRes> InsertReferrerlst([FromBody] InsertReflstReq req)
        {
            InsertContractRes result = new InsertContractRes();
            try
            {
                var _errormsg = CommercialMasterValidation.InsertReferrerlst(req);
                if (!_errormsg.status)
                {
                    result = _tariffMasterRepository.InsertReferrerlst(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertReferrerlst", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetTariffDeptDiscount")]
        public IEnumerable<Tariffdeptdisreq> GetTariffDeptDiscount(Tariffdeptdis req)
        {
            List<Tariffdeptdisreq> objresult = new List<Tariffdeptdisreq>();
            try
            {
                objresult = _tariffMasterRepository.GetTariffDeptDiscount(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffDeptDiscount", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.RateListNo, req.IsApproval);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/CommercialMaster/PriceHistoryService")]
        public RateHistoryServiceResponse PriceHistoryService(RateHistoryServiceRequest req)
        {
            RateHistoryServiceResponse objresult = new RateHistoryServiceResponse();
            try
            {
                objresult = _tariffMasterRepository.GetPriceHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PriceHistoryService", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/CommercialMaster/GetBasePrice")]
        public List<BaseRateResponse> GetBasePrice(RateHistoryServiceRequest req)
        {
            List<BaseRateResponse> objresult = new List<BaseRateResponse>();
            try
            {
                objresult = _tariffMasterRepository.GetBasePrice(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetBasePrice", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }
        
        [HttpPost]
        [Route("api/CommercialMaster/InsertBaseRate")]
        public int InsertBaseRate(List<BaseRateResponse> req)
        {
            int objresult = 0;
            try
            {
                objresult = _tariffMasterRepository.InsertBaseRate(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertBaseRate", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }        
    }
}