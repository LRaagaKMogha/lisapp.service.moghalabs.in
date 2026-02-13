using Service.Model.FrontOffice;
using System.Collections.Generic;

namespace Service.IRepository.FrontOffice
{
    public interface IClientBranchSamplePickupRepository
    {
        List<ClientBranchSamplePickupResponse> GetClientBranchSamplePickup(ClientBranchSamplePickupRequest RequestItem);
        ClientBranchSamplePickupInsertResponse InsertClientBranchSamplePickup(ClientBranchSamplePickupInsertRequest request);
        ClientBranchSamplePickupRiderInsertResponse InsertRiderClientBranchSamplePickup(ClientBranchSamplePickupRiderInsertRequest request);
    }
}
