using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.StrainMediaMapping;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("StrainMediaMappingMgmt")]
    public class StrainMediaMappingController : ApiController
    {
        private readonly IStrainMediaMappingService _strainMediaMappingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StrainMediaMappingController(IStrainMediaMappingService strainMediaMappingService, IHttpContextAccessor httpContextAccessor)
        {
            _strainMediaMappingService = strainMediaMappingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<StrainMediaMappingResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStrainMediaMapping(Int64 id)
        {
            ErrorOr<Models.StrainMediaMapping> getStrainMediaMappingResult = await _strainMediaMappingService.GetStrainMediaMapping(id);

            return getStrainMediaMappingResult.Match(
                strainMediaMapping => base.Ok(new ServiceResponse<StrainMediaMappingResponse>("", "200", MapStrainMediaMappingResponse(strainMediaMapping))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<StrainMediaMappingResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllStrainMediaMappings(StrainMediaMappingFilterRequest request)
        {
            ErrorOr<List<Models.StrainMediaMapping>> response = await _strainMediaMappingService.GetStrainMediaMappings(request);
            return response.Match(
                strainMediaMappings => base.Ok(new ServiceResponse<List<StrainMediaMappingResponse>>("", "200", strainMediaMappings.Select(x => MapStrainMediaMappingResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertStrainMediaMappingStatus(UpsertStrainMediaMappingStatusRequest request)
        {
            ErrorOr<bool> upsertStrainMediaMappingStatusResult = await _strainMediaMappingService.UpdateMappingStatus(
                new List<long>() { request.StrainMediaMappingId },
                request.Status,
                request.ModifiedBy,
                request.ModifiedByUserName,
                request.LastModifiedDateTime,
                request.Comments,
                true);

            return upsertStrainMediaMappingStatusResult.Match(
                result => base.Ok(new ServiceResponse<bool>("", "200", result)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<StrainMediaMappingResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertStrainMediaMappings(UpsertStrainMediaMappingsRequest request)
        {
            var strainMediaMappings = new List<Models.StrainMediaMapping>();
            List<Error>? errors = null;
            request.Mappings.ForEach(strainMediaMapping =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.StrainMediaMapping> requestToStrainMediaMappingResult = Models.StrainMediaMapping.From(strainMediaMapping, _httpContextAccessor.HttpContext!);

                if (requestToStrainMediaMappingResult.IsError)
                {
                    errors.AddRange(requestToStrainMediaMappingResult.Errors);
                }
                else
                {
                    strainMediaMappings.Add(requestToStrainMediaMappingResult.Value);
                }
            });

            ErrorOr<List<Models.StrainMediaMapping>> upsertStrainMediaMappingResult = await _strainMediaMappingService.UpsertStrainMediaMappings(strainMediaMappings);

            return upsertStrainMediaMappingResult.Match(
                strainMediaMappings => base.Ok(new ServiceResponse<List<StrainMediaMappingResponse>>("", "200", upsertStrainMediaMappingResult.Value.Select(x => MapStrainMediaMappingResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static StrainMediaMappingResponse MapStrainMediaMappingResponse(Models.StrainMediaMapping strainMediaMapping)
        {
            return new StrainMediaMappingResponse(
                strainMediaMapping.Identifier,
                strainMediaMapping.ReceivedDateAndTime,
                strainMediaMapping.StrainInventoryId,
                strainMediaMapping.MediaInventoryId,
                strainMediaMapping.Remarks,
                strainMediaMapping.Status,
                strainMediaMapping.ModifiedBy,
                strainMediaMapping.ModifiedByUserName,
                strainMediaMapping.LastModifiedDateTime,
                strainMediaMapping.IsActive,
                strainMediaMapping.BatchId
            );
        }
    }
}
