using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Models;
using BloodBankManagement.Helpers;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using BloodBankManagement.Contracts;
using System.Data;
using Microsoft.Data.SqlClient;
using BloodBankManagement.Services.BloodBankRegistrations;

namespace BloodBankManagement.Services.SampleReceiving
{
    public class SampleReceivingService : ISampleReceivingService
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IMapper mapper;
        protected readonly IConfiguration Configuration;
        protected readonly IBloodBankRegistrationService BloodBankRegistrationService;

        public SampleReceivingService(BloodBankDataContext dataContext, IMapper mapper, IConfiguration configuration, IBloodBankRegistrationService bloodBankRegistrationService)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.Configuration = configuration;
            this.BloodBankRegistrationService = bloodBankRegistrationService;
        }


        public async Task<ErrorOr<List<Models.BloodBankRegistration>>> GetSampleReceivingList()
        {
            return await dataContext.BloodBankRegistrations.Where(x => x.Status == "Registered").Include(x => x.Products).Include(x => x.Results).Include(x => x.SpecialRequirements).ToListAsync();
        }

        public async Task<ErrorOr<List<Models.BloodSample>>> SaveBloodSamplesList(List<Models.BloodSample> request)
        {
            for(var i = 0; i < request.Count; i++)
            {
                var bloodsample = request[i];
                var sampleInfo = dataContext.BloodSamples.FirstOrDefault(x => x.Identifier == bloodsample.Identifier);
                if (sampleInfo == null)
                {
                    await dataContext.BloodSamples.AddAsync(bloodsample);
                }
            };
            await this.dataContext.SaveChangesAsync();
            var registrationIds = request.Select(x => x.RegistrationId).Distinct().ToList();
            await this.BloodBankRegistrationService.UpdateRegistrationStatus(registrationIds, "SampleReceived", request.First().ModifiedBy, request.First().ModifiedByUserName);
            return request;
        }

        public async Task<ErrorOr<List<Models.BloodSample>>> GetActiveSamplesForPatient(long patientId)
        {
            return await dataContext.BloodSamples.Where(x => x.PatientId == patientId && x.LastModifiedDateTime.Date.AddDays(3) >= DateTime.Now.Date && x.ParentRegistrationId.GetValueOrDefault() == 0).ToListAsync();
        }

        public async Task<ErrorOr<List<string>>> GetBarCodes(Int64 registrationId, int numberOfBarCodes)
        {
            var response = new List<string>();
            var today = DateTime.Now;
            for (var i = 0; i < numberOfBarCodes; i++)
            {
                var id = GetNextSequenceNumber().ToString().PadLeft(4, '0');
                var barcode = "99" + today.Year.ToString().Substring(2) + today.Month.ToString().PadLeft(2,'0') + today.Day.ToString().PadLeft(2, '0') + id;
                response.Add(barcode);
            }
            return response;
        }

        private int GetNextSequenceNumber()
        {
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            dataContext.Database.ExecuteSqlRaw("set @result = NEXT VALUE FOR BarCodeId", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

    }
}
