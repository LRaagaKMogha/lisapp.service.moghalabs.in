using Service.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
   public class InsertTariffMasterRequest
    {
        public int RateListNo { get; set; }
        public string RateListName { get; set; }
        public int? ClientNo { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public int? CurrencyNo { get; set; }
        public bool? IsDefault { get; set; }
        public int? SequenceNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? mappingType { get; set; }
        public int OldRateListNo { get; set; }
        public int BaseRateListNo { get; set; }
        public int IsBasePriceChanged { get; set; }
        public int IsApproval { get; set; }
        public bool IsReject { get; set; }
        public string RejectReason { get; set; }
        public int OldRateListAppNo { get; set; }
        public int AppRateListAppNo { get; set; }

        public List<tariffServiceDetails> serviceDetails { get; set; }
        public List<deptDetails> deptDetails { get; set; }
    }

    public class tariffServiceDetails
    {      
        public int? testNo { get; set; }
        public string testType { get; set; }
        public decimal? rate { get; set; }
        public int? departmentNo { get; set; }
        public decimal discountAmount { get; set; }
        public decimal revenueAmount { get; set; }
        public int rateServicesNo { get; set; }
        public int? customerNo { get; set; }
        public int? physicianNo { get; set; }
        public bool isChecked { get; set; }
        public string extCode { get; set; }
        public decimal oldAmount { get; set; }
    }

    public class deptDetails
    {
        public int? deptDiscountNo { get; set; }
        public int ratelistNo { get; set; }
        public int deptNo { get; set; }
        public int? discount { get; set; }
        public int oldratelistNo { get; set; }
    }
    public class InsertCTMRequest
    {
        public int RateListNo { get; set; }
        public string RateListName { get; set; }
        public int? ClientNo { get; set; }
        public int? PhysicianNo { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public int? CurrencyNo { get; set; }
        public bool? IsDefault { get; set; }
        public int? SequenceNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? mappingType { get; set; }
        public int OldRateListNo { get; set; }

        public List<tariffServiceDetails> serviceDetails { get; set; }
    }

    public class InsertContractReq
    {
        public Byte VenueNo { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Int16? ContractNo { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? UserNo { get; set; }
        public int IsApproval { get; set; }
        public bool IsReject { get; set; }
        public string RejectReason { get; set; }
        public Int16 OldContractNo { get; set; }
        public List<ContractListXml> serviceDetails { get; set; }
        public List<ContractVsClient> ContractVsClient { get; set; }
    }

    public class ContractVsClient
    {
        public Int16 contractMasterNo { get; set; }
        public int? clientNo { get; set; }
        public Boolean status { get; set; }
        public Boolean isChecked  { get; set; }
    }

    public class ContractListXml 
    {
        public int contractDtlsNo  { get; set; }
        public int? serviceNo { get; set; }
        public string serviceType { get; set; }
        public decimal? Amount { get; set; }
        public bool isChecked { get; set; }
        public decimal oldAmount { get; set; }
    }

    public class InsertContractRes
    {
        public int resultStatus { get; set; }
    }
    public class InsertBaseRateRes
    {
        public int result { get; set; }
    }

    public class InsTariffReq
    {
        public Int16 ClientTariffMapNo { get; set; }
        public byte RefTypeNo { get; set; }
        public int ReferrerNo { get; set; }
        public int RateListNo { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? UserNo { get; set; }
        public int VenueNo { get; set; }
    }
    public class InsTariffRes
    {
        public Int16 ClientTariffMapNo { get; set; }
    }
    public class GetTariffReq
    {
        public Int16 ClientTariffMapNo { get; set; }
        public byte RefTypeNo { get; set; }
        public int ReferrerNo { get; set; }
        public int RateListNo { get; set; }
        public int pageIndex { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class GetTariffRes
    {
        public Int16 ClientTariffMapNo { get; set; }
        public byte RefTypeNo { get; set; }
        public int ReferrerNo { get; set; }
        public int RateListNo { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public string CustomerName { get; set; }
        public string RateListName { get; set; }
        public bool? Status { get; set; }
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public string EffFm { get; set; }
        public string EffTo { get; set; }
        public string RefTypeDesc { get; set; }
        public string ReferrerName { get; set; }
        public string physicianName { get; set; }
    }

    public class InsertReflstReq
    {
        public Byte VenueNo { get; set; }
        public byte RefTypeNo { get; set; }
        public int ReferrerNo { get; set; }
        public Int16? RefSplNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? Status { get; set; }
        public int? UserNo { get; set; }
        public List<SplPriceListXml> serviceDetails { get; set; }
    }

    public class SplPriceListXml
    {
        public Int16 RefSplNo { get; set; }
        public int? serviceNo { get; set; }
        public string serviceType { get; set; }
        public decimal? Amount { get; set; }
    }

    public class Tariffdeptdis 
    {
        public int RateListNo { get; set; }
        public int VenueNo { get; set; }
        public int IsApproval { get; set; }
    }
    public class Tariffdeptdisreq
    {
        public int RowNo { get; set; }
        public int DepartmentNo { get; set; }
        public int DeptDiscountNo { get; set; }
        public int Discount { get; set; }
        public string? DepartmentName { get; set; }
        public int RatelistNo { get; set; }
        public int OldDeptDiscountNo { get; set; }
        public int OldratelistNo { get; set; }
    }
    public class RateHistoryServiceRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int RateListNo { get; set; }
        public int ContractNo { get; set; }
        public int RefTypeNo { get; set; }
        public int ReferrerNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string PageCode { get; set; }
        public int UserNo { get; set; }
    }
    public class RateHistoryServiceResponse
    {
        public int RowNo { get; set; }
        public string EntityName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public List<PriceHistoryService> RateHistory { get; set; }
    }
    public class PriceHistoryServiceResponse
    {
        public int RowNo { get; set; }
        public string EntityName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string RateHistory { get; set; }
    }
    public class BaseRateResponse
    {
        public int BaseRateNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public decimal BaseAmount { get; set; }
        public int venueNo { get; set; }
        
    }
           
 
    public class PriceHistoryService
    {
        public int RowNo { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public decimal Rate { get; set; }
    }
}

