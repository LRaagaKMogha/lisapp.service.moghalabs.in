using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BloodBankRegistrationEntityConfiguration : IEntityTypeConfiguration<BloodBankRegistration>
    {
        public void Configure(EntityTypeBuilder<BloodBankRegistration> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.RegistrationId);
            builder.Property(x => x.ProductTotal)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.WardId).IsRequired(false);
            builder.Property(x => x.ClinicId).IsRequired(false);
            builder.Property(x => x.DoctorId).IsRequired(false);
            builder.Property(x => x.ClinicalDiagnosisId).IsRequired(false);
            builder.Property(x => x.ClinicalDiagnosisOthers).IsRequired(false);
            builder.Property(x => x.IndicationOfTransfusionOthers).IsRequired(false);
            builder.Property(x => x.DoctorOthers).IsRequired(false);
            builder.Property(x => x.DoctorMCROthers).IsRequired(false);
            builder.Property(x => x.NurseId).IsRequired(false);
            builder.Property(x => x.IssuingComments).IsRequired(false);
            builder.Property(x => x.ModifiedByUserName).IsRequired(false);
            builder.Property(x => x.SampleReceivedDateTime).IsRequired(false);
            builder.Property(x => x.LabAccessionNumber).IsRequired(true);
            builder.Property(x => x.VisitId).IsRequired(false);
            builder
                .HasOne(e => e.BloodBankPatient)
                .WithMany(e => e.BloodBankRegistrations)
                .HasForeignKey(e => e.BloodBankPatientId)
                .IsRequired();
        }
    }
}