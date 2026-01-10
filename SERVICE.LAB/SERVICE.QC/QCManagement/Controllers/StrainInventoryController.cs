using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.StrainInventory;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("StrainInventoryMgmt")]
    public class StrainInventoryController : ApiController
    {
        private readonly IStrainInventoryService _inventoryService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StrainInventoryController(IStrainInventoryService inventoryService, IHttpContextAccessor httpContextAccessor)
        {
            _inventoryService = inventoryService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.StrainInventoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStrainInventory(Int64 id)
        {
            ErrorOr<Models.StrainInventory> getStrainInventoryResult = await _inventoryService.GetStrainInventory(id);

            return getStrainInventoryResult.Match(
                inventory => base.Ok(new ServiceResponse<Contracts.StrainInventoryResponse>("", "200", MapInventoryResponse(inventory))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.StrainInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventories(StrainInventoryFilterRequest request)
        {
            ErrorOr<List<Models.StrainInventory>> response = await _inventoryService.GetStrainInventories(request);
            return response.Match(
                inventories => base.Ok(new ServiceResponse<List<Contracts.StrainInventoryResponse>>("", "200", inventories.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }



        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventoryStatus(UpsertStrainInventoryStatusRequest request)
        {
            ErrorOr<bool> upsertInventoryStatusResult = await _inventoryService.UpdateInventoryStatus(new List<long>() { request.StrainInventoryId }, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.Comments, true);

            return upsertInventoryStatusResult.Match(
                inventory => base.Ok(new ServiceResponse<bool>("", "200", upsertInventoryStatusResult.Value)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.StrainInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventory(UpsertStrainInventoriesRequest request)
        {
            var inventories = new List<Models.StrainInventory>();
            List<Error>? errors = null;
            request.Inventories.ForEach(inventory =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.StrainInventory> requestToInventoryResult = Models.StrainInventory.From(inventory, httpContextAccessor.HttpContext!);

                if (requestToInventoryResult.IsError)
                {
                    errors.AddRange(requestToInventoryResult.Errors);
                }
                else
                {
                    inventories.Add(requestToInventoryResult.Value);
                }
            });

            ErrorOr<List<Models.StrainInventory>> upsertInventoryResult = await _inventoryService.UpsertStrainInventories(inventories);

            return upsertInventoryResult.Match(
                inventories => base.Ok(new ServiceResponse<List<Contracts.StrainInventoryResponse>>("", "200", upsertInventoryResult.Value.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.StrainInventoryResponse MapInventoryResponse(Models.StrainInventory inventory)
        {
            return new Contracts.StrainInventoryResponse(
                inventory.Identifier,
                inventory.BatchId,
                inventory.ReceivedDateAndTime,
                inventory.StrainId,
                inventory.StrainLotNumber,
                inventory.ExpirationDateAndTime,
                inventory.Remarks,
                inventory.Status,
                inventory.ModifiedBy,
                inventory.ModifiedByUserName,
                inventory.LastModifiedDateTime,
                inventory.IsActive
            );
        }
    }
}
