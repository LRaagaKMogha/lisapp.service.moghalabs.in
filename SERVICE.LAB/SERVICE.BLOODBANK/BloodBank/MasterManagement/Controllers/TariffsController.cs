using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using ErrorOr;
using MasterManagement.Contracts;
using MasterManagement.Models;
using MasterManagement.Services.Tariffs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Shared.Audit;

namespace MasterManagement.Controllers
{

    [CustomAuthorize("LIMSFRONTOFFICE,LIMSSAMPLEMNTC,LIMSPATIENTRESULTS,LIMSPATIENTREPORTS,LIMSMISREPORTS,LIMSMasters,LIMSUserMgmt,BloodBankMgmt,BloodBankMasters,QCMgmt,MicroQCMgmt")]
    public class TariffsController : ApiController
    {
        private readonly ITariffService _TariffService;
        private readonly IAuditService _auditService;
        public TariffsController(ITariffService TariffService, IAuditService auditService)
        {
            _TariffService = TariffService;
            _auditService = auditService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Tariff>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateTariff(UpsertTariffsRequest request)
        {
            var tariffs = new List<Models.Tariff>();
            var errors = new List<Error>();
            request.Tariffs.ForEach(tariff =>
            {
                ErrorOr<Models.Tariff> requestToTariffResult = Models.Tariff.From(tariff.Id.GetValueOrDefault(), tariff);
                if (requestToTariffResult.IsError)
                {
                    errors.AddRange(requestToTariffResult.Errors);
                }
                else
                {
                    tariffs.Add(requestToTariffResult.Value);
                }
            });
            ErrorOr<List<Models.Tariff>> upsertTariffResult;
            using (var auditScope = new AuditScope<UpsertTariffRequest>(request.Tariffs, _auditService, "", new string[] { "Tariffs Update" }))
            {
                upsertTariffResult = await _TariffService.CreateTariff(tariffs);
            }
            return upsertTariffResult.Match(
                Tariffs => base.Ok(new ServiceResponse<List<Contracts.Tariff>>("", "200", upsertTariffResult.Value.Select(x => MapTariffResponse(x)).ToList())),
                errors => Problem(errors));

        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Tariff>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTariffs()
        {
            ErrorOr<List<Models.Tariff>> response = await _TariffService.GetTariffs();
            return response.Match(
                Tariffs => base.Ok(new ServiceResponse<List<Contracts.Tariff>>("", "200", Tariffs.Select(x => MapTariffResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.Tariff>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTariff(Int64 id)
        {
            ErrorOr<Models.Tariff> getTariffResult = await _TariffService.GetTariff(id);

            return getTariffResult.Match(
                Tariff => base.Ok(new ServiceResponse<Contracts.Tariff>("", "200", MapTariffResponse(Tariff))),
                errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<List<Contracts.Tariff>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertTariff(UpsertTariffsRequest request)
        {
            var tariffs = new List<Models.Tariff>();
            var errors = new List<Error>();
            request.Tariffs.ForEach(tariff =>
            {
                ErrorOr<Models.Tariff> requestToTariffResult = Models.Tariff.From(tariff.Id.GetValueOrDefault(), tariff);
                if (requestToTariffResult.IsError)
                {
                    errors.AddRange(requestToTariffResult.Errors);
                }
                else
                {
                    tariffs.Add(requestToTariffResult.Value);
                }
            });

            ErrorOr<List<Models.Tariff>> upsertTariffResult;
            using (var auditScope = new AuditScope<UpsertTariffRequest>(request.Tariffs, _auditService, "", new string[] { "Tariffs Update" }))
            {
                upsertTariffResult = await _TariffService.UpsertTariff(tariffs);
            }

            return upsertTariffResult.Match(
                Tariffs => base.Ok(new ServiceResponse<List<Contracts.Tariff>>("", "200", upsertTariffResult.Value.Select(x => MapTariffResponse(x)).ToList())),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTariff(Int64 id)
        {
            ErrorOr<Deleted> deleteTariffResult = await _TariffService.DeleteTariff(id);

            return deleteTariffResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private static Contracts.Tariff MapTariffResponse(Models.Tariff Tariff)
        {
            return new Contracts.Tariff(
                Tariff.Id,
                Tariff.ProductId,
                Tariff.ResidenceId,
                Tariff.MRP,
                Tariff.IsActive,
                Tariff.LastModifiedDateTime,
                Tariff.ServiceType
            );
        }
    }
}