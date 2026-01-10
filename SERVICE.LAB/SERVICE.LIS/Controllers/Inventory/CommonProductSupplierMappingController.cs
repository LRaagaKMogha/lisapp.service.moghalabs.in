using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers.Inventory
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CommonProductSupplierMappingController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICommonProductSupplierMappingRepository _repository;

        public CommonProductSupplierMappingController(ICommonProductSupplierMappingRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost]
        [Route("api/CommonProductSupplierMappingMaster")]
        public async Task<IActionResult> GetProductSupplierMapping([FromBody] ProductSupplierMappingRequestDTO request)
        {
            object result = null;

            try
            {
                result = await _repository.GetProductSupplierMappingAsync(request);

                if (result == null)
                    return NotFound("No mapping data found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonProductSupplierMappingController.GetProductSupplierMapping", ExceptionPriority.Medium, ApplicationType.APPSERVICE, request.VenueNo, 0, 0);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        [Route("api/CommonProductSupplierMappingInsert")]
        public async Task<IActionResult> InsertProductSupplierMapping([FromBody] ProductSupplierMappingInsertDTO dto)
        {
            try
            {
                var status = await _repository.InsertProductSupplierMappingAsync(dto);
                return Ok(new { status }); // returns { status: 1 } or { status: 0 }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertProductSupplierMapping",
                    ExceptionPriority.Medium, ApplicationType.APPSERVICE, dto.VenueNo, dto.UserNo, 0);
                return StatusCode(500, new { status = 0 });
            }
        }

    }
}
