using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.EntityConfigurations
{
    public class StrainMasterEntityConfiguration : IEntityTypeConfiguration<StrainMaster>
    {
        public void Configure(EntityTypeBuilder<StrainMaster> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
        }
    }
}
