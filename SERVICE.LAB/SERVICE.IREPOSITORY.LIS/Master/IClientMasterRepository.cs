using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IClientMasterRepository
    {
        List<CustomerResponse> GetClientMasterDetails(GetCustomerRequest getCustomerRequest);

        List<TblCustomer> SearchClientMaster(string ClientMasterName);

        InsertCustomerResponse InsertClientMasterDetails(PostCustomerMaster postcustomerDTO);

        int InsertClientSubMaster(PostCustomersubuserMaster postcustomerDTO);

        List<CustomerMappingDTO> GetSubCustomerDetailbyCustomer(int CustomerNo, int VenueNo, int VenueBranchNo,int IsApproval);
        List<CustomerMappingDTO> GetSubClinic(int CustomerNo, int VenueNo, int VenueBranchNo);

        int InsertSubClientMasterDetails(List<CustomerMappingDTO> subclient, int VenueNo, int VenueBranchNo, int UserID, int CustomerNo,int IsApproval, bool IsReject);

        ClientRestrictionDayResponse GetClientRestrictionDayIsValid(ClientRestrictionDay ObjRequest);
        int DocumentUploadDetails(List<DocumentUploadlst> documentUploadlst, int VenueNo, int VenueBranchNo, int UserID, int CustomerNo);
        List<ClientDocUploadDetailRes> GetClientDocumentDetails(ClientDocUploadReq Req);
        List<ClientSubUserResponse> GetclientSubUser(GetCustomerRequest getCustomerRequest);
        List<ClientSubClientMappingDTO> GetAllClientBySubClinic(int VenueNo, int VenueBranchNo);
    }
}
