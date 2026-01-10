using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Services.BloodSampleResults;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace BloodBankManagement.Controllers
{

    [CustomAuthorize("BloodBankMgmt")]
    public class BloodSampleResultController : ApiController
    {
        private readonly IBloodSampleResultService _bloodSampleResultService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService _auditService;
        public BloodSampleResultController(IBloodSampleResultService bloodSampleResultService, IHttpContextAccessor _httpContextAccessor, IAuditService auditService)
        {
            _bloodSampleResultService = bloodSampleResultService;
            httpContextAccessor = _httpContextAccessor;
            _auditService = auditService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodSampleResultResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBloodSampleResults(UpsertBloodSampleResultsRequest request)
        {
            var bloodsampleResults = new List<Models.BloodSampleResult>();
            List<Error>? errors = null;
            request.BloodSampleResults.ForEach(sampleResult =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.BloodSampleResult> sampleRequestResult = Models.BloodSampleResult.From(sampleResult, httpContextAccessor.HttpContext!);
                if (sampleRequestResult.IsError)
                {
                    errors.AddRange(sampleRequestResult.Errors);
                }
                else
                {
                    bloodsampleResults.Add(sampleRequestResult.Value);
                }
            });

            ErrorOr<List<Models.BloodSampleResult>> upsertBloodSampleResult = await _bloodSampleResultService.AddBloodSampleResults(bloodsampleResults);

            return upsertBloodSampleResult.Match(
                results => base.Ok(new ServiceResponse<List<Contracts.BloodSampleResultResponse>>("", "200", upsertBloodSampleResult.Value.Select(x => MapSampleResultResponse(x)).ToList())),
                errors => Problem(errors));
        }
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodSampleResultResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertBloodSampleResults(UpsertBloodSampleResultsRequest request)
        {
            var bloodsampleResults = new List<Models.BloodSampleResult>();
            List<Error>? errors = errors = new List<Error>(); ;
            request.BloodSampleResults.ForEach(sampleResult =>
            {
                ErrorOr<Models.BloodSampleResult> sampleRequestResult = Models.BloodSampleResult.From(sampleResult, httpContextAccessor.HttpContext!);
                if (sampleRequestResult.IsError)
                {
                    errors.AddRange(sampleRequestResult.Errors);
                }
                else
                {
                    bloodsampleResults.Add(sampleRequestResult.Value);
                }
            });
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Models.BloodSampleResult>> upsertBloodSampleResult;
            var userAction = request.BloodSampleResults.First().Status == "SampleProcessed" ? "Sample Processing - Result Entry" : "Result Validation";
            using (var auditScope = new AuditScope<UpsertBloodSampleResultRequest>(request.BloodSampleResults, _auditService, "", new string[] { userAction }))
            {
                upsertBloodSampleResult = await _bloodSampleResultService.UpsertBloodSampleResult(bloodsampleResults);
                auditScope.IsRollBack = upsertBloodSampleResult.IsError;
                auditScope.VisitNo = request.BloodSampleResults.First().RegistrationId.ToString();
            }

            return upsertBloodSampleResult.Match(
                results => base.Ok(new ServiceResponse<List<Contracts.BloodSampleResultResponse>>("", "200", upsertBloodSampleResult.Value.Select(x => MapSampleResultResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.BloodSampleResultResponse MapSampleResultResponse(Models.BloodSampleResult sampleResult)
        {
            return new Contracts.BloodSampleResultResponse(
                sampleResult.Identifier,
                sampleResult.BloodBankRegistrationId,
                sampleResult.ContainerId,
                sampleResult.TestId,
                sampleResult.ParentTestId,
                sampleResult.InventoryId,
                sampleResult.TestName,
                sampleResult.Unit,
                sampleResult.TestValue,
                sampleResult.Comments,
                sampleResult.BarCode,
                sampleResult.Status,
                sampleResult.IsRejected,
                sampleResult.ParentRegistrationId,
                sampleResult.ModifiedBy,
                sampleResult.ModifiedByUserName,
                sampleResult.LastModifiedDateTime,
                sampleResult.ReCheck,
                sampleResult.SentToHSA,
                sampleResult.interfaceispicked,
                sampleResult.IsUploadAvail,
                sampleResult.GroupId
            );
        }

    }
}
