using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Service.IRepository;
using Service.Common;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceOrderRepository _ServiceOrderRepository;
        public ServiceOrderController(IServiceOrderRepository noteRepository)
        {
            _ServiceOrderRepository = noteRepository;
        }

        [HttpPost]
        [Route("api/ServiceOrder/GetserviceOrderMaster")]
        public IEnumerable<GetserviceDetails> GetserviceOrderMaster(ServiceOrderMasterRequest serviceOrderItem)
        {
            List<GetserviceDetails> Objresult = new List<GetserviceDetails>();
            try
            {
              
                Objresult = _ServiceOrderRepository.GetserviceOrderMaster(serviceOrderItem);
                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ServiceOrderController.GetserviceOrderMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, serviceOrderItem.VenueNo, serviceOrderItem.ServiceNo, 0);
            }
            return Objresult;
        }   

        [HttpPost]
        [Route("api/ServiceOrder/InsertServiceOrderMaster")]
        public ServiceOrderMasterResponse InsertServiceOrderMaster(TblServiceOrder resultItem)
        {
            ServiceOrderMasterResponse result = new ServiceOrderMasterResponse();
            try
            {
                result = _ServiceOrderRepository.InsertServiceOrderMaster(resultItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ServiceOrderController.InsertServiceOrderMaster", ExceptionPriority.Medium, ApplicationType.APPSERVICE, resultItem.venueNo, resultItem.venueBranchNo, 0);
            }
            return result;
        }

    }
}