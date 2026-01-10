using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.BloodBankPatients
{
    public interface IBloodBankPatientService
    {
        Task<ErrorOr<List<BloodBankPatient>>> GetBloodBankPatientSearch(string searchText);
        Task<ErrorOr<List<RegisteredSpecialRequirement>>> GetSpecialRequierments(List<Int64> patientIds);
        Task<ErrorOr<BloodBankPatient>> AddBloodBankPatient(BloodBankRegistration registeredPatient);
    }
}