using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasterManagement.Models;

namespace MasterManagement.Entities
{
    public class LookupEntityConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.Property( x=> x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedDateTime).IsRequired(false);
            builder.Property(x => x.LastModifiedDateTime).IsRequired(false);
            builder.HasNoDiscriminator()
                .HasKey(da => da.Id);

        }

    }
}