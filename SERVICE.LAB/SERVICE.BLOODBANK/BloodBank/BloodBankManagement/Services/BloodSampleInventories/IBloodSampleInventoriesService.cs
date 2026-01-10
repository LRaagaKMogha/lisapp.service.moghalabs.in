using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.BloodSampleInventories
{
    public interface IBloodSampleInventoriesService
    {
        Task<ErrorOr<List<BloodSampleInventory>>> UpsertBloodSampleInventoriesWithChecks(List<BloodSampleInventory> bloodSampleInventories, bool isEmergency = false, bool isRedCellsProduct = true);
        Task<ErrorOr<List<Contracts.UpdateBloodSampleInventory>>> UpsertBloodSampleInventoriesStatus(Contracts.UpdateBloodSampleInventoryStatusRequest request);
        Task<ErrorOr<List<BloodSampleInventory>>> UpsertBloodSampleInventories(List<BloodSampleInventory> inventories);
        Task<ErrorOr<bool>> UpdateInventoriesWithAssignments(List<Contracts.UpsertInventoryRequest> input);
        Task<ErrorOr<List<BloodSampleInventory>>> GetBloodSampleInventories(Contracts.FetchBloodSampleInventoriesRequest request);

        Task<ErrorOr<bool>> RemoveInventoryAssociation(Contracts.RemoveInventoryAssociationRequest request);
        Task<ErrorOr<List<Contracts.BloodBankRegistration>>> GetPatientHistory(Contracts.FetchPatientHistoryRequest request);
    }
}