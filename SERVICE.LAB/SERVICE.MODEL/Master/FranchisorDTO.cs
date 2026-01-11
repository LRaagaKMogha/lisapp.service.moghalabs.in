using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Master
{
    public class GetFranchiseResponse
    {
        public int FranchisorNo { get; set; }        
        public int FranchiseNo { get; set; }  
        public string FranchiseName { get; set; } 
        public string FranchiseNameDisplayText { get; set; } 
        public bool Status { get; set; }
        public bool IsFranchise { get; set; } 
    }
    public class GetFranchiseRevenueSharingByServiceRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int? DepartmentNo { get; set; }
        public string? ServiceType { get; set; }
        public int? ServiceNo { get; set; }
        public int FranchiseNo { get; set; }
        public int FranchisorNo { get; set; }
    }
    public class FranchiseRevenueSharingServiceDto
    {
        public int Sno { get; set; }
        public int TestNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string TestType { get; set; }
        public decimal RevenueValue { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public int RateServicesNo { get; set; }
        public string RevenueType { get; set; }
    }

    public class FranchiseRevenueSharingInsertDTO
    {
        public int VenueNo { get; set; }
        public int UserNo { get; set; }
        public int FranchisorNo { get; set; }
        public int FranchiseNo { get; set; }
        public List<FranchiseRevenueSharingItemDTO> FranchiseRevenueSharingList { get; set; }
    }

    public class FranchiseRevenueSharingItemDTO
    {
        public int ServiceRevenueID { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string RevenueType { get; set; }
        public decimal RevenueValue { get; set; }
    }

}
