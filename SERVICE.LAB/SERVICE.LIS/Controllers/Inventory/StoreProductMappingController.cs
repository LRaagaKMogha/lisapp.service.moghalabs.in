using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StoreProductMappingController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IStoreProductMappingRepository _StoreProductMappingRepository;

        public StoreProductMappingController(IStoreProductMappingRepository StoreProductMappingRepository, IConfiguration config)
        {
            _StoreProductMappingRepository = StoreProductMappingRepository;
            _config = config;
        }

        [HttpPost]
        [Route("api/StoreProductMapping/GetStoreProductMapping")]
        public async Task<IActionResult> GetStoreProductMapping([FromBody] StoreProductMappingRequestDTO request)
        {
            object result = null;

            try
            {
                result = await _StoreProductMappingRepository.GetStoreProductMappingAsync(request);

                if (result == null)
                    return NotFound("No mapping data found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreProductMappingController.GetStoreProductMapping",
                    ExceptionPriority.Medium, ApplicationType.APPSERVICE, request.VenueNo, 0, 0);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("api/StoreProductMapping/InsertStoreProductMapping")]
        public async Task<IActionResult> InsertStoreProductMapping([FromBody] StoreProductMappingInsertDTO dto)
        {
            try
            {
                var status = await _StoreProductMappingRepository.InsertStoreProductMappingAsync(dto);
                return Ok(new { status });
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreProductMappingController.InsertStoreProductMapping",
                    ExceptionPriority.Medium, ApplicationType.APPSERVICE, dto.VenueNo, dto.UserNo, 0);
                return StatusCode(500, new { status = 0 });
            }
        }
    }
}
