using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetTariffMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int rateListNo { get; set; }
        public int departmentNo { get; set; }
        public int type { get; set; }
        public int clientNo { get; set; }
        public int physicianNo { get; set; }
        public int filterClientNo { get; set; }
        public int IsFranchisee { get; set; } = 0;
        public int filterDoctorNo { get; set; }
    }
    public class GetTariffMasterListRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int rateListNo { get; set; }
        public int departmentNo { get; set; }
        public int IsApproval { get; set; }
        public int israteshow { get; set; }
        public int CommercialType { get; set; }
    }
    public class GetClientTariffMasterRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public int rateListNo { get; set; }
        public int departmentNo { get; set; }
        public int type { get; set; }
        public int clientNo { get; set; }
        public int physicianNo { get; set; }
        public int filterClientNo { get; set; }
        public int filterDoctorNo { get; set; }
    }
    public class GetTariffupdateRequest
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public char ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public int DeptNo { get; set; }
    }
    public class GetTariffupdateResponse
    {
        public int Id { get; set; }
        public string? JsonData { get; set; }
        public string? ColumnName { get; set; }
    }

    public class GetContractMasterListRequest
    {
        public int pageIndex { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public Int16 ContractNo { get; set; }
        public int departmentNo { get; set; }
        public int ServiceNo { get; set; }
        public int IsApproval { get; set; }
        public string ServiceType { get; set; }
        public int ismodified { get; set; }
        
    }
}

