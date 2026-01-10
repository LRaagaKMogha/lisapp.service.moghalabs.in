using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class StandardPatinetReportEntityConfiguration : IEntityTypeConfiguration<StandardPatinetReport>
    {
        public void Configure(EntityTypeBuilder<StandardPatinetReport> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.RowNo);
            builder.Property(x => x.RegistrationId).IsRequired(true);
            builder.Property(x => x.LabAccessionNo).IsRequired(false);
            builder.Property(x => x.PatientName).IsRequired(false);
            builder.Property(x => x.VisitId).IsRequired(false);
            builder.Property(x => x.NRICNumber).IsRequired(false);
            builder.Property(x => x.Gender).IsRequired(false);
            builder.Property(x => x.DOB).IsRequired(false);
            builder.Property(x => x.RegDTTM).IsRequired(false);
            builder.Property(x => x.ProductName).IsRequired(false);
            builder.Property(x => x.TestId).IsRequired(false);
            builder.Property(x => x.TestName).IsRequired(false);
            builder.Property(x => x.Result).IsRequired(false);
            builder.Property(x => x.DonorID).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(false);
            builder.Property(x => x.ModifiedBy).IsRequired(false);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.TotalRecords).IsRequired(false);
            builder.Property(x => x.PageIndex).IsRequired(false);
            builder.Property(x => x.InventoryDonationId).IsRequired(false);
            builder.Property(x => x.CheckedBy).IsRequired(false);
            builder.Property(x => x.ExpirationDateAndTime).IsRequired(false);
            builder.Property(x => x.Volume).IsRequired(false);
            builder.Property(x => x.InventoryAboOnLabel).IsRequired(false);
            builder.Property(x => x.NationalityId).IsRequired(false);
            builder.Property(x => x.RaceId).IsRequired(false);
            builder.Property(x => x.ResidenceStatusId).IsRequired(false);
            builder.Property(x => x.Nationality).IsRequired(false);
            builder.Property(x => x.Race).IsRequired(false);
            builder.Property(x => x.Residence).IsRequired(false);
            builder.Property(x => x.GenderId).IsRequired(false);
            builder.Property(x => x.UnitAttribute).IsRequired(false);
            builder.Property(x => x.CompatibilityResults).IsRequired(false);
            builder.Property(x => x.Remarks).IsRequired(false);
            builder.Property(x => x.CompatibilityValidTill).IsRequired(false);
            builder.Property(x => x.IssuedDateAndTime).IsRequired(false);
            builder.Property(x => x.PatientBloodGroup).IsRequired(false);
            builder.Property(x => x.PatientAntibodyScreen).IsRequired(false);
            builder.Property(x => x.PatientSpecialInstructions).IsRequired(false);
            builder.Property(x => x.ProductCode).IsRequired(false);
            builder
                .HasOne(e => e.objReportPrint).WithMany().HasForeignKey(e=>e.RegistrationId).IsRequired();
        }
    }
}