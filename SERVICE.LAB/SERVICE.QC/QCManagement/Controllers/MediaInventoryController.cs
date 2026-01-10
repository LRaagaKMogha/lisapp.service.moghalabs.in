using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCManagement.Contracts;
using QCManagement.Services.MediaInventory;
using Shared;

namespace QCManagement.Controllers
{
    //[CustomAuthorize("MediaInventoryMgmt")]
    public class MediaInventoryController : ApiController
    {
        private readonly IMediaInventoryService _inventoryService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MediaInventoryController(IMediaInventoryService inventoryService, IHttpContextAccessor httpContextAccessor)
        {
            _inventoryService = inventoryService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.MediaInventoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMediaInventory(Int64 id)
        {
            ErrorOr<Models.MediaInventory> getMediaInventoryResult = await _inventoryService.GetMediaInventory(id);

            return getMediaInventoryResult.Match(
                inventory => base.Ok(new ServiceResponse<Contracts.MediaInventoryResponse>("", "200", MapInventoryResponse(inventory))),
                errors => Problem(errors));
        }

        [HttpPost("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.MediaInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventories(MediaInventoryFilterRequest request)
        {
            ErrorOr<List<Models.MediaInventory>> response = await _inventoryService.GetMediaInventories(request);
            return response.Match(
                inventories => base.Ok(new ServiceResponse<List<Contracts.MediaInventoryResponse>>("", "200", inventories.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }



        [HttpPut("updatestatus")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventoryStatus(UpsertMediaInventoryStatusRequest request)
        {
            ErrorOr<bool> upsertInventoryStatusResult = await _inventoryService.UpdateInventoryStatus(new List<long>() { request.InventoryId }, request.Status, request.ModifiedBy, request.ModifiedByUserName, request.LastModifiedDateTime, request.Comments, true);

            return upsertInventoryStatusResult.Match(
                inventory => base.Ok(new ServiceResponse<bool>("", "200", upsertInventoryStatusResult.Value)),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.MediaInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertInventory(UpsertMediaInventoriesRequest request)
        {
            var inventories = new List<Models.MediaInventory>();
            List<Error>? errors = null;
            request.Inventories.ForEach(inventory =>
            {
                if (errors == null) errors = new List<Error>();
                ErrorOr<Models.MediaInventory> requestToInventoryResult = Models.MediaInventory.From(inventory, httpContextAccessor.HttpContext!);

                if (requestToInventoryResult.IsError)
                {
                    errors.AddRange(requestToInventoryResult.Errors);
                }
                else
                {
                    inventories.Add(requestToInventoryResult.Value);
                }
            });

            ErrorOr<List<Models.MediaInventory>> upsertInventoryResult = await _inventoryService.UpsertMediaInventories(inventories);

            return upsertInventoryResult.Match(
                inventories => base.Ok(new ServiceResponse<List<Contracts.MediaInventoryResponse>>("", "200", upsertInventoryResult.Value.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.MediaInventoryResponse MapInventoryResponse(Models.MediaInventory inventory)
        {
            return new Contracts.MediaInventoryResponse(
                inventory.Identifier,
                inventory.BatchId,
                inventory.ReceivedDateAndTime,
                inventory.MediaId,
                inventory.MediaLotNumber,
                inventory.ExpirationDateAndTime,
                inventory.Colour,
                inventory.Crack,
                inventory.Contaminate,
                inventory.Leakage,
                inventory.Turbid,
                inventory.VolumeOrAgarThickness,
                inventory.Sterlity,
                inventory.Vividity,
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
