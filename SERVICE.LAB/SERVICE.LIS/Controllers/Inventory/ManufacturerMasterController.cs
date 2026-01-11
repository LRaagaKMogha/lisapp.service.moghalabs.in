using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.Inventory.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ManufacturerMasterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IManufacturerMasterRepository _ManufacturerMasterRepository;
        public ManufacturerMasterController(IManufacturerMasterRepository ManufacturerMasterRepository,IConfiguration config)
        {
            _ManufacturerMasterRepository = ManufacturerMasterRepository;
            _config = config;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Manufacturer/InsertManufacturerDetails")]

        public int InsertManufacturerDetails(postManufacturerMasterDTO objManufacturer)
        {
            int result = 0;
            try
            {
                result = _ManufacturerMasterRepository.InsertManufacturerDetails(objManufacturer);
                string _CacheKey = CacheKeys.CommonMaster + "MANUFACTURER" + objManufacturer.venueNo + objManufacturer.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManufacturerMasterController.InsertManufacturerDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, objManufacturer.venueNo, objManufacturer.userNo, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Manufacturer/GetManufacturersDetail")]
        public List<GetManufacturerMasterResponse> GetManufacturersDetail(ManufacturerMasterRequest masterRequest)
        {
            List<GetManufacturerMasterResponse> objResult = new List<GetManufacturerMasterResponse>();
            try
            {
                objResult = _ManufacturerMasterRepository.GetManufacturersDetail(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ManufacturerMasterController.GetManufacturersDetail", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.venueNo, 0, 0);
            }
            return objResult;
        }
    }
}