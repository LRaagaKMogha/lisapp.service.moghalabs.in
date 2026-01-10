using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class GetServiceDetails
    {
        public int RowNo { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceTypeName { get; set; }
        public int ServiceNo { get; set; }
        public string? TestName { get; set; }
        public string? MainDeptName { get; set; }
        public Int16 MainDeptNo { get; set; }
        public string? DepartmentName { get; set; }
        public int DeptNo { get; set; }
        public Int16 DeptOrder { get; set; }
        public int Rate { get; set; }
        public int SequenceNo { get; set; }
        public bool IsChecked { get; set; }

    }
    public partial class TblServiceOrder
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public List<ServiceRateUpdateDTO>?testServiceXML { get; set; }
    }
    public class ServiceOrderMasterRequest
    {
        public Int16 MainDeptNo { get; set; }
        public int DeptNo { get; set; }
        public int ServiceNo { get; set; }
        public string? ServiceType { get; set; } 
        public int VenueNo { get; set; }    
    }
    public partial class ServiceRateUpdateDTO
    {
        public int DeptNo { get; set; }
        public int ServiceNo { get; set; }
        public string? ServiceType { get; set; } 
        public int SequenceNo { get; set; }
       public decimal Rate { get; set; }            

    }
   
    public class ServiceOrderMasterResponse
    {
        public bool? Status { get; set; }
    }
    

}
