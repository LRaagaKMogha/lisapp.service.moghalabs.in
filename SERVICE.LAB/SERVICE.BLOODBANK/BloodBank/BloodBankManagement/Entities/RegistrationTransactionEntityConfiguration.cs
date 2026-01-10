using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class RegistrationTransactionEntityConfiguration : IEntityTypeConfiguration<RegistrationTransaction>
    {
        public void Configure(EntityTypeBuilder<RegistrationTransaction> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.RegistrationId).IsRequired(true);
            builder.Property(x => x.RegistrationStatus).IsRequired(true);
            builder.Property(x => x.Comments).IsRequired(true);
            builder.Property(x => x.ModifiedBy).IsRequired(true);
            builder.Property(x => x.EntityType).IsRequired(false);
            builder.Property(x => x.EntityId).IsRequired(false);
            builder.Property(x => x.EntityAction).IsRequired(false);
            builder
                .HasOne(e => e.BloodBankRegistration)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.RegistrationId)
                .IsRequired()
                ;
        }
    }
}