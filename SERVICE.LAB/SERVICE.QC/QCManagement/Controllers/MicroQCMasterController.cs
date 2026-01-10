using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.MicroQCMaster;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("MicroQCMasterMgmt")]
    public class MicroQCMasterController : ApiController
    {
        private readonly IMicroQCMasterService _microQCMasterService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MicroQCMasterController(IMicroQCMasterService microQCMasterService, IHttpContextAccessor httpContextAccessor)
        {
            _microQCMasterService = microQCMasterService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<MicroQCMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMicroQCMaster(Int64 id)
        {
            ErrorOr<Models.MicroQCMaster> getMicroQCMasterResult = await _microQCMasterService.GetMicroQCMaster(id);

            return getMicroQCMasterResult.Match(
                microQCMaster => base.Ok(new ServiceResponse<MicroQCMasterResponse>("", "200", MapMicroQCMasterResponse(microQCMaster))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<MicroQCMasterResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMicroQCMasters(MicroQCMasterFilterRequest request)
        {
            ErrorOr<List<Models.MicroQCMaster>> response = await _microQCMasterService.GetMicroQCMasters(request);
            return response.Match(
                microQCMasters => base.Ok(new ServiceResponse<List<MicroQCMasterResponse>>("", "200", microQCMasters.Select(x => MapMicroQCMasterResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertMicroQCMasterStatus(UpsertMicroQCMasterStatusRequest request)
        {
            ErrorOr<bool> upsertMicroQCMasterStatusResult = await _microQCMasterService.UpdateMicroQCMasterStatus(
                new List<long>() { request.MicroQCMasterId },
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.Comments,
                true);

            return upsertMicroQCMasterStatusResult.Match(
                result => base.Ok(new ServiceResponse<bool>("", "200", result)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<MicroQCMasterResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertMicroQCMaster(UpsertMicroQCMastersRequest request)
        {
            var microQCMasters = new List<Models.MicroQCMaster>();
            List<Error>? errors = null;
            request.MicroQCMasters.ForEach(microQCMaster =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.MicroQCMaster> requestToMicroQCMasterResult = Models.MicroQCMaster.From(microQCMaster, _httpContextAccessor.HttpContext!);

                if (requestToMicroQCMasterResult.IsError)
                {
                    errors.AddRange(requestToMicroQCMasterResult.Errors);
                }
                else
                {
                    microQCMasters.Add(requestToMicroQCMasterResult.Value);
                }
            });

            ErrorOr<List<Models.MicroQCMaster>> upsertMicroQCMasterResult = await _microQCMasterService.UpsertMicroQCMasters(microQCMasters);

            return upsertMicroQCMasterResult.Match(
                microQCMasters => base.Ok(new ServiceResponse<List<MicroQCMasterResponse>>("", "200", upsertMicroQCMasterResult.Value.Select(x => MapMicroQCMasterResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static MicroQCMasterResponse MapMicroQCMasterResponse(Models.MicroQCMaster microQCMaster)
        {
            return new MicroQCMasterResponse(
                microQCMaster.Identifier,
                microQCMaster.CultureReagentTestId,
                microQCMaster.PositiveStrainIds,
                microQCMaster.NegativeStrainIds,
                microQCMaster.Frequency,
                microQCMaster.Status,
                microQCMaster.ModifiedBy,
                microQCMaster.ModifiedByUserName,
                microQCMaster.LastModifiedDateTime,
                microQCMaster.IsActive
            );
        }
    }
}
