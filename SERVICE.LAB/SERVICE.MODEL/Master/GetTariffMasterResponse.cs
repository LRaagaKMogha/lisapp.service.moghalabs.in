using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetTariffMasterResponse
    {
        public int Sno { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int rateListNo { get; set; }
        public string rateListName { get; set; }
        public string effectiveFrom { get; set; }
        public string effectiveTo { get; set; }
        public int sequenceNo { get; set; }
        public bool status { get; set; }
        public int type { get; set; }
        public string typeValue { get; set; }
        public int typeNo { get; set; }
        public string typeName { get; set; }
        public string extCode { get; set; }
        //public string ModifiedBy { get; set; }
        //public string ModifiedOn { get; set; }
        //public int OldRateListNoApproval { get; set; }
        //public string? ApprovedBy { get; set; }
        //public string ApprovedOn { get; set; }
        //public string RejectedBy { get; set; }
        //public string RejectedOn { get; set; }
        //public string RejectReason { get; set; }
    }
    public class GetServices
    {
        public int? sNo { get; set; }
        public int? testNo { get; set; }
        public string testCode { get; set; }
        public string testName { get; set; }
        public string testType { get; set; }
        public decimal? amount { get; set; }
        public decimal? discountAmount { get; set; }
        public int? departmentNo { get; set; }
        public string departmentName { get; set; }
        public int? rateServicesNo { get; set; }
        public bool isChecked { get; set; }
        public decimal? calculateAmount { get; set; }
        public decimal revenueAmount { get; set; }
        public string extCode { get; set; }
    }
    public class GetTariffMasterListResponse
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int RateListNo { get; set; }
        public string RateListName { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public string EffectiveFromDt { get; set; }
        public string EffectiveToDt { get; set; }
        public int SequenceNo { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int OldRateListNoApproval { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedOn { get; set; }
        public string RejectReason { get; set; }
        public int CommercialType { get; set; }
        public string CommercialValue { get; set; }
    }
    public class TariffMastServicesResponse
    {
        public int? sNo { get; set; }
        public int? testNo { get; set; }
        public string testCode { get; set; }
        public string testName { get; set; }
        public string testType { get; set; }
        public decimal? amount { get; set; }
        public decimal? discountAmount { get; set; }
        public int? departmentNo { get; set; }
        public string departmentName { get; set; }
        public int? rateServicesNo { get; set; }
        public bool isChecked { get; set; }
        public int? OldRateServicesApprovalNo { get; set; }
        public decimal? calculateAmount { get; set; }
        public bool isSpecialCategory { get; set; }
    }
    public class GetClientTariffMasterListResponse
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int RateListNo { get; set; }
        public string RateListName { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }
        public int SequenceNo { get; set; }
        public bool Status { get; set; }
        public int Type { get; set; }
        public string TypeValue { get; set; }
        public int TypeNo { get; set; }
        public string TypeName { get; set; }
    }
    public class ClientTariffServicesResponse
    {
        public int? sNo { get; set; }
        public int? testNo { get; set; }
        public string testCode { get; set; }
        public string testName { get; set; }
        public string testType { get; set; }
        public decimal? amount { get; set; }
        public decimal? discountAmount { get; set; }
        public int? departmentNo { get; set; }
        public string departmentName { get; set; }
        public int? rateServicesNo { get; set; }
        public bool isChecked { get; set; }
    }

    public class GetContractRes
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public Int16 ContractMasterNo { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public bool Status { get; set; }
    }

    public class GetContractReq
    {
        public byte VenueNo { get; set; }
        public Int16 ContractNo { get; set; }
        public int pageIndex { get; set; }

    }
    public class GetContractVsClientRes
    {
        public int Sno { get; set; }
        public Int16 ContractClientNo { get; set; }
        public Int16 ContractMasterNo { get; set; }
        public int ClientNo { get; set; }
        public string ClientName { get; set; }
        public bool ClientStatus { get; set; }
        public bool SubClientStatus { get; set; }
        public bool IsChecked { get; set; }
        public int MapCustomerNo { get; set; }
        public string SubCustomerName { get; set; }
        public int SubCustomerNo { get; set; }
    }
    public class ContractVsCustomerMap
    {
        public int Sno { get; set; }
        public Int16 ContractClientNo { get; set; }
        public Int16 ContractMasterNo { get; set; }
        public int ClientNo { get; set; }
        public string ClientName { get; set; }
        public bool Status { get; set; }
        public List<ContractVsSubCustomerMap> SubCustomerMap { get; set; }
    }
    public class ContractVsSubCustomerMap
    {
        public int RowNo { get; set; }
        public int MapCustomerNo { get; set; }
        public string SubCustomerName { get; set; }
        public int SubCustomerNo { get; set; }
        public bool Status { get; set; }

    }

    public class GetContractVsClientReq
    {
        public int VenueNo { get; set; }
        public Int16 ContractNo { get; set; }
        public int IsApproval { get; set; }


    }
    public class GetReflstRes
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public Int16 RefSplNo { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public int ReferrerNo { get; set; }
    }
}


