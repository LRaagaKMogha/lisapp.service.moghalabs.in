using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasterManagement.Models;

namespace MasterManagement.Entities
{
    public class NurseEntityConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.EmployeeId).IsRequired(true);
            builder.HasNoDiscriminator()
                .HasKey(da => da.Id);

        }

    }
}