using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBankManagement.Entities
{
    public class BBTblReportMasterDetailsEntityConfiguration : IEntityTypeConfiguration<BBTblReportMasterDetails>
    {
        public void Configure(EntityTypeBuilder<BBTblReportMasterDetails> builder)
        {
            builder.HasNoDiscriminator().HasKey(da => da.ReportNo);
            builder.Property(x => x.ReportKey).IsRequired(true);
            builder.Property(x => x.ReportName).IsRequired(false);
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.ProcedureName).IsRequired(false);
            builder.Property(x => x.ReportPath).IsRequired(false);
            builder.Property(x => x.ExportPath).IsRequired(false);
            builder.Property(x => x.ExportURL).IsRequired(false);
            builder.Property(x => x.Parameterstring).IsRequired(false);
            builder.Property(x => x.VenueNo).IsRequired(false);
            builder.Property(x => x.VenueBranchNo).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(false);
        }
    }
}