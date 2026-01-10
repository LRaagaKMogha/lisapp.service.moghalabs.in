using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Models;
using QCManagement.Services.Reagent;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("QCMgmt")]    
    public class ReagentController : ApiController
    {
        private readonly IReagentService ReagentService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReagentController(IReagentService ReagentService, IHttpContextAccessor httpContextAccessor)
        {
            this.ReagentService = ReagentService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<ReagentResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateReagent(ReagentsRequest request)
        {
            var input = new List<Reagent>();
            List<Error>? errors = new List<Error>();
            request.Reagents.ForEach(row =>
            {
                var inputRow = Models.Reagent.From(row, httpContextAccessor.HttpContext!);
                if (inputRow.IsError)
                {
                    errors.AddRange(inputRow.Errors);
                }
                else
                {
                    input.Add(inputRow.Value);
                }
            });
            if (errors.Count > 0)
            {
                return Problem(errors);
            }

            var createReagentResult = await ReagentService.CreateReagent(input);
            return createReagentResult.Match(
                Reagents => base.Ok(new ServiceResponse<List<Contracts.ReagentResponse>>("", "200", Reagents.Select(x => MapReagentResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<ReagentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateReagent(ReagentRequest request)
        {
            var reagent = Reagent.From(request, httpContextAccessor.HttpContext!);

            if (reagent.IsError)
            {
                return Problem(reagent.Errors);
            }

            var updateReagentResult = await ReagentService.UpdateReagent(reagent.Value);

            return updateReagentResult.Match(
                updated => CreatedAtReagent(updated),
                errors => Problem(errors));
        }

       

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<ReagentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReagentById(Int64 id)
        {
            var getReagentResult = await ReagentService.GetReagent(id);

            return getReagentResult.Match(
                Reagent => base.Ok(new ServiceResponse<ReagentResponse>("", "200", MapReagentResponse(Reagent))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.ReagentResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllReagents(ReagentFilterRequest request)
        {
            ErrorOr<List<Models.Reagent>> response = await ReagentService.GetReagents(request);
            return response.Match(
                Reagents => base.Ok(new ServiceResponse<List<Contracts.ReagentResponse>>("", "200", Reagents.Select(x => MapReagentResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtReagent(Reagent Reagent)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetReagentById),
                routeValues: new { id = Reagent.Identifier },
                value: new ServiceResponse<ReagentResponse>("", "201", MapReagentResponse(Reagent)));
        }

        private static ReagentResponse MapReagentResponse(Reagent response)
        {
            return new ReagentResponse(
                response.Identifier,
                response.Name,
                response.LotNo,
                response.SequenceNo,
                response.ExpirationDate,
                response.LotSetupOrInstallationDate,
                response.ManufacturerID,
                response.DistributorID,
                response.Status,
                response.ModifiedBy,
                response.ModifiedByUserName,
                response.LastModifiedDateTime,
                response.DepartmentId
               
            );
        }
    }
}