using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodSampleResultEntityConfiguration : IEntityTypeConfiguration<BloodSampleResult>
    {
        public void Configure(EntityTypeBuilder<BloodSampleResult> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.Identifier);
            builder.Property(x => x.TestValue).IsRequired(false);
            builder.Property(x => x.Comments).IsRequired(false);
            builder.Property(x => x.BarCode).IsRequired(false);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.ParentRegistrationId).IsRequired(false);
            builder.Property(x => x.interfaceispicked).IsRequired(false);
            builder.Property(x => x.IsUploadAvail).IsRequired(false);
            builder
             .HasOne(e => e.BloodBankRegistration)
             .WithMany(e => e.Results)
             .HasForeignKey(e => e.BloodBankRegistrationId)
             .IsRequired();
        }
    }
}