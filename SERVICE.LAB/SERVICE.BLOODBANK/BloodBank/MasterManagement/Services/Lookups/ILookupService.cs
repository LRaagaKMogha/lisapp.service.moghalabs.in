using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using ErrorOr;
using Shared;

namespace MasterManagement.Services.Lookups
{
    public interface ILookupService
    {
        Task<ErrorOr<Created>> CreateLookup(Lookup Lookup, User user);
        Task<ErrorOr<List<Lookup>>> GetLookups();
        Task<ErrorOr<List<Lookup>>> GetLookups(string type, string searchText);
        
        Task<ErrorOr<Lookup>> GetLookup(Int64 id);
        Task<ErrorOr<UpsertedLookup>> UpsertLookup(Lookup Lookup, User User);
        Task<ErrorOr<Deleted>> DeleteLookup(Int64 id);
    }
}