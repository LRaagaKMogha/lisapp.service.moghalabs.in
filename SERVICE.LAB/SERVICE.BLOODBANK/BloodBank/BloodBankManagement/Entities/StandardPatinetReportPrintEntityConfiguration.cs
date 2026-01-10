using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class StandardPatinetReportPrintEntityConfiguration : IEntityTypeConfiguration<GetPatientPrintReport>
    {
        public void Configure(EntityTypeBuilder<GetPatientPrintReport> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.RowNo);
            builder.Property(x => x.Sno).IsRequired(true);
            builder.Property(x => x.PatientRecord).IsRequired(false);
            builder.Property(x => x.Labno).IsRequired(false);
            builder.Property(x => x.MRONo).IsRequired(false);
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.Sex).IsRequired(false);
            builder.Property(x => x.DOB).IsRequired(false);
            builder.Property(x => x.Sample1).IsRequired(false);
            builder.Property(x => x.Sample1Date).IsRequired(false);
            builder.Property(x => x.Sample2).IsRequired(false);
            builder.Property(x => x.Sample2Date).IsRequired(false);
            builder.Property(x => x.AntibodyScreen).IsRequired(false);
            builder.Property(x => x.Remarks).IsRequired(false);
            builder.Property(x => x.LastABSC).IsRequired(false);
            builder.Property(x => x.LastAbscDesc).IsRequired(false);
            builder.Property(x => x.LastPOS).IsRequired(false);
            builder.Property(x => x.LastPOSDesc).IsRequired(false);
            builder.Property(x => x.PatientComment).IsRequired(false);
            builder.Property(x => x.KnowAntibodies).IsRequired(false);
            builder.Property(x => x.SpecialReq).IsRequired(false);
            builder.Property(x => x.ValiditySplReq).IsRequired(false);
            builder.Property(x => x.AdditionalReq).IsRequired(false);
            builder.Property(x => x.TransfusionReaction).IsRequired(false);
            builder.Property(x => x.DonationId).IsRequired(false);
            builder.Property(x => x.ProductCode).IsRequired(false);
            builder.Property(x => x.PhenoType).IsRequired(false);
            builder.Property(x => x.DonationBarcodeId).IsRequired(false);
            builder.Property(x => x.Barcode2).IsRequired(false);
            builder.Property(x => x.BloodGroup).IsRequired(false);
            builder.Property(x => x.Quantity).IsRequired(false);
            builder.Property(x => x.ExpiryDate).IsRequired(false);
            builder.Property(x => x.CompatibleResult).IsRequired(false);
            builder.Property(x => x.Volume).IsRequired(false);
            builder.Property(x => x.Sample1BldGrp).IsRequired(false);
            builder.Property(x => x.Sample1BldGrpType).IsRequired(false);
            builder.Property(x => x.Sample2BldGrp).IsRequired(false);
            builder.Property(x => x.Sample2BldGrpType).IsRequired(false);
        }
    }
}