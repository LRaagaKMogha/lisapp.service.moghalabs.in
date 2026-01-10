using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodBankInventoryTransactionEntityConfiguration : IEntityTypeConfiguration<BloodBankInventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<BloodBankInventoryTransaction> builder)
        {
            builder.HasNoDiscriminator().HasKey(x => x.Identifier);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.InventoryId).IsRequired(true);
            builder.Property(x => x.InventoryStatus).IsRequired(true);
            builder.Property(x => x.Comments).IsRequired(true);
            builder.Property(x => x.ModifiedBy).IsRequired(true);
            builder
                .HasOne(e => e.BloodBankInventory)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.InventoryId)
                .IsRequired();
        }
    }
}