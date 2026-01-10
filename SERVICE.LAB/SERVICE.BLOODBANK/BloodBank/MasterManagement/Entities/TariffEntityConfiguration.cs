using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterManagement.Entities
{
    public class TariffEntityConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.Property(x => x.ProductId).IsRequired(true);
            builder.Property(x => x.ResidenceId).IsRequired(true);
            builder.Property(x => x.MRP)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            builder.HasNoDiscriminator().HasKey(da => da.Id);
        }
    }
}