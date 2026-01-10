using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Models
{
    public class RegisteredProduct
    {
        public Int64 Identifier { get; set; }
        public Int64 ProductId { get; set; }
        public decimal MRP { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }

        public BloodBankRegistration BloodBankRegistration { get; set; }
        public Int64 BloodBankRegistrationId { get; set; }

        public RegisteredProduct() 
        {

        }

        public RegisteredProduct(Int64 productId, decimal mrp, int unit, decimal price)
        {
            ProductId = productId;
            MRP = mrp;
            Unit = unit; 
            Price = price;
        }
    }
}