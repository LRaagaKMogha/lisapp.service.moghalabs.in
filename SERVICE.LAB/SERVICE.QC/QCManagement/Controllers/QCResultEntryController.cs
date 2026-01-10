using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Models;
using QCManagement.Services.QCResultEntry;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("QCMgmt")]
    public class QCResultEntryController : ApiController
    {
        private readonly IQCResultEntryService qcResultEntryService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public QCResultEntryController(IQCResultEntryService qcResultEntryService, IHttpContextAccessor httpContextAccessor)
        {
            this.qcResultEntryService = qcResultEntryService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<QCResultEntryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateQCResultEntry(QCResultEntryRequest request)
        {
            var qcResultEntry = QCResultEntry.From(request, httpContextAccessor.HttpContext!);

            if (qcResultEntry.IsError)
            {
                return Problem(qcResultEntry.Errors);
            }

            var createQCResultEntryResult = await qcResultEntryService.CreateQCResultEntry(qcResultEntry.Value);

            return createQCResultEntryResult.Match(
                created => CreatedAtQCResultEntry(created),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<QCResultEntryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateQCResultEntry(UpsertQCResultEntryRequest request)
        {
            var qCResultEntries = new List<QCResultEntry>();
            List<Error>? errors = new List<Error>();
            request.QCResultEntries.ForEach(sampleResult =>
            {
                ErrorOr<QCResultEntry> sampleRequestResult = QCResultEntry.From(sampleResult, httpContextAccessor.HttpContext!);
                if (sampleRequestResult.IsError)
                {
                    errors.AddRange(sampleRequestResult.Errors);
                }
                else
                {
                    qCResultEntries.Add(sampleRequestResult.Value);
                }
            });
            if(errors.Count > 0) 
            {
                return Problem(errors);
            }
            var updateQCResultEntryResult = await qcResultEntryService.UpdateQCResultEntry(qCResultEntries, request.Type);
            return updateQCResultEntryResult.Match(
                           results => base.Ok(new ServiceResponse<List<Contracts.QCResultEntryResponse>>("", "200", results.Select(x => MapQCResultEntryResponse(x)).ToList())),
                           errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<QCResultEntryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetQCResultEntryById(Int64 id)
        {
            var getQCResultEntryResult = await qcResultEntryService.GetQCResultEntry(id);

            return getQCResultEntryResult.Match(
                qcResultEntry => base.Ok(new ServiceResponse<QCResultEntryResponse>("", "200", MapQCResultEntryResponse(qcResultEntry))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.QCResultEntryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllQCResultEntries(QCResultEntryFilterRequest request)
        {
            ErrorOr<List<Models.QCResultEntry>> response = await qcResultEntryService.GetQCResultEntries(request);
            return response.Match(
                qcResultEntries => base.Ok(new ServiceResponse<List<Contracts.QCResultEntryResponse>>("", "200", qcResultEntries.Select(x => MapQCResultEntryResponse(x)).ToList())),
                errors => Problem(errors));
        }
        private CreatedAtActionResult CreatedAtQCResultEntry(QCResultEntry qcResultEntry)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetQCResultEntryById),
                routeValues: new { id = qcResultEntry.Identifier },
                value: new ServiceResponse<QCResultEntryResponse>("", "201", MapQCResultEntryResponse(qcResultEntry)));
        }

        private static QCResultEntryResponse MapQCResultEntryResponse(QCResultEntry response)
        {
            return new QCResultEntryResponse(
                response.Identifier,
                response.TestID,
                response.ParameterName,
                response.ControlID,
                response.AnalyzerID,
                response.QcMonitoringMethod,
                response.TestDate,
                response.ObservedValue,
                response.Comments,
                response.Status,
                response.BatchId,
                response.IsActive,
                response.ModifiedBy,
                response.ModifiedByUserName,
                response.LastModifiedDateTime,
                response.TestControlSamplesID,
                response.IsMicroQc,
                response.IsAntibiotic
            );
        }
    }
}