using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class MicroQCMasterEntityConfiguration : IEntityTypeConfiguration<MicroQCMaster>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MicroQCMaster> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.PositiveStrainIds).IsRequired(false);
            builder.Property(x => x.NegativeStrainIds).IsRequired(false);
        }
    }
}
