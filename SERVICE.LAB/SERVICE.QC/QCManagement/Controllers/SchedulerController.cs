using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.Scheduler;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("QCMgmt")]
    public class SchedulerController : ApiController
    {
        private readonly ISchedulerService SchedulerService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SchedulerController(ISchedulerService _SchedulerService, IHttpContextAccessor _httpContextAccessor)
        {
            SchedulerService = _SchedulerService;
            httpContextAccessor = _httpContextAccessor;
        }
        [HttpPost("delete")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.DeleteSchedulerRequest>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteScheduler(DeleteSchedulerRequest request)
        {
            ErrorOr<DeleteSchedulerRequest> getSchedulerResult = await SchedulerService.DeleteSchedulers(request);
            return getSchedulerResult.Match(
                Scheduler => base.Ok(new ServiceResponse<DeleteSchedulerRequest>("", "200", Scheduler)),
                errors => Problem(errors));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.SchedulerResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateScheduler(SchedulerRequests request)
        {
            var requests = new List<SchedulerRequest>();

            foreach (var time in request.Times)
            {
                var startDateWithTime = request.ScheduleStartDate.Date.Add(TimeSpan.Parse(time));
                while (startDateWithTime <= request.ScheduleEndDate)
                {
                    requests.Add(new SchedulerRequest(
                        request.Identifier,
                        request.AnalyzerId,
                        request.TestId,
                        request.ScheduleTitle,
                        startDateWithTime,
                        startDateWithTime.AddMinutes(30),
                        request.ModifiedBy,
                        request.ModifiedByUserName,
                        request.LastModifiedDateTime
                    ));
                    startDateWithTime = startDateWithTime.AddDays(1);
                }
            }

            var input = new List<Models.Scheduler>();
            List<Error>? errors = new List<Error>();

            requests.ForEach(row =>
            {
                var inputRow = Models.Scheduler.From(row, httpContextAccessor.HttpContext!);
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

            ErrorOr<List<Models.Scheduler>> createSchedulerResult = await SchedulerService.CreateScheduler(input);
            return createSchedulerResult.Match(
                           results => base.Ok(new ServiceResponse<List<Contracts.SchedulerResponse>>("", "200", results.Select(x => MapSchedulerResponse(x)).ToList())),
                           errors => Problem(errors));
        }
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.SchedulerResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateScheduler(SchedulerRequest request)
        {
            var SchedulerRequest = Models.Scheduler.From(request, httpContextAccessor.HttpContext!);

            if (SchedulerRequest.IsError)
            {
                return Problem(SchedulerRequest.Errors);
            }
            var Scheduler = SchedulerRequest.Value;
            ErrorOr<Models.Scheduler> createSchedulerResult = await SchedulerService.UpdateScheduler(Scheduler);

            return createSchedulerResult.Match(
                created => CreatedAtScheduler(Scheduler),
                errors => Problem(errors));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.SchedulerResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchedulerById(Int64 id)
        {
            ErrorOr<Models.Scheduler> getSchedulerResult = await SchedulerService.GetScheduler(id);

            return getSchedulerResult.Match(
                Scheduler => base.Ok(new ServiceResponse<Contracts.SchedulerResponse>("", "200", MapSchedulerResponse(Scheduler))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.SchedulerResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSchedulerDetails(SchedulerFilterRequest request)
        {
            ErrorOr<List<Models.Scheduler>> response = await SchedulerService.GetSchedulers(request);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.SchedulerResponse>>("", "200", Inventories.Select(x => MapSchedulerResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtScheduler(Models.Scheduler Scheduler)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetSchedulerById),
                routeValues: new { id = Scheduler.Identifier },
                value: new ServiceResponse<Contracts.SchedulerResponse>("", "201", MapSchedulerResponse(Scheduler)));
        }

        private static Contracts.SchedulerResponse MapSchedulerResponse(Models.Scheduler response)
        {
            return new Contracts.SchedulerResponse(
                response.Identifier,
                response.AnalyzerId,
                response.TestId,
                response.ScheduleTitle,
                response.ScheduleStartDate,
                response.ScheduleEndDate,
                response.BatchId,
                response.ModifiedBy,
                response.ModifiedByUserName,
                response.LastModifiedDateTime
            );
        }

    }


}