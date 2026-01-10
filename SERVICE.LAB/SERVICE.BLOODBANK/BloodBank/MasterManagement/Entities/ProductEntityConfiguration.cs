using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MasterManagement.Entities
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired(true);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.EffectiveFromDate).IsRequired(true);
            builder.Property(x => x.EffectiveToDate).IsRequired(false);
            builder.Property(x => x.MinCount).IsRequired(true);
            builder.Property(x => x.MaxCount).IsRequired(true);
            builder.Property(x => x.ThresholdCount).IsRequired(true);
            builder.HasNoDiscriminator().HasKey(da => da.Id);
        }
    }
}