using Dev.IRepository.Master;
using DEV.Common;
using Service.Model.Inventory;
using Service.Model.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FranchisorController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IFranchisorRepository _FranchisorRepository;

        public FranchisorController(IConfiguration config, IFranchisorRepository FranchisorRepository)
        {
            _config = config;
            _FranchisorRepository = FranchisorRepository;
        }
        #region Franchises
        [HttpGet]
        [Route("api/Franchisor/GetFranchises")]
        public List<GetFranchiseResponse> GetFranchises(int VenueNo, int VenueBranchNo)
        {
            List<GetFranchiseResponse> result = new List<GetFranchiseResponse>();
            try
            {
                result = _FranchisorRepository.GetFranchises(VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchisorController.GetFranchises", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        #endregion
        #region Franchise Revenue Sharing Services

        [HttpPost]
        [Route("api/Franchisor/GetFranchiseRevenueSharingByService")]
        public List<FranchiseRevenueSharingServiceDto> GetFranchiseRevenueSharingByService([FromBody] GetFranchiseRevenueSharingByServiceRequest request)
        {
            List<FranchiseRevenueSharingServiceDto> result = new List<FranchiseRevenueSharingServiceDto>();
            try
            {
                result = _FranchisorRepository.GetFranchiseRevenueSharingByService(request);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchisorController.GetFranchiseRevenueSharingByService", ExceptionPriority.High, ApplicationType.REPOSITORY, request.VenueNo, 0, 0);
            }
            return result;
        }

        #endregion
        #region Franchise Revenue Sharing Insert
        [HttpPost]
        [Route("api/Franchisor/InsertFranchiseRevenueSharing")]
        public async Task<IActionResult> InsertFranchiseRevenueSharing([FromBody] FranchiseRevenueSharingInsertDTO dto)
        {
            try
            {
                var status = await _FranchisorRepository.InsertFranchiseRevenueSharingAsync(dto);
                return Ok(new { status });
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchisorController.InsertFranchiseRevenueSharing",
                    ExceptionPriority.Medium, ApplicationType.APPSERVICE, dto.VenueNo, 0, 0);
                return StatusCode(500, new { status = 0 });
            }
        }
        #endregion
    }
}
