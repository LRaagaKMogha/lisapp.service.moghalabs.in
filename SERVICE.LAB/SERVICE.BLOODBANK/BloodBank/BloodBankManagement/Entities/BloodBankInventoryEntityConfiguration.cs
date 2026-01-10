using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodBankInventoryEntityConfiguration : IEntityTypeConfiguration<BloodBankInventory>
    {
        public void Configure(EntityTypeBuilder<BloodBankInventory> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.Identifier);
            builder.Property(x => x.BatchId).IsRequired(true).HasDefaultValueSql("NEXT VALUE FOR BatchId");
            builder.Property(x => x.DonationId).IsRequired(true);
            builder.Property(x => x.CalculatedDonationId).IsRequired(false);
            builder.Property(x => x.ProductCode).IsRequired(true);
            builder.Property(x => x.ExpirationDateAndTime).IsRequired(true);
            builder.Property(x => x.AboOnLabel).IsRequired(true);
            builder.Property(x => x.Volume).IsRequired(true);
            builder.Property(x => x.AntiAGrading).IsRequired(false);
            builder.Property(x => x.AntiBGrading).IsRequired(false);
            builder.Property(x => x.AntiABGrading).IsRequired(false);
            builder.Property(x => x.AboResult).IsRequired(false);
            builder.Property(x => x.Comments).IsRequired(false);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.Antibodies).IsRequired(false);
            builder.Property(x => x.ModifiedProductId).IsRequired(true);
            builder.Property(x => x.AboPerformedByDateTime).IsRequired(false);
        }  
    }
}