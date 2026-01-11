using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class CustomerMappingDTO
    {
        public long Row_Num { get; set; }
        public bool Isselect { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }

    }
    public class ClientSubClientMappingDTO
    {
        public string SubCustomerCode { get; set; }
        public string SubCustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public bool Status { get; set; }
        public bool IsTaxable { get; set; }

    }
    public class ReqCustomerMappingDTO
    {
        public int CustomerNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class CustomerReturnResponse
    {
        public int CustomerNo { get; set; }

    }
}

