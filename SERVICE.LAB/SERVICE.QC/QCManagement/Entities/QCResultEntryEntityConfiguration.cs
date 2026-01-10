using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class QCResultEntryEntityConfiguration : IEntityTypeConfiguration<QCResultEntry>
    {
        public void Configure(EntityTypeBuilder<QCResultEntry> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.ObservedValue)
                .IsRequired(true)
                .HasColumnType("decimal(18,3)");
            builder.Property(x => x.Comments).IsRequired(false);
            builder.Property(x => x.BatchId).IsRequired(false);
            // builder
            //     .HasOne(e => e.TestControlSamples)
            //     .WithMany(e => e.QCResultEntries)
            //     .HasForeignKey(e => e.TestControlSamplesID)
            //     .IsRequired();
        }
    }
}