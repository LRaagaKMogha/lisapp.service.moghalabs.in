using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterManagement.Models
{
    public class ProductSpecialRequirement
    {
        public Int64 ProductId {get; set; }
        public Product Product {get; set; }
        public Int64 SpecialRequirementId { get; set; }
        public Lookup SpecialRequirement { get; set; }

        public ProductSpecialRequirement() 
        {

        }

        public ProductSpecialRequirement(Int64 specialRequirementId) 
        {
            this.SpecialRequirementId = specialRequirementId;
        }

        public ProductSpecialRequirement(Int64 productId, Int64 specialRequirementId)
        {
            this.ProductId = productId; 
            this.SpecialRequirementId = specialRequirementId;
        }
    }
}