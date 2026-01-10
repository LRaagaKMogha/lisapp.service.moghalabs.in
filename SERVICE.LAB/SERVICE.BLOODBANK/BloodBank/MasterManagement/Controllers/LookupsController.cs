using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MasterManagement.Contracts;
using MasterManagement.Models;
using MasterManagement.Services.Lookups;
using MasterManagement.Helpers;
using Shared;
using Microsoft.AspNetCore.Http;
using Shared.Audit;

namespace MasterManagement.Controllers
{
    [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
    public class LookupsController : ApiController
    {
        private readonly ILookupService _LookupService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService _auditService;

        public LookupsController(ILookupService LookupService, IHttpContextAccessor _httpContextAccessor, IAuditService auditService)
        {
            _LookupService = LookupService;
            httpContextAccessor = _httpContextAccessor;
            _auditService = auditService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Lookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateLookup(UpsertLookupRequest request)
        {
            var requestToLookupResult = Models.Lookup.From(request, httpContextAccessor.HttpContext!);

            if (requestToLookupResult.IsError)
            {
                return Problem(requestToLookupResult.Errors);
            }

            var Lookup = requestToLookupResult.Value;
            var user = httpContextAccessor.HttpContext!.Items["User"] as User;
            ErrorOr<Created> createLookupResult;
            using (var auditScope = new AuditScope<UpsertLookupRequest>(request, _auditService, "", new string[] { "Create " + request.Type }))
            {
                createLookupResult = await _LookupService.CreateLookup(Lookup, user!);
                auditScope.IsRollBack = createLookupResult.IsError;
            }
            return createLookupResult.Match(
                created => CreatedAtGetLookup(Lookup),
                errors => Problem(errors));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Lookup>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllLookups()
        {
            try
            {
                ErrorOr<List<Models.Lookup>> response = await _LookupService.GetLookups();
                return response.Match(
                    Lookups => base.Ok(new ServiceResponse<List<Contracts.Lookup>>("", "200", Lookups.Select(x => MapLookupResponse(x)).ToList())),
                    errors => Problem(errors));
            }catch(Exception Error)
            {
                return null;
            }
           
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Lookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLookup(Int64 id)
        {
            ErrorOr<Models.Lookup> getLookupResult = await _LookupService.GetLookup(id);

            return getLookupResult.Match(
                Lookup => base.Ok(new ServiceResponse<Contracts.Lookup>("", "200", MapLookupResponse(Lookup))),
                errors => Problem(errors));
        }
        [HttpGet("filter/{type}/{searchText?}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Lookup>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLookupsByType(string type, string searchText = "")
        {
            if (searchText == "-") searchText = "";
            ErrorOr<List<Models.Lookup>> response = await _LookupService.GetLookups(type, searchText);

            return response.Match(
                 Lookups => base.Ok(new ServiceResponse<List<Contracts.Lookup>>("", "200", Lookups.Select(x => MapLookupResponse(x)).ToList())),
                 errors => Problem(errors));

        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Lookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertLookup(Int64 id, UpsertLookupRequest request)
        {
            ErrorOr<Models.Lookup> requestToLookupResult = Models.Lookup.From(id, request, httpContextAccessor.HttpContext!);

            if (requestToLookupResult.IsError)
            {
                return Problem(requestToLookupResult.Errors);
            }

            var Lookup = requestToLookupResult.Value;
            var user = httpContextAccessor.HttpContext!.Items["User"] as User;
            ErrorOr<UpsertedLookup> upsertLookupResult;
            using (var auditScope = new AuditScope<UpsertLookupRequest>(id.ToString(), request, _auditService, "", new string[] { "Update " + request.Type }))
            {
                upsertLookupResult = await _LookupService.UpsertLookup(Lookup, user!);
                auditScope.IsRollBack = upsertLookupResult.IsError;
            }

            return upsertLookupResult.Match(
                upserted => CreatedAtGetLookup(Lookup),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookup(Int64 id)
        {
            ErrorOr<Deleted> deleteLookupResult = await _LookupService.DeleteLookup(id);

            return deleteLookupResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtGetLookup(Models.Lookup Lookup)
        {
            return base.CreatedAtAction(
                actionName: nameof(GetLookup),
                routeValues: new { id = Lookup.Id },
                value: new ServiceResponse<Contracts.Lookup>("", "201", MapLookupResponse(Lookup)));
        }

        private static Contracts.Lookup MapLookupResponse(Models.Lookup Lookup)
        {
            return new Contracts.Lookup(
                Lookup.Id,
                Lookup.Name,
                Lookup.Description,
                Lookup.Code,
                Lookup.IsActive,
                Lookup.LastModifiedDateTime,
                Lookup.CreatedDateTime,
                Lookup.ModifiedBy,
                Lookup.CreatedBy,
                Lookup.Type
            );
        }
    }
}