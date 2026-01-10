using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class SchedulerEntityConfiguration : IEntityTypeConfiguration<Scheduler>
    {
        public void Configure(EntityTypeBuilder<Scheduler> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
        }
    }
}