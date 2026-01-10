using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodBankPatientEntityConfiguration : IEntityTypeConfiguration<BloodBankPatient>
    {
        public void Configure(EntityTypeBuilder<BloodBankPatient> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.Identifier);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.AntibodyScreening).IsRequired(false);
            builder.Property(x => x.AntibodyIdentified).IsRequired(false);
            builder.Property(x => x.ColdAntibodyIdentified).IsRequired(false);
            builder.Property(x => x.BloodGroupingDateTime).IsRequired(false);
            builder.Property(x => x.AntibodyScreeningDateTime).IsRequired(false);
            builder.Property(x => x.LatestAntibodyScreeningDateTime).IsRequired(false);
            builder.Property(x => x.Comments).IsRequired(false);
        }
    }
}