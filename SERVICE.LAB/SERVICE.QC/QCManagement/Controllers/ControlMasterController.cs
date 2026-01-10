using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Models;
using QCManagement.Services.ControlMaster;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("QCMgmt")]    
    public class ControlMasterController : ApiController
    {
        private readonly IControlMasterService controlMasterService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ControlMasterController(IControlMasterService controlMasterService, IHttpContextAccessor httpContextAccessor)
        {
            this.controlMasterService = controlMasterService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<ControlMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateControlMaster(ControlMasterRequest request)
        {
            var controlMaster = ControlMaster.From(request, httpContextAccessor.HttpContext!);

            if (controlMaster.IsError)
            {
                return Problem(controlMaster.Errors);
            }

            var createControlMasterResult = await controlMasterService.CreateControlMaster(controlMaster.Value);

            return createControlMasterResult.Match(
                created => CreatedAtControlMaster(created),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<ControlMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateControlMaster(ControlMasterRequest request)
        {
            var controlMaster = ControlMaster.From(request, httpContextAccessor.HttpContext!);

            if (controlMaster.IsError)
            {
                return Problem(controlMaster.Errors);
            }

            var updateControlMasterResult = await controlMasterService.UpdateControlMaster(controlMaster.Value);

            return updateControlMasterResult.Match(
                updated => CreatedAtControlMaster(updated),
                errors => Problem(errors));
        }

        [HttpPut("prepare")]
        [ProducesResponseType(typeof(ServiceResponse<ControlMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PrepareControlMaster(PrepareControlMasterRequest request)
        {
            var updateControlMasterResult = await controlMasterService.PrepareControlMaster(request);

            return updateControlMasterResult.Match(
                updated => CreatedAtControlMaster(updated),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<ControlMasterResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetControlMasterById(Int64 id)
        {
            var getControlMasterResult = await controlMasterService.GetControlMaster(id);

            return getControlMasterResult.Match(
                controlMaster => base.Ok(new ServiceResponse<ControlMasterResponse>("", "200", MapControlMasterResponse(controlMaster))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.ControlMasterResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllControlMasters(ControlMasterFilterRequest request)
        {
            ErrorOr<List<Models.ControlMaster>> response = await controlMasterService.GetControlMasters(request);
            return response.Match(
                controlmasters => base.Ok(new ServiceResponse<List<Contracts.ControlMasterResponse>>("", "200", controlmasters.Select(x => MapControlMasterResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtControlMaster(ControlMaster controlMaster)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetControlMasterById),
                routeValues: new { id = controlMaster.Identifier },
                value: new ServiceResponse<ControlMasterResponse>("", "201", MapControlMasterResponse(controlMaster)));
        }

        private static ControlMasterResponse MapControlMasterResponse(ControlMaster response)
        {
            var tests = response.TestControlSamples != null && response.TestControlSamples.Count > 0 ?
            response.TestControlSamples.Select(x => new TestControlSamplesResponse(x.Identifier, x.TestID, x.SubTestID, x.QcMonitoringMethod, x.ControlLimit, x.ParameterName, x.ControlID, x.TargetRangeMin, x.TargetRangeMax, x.SampleTypeId, x.UoMId, x.UomText, x.UoMId2, x.UomText2, x.Mean, x.Cv, x.SD, x.ModifiedBy, x.ModifiedByUserName, x.LastModifiedDateTime, x.IsSelected, x.StartTime, x.EndTime, x.DecimalPlaces)).ToList() : new List<TestControlSamplesResponse>();
            return new ControlMasterResponse(
                response.Identifier,
                response.ControlName,
                response.ControlType,
                response.LotNumber,
                response.ExpirationDate,
                response.ManufacturerID,
                response.DistributorID,
                response.Notes,
                response.Form,
                response.Level,
                response.ReagentID,
                response.Status,
                response.ModifiedBy,
                response.ModifiedByUserName,
                response.LastModifiedDateTime,
                tests,
                response.PreparationDateTime,
                response.PreparedBy,
                response.PreparedByUserName,
                response.StorageTemperature,
                response.AliquoteCount,
                response.IsActive,
                response.IsQualitative,
                response.IsAntibiotic
            );
        }
    }
}