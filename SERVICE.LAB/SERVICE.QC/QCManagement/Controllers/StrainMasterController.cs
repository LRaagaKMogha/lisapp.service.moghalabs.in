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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.StrainMaster;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("StrainMasterMgmt")]
    public class StrainMasterController : ApiController
    {
        private readonly IStrainMasterService _strainMasterService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StrainMasterController(IStrainMasterService strainMasterService, IHttpContextAccessor httpContextAccessor)
        {
            _strainMasterService = strainMasterService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.StrainMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStrainMaster(Int64 id)
        {
            ErrorOr<Models.StrainMaster> getStrainMasterResult = await _strainMasterService.GetStrainMaster(id);

            return getStrainMasterResult.Match(
                strainMaster => base.Ok(new ServiceResponse<Contracts.StrainMasterResponse>("", "200", MapStrainMasterResponse(strainMaster))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.StrainMasterResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllStrainMasters(StrainMasterFilterRequest request)
        {
            ErrorOr<List<Models.StrainMaster>> response = await _strainMasterService.GetStrainMasters(request);
            return response.Match(
                strainMasters => base.Ok(new ServiceResponse<List<Contracts.StrainMasterResponse>>("", "200", strainMasters.Select(x => MapStrainMasterResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertStrainMasterStatus(UpsertStrainMasterStatusRequest request)
        {
            ErrorOr<bool> upsertStrainMasterStatusResult = await _strainMasterService.UpdateStrainMasterStatus(new List<long>() { request.StrainMasterId }, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.Comments, true);

            return upsertStrainMasterStatusResult.Match(
                strainMaster => base.Ok(new ServiceResponse<bool>("", "200", upsertStrainMasterStatusResult.Value)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.StrainMasterResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertStrainMaster(UpsertStrainMastersRequest request)
        {
            var strainMasters = new List<Models.StrainMaster>();
            List<Error>? errors = null;
            request.StrainMasters.ForEach(strainMaster =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.StrainMaster> requestToStrainMasterResult = Models.StrainMaster.From(strainMaster, httpContextAccessor.HttpContext!);

                if (requestToStrainMasterResult.IsError)
                {
                    errors.AddRange(requestToStrainMasterResult.Errors);
                }
                else
                {
                    strainMasters.Add(requestToStrainMasterResult.Value);
                }
            });

            ErrorOr<List<Models.StrainMaster>> upsertStrainMasterResult = await _strainMasterService.UpsertStrainMasters(strainMasters);

            return upsertStrainMasterResult.Match(
                strainMasters => base.Ok(new ServiceResponse<List<Contracts.StrainMasterResponse>>("", "200", upsertStrainMasterResult.Value.Select(x => MapStrainMasterResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.StrainMasterResponse MapStrainMasterResponse(Models.StrainMaster strainMaster)
        {
            return new Contracts.StrainMasterResponse(
                strainMaster.Identifier,
                strainMaster.StrainCategoryId,
                strainMaster.StrainName,
                strainMaster.ExpiryAlertPeriod,
                strainMaster.LinkedMedias,
                strainMaster.Status,
                strainMaster.ModifiedBy,
                strainMaster.ModifiedByUserName,
                strainMaster.LastModifiedDateTime,
                strainMaster.IsActive
            );
        }
    }
}
