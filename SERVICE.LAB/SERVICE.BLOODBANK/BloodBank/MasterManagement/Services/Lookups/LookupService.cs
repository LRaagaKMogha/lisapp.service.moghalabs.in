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
using Shared;
namespace MasterManagement.Services.Lookups
{
    public class LookupService : ILookupService
    {
        private readonly MasterDataContext dataContext;

        public LookupService(MasterDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ErrorOr<Created>> CreateLookup(Lookup Lookup, User user)
        {
            var now = DateTime.Now;
            var records = dataContext.Lookups.Where(x => x.Name.ToLower() == Lookup.Name.ToLower() || x.Code.ToLower() == Lookup.Code.ToLower()).ToList();
            if (records.Any(x => x.Type == Lookup.Type && x.Type != "media"))
                return Errors.Lookup.LookupExists;
            Lookup.CreatedDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
            Lookup.CreatedBy = user.UserNo;
            dataContext.Lookups.Add(Lookup);
            await this.dataContext.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteLookup(Int64 id)
        {
            var LookupToUpdate = await dataContext.Lookups.FindAsync(id);
            if (LookupToUpdate != null)
            {
                LookupToUpdate.IsActive = false;
                await dataContext.SaveChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<Lookup>>> GetLookups()
        {
            return await dataContext.Lookups.ToListAsync();
        }

        public async Task<ErrorOr<List<Lookup>>> GetLookups(string type, string searchText)
        {
            var query = dataContext.Lookups.Where(x => x.Type == type);
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchText.ToLower()) || x.Code.ToLower().Trim().Contains(searchText.ToLower()) || x.Description.ToLower().Trim().Contains(searchText.ToLower()));
            }
            return await query.ToListAsync();
        }

        public async Task<ErrorOr<Lookup>> GetLookup(Int64 id)
        {
            var data = await dataContext.Lookups.FindAsync(id);
            if (data != null) return data;
            return Errors.Lookup.NotFound;
        }

        public async Task<ErrorOr<UpsertedLookup>> UpsertLookup(Lookup Lookup, User user)
        {
            var now = DateTime.Now;
            var isNewlyCreated = false;
            var data = dataContext.Lookups.FirstOrDefault(x => x.Id == Lookup.Id);
            if (data != null)
            {

                data.Name = Lookup.Name;
                data.Description = Lookup.Description;
                data.Code = Lookup.Code;
                data.IsActive = Lookup.IsActive;
                data.LastModifiedDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
                data.ModifiedBy = user.UserNo;
                dataContext.Lookups.Update(data);
            }
            else
            {

                isNewlyCreated = true;
                data.Name = Lookup.Name;
                data.Description = Lookup.Description;
                data.Code = Lookup.Code;
                data.IsActive = Lookup.IsActive;
                data.CreatedDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
                data.CreatedBy = user.UserNo;
                await dataContext.Lookups.AddAsync(data);
            }
            await this.dataContext.SaveChangesAsync();
            return new UpsertedLookup(isNewlyCreated);


        }


    }
}