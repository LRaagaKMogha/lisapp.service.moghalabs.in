using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BloodBankManagement.Contracts;
using BloodBankManagement.Helpers;
using BloodBankManagement.Models;
using BloodBankManagement.Services.BloodBankInventories;
using BloodBankManagement.Services.BloodBankRegistrations;
using BloodBankManagement.Services.BloodSampleInventories;
using BloodBankManagement.Services.BloodSampleResults;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly BloodBankDataContext dataContext;
        protected readonly IBloodBankRegistrationService BloodBankRegistrationService;
        protected readonly IBloodSampleResultService bloodSampleResultService;
        protected readonly IBloodSampleInventoriesService bloodSampleInventoryService;
        protected readonly IBloodBankInventoryService bloodBankInventoryService;

        public ReportsService(BloodBankDataContext dataContext, IBloodBankRegistrationService bloodBankRegistrationService, IBloodSampleResultService _bloodSampleResultService, IBloodBankInventoryService _bloodBankInventoryService, IBloodSampleInventoriesService _bloodSampleInventoryService)
        {
            this.dataContext = dataContext;
            this.BloodBankRegistrationService = bloodBankRegistrationService;
            this.bloodSampleResultService = _bloodSampleResultService;
            this.bloodBankInventoryService = _bloodBankInventoryService;
            this.bloodSampleInventoryService = _bloodSampleInventoryService;
        }

        public async Task<ErrorOr<List<Models.BloodBankRegistration>>> GetBillingHistory(FetchPatientBillingRequest request)
        {
            Expression<Func<Models.BloodBankRegistration, bool>> NRICNumber = e => e.NRICNumber == request.NRICNumber;
            Expression<Func<Models.BloodBankRegistration, bool>> Status = e => e.Status == request.Status;
            var query = dataContext.BloodBankRegistrations.Where(x => x.IsActive == true && x.LastModifiedDateTime >= request.StartDate && x.LastModifiedDateTime <= request.EndDate);
            if (!string.IsNullOrEmpty(request.NRICNumber)) query = query.Where(NRICNumber);
            if (!string.IsNullOrEmpty(request.Status)) query = query.Where(Status);

            return await query.OrderByDescending(x => x.LastModifiedDateTime).Include(x => x.Billings).Include(x => x.BloodSampleInventories).Include(x => x.Products).Include(x => x.Transactions).Include(x => x.Results).AsSplitQuery().ToListAsync();
        }

        public async Task<ErrorOr<List<BloodBankInventory>>> GetBloodBankInventories(FetchBloodBankInventoriesRequest request)
        {
            var statuses = request.Statuses != null ? request.Statuses : new List<string>();
            if (string.IsNullOrEmpty(request.DonationId))
            {
                if (statuses.Any(x => x == "Expired"))
                {
                    var filterStatus = new List<string>() { "available", "OnHold", "Assigned"};
                    return await dataContext.BloodBankInventories.Where(x => x.ExpirationDateAndTime >= request.StartDate && x.ExpirationDateAndTime < request.EndDate && filterStatus.Any(y => y == x.Status)).ToListAsync();
                }
                else
                {
                    var query = from inventory in dataContext.BloodBankInventories
                                from transaction in inventory.Transactions
                                where transaction.LastModifiedDateTime >= request.StartDate && transaction.LastModifiedDateTime <= request.EndDate && statuses.Any(y => y == transaction.InventoryStatus)
                                select inventory;
                    var response = await query.ToListAsync();
                    return response.DistinctBy(x => x.Identifier).ToList();
                }

            }
            else
            {
                if (request.DonationId == "-")
                {
                    return await dataContext.BloodBankInventories.Where(x => statuses.Any(y => y == x.Status)).ToListAsync();
                }
                return await dataContext.BloodBankInventories.Where(x => x.DonationId == request.DonationId).ToListAsync();
            }
        }
        public async Task<ErrorOr<List<Contracts.BloodBankRegistration>>> GetRequestsHistory(FetchPatientHistoryRequest request)
        {
            return await this.bloodSampleInventoryService.GetPatientHistory(request);
        }

        public async Task<ErrorOr<List<BloodBankInventoryTransaction>>> GetBloodBankInventoryTransactions(Int64 inventoryId)
        {
            return await dataContext.bloodBankInventoryTransactions.Where(x => x.InventoryId == inventoryId).OrderByDescending(x => x.LastModifiedDateTime).ToListAsync();
        }

    }
}
