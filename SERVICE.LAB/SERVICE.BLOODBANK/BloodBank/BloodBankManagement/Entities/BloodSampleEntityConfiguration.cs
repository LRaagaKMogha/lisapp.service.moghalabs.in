using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodSampleEntityConfiguration : IEntityTypeConfiguration<BloodSample>
    {
        public void Configure(EntityTypeBuilder<BloodSample> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.RegistrationId).IsRequired(true);
            builder.Property(x => x.SampleTypeId).IsRequired(true);
            builder.Property(x => x.UnitCount).IsRequired(true);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.ParentRegistrationId).IsRequired(false);
            builder.Property(x => x.PatientId).IsRequired(true);
            builder.Property(x => x.Tests).IsRequired(false);
        }
    }
}