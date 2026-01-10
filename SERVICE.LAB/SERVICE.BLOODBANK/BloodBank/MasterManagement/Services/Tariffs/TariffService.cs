using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using MasterManagement.Helpers;
using MasterManagement.Models;
using MasterManagement.ServiceErrors;
using Microsoft.EntityFrameworkCore;

namespace MasterManagement.Services.Tariffs
{
    public class TariffService : ITariffService
    {
        private readonly MasterDataContext dataContext;
        public TariffService(MasterDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ErrorOr<List<Tariff>>> CreateTariff(List<Tariff> Tariffs)
        {
            foreach (var tariff in Tariffs)
            {
                if (dataContext.Tariffs.Where(x => x.Id == tariff.Id && x.ResidenceId == tariff.ResidenceId && x.ServiceType == tariff.ServiceType).Count() > 0)
                    return Errors.Tariff.TariffExists;
                dataContext.Tariffs.Add(tariff);
            }
            await this.dataContext.SaveChangesAsync();
            return Tariffs;
        }

        public async Task<ErrorOr<Deleted>> DeleteTariff(Int64 id)
        {
            var TariffToUpdate = await dataContext.Tariffs.FindAsync(id);
            if (TariffToUpdate != null)
            {
                TariffToUpdate.IsActive = false;
                await dataContext.SaveChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<Tariff>> GetTariff(Int64 id)
        {
            var data = dataContext.Tariffs.FirstOrDefault(x => x.Id == id);
            if (data != null) return data;
            return Errors.Tariff.NotFound;
        }

        public async Task<ErrorOr<List<Tariff>>> GetTariffs()
        {
            return await dataContext.Tariffs.ToListAsync();

        }

        public async Task<ErrorOr<List<Tariff>>> UpsertTariff(List<Tariff> Tariffs)
        {
            for(var i = 0; i < Tariffs.Count; i++)
            {
                var tariff = Tariffs[i];
                var currentTariff = dataContext.Tariffs.FirstOrDefault(x => x.ProductId == tariff.ProductId && x.ResidenceId == tariff.ResidenceId && x.ServiceType == tariff.ServiceType);
                if (currentTariff != null)
                {
                    currentTariff.MRP = tariff.MRP;
                    currentTariff.IsActive = tariff.IsActive;
                    dataContext.Tariffs.Update(currentTariff);
                }
                else
                {
                    await dataContext.Tariffs.AddAsync(tariff);
                }
            };

            await this.dataContext.SaveChangesAsync();
            return Tariffs;
        }
    }
}
