using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QCManagement.Models;

namespace QCManagement.Entities
{
    public class TestControlSamplesEntityConfiguration : IEntityTypeConfiguration<TestControlSamples>
    {
        public void Configure(EntityTypeBuilder<TestControlSamples> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.TestID).IsRequired(false);
            builder.Property(x => x.SubTestID).IsRequired(false);
            builder.Property(x => x.ParameterName).IsRequired(false);
            builder.Property(x => x.StartTime).IsRequired(false);
            builder.Property(x => x.EndTime).IsRequired(false);
            builder.Property(x => x.UomText2).IsRequired(false);
            builder.Property(x => x.UomText).IsRequired(false);
            builder
            .HasOne(e => e.ControlMaster)
            .WithMany(e => e.TestControlSamples)
            .HasForeignKey(e => e.ControlID)
            .IsRequired();
            builder.Property(x => x.TargetRangeMin)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");
            builder.Property(x => x.TargetRangeMax)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");
            builder.Property(x => x.Mean)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");
            builder.Property(x => x.SD)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");
            builder.Property(x => x.Cv)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");
        }
    }
}