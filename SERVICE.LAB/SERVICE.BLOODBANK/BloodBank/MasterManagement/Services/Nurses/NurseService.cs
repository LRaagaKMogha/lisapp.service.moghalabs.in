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
namespace MasterManagement.Services.Nurses
{
    public class NurseService : INurseService
    {
        private readonly MasterDataContext dataContext;

        public NurseService(MasterDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ErrorOr<Created>> CreateNurse(Nurse Nurse)
        {
            var records = dataContext.Nurses.Where(x => x.Name.ToLower() == Nurse.Name.ToLower() || x.EmployeeId.ToLower() == Nurse.EmployeeId.ToLower()).ToList();
            if (records.Any() )
                return Errors.Nurse.NurseExists;
            dataContext.Nurses.Add(Nurse);
            await this.dataContext.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteNurse(Int64 id)
        {
            var NurseToUpdate = await dataContext.Nurses.FindAsync(id);
            if (NurseToUpdate != null)
            {
                NurseToUpdate.IsActive = false;
                await dataContext.SaveChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<Nurse>>> GetNurses()
        {
            return await dataContext.Nurses.ToListAsync();
        }

        public async Task<ErrorOr<List<Nurse>>> GetNurses(string type)
        {
           return await dataContext.Nurses.ToListAsync();
        }

        public async Task<ErrorOr<Nurse>> GetNurse(Int64 id)
        {
            var data = await dataContext.Nurses.FindAsync(id);
            if (data != null) return data;
            return Errors.Nurse.NotFound;
        }

        public async Task<ErrorOr<UpsertedNurse>> UpsertNurse(Nurse Nurse)
        {
            var isNewlyCreated = false;
            if (dataContext.Nurses.Where(x => x.Id == Nurse.Id).Count() > 0)
            {
                dataContext.Nurses.Update(Nurse);
            }
            else
            {
                isNewlyCreated = true;
                await dataContext.Nurses.AddAsync(Nurse);
            }
            await this.dataContext.SaveChangesAsync();
            return new UpsertedNurse(isNewlyCreated);
        }


    }
}