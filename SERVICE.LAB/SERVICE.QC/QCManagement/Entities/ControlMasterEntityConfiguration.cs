using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class ControlMasterEntityConfiguration : IEntityTypeConfiguration<ControlMaster>
    {
        public void Configure(EntityTypeBuilder<ControlMaster> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.Notes).IsRequired(false);
            builder.Property(x => x.PreparationDateTime).IsRequired(false);
            builder.Property(x => x.PreparedBy).IsRequired(false);
            builder.Property(x => x.PreparedByUserName).IsRequired(false);
            builder.Property(x => x.StorageTemperature).IsRequired(false);
            builder.Property(x => x.AliquoteCount).IsRequired(false);
        }
    }
}