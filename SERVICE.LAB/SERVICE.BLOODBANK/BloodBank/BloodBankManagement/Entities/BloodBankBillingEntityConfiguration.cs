using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodBankBillingEntityConfiguration : IEntityTypeConfiguration<BloodBankBilling>
    {
        public void Configure(EntityTypeBuilder<BloodBankBilling> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.MRP)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(e => e.BloodBankRegistration)
                .WithMany(e => e.Billings)
                .HasForeignKey(e => e.BloodBankRegistrationId)
                .IsRequired()
                ;


        }
    }
}