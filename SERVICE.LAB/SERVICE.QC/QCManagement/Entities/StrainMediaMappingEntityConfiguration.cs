using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class StrainMediaMappingEntityConfiguration : IEntityTypeConfiguration<StrainMediaMapping>
    {
        public void Configure(EntityTypeBuilder<StrainMediaMapping> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
        }
    }
}