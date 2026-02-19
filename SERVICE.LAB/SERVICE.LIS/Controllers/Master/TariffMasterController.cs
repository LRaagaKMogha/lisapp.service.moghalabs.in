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
            List<GetTariffMasterResponse> Objresult = new List<GetTariffMasterResponse>();
            try
            {

                Objresult = _tariffMasterRepository.GetTariffMasterDetails(getRequest);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return Objresult;
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
        [Route("api/TariffMaster/GetserviceDetails")]
        public IEnumerable<Getservices> GetserviceDetails(GetTariffMasterRequest getRequest)
        {
            List<Getservices> Objresult = new List<Getservices>();
            try
            {
                Objresult = _tariffMasterRepository.GetTariffService(getRequest);                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetserviceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return Objresult;
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
            List<GetTariffMasterListResponse> Objresult = new List<GetTariffMasterListResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetTariffMasterList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffMasterList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
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
            List<TariffMastServicesResponse> Objresult = new List<TariffMastServicesResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetTariffMasterServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - TariffMasterServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
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
            List<GetClientTariffMasterListResponse> Objresult = new List<GetClientTariffMasterListResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetClientTariffMasterList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetClientTariffMasterList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
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
            List<ClientTariffServicesResponse> Objresult = new List<ClientTariffServicesResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetClientTariffServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientTariff - ServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/TariffMaster/GetTariffupdateList")]
        public GetTariffupdateResponse GetTariffupdateList(GetTariffupdateRequest getRequest)
        {
            GetTariffupdateResponse Objresult = new GetTariffupdateResponse();
            try
            {
                Objresult = _tariffMasterRepository.GetTariffupdateList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientTariff - ServiceDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion
        
        [HttpPost]
        [Route("api/TariffMaster/GetContractMaster")]
        public IEnumerable<GetContractRes> GetContractMaster(GetContractReq req)
        {
            List<GetContractRes> Objresult = new List<GetContractRes>();
            try
            {
                Objresult = _tariffMasterRepository.GetContractMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetContractMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo,0);
            }
            return Objresult;
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
            List<TariffMastServicesResponse> Objresult = new List<TariffMastServicesResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetContractMasterServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetContractMasterServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetContractVsClient")]
        public IEnumerable<ContractVsCustomerMap> GetContractVsClient(GetContractVsClientReq getRequest)
        {
            List<ContractVsCustomerMap> Objresult = new List<ContractVsCustomerMap>();
            try
            {
                Objresult = _tariffMasterRepository.GetContractVsClient(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetContractMasterServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.VenueNo, getRequest.ContractNo,0);
            }
            return Objresult;
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
            List<GetTariffRes> Objresult = new List<GetTariffRes>();
            try
            {
                Objresult = _tariffMasterRepository.GetClienttTariffMap(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetClienttTariffMap", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0, 0);
            }
            return Objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetRefSplRateServiceList")]
        public IEnumerable<TariffMastServicesResponse> GetRefSplRateServiceList(GetContractMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> Objresult = new List<TariffMastServicesResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetRefSplRateServiceList(getRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMaster - GetRefSplRateServiceList", ExceptionPriority.Low, ApplicationType.APPSERVICE, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return Objresult;
        }
        
        [HttpPost]
        [Route("api/TariffMaster/GetReflst")]
        public IEnumerable<GetReflstRes> GetReflst(GetContractReq req)
        {
            List<GetReflstRes> Objresult = new List<GetReflstRes>();
            try
            {
                Objresult = _tariffMasterRepository.GetReflst(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetReflst", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, 0);
            }
            return Objresult;
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
            List<Tariffdeptdisreq> Objresult = new List<Tariffdeptdisreq>();
            try
            {
                Objresult = _tariffMasterRepository.GetTariffDeptDiscount(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetTariffDeptDiscount", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.RateListNo, req.IsApproval);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/CommercialMaster/PriceHistoryService")]
        public RateHistoryServiceResponse PriceHistoryService(RateHistoryServiceRequest req)
        {
            RateHistoryServiceResponse Objresult = new RateHistoryServiceResponse();
            try
            {
                Objresult = _tariffMasterRepository.GetPriceHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PriceHistoryService", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Objresult;
        }
        
        [HttpPost]
        [Route("api/CommercialMaster/GetBasePrice")]
        public List<BaseRateResponse> GetBasePrice(RateHistoryServiceRequest req)
        {
            List<BaseRateResponse> Objresult = new List<BaseRateResponse>();
            try
            {
                Objresult = _tariffMasterRepository.GetBasePrice(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetBasePrice", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return Objresult;
        }
        
        [HttpPost]
        [Route("api/CommercialMaster/InsertBaseRate")]
        public int InsertBaseRate(List<BaseRateResponse> req)
        {
            int Objresult = 0;
            try
            {
                Objresult = _tariffMasterRepository.InsertBaseRate(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertBaseRate", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }        
    }
}