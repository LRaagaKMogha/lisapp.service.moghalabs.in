using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using ErrorOr;

namespace MasterManagement.Services.Tariffs
{
    public interface ITariffService
    {
        Task<ErrorOr<List<Tariff>>> CreateTariff(List<Tariff> Tariffs);
        Task<ErrorOr<List<Tariff>>> GetTariffs();
        Task<ErrorOr<Tariff>> GetTariff(Int64 id);
        Task<ErrorOr<List<Tariff>>> UpsertTariff(List<Tariff> Tariffs);
        Task<ErrorOr<Deleted>> DeleteTariff(Int64 id);
    }
}