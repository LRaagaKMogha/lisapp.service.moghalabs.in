using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Services.BloodBankInventories;
using BloodBankManagement.Validators;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
    public class BloodBankInventoryController : ApiController
    {
        private readonly IBloodBankInventoryService _inventoryService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuditService _auditService;
        public BloodBankInventoryController(IBloodBankInventoryService inventoryService, IHttpContextAccessor _httpContextAccessor, IAuditService auditService)
        {
            _inventoryService = inventoryService;
            httpContextAccessor = _httpContextAccessor;
            _auditService = auditService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.BloodBankInventoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getBloodBankInventory(Int64 id)
        {
            ErrorOr<Models.BloodBankInventory> getBloodBankInventoryResult = await _inventoryService.GetBloodBankInventory(id);

            return getBloodBankInventoryResult.Match(
                Inventory => base.Ok(new ServiceResponse<Contracts.BloodBankInventoryResponse>("", "200", MapInventoryResponse(Inventory))),
                errors => Problem(errors));
        }

        [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventories(FetchBloodBankInventoriesRequest request)
        {
            ErrorOr<List<Models.BloodBankInventory>> response = await _inventoryService.GetBloodBankInventories(request);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodBankInventoryResponse>>("", "200", Inventories.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPost("exists")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.InventoryValidation>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ValidateInventories(List<Contracts.InventoryValidation> inventories)
        {
            ErrorOr<List<Contracts.InventoryValidation>> response = await _inventoryService.ValidationInventories(inventories);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.InventoryValidation>>("", "200", Inventories)),
                errors => Problem(errors));
        }

        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventoryStatus(UpsertBloodBankInventoryStatusRequest request)
        {
            List<Error>? errors = Shared.Helpers.ValidateInput<UpsertBloodBankInventoryStatusRequest, UpsertBloodBankInventoryStatusRequestValidator>(request, httpContextAccessor.HttpContext!);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<bool> upsertInventoryStatusResult;
            using (var auditScope = new AuditScope<UpsertBloodBankInventoryStatusRequest>(request, _auditService, "", new string[] { "Update Inventory Status" }))
            {
                upsertInventoryStatusResult = await _inventoryService.UpdateInventoryStatus(new List<long>() { request.InventoryId }, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.Comments, true);
                auditScope.IsRollBack = upsertInventoryStatusResult.IsError;
            }
            return upsertInventoryStatusResult.Match(
                inventory => base.Ok(new ServiceResponse<bool>("", "200", upsertInventoryStatusResult.Value)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventory(UpsertBloodBankInventoriesRequest request)
        {
            var inventories = new List<Models.BloodBankInventory>();
            List<Error>? errors = new List<Error>();
            request.Inventories.ForEach(inventory =>
            {
                ErrorOr<Models.BloodBankInventory> requestToInventoryResult = Models.BloodBankInventory.From(inventory, httpContextAccessor.HttpContext!);

                if (requestToInventoryResult.IsError)
                {
                    errors.AddRange(requestToInventoryResult.Errors);
                }
                else
                {
                    inventories.Add(requestToInventoryResult.Value);
                }
            });
            if (errors.Count > 0)
            {
                return Problem(errors);
            }
            ErrorOr<List<Models.BloodBankInventory>> upsertInventoryResult;
            using (var auditScope = new AuditScope<UpsertBloodBankInventoryRequest>(request.Inventories, _auditService, "", new string[] { "Inventory Update" }))
            {
                upsertInventoryResult = await _inventoryService.UpsertBloodBankInventories(inventories);
                auditScope.IsRollBack = upsertInventoryResult.IsError;
            }

            return upsertInventoryResult.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodBankInventoryResponse>>("", "200", upsertInventoryResult.Value.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.BloodBankInventoryResponse MapInventoryResponse(Models.BloodBankInventory inventory)
        {
            var createdDateTime = inventory.Transactions.OrderBy(x => x.LastModifiedDateTime).FirstOrDefault();
            return new Contracts.BloodBankInventoryResponse(
                inventory.Identifier,
                inventory.BatchId,
                inventory.DonationId,
                inventory.CalculatedDonationId,
                inventory.ProductCode,
                inventory.ExpirationDateAndTime,
                inventory.AboOnLabel,
                inventory.Volume,
                inventory.AntiAGrading,
                inventory.AntiBGrading,
                inventory.AntiABGrading,
                inventory.AboResult,
                inventory.AboPerformedByUserName,
                inventory.AboPerformedByDateTime,
                inventory.Status,
                inventory.IsRejected,
                inventory.IsVisualInspectionPassed,
                inventory.Comments,
                inventory.Temprature,
                inventory.ModifiedBy,
                inventory.ModifiedByUserName,
                inventory.Antibodies,
                inventory.ModifiedProductId,
                inventory.LastModifiedDateTime,
                createdDateTime != null ? createdDateTime.LastModifiedDateTime : null
            );
        }
    }
}
