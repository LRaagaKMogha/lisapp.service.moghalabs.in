using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Services.BloodSampleInventories;
using BloodBankManagement.Services.BloodSampleResults;
using BloodBankManagement.Validators;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]
    public class BloodSampleInventoryController : ApiController
    {
        private readonly IBloodSampleInventoriesService bloodSampleInventoriesService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBloodSampleResultService bloodSampleResultService;
        private readonly IAuditService _auditService;
        public BloodSampleInventoryController(IBloodSampleInventoriesService _bloodSampleInventoriesService, IHttpContextAccessor _httpContextAccessor, IBloodSampleResultService _bloodSampleResultService, IAuditService auditService)
        {
            bloodSampleInventoriesService = _bloodSampleInventoriesService;
            httpContextAccessor = _httpContextAccessor;
            bloodSampleResultService = _bloodSampleResultService;
            _auditService = auditService;
        }


        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodSampleInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventories(FetchBloodSampleInventoriesRequest request)
        {
            ErrorOr<List<Models.BloodSampleInventory>> response = await bloodSampleInventoriesService.GetBloodSampleInventories(request);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodSampleInventoryResponse>>("", "200", Inventories.Select(x => MapSampleResultResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPost("history")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankRegistration>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPatientHistory(FetchPatientHistoryRequest request)
        {
            ErrorOr<List<Contracts.BloodBankRegistration>> response = await bloodSampleInventoriesService.GetPatientHistory(request);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodBankRegistration>>("", "200", Inventories)),
                errors => Problem(errors));
        }


        //1. CALLED DURING INVENTORY ASSIGNMENT SAVE...
        //2. 
        [HttpPut("updateinventories")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.UpsertInventoryRequest>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertBloodSampleInventoriesAssignment(UpsertInventoriesRequest request)
        {
            ErrorOr<bool> upsertBloodSampleInventory;
            using (var auditScope = new AuditScope<UpsertInventoryRequest>(request.Inventories, _auditService, "", new string[] { "Inventory Assignment" }))
            {
                upsertBloodSampleInventory = await bloodSampleInventoriesService.UpdateInventoriesWithAssignments(request.Inventories);
                auditScope.IsRollBack = upsertBloodSampleInventory.IsError;
                auditScope.VisitNo = request.Inventories.First().RegistrationId.ToString();
            }
            

            return upsertBloodSampleInventory.Match(
                results => base.Ok(new ServiceResponse<List<Contracts.UpsertInventoryRequest>>("", "200", request.Inventories)),
                errors => Problem(errors));
        }

        //1. CALLED DURING INVENTORY ASSIGNMENT REMOVAL OF ASSIGNMENT...
        [HttpPut("removeinventoryassociation")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.RemoveInventoryAssociationRequest>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveInventoryAssociation(RemoveInventoryAssociationRequest request)
        {
            var errors = Shared.Helpers.ValidateInput<RemoveInventoryAssociationRequest, RemoveInventoryAssociationRequestValidator>(request, httpContextAccessor.HttpContext!);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<bool> removeBloodSampleInventory = await bloodSampleInventoriesService.RemoveInventoryAssociation(request);

            return removeBloodSampleInventory.Match(
                results => base.Ok(new ServiceResponse<Contracts.RemoveInventoryAssociationRequest>("", "200", request)),
                errors => Problem(errors));
        }

        //1. CALLED DURING PRODUCT ISSUE SAVE.
        //2. CALLED DURING BLOOD PRODUCT RETURN 
        //3. CALLED DURING BLOOD TRANSFUSION. 
        //4. CALLED DURING UPDATION OF TRANSFUSION COMMENTS FROM REGISTRATION LIST.

        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.UpdateBloodSampleInventory>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertBloodSampleInventoriesStatus(UpdateBloodSampleInventoryStatusRequest request)
        {
            var errors = Shared.Helpers.ValidateInput<UpdateBloodSampleInventoryStatusRequest, UpdateBloodSampleInventoryStatusRequestValidator>(request, httpContextAccessor.HttpContext!);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Contracts.UpdateBloodSampleInventory>> upsertBloodSampleInventory;
            using (var auditScope = new AuditScope<UpdateBloodSampleInventoryStatusRequest>(request, _auditService, request.BloodSampleInventories.First().Status, new string[] { request.BloodSampleInventories.First().Status }))
            {
                upsertBloodSampleInventory = await bloodSampleInventoriesService.UpsertBloodSampleInventoriesStatus(request);
                auditScope.IsRollBack = upsertBloodSampleInventory.IsError;
                auditScope.VisitNo = request.BloodSampleInventories.First().RegistrationId.ToString();
            }

            return upsertBloodSampleInventory.Match(
                results => base.Ok(new ServiceResponse<List<Contracts.UpdateBloodSampleInventory>>("", "200", request.BloodSampleInventories)),
                errors => Problem(errors));
        }


        //1. CALLED DURING SAMPLE PROCESSING -> EDIT CROSS MATCHING -> DURING SAVE 
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodSampleInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertBloodSampleInventories(UpsertBloodSampleInventoriesRequest request)
        {
            var BloodSampleInventories = new List<Models.BloodSampleInventory>();
            var errors = new List<Error>();
            request.BloodSampleInventories.ForEach(sampleResult =>
            {
                ErrorOr<Models.BloodSampleInventory> sampleRequestResult = Models.BloodSampleInventory.From(sampleResult, httpContextAccessor.HttpContext!);
                if (sampleRequestResult.IsError)
                {
                    errors.AddRange(sampleRequestResult.Errors);
                }
                else
                {
                    BloodSampleInventories.Add(sampleRequestResult.Value);
                }
            });
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Models.BloodSampleInventory>> upsertBloodSampleInventory;
            using (var auditScope = new AuditScope<UpsertBloodSampleInventoryRequest>(request.BloodSampleInventories, _auditService, "", new string[] { "Sample Processing - Cross Matching Assignment" }))
            {
                upsertBloodSampleInventory = await bloodSampleInventoriesService.UpsertBloodSampleInventoriesWithChecks(BloodSampleInventories, false, true);
                var status = BloodSampleInventories.FirstOrDefault()?.Status;
                if (status != null && status != "OnHold")
                {
                    ErrorOr<List<Models.BloodSampleResult>> upsertBloodSampleResultStatus = await bloodSampleResultService.UpsertBloodSampleResultStatus(BloodSampleInventories.Select(x => x.BloodSampleResultId).ToList(), status);
                }
                auditScope.IsRollBack = upsertBloodSampleInventory.IsError;
                auditScope.VisitNo = request.BloodSampleInventories.First().RegistrationId.ToString();
            }
               
            return upsertBloodSampleInventory.Match(
                results => base.Ok(new ServiceResponse<List<Contracts.BloodSampleInventoryResponse>>("", "200", upsertBloodSampleInventory.Value.Select(x => MapSampleResultResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.BloodSampleInventoryResponse MapSampleResultResponse(Models.BloodSampleInventory sampleResult)
        {
            return new Contracts.BloodSampleInventoryResponse(
                sampleResult.Identifier,
                sampleResult.RegistrationId,
                sampleResult.BloodSampleResultId,
                sampleResult.ProductId,
                sampleResult.InventoryId,
                sampleResult.IsMatched,
                sampleResult.IsComplete,
                sampleResult.Comments,
                sampleResult.Status,
                sampleResult.IssuedToNurseId,
                sampleResult.ClinicId,
                sampleResult.PostIssuedClinicId,
                sampleResult.ReturnedByNurseId,
                sampleResult.TransfusionDateTime,
                sampleResult.TransfusionVolume,
                sampleResult.IsTransfusionReaction,
                sampleResult.TransfusionComments,
                sampleResult.ModifiedBy,
                sampleResult.ModifiedByUserName,
                sampleResult.LastModifiedDateTime,
                sampleResult.CrossMatchingTestId,
                sampleResult.CompatibilityValidTill,
                sampleResult.IssuedDateTime
            );
        }

    }
}
