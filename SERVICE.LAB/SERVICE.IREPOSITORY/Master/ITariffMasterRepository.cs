using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository
{
   public interface ITariffMasterRepository
    {
        List<GetTariffMasterResponse> GetTariffMasterDetails(GetTariffMasterRequest getRequest);
        List<Getservices> GetTariffService(GetTariffMasterRequest getRequest);
        InsertTariffMasterResponse InsertTariffMasterDetails(InsertTariffMasterRequest tariffMasteritem);
        List<GetTariffMasterListResponse> GetTariffMasterList(GetTariffMasterListRequest getRequest);
        List<TariffMastServicesResponse> GetTariffMasterServiceList(GetTariffMasterListRequest getRequest);
        TariffMasterInsertResponse InsertTariffMaster(InsertTariffMasterRequest tariffMasteritem);
        List<GetClientTariffMasterListResponse> GetClientTariffMasterList(GetClientTariffMasterRequest getRequest);
        CTMInsertResponse InsertClientTariffMaster(InsertCTMRequest tariffMasteritem);
        List<ClientTariffServicesResponse> GetClientTariffServiceList(GetClientTariffMasterRequest getRequest);
        GetTariffupdateResponse GetTariffupdateList(GetTariffupdateRequest getRequest);
        List<GetContractRes> GetContractMaster(GetContractReq req);
        InsertContractRes InserContractMaster(InsertContractReq req);
        List<TariffMastServicesResponse> GetContractMasterServiceList(GetContractMasterListRequest getRequest);
        List<ContractVsCustomerMap> GetContractVsClient(GetContractVsClientReq getRequest);
        InsTariffRes InsertClienttTariffMap(InsTariffReq req);
        List<GetTariffRes> GetClienttTariffMap(GetTariffReq req);
        List<TariffMastServicesResponse> GetRefSplRateServiceList(GetContractMasterListRequest getRequest);
        List<GetReflstRes> GetReflst(GetContractReq req);
        InsertContractRes InsertReferrerlst(InsertReflstReq req);
        List<Tariffdeptdisreq> GetTariffDeptDiscount(Tariffdeptdis req);
        RateHistoryServiceResponse GetPriceHistory(RateHistoryServiceRequest req);
        List<BaseRateResponse> GetBasePrice(RateHistoryServiceRequest req);
        int InsertBaseRate(List<BaseRateResponse> req);
    }
}