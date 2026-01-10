using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Services.BloodBankInventories;
using BloodBankManagement.Services.Reports;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace BloodBankManagement.Controllers
{

    [CustomAuthorize("BloodBankMgmt")]
    public class ReportsController : ApiController
    {
        private readonly IReportsService reportsService;
        public ReportsController(IReportsService _reportsService)
        {
            reportsService = _reportsService;
        }

        [HttpPost("inventories")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankInventoryResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventories(FetchBloodBankInventoriesRequest request)
        {
            ErrorOr<List<Models.BloodBankInventory>> response = await reportsService.GetBloodBankInventories(request);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodBankInventoryResponse>>("", "200", Inventories.Select(x => MapInventoryResponse(x)).ToList())),
                errors => Problem(errors));
        }


        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankInventoryTransactionResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInventoryTransactions(Int64 id)
        {
            ErrorOr<List<Models.BloodBankInventoryTransaction>> response = await reportsService.GetBloodBankInventoryTransactions(id);
            return response.Match(
                Inventories => base.Ok(new ServiceResponse<List<Contracts.BloodBankInventoryTransactionResponse>>("", "200", Inventories.Select(x => MapInventoryTransactionResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpPost("requests")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankRegistration>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRequestsHistory(FetchPatientHistoryRequest request)
        {
            ErrorOr<List<Contracts.BloodBankRegistration>> response = await reportsService.GetRequestsHistory(request);
            return response.Match(
                requests => base.Ok(new ServiceResponse<List<Contracts.BloodBankRegistration>>("", "200", requests)),
                errors => Problem(errors));
        }

        [HttpPost("billing")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.BloodBankRegistration>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillingHistory(FetchPatientBillingRequest request)
        {
            ErrorOr<List<Models.BloodBankRegistration>> response = await reportsService.GetBillingHistory(request);
            return response.Match(
                requests => base.Ok(new ServiceResponse<List<Contracts.BloodBankRegistration>>("", "200", requests.Select(row => BloodBankRegistrationController.MapPatientRegistrationResponse(row)).ToList())),
                errors => Problem(errors));
        }

        private static Contracts.BloodBankInventoryTransactionResponse MapInventoryTransactionResponse(Models.BloodBankInventoryTransaction transaction)
        {
            return new Contracts.BloodBankInventoryTransactionResponse(
                transaction.Identifier,
                transaction.InventoryStatus,
                transaction.Comments,
                transaction.ModifiedBy,
                transaction.ModifiedByUserName,
                transaction.InventoryId,
                transaction.LastModifiedDateTime
            );
        }

        private static Contracts.BloodBankInventoryResponse MapInventoryResponse(Models.BloodBankInventory inventory)
        {
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
                null
            );
        }
    }
}
