using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.StartupServices;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.BloodBankPatients
{
    public class BloodBankPatientService : IBloodBankPatientService
    {
        private readonly BloodBankDataContext dataContext;
        protected readonly IConfiguration Configuration;

        public BloodBankPatientService(BloodBankDataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.Configuration = configuration;
        }

        public async Task<ErrorOr<List<BloodBankPatient>>> GetBloodBankPatientSearch(string searchText)
        {
            return await dataContext.BloodBankPatients.Where(x => x.NRICNumber.Contains(searchText)).OrderBy(x => x.LastModifiedDateTime).ToListAsync();
        }

        public async Task<ErrorOr<List<RegisteredSpecialRequirement>>> GetSpecialRequierments(List<Int64> patientIds)
        {
            return await dataContext.RegisteredSpecialRequirements.Where(x => patientIds.Any(y => y == x.PatientId) && x.Validity.Date >= DateTime.Today.Date).ToListAsync();
        }

        public async Task<ErrorOr<BloodBankPatient>> AddBloodBankPatient(BloodBankRegistration registeredPatient)
        {
            var bloodBankPatient = BloodBankPatient.Create(registeredPatient.NRICNumber, registeredPatient.PatientName, registeredPatient.PatientDOB, registeredPatient.NationalityId, registeredPatient.GenderId, registeredPatient.RaceId, registeredPatient.ResidenceStatusId, "-", 0, registeredPatient.ModifiedBy, registeredPatient.ModifiedByUserName, "", "", "");
            if (!bloodBankPatient.IsError)
            {
                var patientData = dataContext.BloodBankPatients.FirstOrDefault(x => x.NRICNumber == registeredPatient.NRICNumber);
                if (patientData != null)
                {
                    if(!registeredPatient.PatientDOB.Equals(patientData.PatientDOB))
                        patientData.PatientDOB = registeredPatient.PatientDOB;
                    dataContext.BloodBankPatients.Update(patientData);
                    await dataContext.SaveChangesAsync();
                    return patientData;
                }
                else
                {
                    dataContext.BloodBankPatients.Add(bloodBankPatient.Value);
                    await dataContext.SaveChangesAsync();
                    return bloodBankPatient;
                }
            }
            return ServiceErrors.Errors.BloodBankPatient.NotFound;
        }
    }
}