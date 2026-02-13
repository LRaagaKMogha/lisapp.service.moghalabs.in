using Service.IRepository.FrontOffice;
using Service.Common;
using Service.Model.FrontOffice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ClientBranchSamplePickupController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IClientBranchSamplePickupRepository _IClientBranchSamplePickupRepository;
        public ClientBranchSamplePickupController(IClientBranchSamplePickupRepository ClientBranchSamplePickupRepository, IConfiguration config)
        {
            _IClientBranchSamplePickupRepository = ClientBranchSamplePickupRepository;
            _config = config;
        }
        [HttpPost]
        [Route("api/ClientBranchSamplePickup/GetClientBranchSamplePickup")]
        public List<ClientBranchSamplePickupResponse> ClientBranchSamplePickup(ClientBranchSamplePickupRequest RequestItem)
        {
            List<ClientBranchSamplePickupResponse> objresult = new List<ClientBranchSamplePickupResponse>();
            try
            {
                objresult = _IClientBranchSamplePickupRepository.GetClientBranchSamplePickup(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupController.GetClientBranchSamplePickup", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/ClientBranchSamplePickup/InsertClientBranchSamplePickup")]
        public ClientBranchSamplePickupInsertResponse InsertClientBranchSamplePickup(ClientBranchSamplePickupInsertRequest request)
        {
            ClientBranchSamplePickupInsertResponse objResult = new ClientBranchSamplePickupInsertResponse();
            try
            {
                objResult = _IClientBranchSamplePickupRepository.InsertClientBranchSamplePickup(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupController.InsertClientBranchSamplePickup", ExceptionPriority.High, ApplicationType.APPSERVICE, request.VenueNo, request.VenueBranchNo, request.UserNo);
            }
            return objResult;
        }
        [HttpPost]
        [Route("api/ClientBranchSamplePickup/InsertRiderClientBranchSamplePickup")]
        public ClientBranchSamplePickupRiderInsertResponse InsertRiderClientBranchSamplePickup(ClientBranchSamplePickupRiderInsertRequest request)
        {
            ClientBranchSamplePickupRiderInsertResponse objResult = new ClientBranchSamplePickupRiderInsertResponse();
            try
            {
                objResult = _IClientBranchSamplePickupRepository.InsertRiderClientBranchSamplePickup(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ClientBranchSamplePickupController.InsertRiderClientBranchSamplePickup", ExceptionPriority.High, ApplicationType.APPSERVICE, request.VenueNo, request.VenueBranchNo, request.UserNo);
            }
            return objResult;
        }
    }
}
