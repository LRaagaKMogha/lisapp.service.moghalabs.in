using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Services.SampleReceiving;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]
    public class SampleReceivingController : ApiController
    {
        private readonly ISampleReceivingService _sampleReceivingService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService _auditService;
        public SampleReceivingController(ISampleReceivingService sampleReceivingService, IHttpContextAccessor _httpContextAccessor, IAuditService auditService)
        {
            _sampleReceivingService = sampleReceivingService;
            httpContextAccessor = _httpContextAccessor;
            _auditService = auditService;
        }


        [HttpGet("filters/{patientId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.SampleResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActiveSamplesForPatient(Int64 patientId)
        {
            ErrorOr<List<Models.BloodSample>> response = await _sampleReceivingService.GetActiveSamplesForPatient(patientId);
            return response.Match(
                samples => base.Ok(new ServiceResponse<List<Contracts.SampleResponse>>("", "200", samples.Select(x => MapBloodSampleResponse(x)).ToList())),
                errors => Problem(errors));
        }
        [HttpGet("getbarcodes/{registrationId}/{count}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<string>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBarCodes(Int64 registrationId, int count)
        {
            ErrorOr<List<string>> response = await _sampleReceivingService.GetBarCodes(registrationId, count);
            return response.Match(
                barcodes => base.Ok(new ServiceResponse<List<string>>("", "200", barcodes.ToList())),
                errors => Problem(errors));
        }
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.SampleResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertBloodSample(UpsertSampleReceivingRequest request)
        {
            var BloodSamples = new List<Models.BloodSample>();
            List<Error>? errors = new List<Error>();
            request.Samples.ForEach(sample =>
            {
                ErrorOr<Models.BloodSample> requestToBloodSampleResult = Models.BloodSample.From(sample, httpContextAccessor.HttpContext!);
                if (requestToBloodSampleResult.IsError)
                {
                    errors.AddRange(requestToBloodSampleResult.Errors);
                }
                else
                {
                    BloodSamples.Add(requestToBloodSampleResult.Value);
                }
            });

            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Models.BloodSample>> upsertBloodSampleResult;
            using (var auditScope = new AuditScope<SampleResponse>(request.Samples, _auditService, "", new string[] { "Sample Receiving" }))
            {
                upsertBloodSampleResult = await _sampleReceivingService.SaveBloodSamplesList(BloodSamples);
                auditScope.IsRollBack = upsertBloodSampleResult.IsError;
                auditScope.VisitNo = string.Join(",", request.Samples.Select(x => x.RegistrationId).ToList());
            }
            return upsertBloodSampleResult.Match(
                BloodSamples => base.Ok(new ServiceResponse<List<Contracts.SampleResponse>>("", "200", upsertBloodSampleResult.Value.Select(x => MapBloodSampleResponse(x)).ToList())),
                errors => Problem(errors));
        }
        private static Contracts.SampleResponse MapBloodSampleResponse(Models.BloodSample response)
        {
            return new Contracts.SampleResponse(
                response.Identifier,
                response.PatientId,
                response.RegistrationId,
                response.SampleTypeId,
                response.UnitCount,
                response.TubeNo,
                response.BarCode,
                response.IsActive,
                response.ModifiedBy,
                response.ModifiedByUserName,
                response.LastModifiedDateTime,
                response.ParentRegistrationId,
                response.Tests
            );
        }

    }
}
