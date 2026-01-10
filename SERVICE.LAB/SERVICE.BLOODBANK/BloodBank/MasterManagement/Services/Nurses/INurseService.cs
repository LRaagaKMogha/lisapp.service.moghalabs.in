using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using ErrorOr;

namespace MasterManagement.Services.Nurses
{
    public interface INurseService
    {
        Task<ErrorOr<Created>> CreateNurse(Nurse Nurse);
        Task<ErrorOr<List<Nurse>>> GetNurses();
        Task<ErrorOr<List<Nurse>>> GetNurses(string type);
        
        Task<ErrorOr<Nurse>> GetNurse(Int64 id);
        Task<ErrorOr<UpsertedNurse>> UpsertNurse(Nurse Nurse);
        Task<ErrorOr<Deleted>> DeleteNurse(Int64 id);
    }
}