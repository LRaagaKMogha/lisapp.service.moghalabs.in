using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class RegisteredSpecialRequirementsEntityConfiguration : IEntityTypeConfiguration<RegisteredSpecialRequirement>
    {
        public void Configure(EntityTypeBuilder<RegisteredSpecialRequirement> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder
                .HasOne(e => e.BloodBankRegistration)
                .WithMany(e => e.SpecialRequirements)
                .HasForeignKey(e => e.RegistrationId)
                .IsRequired();
        }
    }
}