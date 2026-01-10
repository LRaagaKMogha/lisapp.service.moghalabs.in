using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodSampleInventoryEntityConfiguration: IEntityTypeConfiguration<BloodSampleInventory>
    {
        public void Configure(EntityTypeBuilder<BloodSampleInventory> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.Identifier);
            builder.Property(x => x.Comments).IsRequired(false);
            builder.Property(x => x.IssuedToNurseId).IsRequired(false);
            builder.Property(x => x.ClinicId).IsRequired(false);
            builder.Property(x => x.PostIssuedClinicId).IsRequired(false);
            builder.Property(x => x.ReturnedByNurseId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.TransfusionDateTime).IsRequired(false);
            builder.Property(x => x.CompatibilityValidTill).IsRequired(false);
            builder.Property(x => x.IssuedDateTime).IsRequired(false);
            builder.Property(x => x.TransfusionVolume).IsRequired(false);
            builder.Property(x => x.TransfusionComments).IsRequired(false);
            builder.HasOne(e => e.BloodBankRegistration)
                .WithMany(e => e.BloodSampleInventories)
                .HasForeignKey(e => e.RegistrationId)
                .IsRequired();
        }
    }
}