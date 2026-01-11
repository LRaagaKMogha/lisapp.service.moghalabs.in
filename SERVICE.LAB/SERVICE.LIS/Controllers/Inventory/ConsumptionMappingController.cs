using Dev.IRepository.Inventory;
using Dev.Repository.Inventory;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ConsumptionMappingController : ControllerBase
    {
        private readonly IConsumptionMappingRepositoty _consumptionMappingRepositoty;
        public ConsumptionMappingController(IConsumptionMappingRepositoty consumptionMappingRepositoty)
        {
            _consumptionMappingRepositoty = consumptionMappingRepositoty;           
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ConsumptionMapping/GetConsumptionMapping")]
        public IEnumerable<GetConsumptionMappingResponse> GetAllConsumptionMapping(GetAllConsumptionMappingRequest request)
        {
            List<GetConsumptionMappingResponse> result = new List<GetConsumptionMappingResponse>();
            try
            {
                result = _consumptionMappingRepositoty.GetAllConsumptionMapping(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ConsumptionMappingController.GetAllConsumptionMapping", ExceptionPriority.Low, ApplicationType.APPSERVICE, request.venueno, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/ConsumptionMapping/InsertConsumptionMapping")]
        public CommonAdminResponse InsertConsumptionMapping(InsertConsumptionMapping saveConsumption)
        {
            CommonAdminResponse result = new CommonAdminResponse();
            try
            {
                result = _consumptionMappingRepositoty.InsertConsumptionMapping(saveConsumption);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ConsumptionMappingController.InsertConsumptionMapping", ExceptionPriority.Low, ApplicationType.APPSERVICE, saveConsumption.VenueNo, saveConsumption.VenueBranchNo, saveConsumption.Createdby);
            }
            return result;
        }        
    }
}
