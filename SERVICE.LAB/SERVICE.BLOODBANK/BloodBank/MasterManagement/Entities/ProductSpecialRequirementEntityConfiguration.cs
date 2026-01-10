using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MasterManagement.Entities
{
    public class ProductSpecialRequirementEntityConfiguration : IEntityTypeConfiguration<ProductSpecialRequirement>
    {
        public void Configure(EntityTypeBuilder<ProductSpecialRequirement> builder)
        {
            builder.HasKey(ps => new { ps.ProductId, ps.SpecialRequirementId });
            builder.HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSpecialRequirements)
                .HasForeignKey(ps => ps.ProductId);

            builder.HasOne(ps => ps.SpecialRequirement)
                .WithMany(p => p.ProductSpecialRequirements)
                .HasForeignKey(ps => ps.SpecialRequirementId);
        }
    }
}