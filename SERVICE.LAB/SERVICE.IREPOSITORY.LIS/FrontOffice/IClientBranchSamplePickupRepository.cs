using DEV.Model;
using DEV.Model.FrontOffice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.FrontOffice
{
    public interface IClientBranchSamplePickupRepository
    {
        List<ClientBranchSamplePickupResponse> GetClientBranchSamplePickup(ClientBranchSamplePickupRequest RequestItem);
        ClientBranchSamplePickupInsertResponse InsertClientBranchSamplePickup(ClientBranchSamplePickupInsertRequest request);
        ClientBranchSamplePickupRiderInsertResponse InsertRiderClientBranchSamplePickup(ClientBranchSamplePickupRiderInsertRequest request);

    }
}
