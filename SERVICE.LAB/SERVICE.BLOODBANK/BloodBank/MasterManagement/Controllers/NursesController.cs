using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MasterManagement.Contracts;
using MasterManagement.Models;
using MasterManagement.Services.Nurses;
using MasterManagement.Helpers;
using Shared;
using Shared.Audit;

namespace MasterManagement.Controllers
{
    [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
    public class NursesController : ApiController
    {
        private readonly INurseService _NurseService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService auditService;
        public NursesController(INurseService NurseService, IHttpContextAccessor _httpContextAccessor, IAuditService _auditService)
        {
            _NurseService = NurseService;
            httpContextAccessor = _httpContextAccessor;
            auditService = _auditService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Nurse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateNurse(UpsertNurseRequest request)
        {
            var requestToNurseResult = Models.Nurse.From(request, httpContextAccessor.HttpContext!);

            if (requestToNurseResult.IsError)
            {
                return Problem(requestToNurseResult.Errors);
            }

            var Nurse = requestToNurseResult.Value;
            ErrorOr<Created> createNurseResult;
            using (var auditScope = new AuditScope<UpsertNurseRequest>(request, auditService, "", new string[] { "Create Nurse" }))
            {
                createNurseResult = await _NurseService.CreateNurse(Nurse);
                auditScope.IsRollBack = createNurseResult.IsError;
            }

            return createNurseResult.Match(
                created => CreatedAtGetNurse(Nurse),
                errors => Problem(errors));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Nurse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllNurses()
        {
            ErrorOr<List<Models.Nurse>> response = await _NurseService.GetNurses();
            return response.Match(
                Nurses => base.Ok(new ServiceResponse<List<Contracts.Nurse>>("", "200", Nurses.Select(x => MapNurseResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Nurse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetNurse(Int64 id)
        {
            ErrorOr<Models.Nurse> getNurseResult = await _NurseService.GetNurse(id);

            return getNurseResult.Match(
                Nurse => base.Ok(new ServiceResponse<Contracts.Nurse>("", "200", MapNurseResponse(Nurse))),
                errors => Problem(errors));
        }
        [HttpGet("filter/{type}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Nurse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetNursesByType(string type)
        {
            ErrorOr<List<Models.Nurse>> response = await _NurseService.GetNurses(type);

            return response.Match(
                 Nurses => base.Ok(new ServiceResponse<List<Contracts.Nurse>>("", "200", Nurses.Select(x => MapNurseResponse(x)).ToList())),
                 errors => Problem(errors));
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Nurse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertNurse(Int64 id, UpsertNurseRequest request)
        {
            ErrorOr<Models.Nurse> requestToNurseResult = Models.Nurse.From(id, request, httpContextAccessor.HttpContext!);

            if (requestToNurseResult.IsError)
            {
                return Problem(requestToNurseResult.Errors);
            }

            var Nurse = requestToNurseResult.Value;
            ErrorOr<UpsertedNurse> upsertNurseResult;
            using (var auditScope = new AuditScope<UpsertNurseRequest>(id.ToString(), request, auditService, "", new string[] { "Update Nurse" }))
            {
                upsertNurseResult = await _NurseService.UpsertNurse(Nurse);
                auditScope.IsRollBack = upsertNurseResult.IsError;
            }

            return upsertNurseResult.Match(
                upserted => CreatedAtGetNurse(Nurse),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurse(Int64 id)
        {
            ErrorOr<Deleted> deleteNurseResult = await _NurseService.DeleteNurse(id);

            return deleteNurseResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtGetNurse(Models.Nurse Nurse)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetNurse),
                routeValues: new { id = Nurse.Id },
                value: new ServiceResponse<Contracts.Nurse>("", "201", MapNurseResponse(Nurse)));
        }

        private static Contracts.Nurse MapNurseResponse(Models.Nurse Nurse)
        {
            return new Contracts.Nurse(
                Nurse.Id,
                Nurse.Name,
                Nurse.Description,
                Nurse.EmployeeId,
                Nurse.IsActive,
                Nurse.ModifiedBy,
                Nurse.LastModifiedDateTime
            );
        }
    }
}